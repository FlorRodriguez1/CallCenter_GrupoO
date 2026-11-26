using System.Net.Mail;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        // Sin parámetros: toma host, puerto y credenciales del Web.config
        private SmtpClient server = new SmtpClient();

        public void ArmarCorreo(string destinatario, string asunto, string cuerpoHtml)
        {
            email = new MailMessage();   // el remitente sale de "from" del Web.config
            email.To.Add(destinatario);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpoHtml;
        }

        public void EnviarEmail()
        {
            server.Send(email);
        }
    }
}