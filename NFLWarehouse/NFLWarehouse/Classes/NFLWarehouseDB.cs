using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NFLWarehouse.Classes;
using System.Net;

// November 25: Transactions is not done.  Next thing is to add Allocation to GUI.

namespace NFLWarehouse.Classes
{
    class NFLWarehouseDB : SqlHelper 
    {
        #region Global Variables
        int allocated = 1;
        int floating = 2;
        int shipped = 3;
        #endregion

        System.Windows.Forms.Form commingFrom;
        public NFLWarehouseDB(): base("nfl")
        {

        }
        public NFLWarehouseDB(System.Windows.Forms.Form commingForm) : base("nfl")
        {
            this.commingFrom = commingFrom;
        }



        #region  Inventory Transactions
        public bool AllocateTote(string Tote, string location)
        {
            int locationId;
            //Checking if provided location exists
            if (LocationExists(location))
            {// Location provided is valid, get locationid
                locationId = GetLocationId(location);
            }
            else
            {// Location is invalid, interrupt allocation transaction.
                MsgTypes.printme(MsgTypes.msg_failure, "Location " + location + " is not created.", commingFrom);
                return false;
            }

            try
            {
                if (ToteExists(Tote))
                {// Tote in system, checking status
                    int toteid = GetToteId(Tote);
                    if (IsToteFloating(Tote))
                    {// If Tote is Floating, then update location to location provided
                        UpdateLocation(toteid, locationId);
                        UpdateStatus(toteid, allocated);
                        return true;
                    }else
                    {// if tote is Allocated, then return warning message without updating location
                        MsgTypes.printme(MsgTypes.msg_failure, "Tote " +  Tote + " is allocated in " + GetLocation(toteid), commingFrom);
                        return false;
                    }
                }else
                {// Tote not in system, inserting new Tote
                    InsertTote(Tote, locationId, Dns.GetHostName(),allocated);
                    return true;
                }
                return true;
            }
            catch(Exception ex)
            {
                MsgTypes.printme(MsgTypes.msg_failure, "Could not allocate tote " + Tote, commingFrom);
                return false;
            }
            
        }
        public bool ReleaseTote(int ToteId, string hostname)
        {
            UpdateLocation(ToteId, 2);
            UpdateStatus(ToteId, 1);
            return true;
        }
        public bool Shiptote()
        {
            return true;
        }
        #endregion


        #region Database transactions
        public bool InsertTote(string Tote, int LocationId,string hostname, int statusId)  
        {
            string sql;
            try
            {
                // If tote is not created. Insert new record
                sql = "INSERT INTO dbo.tbl_NFLWarehouse_Tote (Name,LocationId,CreatedOn,CreatedBy,StatusId) ";
                sql += "VALUES(@Name,@LocationId,@CreatedOn,@CreatedBy,@StatusId)";


                // Query parameters setup
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Name",Tote),
                    new SqlParameter("@LocationId",LocationId),
                    new SqlParameter("@CreatedOn",DateTime.Now),
                    new SqlParameter("@CreatedBy",hostname),
                    new SqlParameter("@StatusId",statusId)
                };
                ExecuteReader(sql, parameters);
               
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool InsertTransaction(int toteid,int locationid,int transactiontypeid,string hostname,int statusIn, int statusOut,string ip)
        {
            return true;
        }
        public bool UpdateLocation(int ToteId, int LocationId)
        {
            string sql;
            try
            {
                sql = "UPATE dbo.tbl_NFLWarehouse_Tote SET LocationId = @LocationId WHERE TOTE.ToteId = @ToteId";
                var parameters = new List<SqlParameter> 
                { new SqlParameter("@LocationId", LocationId),
                  new SqlParameter("@ToteId", ToteId) };
                ExecuteReader(sql, parameters);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public bool UpdateStatus(int ToteId, int StatusId)
        {
            string sql;
            try
            {
                sql = "UPATE dbo.tbl_NFLWarehouse_Tote SET StatusId = @StatusId WHERE TOTE.ToteId = @ToteId";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@StatusId", StatusId),
                  new SqlParameter("@ToteId", ToteId) };
                ExecuteReader(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool querytest()
        {
            string sql = "SELECT COUNT(*) FROM dbo.tbl_NFLWarehouse_Tote";
            ExecuteReader(sql);
            
            return true;
        }
        #endregion

        #region Support methods
        public bool IsToteFloating(string Tote)
        {
            try
            {
                string sql = "SELECT StatusId FROM tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = '@Name'";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@Name",Tote)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    int statusid = Int32.Parse(rows[0].FieldValues[0].ToString());
                    if(statusid == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ToteExists(string Tote)
        {
            try
            {
                string sql = "SELECT ToteId FROM tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = '@Name'";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@Name",Tote)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int GetToteId(string Tote)
        {
            try
            {
                string sql = "SELECT ToteId FROM dbo.tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = '@Name'";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@Name",Tote)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    return Int32.Parse(rows[0].FieldValues[0].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string GetLocation(string Tote)
        {
            try
            {
                string sql = "SELECT LocationId FROM dbo.tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = '@Name'";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@Name",Tote)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    int locationId = Int32.Parse(rows[0].FieldValues[0].ToString());
                    return GetLocation(locationId);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public bool LocationExists(string Location)
        {
            try
            {
                string sql = String.Format("SELECT locationId FROM tbl_NFLWarehouse_Master_Location /*WHERE Name = '{0}'*/",Location);

                // Passign parameteres is baddly implemented. Review later.
                //var parameters = new List<SqlParameter>
                //{ new SqlParameter("@Name",Location)};
                //var rows = ExecuteReader(sql, parameters);

                var rows = ExecuteReader(sql);
                if (rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int GetLocationId(string Location)
        {
            try
            {
                string sql = "SELECT LocationId FROM dbo.tbl_NFLWarehouse_Master_Location LOC WHERE LOC.Name = '@Location'";
                var parameters = new List<SqlParameter>
                {new SqlParameter("@Location",Location)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    return Int32.Parse(rows[0].FieldValues[0].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public string GetLocationName(int locationId)
        {
            try
            {
                string sql = "SELECT Name FROM dbo.tbl_NFLWarehouse_Master_Location LOC WHERE LOC.Name = '@locationId'";
                var parameters = new List<SqlParameter>
                {new SqlParameter("@locationId",locationId)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    return rows[0].FieldValues[0].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string GetLocation(int toteid)
        {
            try
            {
                string sql = "SELECT Name FROM dbo.tbl_NFLWarehouse_Tote TOTE WHERE TOTE.ToteId = '@toteid'";
                var parameters = new List<SqlParameter>
                {new SqlParameter("@toteid",toteid)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    return rows[0].FieldValues[0].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion
    }
}
