using System.Threading.Tasks;
using Xunit;
using Zammad.Client.Resources;

namespace Zammad.Client.IntegrationTests
{
    [TestCaseOrderer("Zammad.Client.IntegrationTests.TestOrderer", "Zammad.Client.IntegrationTests")]
    public class OrganizationClientTest
    {
        private static int NotFromTestOrganizationCount { get; set; } = 0;
        private static int KrustyBurgerId { get; set; } = 0;
        private static int SpringfieldNuclearPowerPlantId { get; set; } = 0;
        private static int SpringfieldElementarySchoolId { get; set; } = 0;

        [Fact, Order(TestOrder.OrganizationListBefore)]
        public async void Organization_List_Before_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();

            var organizationList = await client.GetOrganizationListAsync(0, 100);

            Assert.NotNull(organizationList);
            NotFromTestOrganizationCount = organizationList.Count;
        }

        [Fact, Order(TestOrder.OrganizationCreate)]
        public async void Organization_Create_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();
            
            var organization1 = await client.CreateOrganizationAsync(new Organization
            {
                Name = "Krusty Burger",
                Shared = true,
                Domain = "krustyburger.com",
                DomainAssignment = true,
                Active = true
            });

            var organization2 = await client.CreateOrganizationAsync(new Organization
            {
                Name = "Springfield Nuclear Power Plant",
                Shared = true,
                Domain = "nuclearpowerplant.com",
                DomainAssignment = true,
                Active = true
            });

            var organization3 = await client.CreateOrganizationAsync(new Organization
            {
                Name = "Springfield Elementary School",
                Shared = true,
                Domain = "springfield-elementaryschool.com",
                DomainAssignment = true,
                Active = true
            });

            Assert.NotNull(organization1);
            Assert.NotNull(organization2);
            Assert.NotNull(organization3);

            KrustyBurgerId = organization1.Id;
            SpringfieldNuclearPowerPlantId = organization2.Id;
            SpringfieldElementarySchoolId = organization3.Id;
        }

        [Fact, Order(TestOrder.OrganizationList)]
        public async void Organization_List_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();

            var organizationList = await client.GetOrganizationListAsync(0, 100);

            Assert.Equal(NotFromTestOrganizationCount + 3, organizationList.Count);
        }

        [Fact, Order(TestOrder.OrganizationDetail)]
        public async Task Organization_Detail_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();

            var organization = await client.GetOrganizationAsync(KrustyBurgerId);

            Assert.Equal(KrustyBurgerId, organization.Id);
            Assert.Equal("Krusty Burger", organization.Name);
            Assert.True(organization.Shared);
            Assert.Equal("krustyburger.com", organization.Domain);
            Assert.True(organization.DomainAssignment);
            Assert.True(organization.Active);
        }

        [Fact, Order(TestOrder.OrganizationSearch)]
        public async Task Organization_Search_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();

            await Task.Delay(5000); // Wait for Zammad search indexer
            var organizationSearch = await client.SearchOrganizationAsync("Krusty Burger", 20);

            Assert.Equal(1, organizationSearch.Count);
            Assert.Equal(KrustyBurgerId, organizationSearch[0].Id);
        }

        [Fact, Order(TestOrder.OrganizationUpdate)]
        public async Task Organization_Update_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();

            var organization1 = await client.GetOrganizationAsync(SpringfieldElementarySchoolId);
            organization1.Domain = "springfieldelementaryschool.com";

            var organization2 = await client.UpdateOrganizationAsync(SpringfieldElementarySchoolId, organization1);

            Assert.Equal(organization1.Domain, organization2.Domain);
        }

        [Fact, Order(TestOrder.OrganizationDelete)]
        public async Task Organization_Delete_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateOrganizationClient();

            var organization1 = await client.DeleteOrganizationAsync(KrustyBurgerId);
            var organization2 = await client.DeleteOrganizationAsync(SpringfieldNuclearPowerPlantId);
            var organization3 = await client.DeleteOrganizationAsync(SpringfieldElementarySchoolId);

            Assert.True(organization1);
            Assert.True(organization2);
            Assert.True(organization3);
        }

    }
}
