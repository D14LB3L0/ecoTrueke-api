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
                Type = "updated",
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
            public static Result MarkAsRead => new()
            {
                Code = Result.OK,
                Type = "mark_as_read",
                Message = "Las notificaciones se han marcado como leídas correctamente."
            };
            public static Result DeleteNotification => new()
            {
                Code = Result.OK,
                Type = "notification_delete",
                Message = "La notificación se ha eliminado correctamente."
            };
        }

        public static class Product
        {
            public static Result RegisteredProduct => new()
            {
                Code = Result.OK,
                Type = "registered_product",
                Message = "Producto registrado correctamente."
            };
            public static Result GetPaginatedProducts => new()
            {
                Code = Result.OK,
                Type = "get_paginated_products",
                Message = "Productos obtenidos correctamente."
            };

            public static Result ProductFound => new()
            {
                Code = Result.OK,
                Type = "product_found",
                Message = "Producto obtenido correctamente."
            };

            public static Result ProductDeleted => new()
            {
                Code = Result.OK,
                Type = "product_deleted",
                Message = "El producto se ha eliminado correctamente."
            };

            public static Result UpdatedProduct => new()
            {
                Code = Result.OK,
                Type = "updated",
                Message = "Los datos se han actualizado correctamente."
            };
        }

        public static class Proposal
        {
            public static Result RegisterProposal => new()
            {
                Code = Result.OK,
                Type = "product_found",
                Message = "Propuesta enviada correctamente."
            }; 
            public static Result AcceptProposal => new()
            {
                Code = Result.OK,
                Type = "accept_proposal",
                Message = "Se aceptó la propuesta correctamente."
            }; 
            public static Result RejectProposal => new()
            {
                Code = Result.OK,
                Type = "reject_proposal",
                Message = "Se rechazó la propuesta correctamente."
            };
            public static Result GetProposal => new()
            {
                Code = Result.OK,
                Type = "get_proposal",
                Message = "Los datos se han obtenido correctamente."
            };
        }
    }
}
