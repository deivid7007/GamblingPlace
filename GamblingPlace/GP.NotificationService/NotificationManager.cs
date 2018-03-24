﻿using GP.LogService;
using GP.LogService.Domain;
using GP.NotificationService.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GP.UserService.Domain;

namespace GP.NotificationService
{
    public class NotificationManager : INotificationActor
    {
        private ILog _logger = Logger.GetInstance;
        private string confirmationEmailUrl = "https://gamblingplace.azurewebsites.net/account/ValidateEmail";
        //private string confirmationEmailUrl = "http://localhost:57896/account/ValidateEmail";
        private string passwordResetUrl = "https://itgigs.azurewebsites.net/account/PasswordReset";


        public async Task SendChangePasswordEmailAsync(User user)
        {
            string callbackUrl = $"{passwordResetUrl}?userId={user.Id}&validationCode={user.ValidationCode}";
            string link = $"<a href='{callbackUrl}'>here</a>!";
            await SendEmailAsync(user.Email, "GamblingPlace change password request",
                $"Please reset your password by clicking {link}");
        }

        public async Task SendConfirmationEmailAsync(User user)
        {
            string callbackUrl = $"{confirmationEmailUrl}?userId={user.Id}&validationCode={user.ValidationCode}";
            string link = $"<a href='{callbackUrl}'>here</a>!";
            await SendEmailAsync(user.Email, "GamblingPlace registration request",
                $"To confirm your account click  -> {link}");
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("srednogortsi@gmail.com", "ludaka1234")
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("whoever@me.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                await _logger.LogCustomExceptionAsync(ex, null);
                throw new ApplicationException($"Unable to load : '{ex.Message}'.");
            }
        }
    }
}
