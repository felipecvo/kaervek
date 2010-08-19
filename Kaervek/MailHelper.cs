namespace Kaervek {
  using System.Net.Mail;
  using System.Net;
  using System;
using System.Collections.Generic;

  public static class MailHelper {
    public static void SendMail(MailAddress from, MailAddress to, string subject, string message) {
      var mail = new MailMessage(from, to);

      mail.Subject = subject;
      mail.IsBodyHtml = true;
      mail.Body = message;

      Send(mail);
    }

    public static void SendMail(string from, string to, string subject, string message) {
      SendMail(from, to, subject, message, new Dictionary<string, string>());
    }

    public static void SendMail(string from, string to, string subject, string message, Dictionary<string, string> headers) {
      var mail = new MailMessage(from, to);
      mail.Subject = subject;
      if (message.Length > 5000) {
        message = message.Substring(0, 5000);
      }
      mail.Body = message;
      mail.IsBodyHtml = true;

      foreach (var header in headers) {
        mail.Headers.Add(header.Key, header.Value);
      }

      Send(mail);
    }

    private static void Send(MailMessage mail) {
      var client = new SmtpClient();
      client.Send(mail);
    }

  }
}