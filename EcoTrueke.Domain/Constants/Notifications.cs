namespace EcoTrueke.Domain.Constants
{
    public static class Notifications
    {
        public static class FinishSetup
        {
            public const string Title = "Completa la configuración de tu cuenta";
            public const string Message = "Aún faltan algunos pasos para terminar tu registro. Completa la configuración y aprovecha todas las funciones de EcoTrueke.";
            public const string Type = "info";
            public const string Link = "/dashboard/profile";
        }     
        public static class ExchangeRequest
        {
            public const string Title = "Solicitud de intercambio";
            public const string Message = "Haz recibido una solictud de intecambio";
            public const string Type = "info";
            public const string Link = "/dashboard/my-products/requests";
        }
        public static class OwnerUserAcceptedProposal
        {
            public const string Title = "Propuesta de intercambio";
            public const string Message = "Has aceptado la propuesta de intercambio. Revisa tu correo electrónico y comunícate con el otro usuario.";
            public const string Type = "info";
            public const string Link = "";
        }

        public static class ProposerUserProposal
        {
            public const string Title = "Propuesta de intercambio";
            public const string Message = "Tu propuesta de intercambio ha sido aceptada. Revisa tu correo electrónico y comunícate con el otro usuario.";
            public const string Type = "info";
            public const string Link = "";
        }

    }
}
