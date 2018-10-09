using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using PatrolWebApp.Controllers;
using Npgsql;

namespace PatrolWebApp.Controllers
{
    public  class devicecls
    {
        public int deviceid { get; set; }
        public string devicenumber{ get; set; }

        public int ahwalid { get; set; }

        public string model { get; set; }
        public int devicetypeid { get; set; }

        public int defective { get; set; }

        public int rental { get; set; }

        public string barcode { get; set; }
    }
    
    [Route("api/[controller]")]
    public class MaintainenceController : Controller
    {

        // public String constr2 = "Server=BCI666016PC57;Database=patrols;User Id =patrol;Password=patrol;";
        public String constr = "server=localhost;Port=5432;User Id=postgres;password=admin;Database=PatrolWebApp";


        [HttpPost("adddevices")]
        public int PostAddDevices( [FromBody]devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "insert into devices(AhwalID,devicenumber,model,devicetypeid,defective,rental,barcode) values (" + frm.ahwalid + ",'" + frm.devicenumber + "'," + frm.model + "," + frm.devicetypeid + "," + frm.defective + "," + frm.rental  + ",'" + frm.barcode + "')";
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }

        [HttpPost("updatedevices")]
        public int PostUpdateDevices([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "update devices set AhwalID = " + frm.ahwalid + ",devicenumber = '" + frm.devicenumber + "',model = '" + frm.model + "',devicetypeid='" + frm.devicetypeid + "',defective = " + frm.defective + ",rental = " + frm.rental + ",barcode = '" + frm.barcode + "' where deviceid=" + frm.deviceid ;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }


        [HttpPost("deldevices")]
        public int PostDeleteDevices([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "delete from devices  where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();
            return ret;
        }


       

        [HttpPost("deviceslist")]
        public DataTable PostDevicesList2()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            //            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,t.name as type,d.Defective,d.Rental,d.BarCode,a.Name from Devices d INNER JOIN Ahwal a ON a.AhwalID = d.AhwalID inner join devicetypes t on t.devicetypeid = d.devicetypeid ", cont);
            //NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,(select dt.name from devicetypes dt where dt.devicetypeid = d.devicetypeid)  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,'1'  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }


        [HttpPost("devicesinventory")]
        public DataTable PostDevicesInventoryList()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            string Qry = "SELECT        DeviceCheckInOutID, CheckInOutStates.Name AS StateName, Ahwal.AhwalID, Ahwal.Name AS AhwalName, Devices.deviceNumber, Devices.Model,(select dt.name from devicetypes dt where dt.devicetypeid = devices.devicetypeid) as Type, Persons.MilNumber, ";
            Qry = Qry + " Ranks.Name AS PersonRank, Persons.Name AS PersonName, DevicesCheckInOut.SavedTime, CheckInOutStates.CheckInOutStateID";

            Qry = Qry + "  FROM Ahwal INNER JOIN";

            Qry = Qry + " Devices  ON Ahwal.AhwalID = Devices.AhwalID INNER JOIN";

            Qry = Qry + " DevicesCheckInOut ON Devices.DeviceID = DevicesCheckInOut.DeviceID INNER JOIN";

            Qry = Qry + " CheckInOutStates ON DevicesCheckInOut.CheckInOutStateID = CheckInOutStates.CheckInOutStateID INNER JOIN";

            Qry = Qry + " Persons ON Ahwal.AhwalID = Persons.AhwalID AND DevicesCheckInOut.PersonID = Persons.PersonID INNER JOIN";

            Qry = Qry + " Ranks ON Persons.RankID = Ranks.RankID";
            Qry = Qry + "  ORDER BY DevicesCheckInOut.SavedTime";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Qry, cont);
            dt.Columns.Add("devicecheckinoutid");
            dt.Columns.Add("statename");
            dt.Columns.Add("ahwalid");
            dt.Columns.Add("ahwalname");
            dt.Columns.Add("devicenumber");
            dt.Columns.Add("model");

            dt.Columns.Add("type");
            dt.Columns.Add("milnumber");
            dt.Columns.Add("personrank");
            dt.Columns.Add("personname");
            dt.Columns.Add("savedtime");
            dt.Columns.Add("checkinoutstateid");
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }


        [HttpPost("dispatchlist")]
        public DataTable Postdispatchlist()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            String Qry = "SELECT AhwalMappingID, AhwalID, ShiftID, SectorID, DeviceRoleID, CityGroupID,(Select MilNumber From Persons where PersonID = AhwalMapping.PersonID) as MilNumber,";
Qry = Qry + " (Select RankID From Persons where PersonID = AhwalMapping.PersonID) as RankID, (Select Name From Persons where PersonID = AhwalMapping.PersonID) as PersonName, CallerID,  ";
Qry = Qry + " HasDevices, '' as Serial,  (Select DeviceNumber From Devices where DeviceID = AhwalMapping.DeviceID) as DeviceNumber, ";
            Qry = Qry + " DevicePersonStateID, SunRiseTimeStamp, SunSetTimeStamp, SortingIndex,(Select Mobile From Persons where PersonID = AhwalMapping.PersonID) as PersonMobile,IncidentID,";
            Qry = Qry + " LastStateChangeTimeStamp,(Select ShortName From sectors where SectorID=AhwalMapping.SectorID) as SectorDesc , (Select (select Name from Ranks where rankid = persons.rankid) From Persons where PersonID=AhwalMapping.PersonID) as RankDesc FROM AhwalMapping ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Qry, cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }

        [HttpPost("organizationlist")]
        public DataTable PostOrganizationList()
        {
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            String Qry = "SELECT name as value, name as text FROM Ahwal ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Qry, cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();

            return dt;
        }

        [HttpPost("devicetyplist")]
        public DataTable Postdevicetyplist()
        {
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            String Qry = "SELECT id as value, name as text FROM devicetypes ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Qry, cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();

            return dt;
        }

        /*Hand Helds*/
        #region Hand Helds
        [HttpPost("addhandhelds")]
        public int PostAddHandhelds([FromBody]devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "insert into devices(AhwalID,devicenumber,model,devicetypeid,defective,rental,barcode) values (" + frm.ahwalid + ",'" + frm.devicenumber + "'," + frm.model + "," + frm.devicetypeid + "," + frm.defective + "," + frm.rental + ",'" + frm.barcode + "')";
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }

        [HttpPost("updatehandhelds")]
        public int PostUpdateHandhelds([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "update devices set AhwalID = " + frm.ahwalid + ",devicenumber = '" + frm.devicenumber + "',model = '" + frm.model + "',devicetypeid='" + frm.devicetypeid + "',defective = " + frm.defective + ",rental = " + frm.rental + ",barcode = '" + frm.barcode + "' where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }


        [HttpPost("delhandheld")]
        public int PostDeletehandheld([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "delete from devices  where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();
            return ret;
        }




        [HttpPost("handheldlist")]
        public DataTable PostHandHeldList()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            //            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,t.name as type,d.Defective,d.Rental,d.BarCode,a.Name from Devices d INNER JOIN Ahwal a ON a.AhwalID = d.AhwalID inner join devicetypes t on t.devicetypeid = d.devicetypeid ", cont);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,(select dt.name from devicetypes dt where dt.devicetypeid = d.devicetypeid)  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            // NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,'1'  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }


        #endregion

        /*Accessory*/
        #region Accessory
        [HttpPost("addaccessories")]
        public int PostAddaccessories([FromBody]devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "insert into devices(AhwalID,devicenumber,model,devicetypeid,defective,rental,barcode) values (" + frm.ahwalid + ",'" + frm.devicenumber + "'," + frm.model + "," + frm.devicetypeid + "," + frm.defective + "," + frm.rental + ",'" + frm.barcode + "')";
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }

        [HttpPost("updateaccessories")]
        public int PostUpdateaccessories([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "update devices set AhwalID = " + frm.ahwalid + ",devicenumber = '" + frm.devicenumber + "',model = '" + frm.model + "',devicetypeid='" + frm.devicetypeid + "',defective = " + frm.defective + ",rental = " + frm.rental + ",barcode = '" + frm.barcode + "' where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }


        [HttpPost("delaccessorie")]
        public int PostDeleteaccessorie([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "delete from devices  where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();
            return ret;
        }




        [HttpPost("accessorielist")]
        public DataTable PostaccessorieList()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            //            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,t.name as type,d.Defective,d.Rental,d.BarCode,a.Name from Devices d INNER JOIN Ahwal a ON a.AhwalID = d.AhwalID inner join devicetypes t on t.devicetypeid = d.devicetypeid ", cont);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,(select dt.name from devicetypes dt where dt.devicetypeid = d.devicetypeid)  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            // NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,'1'  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }


        #endregion

        /*Persons*/
        #region Persons
        [HttpPost("addpersons")]
        public int PostAddpersons([FromBody]devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "insert into devices(AhwalID,devicenumber,model,devicetypeid,defective,rental,barcode) values (" + frm.ahwalid + ",'" + frm.devicenumber + "'," + frm.model + "," + frm.devicetypeid + "," + frm.defective + "," + frm.rental + ",'" + frm.barcode + "')";
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }

        [HttpPost("updatepersons")]
        public int PostUpdatepersons([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "update devices set AhwalID = " + frm.ahwalid + ",devicenumber = '" + frm.devicenumber + "',model = '" + frm.model + "',devicetypeid='" + frm.devicetypeid + "',defective = " + frm.defective + ",rental = " + frm.rental + ",barcode = '" + frm.barcode + "' where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();


            return ret;
        }


        [HttpPost("delperson")]
        public int PostDeleteperson([FromBody] devicecls frm)
        {
            int ret = 0;
            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cont;
            cmd.CommandText = "delete from devices  where deviceid=" + frm.deviceid;
            ret = cmd.ExecuteNonQuery();
            cont.Close();
            cont.Dispose();
            return ret;
        }




        [HttpPost("personlist")]
        public DataTable PostpersonList()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            //            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,t.name as type,d.Defective,d.Rental,d.BarCode,a.Name from Devices d INNER JOIN Ahwal a ON a.AhwalID = d.AhwalID inner join devicetypes t on t.devicetypeid = d.devicetypeid ", cont);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,(select dt.name from devicetypes dt where dt.devicetypeid = d.devicetypeid)  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            // NpgsqlDataAdapter da = new NpgsqlDataAdapter("select d.deviceid,d.DeviceNumber,d.Model,'1'  as type,d.Defective,d.Rental,d.BarCode,'jjjj' as Name from Devices d", cont);
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }


        #endregion

        #region Hand Held Invenory
        [HttpPost("handheldinventory")]
        public DataTable PostHandHeldInventoryList()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            string Qry = "SELECT        DeviceCheckInOutID, CheckInOutStates.Name AS StateName, Ahwal.AhwalID, Ahwal.Name AS AhwalName, Devices.deviceNumber, Devices.Model,(select dt.name from devicetypes dt where dt.devicetypeid = devices.devicetypeid) as Type, Persons.MilNumber, ";
            Qry = Qry + " Ranks.Name AS PersonRank, Persons.Name AS PersonName, DevicesCheckInOut.SavedTime, CheckInOutStates.CheckInOutStateID";

            Qry = Qry + "  FROM Ahwal INNER JOIN";

            Qry = Qry + " Devices  ON Ahwal.AhwalID = Devices.AhwalID INNER JOIN";

            Qry = Qry + " DevicesCheckInOut ON Devices.DeviceID = DevicesCheckInOut.DeviceID INNER JOIN";

            Qry = Qry + " CheckInOutStates ON DevicesCheckInOut.CheckInOutStateID = CheckInOutStates.CheckInOutStateID INNER JOIN";

            Qry = Qry + " Persons ON Ahwal.AhwalID = Persons.AhwalID AND DevicesCheckInOut.PersonID = Persons.PersonID INNER JOIN";

            Qry = Qry + " Ranks ON Persons.RankID = Ranks.RankID";
            Qry = Qry + "  ORDER BY DevicesCheckInOut.SavedTime";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Qry, cont);
            dt.Columns.Add("devicecheckinoutid");
            dt.Columns.Add("statename");
            dt.Columns.Add("ahwalid");
            dt.Columns.Add("ahwalname");
            dt.Columns.Add("devicenumber");
            dt.Columns.Add("model");

            dt.Columns.Add("type");
            dt.Columns.Add("milnumber");
            dt.Columns.Add("personrank");
            dt.Columns.Add("personname");
            dt.Columns.Add("savedtime");
            dt.Columns.Add("checkinoutstateid");
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }

        #endregion

        #region Accessory Inventory
        [HttpPost("accessoryinventory")]
        public DataTable PostAccessoryInventoryList()
        {


            NpgsqlConnection cont = new NpgsqlConnection();
            cont.ConnectionString = constr;
            cont.Open();
            DataTable dt = new DataTable();
            string Qry = "SELECT        DeviceCheckInOutID, CheckInOutStates.Name AS StateName, Ahwal.AhwalID, Ahwal.Name AS AhwalName, Devices.deviceNumber, Devices.Model,(select dt.name from devicetypes dt where dt.devicetypeid = devices.devicetypeid) as Type, Persons.MilNumber, ";
            Qry = Qry + " Ranks.Name AS PersonRank, Persons.Name AS PersonName, DevicesCheckInOut.SavedTime, CheckInOutStates.CheckInOutStateID";

            Qry = Qry + "  FROM Ahwal INNER JOIN";

            Qry = Qry + " Devices  ON Ahwal.AhwalID = Devices.AhwalID INNER JOIN";

            Qry = Qry + " DevicesCheckInOut ON Devices.DeviceID = DevicesCheckInOut.DeviceID INNER JOIN";

            Qry = Qry + " CheckInOutStates ON DevicesCheckInOut.CheckInOutStateID = CheckInOutStates.CheckInOutStateID INNER JOIN";

            Qry = Qry + " Persons ON Ahwal.AhwalID = Persons.AhwalID AND DevicesCheckInOut.PersonID = Persons.PersonID INNER JOIN";

            Qry = Qry + " Ranks ON Persons.RankID = Ranks.RankID";
            Qry = Qry + "  ORDER BY DevicesCheckInOut.SavedTime";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Qry, cont);
            dt.Columns.Add("devicecheckinoutid");
            dt.Columns.Add("statename");
            dt.Columns.Add("ahwalid");
            dt.Columns.Add("ahwalname");
            dt.Columns.Add("devicenumber");
            dt.Columns.Add("model");

            dt.Columns.Add("type");
            dt.Columns.Add("milnumber");
            dt.Columns.Add("personrank");
            dt.Columns.Add("personname");
            dt.Columns.Add("savedtime");
            dt.Columns.Add("checkinoutstateid");
            da.Fill(dt);
            cont.Close();
            cont.Dispose();


            return dt;
        }

        #endregion


    }
}
