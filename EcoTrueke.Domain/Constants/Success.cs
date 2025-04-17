using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Constants
{
    public static class Success
    {
        public static class User
        {
            public static Result Registered => new()
            {
                Code = Result.CREATED,
                Type = "user_registered",
                Message = "Usuario registrado correctamente."
            };
            public static Result LoggedIn => new()
            {
                Code = Result.OK,
                Type = "user_logged_in",
                Message = "Inicio de sesión exitoso."
            };
        }
    }
}
