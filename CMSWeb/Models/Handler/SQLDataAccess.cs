using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using InjectionApi.Sdk.Email;
using RestSharp;
using InjectionApi.Sdk.Client;
using System.Web.Script.Serialization;

using CMSWeb.Models.Consumable;

namespace CMSWeb.Models.Handler
{
    public class SQLDataAccess
    {

        Regex regex_script;
        public SQLDataAccess()
        {
            regex_script = new Regex("<script.*</script>|<script.*/>", RegexOptions.IgnoreCase);
        }

        // if To Address is blank not send
        // if return not NULL, could use ref variable
        public string CleanEmailMailMessage(ref MailMessage mailMessage)
        {
            string retStr = string.Empty;
            int numberOfEmailToSend = mailMessage.To.Count + mailMessage.Bcc.Count + mailMessage.CC.Count;
            int numberOfEmailVerify = 0;

            MailMessage retMailMessage = new MailMessage();

            // Clean "To" Address
            MailAddressCollection _toAddresses = new MailAddressCollection();
            _toAddresses = CleanEmailAddress(mailMessage.To);
            if (_toAddresses.Count == 0) // no able to To Address, just quit
            {
                mailMessage.To.Clear();
                retStr = "No valid (To) email address. No email was sent out";
                return retStr;
            }

            mailMessage.To.Clear();
            foreach (MailAddress aMailAddress in _toAddresses)
            {
                numberOfEmailVerify++;
                retStr += aMailAddress + " ";
                mailMessage.To.Add(aMailAddress);
            }


            // Clean "cc" Address
            MailAddressCollection _ccAddresses = new MailAddressCollection();
            _ccAddresses = CleanEmailAddress(mailMessage.CC);
            if (_ccAddresses.Count > 0) // no able to To Address, just quit
            {
                mailMessage.CC.Clear();
                foreach (MailAddress aMailAddress in _ccAddresses)
                {
                    numberOfEmailVerify++;
                    retStr += aMailAddress + " ";
                    mailMessage.CC.Add(aMailAddress);
                }
            }
            else
                mailMessage.CC.Clear();        // after clean, if no email should be send then clean

            // Clean "Bcc" Address
            MailAddressCollection _bccAddresses = new MailAddressCollection();
            _bccAddresses = CleanEmailAddress(mailMessage.Bcc);
            if (_bccAddresses.Count > 0) // no able to To Address, just quit
            {
                mailMessage.Bcc.Clear();
                foreach (MailAddress aMailAddress in _bccAddresses)
                {
                    numberOfEmailVerify++;
                    retStr += aMailAddress + " ";
                    mailMessage.Bcc.Add(aMailAddress);
                }
            }
            else
                mailMessage.Bcc.Clear();

            retStr = "Message sent to: " + retStr;
            if (numberOfEmailToSend != numberOfEmailVerify)
            {
                retStr.Trim();
                retStr += ". Other email(s) are invalid or being validated.";
            }
            return retStr;

        }

        public MailAddressCollection CleanEmailAddress(MailAddressCollection collectionEmail)
        {
            string errMsg = string.Empty;
            string lvlValidation = string.Empty;
            SQLDataAccess da = new SQLDataAccess();

            MailAddressCollection addressCollection = new MailAddressCollection();

            foreach (MailAddress anToAddress in collectionEmail)
            {
                string strToEmail = anToAddress.Address;
                EmailValidationResult emailValidationResult = da.CheckEmailCombination(strToEmail, out lvlValidation);
                if (lvlValidation.Equals("db"))
                {
                    EmailValidationResult emailDisposable = CheckEmailInMasterTable(strToEmail);
                    if (emailDisposable == null)
                        emailValidationResult.InsertToMasterEmailTable(out errMsg);

                    // send email if UC or Verified
                    bool emailIsUCVerified = da.IsEmailUcOrVerifiedDB(emailValidationResult);
                    if (emailIsUCVerified)
                    {
                        addressCollection.Add(emailValidationResult.Email);
                    }
                }
                else if (lvlValidation.Equals("online")) // always intro new.
                {
                    emailValidationResult.InsertToMasterEmailTable(out errMsg); // in case this is verified email

                    bool emailIsUCVerified = da.IsEmailUcOrVerifiedDB(emailValidationResult);
                    if (emailIsUCVerified)
                    {
                        addressCollection.Add(emailValidationResult.Email);
                    }
                }
            }
            return addressCollection;
        }

        /// <summary>
        /// Email to Check in MasterTable. 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>null if NOT exists in Master table</returns>
        public EmailValidationResult CheckEmailInMasterTable(string email)
        {
            // check db
            try
            {
                Connections t = new Connections();
                ArrayList al = t.select("dbo.[sp_SelectMasterEmail] @EmailAddress = @EmailAddress",
                    new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@EmailAddress", email) });

                if (al != null && al.Count != 0)
                {
                    Hashtable emailMaster = (Hashtable)(al[0]);
                    string strValidationMethod = emailMaster["ValidationMethod"].ToString();
                    string strCertainty = emailMaster["Certainty"].ToString();
                    string strMessage = emailMaster["Message"].ToString();
                    string strStatus = emailMaster["Status"].ToString();
                    string strCreatedDate = emailMaster["CreatedDate"].ToString();
                    string strLastAction = emailMaster["LastAction"].ToString();
                    string strLastUpdate = emailMaster["LastUpdate"].ToString();
                    string strNumberOfValidation = emailMaster["NumberOfValidation"].ToString();
                    string strCorrectionEmail = emailMaster["CorrectionEmail"].ToString();
                    string strComment = emailMaster["Comment"].ToString();
                    if ((strCorrectionEmail.Equals("NULL") || strCorrectionEmail.Equals(DBNull.Value.ToString())))
                        strCorrectionEmail = string.Empty;

                    EmailValidationResult emailValidationResult = new EmailValidationResult(email, strCertainty, strCorrectionEmail, strValidationMethod, strMessage, strLastAction, DateTime.Parse(strCreatedDate), DateTime.Parse(strLastUpdate), int.Parse(strNumberOfValidation), int.Parse(strStatus), strComment);

                    return emailValidationResult;
                }
                return null;
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>EmailValidationResult </returns>
        public EmailValidationResult CheckEmailCombination(string email, out string atLevel)
        {
            atLevel = string.Empty;
            EmailValidationResult emailValidationResult = CheckEmailInMasterTable(email);

            if (emailValidationResult != null)
            {
                atLevel = "db";
                return emailValidationResult;
            }
            else // case of disposable email and not find in db yet
            {
                if (IsDisposableEmail(email))
                {
                    atLevel = "db";
                    EmailValidationResult emailValidateResult = new EmailValidationResult(email, "verified", "", "CRM", "OK", "IntroNew");
                    return emailValidateResult;
                }
                else
                {
                    //go online
                }
            }
            emailValidationResult = CheckEmailOnline(email); // with online, always has result
            atLevel = "online";

            return emailValidationResult;

        }

        // check email if it is disposalbe email or not
        public bool IsDisposableEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) // if email is blank
                return false;

            MailAddress address = new MailAddress(email);
            string host = address.Host; // host contains yahoo.com

            Connections t = new Connections();
            ArrayList al = t.select("dbo.[sp_SelectQADiposalDomain] @domain = @domain",
                    new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@domain", host) });

            if (al != null && al.Count != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validate Email by using Experian Validation Service. After validating, email status set.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Result return by Online Experian Service</returns>
        public EmailValidationResult CheckEmailOnline(string email)
        {
            EmailValidator emailValidator = new EmailValidator();

            string errorMessage;
            EmailValidateObject emailValidateObject = emailValidator.SubmitValidationRequest(email, out errorMessage);

            if (emailValidateObject == null)
                return null;

            string strCertainty = emailValidateObject.Certainty;
            string strMessage = emailValidateObject.Message;

            string correctionEmail = String.Empty;

            if (emailValidateObject.Corrections != null)
                correctionEmail = emailValidateObject.Corrections[0]; // only get the first correction.


            EmailValidationResult emailValidateResult = new EmailValidationResult(email, strCertainty, correctionEmail, "API", strMessage, "IntroNew");

            // If Experian Email Validation Service is down, then no message returned from them. 
            // EmailValidationResult will be null

            return emailValidateResult;

        }

        /// <summary>
        /// Check again in db if Email is UC or already verified
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailUcOrVerifiedDB(string email)
        {
            bool isUcOrVerified = false;
            int maxNumValidation = GetMaxEmailNumValidation();

            EmailValidationResult emailMaster = CheckEmailInMasterTable(email);
            if (emailMaster == null)
            {
                return false;
            }

            if (emailMaster.Status == 1) // verified email
            {
                return true;
            }
            else if ((emailMaster.Status == 2) && (emailMaster.NumberOfValidation > maxNumValidation))
            {
                return true;
            }
            return isUcOrVerified;
        }

        public bool IsEmailUcOrVerifiedDB(EmailValidationResult emailValidationResult)
        {
            bool isUcOrVerified = false;
            int maxNumValidation = GetMaxEmailNumValidation();

            if (emailValidationResult == null)
            {
                return false;
            }

            if (emailValidationResult.Status == 1) // verified email
            {
                return true;
            }
            else if ((emailValidationResult.Status == 2) && (emailValidationResult.NumberOfValidation > maxNumValidation))
            {
                return true;
            }
            return isUcOrVerified;
        }

        public static int GetMaxEmailNumValidation()
        {
            int experianMaxNumValidation = 2;
            Connections t = new Connections();
            string error = string.Empty;
            var result = t.ExecuteScalar("SELECT ServerURL FROM tblGlobalServer WHERE serverName='ExperianMaxNumValidation'", null, out error);

            if (result != null && string.IsNullOrWhiteSpace(error))
            {
                int maxValidation;
                if (Int32.TryParse(result.ToString(), out maxValidation))
                    experianMaxNumValidation = maxValidation;
            }

            return experianMaxNumValidation;
        }

    }

    public class EmailValidationResult : IDisposable
    {
        public string Email;
        public string Certainty;
        public string CorrectionEmail;

        // Field needed 
        public int Status;
        public string Message;
        public string ValidationMethod;
        public string LastAction;

        public DateTime CreatedDate;
        public DateTime LastUpdate;
        public int NumberOfValidation;
        public string Comment;


        public EmailValidationResult(string email, string certaintyByExperian, string correctionEmail, string validationMethod, string message, string lastAction)
        {
            Email = email;
            Certainty = certaintyByExperian;
            CorrectionEmail = correctionEmail;
            ValidationMethod = validationMethod;
            Message = message;
            LastAction = lastAction;

            CreatedDate = DateTime.Now;
            LastUpdate = DateTime.Now;

            NumberOfValidation = 1;

            Status = SetStatus(certaintyByExperian);

            Comment = string.Empty;
        }

        public EmailValidationResult(string email, string certaintyByExperian, string correctionEmail, string validationMethod, string message, string lastAction, string comment)
        {
            Email = email;
            Certainty = certaintyByExperian;
            CorrectionEmail = correctionEmail;
            ValidationMethod = validationMethod;
            Message = message;
            LastAction = lastAction;

            CreatedDate = DateTime.Now;
            LastUpdate = DateTime.Now;

            NumberOfValidation = 1;

            Status = SetStatus(certaintyByExperian);

            Comment = comment;
        }

        public EmailValidationResult(string email, string certaintyByExperian, string correctionEmail, string validationMethod, string message, string lastAction, DateTime createdDate, DateTime lastUpdateDate, int numberOfValidation, int status)
        {
            Email = email;
            Certainty = certaintyByExperian;
            CorrectionEmail = correctionEmail;
            ValidationMethod = validationMethod;
            Message = message;
            LastAction = lastAction;

            CreatedDate = createdDate;
            LastUpdate = lastUpdateDate;

            NumberOfValidation = numberOfValidation;

            Status = status;

            Comment = String.Empty;
        }

        // for getdate from db
        public EmailValidationResult(string email, string certaintyByExperian, string correctionEmail, string validationMethod, string message, string lastAction, DateTime createdDate, DateTime lastUpdateDate, int numberOfValidation, int status, string comment)
        {
            Email = email;
            Certainty = certaintyByExperian;
            CorrectionEmail = correctionEmail;
            ValidationMethod = validationMethod;
            Message = message;
            LastAction = lastAction;

            CreatedDate = createdDate;
            LastUpdate = lastUpdateDate;

            NumberOfValidation = numberOfValidation;

            Status = status;

            Comment = comment;
        }


        private int SetStatus(string certaintyByExperian)
        {
            int status = 2; //  assume is Unknown

            switch (certaintyByExperian.ToLower())
            {
                case "verified":
                    status = 1;
                    break;
                case "undeliverable":
                    status = 0;
                    break;
                case "unreachable":
                    status = 0;
                    break;
                case "illegitimate":
                    status = 0;
                    break;
                case "disposable":
                    status = 1;
                    break;
                case "unknown":
                    status = 2;
                    break;
            }

            return status;
        }

        public static EmailValidationResult GetEmail(string email)
        {
            SQLDataAccess da = new SQLDataAccess();
            return da.CheckEmailInMasterTable(email);
        }


        public void UpdateEmail(out string errorMsg)
        {
            Connections t = new Connections();
            errorMsg = string.Empty;

            try
            {
                t.ExecuteNonQuery(@"[sp_UpdateMasterEmail] 
                                            @EmailAddress=@EmailAddress, 
                                            @ValidationMethod =@ValidationMethod, 
                                            @Certainty =@Certainty,
                                            @Message =@Message,
                                            @Status =@Status,
                                            @CreatedDate=@CreatedDate,
                                            @LastAction=@LastAction,
                                            @LastUpdate=@LastUpdate,
                                            @NumberOfValidation=@NumberOfValidation,
                                            @CorrectionEmail=@CorrectionEmail,
                                            @Comment=@Comment",
                    new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@EmailAddress", Email), 
                    new System.Data.SqlClient.SqlParameter("@ValidationMethod", ValidationMethod), 
                    new System.Data.SqlClient.SqlParameter("@Certainty", Certainty), 
                    new System.Data.SqlClient.SqlParameter("@Message", Message),
                    new System.Data.SqlClient.SqlParameter("@Status", Status),
                    new System.Data.SqlClient.SqlParameter("@CreatedDate", CreatedDate),
                    new System.Data.SqlClient.SqlParameter("@LastUpdate", LastUpdate),
                    new System.Data.SqlClient.SqlParameter("@LastAction", LastAction),
                    new System.Data.SqlClient.SqlParameter("@NumberOfValidation", NumberOfValidation),
                    new System.Data.SqlClient.SqlParameter("@CorrectionEmail", CorrectionEmail),
                    new System.Data.SqlClient.SqlParameter("@Comment", Comment),
                }, out errorMsg);
            }
            catch (Exception e)
            {
                string message = e.Message;
                try
                {
                    t.ExecuteNonQuery("exec sp_MasterEmailLogInsert @EmailAddress=@EmailAddress, @Action=@Action, @Comment=@Comment", new System.Data.SqlClient.SqlParameter[] 
                    {
                        new System.Data.SqlClient.SqlParameter("@EmailAddress",Email),
                        new System.Data.SqlClient.SqlParameter("@Action", "Update"),
                        new System.Data.SqlClient.SqlParameter("@Comment", message)
                    }, out message);
                }
                catch { }
            }
        }


        public void InsertToMasterEmailTable(out string message)
        {

            message = string.Empty;

            if (GetEmail(Email) != null) return;

            Connections t = new Connections();

            try
            {
                t.ExecuteNonQuery("exec sp_MasterEmailInsert @Email=@Email, @ValidationMethod=@ValidationMethod, @Certainty=@Certainty,@Message=@Message,@Status=@Status,@CreatedDate=@CreatedDate,@LastAction=@LastAction,@LastUpdate=@LastUpdate,@NumberOfValidation=@NumberOfValidation,@CorrectionEmail=@CorrectionEmail,@Comment=@Comment", new System.Data.SqlClient.SqlParameter[] 
                {
                    new System.Data.SqlClient.SqlParameter("@Email",Email),
                    new System.Data.SqlClient.SqlParameter("@ValidationMethod", ValidationMethod),
                    new System.Data.SqlClient.SqlParameter("@Certainty", Certainty),
                    new System.Data.SqlClient.SqlParameter("@Message", Message),   
                    new System.Data.SqlClient.SqlParameter("@Status", Status),      
                    new System.Data.SqlClient.SqlParameter("@CreatedDate", CreatedDate),         
                    new System.Data.SqlClient.SqlParameter("@LastAction", LastAction),  
                    new System.Data.SqlClient.SqlParameter("@LastUpdate", LastUpdate),  
                    new System.Data.SqlClient.SqlParameter("@NumberOfValidation", NumberOfValidation),   
                    new System.Data.SqlClient.SqlParameter("@CorrectionEmail", CorrectionEmail),
                    new System.Data.SqlClient.SqlParameter("@Comment", Comment),
                }, out message);
            }

            catch (Exception e)
            {
                message = e.Message;
                try
                {
                    t.ExecuteNonQuery("exec sp_MasterEmailLogInsert @EmailAddress=@EmailAddress, @Action=@Action, @Comment=@Comment", new System.Data.SqlClient.SqlParameter[] 
                    {
                        new System.Data.SqlClient.SqlParameter("@EmailAddress",Email),
                        new System.Data.SqlClient.SqlParameter("@Action", "AddNew"),
                        new System.Data.SqlClient.SqlParameter("@Comment", message)
                    }, out message);
                }
                catch { }
            }
        }


        public void Dispose()
        {
            Email = null;
            Certainty = null;
            CorrectionEmail = null;

        }

    }

    public class EmailValidator
    {
        private string _authorizeToken;
        private string _machineURL;
        private int _timeout;
        private int _correctSize;
        private string _validationMode;
        private bool _useCorrection;
        /// <summary>
        /// Constructor for on-demand validation. Default from DB (tblGlobalServer)
        /// </summary>
        /// <param name="authorizeToken">Override DB token</param>
        /// <param name="machineURL">Override DB URL </param>
        /// <param name="useCorrections"> Specify whether Email Validate should return email corrections. </param>
        /// <param name="timeout">Specify timeout</param>
        /// <returns></returns>
        public EmailValidator()
        {
            ExperianEmailValidationSetting experianEmailSetting = new ExperianEmailValidationSetting(null);
            string errorMessage = String.Empty;
            experianEmailSetting.GetExperianSetting(out errorMessage);

            this._authorizeToken = experianEmailSetting.AuthorizeToken;
            this._machineURL = experianEmailSetting.MachineURL;
            this._timeout = experianEmailSetting.Timeout;
            this._correctSize = experianEmailSetting.CorrectSize;
            this._validationMode = experianEmailSetting.ValidationMode;
            this._useCorrection = (_correctSize > 0);
        }

        /// <summary>
        /// Constructor for on-demand validation. Value not specify (null) then from DB
        /// </summary>
        /// <param name="authorizeToken">Override DB token</param>
        /// <param name="machineURL">Override DB URL </param>
        /// <param name="timeout">Specify timeout</param>
        /// <param name="correctSize">0: mean not use</param>
        /// <returns></returns>
        public EmailValidator(string authorizeToken, string machineURL, int correctSize, int timeout, string validationMode)
        {
            if (!string.IsNullOrEmpty(authorizeToken))
                this._authorizeToken = authorizeToken;

            if (!string.IsNullOrEmpty(machineURL))
                this._machineURL = machineURL;

            if (timeout != 0)
                this._timeout = timeout;

            if (correctSize != 0)
                this._correctSize = correctSize;

            if (!string.IsNullOrEmpty(validationMode))
                this._validationMode = validationMode;

            if (correctSize <= 0)
                this._useCorrection = false;

        }

        /// <summary>
        /// All the typos had been checked before calling this function
        /// </summary>
        /// <param name="email">Email to Validate</param>
        /// <param name="ErrorMessage">Error Message if Any</param>
        /// <returns></returns>
        public EmailValidateObject SubmitValidationRequest(string email, out string ErrorMessage)
        {
            string authorizationHeader = "Auth-Token";
            ErrorMessage = string.Empty;

            try
            {
                Uri queryUri = new Uri(string.Format("{0}/sync/queryresult/EmailValidate/1.0/", _machineURL)); // default is synchronous
                if (_validationMode.Equals("asynchronous"))
                    queryUri = new Uri(string.Format("{0}/Query/EmailValidate/1.0/", _machineURL));
                else if (_validationMode.Equals("synchronous"))
                    queryUri = new Uri(string.Format("{0}/sync/queryresult/EmailValidate/1.0/", _machineURL));

                HttpWebRequest queryRequest = (HttpWebRequest)WebRequest.Create(queryUri);
                queryRequest.Method = WebRequestMethods.Http.Post;
                queryRequest.ContentType = "application/json";
                queryRequest.Headers.Add(authorizationHeader, _authorizeToken);

                string postData = string.Format("{{ \"Email\":{0},\"UseCorrections\":\"{1}\",\"CorrectionSize\":\"{2}\",\"Timeout\":\"{3}\"}}", 
                    JsonUtility.EscapeJSON(email), _useCorrection, _correctSize, _timeout);

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postData);
                queryRequest.ContentLength = data.Length;

                using (Stream stream = queryRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (HttpWebResponse response = queryRequest.GetResponse() as HttpWebResponse)
                {
                    if ((response.StatusCode != HttpStatusCode.OK) && (response.StatusCode != HttpStatusCode.Created))
                    {
                        ErrorMessage = String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription);
                        return null;
                    }

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(EmailValidateObject));

                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    EmailValidateObject jsonResponse = objResponse as EmailValidateObject;

                    return jsonResponse;
                }

            }
            catch (Exception e)
            {
                ErrorMessage += e.Message;
                return null;
            }

            return null;
        }



    }

    public class ExperianEmailValidationSetting : Connections
    {
        public string AuthorizeToken;
        public string MachineURL;
        public int Timeout;
        public int CorrectSize;
        public string ValidationMode; // only 2 options: synchronous vs asynchronous

        // pass null then tbl connection string
        public ExperianEmailValidationSetting(string conn)
        {
            if (!string.IsNullOrEmpty(conn)) set_cnnctn_str(conn);
        }

        public void GetExperianSetting(out string err)
        {

            try
            {
                System.Collections.ArrayList al = this.select("EXEC sp_GetExperianEmailValidationSetting ", out err);

                if (al != null && al.Count > 0)
                {
                    System.Collections.Hashtable ht = (System.Collections.Hashtable)(al[0]);

                    AuthorizeToken = Connections.getHashtableField(ht, "ExperianAPIKey");
                    MachineURL = Connections.getHashtableField(ht, "ExperianValidationURL");
                    Timeout = Convert.ToInt32(Connections.getHashtableField(ht, "ExperianAPITimeout"));
                    CorrectSize = Convert.ToInt32(Connections.getHashtableField(ht, "ExperianNumberOfCorrection"));
                    ValidationMode = Connections.getHashtableField(ht, "ExperianValidationValidationMode");

                }
                err = string.Empty;
            }
            catch (Exception e)
            {
                err = e.Message;
            }

        }
    }

    [DataContract]
    public class EmailValidateObject
    {
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "Certainty")]
        public string Certainty { get; set; }
        [DataMember(Name = "Message")]
        public string Message { get; set; }
        [DataMember(Name = "Corrections")]
        public string[] Corrections { get; set; }
    }

    public class SdkEmailMessage
    {
        public string MailingId { get; set; }
        public string MessageId { get; set; }
        public int ServerId { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string RecipientName { get; set; }
        public DateTime DateTimeSent { get; set; }
        public DateTime DateTimeLastUpdate { get; set; }
        public string Delivered { get; set; }
        public string Failed { get; set; }
        public string FailureCode { get; set; }
        public string FailureReason { get; set; }
        public string FailureType { get; set; }
        public string Tracking { get; set; }
        public int NumClick { get; set; }
        public int NumOpen { get; set; }
        public int NumUnsubscribe { get; set; }
        public string Complaint { get; set; }

        public SdkEmailMessage(string mailingId, string messageId, int serverId, string fromEmail, string toEmail, string recipientName)
        {
            MailingId = mailingId;
            MessageId = messageId;
            ServerId = serverId;
            FromEmail = fromEmail;
            ToEmail = toEmail;
            RecipientName = recipientName;
            DateTimeSent = DateTime.Now;
            DateTimeLastUpdate = DateTime.Now;
            Delivered = "N";
            Failed = "N";
            FailureCode = "";
            FailureReason = "";
            FailureType = "";
            Tracking = "N";
            NumClick = 0;
            NumOpen = 0;
            NumUnsubscribe = 0;
            Complaint = "N";
        }

    }

    public static class IEnumerableExtensions
    {
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }
            return table;
        }
    }

    public class JsonUtility
    {
        /// <summary>
        /// Escapes JSON characters.
        /// </summary>
        /// <param name="jsonString">A string to JSON escape.</param>
        /// <returns>A escaped JSON.</returns>
        public static string EscapeJSON(string jsonString)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                return EscapeJSON(jsonString, stringWriter).ToString();
            }
        }

        /// <summary>
        /// Static method to escape JSON. 
        /// </summary>
        /// <param name="jsonString">A string to JSON escape.</param>
        /// <param name="stringWriter">String writer object.</param>
        /// <returns>Returns String Writer object that has the escaped JSON.</returns>
        public static StringWriter EscapeJSON(string jsonString, StringWriter stringWriter)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                stringWriter.Write("\"\"");
                return stringWriter;
            }

            char backSlashCharacter;
            char currentCharacter = '0';
            string hexadecimalCharacter;
            int length = jsonString.Length;

            stringWriter.Write('"');
            for (int i = 0; i < length; i += 1)
            {
                backSlashCharacter = currentCharacter;
                currentCharacter = jsonString[i];
                switch (currentCharacter)
                {
                    case '\\':
                    case '"':
                        stringWriter.Write('\\');
                        stringWriter.Write(currentCharacter);
                        break;
                    case '/':

                        if (backSlashCharacter == '<')
                        {
                            stringWriter.Write('\\');
                        }

                        stringWriter.Write(currentCharacter);
                        break;
                    case '\b':
                        stringWriter.Write("\\b");
                        break;
                    case '\t':
                        stringWriter.Write("\\t");
                        break;
                    case '\n':
                        stringWriter.Write("\\n");
                        break;
                    case '\f':
                        stringWriter.Write("\\f");
                        break;
                    case '\r':
                        stringWriter.Write("\\r");
                        break;
                    default:

                        if (currentCharacter < ' ' || (currentCharacter >= '\u0080' && currentCharacter < '\u00a0')
                                || (currentCharacter >= '\u2000' && currentCharacter < '\u2100'))
                        {
                            stringWriter.Write("\\u");
                            hexadecimalCharacter = Convert.ToByte(currentCharacter).ToString("X4");
                            stringWriter.Write("0000", 0, 4 - hexadecimalCharacter.Length);
                            stringWriter.Write(hexadecimalCharacter);
                        }
                        else
                        {
                            stringWriter.Write(currentCharacter);
                        }

                        break;
                }
            }

            stringWriter.Write('"');
            return stringWriter;
        }
    }

    public class SocketLabsSendNotification
    {

        private static ServerAPI serverAPIAdminClients = null;
        private static ServerAPI serverAPIClientSites = null;

        private static object syncRootAdmin = new Object();
        private static object syncRootClient = new Object();

        private string _fromDisplayName = string.Empty;
        private string _fromEmailAddress = string.Empty;



        public SocketLabsSendNotification(NotificationBy hostType)
        {
            //if (hostType == NotificationBy.ApiAdminClients)
            //{
            //    if (serverAPIAdminClients == null)
            //    {
            //        lock (syncRootAdmin)
            //        {
            //            if (serverAPIAdminClients == null)
            //            {
            serverAPIAdminClients = GetNotificationServerAdminClients();
            //            }

            //        }
            //    }
            //}
            //else if (hostType == NotificationBy.ApiClientSites)
            //{
            //    if (serverAPIClientSites == null)
            //    {
            //        lock (syncRootAdmin)
            //        {
            //            if (serverAPIClientSites == null)
            //            {
            //                serverAPIClientSites = GetNotificationServerClientSites();
            //            }

            //        }
            //    }
            //}

            GetFromAddress();
        }

        private ServerAPI GetNotificationServerAdminClients()
        {
            ServerAPI serverAPI = null;
            Connections da = new Connections();
            string connectionString = da.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAPINotificationServerAdminClients", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string _serverId = reader["ServerId"].ToString();
                            string _serverName = reader["ServerName"].ToString();
                            string _apiKey = reader["APIKey"].ToString();
                            string _apiURL = reader["APIUrl"].ToString();
                            string _isActiveCampaign = reader["IsCampaignActive"].ToString();
                            string _isNotificationActiveAdminClients = reader["IsNotificationActiveAdminClients"].ToString();
                            if (_isNotificationActiveAdminClients.Equals("1"))
                            {
                                serverAPI = new ServerAPI(_serverId, _serverName, _apiKey, _apiURL);
                                return serverAPI;
                            }
                        }
                    }
                    else
                    {
                    }



                }
            }
            return serverAPI; // reach here mean NULL
        }

        private ServerAPI GetNotificationServerClientSites()
        {
            ServerAPI serverAPI = null;
            Connections da = new Connections();
            string connectionString = da.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAPINotificationServerClientSites", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string _serverId = reader["ServerId"].ToString();
                            string _serverName = reader["ServerName"].ToString();
                            string _apiKey = reader["APIKey"].ToString();
                            string _apiURL = reader["APIUrl"].ToString();
                            string _isActiveCampaign = reader["IsCampaignActive"].ToString();
                            string _isNotificationActiveClientSites = reader["IsNotificationActiveClientSites"].ToString();
                            if (_isNotificationActiveClientSites.Equals("1"))
                            {
                                serverAPI = new ServerAPI(_serverId, _serverName, _apiKey, _apiURL);
                                return serverAPI;
                            }
                        }
                    }
                    else
                    {
                    }



                }
            }
            return serverAPI; // reach here mean NULL
        }

        private void GetFromAddress()
        {
            string err = string.Empty;
            Connections t = new Connections();

            ArrayList al = t.select("exec [dbo].[sp_GetGlobalFromAddress]", null, out err);

            if (al != null && al.Count == 1)
            {
                this._fromEmailAddress = Connections.getHashtableField(al[0], "GlobalMailFrom").ToString();
                this._fromDisplayName = Connections.getHashtableField(al[0], "GlobalMailDisplayName").ToString();
            }
        }









        public void Send(MailMessage mailMessage)
        {
            NotificationBy hostType = NotificationBy.ApiAdminClients;
            Send(mailMessage, hostType);
        }

        public void Send(MailMessage mailMessage, NotificationBy hostType)
        {
            SQLDataAccess da = new SQLDataAccess();
            da.CleanEmailMailMessage(ref mailMessage);

            if (mailMessage.To.Count == 0) // send valid email address
                return;

            string errorMsg = string.Empty;

            DateTime receiveTime = DateTime.Now;

            ServerAPI serverAPI = null;

            if (hostType == NotificationBy.ApiAdminClients)
            {
                serverAPI = serverAPIAdminClients;
            }
            else if (hostType == NotificationBy.ApiClientSites)
            {
                serverAPI = serverAPIClientSites;
            }

            if (serverAPI == null)
            {
                errorMsg = "There is not server available for this operation";
                return;
            }

            EmailMessage emailMessage = SetDataEmailMessage(mailMessage);
            List<EmailMessage> msgList = new List<EmailMessage>();
            msgList.Add(emailMessage);

            var client = new RestClient(serverAPI.APIUrl);

            // Construct the object used to generate JSON for the POST request.
            var postBody = new PostBody
            {
                ServerId = Convert.ToInt32(serverAPI.ServerId),
                ApiKey = serverAPI.APIKey,
                Messages = msgList.ToArray()
            };

            try
            {
                // Generate a new POST request.
                var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

                // Store the request data in the request object.
                request.AddBody(postBody);

                UpdateEmailSentTracking(emailMessage, Convert.ToInt32(serverAPI.ServerId));

                // Make the POST request.
                var result = client.ExecuteAsPost<PostResponse>(request, "POST");


                // Store the response result in our custom class.
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(result.Content)))
                {

                    stream.Position = 0;
                    var sr = new StreamReader(stream);
                    string myStr = sr.ReadToEnd();

                    var jserializer = new JavaScriptSerializer();
                    var resp = jserializer.Deserialize<PostResponse>(myStr);

                    // Display the results.
                    if (resp.ErrorCode.Equals("Success"))
                    {
                        LogSendCampaignRequest(Convert.ToInt32(serverAPI.ServerId), emailMessage.MailingId, 1, receiveTime, emailMessage.HtmlBody, string.Empty, DateTime.Now, 1, "Success");
                        //UpdateEmailSentTracking(emailMessage, Convert.ToInt32(serverAPI.ServerId) );
                        return;
                    }
                    else
                    {

                        LogSendCampaignRequest(Convert.ToInt32(serverAPI.ServerId), emailMessage.MailingId, 1, receiveTime, emailMessage.HtmlBody, string.Empty, DateTime.Now, 1, result.Content);

                        return;
                    }

                }
            }

            catch (Exception ex)
            {
                LogSendCampaignRequest(Convert.ToInt32(serverAPI.ServerId), emailMessage.MailingId, 1, receiveTime, emailMessage.HtmlBody, string.Empty, DateTime.Now, 1, ex.Message);
            }
            finally
            {
            }
        }

        private void LogSendCampaignRequest(int ServerId, string MailingId, int NumberRecipient, DateTime DateTimeReceive, string BodyHTML, string HashMerge, DateTime DateTimeFinishedUpload, int NumberOfTry, string errorMessage)
        {
            string currMsg = string.Empty;
            Connections t = new Connections();


            t.ExecuteNonQuery(@"[sp_SKL_SendCampaign_Log]
                                            @ServerId=@ServerId,
                                            @MailingId=@MailingId, 
                                            @NumberRecipient=@NumberRecipient,
                                            @DateTimeReceive =@DateTimeReceive, 
                                            @BodyHTML = @BodyHTML,
                                            @HashMerge =@HashMerge,
                                            @DateTimeFinishedUpload =@DateTimeFinishedUpload,
                                            @NumberOfTry=@NumberOfTry,
                                            @ErrorMessage=@ErrorMessage",
                new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@ServerId", ServerId), 
                    new System.Data.SqlClient.SqlParameter("@MailingId", MailingId), 
                    new System.Data.SqlClient.SqlParameter("@NumberRecipient", NumberRecipient), 
                    new System.Data.SqlClient.SqlParameter("@DateTimeReceive", DateTimeReceive), 
                    new System.Data.SqlClient.SqlParameter("@BodyHTML", BodyHTML), 
                    new System.Data.SqlClient.SqlParameter("@HashMerge", HashMerge),
                    new System.Data.SqlClient.SqlParameter("@DateTimeFinishedUpload", DateTimeFinishedUpload),
                    new System.Data.SqlClient.SqlParameter("@NumberOfTry", NumberOfTry),
                    new System.Data.SqlClient.SqlParameter("@ErrorMessage", errorMessage)
                }, out currMsg);

        }

        private EmailMessage SetDataEmailMessage(MailMessage mailMessage)
        {

            EmailMessage emailMessage = new EmailMessage();
            emailMessage.MailingId = System.Guid.NewGuid().ToString();
            emailMessage.MessageId = System.Guid.NewGuid().ToString();

            // custom DKIM signature 
            //CustomHeader customHeader = new CustomHeader("Sender", "encompasscrm.com");
            CustomHeader customHeader = new CustomHeader("Sender", "Encompass CRM <info@encompasscrm.com>");
            List<CustomHeader> customHeaderList = new List<CustomHeader>();
            customHeaderList.Add(customHeader);
            emailMessage.CustomHeaders = customHeaderList.ToArray();


            // from
            emailMessage.From = new Address();
            emailMessage.From.EmailAddress = _fromEmailAddress;
            emailMessage.From.FriendlyName = _fromDisplayName;

            // user From address become Reply-To now
            emailMessage.ReplyTo = new Address();
            emailMessage.ReplyTo.EmailAddress = _fromEmailAddress;
            emailMessage.ReplyTo.FriendlyName = _fromEmailAddress;

            // to
            emailMessage.To = GetSocketLabsAddress(mailMessage.To);

            // cc
            emailMessage.Cc = GetSocketLabsAddress(mailMessage.CC);

            // bcc
            emailMessage.Bcc = GetSocketLabsAddress(mailMessage.Bcc);

            // subject
            emailMessage.Subject = mailMessage.Subject;
            if (string.IsNullOrEmpty(emailMessage.Subject))
                emailMessage.Subject = "Blank Subject";


            // attachments
            // alterative view check
            if ((mailMessage.AlternateViews != null) && (mailMessage.AlternateViews.Count > 0))
            {
                emailMessage.HtmlBody = GetPlainAlterView(mailMessage);

                List<InjectionApi.Sdk.Email.Attachment> attachList = GetLinkRes(mailMessage.AlternateViews[0]);
                if (attachList.Count > 0)
                    emailMessage.Attachments = attachList.ToArray();
            }
            else // just body, no link resources or attachment
            {
                emailMessage.HtmlBody = mailMessage.Body;// +"<HsUnsubscribe>Custom Link Testing Unsubscribe Text</HsUnsubscribe>";
            }

            if (mailMessage.Attachments.Count > 0)
            {
                List<InjectionApi.Sdk.Email.Attachment> attachList = GetAttachment(mailMessage.Attachments);
                if (attachList.Count > 0)
                {
                    emailMessage.Attachments.Union(attachList);
                }
            }
            return emailMessage;
        }


        private Address GetSocketLabsAddress(MailAddress stmpAddress)
        {
            Address skEmailAddress = new Address();
            skEmailAddress.EmailAddress = stmpAddress.Address;
            skEmailAddress.FriendlyName = stmpAddress.DisplayName;

            return skEmailAddress;
        }

        private Address[] GetSocketLabsAddress(MailAddressCollection smtpAddressCollection)
        {
            List<Address> skEmailAddressCollection = new List<Address>();

            foreach (MailAddress smtpAddress in smtpAddressCollection)
            {
                Address skEmailAddress = GetSocketLabsAddress(smtpAddress);
                skEmailAddressCollection.Add(skEmailAddress);
            }
            return skEmailAddressCollection.ToArray();
        }

        private string GetPlainAlterView(MailMessage msg)
        {
            StreamReader plain_text_body_reader = new StreamReader(msg.AlternateViews[0].ContentStream);
            return (plain_text_body_reader.ReadToEnd());
        }

        private List<InjectionApi.Sdk.Email.Attachment> GetLinkRes(AlternateView alView)
        {
            List<InjectionApi.Sdk.Email.Attachment> attList = new List<InjectionApi.Sdk.Email.Attachment>();

            foreach (LinkedResource linkItem in alView.LinkedResources)
            {
                InjectionApi.Sdk.Email.Attachment anAttachment = new InjectionApi.Sdk.Email.Attachment();
                anAttachment.Content = Convert.ToBase64String(ReadFully(linkItem.ContentStream));
                anAttachment.ContentType = linkItem.ContentType.ToString();
                anAttachment.ContentId = linkItem.ContentId;

                attList.Add(anAttachment);
            }

            return attList;
        }

        private List<InjectionApi.Sdk.Email.Attachment> GetAttachment(AttachmentCollection mailAttachment)
        {
            List<InjectionApi.Sdk.Email.Attachment> attList = new List<InjectionApi.Sdk.Email.Attachment>();
            foreach (System.Net.Mail.Attachment aAttachment in mailAttachment)
            {
                InjectionApi.Sdk.Email.Attachment anAttachment = new InjectionApi.Sdk.Email.Attachment();
                anAttachment.Content = EncodeTo64(anAttachment.Content);
                anAttachment.ContentType = aAttachment.ContentType.ToString();
                anAttachment.ContentId = aAttachment.ContentId;

                attList.Add(anAttachment);
            }

            return attList;
        }

        private string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        private byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private void UpdateEmailSentTracking(EmailMessage emailMessage, int serverId)
        {
            List<SdkEmailMessage> listEmailMessage = new List<SdkEmailMessage>();
            string from = emailMessage.From.EmailAddress;
            string mailingId = emailMessage.MailingId;
            //string messageId = Guid.NewGuid().ToString();

            foreach (Address toAddress in emailMessage.To)
            {
                SdkEmailMessage sdkEmail = new SdkEmailMessage(mailingId, Guid.NewGuid().ToString(), serverId, from, toAddress.EmailAddress, toAddress.FriendlyName);
                listEmailMessage.Add(sdkEmail);
            }

            foreach (Address ccAddress in emailMessage.Cc)
            {
                SdkEmailMessage sdkEmail = new SdkEmailMessage(mailingId, Guid.NewGuid().ToString(), serverId, from, ccAddress.EmailAddress, ccAddress.FriendlyName);
                listEmailMessage.Add(sdkEmail);
            }

            foreach (Address bccAddress in emailMessage.Bcc)
            {
                SdkEmailMessage sdkEmail = new SdkEmailMessage(mailingId, Guid.NewGuid().ToString(), serverId, from, bccAddress.EmailAddress, bccAddress.FriendlyName);
                listEmailMessage.Add(sdkEmail);
            }


            Connections da = new Connections();
            string connectionString = da.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = "dbo.tblEmailApiSentTracking";
                    try
                    {
                        bulkCopy.WriteToServer(listEmailMessage.AsDataTable());
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        conn.Close();
                    }
                }

                transaction.Commit();
            }

        }

    }

    public class ServerAPI
    {
        public string ServerId;
        public string ServerName;
        public string APIKey;
        public string APIUrl;


        public ServerAPI(string serverId, string serverName, string apiKey, string apiURL)
        {
            ServerId = serverId;
            ServerName = serverName;
            APIKey = apiKey;
            APIUrl = apiURL;
        }

    }

    public class SmtpHostsEntry
    {
        public SmtpHostsEntry() { this.EnableSsl = false; this.TimeoutInSeconds = -1; FromDB = FromConst = false; }

        public string HostType;
        public string HostName;
        public int Port;
        public string UserName;
        public string Password;
        public string Domain;
        public bool EnableSsl;
        public int TimeoutInSeconds;

        //////////////////////////////////////
        public string Error;
        public bool FromDB;
        public bool FromConst;

        public bool HasUser()
        {
            return !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password);
        }
        public bool HasDomain()
        {
            return !string.IsNullOrEmpty(this.Domain);
        }
        public bool HasTimeout()
        {
            return this.TimeoutInSeconds > 0;
        }

    }


}