namespace EcoTrueke.Domain.Constants
{
    public static class Types
    {
        public static class AccountStatus
        {
            public const string Active = "active";
            public const string Suspended = "suspended";
        }

        public static class ProductStatus
        {
            public const string Pending = "pending"; 
            public const string Active = "active";
            public const string Traded = "traded";
            public const string Sold = "sold";
            public const string Donnated = "donnated";
        }

        public static class ProposalStatus
        {
            public const string Completed = "completed";
            public const string Cancelled = "cancelled";
            public const string Pending = "pending";
            public const string Rejected = "rejected";
            public const string Accepted = "accepted";
        }
    }
}
