using System;
using System.Net.Http;
using Zammad.Client.Core;
using Zammad.Client.Group;
using Zammad.Client.OnlineNotification;
using Zammad.Client.Organization;
using Zammad.Client.Ticket;
using Zammad.Client.User;

namespace Zammad.Client
{
    public class ZammadAccount
    {
        private string _schema;
        private string _host;
        private string _user;
        private string _password;
        private string _token;
        private ZammadAuthMethod _authMethod;

        public ZammadAccount(string schema, string host, ZammadAuthMethod authMethod, string user, string password, string token)
        {
            if (string.Compare(schema, "http", true) != 0 && string.Compare(schema, "https", true) != 0)
            {
                throw new ArgumentException($"Parameter {nameof(schema)} must be http or https.");
            }

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException($"Parameter {nameof(host)} must be set and can not be empty.");
            }

            if (authMethod == ZammadAuthMethod.Basic)
            {
                if (string.IsNullOrEmpty(user))
                {
                    throw new ArgumentNullException($"Parameter {nameof(user)} must be set and can not be empty.");
                }

                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException($"Parameter {nameof(password)} must be set and can not be empty.");
                }
            }
            else if (authMethod == ZammadAuthMethod.Token)
            {
                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException($"Parameter {nameof(token)} must be set and can not be empty.");
                }
            }

            _schema = schema;
            _host = host;
            _token = token;
            _user = user;
            _password = password;
            _authMethod = authMethod;
        }

        public string Schema => _schema;
        public string Host => _host;
        public string User => _user;
        public string Password => _password;
        public string Token => _token;
        public ZammadAuthMethod AuthMethod => _authMethod;

        internal protected virtual HttpClient CreateHttpClient()
        {
            return new HttpClient(new HttpClientHandler(), true);
        }

        public static ZammadAccount CreateBasicAccount(string schema, string host, string user, string password)
        {
            return new ZammadAccount(schema, host, ZammadAuthMethod.Basic, user, password, null);
        }

        public static ZammadAccount CreateTokenAccount(string schema, string host, string token)
        {
            return new ZammadAccount(schema, host, ZammadAuthMethod.Token, null, null, token);
        }

        public GroupClient CreateGroupClient()
        {
            return new GroupClient(this);
        }

        public OnlineNotificationClient CreateOnlineNotificationClient()
        {
            return new OnlineNotificationClient(this);
        }

        public OrganizationClient CreateOrganizationClient()
        {
            return new OrganizationClient(this);
        }

        public TicketClient CreateTicketClient()
        {
            return new TicketClient(this);
        }

        public UserClient CreateUserClient()
        {
            return new UserClient(this);
        }
    }
}
