using System.Globalization;
using System.Net;
using System.Net.Mail;
namespace Qorpent.Integration.Feed.Mailman {
    /// <summary>
    ///     Подсистема автоматической рассылки сообщений
    /// </summary>
    public static class MailmanSender {
        /// <summary>
        /// 
        /// </summary>
        public static void SendMessage()
        {
            var message = new MailMessage();
            message.To.Add("ektos.pro@gmail.com");
            message.Subject = "Test";
            message.From = new MailAddress("info.assoi@ugmk.com");
            message.IsBodyHtml = false;
            message.Body = MailmanMongoDbSerializer.TestItem.Text;
            var smtp = new SmtpClient
            {
                Host = "post.ugmk.com",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("assoiws_8", "rhfcysq$8")
            };
            smtp.Send(message);
        }
    }
}
