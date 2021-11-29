using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using NFLWarehouse.Classes;


// November 25: Transactions is not done.  Next thing is to add Allocation to GUI.

namespace NFLWarehouse.Classes
{
    public class NFLWarehouseDB : SqlHelper
    {
        #region Global Variables
        string version;
        System.Windows.Forms.Form commingFrom;
        // Messages
        string message1 = "Location {0} is not created. Please let know supervisor to review if location needs to be created.";
        string message2 = "tote allocated: {0} in {1}";
        string message3 = "Tote {0} is already allocated in {1}";
        string message4 = "Could not record this transaction. Please check network connection.";
        string message5 = "Tote {0} location updated from {1} to {2}";
        string message6 = "Database unreachable when {}";
        string message7 = "Tote {0} is now floating, transfer it to its destination.";
        string message8 = "Tote {0} is now set as shipped, take it to loading process.";
        string message9 = "Tote {0} is still allocated, if tote needs to be shipped, scan it out first.";
        string message10 = "Tote {0} is already Shipped!, notify supervisor about this Tote.";
        string message11 = "Tote {0} is looping back from Shipping status, allocating in {1}";
        #endregion

        public NFLWarehouseDB(): base("nfl")
        {
            SetVersion();
        }
        public NFLWarehouseDB(System.Windows.Forms.Form commingFrom) : base("nfl", commingFrom)
        {
            this.commingFrom = commingFrom;
            SetVersion();
        }


        #region Logic
        public void SetVersion()
        {
            if (!Tools.isDebugMode())
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                version = "debug";
            }
        }
        #endregion


        #region  Inventory Transactions
        public bool AllocateTote(string Tote, string location)
        {
            int locationIn = Location.None;
            int locationOut = Location.None;
            int statusIn = GetStatusId(GetToteId(Tote));
            int statusOut = Status.statusAllocated;
            int toteid = 0;

            //Checking if provided location exists.
            if (LocationExists(location))
            {// Location provided is valid, get locationid
                locationOut = GetLocationId(location);
            }
            else
            {// Location is invalid, interrupt allocation transaction.
                MsgTypes.printme(MsgTypes.msg_failure, String.Format(message1,location), commingFrom);

                InsertTransaction(toteid, Tote, location, 0, 0, TransactionType.transactionTypeInsert, Dns.GetHostName(),
                             GetStatusId(toteid), GetStatusId(toteid), Tools.GetLocalIPAddress(), TransactionType.Failed,version);

                return false;
            }
            //Inserting tote.
            try
            {
                if (ToteExists(Tote))
                {// Tote in system, checking status
                    toteid = GetToteId(Tote);
                    locationIn = GetLocationId(Tote);

                    statusIn = GetStatusId(toteid);
                    if (IsToteFloating(Tote) || IsToteShipped(Tote))
                    {// If Tote is Floating, then update location to location provided

                        UpdateStatusLocation(toteid, statusOut, locationOut);

                        if (statusIn == Status.statusShipped)
                        {
                            MsgTypes.printme(MsgTypes.msg_failure, String.Format(message11, Tote, location), commingFrom);
                        }
                        else
                        {
                            MsgTypes.printme(MsgTypes.msg_failure, String.Format(message5, Tote, locationIn,locationOut), commingFrom);
                        }
                        return true;
                    }else
                    {// if tote is Allocated, then return warning message without updating location
                        MsgTypes.printme(MsgTypes.msg_failure, String.Format(message3,Tote,GetLocation(Tote)), commingFrom);
                        InsertTransaction(toteid,Tote,location, locationIn, locationOut, TransactionType.transactionTypeInsert, Dns.GetHostName(), 
                                          GetStatusId(toteid), GetStatusId(toteid), Tools.GetLocalIPAddress(), TransactionType.Failed,version);
                        return false;
                    }
                }else
                {// Tote not in system, inserting new Tote
                    InsertTote(Tote, locationOut, Dns.GetHostName(), Status.statusAllocated);
                    toteid = GetToteId(Tote);
                    MsgTypes.printme(MsgTypes.msg_success,String.Format(message2,Tote,location),commingFrom);
                    return true;
                }
        
            }
            catch(Exception ex)
            {
                MsgTypes.printme(MsgTypes.msg_failure, "Could not allocate tote " + Tote, commingFrom);
                MsgTypes.printme(MsgTypes.msg_failure, String.Format(message3, Tote, GetLocation(Tote)), commingFrom);
                InsertTransaction(toteid,Tote,location, statusIn, statusOut, TransactionType.transactionTypeInsert, Dns.GetHostName(),
                                  GetStatusId(toteid), GetStatusId(toteid), Tools.GetLocalIPAddress(), TransactionType.Failed,version);
                Console.WriteLine(ex);
                return false;
            }
            
        }
        public bool ReleaseTote(int toteid)
        {
            UpdateStatusLocation(toteid, 2, 1);
            MsgTypes.printme(MsgTypes.msg_success, String.Format(message7, GetToteName(toteid)), commingFrom);
            return true;
        }
        public bool ReleaseTote(string Tote)
        {
            int toteid = GetToteId(Tote);

            UpdateStatusLocation(toteid, 2, 1);
            MsgTypes.printme(MsgTypes.msg_success, String.Format(message7, Tote), commingFrom);
            return true;
        }
        public bool ShipTote(string Tote)
        {
            int toteid = GetToteId(Tote);

            if(GetStatusId(toteid)== Status.statusFloating)
            {
                UpdateStatusLocation(toteid, Status.statusShipped, Location.None);
                MsgTypes.printme(MsgTypes.msg_success, String.Format(message8, Tote), commingFrom);
                return true;
            }else if(GetStatusId(toteid) == Status.statusShipped)
            {
                MsgTypes.printme(MsgTypes.msg_success, String.Format(message10, Tote), commingFrom);
                InsertTransaction(toteid, Tote, GetLocationName(Location.None), GetLocationId(toteid), Location.None,
                    TransactionType.transactionTypeUpdate, Dns.GetHostName(), GetStatusId(toteid), Status.statusShipped, Tools.GetLocalIPAddress(),
                    TransactionType.Failed, version);
                return false;
            }
            else
            {
                MsgTypes.printme(MsgTypes.msg_success, String.Format(message9, Tote), commingFrom);
                InsertTransaction(toteid, Tote, GetLocationName(Location.None), GetLocationId(toteid), Location.None,
                    TransactionType.transactionTypeUpdate, Dns.GetHostName(), GetStatusId(toteid), Status.statusShipped, Tools.GetLocalIPAddress(),
                    TransactionType.Failed, version);
                return false;
            }
        }
     
        #endregion


        #region Database transactions
        public bool InsertTote(string Tote, int LocationId,string hostname, int statusId)  
        {
            // TODO: change to object oriented.
            int toteid = GetToteId(Tote);
            int statusIn = GetStatusId(toteid);
            int statusOut = statusId;
            int locationIn = Location.None;
            int locationOut = LocationId;
            string ip = Tools.GetLocalIPAddress();
            int transactionType = TransactionType.transactionTypeInsert;
            
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
                toteid = GetToteId(Tote);
                InsertTransaction(toteid,Tote,"", locationIn, locationOut, transactionType, hostname, statusIn, statusOut, ip, TransactionType.Succeeded,version);
                return true;
            }
            catch(Exception ex)
            {
                InsertTransaction(toteid, Tote,"",locationIn, locationOut, transactionType, hostname, statusIn, statusOut, ip, TransactionType.Failed,version);
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateStatusLocation(int ToteId, int StatusId, int LocationId)
        {
            string sql;
            int locationIn = GetToteLocationId(GetToteName(ToteId));
            int locationOut = LocationId;
            int statusIn = GetStatusId(ToteId);
            int statusOut = StatusId;
            try
            {
                sql = "UPDATE dbo.tbl_NFLWarehouse_Tote SET StatusId = @StatusId, LocationId = @LocationId, " +
                      "ModifiedOn = @ModifiedOn, ModifiedBy = @ModifiedBy WHERE ToteId = @ToteId";
                var parameters = new List<SqlParameter>
                { 
                    new SqlParameter("@StatusId", StatusId),
                    new SqlParameter("@LocationId", LocationId),
                    new SqlParameter("@ToteId", ToteId),
                    new SqlParameter("@ModifiedOn", DateTime.Now),
                    new SqlParameter("@ModifiedBy", Dns.GetHostName())
                };
                ExecuteReader(sql, parameters);
                InsertTransaction(ToteId, GetToteName(ToteId), GetLocationName(LocationId), locationIn,
                    locationOut, TransactionType.transactionTypeUpdate, Dns.GetHostName(), statusIn, statusOut,
                    Tools.GetLocalIPAddress(), TransactionType.Succeeded, version);
                return true;
            }
            catch (Exception ex)
            {
                InsertTransaction(ToteId, GetToteName(ToteId), GetLocationName(LocationId), locationIn,
                   locationOut, TransactionType.transactionTypeUpdate, Dns.GetHostName(), statusIn, statusOut,
                   Tools.GetLocalIPAddress(), TransactionType.Succeeded, version);
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateLocation(int ToteId, int LocationId)
        {
            string sql;
            try
            {
                sql = "UPDATE dbo.tbl_NFLWarehouse_Tote SET LocationId = @LocationId," +
                      "ModifiedOn = @ModifiedOn, ModifiedBy = @ModifiedBy WHERE ToteId = @ToteId";
                var parameters = new List<SqlParameter> 
                { 
                    new SqlParameter("@LocationId", LocationId),
                    new SqlParameter("@ToteId", ToteId),
                    new SqlParameter("@ModifiedOn", DateTime.Now),
                    new SqlParameter("@ModifiedBy", Dns.GetHostName())
                };
                ExecuteReader(sql, parameters);
                InsertTransaction(ToteId, GetToteName(ToteId), GetLocationName(LocationId),GetToteLocationId(GetToteName(ToteId)),
                    LocationId, TransactionType.transactionTypeUpdate, Dns.GetHostName(), GetStatusId(ToteId), GetStatusId(ToteId),
                    Tools.GetLocalIPAddress(), TransactionType.Succeeded, version);
                return true;
            }catch(Exception ex)
            {
                InsertTransaction(ToteId, GetToteName(ToteId), GetLocationName(LocationId), GetToteLocationId(GetToteName(ToteId)),
                   LocationId, TransactionType.transactionTypeUpdate, Dns.GetHostName(), GetStatusId(ToteId), Status.statusFloating,
                   Tools.GetLocalIPAddress(), TransactionType.Failed, version);
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateStatus(int ToteId, int StatusId)
        {
            int statusIn = GetStatusId(ToteId);
            int statusOut = StatusId;
            int locationIn = GetLocationId(ToteId);
            int locationOut = GetLocationId(ToteId);
            string ip = Tools.GetLocalIPAddress();
            string hostname = Dns.GetHostName();
            string Tote = GetToteName(ToteId);

            string sql;
         
            try
            {
                sql = "UPDATE dbo.tbl_NFLWarehouse_Tote SET StatusId = @StatusId," +
                      "ModifiedOn = @ModifiedOn, ModifiedBy = @ModifiedBy WHERE ToteId = @ToteId";
                var parameters = new List<SqlParameter>
                { 
                    new SqlParameter("@StatusId", StatusId),
                    new SqlParameter("@ToteId", ToteId),
                    new SqlParameter("@ModifiedOn", DateTime.Now),
                    new SqlParameter("@ModifiedBy", Dns.GetHostName())
                };
                ExecuteReader(sql, parameters);
                InsertTransaction(ToteId,Tote, "",locationIn, locationOut, TransactionType.transactionTypeUpdate, hostname, statusIn, statusOut,ip, TransactionType.Succeeded,version);
                return true;
            }
            catch (Exception ex)
            {
                InsertTransaction(ToteId, Tote,"",locationIn, locationOut, TransactionType.transactionTypeUpdate, hostname, statusIn, statusOut,ip, TransactionType.Failed,version);
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool InsertTransaction(int toteid,string tote,string location, int locationIn,int locationOut, int transactiontypeid, string hostname,
        int statusIn, int statusOut, string ip, int Success, string toolversion)
        {
            try
            {
                string sql = "INSERT INTO dbo.tbl_NFLWarehouse_Transaction VALUES(@toteid,@tote,@location,@IncomingLocationId,@OutgoingLocationId,";
                sql += "@TransactionTypeId,@CreatedOn,@CreatedBy,@IncomingStatusId,@OutgoingStatusId,@Ipaddress,@Success,@ToolVersion)";
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@toteid",toteid),
                new SqlParameter("@tote",tote),
                new SqlParameter("@location",location),
                new SqlParameter("@IncomingLocationId",locationIn),
                new SqlParameter("@OutgoingLocationId",locationOut),
                new SqlParameter("@TransactionTypeId",transactiontypeid),
                new SqlParameter("@CreatedOn",DateTime.Now),
                new SqlParameter("@CreatedBy",hostname),
                new SqlParameter("@IncomingStatusId",statusIn),
                new SqlParameter("@OutgoingStatusId",statusOut),
                new SqlParameter("@Ipaddress",ip),
                new SqlParameter("@Success", Success),
                new SqlParameter("@ToolVersion", toolversion)
            };
                ExecuteReader(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                MsgTypes.printme(MsgTypes.msg_failure, message4, commingFrom);
                Console.WriteLine(ex);
                return false;
            }

        }
        #endregion



        #region Support methods
        public bool IsToteFloating(string Tote)
        {
            try
            {
                string sql = "SELECT StatusId FROM tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = @Name";
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
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool IsToteShipped(string Tote)
        {
            try
            {
                string sql = "SELECT StatusId FROM tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = @Name";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@Name",Tote)};
                var rows = ExecuteReader(sql, parameters);
                if (rows.Count > 0)
                {
                    int statusid = Int32.Parse(rows[0].FieldValues[0].ToString());
                    if (statusid == 3)
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
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool ToteExists(string Tote)
        {
            try
            {
                string sql = "SELECT ToteId FROM tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = @Name";
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
                Console.WriteLine(ex);
                return false;
            }
        }
        public int GetToteId(string Tote)
        {
            try
            {
                string sql = "SELECT ToteId FROM dbo.tbl_NFLWarehouse_Tote TOTE WHERE TOTE.Name = @Name";
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
                Console.WriteLine(ex);
                return 0;
            }
        }
        public string GetToteName(int toteid)
        {
            try
            {
                string sql = "SELECT Name FROM dbo.tbl_NFLWarehouse_Tote TOTE WHERE TOTE.ToteId = @toteid";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@toteid",toteid)};
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
                Console.WriteLine(ex);
                return "";
            }
        }
        public string GetLocation(string Tote)
        {
            try
            {
                int toteid = GetToteId(Tote);
                int locationid = GetLocationId(toteid);
            
                string sql = "SELECT Name FROM dbo.tbl_NFLWarehouse_Master_Location WHERE locationId = @locationId";
                var parameters = new List<SqlParameter>
                { new SqlParameter("@locationId",locationid)};
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
                Console.WriteLine(ex);
                return "";
            }
        }
        public int GetToteLocationId(string Tote)
        {
            try
            {
                int toteid = GetToteId(Tote);
                int locationid = GetLocationId(toteid);
                return locationid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }
        public int GetStatusId(int toteid)
        {
            try
            {
                string sql = String.Format("SELECT StatusId FROM dbo.tbl_NFLWarehouse_Tote WHERE ToteId = @toteid", toteid);
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@toteid",toteid),
                };
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
                Console.WriteLine(ex);
                return 0;
            }
        }
        public string GetCreationDate(int toteid)
        {
            try
            {
                string sql = String.Format("SELECT CreatedOn FROM dbo.tbl_NFLWarehouse_Tote WHERE ToteId = @toteid", toteid);
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@toteid",toteid),
                };
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
                Console.WriteLine(ex);
                return "";
            }
        }
        public string GetCreatedBy(int toteid)
        {
            try
            {
                string sql = String.Format("SELECT CreatedBy FROM dbo.tbl_NFLWarehouse_Tote WHERE ToteId = @toteid", toteid);
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@toteid",toteid),
                };
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
                Console.WriteLine(ex);
                return "";
            }
        }
        public string GetStatusName(int statusid)
        {
            try
            {
                string sql = String.Format("SELECT Name FROM dbo.tbl_NFLWarehouse_Master_Status WHERE StatusId = @StatusId", statusid);
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@StatusId",statusid),
                };
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
                Console.WriteLine(ex);
                return "";
            }
        }
        public int GetLocationId(int toteid)
        {
            try
            {
                string sql = String.Format("SELECT LocationId FROM dbo.tbl_NFLWarehouse_Tote WHERE ToteId = @toteid", toteid);
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@toteid",toteid),
                };
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
                Console.WriteLine(ex);
                return 0;
            }
        }
        public bool LocationExists(string Location)
        {
            try
            {
                string sql = "SELECT locationId FROM tbl_NFLWarehouse_Master_Location WHERE Name = @Name";

                var parameters = new List<SqlParameter>
                { new SqlParameter("@Name",Location)};
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
                MsgTypes.printme(MsgTypes.msg_failure, String.Format(message6, "querying location."), commingFrom);
                Console.WriteLine(ex);
                return false;
            }
        }
        public int GetLocationId(string Location)
        {
            try
            {
                string sql = String.Format("SELECT LocationId FROM dbo.tbl_NFLWarehouse_Master_Location WHERE Name = @Location",Location);
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Location",Location),
                };
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
                Console.WriteLine(ex);
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
                Console.WriteLine(ex);
                return "";
            }
        }
        #endregion
    }
}
