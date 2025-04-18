using System.Net;
using System.Net.Mail;

namespace Wayni.Communication.Mail
{
    public class Mailer
    {
        public static async Task SendAsync(MailSettings mailSetting)
        {
            try
            {
                var objMailMessage = new MailMessage();
                objMailMessage.From = new MailAddress(mailSetting.Email, mailSetting.Nombre);

                foreach (string Destinatario in mailSetting.Destinatario)
                    objMailMessage.To.Add(Destinatario);

                objMailMessage.Subject = mailSetting.Asunto;
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = mailSetting.Contenido;

                if (mailSetting.Adjuntos?.Count > 0)
                {
                    foreach (var memoryStream in mailSetting.Adjuntos)
                    {
                        StreamWriter streamWriter = new StreamWriter(memoryStream.Item1);
                        await streamWriter.FlushAsync();
                        memoryStream.Item1.Position = 0;
                        objMailMessage.Attachments.Add(new Attachment(memoryStream.Item1, memoryStream.Item2));
                    }
                }

                var smtp = new SmtpClient
                {
                    EnableSsl = mailSetting.EnableSsl,
                    Host = mailSetting.Host,
                    Port = mailSetting.Port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSetting.Username, mailSetting.Password)
                };

                await smtp.SendMailAsync(objMailMessage);
                objMailMessage.Attachments?.Clear();
                objMailMessage.Dispose();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error al enviar el correo.", ex);
            }
        }
    }
}
