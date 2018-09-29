namespace Zammad.Client.IntegrationTests
{
    public class TestOrder
    {
        public const int First = 1;

        public const int OrganizationListBefore = First;
        public const int OrganizationCreate = OrganizationListBefore + 1;
        public const int OrganizationList = OrganizationCreate + 1;
        public const int OrganizationDetail = OrganizationList + 1;
        public const int OrganizationSearch = OrganizationDetail + 1;
        public const int OrganizationUpdate = OrganizationSearch + 1;
        public const int OrganizationDelete = OrganizationUpdate + 1;

        public const int TicketCreate = OrganizationDelete + 1;
        public const int TicketSearch = TicketCreate + 1;
    }
}
