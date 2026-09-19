using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace OnlineBanking
{
    internal class EmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string accountNumber, string recipientEmail, string transactionType, double amount, double newBalance)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _configuration["EmailSettings:FromName"],
                _configuration["EmailSettings:FromEmail"]
            ));
            message.To.Add(new MailboxAddress("Account Owner", recipientEmail));
            message.Subject = $"BSIT 3-1 - {transactionType} Notification";
            message.Body = new TextPart("plain")
            {
                Text = $"Account Number: {accountNumber}\n\n" +
                       $"Transaction Type: {transactionType}\n" +
                       $"Amount: PHP {amount:N2}\n" +
                       $"New Balance: PHP {newBalance:N2}\n\n" +
                       "This is an automated notification."
            };

            using (var client = new SmtpClient())
            {
                client.Connect(
                    _configuration["EmailSettings:SmtpHost"],
                    int.Parse(_configuration["EmailSettings:SmtpPort"]),
                    SecureSocketOptions.StartTls
                );

                client.Authenticate(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                );

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}








        