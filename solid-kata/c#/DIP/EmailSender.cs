using static System.Console;

namespace DIP
{
    public class EmailSender
    {
        public void Send(Email email) =>
            Write($"To:{email.To}, Subject: {email.Subject}, Message: {email.Message}");
    }
}