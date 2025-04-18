using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Wayni.Communication.Mail;

namespace EcoTrueke.Infrastructure.Communications
{
    public class MailerService : IMailerService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string MAIL_ECOTRUEKE_TEMPLATE = "Templates/EcoTruekeTemplate.html";
        private const string MAIL_ECOTRUEKE_RESET_PASSWORD = "Templates/EcoTruekeResetPassword.html";

        public MailerService(IConfiguration configuration, IHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task SendSupportCallMail(string recipient, string subject, string hidden, string tittle, string content, MailSettings? mailSetting = null)
        {
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var logoUrl = Path.Combine(baseUrl, "Images/logo-horizontal.webp");
            var mail = File.ReadAllText(Path.Combine(_environment.ContentRootPath, MAIL_ECOTRUEKE_TEMPLATE))
                .Replace("{LogoUrl}", logoUrl)
                .Replace("{Title}", tittle)
                .Replace("{Hidden}", hidden)
                .Replace("{Content}", content)
                .Replace("{MailCode}", Guid.NewGuid().ToString());

            mailSetting = new MailSettings()
            {
                Adjuntos = null,
                Asunto = subject,
                Contenido = mail,
                Destinatario = new string[] { recipient },
                Email = mailSetting?.Email ?? _configuration.GetSection("Mail").GetSection("SMTPEmail").Value,
                EnableSsl = bool.Parse(_configuration.GetSection("Mail").GetSection("SMTPSSL").Value),
                Host = mailSetting?.Host ?? _configuration.GetSection("Mail").GetSection("SMTPHost").Value,
                Nombre = mailSetting?.Nombre ?? "EcoTrueke",
                Username = mailSetting?.Username ?? _configuration.GetSection("Mail").GetSection("SMTPUser").Value,
                Password = mailSetting?.Password ?? _configuration.GetSection("Mail").GetSection("SMTPPassword").Value,
                Port = int.Parse(_configuration.GetSection("Mail").GetSection("SMTPPort").Value)
            };
            await Mailer.SendAsync(mailSetting);
        }

        public async Task<Result> SendMailResetPassword(User user, Person person, string newPassword)
        {
            var content = File.ReadAllText(Path.Combine(_environment.ContentRootPath, MAIL_ECOTRUEKE_RESET_PASSWORD))
                .Replace("{Password}", newPassword);

            await SendSupportCallMail(user.Email, "Solicitud de contraseña", "Restablecer contraseña", $"Hola {person.FirstName} {person.LastName}", content);

            return new Result { Code = Result.OK };
        }
    }
}
