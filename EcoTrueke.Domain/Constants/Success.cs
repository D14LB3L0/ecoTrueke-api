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

            public static Result ResetPassword => new()
            {
                Code = Result.OK,
                Type = "reset_password",
                Message = "Se ha enviado un correo con la nueva contraseña generada."
            };
        }
    }
}
