namespace Wayni.Communication.Mail
{
    public class MailSettings
    {
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public string[] Destinatario { get; set; }
        public List<Tuple<Stream, string>> Adjuntos { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
