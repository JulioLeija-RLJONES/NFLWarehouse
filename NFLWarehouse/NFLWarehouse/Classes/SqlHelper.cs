using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using NFLWarehouse.Classes;
using System.Configuration;

namespace NFLWarehouse.Classes
{

    public class SqlHelper : IDisposable
    {
        protected readonly ConnectionStringManager ConnStrMgr;
        protected readonly SqlConnection Connection;

        ConnectionStringSettingsCollection settings =
        ConfigurationManager.ConnectionStrings;

        public SqlHelper(string connectionName)
        {
            if(connectionName == "nfl")
            {
                Connection = new SqlConnection(settings[1].ConnectionString.ToString());
            }
            
            Console.WriteLine("using connection: " + Connection.Database);
            Console.WriteLine("passed connection name" + connectionName.ToString());


            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("connection string");
            Console.WriteLine(settings[3].ConnectionString.ToString());
            Console.WriteLine("----------------------------------------------");

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
                string msg = "experiencing connection issues..";
                Console.WriteLine(msg);
            }
        }
        

        public async Task OpenAsync()
        {
            try
            {
            if (Connection != null)
                await Connection.OpenAsync();
            }
            catch (Exception ex)
            {
                string msg = "experiencing connection issues..";
                Console.WriteLine(msg);
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
