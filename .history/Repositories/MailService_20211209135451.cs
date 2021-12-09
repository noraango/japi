using System.Collections.Generic;
using api.Repositories.Data;
using api.Repositories.Dependencies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.DBModels;
using System;
using MailKit.Net.Smtp;
using MimeKit;
namespace api.Repositories
{
    public class MailService
    {
        private readonly Context _context;

        public Task Send(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("japstorefu@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };
            email.InReplyTo = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss");
            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                smtp.Authenticate("japstorefu@gmail.com", "JapStore123");
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}