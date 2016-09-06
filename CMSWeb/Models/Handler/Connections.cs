using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMSWeb.Models.Handler
{
    public class Connections
    {
        #region Variables
        // Variables 
        protected int COMMAND_TIMEOUT = 0;
        protected string connection_string = "";
        protected string tbl_name = "";
        #endregion

        //TODO: Trabajar las conecciones para las diferentes base de datos
        #region Constructor
        // Constructor
        public Connections()
        {
            this.connection_string = get_cnnctn_str();
        }

        public Connections(string tbl_name)
        {
            this.connection_string = get_cnnctn_str();
            this.tbl_name = tbl_name;
        }

        public Connections(bool change)
        {
            this.connection_string = 
                this.connection_string.Length > 0 ? this.connection_string : 
                GetConnectionString(true);        
        }
        #endregion

        #region Connection Setting 

        public void set_cnnctn_str(string conn)
        {
            connection_string = conn;
        }
        
        public void setCOMMAND_TIMEOUT(int timeout)
        {
            COMMAND_TIMEOUT = timeout;
        }

        // set to connection
        public string get_cnnctn_str() 
        {
            return this.connection_string.Length > 0 ? this.connection_string : GetConnectionString();
        }

        //Connection String
        public string GetConnectionString(bool change = false)
        {
            string str = "";
            string _key = "SqlConnectionString";
            string CONFIG_SECTIONS_NAME = change ? "CMSWebSettings" : "PropertyInfoSettings";
            System.Collections.Specialized.NameValueCollection name_values = (System.Collections.Specialized.NameValueCollection) 
                ConfigurationManager.GetSection(CONFIG_SECTIONS_NAME);

            if (name_values == null || name_values[_key] == null || name_values[_key] == "")
            {
                str = ConfigurationManager.AppSettings[_key];
                if (!string.IsNullOrEmpty(str))
                    return str;
                return "";
            }
            else
            {
                str = name_values[_key];
            }
            return (str);
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Vehicle for executing INSERT, UPDATE, DELETE.
        /// Returns the number of rows affected by the command.
        /// </summary>
        /// <param name="command_text"></param>
        /// <returns></returns>
        [Obsolete("You must use method with SqlParameter[].")]
        public int ExecuteNonQuery(string command_text, out string error)
        {
            error = "";
            int ret = 0;
            SqlConnection conn = new SqlConnection(this.connection_string);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = command_text;
                cmd.Connection = conn;
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                ret = cmd.ExecuteNonQuery(); //  returns the number of rows affected.
            }
            catch (SqlException ex)
            {
                ret = 0; error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Command.Parameters version of Vehicle for executing INSERT, UPDATE, DELETE.
        /// Returns the number of rows affected by the command.
        /// </summary>
        /// <param name="command_text"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string command_text, Hashtable ht, out string error)
        {
            error = "";
            int ret = 0;
            SqlConnection conn = new SqlConnection(this.connection_string);//get_cnnctn_str());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = command_text;
                foreach (DictionaryEntry de in ht)
                {
                    string par_name = "@" + de.Key.ToString();
                    //cmd.Parameters.Add(par_name, SqlDbType.Char);
                    //cmd.Parameters[par_name].Value = de.Value.ToString();
                    //SqlParameter p= 
                    cmd.Parameters.AddWithValue(par_name, de.Value);
                    //SqlParameter p = cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                }
                cmd.Connection = conn;
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                ret = cmd.ExecuteNonQuery(); //  returns the number of rows affected.
            }
            catch (SqlException ex)
            {
                ret = 0; error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }
 
        public int ExecuteNonQuery(string commandText, SqlParameter[] parameters)
        {
            string err;
            return ExecuteNonQuery(commandText, parameters, out err);
        }

        public int ExecuteNonQuery(string commandText, SqlParameter[] parameters, out string error)
        {
            error = string.Empty;
            int ret = 0;
            SqlConnection conn = new SqlConnection(this.connection_string);//get_cnnctn_str());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText, conn);
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error += ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// returns the number of rows affected.
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <param name="ht"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int ExecuteNonQueryProc(string StoredProcedureName, Hashtable ht, out string error)
        {
            return ExecuteNonQueryProc(StoredProcedureName, ht, null, out error);
        }

        /// <summary>
        /// returns the number of rows affected.
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <param name="ht"></param>
        /// <param name="htOut"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int ExecuteNonQueryProc(string StoredProcedureName, Hashtable ht, Hashtable htOut, out string error)
        {
            error = "";
            int ret = 0;
            SqlConnection conn = new SqlConnection(this.connection_string);//get_cnnctn_str());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry de in ht)
                {
                    string par_name = "@" + de.Key.ToString();
                    cmd.Parameters.AddWithValue(par_name, de.Value);
                }
                if (htOut != null)
                {
                    foreach (DictionaryEntry de in htOut)
                    {
                        string par_name = "@" + de.Key.ToString();
                        SqlParameter p = cmd.Parameters.AddWithValue(par_name, de.Value);
                        p.Direction = ParameterDirection.Output;
                    }
                }

                cmd.Connection = conn;
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                ret = cmd.ExecuteNonQuery(); //  returns the number of rows affected.

                if (htOut != null)
                {
                    System.Collections.Generic.List<string> keys = new System.Collections.Generic.List<string>();
                    foreach (DictionaryEntry de in htOut)
                        keys.Add(de.Key.ToString());
                    foreach (string key in keys)
                    {
                        htOut[key] = cmd.Parameters["@" + key].Value.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                ret = 0; error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }



        #endregion

        #region ExecuteScalar
        /// <summary>
        /// Vehicle for executing .
        /// Returns 0 (error) or 1st column of the 1st row in the result set returned by the query
        /// </summary>
        /// <param name="command_text"></param>
        /// <returns></returns>
        [Obsolete("You must use method with SqlParameter[].")]
        public object ExecuteScalar(string command_text, out string error)
        {
            error = "";
            int ret = 0;
            SqlConnection conn = new SqlConnection(this.connection_string);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = command_text;
                cmd.Connection = conn;
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                ret = 1; error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        public object ExecuteScalar(string commandText, SqlParameter[] parameters)
        {
            string err;
            return ExecuteScalar(commandText, parameters, out err);
        }

        public object ExecuteScalar(string commandText, SqlParameter[] parameters, out string error)
        {
            error = "";
            object ret = null;
            SqlConnection conn = new SqlConnection(this.connection_string);//get_cnnctn_str());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText, conn);
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                ret =  cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                error = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        #endregion

        #region ExecuteReader
        /// <summary>
        /// call reader.Close()after usage of the returned reader 
        /// </summary>
        /// <param name="command_text"></param>
        /// <returns></returns>
        [Obsolete("You must use method with SqlParameter[].")]
        public SqlDataReader ExecuteReader(string command_text)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(this.connection_string);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = command_text;
                cmd.Connection = conn;
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch //(SqlException ex)
            {
                reader = null;
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return reader;
        }

        public SqlDataReader ExecuteReader(string commandText, SqlParameter[] parameters)
        {
            string err;
            return ExecuteReader(commandText, parameters, out err);
        }

        public SqlDataReader ExecuteReader(string commandText, SqlParameter[] parameters, out string error)
        {
            error = "";
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(this.connection_string);//get_cnnctn_str());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText, conn);
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                error = ex.ToString();
                conn.Close();
            }
            //finally
            //{
            //    reader.Close();
            //    conn.Close();
            //}

            return reader;
        }
        #endregion

        #region DataTableExecuteReader

        public DataTable DataTableExecuteReader(string commandText, SqlParameter[] parameters)
        {
            string err;
            return DataTableExecuteReader(commandText, parameters, out err);
        }

        public DataTable DataTableExecuteReader(string commandText, SqlParameter[] parameters, out string error)
        {
            error = "";
            SqlConnection conn = new SqlConnection(this.connection_string);//get_cnnctn_str());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(commandText, conn);
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = COMMAND_TIMEOUT;
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    DataTable t = new DataTable();
                    t.Load(reader);

                    return t;
                }
            }
            catch (SqlException ex)
            {
                error = ex.ToString();
                conn.Close();
            }

            return null;
        }

        #endregion

        //TODO: Agregarlos en la clase de Insert
        #region Insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool insert(Hashtable ht, out string error)
        {
            return insert(null, ht, out  error);
        }

        /// <summary>
        /// input Hashtable consists from pairs (fiel, field's value)
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public bool insert(string dest_tbl_name, Hashtable ht, out string error)
        {
            error = "";
            string sql = " insert into {2} ( {0} ) values( {1} ) ";
            string fields = "";
            string values = "";
            try
            {
                foreach (string key in ht.Keys)
                {
                    fields += string.Format(" {0},", key);
                    values += string.Format(" @{0},", key);
                }

                fields = fields.Trim();
                values = values.Trim();

                fields = fields.Remove(fields.LastIndexOf(','));
                values = values.Remove(values.LastIndexOf(','));

                sql = string.Format(sql, fields, values, dest_tbl_name != null ? dest_tbl_name : tbl_name);
                return (1 == ExecuteNonQuery(sql, ht, out error));
            }
            catch (Exception ex) { error = ex.ToString(); }
            return false;
        }

        /// <summary>
        /// input Hashtable consists from pairs (fiel, field's value)
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public bool insert1(Hashtable ht, out string error)
        {
            error = "";
            string sql = " insert into {2} ( {0} ) values( {1} ) ";
            string fields = "";
            string values = "";
            try
            {
                foreach (string key in ht.Keys)
                    fields += string.Format(" {0},", key);

                foreach (string value in ht.Values)
                    values += string.Format(" '{0}',", value);

                fields = fields.Trim();
                values = values.Trim();

                fields = fields.Remove(fields.LastIndexOf(','));
                values = values.Remove(values.LastIndexOf(','));

                sql = string.Format(sql, fields, values, this.tbl_name);
                return (1 == this.ExecuteNonQuery(sql, (SqlParameter[])null, out error));
            }
            catch (Exception ex) { error = ex.ToString(); }
            return false;
        }

        #endregion

        //TODO: Agregarlos en la clase de Select
        #region Select
        // new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@", ) }
        /// <summary>
        /// returns ArrayList of Hashtables. 
        /// sql is select query. 
        /// Every hashtable includey key and value for fields in the sql query.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList select(string sql, SqlParameter[] parameters)
        {
            string dummy;
            return this.select(sql, parameters, out dummy);
        }

        public ArrayList select(string sql)
        {
            string dummy;
            return this.select(sql, null, out dummy);
        }

        public ArrayList select(string sql, out string error)
        {
            return this.select(sql, null, out error);
        }

        public ArrayList select(string sql, SqlParameter[] parameters, out string error)
        {
            error = "";
            ArrayList al_ID_NAME = new ArrayList();
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteReader(sql, parameters, out error);
                bool hasMoreRecords = false;

                if (reader.HasRows)
                    hasMoreRecords = true;

                while (hasMoreRecords)
                {
                    Hashtable ID_NAME = ReaderRecord2Hashtable(reader, false, out hasMoreRecords);
                    if (ID_NAME.Count != 0)
                        al_ID_NAME.Add(ID_NAME);
                }
                reader.Close();
                return al_ID_NAME;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                al_ID_NAME.Clear();
            }
            finally { if (reader != null) reader.Close(); }
            return al_ID_NAME;
        }


        #endregion 

        //TODO: Agregarlos en la clase de Update
        #region update
        /// <summary>
        /// Input parameter Hashtable consists from pairs (fiel, field's value). 
        /// Must to include uniqueidentifier field name uniqueID.
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool update(Hashtable ht, string uniqueIDfieldName, out string error)
        {
            error = "";
            string sql = " update {0} set {1} where {2} = @{2} ";//2-table's name, {1}-uniqueIdetifier
            string field_value_pairs = "";

            try
            {
                foreach (DictionaryEntry de in ht)
                    field_value_pairs += string.Format(" {0}=@{0},", de.Key, de.Value);

                //remove last ','
                field_value_pairs = field_value_pairs.Trim();
                field_value_pairs = field_value_pairs.Remove(field_value_pairs.LastIndexOf(','));

                sql = string.Format(sql, this.tbl_name, field_value_pairs, uniqueIDfieldName);
                return (1 == ExecuteNonQuery(sql, ht, out error));
            }
            catch (Exception ex) { error = ex.ToString(); }
            return false;

        }

        #endregion

        #region getRecordByUID
        /// <summary>
        /// "select * from {0} where {1}='{2}' ", tblName, UID_FieldName, UID_FieldValue
        /// </summary>
        /// <param name="EmailTemplate_ID"></param>
        /// <returns></returns>
        public Hashtable getRecordByUID(string tblName, string UID_FieldName, string UID_FieldValue, out string error)
        {
            error = "";
            try
            {
                string sql = String.Format("select * from {0} with(nolock) where {1} = '{2}' ", tblName, UID_FieldName, UID_FieldValue.Replace("'", "''"));
                SqlDataReader reader = ExecuteReader(sql, null);
                bool dummy;
                return ReaderRecord2Hashtable(reader, true, out dummy);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return new Hashtable();//to have the error respond like in return ReaderRecord2Hashtable(reader, true);
        }

        /// <summary>
        /// "select * from {0} where {1}='{2}' ", tbl_name, UID_FieldName, UID_FieldValue
        /// </summary>
        /// <param name="EmailTemplate_ID"></param>
        /// <returns></returns>
        public Hashtable getRecordByUID(string UID_FieldName, string UID_FieldValue, out string error)
        {
            return getRecordByUID(tbl_name, UID_FieldName, UID_FieldValue, out error);
        }

        #endregion

        #region getFieldByUID
        /// <summary>
        /// "select {3} from {0} where {1}='{2}' ", tbl_name, UID_FieldName, UID_FieldValue, FieldName
        /// </summary>
        /// <param name="UID_FieldName"></param>
        /// <param name="UID_FieldValue"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public string getFieldByUID(string FieldName, string UID_FieldName, string UID_FieldValue, out string error)
        {
            return getFieldByUID(FieldName, UID_FieldName, UID_FieldValue, "", out  error);
        }

        /// <summary>
        /// "select {3} from {0} where {1}='{2}' ", tbl_name, UID_FieldName, UID_FieldValue, FieldName
        /// or "select {3} from {0} where {1}='{2}' AND {$where}", if where!="".
        /// </summary>
        /// <param name="EmailTemplate_ID"></param>
        /// <returns></returns>
        public string getFieldByUID(string FieldName, string UID_FieldName, string UID_FieldValue, string where, out string error)
        {
            error = "";
            if (FieldName.Length == 0 ||
                UID_FieldName.Length == 0 ||
                UID_FieldValue.Length == 0)
            {
                error = "empty arg. (35741).";
                return "";
            }
            try
            {
                string sql = String.Format("select {3} from {0} with(nolock) where {1}='{2}' ", tbl_name, UID_FieldName, UID_FieldValue.Replace("'", "''"), FieldName);
                if (where.Length > 0)
                    sql += " AND " + where;
                SqlDataReader reader = ExecuteReader(sql, null);
                bool dummy;
                Hashtable ht = ReaderRecord2Hashtable(reader, true, out dummy);
                if (ht.Count > 0)
                {
                    return ht[UID_FieldName].ToString();
                }
                else
                    return "";

            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return "";
        }

        #endregion

        #region Hasht
        /// <summary>
        /// return ((Hashtable)o)[key].ToString();
        /// </summary>
        /// <param name="o"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getHashtableField(Object o, string key)
        {
            try
            {
                if (o.GetType().Name.Equals("Hashtable")) //== //Type.GetType("Hashtable") )
                {
                    return ((Hashtable)o)[key].ToString().TrimEnd();
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// reader should be configured as CommandBehavior.CloseConnection
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="is_close"></param>
        /// <param name="hasMoreRecords"></param>
        /// <returns></returns>
        public Hashtable ReaderRecord2Hashtable(SqlDataReader reader, bool is_close, out bool hasMoreRecords)
        {
            hasMoreRecords = false;
            Hashtable ht = new Hashtable();
            try
            {
                hasMoreRecords = reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                    ht.Add(reader.GetName(i), reader[i]);
            }
            catch //(Exception ex) 
            { ht.Clear(); }
            finally { if (is_close) reader.Close(); }
            return ht;
        }
        #endregion

        #region Other
        /// <summary>
        /// Guid.Empty in case of wrong s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid GuidOrEmpty(string s)
        {
            if (string.IsNullOrEmpty(s))
                return Guid.Empty;

            try { return new Guid(s); }
            catch { return Guid.Empty; }
        }

        /// <summary>
        /// DateTime.MinValue in case of wrong s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime DateTimeOrMinValue(string s)
        {
            if (string.IsNullOrEmpty(s))
                return DateTime.MinValue;
            DateTime dt;

            if (DateTime.TryParse(s, out dt))
                return dt;

            return DateTime.MinValue;
        }

        public static int IntOrYourValue(string s, int YourValue)
        {
            if (string.IsNullOrEmpty(s))
                return YourValue;
            int i;

            if (int.TryParse(s, out i))
                return i;

            return YourValue;
        }

        public static bool boolOrYourValue(string s, bool YourValue)
        {
            if (string.IsNullOrEmpty(s))
                return YourValue;
            bool i;

            if (bool.TryParse(s, out i))
                return i;

            return YourValue;
        }

        #endregion
    }
}