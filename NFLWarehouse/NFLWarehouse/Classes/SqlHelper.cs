using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using NFLWarehouse.Forms;

namespace NFLWarehouse.Classes
{

    public class SqlHelper : IDisposable
    {

        #region Global Variables
        protected readonly ConnectionStringManager ConnStrMgr;
        protected readonly SqlConnection Connection;

        ConnectionStringSettingsCollection settings =
        ConfigurationManager.ConnectionStrings;
        private System.Windows.Forms.Form commingFrom;

        // Messages
        string message1 = "connection completed.";
        string message2 = "experiencing connection issues...";
        #endregion



        public SqlHelper(string connectionName)
        {
            ConnStrMgr = new ConnectionStringManager(connectionName);
            Connection = new SqlConnection(ConnStrMgr.ToString());
           
        }

        public SqlHelper(string connectionName, System.Windows.Forms.Form commingFrom)
        {
            ConnStrMgr = new ConnectionStringManager(connectionName);
            Connection = new SqlConnection(ConnStrMgr.ToString());
            this.commingFrom = commingFrom;
            
        }
        public void Dispose()
        {
            if (Connection != null)
                Connection.Close();
        }

        public void Open()
        {
            try
            {
                if (Connection != null)
                    Connection.Open();
            }catch(Exception ex)
            {
                string msg = "experiencing connection issues.." ;
                Console.WriteLine(ex);
                Console.WriteLine(msg);
            }
        }
        

        public async Task OpenAsync()
        {
            try
            {
            if (Connection != null)
                await Connection.OpenAsync();
                MsgTypes.printme(MsgTypes.msg_success, message1, commingFrom);

                if(commingFrom.Text.Contains("Scan In Station"))
                {
                    NFLWarehouse.Forms.FrmScanIn frm = (NFLWarehouse.Forms.FrmScanIn)commingFrom;
                    frm.releaseForm();
                }
                else if(commingFrom.Text.Contains("Scan Out Station"))
                {
                    NFLWarehouse.Forms.FrmScanout frm = (NFLWarehouse.Forms.FrmScanout)commingFrom;
                    frm.releaseForm();
                }
                else if(commingFrom.Text.Contains("Shipping"))
                {
                    NFLWarehouse.Forms.FrmShipping frm = (NFLWarehouse.Forms.FrmShipping)commingFrom;
                    frm.releaseForm();
                }
            }
            catch (Exception ex)
            {
                MsgTypes.printme(MsgTypes.msg_success, message2, commingFrom);
                Console.WriteLine(message2);
                Console.WriteLine(ex);
            }
        }
        

        public List<SqlTableRow> ExecuteReader(string sql, IEnumerable<SqlParameter> parameters = null)
        {
            List<SqlTableRow> allRows = new List<SqlTableRow>();

            if (parameters == null)
                parameters = new List<SqlParameter>();

            using (var cmd = new SqlCommand(sql, Connection))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SqlTableRow row = new SqlTableRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Process each column as appropriate
                        object fieldValue = reader.GetFieldValue<object>(i);
                        row.FieldValues.Add(fieldValue);
                    }
                    if (row.FieldValues.Count > 0)
                        allRows.Add(row);
                }
                reader.Close();
            }
            return allRows;
        }

        public async Task<List<SqlTableRow>>
            ExecuteReaderAsync(string sql, IEnumerable<SqlParameter> parameters = null)
        {
            List<SqlTableRow> allRows = new List<SqlTableRow>();

            if (parameters == null)
                parameters = new List<SqlParameter>();

            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(parameters.ToArray());

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    SqlTableRow row = new SqlTableRow();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Process each column as appropriate
                        object fieldValue = await reader.GetFieldValueAsync<object>(i);
                        row.FieldValues.Add(fieldValue);
                    }
                    if (row.FieldValues.Count > 0)
                        allRows.Add(row);
                }
            }
            return allRows;
        }
    }

}
