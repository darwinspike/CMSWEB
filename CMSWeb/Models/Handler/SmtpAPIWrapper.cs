using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CMSWeb.Models.Handler
{

    public enum NotificationBy { SmtpAdminClients, SmtpClientSites, ApiAdminClients, ApiClientSites };

    public class SmtpAPIWrapper : IDisposable
    {

        private NotificationBy _hostType;
        private SmtpClient _smtpClient = null;
        private SocketLabsSendNotification _socketLabNotification = null;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public SmtpAPIWrapper(NotificationBy hostType)
        {
            //_hostType = hostType;


            //if (hostType == NotificationBy.SmtpAdminClients)
            //{
            //    _smtpClient = SMTP.Smtp_ClientCreaCRM();
            //}
            //else if (hostType == NotificationBy.SmtpClientSites)
            //{
            //    _smtpClient = SMTP.Smtp_ClientCreaClientSites();
            //}
            //else if (hostType == NotificationBy.ApiAdminClients)
            //{
                _socketLabNotification = new SocketLabsSendNotification(hostType);
            //}
            //else if (hostType == NotificationBy.ApiClientSites)
            //{
            //    _socketLabNotification = new SocketLabsSendNotification(hostType);
            //}
        }

        public void Send(MailMessage mailMessage)
        {
            //if ((_hostType == NotificationBy.SmtpClientSites) || (_hostType == NotificationBy.SmtpAdminClients))
            //{
            //    _smtpClient.Send(mailMessage);
            //}
            //else if ((_hostType == NotificationBy.ApiAdminClients) || (_hostType == NotificationBy.ApiClientSites))
            //{
                _socketLabNotification.Send(mailMessage, _hostType);
            //}
        }
    }




}