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
            
            public static Result ChangePassword => new()
            {
                Code = Result.OK,
                Type = "change_password",
                Message = "La contraseña se ha actualizado correctamente."
            };      
            public static Result AccountDeleted => new()
            {
                Code = Result.OK,
                Type = "account_deleted",
                Message = "La cuenta se ha eliminado correctamente."
            };
        }

        public static class Person
        {
            public static Result UpdatedPerson => new()
            {
                Code = Result.OK,
                Type = "account_deleted",
                Message = "Los datos se han actualizado correctamente."
            };
        }

        public static class Notification
        {
            public static Result GetPaginatedNotifications => new()
            {
                Code = Result.OK,
                Type = "get_paginated_notifications",
                Message = "Notificaciones obtenidas correctamente."
            };
        }
    }
}
