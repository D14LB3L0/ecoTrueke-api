using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Constants
{
    public static class Errors
    {
        public static class User
        {
            public static Result NotFoundUser => new()
            {
                Code = Result.NOT_FOUND,
                Type = "not_found_user",
                Message = "El usuario no ha sido encontrado."
            };        
            public static Result PasswordsDoNotMatch => new()
            {
                Code = Result.UNPROCESSABLE_ENTITY,
                Type = "passwords_do_not_match",
                Message = "Las contraseñas no coinciden."
            };      
            public static Result IncorrectPassword => new()
            {
                Code = Result.UNPROCESSABLE_ENTITY,
                Type = "incorrect_password",
                Message = "La contraseña es incorrecta."
            };
            public static Result AlreadyExists => new()
            {
                Code = Result.UNPROCESSABLE_ENTITY,
                Type = "user_already_exists",
                Message = "El usuario ya existe."
            };   
            public static Result AccountStatutsSuspended => new()
            {
                Code = Result.UNPROCESSABLE_ENTITY,
                Type = "account_status_suspended",
                Message = "La cuenta está suspendida."
            };
        }
    }
}
