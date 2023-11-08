//using System;
//using SendGrid;
//using SendGrid.Helpers.Mail;

//namespace FixMaster
//{
//	public class EmailService
//	{
//        private readonly string _sendGridApiKey;
//        private readonly string _senderEmail;

//        public EmailService(IConfiguration configuration)
//        {
//            _sendGridApiKey = configuration["SendGridApiKey"];
//            _senderEmail = configuration["SenderEmail"];
//        }

//        public async Task SendBookingConfirmationEmail(string recipientEmail, string bookingDetails)
//        {
//            var client = new SendGridClient(_sendGridApiKey);
//            var msg = new SendGridMessage
//            {
//                From = new EmailAddress(_senderEmail, "Your Application Name"),
//                Subject = "Booking Confirmation",
//                PlainTextContent = "Your booking has been confirmed!",
//                HtmlContent = $"<p>{bookingDetails}</p>"
//            };
//            msg.AddTo(new EmailAddress(recipientEmail));

//            var response = await client.SendEmailAsync(msg);
//        }
//    }
//}

