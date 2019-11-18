using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dynamicAirlines : System.Web.UI.Page
{

    public static string Aircode = "Lufthansa";
    public static string Airline_sp ="" ;
    public string city = "";

    public static string cnstr_track_data = ConfigurationManager.AppSettings["sqlcn_track_data"].ToString();

    public static string ss_data = ConfigurationManager.ConnectionStrings["ss_data"].ToString();

    

    public string Dymanic_Metadata_Db = "";
    public string strContent = "";
    public string Airline_Description = "";
    public string Airline_header = "";
    public string Airline_information = "";
    public string airline_headerImage = "";
    public string strMetaTemp = "";
    public string AIrline_header_offer ="";



    protected void Page_Init(object sender, EventArgs e)
    {
        Search search = new Search();

        try
        {

            DataTable Metatable = new DataTable();

            Aircode = Convert.ToString(Request.QueryString["air"]).Replace("-", " ");

            if(Aircode =="AC")
            {
                Aircode = "IB";
            }


            if (Aircode != "")
            {

                SqlParameter[] param = { new SqlParameter("@name", Aircode) };
                // local connection sqlcn_t
                Metatable = DataLayer.GetData("dept_dest_meta_content_get_name", param, ConfigurationManager.AppSettings["sqlcn"].ToString());

                if (Metatable.Rows.Count > 0)
                {


                    strMetaTemp = Metatable.Rows[0]["metatag"].ToString();
                    string strContentTemp = Metatable.Rows[0]["description"].ToString();
                    // header airline image ex TK.jpg
                    airline_headerImage = Metatable.Rows[0]["airline"].ToString();
                    strMetaTemp = HttpUtility.HtmlDecode(strMetaTemp);
                    strContentTemp = HttpUtility.HtmlDecode(strContentTemp);


                    //Format_String objFormat = new Format_String();
                    //Dymanic_Metadata_Db = objFormat.FormatWith(strMetaTemp, search);
                    //strContent = objFormat.FormatWith(strContentTemp, search);
                    // char[] ch = new char[] {'/@'};

                    string[] str = strContentTemp.Split('@');
                    AIrline_header_offer = str[0];
                    string removePtag = str[1].Replace("</p>", "");
                    Airline_Description = removePtag.Replace("<p>", "");
                    Airline_header = str[2];
                    Airline_information = str[3];

                }
            }
        }
        catch (Exception ex) { }

    }


    
    protected void Page_Load(object sender, EventArgs e)

    {
        
        if (Aircode ==null)
        {
            Aircode = "Lufthansa";
        }
        
        string ipaddress;
        if (!IsPostBack)
        {
            
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipaddress == "" || ipaddress == null)
                ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();

            string[] strIPSplit = ipaddress.Split('.');

            string Country = "";
            //getting the Ip contry  code from the database based on the ipaddress and country as well as city
            Country = getIPCountryCode(ipaddress, out city);

            //sessions creation for Ipaddress, Country and City
            Session["ipaddress"] = ipaddress;
            Session["ipaddressCountry"] = Country;
            Session["ipcity"] = Country;
        }
    }

  
    [System.Web.Services.WebMethod]
    public static void Update_Device_ID(string fingerprint)
    {

        HttpContext.Current.Session["fingerprint"] = fingerprint;
       
    }


    [System.Web.Services.WebMethod]
    public static string GetScratch()
    {
        string jsonScratchpad = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtDeals = new DataTable();

        //get top 8 Airlines  from  tblAirline table from db
        
        dt = DataLayer.GetData("Get_top_Airlines_tblAirlinescodes", ConfigurationManager.ConnectionStrings["ss_data"].ToString());
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                //adding the dt datatable to the dataset ds by providing the name Airlines to the datatable
                DataTable dtCopy = dt.Copy();
                dtCopy.TableName = "Airlines";
                ds.Tables.Add(dtCopy);
            }
        }
        else
        {
            //Email1.SendEmail("scratchpad error", "scratchpad error");
        }
        
            SqlParameter[] parameters = { new SqlParameter("@Airline", Aircode) };
       
        // get popular 5 routes for particular airline
            dtDeals = DataLayer.GetData("Top_Airline_GET", parameters, ConfigurationManager.ConnectionStrings["ss_data"].ToString());

     

        if (dtDeals != null)
            {
                if (dtDeals.Rows.Count > 0)
                {
                    //adding the dtDeals datatable to the dataset ds by providing the name Deals to the datatable
                    DataTable dtCopy1 = dtDeals.Copy();
                    dtCopy1.TableName = "Deals";
                    ds.Tables.Add(dtCopy1);
                }
            }
            else
            {
                // Email1.SendEmail("dtDeals error", "dtDeals error");
            }
        

        //converting the dataset to json file 
        jsonScratchpad = DataTableToJSONWithJavaScriptSerializer(ds);
        return jsonScratchpad;
    }
    //converts the Datatable to the Json format
    public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = int.MaxValue;
        List<System.Collections.Generic.Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    //converts the dataset to the Json format
    public static string DataTableToJSONWithJavaScriptSerializer(DataSet dataset)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = int.MaxValue;
        Dictionary<string, object> ssvalue = new Dictionary<string, object>();
        foreach (DataTable table in dataset.Tables)
        {
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            string tablename = table.TableName;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            ssvalue.Add(tablename, parentRow);
        }
        return jsSerializer.Serialize(ssvalue);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    //method to get the IPLocation from the database based on client Ip address and returns the Country and city as out string
    protected string getIPCountryCode(string ipAddress, out string city)
    {
        try
        {
            string[] strIPSplit = ipAddress.Split('.');
            long ipNumber1 = Convert.ToInt64(strIPSplit[0]) * 256 * 256 * 256;
            long ipNumber2 = Convert.ToInt64(strIPSplit[1]) * 256 * 256;
            long ipNumber3 = Convert.ToInt64(strIPSplit[2]) * 256;
            long ipNumber4 = Convert.ToInt64(strIPSplit[3]);
            long ipNumber = ipNumber1 + ipNumber2 + ipNumber3 + ipNumber4;

            SqlParameter[] param = { new SqlParameter("@ipNumber", ipNumber) };
            DataTable dt = DataLayer.GetData("ip_master_select_by_ipnum", param, ConfigurationManager.AppSettings["sqlcn_ip"].ToString());


            if (dt.Rows.Count > 0)
            {

                city = dt.Rows[0]["city"].ToString();
                return dt.Rows[0]["country_code"].ToString();

            }
            else
            {
                city = "";
                return "country_code";
            }
        }
        catch
        {
            city = "";
            return "";
        }
    }
    public string GetCountriesDetails(string term)
    {
        string CS = ConfigurationManager.AppSettings["sqlcn"].ToString();

        List<Countries> countries = new List<Countries>();
        using (SqlConnection con = new SqlConnection(CS))
        {
            string cnstr = ConfigurationManager.AppSettings["sqlcn"].ToString();
            SqlParameter[] parameters = { new SqlParameter("@letters", term) };
            DataTable dt1 = DataLayer.GetData("Airport_selection", parameters, cnstr);
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    city = dt1.Rows[0]["City"].ToString() + " (" + dt1.Rows[0]["AirportCode"].ToString() + ") : " + dt1.Rows[0]["Country"].ToString() + "^" + dt1.Rows[0]["AirportCode"].ToString();
                }
            }
        }

        return city;
    }

    
}