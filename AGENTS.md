# Agent Instructions

.NET client for the [Zammad](https://zammad.org/) helpdesk REST API, using `System.Text.Json`. It is a hard fork of
`Zammad.Client`, which used `Newtonsoft.Json`.

## Layout

- `src/Zammad.Client.SystemTextJson`: the library (NuGet package). Targets `netstandard2.0;net8.0;net9.0;net10.0;net11.0`.
- `src/Zammad.Client.SystemTextJson.Extensions`: DI registration (`AddZammadClient`), a separate package.
- `src/Zammad.Example`: sample app.
- `test/Zammad.Client.Tests`: unit tests, mostly deserialization of recorded JSON responses.
- `test/Zammad.Client.IntegrationTests`: tests against a real Zammad stack started with Testcontainers.

## Commands

```bash
dotnet tool restore                                   # csharpier, husky, nbgv (manifest: dotnet-tools.json)
dotnet build
dotnet run --project test/Zammad.Client.Tests         # unit tests, < 1s
dotnet run --project test/Zammad.Client.IntegrationTests -- --treenode-filter "/*/*/OrganizationTests/*"
dotnet csharpier format <files>                       # also runs as a husky pre-commit hook
```

- Tests use TUnit on Microsoft.Testing.Platform (set in `global.json`), not VSTest. VSTest arguments such as
  `--filter` don't work. Use `--treenode-filter "/<assembly>/<namespace>/<class>/<test>"` instead.
- Integration tests need Docker. Starting the Zammad stack (Rails, Postgres, Redis, Elasticsearch, memcached, nginx,
  scheduler, websocket) takes **about 2–3 minutes** before any test runs. Run a filtered subset if you can, and use a
  long command timeout (10 min).
- Build output goes to `artifacts/` (`UseArtifactsOutput`), not `bin/`/`obj/` in the project folders.

## Build rules that bite

- `TreatWarningsAsErrors` is on, and the library projects (not the test projects) run the Roslynator, SonarAnalyzer
  and VS Threading analyzers. Every analyzer warning fails the build.
- The library builds for `netstandard2.0` **and** modern .NET, which have different nullable annotations. For example,
  `string.IsNullOrWhiteSpace` doesn't narrow nullability on netstandard2.0, while Sonar `S8969` rejects the `!`
  operator on net8+. Use pattern matching to satisfy both: `if (value is not { } v || string.IsNullOrWhiteSpace(v))`.
  APIs added after netstandard2.0 (e.g. `ReadAsStringAsync(CancellationToken)`) aren't available.
- Empty `catch (Exception)` blocks are rejected (RCS1075). Catch specific types with a `when` filter.
- Formatting is csharpier, 120 columns.

## Code conventions

- Each API area is one `ZammadClient.<Area>.cs` file. It contains an `I<Area>Service` interface and a
  `public sealed partial class ZammadClient : I<Area>Service` with endpoint constants. New service interfaces must also
  be added to `IZammadClient` in `ZammadClient.cs`.
- IDs are strongly typed (`UserId`, `TicketId`, …) via the `StronglyTypedIds` source generator
  (`Resources/StronglyTypedIds.cs`). Don't use raw `int`s in public signatures.
- HTTP helpers in `ZammadClient.cs`: `GetAsync` returns `default` on 404. All other non-success responses throw
  `ZammadException`, which has `Code`, `Content` (raw body) and `Error` (Zammad's `error_human`/`error`). If a non-null
  result comes back null, throw `LogicException.UnexpectedNullResult`.
- When adding a resource, also add a recorded response under `test/Zammad.Client.Tests/Deserialization/Responses` and
  an `[Arguments(...)]` row in `DeserializationTests`.
- Changes to public behavior, including exception message text, are API changes. Call them out in the PR.

## Zammad behavior that isn't obvious

- **Always send `Accept: application/json`** (`SendRawAsync` does this). Without it, Zammad renders errors as HTML
  pages instead of `{"error": ..., "error_human": ...}`.
- **Delete endpoints return 422 "Can't delete, object has references."** if any row in any table points at the record.
  The check (`Models.references` in Zammad's `lib/models.rb`) looks at `<model>_id` columns, `belongs_to` links, and for
  users also `created_by_id`/`updated_by_id`. The controller also turns *any* error during that check into a 422, so a
  422 on delete isn't always about references.
- **`ObjectLookup`/`TypeLookup` rows are created lazily** (check-then-create inside the request transaction). On a
  fresh database, concurrent requests race on the unique index and one fails with 422 "This object already exists."
  (`PG::UniqueViolation` in the Rails log). `Setup/docker-entrypoint` seeds these rows after the auto wizard.
- **Zammad does a lot of work asynchronously** in the scheduler container (avatar lookups, triggers, escalation
  calculation, search indexing). A record you just created may still be changed by a background job a moment later.
- **Search goes through Elasticsearch and lags behind writes.** Search tests wait `TestSetup.IndexerDelay` and use
  `[Retry]`.
- To check how an endpoint really behaves, read Zammad's source at the version in use (`ZammadImage` in
  `ZammadStackFixture.cs`), e.g. `git clone --depth 1 --branch <version> https://github.com/zammad/zammad.git`. The
  controllers are in `app/controllers`, error rendering in `app/controllers/application_controller/handles_errors.rb`.

## Integration tests

- All test classes share one Zammad instance (`[ClassDataSource<ZammadStackFixture>(Shared = SharedType.PerAssembly)]`)
  and run in parallel. Data persists across tests, so names, emails and logins need a random suffix
  (`TestSetup.RandomString()`, letters only: Zammad treats 6+ digits in a user field as a phone number and
  stores a caller ID that blocks deleting the user). Never assert on global counts.
- Tests within a class are chained with `[DependsOn]` and pass IDs through `static` properties (create → get → update →
  delete). If one step fails, its dependents are skipped.
- **Object manager migrations stop the stack.** After a migration that changes columns, Zammad (`auto_shutdown`) makes
  the railsserver, scheduler and websocket processes exit, and the containers have no restart policy.
  `ObjectTests.ExecuteMigration` is the only test that migrates and calls `zammadStack.RestartAsync()`. Tests that need
  new attributes create them in a step that `ExecuteMigration` depends on (see `CustomFieldTests.CreateAttributes`) and
  depend on `ExecuteMigration` themselves, so the stack restarts once per run.
- Zammad never notifies the user who made a change. `OnlineNotificationTests` creates the ticket on behalf of
  `agent1@example.org` (`GetClientOnBehalfOfAsync`, `X-On-Behalf-Of`) with the admin as owner, then polls until the
  scheduler has created the admin's notification.
- `Setup/docker-entrypoint` is a patched copy of Zammad's `bin/docker-entrypoint` for the pinned image version. It runs
  the auto wizard (`Setup/autowizard.json`, admin `admin@example.org` / `TestPassword1234`) and prints a marker that the
  fixture waits for. When you bump the Zammad image, re-apply the patch on top of the new upstream entrypoint, align
  the other images with the referenced `zammad-docker-compose` commit, and update the version in `README.md`.
  It also sets the period of the `DataPrivacyTaskJob` scheduler to 10 seconds (the seed uses 10 minutes) so the data
  privacy tests finish quickly; re-apply this together with the rest of the patch when you bump the image.
- **The stack has no internet access.** All containers are on an internal Docker network. Only the containers with
  port bindings (nginx, Elasticsearch, Postgres, Redis) are also on a regular network, because Docker doesn't publish
  ports of internal-only containers. The Zammad containers (railsserver, scheduler, websocket, init) can't reach
  anything outside the stack, so outgoing requests (avatar/geo lookups, webhooks, mail) fail with DNS errors.
- Notification emails are written to `tmp/mails/*.eml` inside the containers (`ZAMMAD_MAIL_TO_FILE`), not sent. The
  entrypoint replaces the seeded SMTP notification channel (`host.example.com`) because a failed delivery marks the
  channel as failed, and the health check then stays unhealthy (seen as `RestartAsync` timing out in
  `ExecuteMigration`). The timeout message includes the last health check message.
- **Debugging failures:** for every failed test, the fixture writes the railsserver, scheduler, websocket and nginx logs
  from that test's time window to `<results-directory>/zammad-logs/*.log`. Rails logs the real exception and its stack
  trace there. Console output from tests is *not* shown in CI.

## CI

- `.github/workflows/dotnet.yml` retries the whole `dotnet test` step up to 3 times (`nick-fields/retry`) because the
  integration tests are flaky. A green run can still contain failed attempts; search the job log for
  `Attempt 1 failed`.
- Logs of failed tests are uploaded as the `zammad-logs` artifact, also when a retry made the run green. Download it with
  `gh run download <run-id> -n zammad-logs`.
- Known flaky tests (as of 2026-10): `TicketAccountingTests.CreateTicketAccounting` (500). Root cause not confirmed
  yet. Most likely a race with Zammad's background jobs, see above. The 422 "This object already exists." on
  `DeleteOrganization` was the lazy lookup race described above. The 422 "Can't delete, object has references." on
  `DeleteUser` came from digits in the random suffix (see `TestSetup.RandomString`).
- Actions are pinned by commit SHA with a version comment. Keep it that way; Renovate updates them.

## Versioning and releases

- Versions come from Nerdbank.GitVersioning (`version.json`). Only `master` and `v*` branches produce public
  (non-prerelease) versions.
- NuGet publishing is a manual workflow (`nuget.yml`, `workflow_dispatch`).
- Commit messages follow Conventional Commits (`feat:`, `fix:`, `chore(deps):`).
