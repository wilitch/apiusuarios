using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Infra.Messages.Services
{
    public class EmailMessage
    {
        //atributos
        private const string? _conta = "csharpcoti@outlook.com";
        private const string? _senha = "@Admin12345";
        private const string? _smtp = "smtp-mail.outlook.com";
        private const int _porta = 587;

        public void Send(string to, string subject, string body)
        {
            #region Criando a mensagem do email

            var mailMessage = new MailMessage(_conta, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            #endregion

            #region Enviando o email

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }

            #endregion
        }
    }
}



