using InjectionApi.Sdk.Client;
using InjectionApi.Sdk.Email;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace CMSWeb.Models.Handler
{
    public class SMTP
    {
        public const string HostType_CRM = "host_crm";
        public const string HostType_ClientSites = "host_clientsites";
        public const string HostType_Default = "host_default";
        public const string HostType_Rates = "host_rates";
        public const string HostType_Tests = "host_tests";
        public const string HostType_Chain41 = "host_chain41";

        private string err = string.Empty;

        public const string host_default = "mx3.arginteractive.com";
        public const string host_rates = "mx2.arginteractive.com";
        public const string host_tests = "mx4.arginteractive.com";
        public const string host_chain41 = "mx4.arginteractive.com";
        /// <summary>
        /// Crm notifications. Client site short and long folms.
        /// </summary>
        public const string host_crm = "crm-mail.arginteractive.com";
        /// <summary>
        /// messages from LO sites\ custom forms (standard long and short forms use crm notifications- host_crm)
        /// </summary>
        public const string host_clientsites = "mx4.arginteractive.com";


        public SMTP() { }

        
        public static SmtpAPIWrapper SmtpClientCreaCRM()
        {
            //bool useCoderoSmtp = false;//AdminClients.DataAccess.SQLDataAccess.IsUseSmtpCodero();
            SmtpAPIWrapper wrapper = null;
            //if (useCoderoSmtp)
            //{
            //    wrapper = new SmtpAPIWrapper(NotificationBy.SmtpAdminClients);
            //    return wrapper;
            //}
            //else
            //{
                wrapper = new SmtpAPIWrapper(NotificationBy.ApiAdminClients);
                return wrapper;
//            }

        }

        public static SmtpClient Smtp_ClientCreaCRM()
        {
            return SmtpClientCrea(HostType_CRM);

        }

        public static SmtpClient Smtp_ClientCreaClientSites()
        {
            return SmtpClientCrea(HostType_ClientSites);
        }


        public static SmtpClient SmtpClientCrea(string host_type)
        {
            return SmtpClientCrea(host_type, true);
        }

        public static SmtpClient SmtpClientCrea(string host_type, bool isSmtpClientCreaFromDB)
        {
            SmtpHostsEntry e;
            if (isSmtpClientCreaFromDB)
            {
                e = new SMTP().getEntryFromDB(host_type);
                if (!string.IsNullOrEmpty(e.Error))
                    throw new Exception(string.Format("Err.119764: DBSmtpHosts host_type='{0}'; Error='{1}'.", host_type, e.Error));
                if (!e.FromDB)
                    e = getEntryConst(host_type);
            }
            else
                e = getEntryConst(host_type);

            SmtpClient smtp_cli = new System.Net.Mail.SmtpClient(e.HostName, e.Port);
            if (e.HasUser())
                smtp_cli.Credentials = e.HasDomain() ? new System.Net.NetworkCredential(e.UserName, e.Password, e.Domain) : new System.Net.NetworkCredential(e.UserName, e.Password);
            else
                smtp_cli.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            smtp_cli.EnableSsl = e.EnableSsl ? true : false;

            if (e.HasTimeout())
                smtp_cli.Timeout = e.TimeoutInSeconds * 1000; //smtp_cli.Timeout is in milliseconds. The default value is 100,000 (100 seconds).

            return smtp_cli;
        }

        public static SmtpHostsEntry getEntryConst(string HostType)
        {
            SmtpHostsEntry e = new SmtpHostsEntry();

            switch (HostType)
            {
                case HostType_Default: e.HostName = host_default; break;
                case HostType_Rates: e.HostName = host_rates; break;
                case HostType_Tests: e.HostName = host_tests; break;
                case HostType_Chain41: e.HostName = host_chain41; break;
                case HostType_CRM: e.HostName = host_crm; break;
                case HostType_ClientSites: e.HostName = host_clientsites; break;
                default: e.Error = "Unknown HostType (err.15862)";
                    return e;
            }
            e.FromConst = true;
            e.HostType = HostType;
            e.Port = 25;
            return e;
        }



        public SmtpHostsEntry getEntryFromDB(string HostType)//, out bool isFound)
        {
            //isFound = false;
            List<SmtpHostsEntry> all2 = null; // base.selectall2();
            if (all2 == null || all2.Count == 0)
                return new SmtpHostsEntry();
            foreach (SmtpHostsEntry e in all2)
            {
                if (e.HostType.Equals(HostType))
                {
                    e.FromDB = true;
                    //isFound = true;
                    return e;
                }
            }
            return new SmtpHostsEntry();
        }


    }

}