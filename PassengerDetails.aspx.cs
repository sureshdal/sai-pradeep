using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Text;
using System.Threading;

public partial class PassengerDetails : System.Web.UI.Page
{
    // this is modified cabin and baggage added live pages  and patylater also

   

    public string sid = "";
    public string Airline_Code = "";
    public string ssid = "";
    public string product_Id = "";
    public string promocode = "";
    public string deptCode = "";
    public string destCode = "";
    public string searchType = "";
    public string Flexible = "";
    public string afid = "";
    public string arrivedate = "";
    public string ipcity = "";
    public string ipstate = "";
    public string ipCountry = "";
    public string cancel = "no";
    public string Promo = "no";
    public string PromoAm = "0";
    public string InsuAm = "0";
    public string closeuppopup = "false";
    DataTable dtSearchData = new DataTable();
    public static long track_id = 0;
    public static string cnstr_track_data = ConfigurationManager.AppSettings["sqlcn_track_data"].ToString();
    public double discount = 0;
    private static string strconn = ConfigurationManager.AppSettings["save_search_data"].ToString();
    private static string sqlcn = ConfigurationManager.AppSettings["sqlcn"].ToString();
    //private static string strconnhotel = ConfigurationManager.AppSettings["sqlcn_hotel_search_data"].ToString();

    public DataTable dtAAFares = new DataTable();
    public DataTable dtAAFlights = new DataTable();
    public DataTable dt_paylater = new DataTable();
    double cardCharges = 0;
    long wa_idno = 0;
    bool isMaskedAirline = false;

    DataRow[] drAAFares = null;
    DataRow[] drAAFlights = null;
    DataRow[] drAAFlightsOut = null;
    DataRow[] drAAFlightsIn = null;
    private string[] strIDSplit = null;
    int TotalAdults = 0;
    int TotalChilds = 0;
    int TotalInfants = 0;
    public int TotalPax = 0;
    public double TtAmount = 0;
    public double TtAmountCalc = 0;
    public string deptCountry = "";
    public string destCountry = "";
    public string username = "";
    public string _sessionid = "";

    public string paynowORlater = "";
    public string paylater_popup_restriction = "";

    //public string ReviewtripTerm = "";
    public string chk_paynow_paylater = "";

    public string timer_payletr = "";

    public string deptDate_Hj = "";



    Hashtable htAirLines = new Hashtable();


    string Country = "";
    string city = "";
    string ipaddress = "";


    public string isProfitMaxi_Country_or_City = "TRUE";

    public string[] non_ProfitMaxi_Countries = { "IN", "-", "CN", "KG" };
    public string[] non_ProfitMaxi_Cities = { "Hyderabad" };
    public string[] WhiteList_IPs_Global = { "183.82.104.7","183.82.3.90", "115.97.171.144" };

    public string[] WhiteList_Source_GLOBAL = { "WEGO", "WEGOWAP" };


    [System.Web.Services.WebMethod]
    public static void Update_Device_ID(string fingerprint, string track_id, string ssid)
    {
        if (track_id != "0")
        {
            HttpContext.Current.Session["fingerprint"] = fingerprint;


            SqlParameter[] param_track_fare = {new SqlParameter("@fingerprint",fingerprint),
                                                      new SqlParameter("@trID",Convert.ToInt64(track_id))};
            DataLayer.UpdateData("afid_track_data_update_fingerprint", param_track_fare, cnstr_track_data);

            SqlParameter[] parametersTm_Cart ={new SqlParameter("@id", HttpContext.Current.Session["TM_Cart_id" + ssid]),
                                      new SqlParameter("@Device_id", fingerprint)
                                      };
            DataLayer.UpdateData("Update_TM_Cart_Device_id", parametersTm_Cart, ConfigurationManager.AppSettings["save_search_data"].ToString());

        }

        //return fingerprint;
    }

    [System.Web.Services.WebMethod]
    public static void UpdateCancellation(string cancel, string am, string ssid)
    {
        try
        {
            if (cancel == "yes")
            {
                HttpContext.Current.Session["ExtendedCancellation" + ssid] = am;
                HttpContext.Current.Session["Extended" + ssid] = "yes";
               
            }
            else
            {
                HttpContext.Current.Session["ExtendedCancellation" + ssid] = null;
                HttpContext.Current.Session["Extended" + ssid] = "no";
               
            }
        }
        catch
        { 
        
        }
    }

    [System.Web.Services.WebMethod]
    public static void UpdateInsurance(string cancel, string am, string ssid)
    {
        if (cancel == "yes")
        {
            HttpContext.Current.Session["InsuranceAmount" + ssid] = am;
            HttpContext.Current.Session["Insurance" + ssid] = "yes";
        }
        else
        {
            HttpContext.Current.Session["InsuranceAmount" + ssid] = null;
            HttpContext.Current.Session["Insurance" + ssid] = "no";
        }
    }

    [System.Web.Services.WebMethod]
    public static void UpdatePromoCode(string id, string am, string status, string ssid)
    {
        if (status == "yes")
        {
            HttpContext.Current.Session["VoucherAmount" + ssid] = am;
            HttpContext.Current.Session["VoucherCode" + ssid] = id;
            HttpContext.Current.Session["Voucher" + ssid] = "yes";
        }
        else
        {
            HttpContext.Current.Session["VoucherAmount" + ssid] = null;
            HttpContext.Current.Session["VoucherCode" + ssid] = null;
            HttpContext.Current.Session["Voucher" + ssid] = "no";
        }
        if (HttpContext.Current.Session["CancelID" + ssid] != null)
        {
            if (HttpContext.Current.Session["closepop" + ssid].ToString() == "yes")
            {
                SqlParameter[] param_track_fare = {
                                                    new SqlParameter("@CancelID", HttpContext.Current.Session["CancelID" + ssid].ToString()),
                                                    new SqlParameter("@Opted", "no"),
                                                    new SqlParameter("@Changed", "yes"),
                                                    new SqlParameter("@Amount", am),
                                                    new SqlParameter("@Device_id", HttpContext.Current.Session["fingerprint"]),
                                                    new SqlParameter("@Status", "no")
                                                };
                DataLayer.UpdateData("Update_PopCloseup", param_track_fare, sqlcn);
            }
            HttpContext.Current.Session["closepop" + ssid] = "no";
            //update Statistic in database for this 
            //
        }
    }

    [System.Web.Services.WebMethod]
    public static void UpdatePromoCodeClosePop(string id, string am, string status, string ssid, string sid)
    {
        if (status == "yes")
        {
            HttpContext.Current.Session["VoucherAmount" + ssid] = am;
            HttpContext.Current.Session["VoucherCode" + ssid] = id;
            HttpContext.Current.Session["Voucher" + ssid] = "yes";
            HttpContext.Current.Session["closepop" + ssid] = "yes";

            if (HttpContext.Current.Session["CancelID" + ssid] == null)
            {
                SqlParameter[] param_track_fare = {
                                                    new SqlParameter("@Opted", "yes"),
                                                    new SqlParameter("@Changed", "no"),
                                                    new SqlParameter("@Amount", am),
                                                    new SqlParameter("@Device_id", HttpContext.Current.Session["fingerprint"]),
                                                    new SqlParameter("@Status", "yes"),
                                                    new SqlParameter("@Search_id", sid),
                                                    new SqlParameter("@ssid", ssid)
                                                };
                long i = DataLayer.insertData("Insert_PopCloseup", param_track_fare, sqlcn);
                HttpContext.Current.Session["CancelID" + ssid] = i;
                //update Statistic in database for this 
                //clicked on Apply
            }
            else
            {
                if (HttpContext.Current.Session["closepop" + ssid].ToString() == "yes")
                {
                    SqlParameter[] param_track_fare = {
                                                    new SqlParameter("@CancelID", HttpContext.Current.Session["CancelID" + ssid].ToString()),
                                                    new SqlParameter("@Opted", "yes"),
                                                    new SqlParameter("@Changed", "no"),
                                                    new SqlParameter("@Amount", am),
                                                    new SqlParameter("@Device_id", HttpContext.Current.Session["fingerprint"]),
                                                    new SqlParameter("@Status", "yes")
                                                };
                    DataLayer.UpdateData("Update_PopCloseup", param_track_fare, sqlcn);
                }
            }
        }
        else
        {
            HttpContext.Current.Session["VoucherAmount" + ssid] = null;
            HttpContext.Current.Session["VoucherCode" + ssid] = null;
            HttpContext.Current.Session["Voucher" + ssid] = "no";
            HttpContext.Current.Session["closepop" + ssid] = "no";
            if (HttpContext.Current.Session["CancelID" + ssid] == null)
            {
                SqlParameter[] param_track_fare = {
                                                    new SqlParameter("@Opted", "no"),
                                                    new SqlParameter("@Changed", "no"),
                                                    new SqlParameter("@Amount", am),
                                                    new SqlParameter("@Device_id", HttpContext.Current.Session["fingerprint"]),
                                                    new SqlParameter("@Status", "no"),
                                                    new SqlParameter("@Search_id", sid),
                                                    new SqlParameter("@ssid", ssid)
                                                };
                long i = DataLayer.UpdateData("Insert_PopCloseup", param_track_fare, sqlcn);
                HttpContext.Current.Session["CancelID" + ssid] = i;

                //update Statistic in database for this 
                //clicked on No thanks!
            }
            else
            {
                if (HttpContext.Current.Session["closepop" + ssid].ToString() == "no")
                {
                    SqlParameter[] param_track_fare = {
                                                    new SqlParameter("@CancelID", HttpContext.Current.Session["CancelID" + ssid].ToString()),
                                                    new SqlParameter("@Opted", "no"),
                                                    new SqlParameter("@Changed", "no"),
                                                    new SqlParameter("@Amount", am),
                                                    new SqlParameter("@Device_id", HttpContext.Current.Session["fingerprint"]),
                                                    new SqlParameter("@Status", "no")
                                                };
                    DataLayer.UpdateData("Update_PopCloseup", param_track_fare, sqlcn);
                }
            }
        }
    }

    //InfantCheck ChildCheck AdultCheck
    [System.Web.Services.WebMethod]
    public static string InfantCheck(string arivedate, string dob)
    {
        try
        {
            DateTime newDate = new DateTime(Convert.ToInt32(arivedate.Split('/')[2]), Convert.ToInt32(arivedate.Split('/')[0]), Convert.ToInt32(arivedate.Split('/')[1]));
            DateTime birthdate = new DateTime(Convert.ToInt32(dob.Split('/')[0]), Convert.ToInt32(dob.Split('/')[1]), Convert.ToInt32(dob.Split('/')[2]));
            TimeSpan ts = (newDate - birthdate);

            int years = newDate.Year - birthdate.Year;
            int months = newDate.Month - birthdate.Month;
            int days = newDate.Day - birthdate.Day;

            if (months > 0)
            {
                years++;
            }
            else if (months == 0)
            {
                if (days >= 0)
                {
                    years++;
                }

            }

            if (years > 0 && years <= 2)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        catch
        {
            return "false";
        }
    }

    [System.Web.Services.WebMethod]
    public static string ChildCheck(string arivedate, string dob)
    {
        DateTime newDate = new DateTime(Convert.ToInt32(arivedate.Split('/')[2]), Convert.ToInt32(arivedate.Split('/')[0]), Convert.ToInt32(arivedate.Split('/')[1]));
        DateTime birthdate = new DateTime(Convert.ToInt32(dob.Split('/')[0]), Convert.ToInt32(dob.Split('/')[1]), Convert.ToInt32(dob.Split('/')[2]));


        int years = newDate.Year - birthdate.Year;
        int months = newDate.Month - birthdate.Month;
        int days = newDate.Day - birthdate.Day;

        if (months > 0)
        {
            years++;
        }
        else if (months == 0)
        {
            if (days >= 0)
            {
                years++;
            }

        }

        if (years > 2 && years < 12)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }
    [System.Web.Services.WebMethod]
    public static string AdultCheck(string arivedate, string dob)
    {
        DateTime newDate = new DateTime(Convert.ToInt32(arivedate.Split('/')[2]), Convert.ToInt32(arivedate.Split('/')[0]), Convert.ToInt32(arivedate.Split('/')[1]));
        DateTime birthdate = new DateTime(Convert.ToInt32(dob.Split('/')[0]), Convert.ToInt32(dob.Split('/')[1]), Convert.ToInt32(dob.Split('/')[2]));

        int years = newDate.Year - birthdate.Year;
        int months = newDate.Month - birthdate.Month;
        int days = newDate.Day - birthdate.Day;

        if (months > 0)
        {
            years++;
        }
        else if (months == 0)
        {
            if (days >= 0)
            {
                years++;
            }

        }


        if (years <= 11)
        {
            return "false";
        }
        else
        {
            return "true";
        }
    }





    public string Dymanic_Metadata_Db = "";
    public string strContent = "";
    public string ChatMessage = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        ChatMessage = "We have got some exciting offers for you! Chat with us to find unpublished fares.Hurry Up!! Limited seats available.!!";
        Search search = new Search();
        if (Request.QueryString["sid"] != null)
        {
            sid = Request.QueryString["sid"].ToString();
        }
        try
        {


            DataTable Metatable = new DataTable();

            string pageurl = Request.Url.Segments[Request.Url.Segments.Length - 1].ToLower().Replace(".aspx", "").Trim();

            if (pageurl != "")
            {

                SqlParameter[] param = { new SqlParameter("@name", pageurl) };

                Metatable = DataLayer.GetData("dept_dest_meta_content_get_name", param, ConfigurationManager.AppSettings["sqlcn"].ToString());

                if (Metatable.Rows.Count > 0)
                {
                    search.dept = getAirportCity(Session["Departure" + sid].ToString());
                    search.dest = getAirportCity(Session["Destination" + sid].ToString());

                    string strMetaTemp = Metatable.Rows[0]["metatag"].ToString();
                    string strContentTemp = Metatable.Rows[0]["description"].ToString();
                    strMetaTemp = HttpUtility.HtmlDecode(strMetaTemp);
                    strContentTemp = HttpUtility.HtmlDecode(strContentTemp);


                    Format_String objFormat = new Format_String();
                    Dymanic_Metadata_Db = objFormat.FormatWith(strMetaTemp, search);
                    strContent = objFormat.FormatWith(strContentTemp, search);

                }
            }
        }
        catch (Exception ex) { }

    }


    [System.Web.Services.WebMethod]
    public static string GetMessage(string sid, string amount, string restrict_flag)
    {
        string msg = "We have unpublished lower fare itineraries available for you. To avail this offer please call us on +1-844-666-3779 immediately!!";

        if (restrict_flag.ToLower() == "false")
        {
            msg = "NA";
        }
        else
        {
            try
            {
                double TtFare_Amount_actual = Convert.ToDouble(amount);
                double TtFare_Amount_alt = TtFare_Amount_actual;
                double Difference = 0;

                SqlParameter[] parametersSearch1 = { new SqlParameter("@sid", Convert.ToInt64(sid)) };
                DataTable dtSearchData = DataLayer.GetData("searchdata_master_select_sid", parametersSearch1, ConfigurationManager.AppSettings["save_search_data"].ToString());
                string route_desc = "";

                if (dtSearchData != null)
                {
                    if (dtSearchData.Rows.Count > 0)
                    {
                        route_desc = "  " + getAirportCity(dtSearchData.Rows[0]["dept"].ToString()) + "(" + dtSearchData.Rows[0]["dept"].ToString() + ")-To-" + getAirportCity(dtSearchData.Rows[0]["dest"].ToString()) + "(" + dtSearchData.Rows[0]["dest"].ToString() + ")";
                    }

                }


                msg = "We have unpublished lower fare itineraries" + route_desc + " available for you. To avail this offer please call us on +1-844-666-3779 immediately! Quote Reference : " + sid;


                SqlParameter[] paramAlt = { new SqlParameter("@sid", Convert.ToInt64(sid)) };
                DataTable dtAltFare = DataLayer.GetData("get_alt_fare", paramAlt, ConfigurationManager.AppSettings["save_search_data"].ToString());
                if (dtAltFare != null)
                {
                    if (dtAltFare.Rows.Count > 0)
                    {
                        if (dtAltFare.Rows[0]["lowest_fare"].ToString() != "")
                        {
                            TtFare_Amount_alt = Convert.ToDouble(dtAltFare.Rows[0]["lowest_fare"].ToString());
                            if (TtFare_Amount_actual > TtFare_Amount_alt)
                                Difference = TtFare_Amount_actual - TtFare_Amount_alt;
                        }

                    }

                }

                if (Difference > 0)
                {
                    if (Difference > 100)
                    {
                        TtFare_Amount_actual = TtFare_Amount_actual - 50;
                        msg = "We have unpublished lower fare itineraries" + route_desc + " available for you. To avail this offer please call us on +1-844-666-3779 immediately! fare starts from USD" + String.Format("{0:0.00}", TtFare_Amount_actual) + ".  Quote Reference : " + sid;
                    }
                }
            }
            catch { }
        }

        return msg;
    }




    


  


    protected void Page_Load(object sender, EventArgs e)
    {
        





        //DateTime dt_est_paylater_start= 

        
        //paylater_popup_restriction = "true";


        string paymentType = paymentMode.Value.ToString();

        //Session["PaymentMode"] = paymentType;


        //if (!Request.IsSecureConnection)
        //{
        //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
        //    Response.Redirect(redirectUrl);
        //}

        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ipaddress == "" || ipaddress == null)
     

            ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();

        //ipaddress = "50.197.162.169"; // san franscio
        //ipaddress = "134.201.250.155"; // las angles  
        ipaddress = "52.168.106.81"; // Washington  
        
        //ipaddress = "139.47.28.0"; //Barcelona  52.168.106.81


        string Country_Paylater = "";
        Country_Paylater = getIPCountryCode(ipaddress , out city);
        if(Country_Paylater =="US")
        {
            Country = getIPCountryCode(ipaddress, out city);
            // GetCountriesDetails(city);
            string Paylater_City = city;

            //string Paylater_City = GetCountriesDetails(city);

            //SqlParameter[] block_city_paylater =
            //{
            //    new SqlParameter("@paylater_city" , Paylater_City)
            //};
            //DataTable paylater_block_ipAddress = DataLayer.GetData("ip_master_select_by_ipnum", block_city_paylater, ConfigurationManager.AppSettings["sqlcn"].ToString());



            if (Paylater_City =="New York All" || Paylater_City == "Los Angeles" || Paylater_City =="San Francisco")
                // restrict paylater option in these city's
            {
                 paynowORlater = " ";

               // ReviewtripTerm = "";
            }

            else
            {

                // display paylater option 
                paynowORlater = "<ul id= 'pay-menu' role= 'tablist'>" +
                              "<li><a data-page = 'paynow' class='active'>Pay Now</a></li>" +
                                "<li><a data-page= 'paylater' > Pay Later</a></li>" +
                                        "</ul>";

            }
        }





        // reviewtrip for paylater 
        ReviewtripTerm.InnerHtml = "<h5><strong>Review the trip terms:</strong></h5><ul><li>1. Name changes are not permitted. Tickets are non-refundable and non-transferable.</li><li>2. Where permitted, changes to your itinerary costs you minimum of USD150 per ticket. Airlines fare/fees are additional cost for you.</li> <li>3. Your credit/debit card may be billed in multiple charges totaling the final total price. Billing statement may disply Airlines name or Travelmerry or Agent Fee or our suplier name.</li></ul> ";


        

        Country = getIPCountryCode(ipaddress, out city);
        GetCountriesDetails(city);


        if (!WhiteList_IPs_Global.Contains(ipaddress))
        {
            if (non_ProfitMaxi_Countries.Contains(Country) || non_ProfitMaxi_Cities.Contains(city))
            {
                isProfitMaxi_Country_or_City = "FALSE";
            }
        }


        if (WhiteList_Source_GLOBAL.Contains(Session["afid" + sid].ToString().ToUpper()))
        {
            isProfitMaxi_Country_or_City = "TRUE";
        }



        if (Session["paxemail_tmcom_mybookings"] != null)
        {
            username = Session["paxemail_tmcom_mybookings"].ToString();
        }


        if (Session["_sessionid"] == null)
        {
            _sessionid = RandomPassword.GenerateStrong(50);

            Session["_sessionid"] = _sessionid;
        }
        else
        {
            _sessionid = Session["_sessionid"].ToString();
        }



        string errmsg = "";
        bool isvalidRequest = true;
        if (Request.QueryString["sid"] != null)
        {
            sid = Request.QueryString["sid"].ToString();
        }
        else
        {
            errmsg += "No sid-";
            isvalidRequest = false;
        }
        if (Session["trackid" + sid] != null)
        {
            track_id = Convert.ToInt64(Session["trackid" + sid].ToString());
        }



        if (Request.QueryString["ssid"] != null)
        {
            ssid = Request.QueryString["ssid"].ToString();
        }
        else
        {
            errmsg += "No ssid-";
            isvalidRequest = false;
        }
        if (Session["SearchResult" + sid] == null)
        {
            isvalidRequest = false;
            errmsg += "No Search Results-";
            //Response.Redirect("sessionexpired.aspx?sid=" + sid + "&errMsg=No Search Results");
            //Response.End();
        }
        if (isvalidRequest)
        {
            searchType = Session["SearchType" + sid].ToString();

            Session["RedirectURL"] = "PassengerDetails.aspx?sid=" + sid + "&ssid=" + ssid;

            //Session["SearchType" + sid] = searchType;

            //try
            //{
            //    Email1.SendEmail("Details", searchType);
            //}
            //catch
            //{

            //}


            if (searchType == "FO")
            {
                if (Session["fid" + ssid] == null)
                {
                    isvalidRequest = false;
                    errmsg += "No selected f-";
                }
            }
        }




        if (isvalidRequest)
        {

            if (Request.QueryString["ErrDepDate"] != null)
            {
                if (Request.QueryString["ErrDepDate"].ToString() == "yes")
                {
                    //diverrmsg.Attributes.Add("style", "Display:''");
                    //diverrmsg.InnerHtml = "<h4 class='text-center mrgtop'><i class='fa fa-exclamation-triangle'></i> Apologies, the departure date is within 3 days, please contact our reservation department on 0800 910 1619 or 0203 376 9542 to speedup the ticket delivery or Alternatively One of our sales agent will call you back in few minutes</h4>";

                }
            }

            if (Request.QueryString["ErrMsg"] != null)
            {

                diverrmsg.Attributes.Add("style", "Display:block");
                diverrmsg.InnerHtml = "<div class='pm-content'>" +
                                            "<div class='pm-left pm-error warning'>" +
                                                "<p>" + Request.QueryString["ErrMsg"].ToString() + "</p>" +
                                            "</div>" +
                                        "</div>";

            }

            IFPayment.Attributes["src"] = "BookandPriceRedirect.aspx?ssid=" + ssid + "&sid=" + sid;





           

            if ( IsPostBack)
            {
                DisplayData();
                btnBookNow();
            }
            else
            {

                product_Id = sid + "_" + ssid;

                //Insert_TM_Cart
                DisplayData();
                if (Session["TM_Cart_id" + ssid] == null)
                {
                    string strConnectionString = "";
                    strConnectionString = ConfigurationManager.AppSettings["save_search_data"].ToString();
                    SqlConnection cnn = new SqlConnection();
                    cnn.ConnectionString = strConnectionString;
                    cnn.Open();
                    SqlTransaction trans;
                    trans = cnn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = trans;
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Product_id", sid + "_" + ssid);
                        cmd.Parameters.AddWithValue("@conf", conf.Value);
                        cmd.Parameters.AddWithValue("@in_id", inid.Value);
                        cmd.Parameters.AddWithValue("@out_id", outid.Value);
                        cmd.Parameters.AddWithValue("@price", TotalCost/*Totaladultfare*/);
                        cmd.Parameters.AddWithValue("@sale_price", TotalCost/*Totaladultfare*/);
                        cmd.Parameters.AddWithValue("@action", "Added_To_Cart");
                        cmd.Parameters.AddWithValue("@Device_Id", "");
                        if (afid1.Value == "Travelmerry")
                        {
                            string redirecturl = "https://www.Travelmerry.com/aff_link.aspx?conf=" + conf.Value + "&in=" + inid.Value + "&out=" + outid.Value;
                            cmd.Parameters.AddWithValue("@RedirectUrl", redirecturl);
                        }
                        else
                        {
                            string redirecturl = "https://www.travelmerry.com/inbound.aspx?conf=" + conf.Value + "&in=" + inid.Value + "&out=" + outid.Value + "&pid=" + sid + "_" + ssid;
                            cmd.Parameters.AddWithValue("@RedirectUrl", redirecturl);
                        }


                        cmd.Parameters.AddWithValue("@Airline", dtAAFares.Rows[0]["AirCode"].ToString());
                        cmd.Parameters.AddWithValue("@Afid", dtSearchData.Rows[0]["afid"]);
                        cmd.Parameters.AddWithValue("@Aid", dtSearchData.Rows[0]["aid"]);
                        cmd.Parameters.AddWithValue("@IP_Address", ipaddress);
                        cmd.Parameters.AddWithValue("@Dept", dtSearchData.Rows[0]["dept"].ToString());
                        cmd.Parameters.AddWithValue("@Dest", dtSearchData.Rows[0]["dest"].ToString());
                        cmd.Parameters.AddWithValue("@DeptDate", dtSearchData.Rows[0]["deptDate"].ToString());
                        cmd.Parameters.AddWithValue("@RetDate", dtSearchData.Rows[0]["retDate"].ToString());
                        cmd.Parameters.AddWithValue("@Jtype", dtSearchData.Rows[0]["jType"].ToString());
                        cmd.Parameters.AddWithValue("@Adult", dtSearchData.Rows[0]["Adults"].ToString());
                        cmd.Parameters.AddWithValue("@Child", dtSearchData.Rows[0]["Childs"].ToString());
                        cmd.Parameters.AddWithValue("@Infant", dtSearchData.Rows[0]["Infants"].ToString());
                        cmd.Parameters.AddWithValue("@CabinClass", dtSearchData.Rows[0]["cabinClass"].ToString());
                        string path = Server.MapPath("~/Deals_Img");
                        string path1 = "";
                        path1 = "\\Airports";
                        DirectoryInfo di = new DirectoryInfo(path1);
                        bool flag = false;
                        string airportCity = "";
                        string airline = "";
                        string airportCountry = "";
                        string imgurl = "";
                        string cn = "";
                        airportCity = getAirport(dtSearchData.Rows[0]["Dest"].ToString(), out cn);

                        airportCountry = getCountryCode1(airportCity, cn) + "_Deals.jpg";
                        airportCity = dtSearchData.Rows[0]["Dest"].ToString() + "_Deals.jpg";
                        airline = dtAAFares.Rows[0]["AirCode"].ToString() + "_Deals.jpg";
                        Airline_Code = dtAAFares.Rows[0]["AirCode"].ToString();
                        
                        if (File.Exists(path + path1 + "\\" + airportCity))
                        {
                            flag = true;
                            imgurl = "http://www.Travelmerry.com/Deals_Img/Airports/" + airportCity + "";
                        }
                        else
                        {
                            airportCountry = getCountryCode2(cn) + "_Deals.jpg";
                            if (File.Exists(path + "\\Countries" + "\\" + airportCountry))
                            {
                                flag = true;
                                imgurl = "http://www.Travelmerry.com/Deals_Img/Countries/" + airportCountry + "";
                            }
                        }
                        path1 = "\\Countries";
                        if (!flag)
                        {
                            if (File.Exists(path + path1 + "\\" + airportCountry))
                            {
                                flag = true;
                                imgurl = "http://www.Travelmerry.com/Deals_Img/Countries/" + airportCountry + "";
                            }
                        }
                        path1 = "\\Airlines";
                        if (!flag)
                        {
                            if (File.Exists(path + path1 + "\\" + airline))
                            {
                                flag = true;
                                imgurl = "http://www.Travelmerry.com/Deals_Img/Airlines/" + airline + "";
                            }
                        }
                        if (!flag)
                        {
                            imgurl = "http://www.Travelmerry.com/Deals_Img/TravelMerry_Deals.jpg";
                        }
                        cmd.Parameters.AddWithValue("@ImageUrl", imgurl);
                        cmd.CommandText = "Insert_TM_Cart_2";
                        long TM_Cart_id = Convert.ToInt64(cmd.ExecuteScalar());
                        Session["TM_Cart_id" + ssid] = TM_Cart_id;
                        Session["TotalCost" + ssid] = TotalCost;
                        trans.Commit();
                        cnn.Close();
                    }
                    catch (Exception exDB)
                    {
                        trans.Rollback();
                        cnn.Close();
                        Email1.SendEmail("Flight Booking TM_Cart error", exDB.Message.ToString() + "-" + exDB.StackTrace);
                    }
                }
                

                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                //ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "SessionExpireAlert(" + timeout + ");", true);
                if (ipaddress == "209.29.183.172")
                {
                    isProfitMaxi_Country_or_City = "FALSE";

                }

                if (isProfitMaxi_Country_or_City != "TRUE")
                {


                    string ErrorMessage = "The fare you are looking is no more available. Please search again to get the live fare or call us on " + ContactDetails.Phone_support + ".";
                    //diverrmsg.Attributes.Add("style", "Display:''");
                    //diverrmsg.InnerHtml = "<h4 class='text-center mrgtop'><i class='fa fa-exclamation-triangle'></i> " + ErrorMessage + "</h4>";

                    Response.Redirect("index.aspx?error=Expired");
                    Response.End();

                }

            }


        }
        else
        {
            //conf.Value = dtAAFares.Rows[0]["ciid"].ToString();
            //inid.Value = dtAAFares.Rows[0]["inid"].ToString();
            //outid.Value = dtAAFares.Rows[0]["outid"].ToString();
            if (conf.Value != "" && inid.Value != "" && outid.Value != "")
            {
                //onclick = 'searchagain12(\"" + conf + "\",\"" + inid + "\",\"" + outid + "\")'
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "searchagain12(\"" + conf + "\",\"" + inid + "\",\"" + outid + "\");", true);
            }
            else
            {
                Response.Redirect("sessionexpired.aspx?errmsg=" + errmsg.Substring(0, errmsg.Length - 1));
            }
        }



           bool falg_pay = paylater_limitation();

        if (falg_pay)
        {


            var timeUtc = DateTime.UtcNow;
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            string est_now_time = easternTime.ToString("HH");



            //  if( nowt >=start && nowt <= end)


            if (Convert.ToInt32(est_now_time) >= Convert.ToInt32(dt_paylater.Rows[0]["on_time"].ToString()) &&
                (Convert.ToInt32(est_now_time) <= Convert.ToInt32(dt_paylater.Rows[0]["off_time"].ToString())))
            {


                timer_payletr = dt_paylater.Rows[0]["timer"].ToString();



                paylater_timer_popup.InnerHtml = "<div class='modal fade' id='paylater_popup_timer' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>" +
         "<div class='modal-dialog'>" +
           "<div class='modal-content'>" +
         "<div class='modal-body'>" +
             "<div class='sf-wrap'> " +
            "<div class='sf-content'>" +
                "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>" +

                "<div class='sfTitle'>" +
                    "<h1>$50 Discount </h1>" +
                    "<p>On Selected Flights to " + getAirportCity(Session["Destination" + sid].ToString()) +"</p>" +
                "</div>" +
                "<div class='sfCallnow'>" +
                    "<p>Call Now - Offers available only <br>" +
                    " for Telephone Bookings.</p>" +

                    "<h3> +1-844-666-3779 " +
                    "<span>(Toll free:)</span>" +
                    "</h3>" +

                    "<h5>Quote Referece: " + dtAAFares.Rows[0]["search_fares_id"].ToString() + " </h5>" +
                "</div>" +
                "<div class='sfFooter'>" +
                    "<div class='sf-left'>*Prices are quoted in USD Dollar</div>" +
                    "<div class='sf-right'>*Discount on Fee only.</div>" +
                "</div>" +
            "</div>" +
        "</div>" +
        "</div>" +

           "</div>" +
         "</div>" +
        "</div>";


            }


        }


        string departure1 = dtAAFlights.Rows[0]["DEP_ARP"].ToString();
        
        DataTable dtAirCode_city = getcityname( departure1);

      //  if dtAirCode_city contains columns that is csa restricted states 

        if (dtAirCode_city.Rows.Count > 0)
        {
            //return dtAirCode_city;

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('CSa restricted States dept');", true);




        }

        else
        {
            string destination1 = dtAAFlights.Rows[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString();

            DataTable dtAirCode_city1 = getcityname(destination1);

            if(dtAirCode_city1.Rows.Count >0)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('CSa restricted States dest');", true);



            }




        }


        //DateTime deptDate1 = DateTime.ParseExact(drAAFlightsOut[0]["DEP_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);


       // DateTime DepDate = DateTime.ParseExact(dtSearchData.Rows[0]["deptDate"].ToString(), "ddMMyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        //deptDate_Hj = Convert.ToString(DepDate);




    }
    protected void downloadFile(string strURL, string strFileLocation)
    {
        //bool downloadSt = false;
        try
        {

            WebRequest request = WebRequest.Create(new Uri(strURL));
            request.Method = "HEAD";
            WebResponse response = request.GetResponse();
            //Stream response = request.GetRequestStream();

            if (response.ContentLength > 0)
            {
                WebClient oclient = new WebClient();
                //oclient.DownloadFile(strURL, strFileLocation);
                oclient.DownloadFile(new Uri(strURL), strFileLocation);
                //downloadSt = true;

            }

            long length = response.ContentLength;



        }
        catch (Exception ex)
        {
            //lblStatus.Text = ex.Message;
        }
        //return downloadSt;
    }
    public response ResXml(string res)
    {
        response myObject = new response();

        StringReader read = new StringReader(res);

        XmlSerializer serializer = new XmlSerializer(myObject.GetType());

        XmlReader reader = new XmlTextReader(read);

        try
        {

            myObject = (response)serializer.Deserialize(reader);


           
        }

        catch (Exception ex)
        {
            //logerrors(ex.Message);
        }
        finally
        {
            reader.Close();
            read.Close();
            read.Dispose();
        }
        return myObject;
    }
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





    private DataRow getMaskAirline(string Airline, string depDate, string arrDate, string FareType, string JType, string Departure, string Destination, DataTable dtMask)
    {
        DataRow dr = null;
        try
        {

            DateTime DepDate = Convert.ToDateTime(depDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            DateTime ArrDate = Convert.ToDateTime(arrDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));


            //Exception Airline for predicted dates

            string[] WhiteListAirlines = { };
            string[] WhiteListAirlines_JCB = { };


            SqlParameter[] paramPredicted = { new SqlParameter("@dept",Departure),
                                           new SqlParameter("@dest", Destination),
                    new SqlParameter("@ddate", DepDate.ToString("yyyy-MM-dd")),
                        new SqlParameter("@rdate", ArrDate.ToString("yyyy-MM-dd")),
                        new SqlParameter("@sday",DateTime.Now.ToString("yyyy-MM-dd")) };
               DataTable dtPredicted = DataLayer.GetData("sample_data_airlines_select", paramPredicted, ConfigurationManager.ConnectionStrings["prediction"].ToString());



            if (dtPredicted != null)
            {
                if (dtPredicted.Rows.Count > 0)
                {
                    SqlParameter[] paramWhiteList = { new SqlParameter("@gds", "1S"), new SqlParameter("@country", dtPredicted.Rows[0]["Country"].ToString()) };
                    DataTable dtWhitelist = DataLayer.GetData("whitelist_airlines_pub_select", paramWhiteList, ConfigurationManager.ConnectionStrings["ss_data"].ToString());


       WhiteListAirlines = dtWhitelist.AsEnumerable()
     .Select(row => row.Field<string>("airline"))
     .ToArray();


                    SqlParameter[] paramWhiteList_JCB = { new SqlParameter("@gds", "1S"), new SqlParameter("@country", dtPredicted.Rows[0]["Country"].ToString()) };
                    DataTable dtWhitelist_JCB = DataLayer.GetData("whitelist_airlines_jcb_select", paramWhiteList_JCB, ConfigurationManager.ConnectionStrings["ss_data"].ToString());


                    WhiteListAirlines_JCB = dtWhitelist_JCB.AsEnumerable()
     .Select(row => row.Field<string>("airline"))
     .ToArray();


                }
            }




            if ((!WhiteListAirlines.Contains(Airline) && (FareType == "ADVJP" || FareType == "PUB")) || (!WhiteListAirlines_JCB.Contains(Airline) && (FareType == "ADVJN" || FareType == "SR")))
            {

                //DataRow[] drMask = dtMask.Select("AirCode='" + Airline + "'");
                if (JType == "1")
                {
                    DataRow[] drMask = dtMask.Select("AirCode='" + Airline + "' and FareType='" + FareType + "' and Dept_Date_From<='" + DepDate.ToString("yyyy-MM-dd") + "' and Dept_Date_To>='" + DepDate.ToString("yyyy-MM-dd") + "'" + " and (Departure='" + Departure + "' or Departure='')" + " and (Destination='" + Destination + "' or Destination='')");

                    if (drMask != null)
                    {
                        if (drMask.Length > 0)
                        {
                            dr = drMask[0];
                        }
                    }
                }
                else
                {

                    string query = "AirCode='" + Airline + "' and FareType='" + FareType + "' and Dept_Date_From<='" + DepDate.ToString("yyyy-MM-dd") + "' and Dept_Date_To>='" + DepDate.ToString("yyyy-MM-dd") + "' and Ret_Date_From<='" + ArrDate.ToString("yyyy-MM-dd") + "' and Ret_Date_To>='" + ArrDate.ToString("yyyy-MM-dd") + "'" + " and (Departure='" + Departure + "' or Departure='')" + " and (Destination='" + Destination + "' or Destination='')";

                    DataRow[] drMask = dtMask.Select(query);
                    if (drMask != null)
                    {
                        if (drMask.Length > 0)
                        {
                            dr = drMask[0];
                        }
                    }
                }
            }
            else
            {
                if (!WhiteList_IPs_Global.Contains(ipaddress))
                {
                    if (non_ProfitMaxi_Countries.Contains(Country) || non_ProfitMaxi_Cities.Contains(city))
                    {
                        isProfitMaxi_Country_or_City = "FALSE";
                    }
                }


                if (WhiteList_Source_GLOBAL.Contains(Session["afid" + sid].ToString().ToUpper()))
                {
                    isProfitMaxi_Country_or_City = "TRUE";
                }

            }
        }
        catch
        {
            dr = null;
        }
        return dr;
    }

    public static void FindAlt(object uri)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.ToString());
            // request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string s = reader.ReadToEnd();
                //  Email1.SendEmail("Results", s);
            }
        }
        catch (Exception ex)
        {
            // Email1.SendEmail("Error",ex.StackTrace +ex.Message);
        }
    }

    private void btnBookNow()
    {
        //TtAmount = Convert.ToDouble(totalAm.Value);
        bool flag = true;

        // paylater added 

        string paymentType = paymentMode.Value.ToString();

        Session["PaymentMode" + ssid] = paymentType;

        

        for (int paxCnt = 1; paxCnt <= TotalPax; paxCnt++)
        {


            Session["PTitle" + paxCnt + sid] = Request.Form["ptitle" + paxCnt].ToString();
            Session["PFName" + paxCnt + sid] = Request.Form["fname" + paxCnt].ToString().Trim() + " " + Request.Form["mname" + paxCnt].ToString().Trim();
            Session["FName" + paxCnt + sid] = Request.Form["fname" + paxCnt].ToString().Trim();
            Session["MName" + paxCnt + sid] = Request.Form["mname" + paxCnt].ToString().Trim();
            Session["PLName" + paxCnt + sid] = Request.Form["lname" + paxCnt].ToString().Trim();
            Session["PDOBD" + paxCnt + sid] = Request.Form["dday" + paxCnt].ToString();
            Session["PDOBM" + paxCnt + sid] = Request.Form["dmonth" + paxCnt].ToString();
            Session["PDOBY" + paxCnt + sid] = Request.Form["dyear" + paxCnt].ToString();





            Session["PTitle" + paxCnt + ssid] = Request.Form["ptitle" + paxCnt].ToString();
            Session["PFName" + paxCnt + ssid] = Request.Form["fname" + paxCnt].ToString().Trim() + " " + Request.Form["mname" + paxCnt].ToString().Trim();
            Session["PLName" + paxCnt + ssid] = Request.Form["lname" + paxCnt].ToString().Trim();
            Session["PDOB" + paxCnt + ssid] = Request.Form["dday" + paxCnt].ToString() + "/" + Request.Form["dmonth" + paxCnt].ToString() + "/" + Request.Form["dyear" + paxCnt].ToString();
            Session["PGender" + paxCnt + ssid] = getGender(Request.Form["ptitle" + paxCnt].ToString());//Request.Form["ddlGender" + paxCnt].ToString();

            if (!isMaskedAirline)
            {
                if (paxCnt <= TotalAdults + TotalChilds)
                {
                    if (Request.Form["Seat-" + paxCnt].ToString() != "")
                    {
                        Session["Seat" + paxCnt + ssid] = Request.Form["Seat-" + paxCnt].ToString();
                        Session["Seat" + paxCnt + sid] = Request.Form["Seat-" + paxCnt].ToString();
                    }


                    if (Request.Form["Spe_ser_rq-" + paxCnt].ToString() != "")
                    {
                        Session["SpecialService" + paxCnt + ssid] = Request.Form["Spe_ser_rq-" + paxCnt].ToString();
                        Session["SpecialService" + paxCnt + sid] = Request.Form["Spe_ser_rq-" + paxCnt].ToString();

                    }


                    if (htAirLines.Count > 0)
                    {
                        string Frequent_flyer = "";
                        foreach (DictionaryEntry de in htAirLines)
                        {
                            if (Request.Form["Fre_fly-" + de.Key.ToString() + "-" + paxCnt].ToString() != "")
                                Frequent_flyer += "" + de.Key.ToString() + "-" + Request.Form["Fre_fly-" + de.Key.ToString() + "-" + paxCnt].ToString() + "-" + paxCnt + "@";

                        }

                        if (Frequent_flyer != "")
                        {
                            Session["FrequentFlyer" + paxCnt + ssid] = Frequent_flyer;
                            Session["FrequentFlyer" + paxCnt + sid] = Frequent_flyer;
                        }
                    }
                }


                if (Request.Form["Meal-" + paxCnt].ToString() != "")
                {
                    Session["Meal" + paxCnt + ssid] = Request.Form["Meal-" + paxCnt].ToString();
                    Session["Meal" + paxCnt + sid] = Request.Form["Meal-" + paxCnt].ToString();
                }
            }

            if (paxCnt <= TotalAdults)
            {
                Session["PaxType" + paxCnt + ssid] = "ADT";
            }
            else if (paxCnt > TotalAdults && paxCnt <= TotalAdults + TotalChilds)
            {
                Session["PaxType" + paxCnt + ssid] = "CHD";

            }
            else if (paxCnt > TotalAdults + TotalChilds && paxCnt <= TotalAdults + TotalChilds + TotalInfants)
            {
                Session["PaxType" + paxCnt + ssid] = "INF";
            }

        }
       

        Session["HPhoneC" + ssid] = "0";

        if (paymentType == "paynow")
        {
            Session["HPhoneNo" + ssid] = Request.Form["Mobile"].ToString();
            Session["PMobileNo" + ssid] = Request.Form["Mobile"].ToString();
            Session["HPhoneA" + ssid] = "";
            Session["PPhone" + ssid] = Request.Form["Mobile"].ToString();

            Session["PEmail" + ssid] = Request.Form["Email"].ToString();
            Session["Email"] = Request.Form["Email"].ToString();
            Session["PEmail"] = Request.Form["Email"].ToString();
        }
        else
        {
            Session["HPhoneNo" + ssid] = Request.Form["Mobile1"].ToString();
            Session["PMobileNo" + ssid] = Request.Form["Mobile1"].ToString();
            Session["HPhoneA" + ssid] = "";
            Session["PPhone" + ssid] = Request.Form["Mobile1"].ToString();

            Session["PEmail" + ssid] = Request.Form["Email2"].ToString();
            Session["Email"] = Request.Form["Email2"].ToString();
            Session["PEmail"] = Request.Form["Email2"].ToString();
        }
       
        

        Session["TtPax" + ssid] = TotalPax;
        Session["TtAdult" + ssid] = TotalAdults;
        Session["TtChild" + ssid] = TotalChilds;
        Session["TtInfant" + ssid] = TotalInfants;



        Session["CCTitle" + ssid] = Request.Form["CardHolderName"];
        Session["CCFName" + ssid] = Request.Form["CardHolderName"];
        Session["nameOnCard" + ssid] = Request.Form["CardHolderName"];


        Session["CCLName" + ssid] = "";
        //Session["CCLName" + ssid] = Request.Form["CardHolderName"];
        Session["CCType" + ssid] = Request.Form["cardtype"];

        string card_type = Session["CCType" + ssid].ToString();

        string sss = Request.Form["paymentMode"];

        Session["PAddress1" + ssid] = Request.Form["Address"];
        Session["PCity" + ssid] = Request.Form["City"];

        Session["PState" + ssid] = Request.Form["State"];
        //string c = Request.Form["country"].ToString();
        Session["PCountry" + ssid] = Request.Form["country"];
        Session["PPostCode" + ssid] = Request.Form["ZipCode"];

        if (paymentType == "paynow")
        {
            
        }
        else
        {
            Session["PEmail" + sid] = Request.Form["Email2"].ToString();

            Session["PMobileNo" + sid] = Request.Form["Mobile1"].ToString();

        }

        Session["PAddress1" + sid] = Request.Form["Address"];
        Session["PCity" + sid] = Request.Form["city"];

        Session["PState" + sid] = Request.Form["State"];
        Session["PCountry" + sid] = Request.Form["country"];
        Session["PPostCode" + sid] = Request.Form["ZipCode"];


        Session["CardNumber" + ssid] = Request.Form["cardNumber"].Replace(" ", "");

        Session["CardValidFrom" + ssid] = Request.Form["ExpMonth"].ToString() + Request.Form["ExpYear"].ToString();
        Session["CardExpiry" + ssid] = Request.Form["ExpMonth"].ToString() + Request.Form["ExpYear"].ToString();


        if (Request.Form["ExpYear"].ToString() !="")
        {
            Session["CardExpiry1" + ssid] = Request.Form["ExpMonth"].ToString() + Request.Form["ExpYear"].ToString().Substring(2, 2);
        }
        else
        {
            Session["CardExpiry1" + ssid] = "";
        }

        
        Session["CardExpiryMonth" + ssid] = Request.Form["ExpMonth"].ToString();
        Session["CardExpiryYear" + ssid] = Request.Form["ExpYear"].ToString();
        Session["CardCV2" + ssid] = Request.Form["cvc"];
        Session["IssueNo" + ssid] = "";
        Session["CreditCardAmt" + ssid] = cardCharges;
        //Session["Insurance" + ssid] = 0.0;
        Session["TtAmount" + ssid] = TtAmount;

        CheckTransactionDetailsResponse result = check_transaction_request();

        string validation = "";

        //if (Session["PaymentMode" + ssid].ToString() != "paylater")
        //{

            for (int paxcount = 1; paxcount <= TotalAdults; paxcount++)
            {
                string Response = checkAdultAge(Request.Form["dday" + paxcount].ToString() + "/" + Request.Form["dmonth" + paxcount].ToString() + "/" + Request.Form["dyear" + paxcount].ToString(), paxcount) + "-";
                if (Response != "-")
                    validation += Response;
            }

            for (int paxcount = TotalAdults + 1; paxcount <= TotalAdults + TotalChilds; paxcount++)
            {
                string Response = checkChildAge(Request.Form["dday" + paxcount].ToString() + "/" + Request.Form["dmonth" + paxcount].ToString() + "/" + Request.Form["dyear" + paxcount].ToString(), paxcount) + "-";
                if (Response != "-")
                    validation += Response;
            }

            for (int paxcount = TotalAdults + TotalChilds + 1; paxcount <= TotalAdults + TotalChilds + TotalInfants; paxcount++)
            {
                string Response = checkInfantAge(Request.Form["dday" + paxcount].ToString() + "/" + Request.Form["dmonth" + paxcount].ToString() + "/" + Request.Form["dyear" + paxcount].ToString(), paxcount) + "-";
                if (Response != "-")
                    validation += Response;
            }

            if (validation != "")
            {
                flag = false;
                diverrmsg.Attributes.Add("style", "Display:''");
                //diverrmsg.InnerHtml = validation.TrimEnd('-');
                diverrmsg.InnerHtml = "<div class='pm-content'>" +
                                                "<div class='pm-left pm-error warning'>" +
                                                    "<p>" + validation.TrimEnd('-') + "</p>" +
                                                "</div>" +
                                            "</div>";
            }
       // }

        if (flag)
        {
            wa_idno = 0;


            try
            {
                if (Session["wa_idno" + ssid] == null)
                {
                    string st = searchType;
                    if (Flexible == "Flexible")
                    {
                        st = searchType + "FL";
                    }

                    if (isMaskedAirline)
                    {
                        st = "MASK" + st;
                    }

                    //// wa_idno = website_activity.activity_Insert(Session["PTitle1" + ssid].ToString() + "." + Session["PFName1" + ssid].ToString() + " " + Session["PLName1" + ssid].ToString(), dtAAFares.Rows[0]["AirCode"].ToString(), Session["Departure" + sid].ToString(), Session["Destination" + sid].ToString(), Session["depDateTextValue" + sid].ToString(), Session["arrDateTextValue" + sid].ToString(), Session["JType" + sid].ToString(), TotalPax, Convert.ToDouble(Session["TtAmount" + ssid]), "Phone No : " + Session["HPhoneC" + ssid].ToString() + " " + Session["HPHoneNo" + ssid].ToString() + "; Mobile : " + Session["PMobileNo" + ssid].ToString() + "; Email : " + Session["PEmail" + ssid].ToString(), "", "", Session["afid" + sid].ToString(), dtAAFares.Rows[0]["InfoVia"].ToString(), st);
                    wa_idno = website_activity.activity_Insert_New1(Session["PTitle1" + ssid].ToString() + "." + Session["PFName1" + ssid].ToString() + " " + Session["PLName1" + ssid].ToString(), dtAAFares.Rows[0]["AirCode"].ToString(), Session["Departure" + sid].ToString(), Session["Destination" + sid].ToString(), Session["depDateTextValue" + sid].ToString(), Session["arrDateTextValue" + sid].ToString(), Session["JType" + sid].ToString(), TotalPax, Convert.ToDouble(Session["TtAmount" + ssid]), "Phone No : " + Session["HPhoneC" + ssid].ToString() + " " + Session["HPHoneNo" + ssid].ToString() + "; Mobile : " + Session["PMobileNo" + ssid].ToString() + "; Email : " + Session["PEmail" + ssid].ToString(), "", "", Session["afid" + sid].ToString(), dtAAFares.Rows[0]["InfoVia"].ToString(), st, "", Session["fid" + ssid].ToString());
                }
                else
                {
                    wa_idno = Convert.ToInt64(Session["wa_idno" + ssid].ToString());
                }
            }
            catch
            {
            }

            if (wa_idno != 0)
                Session["wa_idno" + ssid] = wa_idno;

            Thread threadReqalt = new Thread(new ParameterizedThreadStart(FindAlt));
            string urlTemp = @"http://testlocal.travelmerry.com/Site_SEARCH_Again_DeepLink.aspx?sid=" + sid;

            object url = (object)urlTemp;
            threadReqalt.Start(url);

            Thread.Sleep(2000);



            pnlPayment.Visible = false;
            pnl3DSecure.Visible = true;

        }
        else
        {
            DisplayData();
            //diverrmsg.Attributes.Add("style", "Display:''");
            //diverrmsg.InnerHtml = validation.TrimEnd('-');

        }






    }




    protected CheckTransactionDetailsResponse check_transaction_request()
    {
        CheckTransactionDetailsResponse response = new CheckTransactionDetailsResponse();

        try
        {


            string ipaddress;

            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipaddress == "" || ipaddress == null)

                ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();


            //TEST

            //string draUrl = "https://ci-snare.iovation.com/api/CheckTransactionDetails";

            //string subscriberid = "972102";
            //string subscriberaccount = "OLTP";
            //string subscriberpasscode = "RB28IKIJ";


            //LIVE
            string draUrl = "https://soap.iovation.com/api/CheckTransactionDetails";

            string subscriberid = "886800";
            string subscriberaccount = "OLTP";
            string subscriberpasscode = "MINZ3W8C";

            CheckTransactionDetailsService client = new CheckTransactionDetailsService(draUrl);
            CheckTransactionDetails request = new CheckTransactionDetails();
            // initialize the request parameters
            request.accountcode = Session["PEmail" + ssid].ToString();
            request.enduserip = ipaddress;
            request.beginblackbox = (Request.Form.Get("ioBB").Length > 0 && Request.Form.Get("fpBB").Length > 0)
                                        ? Request.Form.Get("ioBB") + ";" + Request.Form.Get("fpBB")
                                        : ((Request.Form.Get("ioBB").Length > 0) ? Request.Form.Get("ioBB") : Request.Form.Get("fpBB"));
            request.subscriberid = subscriberid;
            request.subscriberaccount = subscriberaccount;
            request.subscriberpasscode = subscriberpasscode;
            request.type = "checkout";

            // Add transaction insight values (these are optional values and are not required)
            CheckTransactionDetailsProperty[] ti_values = new CheckTransactionDetailsProperty[16];
            ti_values[0] = new CheckTransactionDetailsProperty();
            ti_values[0].name = "Email";
            ti_values[0].value = Session["PEmail" + ssid].ToString();

            ti_values[1] = new CheckTransactionDetailsProperty();
            ti_values[1].name = "ValueAmount";
            ti_values[1].value = String.Format("{0:0.00}", Convert.ToDouble(Session["TtAmount" + ssid].ToString()));

            ti_values[2] = new CheckTransactionDetailsProperty();
            ti_values[2].name = "ValueCurrency";
            ti_values[2].value = "USD";

            ti_values[3] = new CheckTransactionDetailsProperty();
            ti_values[3].name = "MobilePhoneNumber";



            string phonenumber = "+" + Session["PMobileNo" + ssid].ToString();


            ti_values[3].value = phonenumber;

            ti_values[4] = new CheckTransactionDetailsProperty();
            ti_values[4].name = "BillingStreet";
            ti_values[4].value = Session["PAddress1" + ssid].ToString();

            ti_values[5] = new CheckTransactionDetailsProperty();
            ti_values[5].name = "BillingCity";
            ti_values[5].value = Session["PCity" + ssid].ToString();

            ti_values[6] = new CheckTransactionDetailsProperty();
            ti_values[6].name = "BillingRegion";
            ti_values[6].value = Session["PState" + ssid].ToString();

            ti_values[7] = new CheckTransactionDetailsProperty();
            ti_values[7].name = "BillingCountry";
            ti_values[7].value = Session["PCountry" + ssid].ToString();

            ti_values[8] = new CheckTransactionDetailsProperty();
            ti_values[8].name = "BillingPostalCode";
            ti_values[8].value = Session["PPostCode" + ssid].ToString();

            ti_values[9] = new CheckTransactionDetailsProperty();
            ti_values[9].name = "ShippingStreet";
            ti_values[9].value = Session["PAddress1" + ssid].ToString();

            ti_values[10] = new CheckTransactionDetailsProperty();
            ti_values[10].name = "ShippingCity";
            ti_values[10].value = Session["PCity" + ssid].ToString();

            ti_values[11] = new CheckTransactionDetailsProperty();
            ti_values[11].name = "ShippingRegion";
            ti_values[11].value = Session["PState" + ssid].ToString();

            ti_values[12] = new CheckTransactionDetailsProperty();
            ti_values[12].name = "ShippingCountry";
            ti_values[12].value = Session["PCountry" + ssid].ToString();

            ti_values[13] = new CheckTransactionDetailsProperty();
            ti_values[13].name = "ShippingPostalCode";
            ti_values[13].value = Session["PPostCode" + ssid].ToString();

            ti_values[14] = new CheckTransactionDetailsProperty();
            ti_values[14].name = "billingShippingMismatch";
            ti_values[14].value = "0";

            ti_values[15] = new CheckTransactionDetailsProperty();
            ti_values[15].name = "CreditCardBin";
            ti_values[15].value = Session["CardNumber" + ssid].ToString().Substring(0, 6);

            request.txn_properties = ti_values;


            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                // call check login and see if we allow login
                response = client.CheckTransactionDetails(request);


                if (response != null)
                {

                    SqlParameter[] paramIovation = {
                                               new SqlParameter("@tracknumber",response.trackingnumber.ToString()),
                                               new SqlParameter("@result",response.result.ToString()),
                                               new SqlParameter("@reason",response.reason.ToString()),
                                               new SqlParameter("@Email",Session["PEmail" + ssid].ToString()),

                                               };


                    long i = DataLayer.insertData("iovation_track_insert", paramIovation, ConfigurationManager.AppSettings["Iovation"].ToString());
                    Session["Iovation_trackID" + ssid] = i;
                }

            }
            catch (Exception excp)
            {
                // callResult = -1;
            }
        }
        catch
        {
        }
        return response;
    }


    #region checkFName
    protected string checkFName(string FName)
    {
        string result = "";
        Regex regex1 = new Regex("^([A-Za-z]){1,20}$");
        if (!regex1.IsMatch(FName))
        {
            result = "Invalid First Name. ";
        }
        return result;

    }
    #endregion

    #region checkLName
    protected string checkLName(string LName)
    {
        string result = "";
        Regex regex1 = new Regex("^([A-Za-z]){2,20}$");
        if (!regex1.IsMatch(LName))
        {
            result = "Invalid Last Name. ";
        }
        return result;

    }
    #endregion

    #region checkLName
    protected string checkBirthDate(string LName)
    {
        string result = "";
        Regex regex1 = new Regex(@"(19|20)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$");
        if (!regex1.IsMatch(LName))
        {
            result = "Invalid Date. ";
        }
        return result;

    }
    #endregion

    protected string FormatTime(string P)
    {
        //P = P.PadLeft(4, '0');

        //P = P.Substring(0, 2) + ":" + P.Substring(2, 2);

        //return P;


        P = P.PadLeft(4, '0');
        string P1 = P.Substring(0, 2);
        string P2 = P.Substring(2, 2);

        int temp = Convert.ToInt32(P1);
        if (temp >= 12 && temp < 24)
        {
            if (temp > 12)
            {
                P1 = (temp - 12).ToString().PadLeft(2, '0');
            }
            P2 += " PM";
        }
        else
        {
            if (temp > 24)
            {
                temp = temp - 24;

            }

            if (temp == 0 || temp == 24)
            {
                temp = 12;

            }
            P1 = (temp).ToString().PadLeft(2, '0');


            P2 += " AM";
        }

        P = P1 + ":" + P2;

        return P;
    }


    protected string FormatDurationTime(string P)
    {
        P = P.PadLeft(4, '0');

        P = P.Substring(0, 2) + "H " + P.Substring(2, 2) + "M";

        return P;
    }
    double TotalCost = 0;
    double Totaladultfare = 0;
    public void DisplayData()
    {


        string paymentType = paymentMode.Value.ToString();

        Session["PaymentMode"] = paymentType;

        //Flight Details

        DataTable dtclosevoucher = DataLayer.GetData("select_Deals_Close_up", sqlcn);
        if (dtclosevoucher != null)
        {
            if (dtclosevoucher.Rows.Count > 0)
            {
                clossepop.InnerHtml = "<div class='discountPrice'>" +
                                            "<h3>USD" + String.Format("{0:0.00}", Convert.ToDouble(dtclosevoucher.Rows[0]["amount"].ToString())) + " <span>OFF</span></h3> " +
                                        "</div>" +
                                        "<div class='giftVocher-bg'>" +
                                            "<h5>Use Promocode: <span>" + dtclosevoucher.Rows[0]["vouchertext"].ToString() + "</span> </h5>" +
                                        "</div>";
                //closeuppopup = "true";
                promocode = dtclosevoucher.Rows[0]["vouchertext"].ToString();
            }
        }
        SqlParameter[] parametersSearch = { new SqlParameter("@sid", Convert.ToInt64(sid)) };
        dtSearchData = DataLayer.GetData("searchdata_master_select_sid", parametersSearch, strconn);


        if (Session["fid" + ssid] != null)
        {
            string id = Session["fid" + ssid].ToString();
            strIDSplit = id.Split('-');

            string query = "flight_search_fares_select_1";
            SqlParameter[] parametersFares ={new SqlParameter("@search_id",strIDSplit[0]),
                                      new SqlParameter("@GDS",strIDSplit[1]),
                                      new SqlParameter("@RfNo",strIDSplit[2])
                                      };

            dtAAFares = DataLayer.GetData(query, parametersFares, strconn);
            query = "flight_search_flights_select_2";
            SqlParameter[] parametersFlights ={new SqlParameter("@search_id",strIDSplit[0]),
                                      new SqlParameter("@GDS",strIDSplit[1]),
                                      new SqlParameter("@RfNo",strIDSplit[2])
                                      };
            dtAAFlights = DataLayer.GetData(query, parametersFlights, strconn);
        }



        if (dtAAFares != null && dtAAFlights != null)
        {
            if (dtAAFares.Rows.Count > 0 && dtAAFlights.Rows.Count > 0)
            {
                drAAFares = dtAAFares.Select();
                drAAFlights = dtAAFlights.Select();
                drAAFlightsOut = dtAAFlights.Select("LegNo=1", "search_flight_id ASC");
                drAAFlightsIn = dtAAFlights.Select("LegNo=2", "search_flight_id ASC");
                if (Session["trackid" + strIDSplit[0]] != null)
                {
                    string trid = Session["trackid" + strIDSplit[0]].ToString();
                    SqlParameter[] param_track_fare = {new SqlParameter("@Fare",dtAAFares.Rows[0]["ttFare"].ToString()),
                                                      new SqlParameter("@trID",Convert.ToInt64(trid))};
                    DataLayer.UpdateData("afid_track_data_update_fare", param_track_fare, ConfigurationManager.AppSettings["sqlcn_track_data"].ToString());

                    if (dtAAFares.Rows[0]["ciid"].ToString() != "")
                    {
                        SqlParameter[] param_track_fare1 = {
                                                            new SqlParameter("@conf",dtAAFares.Rows[0]["ciid"].ToString()),
                                                            new SqlParameter("@inid",dtAAFares.Rows[0]["inid"].ToString()),
                                                            new SqlParameter("@outid",dtAAFares.Rows[0]["outid"].ToString()),
                                                            new SqlParameter("@trID",Convert.ToInt64(trid))
                                                        };
                        DataLayer.UpdateData("afid_track_data_update_RedirectUrlSB", param_track_fare1, ConfigurationManager.AppSettings["sqlcn_track_data"].ToString());
                        conf.Value = dtAAFares.Rows[0]["ciid"].ToString();
                        inid.Value = dtAAFares.Rows[0]["inid"].ToString();
                        outid.Value = dtAAFares.Rows[0]["outid"].ToString();
                    }

                }
            }
        }



        DataTable dtCountry = DataLayer.GetData("tblDisplayData_Country_selection_1", dbAccess.connString);

        if (dtCountry != null)
        {
            if (dtCountry.Rows.Count > 0)
            {
                Countryhint.InnerHtml = "<select class='' onblur='val(this)' data-placement='bottom' data-msg-required='Please select the country of the billing address.' name='country' id='country' runat='server'>" +
            "<option value=''>Select country Name</option>";
                for (int i = 0; i < dtCountry.Rows.Count; i++)
                {
                    Countryhint.InnerHtml += "<option value='" + dtCountry.Rows[i]["Country_Code"].ToString() + "'>" + dtCountry.Rows[i]["Country_Name"].ToString() + "</option>";
                }
                // return "" + dtAirCode.Rows[0]["Airport"];
                Countryhint.InnerHtml += "</select>";
            }
        }

        string CabinClass = "";
        if (Session["Class" + sid].ToString() == "2")
        {
            CabinClass = "Business";
        }
        else if (Session["Class" + sid].ToString() == "3")
        {
            CabinClass = "First";
        }
        else if (Session["Class" + sid].ToString() == "0")
        {
            CabinClass = "Economy";
        }

        TotalAdults = Convert.ToInt32(Session["AdultsCnt" + sid].ToString());
        TotalChilds = Convert.ToInt32(Session["ChildsCnt" + sid].ToString());
        TotalInfants = Convert.ToInt32(Session["InfantsCnt" + sid].ToString());
        TotalPax = TotalAdults + TotalChilds + TotalInfants;




        string strItinerary = "";


        //  DeptCountry DestCountry  IpCountry IpCity PaxEmailID  


        DateTime DepDate = Convert.ToDateTime(dtSearchData.Rows[0]["deptDate"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        deptDate_Hj = DepDate.ToString("yyyy-MM-dd");

        DateTime ArrDate = Convert.ToDateTime(dtSearchData.Rows[0]["retDate"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));


        afid = Session["afid" + sid].ToString();
        if (dtSearchData.Rows[0]["aid"].ToString() == "2")
            afid = "Travelmerry";

        DeptDate.Value = DepDate.ToString();
        ArrDate1.Value = ArrDate.ToString();
        afid1.Value = afid;
        JType.Value = Session["JType" + sid].ToString();
        searchType1.Value = searchType;
        deptCode = dtSearchData.Rows[0]["dept"].ToString();
        destCode = dtSearchData.Rows[0]["dest"].ToString();
        //var image = "url(../images/pax_Back/" + deptCode + ".jpg)";
        //BackImgcity.Style.Add("background-image", image);


        string path1 = Server.MapPath("~/images/pax_back");
        if (File.Exists(path1 + "\\" + deptCode + ".jpg"))
        {
            var image = "url(../images/pax_back/" + deptCode + ".jpg)";
            BackImgcity.Style.Add("background-image", image);
        }
        else
        {
            var image = "url()";
            BackImgcity.Style.Add("background-image", image);
        }



        //MaskAirlines
        SqlParameter[] paramMask = { new SqlParameter("@dept",dtSearchData.Rows[0]["dept"].ToString()),
                                           new SqlParameter("@dest", dtSearchData.Rows[0]["dest"].ToString()),
                    new SqlParameter("@depDate", DepDate.ToString("yyyy-MM-dd")),
                        new SqlParameter("@retDate", ArrDate.ToString("yyyy-MM-dd")),
                                   new SqlParameter("@afid", afid/*"Flight_Journey"*/)};
        DataTable dtMaskAirlines = DataLayer.GetData("Airline_Masking_selection_source_1", paramMask, ConfigurationManager.AppSettings["airline_masking"].ToString());



        isMaskedAirline = false;


        DataRow drMask = null;

        if (drAAFares != null)
        {
            if (drAAFares.Length > 0)
            {

                if (drAAFares[0]["isRestricted"].ToString().ToUpper() == "TRUE")
                {

                    Session["fareexpired" + sid] = "true";
                    Response.Redirect("Results.aspx?sid=" + sid);
                    Response.End();
                }






                if (drAAFares[0]["MaskAirline"].ToString().ToUpper() == "TRUE")
                {
                    // isMaskedAirline = true;
                    if (dtMaskAirlines != null)
                    {
                        if (dtMaskAirlines.Rows.Count > 0)
                        {
                            if (Session["JType" + sid].ToString() == "2")
                            {
                                drMask = getMaskAirline(dtAAFares.Rows[0]["AirCode"].ToString(), dtSearchData.Rows[0]["deptDate"].ToString(), dtSearchData.Rows[0]["retDate"].ToString(), dtAAFares.Rows[0]["InfoVia"].ToString(), Session["JType" + sid].ToString(), dtSearchData.Rows[0]["dept"].ToString(), dtSearchData.Rows[0]["dest"].ToString(), dtMaskAirlines);

                            }
                            else
                            {
                                drMask = getMaskAirline(dtAAFares.Rows[0]["AirCode"].ToString(), dtSearchData.Rows[0]["deptDate"].ToString(), dtSearchData.Rows[0]["retDate"].ToString(), dtAAFares.Rows[0]["InfoVia"].ToString(), Session["JType" + sid].ToString(), dtSearchData.Rows[0]["dept"].ToString(), dtSearchData.Rows[0]["dest"].ToString(), dtMaskAirlines);
                            }

                            if (drMask != null)
                            {
                                isMaskedAirline = true;

                            }
                        }
                    }

                }

            }

        }





        bool isTransitVisaRequired = false;
        if (!IsPostBack)
        {
            if (Session["VoucherAmount" + ssid] != null)
            {
                if ("Skyscanner,Travelmerry".ToString().Contains(dtSearchData.Rows[0]["afid"].ToString()) && Convert.ToDouble(dtAAFares.Rows[0]["MarkUp"].ToString()) > 10)
                {
                    closeuppopup = "true";
                    //FDetails += "<h4 class='text-success'>Congratulation! We could offer you $5 lower fare.</h4>";
                    discount = 5;
                    HttpContext.Current.Session["VoucherAmount" + ssid] = 5;
                    HttpContext.Current.Session["VoucherCode" + ssid] = "TMGOODWILL";
                    HttpContext.Current.Session["Voucher" + ssid] = "yes";
                }
            }
            else
            {
                if ("Skyscanner,Travelmerry".ToString().Contains(dtSearchData.Rows[0]["afid"].ToString()) && Convert.ToDouble(dtAAFares.Rows[0]["MarkUp"].ToString()) > 10)
                {
                    closeuppopup = "true";
                    //FDetails += "<h4 class='text-success'>Congratulation! We could offer you $5 lower fare.</h4>";
                    discount = 5;
                    HttpContext.Current.Session["VoucherAmount" + ssid] = 5;
                    HttpContext.Current.Session["VoucherCode" + ssid] = "TMGOODWILL";
                    HttpContext.Current.Session["Voucher" + ssid] = "yes";
                }
            }
        }


        TotalCost = Convert.ToDouble(dtAAFares.Rows[0]["TtFare"].ToString());

        //totalAm.Value = "";
        //TripSummery

        TtAmount = Convert.ToDouble(dtAAFares.Rows[0]["TtFare"].ToString()) + cardCharges;
        TtAmountCalc = Convert.ToDouble(dtAAFares.Rows[0]["TtFare"].ToString()) + cardCharges;
        //if (totalAm.Value == "")
        //    totalAm.Value = TtAmount.ToString();

        //(<%= TtAmount %>) - vamount + cancel)
        //Session["VoucherCode" + ssid] Session["VoucherAmount" + ssid] Session["ExtendedCancellation" + ssid]

        if (Session["VoucherAmount" + ssid] != null)
        {
            Promo = "yes";
            PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
            TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
        }
        if (Session["ExtendedCancellation" + ssid] != null)
        {
            cancel = "yes";
            TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
        }

        DeptDate.Value = DepDate.ToString();
        ArrDate1.Value = ArrDate.ToString();
    

        string JourneyType = "RoundTrip";
        if (Session["jtype" + sid].ToString() == "1")
        {

            JourneyType = "One Way";
        }
        string adultfare = "";
        string childfare = "0.00";
        string infantfare = "0.00";
        string PaxCount = "";
        string ADTCount = TotalAdults + " Adult";
        if (TotalAdults > 1)
        {
            ADTCount = TotalAdults + " Adults";

        }
        adultfare = (TotalAdults * (Convert.ToDouble(dtAAFares.Rows[0]["ABaseFare"].ToString()) + Convert.ToDouble(dtAAFares.Rows[0]["ATax"].ToString()))).ToString();
        PaxCount += ADTCount;
        Totaladultfare = Convert.ToDouble((Convert.ToDouble(dtAAFares.Rows[0]["ABaseFare"].ToString()) + Convert.ToDouble(dtAAFares.Rows[0]["ATax"].ToString())).ToString());

        string CHDCount = "0";
        if (TotalChilds > 0)
        {
            CHDCount = TotalChilds + " Child";
            if (TotalChilds > 1)
            {
                CHDCount = TotalChilds + " Children";
            }

            PaxCount += "," + CHDCount;
            childfare = (TotalChilds * (Convert.ToDouble(dtAAFares.Rows[0]["CBaseFare"].ToString()) + Convert.ToDouble(dtAAFares.Rows[0]["CTax"].ToString()))).ToString();
        }

        string INFCount = "0";
        if (TotalInfants > 0)
        {
            INFCount = TotalInfants + " Infant in lap";
            if (TotalInfants > 1)
            {
                INFCount = TotalInfants + " Infants in lap";
            }
            PaxCount += "," + INFCount;
            infantfare = (TotalInfants * (Convert.ToDouble(dtAAFares.Rows[0]["IBaseFare"].ToString()) + Convert.ToDouble(dtAAFares.Rows[0]["ITax"].ToString()))).ToString();
        }

        if (!isMaskedAirline)
        {



            if (dtAAFares != null && dtAAFlights != null)
            {
                if (dtAAFares.Rows.Count > 0 && dtAAFlights.Rows.Count > 0)
                {


                    string FDHeader = "<div class='row'>";
                    FDHeader += "<div class='col-md-6 col-sm-6 col-xs-5 fd-hide'><h4><i class='fa fa-paper-plane'></i> <span class='fl-d-text'>Please review your flight details</span> ";
                    FDHeader += "<span class='review-txt'>Flight details</span></h4></div>";
                    FDHeader += "<div class='col-md-6 col-sm-6 col-xs-7'>";
                    FDHeader += "<div class='show-price'>";
                    FDHeader += "<p>Total reservation price:</p> " + String.Format("{0:0.00}", TtAmount) + " USD";
                    FDHeader += "<p>" + PaxCount + "," + JourneyType + "</p>";
                    FDHeader += "</div>";
                    FDHeader += "</div>";
                    FDHeader += "</div>";

                    //divFDHeader.InnerHtml = FDHeader;
                    string header = "<div class='pm-content'>" +
                                        "<div class='pm-left flight-review-title'>" +

                                       // "<h4>Reference Number:" + dtAAFares.Rows[0]["search_fares_id"].ToString() + " </h4> " +
                                            "<img src='Design/images/flight-details.png' alt='Flight icon' class='title-icons'>" +



                                            "<h4>Review Your Trip Details & Book </h4>" +
                                            "<p>This is the last step - only a couple more minutes and you’re done!</p>" +
                                        "</div>" +
                                        "<div class='pm-right pm-tl-fare flight-review-fare'>" +
                                            "<p class='pm-tl-text'>Total price</p>" +
                                            "<h4 id='totalAmount3'> " + String.Format("{0:0.00}", TtAmount) + " <small>USD</small></h4>" +
                                            "<p>* inc. all taxes and fees</p>" +
                                        "</div>" +
                                    "</div>";
                    string PaymentHeader = "<div class='pm-right pm-tl-fare'>" +
                                            "<p class='pm-tl-text'>Total price</p>" +
                                            "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount) + " <small>USD</small></h4>" +
                                            "<p>* inc. all taxes and fees</p>" +
                                        "</div>";
                    paymentHeader.InnerHtml = PaymentHeader;
                    divFDHeader1.InnerHtml = header;




                    drAAFares = dtAAFares.Select();
                    drAAFlights = dtAAFlights.Select();
                    drAAFlightsOut = dtAAFlights.Select("LegNo=1");
                    drAAFlightsIn = dtAAFlights.Select("LegNo=2");



                    string FDetails = "";
                    string FlightDetails1 = "";

                    FDetails += "<div class='row'>";
                    FDetails += "<div class='col-md-12'>";
                    FDetails += "<div class='tbl-info'>";

                    Flexible = getFlexible(dtAAFares.Rows[0]["Flexibility"].ToString(), Session["Jtype" + sid].ToString());







                    //FDetails += "<div class='pull-left'>";
                    if (Flexible == "Flexible")
                    {
                        FDetails += "<h6 class='text-danger'>This is a flexible date, please verify the date.<a data-original-title='Please verify the date.' href='#' data-toggle='tooltip' title=''><i class='fa fa-exclamation-circle'></i></a></h6>";
                    }
                    // FDetails += "</div><div class='pull-right show-price'><p>Total reservation price:</p> " + String.Format("{0:0.00}", TtAmount) + " USD<p>" + PaxCount + "," + JourneyType + "</p></div>";



                    if (discount > 0)
                    {
                        //Session["discount" + ssid] = discount;
                        FDetails += "<h4 class='text-success'>Congratulations, you got $" + String.Format("{0:0.00}", discount) + " (USD) lower fare.</h4>";

                    }



                    int seats = 0;



                    if (drAAFlightsOut != null)
                    {
                        if (drAAFlightsOut.Length > 0)
                        {


                            if (drAAFlightsOut.Length >= 2)
                            {
                                isTransitVisaRequired = true;
                            }
                            FlightDetails1 += "<div class='pm-triptype'>" +
                                                    "<div class='pm-left'>" +
                                                        "<div class='pm-ObTitle pm-outbound pm-citi-code-time '><i class='fa fa-plane'></i><span class='blue'>" + getAirportCity(drAAFlightsOut[0]["DEP_ARP"].ToString()) + " - " + getAirportCity(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString()) + "</span> " + drAAFlightsOut[0]["STR_DEP_DAT"].ToString() + "</div>" +
                                                    "</div>" +
                                                "</div>";
                            FlightDetails1 += "<div class='pm-seg-wrap'>";
                            for (int outcnt = 0; outcnt < drAAFlightsOut.Length; outcnt++)
                            {
                                if (!htAirLines.Contains(drAAFlightsOut[outcnt]["ARL_COD"].ToString()))
                                {
                                    htAirLines.Add(drAAFlightsOut[outcnt]["ARL_COD"].ToString(), drAAFlightsOut[outcnt]["CodeShare"].ToString());
                                }
                                if (outcnt == 0)
                                {
                                    seats = Convert.ToInt32(drAAFlightsOut[outcnt]["Seatsavail"].ToString());
                                }
                                if (outcnt != 0 && outcnt < drAAFlightsOut.Length)
                                {
                                    FlightDetails1 += "<div class='pm-layover'>" +
                                                            "<div class='pm-bag-info-cont text-center'><p><i class='icofont icofont-clock-time'></i> " + getAirportCity(drAAFlightsOut[outcnt]["DEP_ARP"].ToString()) + " - <span class='red'>" + drAAFlightsOut[outcnt]["stop_over_duration"].ToString() + " Layover</span></p>" +
                                                            "</div>" +
                                                        "</div>";
                                }
                                FlightDetails1 += "<div class='pm-seg-content'>";
                                string ope = "";
                                if (drAAFlightsOut[outcnt]["ARL_COD"].ToString() != drAAFlightsOut[outcnt]["CodeShare"].ToString())
                                    ope = "Operated by " + getAirline(drAAFlightsOut[outcnt]["CodeShare"].ToString()) + "";
                                FlightDetails1 += "<div class='pm-air-logo'>" +
                                                        "<img src='Design/ShortLogo/" + getAirlinimage(drAAFlightsOut[outcnt]["ARL_COD"].ToString()) + "' alt='" + getAirlinimage(drAAFlightsOut[outcnt]["ARL_COD"].ToString()) + "'>" +
                                                        "<p class='pbold'><span class='mr-15'>" + getAirline(drAAFlightsOut[outcnt]["ARL_COD"].ToString()) + "</span>	<span class='mr-15'>(" + drAAFlightsOut[outcnt]["ARL_COD"].ToString() + "-" + drAAFlightsOut[outcnt]["FLI_NUM"].ToString() + ")</span>	</p>" +
                                                        "<p class='pm-bag-txt'>" + ope + "</p>" +
                                                    "</div>";

                                FlightDetails1 += "<div class='pm-seg-A'>";
                                string Depter = "";
                                string Arrter = "";
                                if (drAAFlightsOut[outcnt]["DEP_TER"].ToString() != "")
                                {
                                    Depter = "<p>Departure Terminal: " + drAAFlightsOut[outcnt]["DEP_TER"].ToString() + " </p>";
                                }
                                if (drAAFlightsOut[outcnt]["ARR_TER"].ToString() != "")
                                {
                                    Arrter = "<p>Arrival Terminal: " + drAAFlightsOut[outcnt]["ARR_TER"].ToString() + " </p>";
                                }
                                FlightDetails1 += "<div class='pm-seg-from'>" +
                                                            "<div class='pm-citi-code-time'>" + getAirportCity(drAAFlightsOut[outcnt]["DEP_ARP"].ToString()) + " <span class='blue' data-toggle='tooltip' data-original-title='" + drAAFlightsOut[outcnt]["STR_DEP"].ToString() + "'>(" + drAAFlightsOut[outcnt]["DEP_ARP"].ToString() + ")</span> " + FormatTime(drAAFlightsOut[outcnt]["DEP_TIM"].ToString()) + "</div>" +
                                                            "<p data-toggle='tooltip' data-original-title='" + drAAFlightsOut[outcnt]["STR_DEP"].ToString() + "'>" + drAAFlightsOut[outcnt]["STR_DEP"].ToString() + ",</p>" +
                                                            "<p>" + drAAFlightsOut[outcnt]["STR_DEP_DAT"].ToString() + "</p>" +
                                                            Depter +
                                                        "</div>";

                                FlightDetails1 += "<div class='pm-duration'>" +
                                                            "<span class='pm-dur-time'>" + getHoursMinutes(drAAFlightsOut[outcnt]["elapsed_time"].ToString()) + "</span>" +
                                                            "<img src='Design/images/grey_durationicon.png' alt='Duration Icon'>" +
                                                            "<span class='pm-dur-text'>Duration</span>" +
                                                        "</div>";
                                FlightDetails1 += "<div class='pm-seg-to'>" +
                                                            "<div class='pm-citi-code-time'>" + getAirportCity(drAAFlightsOut[outcnt]["ARR_ARP"].ToString()) + " <span class='blue' data-toggle='tooltip' data-original-title='" + drAAFlightsOut[outcnt]["STR_ARR"].ToString() + "'>(" + drAAFlightsOut[outcnt]["ARR_ARP"].ToString() + ")</span> " + FormatTime(drAAFlightsOut[outcnt]["ARR_TIM"].ToString()) + "</div>" +
                                                            "<p data-toggle='tooltip' data-original-title='" + drAAFlightsOut[outcnt]["STR_ARR"].ToString() + "'>" + drAAFlightsOut[outcnt]["STR_ARR"].ToString() + ", </p>" +
                                                            "<p>" + drAAFlightsOut[outcnt]["STR_ARR_DAT"].ToString() + "</p>" +
                                                            Arrter +
                                                        "</div>";



                                FlightDetails1 += "<div class='pm-fl-info'>" +
                                                            "<p>" + getCabinClass(drAAFlightsOut[outcnt]["CABIN_CLASS"].ToString()) + "</p>" +
                                                            "<p>Aircraft: " + getEquipmentText(drAAFlightsOut[outcnt]["EQP_COD"].ToString()) + " </p>" +
                                    //"<p>Meals Types:Asian vegetarian</p>" +
                                    //"<p class='blue'>Airline PNR: DFGJ2</p>" +
                                    //"<div class='rs-Includes'>" +
                                    //    "<span class='tootlTip' data-toggle='tooltip' title='' data-original-title='Wi-Fi connectivity'><i class='icofont icofont-ui-wifi'></i></span>" +
                                    //    "<span class='tootlTip' data-toggle='tooltip' title='' data-original-title='Power outlet: AC outlet'><i class='icofont icofont-plugin'></i></span>" +
                                    //    "<span class='tootlTip' data-toggle='tooltip' title='' data-original-title='Personal Screen with on-demand option'><i class='icofont icofont-play-alt-1'></i></span>" +
                                    //"</div>" +
                                "</div>";

                                FlightDetails1 += "</div>";

                                FlightDetails1 += "</div>";

                                if (outcnt == drAAFlightsOut.Length - 1)
                                {
                                    FlightDetails1 += "<div class='pm-bag-info'>" +
                                                        "<div class='pm-bag-info-cont'>" +
                                                            "<div class='pm-left'>" +

                                                            "</div>";
                                    //"<div class='pm-right pm-bag-txt'><i class='fa fa-suitcase'></i>2 Pc Check-In Baggage, 7 Kgs Hand Baggage</div>" +


                                    if (seats > Convert.ToInt32(drAAFlightsOut[outcnt]["Seatsavail"].ToString()))
                                    {
                                        seats = Convert.ToInt32(drAAFlightsOut[outcnt]["Seatsavail"].ToString());
                                    }
                                    FlightDetails1 += "<div class='pm-right'>" +
                                                                "<p class='greenBold'>Outbound Travel Duration : " + CalculateDuration(dtAAFares.Rows[0]["out_trip_duration"].ToString()) + " </p>" +
                                                            "</div>";
                                    FlightDetails1 += "</div>" +
                                                    "</div>";
                                }
                            }
                            if (Session["JType" + sid].ToString() == "2")
                            {
                                FlightDetails1 += "</div>";
                            }
                        }
                    }
                    if (drAAFlightsIn != null)
                    {
                        if (drAAFlightsIn.Length > 0)
                        {
                            if (drAAFlightsIn.Length >= 2)
                            {
                                isTransitVisaRequired = true;
                            }
                            FlightDetails1 += "<div class='pm-triptype mt-15'>" +
                                                "<div class='pm-left'>" +
                                                    "<div class='pm-ObTitle pm-inbond pm-citi-code-time '><i class='fa fa-plane'></i><span class='blue'>" + getAirportCity(drAAFlightsIn[0]["DEP_ARP"].ToString()) + " - " + getAirportCity(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString()) + "</span> " + drAAFlightsIn[drAAFlightsIn.Length - 1]["STR_DEP_DAT"].ToString() + "</div>" +
                                                "</div>" +
                                            "</div>";
                            FlightDetails1 += "<div class='pm-seg-wrap'>";
                            for (int incnt = 0; incnt < drAAFlightsIn.Length; incnt++)
                            {
                                if (!htAirLines.Contains(drAAFlightsIn[incnt]["ARL_COD"].ToString()))
                                {
                                    htAirLines.Add(drAAFlightsIn[incnt]["ARL_COD"].ToString(), drAAFlightsIn[incnt]["CodeShare"].ToString());
                                }
                                if (incnt == 0)
                                {
                                    if (seats > Convert.ToInt32(drAAFlightsIn[incnt]["Seatsavail"].ToString()))
                                    {
                                        seats = Convert.ToInt32(drAAFlightsIn[incnt]["Seatsavail"].ToString());
                                    }
                                }
                                if (incnt != 0 && incnt < drAAFlightsIn.Length)
                                {
                                    FlightDetails1 += "<div class='pm-layover'>" +
                                                            "<div class='pm-bag-info-cont text-center'>" +
                                                                "<p><i class='icofont icofont-clock-time'></i> " + getAirportCity(drAAFlightsIn[incnt]["DEP_ARP"].ToString()) + " - <span class='red'>" + drAAFlightsIn[incnt]["stop_over_duration"].ToString() + " Layover</span></p>" +
                                                            "</div>" +
                                                        "</div>";
                                }

                                FlightDetails1 += "<div class='pm-seg-content'>";
                                string ope = "";
                                if (drAAFlightsIn[incnt]["ARL_COD"].ToString() != drAAFlightsIn[incnt]["CodeShare"].ToString())
                                    ope = "Operated by " + getAirline(drAAFlightsIn[incnt]["CodeShare"].ToString()) + "";
                                FlightDetails1 += "<div class='pm-air-logo'>" +
                                                        "<img src='Design/ShortLogo/" + getAirlinimage(drAAFlightsIn[incnt]["ARL_COD"].ToString()) + "' alt='" + getAirlinimage(drAAFlightsIn[incnt]["ARL_COD"].ToString()) + "'>" +
                                                        "<p class='pbold'><span class='mr-15'>" + getAirline(drAAFlightsIn[incnt]["ARL_COD"].ToString()) + "</span>	<span class='mr-15'>(" + drAAFlightsIn[incnt]["ARL_COD"].ToString() + "-" + drAAFlightsIn[incnt]["FLI_NUM"].ToString() + ")</span>	</p>" +
                                                        "<p class='pm-bag-txt'>" + ope + "</p>" +
                                                    "</div>";

                                FlightDetails1 += "<div class='pm-seg-A'>";
                                string Depter = "";
                                string Arrter = "";
                                if (drAAFlightsIn[incnt]["DEP_TER"].ToString() != "")
                                {
                                    Depter = "<p>Departure Terminal: " + drAAFlightsIn[incnt]["DEP_TER"].ToString() + " </p>";
                                }
                                if (drAAFlightsIn[incnt]["ARR_TER"].ToString() != "")
                                {
                                    Arrter = "<p>Arrival Terminal: " + drAAFlightsIn[incnt]["ARR_TER"].ToString() + " </p>";
                                }
                                FlightDetails1 += "<div class='pm-seg-from'>" +
                                                        "<div class='pm-citi-code-time'>" + getAirportCity(drAAFlightsIn[incnt]["DEP_ARP"].ToString()) + " <span class='blue' data-toggle='tooltip' data-original-title='" + drAAFlightsIn[incnt]["STR_DEP"].ToString() + "'>(" + drAAFlightsIn[incnt]["DEP_ARP"].ToString() + ")</span> " + FormatTime(drAAFlightsIn[incnt]["DEP_TIM"].ToString()) + "</div>" +
                                                        "<p data-toggle='tooltip' data-original-title='" + drAAFlightsIn[incnt]["STR_DEP"].ToString() + "'>" + drAAFlightsIn[incnt]["STR_DEP"].ToString() + ",</p>" +
                                                        "<p>" + drAAFlightsIn[incnt]["STR_DEP_DAT"].ToString() + "</p>" +
                                                        Depter +
                                                    "</div>";
                                FlightDetails1 += "<div class='pm-duration'>" +
                                                        "<span class='pm-dur-time'>" + getHoursMinutes(drAAFlightsIn[incnt]["elapsed_time"].ToString()) + "</span>" +
                                                        "<img src='Design/images/grey_durationicon.png' alt='Duration Icon'>" +
                                                        "<span class='pm-dur-text'>Duration</span>" +
                                                    "</div>";

                                FlightDetails1 += "<div class='pm-seg-to'>" +
                                                        "<div class='pm-citi-code-time'>" + getAirportCity(drAAFlightsIn[incnt]["ARR_ARP"].ToString()) + " <span class='blue' data-toggle='tooltip' data-original-title='" + drAAFlightsIn[incnt]["STR_ARR"].ToString() + "'>(" + drAAFlightsIn[incnt]["ARR_ARP"].ToString() + ")</span> " + FormatTime(drAAFlightsIn[incnt]["ARR_TIM"].ToString()) + "</div>" +
                                                        "<p data-toggle='tooltip' data-original-title='" + drAAFlightsIn[incnt]["STR_ARR"].ToString() + "'>" + drAAFlightsIn[incnt]["STR_ARR"].ToString() + ", </p>" +
                                                        "<p>" + drAAFlightsIn[incnt]["STR_ARR_DAT"].ToString() + "</p>" +
                                                        Arrter +
                                                    "</div>";

                                FlightDetails1 += "<div class='pm-fl-info'>" +
                                                        "<p>" + getCabinClass(drAAFlightsIn[incnt]["CABIN_CLASS"].ToString()) + "</p>" +
                                                        "<p>Aircraft: " + getEquipmentText(drAAFlightsIn[incnt]["EQP_COD"].ToString()) + " </p>" +
                                    //"<div class='rs-Includes'>" +
                                    //    "<span class='tootlTip' data-toggle='tooltip' title='' data-original-title='Wi-Fi connectivity'><i class='icofont icofont-ui-wifi'></i></span>" +
                                    //    "<span class='tootlTip' data-toggle='tooltip' title='' data-original-title='Power outlet: AC outlet'><i class='icofont icofont-plugin'></i></span>" +
                                    //    "<span class='tootlTip' data-toggle='tooltip' title='' data-original-title='Personal Screen with on-demand option'><i class='icofont icofont-play-alt-1'></i></span>" +
                                    //"</div>" +
                                                    "</div>";

                                FlightDetails1 += "</div>";
                                FlightDetails1 += "</div>";



                                //FlightDetails1 +=   "<div class='pm-bag-info'>" +
                                //                        "<div class='pm-bag-info-cont'>" +
                                //                            "<div class='pm-left'>" +
                                //                                "<p class='pm-bag-txt'><a href='http://www.timaticweb2.com/integration/external?ref=699d6f2fe8d16c221031d4c46c58695b&amp;rule=/dc=US/dec=JFK/dcc=US/dac=SFO/ad=2018-03-13/sd=21/sdt=days/st=vacation/ac=B6' target='_blank'><i class='icofont icofont-credit-card'></i> Visa Detail Check</a></p>" +
                                //                            "</div>";
                                if (incnt == drAAFlightsIn.Length - 1)
                                {
                                    if (seats > Convert.ToInt32(drAAFlightsIn[incnt]["Seatsavail"].ToString()))
                                    {
                                        seats = Convert.ToInt32(drAAFlightsIn[incnt]["Seatsavail"].ToString());
                                    }
                                    FlightDetails1 += "<div class='pm-bag-info'>" +
                                                        "<div class='pm-bag-info-cont'>" +
                                                            "<div class='pm-left'>" +
                                        //"<p class='pm-bag-txt'><a href='http://www.timaticweb2.com/integration/external?ref=699d6f2fe8d16c221031d4c46c58695b&amp;rule=/dc=US/dec=JFK/dcc=US/dac=SFO/ad=2018-03-13/sd=21/sdt=days/st=vacation/ac=B6' target='_blank'><i class='icofont icofont-credit-card'></i> Visa Detail Check</a></p>" +
                                                            "</div>";
                                    FlightDetails1 += "<div class='pm-right'>" +
                                                                "<p class='greenBold'>Inbound Travel Duration : " + CalculateDuration(dtAAFares.Rows[0]["in_trip_duration"].ToString()) + "</p>" +
                                                            "</div>";
                                    FlightDetails1 += "</div>" +
                                                        "</div>";
                                }

                            }
                            FlightDetails1 += "";
                        }
                    }


                    string tcURL = getTrasitVisaDetails(dtAAFares, dtAAFlights);

                    backresults.InnerHtml = "<div class='backtoRslt'>" + 
                                                "<div class='backtoRslt-Inner'>";

                    backresults.InnerHtml += "<div class='ticketsLeft'><a href='FlightResults.aspx?sid=" + sid + "&ssid=" + ssid + "'><i class='fa fa-angle-left' aria-hidden='true'></i>More Results</a></div>" +
                                                "</div>" +
                                                "<div class='reviewProcess'>" +
                                                    "<ul>" +
                                                        "<li class='reviewProcessList'><i class='fa fa-search'></i>Review</li>" +
                                                        "<li><i class='fa fa-user'></i>Travellers</li>" +
                                                        "<li><i class='fa fa-ticket'></i>Payment</li>" +
                                                    "</ul>" +
                                                "</div>" +
                                            "</div>";
                    string seatavail = "";
                    if (seats > 0)
                    {
                        seatavail = "<span class='backtoResult'><span class='tcktleft'>Only " + seats + " Seats left</span></span>";
                    }
                    string header1 = "<span class='congText'><img src='Design/images/wing-ribbon-icon.png' alt='Congrats ribbon'> Congratulations, You got the best price. </span>" +
                        "<span class='cancellTxt blue mr-10'>Booking Reference : " + dtAAFares.Rows[0]["search_fares_id"].ToString() +
                                        //"<span class='cancellTxt'>4 Hour Free Cancellation <a href='#' id='popover-Icon'><i class='fa fa-info-circle' aria-hidden='true'></i></a>" +
                                        seatavail +
                                        "</span>" +



                                        "<div id='popover-msg'>" +
                                            "<p>" +
                                                "If you ever need to make changes to your travel plans, we’ve got you covered! Our Customer Care specialists are available 24/7," +
                                                "so you can reach them at any time for assistance. Travelmerry standard cancellation fees apply." +
                                                "If you’d like extra time to change your mind, opt-in to our Extended Cancellation Policy," +
                                                "for $15 per passenger, this allows you the option to cancel your reservation for a full 24-hour period without having to pay" +
                                                "Travelmerry standard cancellation fees. For more information, see our terms and conditions." +
                                            "</p>" +
                                        "</div>";
                    divFDHeader2.InnerHtml = header1;
                    FDetails += "";
                    FDetails += "";
                    FDetails += "<div class='thead-main thead-main-arr'>";
                    FDetails += "<div class='th'>";
                    FDetails += "<small><a href='' class='text-blue spacert' data-toggle='modal' data-target='#Farerules'><i class='fa fa-usd'></i> Fare rules</a></small>";
                    FDetails += "<small><a href='' class='text-blue spacert' data-toggle='modal' data-target='#BaggageInfo'><i class='fa fa-suitcase'></i> Baggage Info</a></small>";

                    if (tcURL != "")
                    {
                        FDetails += "<small><a href='" + tcURL + "' target='_blank' class='text-blue'><i class='fa-file-text'></i>Visa Detail Check</a></small>";
                    }

                    FDetails += "<strong class='pull-right'>* All the times are local to Airports</strong>";
                    FDetails += "";
                    FDetails += "</div>";
                    FDetails += "</div>";




                    FDetails += "</div>";
                    FDetails += "";
                    FDetails += "";
                    FDetails += "</div>";
                    FDetails += "</div>";









                    FDetails += "";
                    FDetails += "<div class='row'>";
                    FDetails += "<div class='col-md-12'>";
                    FDetails += "<div id='Farerules' class='modal fade'>";
                    FDetails += "<div class='modal-dialog'>";
                    FDetails += "<div class='modal-content'>";
                    FDetails += "<div class='modal-header'>";
                    FDetails += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>";
                    FDetails += "<h4 class='modal-title'><i class='fa fa-usd'></i> Fare Rules</h4>";
                    FDetails += "</div>";
                    FDetails += "<div class='modal-body'>";
                    FDetails += "<div class='airfare-costs'>";


                    //FDetails += "<div>";
                    //FDetails += "<p><h5>Cancellation Fee</h5>";
                    //FDetails += "Airline Fee* - This is a non-refundable ticket.<br>";
                    //FDetails += "TravelMerry Fee - USD 50 per passenger.<br>";
                    //FDetails += "Partly utilized tickets cannot be cancelled.</p>";
                    //FDetails += "";
                    //FDetails += "<p><h5>Change Fee</h5>";
                    //FDetails += "Airline Fee* - USD 167 per passenger + fare difference (if applicable).<br>";
                    //FDetails += "TravelMerry Fee - USD 50 per passenger.</p>";
                    //FDetails += "";
                    //FDetails += "<p class='text-justify'>*Airlines stop accepting cancellation/change requests 4 - 72 hours before departure of the flight, depending on the airline. The airline fee is indicative based on an automated interpretation of airline fare rules. TravelMerry doesn't guarantee the accuracy of this information. The change/cancellation fee may also vary based on fluctuations in currency conversion rates. For exact cancellation/change fee, please call us at our customer care number. </p>";
                    //FDetails += "</div>";
                    //FDetails += "";


                    FDetails += "<div>";
                    FDetails += "<h5>Booking Rules:</h5>";
                    FDetails += "<p><h5>Book with confidence: We offer a 4 hour Full Refund Guarantee.  </h5>";
                    FDetails += "";
                    FDetails += "<p><h5>Details</h5>";
                    FDetails += "Fare is non-refundable, name change is not permitted and ticket is non-transferable.</p>";
                    FDetails += "";
                    FDetails += "<p class='text-justify'>Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee) and is based on availability of flight at the time of change.</p>";
                    FDetails += "";
                    FDetails += "<p class='text-justify'> Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change).</p>";
                    FDetails += "";
                    FDetails += "<p class='text-justify'>Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p>";
                    FDetails += "";
                    FDetails += "<p><h5> Contact our 24/7 customer service to make any changes.</h5>";
                    FDetails += "Prior to completing the booking in the <a href='/Termsandconditions.aspx' target='_blank' class='text-blue'>Terms and Conditions</a>, you should review our service fees for exchanges, changes, refunds and cancellations.</p>";
                    FDetails += "";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "<div class='modal-footer'>";
                    FDetails += "<button type='button' class='btn btn-default' data-dismiss='modal'>Close</button>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "";
                    FDetails += "<div id='BaggageInfo' class='modal fade'>";
                    FDetails += "<div class='modal-dialog'>";
                    FDetails += "<div class='modal-content'>";
                    FDetails += "<div class='modal-header'>";
                    FDetails += "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>";
                    FDetails += "<h4 class='modal-title'><i class='fa fa-suitcase'></i> Baggage Information</h4>";
                    FDetails += "</div>";
                    FDetails += "<div class='modal-body'>";
                    FDetails += "<div class='airfare-costs'>";
                    FDetails += "<table class='table-responsive'>";
                    FDetails += "<thead>";
                    FDetails += "<tr>";
                    FDetails += "<th width='40%'><span>Sector/Flight</span></th>";
                    FDetails += "<th width='40%'><span>Check-in Baggage</span></th>";
                    FDetails += "<th width='20%'><span>Cabin Baggage</span></th>";
                    FDetails += "</tr>";
                    FDetails += "</thead>";
                    FDetails += "";
                    FlightDetails1 += "<div class='pm-bag-info'>" +
                        "<div class='pm-bag-info-cont'>";


                    DataRow[] drFlights = dtAAFlights.Select("", "search_flight_id ASC");
                    if (drFlights != null)
                    {
                        for (int seg = 0; seg < drFlights.Length; seg++)
                        {
                            FDetails += " </span></td>";
                            FDetails += "<td><span><i class='fa fa-suitcase'></i> 7kgs</span></td>";
                            FDetails += "</tr>";
                            FDetails += "";
                        }
                        FlightDetails1 += "<ul>";
                        DataRow[] drAdultBaggage = dtAAFlights.Select("", "AdultBaggage ASC");
                        DataRow[] drChildBaggage = dtAAFlights.Select("", "ChildBaggage ASC");
                        DataRow[] drInfantBaggage = dtAAFlights.Select("", "InfantBaggage ASC");

                        for (int iii = 0; iii < 1; iii++)
                        {
                            if (drAdultBaggage[iii]["AdultBaggage"].ToString() != "")
                            {
                                FlightDetails1 += "<li><i class='fa fa-suitcase'></i>Check In Baggage: " + drAdultBaggage[iii]["AdultBaggage"].ToString() + "/Adult - Hand Baggage: 7 Kg/Adult</li>";
                            }
                            else
                            {
                                FlightDetails1 += "<li><i class='fa fa-suitcase'></i>Check In Baggage:Zero(0) Baggage/Adult - Hand Baggage:Zero(0) Baggage/Adult</li>";
                            }
                        }
                        if (TotalChilds > 0)
                        {
                            for (int iii = 0; iii < 1; iii++)
                            {
                                if (drChildBaggage[iii]["ChildBaggage"].ToString() != "")
                                {
                                    FlightDetails1 += "<li><i class='fa fa-suitcase'></i>Check In Baggage: " + drChildBaggage[iii]["ChildBaggage"].ToString() + "/Child - Hand Baggage: 7 Kg/Child</li>";
                                }
                                else
                                {
                                    FlightDetails1 += "<li><i class='fa fa-suitcase'></i>Check In Baggage:Zero(0) Baggage/Child - Hand Baggage:Zero(0) Baggage/Child</li>";
                                }
                            }
                        }
                        if (TotalInfants > 0)
                        {
                            for (int iii = 0; iii < 1; iii++)
                            {
                                if (drInfantBaggage[iii]["InfantBaggage"].ToString() != "")
                                {
                                    FlightDetails1 += "<li><i class='fa fa-suitcase'></i>Check In Baggage: " + drInfantBaggage[iii]["InfantBaggage"].ToString() + "/Infant - Hand Baggage: 7 Kg/Infant</li>";
                                }
                                else
                                {
                                    FlightDetails1 += "<li><i class='fa fa-suitcase'></i>Check In Baggage:Zero(0) Baggage/Infant - Hand Baggage:Zero(0) Baggage/Infant</li>";
                                }
                            }
                        }
                        FlightDetails1 += "</ul>";
                    }

                    if (Session["JType" + sid].ToString() == "2")
                    {
                        FlightDetails1 += "</div>";
                    }
                    else
                        FlightDetails1 += "</div>";

                    FlightDetails1 += "" +
                        "</div></div>";
                    FDetails += "</table>";
                    FDetails += "<hr></hr>";
                    FDetails += "<p><span class='text-danger'>**</span>Information not available</p>";
                    FDetails += "<p class='text-justify'>The information presented above is as obtained from the airline reservation system. TravelMerry does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</p>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "";
                    FDetails += "<div class='modal-footer'>";
                    FDetails += "<button type='button' class='btn btn-default' data-dismiss='modal'>Close</button>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "</div>";
                    FDetails += "";
                    FDetails += "</div>";
                    FDetails += "</div>";

                    string asdf = CalculateDuration(dtAAFares.Rows[0]["out_trip_duration"].ToString().PadLeft(4, '0'), dtAAFares.Rows[0]["in_trip_duration"].ToString().PadLeft(4, '0'));

                    var dur = asdf.PadLeft(4, '0');
                    long hours = int.Parse(dur.Substring(0, 2));
                    hours = (int.Parse(hours.ToString())) * 60;
                    hours = hours / 60;
                    string hour = Math.Round(decimal.Parse(hours.ToString())) + " Hours";

                    long mins = int.Parse(dur.Substring(2, 2));
                    mins = (int.Parse(mins.ToString())) * 60;
                    mins = mins / 60;
                    string min = Math.Round(decimal.Parse(mins.ToString())) + " Mins";

                    FlightDetails1 += "<div class='mt-10'><span class='font12'> * All the times are local to Airports</span><span class='pm-right font12'>In-Flight services and amenities may vary and are subject to change.</span></div>";
                    FlightDetails1 += "<div class='mt-10'><span class='font12'> ** Baggage information not available<br/>The information presented above is as obtained from the airline reservation system. TravelMerry does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</span></div>";

                    //FlightDetails1 += "<div class='mt-10'><span class='font12'> * All the times are local to Airports</span></div>";
                    //FlightDetails1 += "<div class='mt-10'><span class='pm-right font12'></span></div>";
                    //FlightDetails1 += "<div class='mt-10'><a href='http://www.timaticweb2.com/integration/external?ref=699d6f2fe8d16c221031d4c46c58695b&amp;rule=/dc=US/dec=JFK/dcc=US/dac=SFO/ad=2018-03-13/sd=21/sdt=days/st=vacation/ac=B6' target='_blank'><i class='icofont icofont-credit-card'></i> Visa Detail Check</a></div>";
                    divFlightDetails.InnerHtml = FlightDetails1;
                    Session["divFlightDetails" + ssid] = FlightDetails1;
                    // divFlightDetails.Controls.Add(new LiteralControl(FDetails));



                }
            }
        }
        else
        {
            if (drMask["isConfirm"].ToString().ToUpper() != "TRUE")
            {
                Session["ConfirmPageMask" + ssid] = "true";
            }

            string FDHeader = "<div class='row'>";
            FDHeader += "<div class='col-md-6 col-sm-6 col-xs-5 fd-hide'><h4><i class='fa fa-paper-plane'></i> <span class='fl-d-text'>Please review your flight details</span> ";
            FDHeader += "<span class='review-txt'>Flight details</span></h4></div>";
            FDHeader += "<div class='col-md-6 col-sm-6 col-xs-7'>";
            FDHeader += "<div class='show-price'>";
            FDHeader += "<p>Total reservation price:</p> " + String.Format("{0:0.00}", TtAmount) + " USD";
            FDHeader += "<p>" + PaxCount + "," + JourneyType + "</p>";
            FDHeader += "</div>";
            FDHeader += "</div>";
            FDHeader += "</div>";

            //divFDHeader.InnerHtml = FDHeader;<div class='pm-content'><div class='pm-left flight-review-title'><img src='Design/images/flight-details.png' alt='Flight icon' class='title-icons'><h4>Review Your Trip Details &amp; Book </h4><p>This is the last step - only a couple more minutes and you’re done!</p></div><div class='pm-right pm-tl-fare flight-review-fare'><p class='pm-tl-text'>Total price</p><h4 id='totalAmount3'> 756.58 <small>USD</small></h4><p>* inc. all taxes and fees</p></div></div>
            string header = "<div class='pm-content'>" +
                                "<div class='pm-left flight-review-title'>" +
                                    "<img src='Design/images/flight-details.png' alt='Flight icon' class='title-icons'>" +
                                    "<h4>Review Your Trip Details & Book </h4>" +
                                    "<p>This is the last step - only a couple more minutes and you’re done!</p>" +
                                "</div>" +
                                "<div class='pm-right pm-tl-fare'>" +
                                    "<p class='pm-tl-text'>Total price</p>" +
                                    "<h4> " + String.Format("{0:0.00}", TtAmount) + " <small>USD</small></h4>" +
                                    "<p>* inc. all taxes and fees</p>" +
                                "</div>" +
                            "</div>";

            divFDHeader1.InnerHtml = header;

            string header1 = "<div class='congratsTitle'><span class='congText'><img src='Design/images/wing-ribbon-icon.png' alt='Congrats ribbon'> Congratulations, you have selected lowest fare. </span></div>" +
                                "<div class='tmSpecialText'><span class='backtoResult'><span class=''>This is <span class='logoBlue'>Travel</span><span class='logoRed'>Merry</span> exclusive</span></span></div>";
            header1 += "<div class='bargainFare-header'>" +
                                "<div class='bargainFare-title'><img src='Design/images/bargain-icon.png' alt='Bargain Fare Icon'> Special Bargain Fare </div>" +
                                "<div class='bargainFare-callUs'><img src='Design/images/telephoneIcon.png' alt='Telephone Icon'>  <a href='tel:+1-844-666-3779'>+1-844-666-3779</a></div>" +
                            "</div>";
            //string header1 =    "<div class='pm-content'>"+
            //                        "<div class='congratsTitle'>" +
            //                           "<span class='congText'><img src='Design/images/wing-ribbon-icon.png' alt='COngrats ribbon'> Congratulations you have selected cheapest fare </span>" +
            //                        "</div>" +
            //                        "<div class='tmSpecialText'>" +
            //                          "This is <span class='logoBlue'>Travel</span><span class='logoRed'>Merry</span> special" +
            //                        "</div>" +
            //                    "</div>";
            divFDHeader2.InnerHtml = header1;

            DateTime deptDate = DateTime.ParseExact(drAAFlightsOut[0]["DEP_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime arrDate = DateTime.ParseExact(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = arrDate.Subtract(deptDate);


            int DepartureHours = Convert.ToInt32(drAAFlightsOut[0]["DEP_TIM"].ToString()) / 100;
            int ArrivalHours = Convert.ToInt32(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_TIM"].ToString()) / 100;
            string FDetails1 = "<div class='bargainFare-body'><p class='bargText'>Special Bargain Fare helps you to find a lower fare for your trip. These fares are very low but we cannot reveal the name of the airline until you make the booking. The airlines who offer these fares are also very reputed and we believe this itinerary won't disappoint you. Our free cancellation policy helps you to cancel the trip with in 4hours of your booking. Call us now and give the reference number displayed below. We are open 24/7.</p>";
            FDetails1 += "<div class='bargainFare-result'>";
            FDetails1 += "<div class='bargainFare-result-left'>";


            string FDetails = "<div class='result-bg' style='padding:12px;'>";

            string FlightDetails1 = "";

            FDetails += "<div class='row'>";


            FDetails += "<div class='col-md-12'>";




            FDetails += "<div class='pull-left'></div>";

            //FDetails += "<div class='pull-right show-price'><p>Total reservation price:</p> " + String.Format("{0:0.00}", TtAmount) + " USD<p>" + PaxCount + "," + JourneyType + "</p></div>";



            //FDetails += "<h3 class='blue-text' style='margin-top:10px;'><i class='fa fa-plane'></i> Review your trip</h3>";

            //FDetails += "<h5 class='text-success'><i class='fa fa-check'></i> Congratulations You have selected lowest fare.</h5>";
            FDetails += "<span class='blue'>" + getAirportCity(drAAFlightsOut[drAAFlightsOut.Length - 1]["DEP_ARP"].ToString()) + " - " + getAirportCity(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString()) + ", </h4>";
            FDetails += "</div>";

            FDetails += "<div class='col-md-9 col-sm-9 col-xs-12'>";
            FDetails += "<div class='flight-details'>";
            FDetails += "<div class='deprt'>";
            FDetails += "<h5>" + getAirportCity(drAAFlightsOut[0]["DEP_ARP"].ToString()) + " (" + drAAFlightsOut[0]["DEP_ARP"].ToString() + ")</h5>";


            string Departs_Time = "";
            if (DepartureHours > 3)
            {
                Departs_Time = FormatTime((DepartureHours - 3).ToString().PadLeft(2, '0') + "00") + " - " + FormatTime((DepartureHours + 3).ToString().PadLeft(2, '0') + "00");
                // Departs_Time = (DepartureHours - 3).ToString().PadLeft(2, '0') + ":00 - " + (DepartureHours + 3).ToString().PadLeft(2, '0') + ":00";
            }
            else
            {
                Departs_Time = FormatTime("0000") + " - " + FormatTime((DepartureHours + 3).ToString().PadLeft(2, '0') + "00");
                //  Departs_Time = "00:00 - " + (DepartureHours + 3).ToString().PadLeft(2, '0') + ":00";

            }



            FDetails += "<span class='travel-dates'>Depart: " + Departs_Time + "</span>";
            FDetails += "</div>";
            FDetails += "<span class='arrow'>→</span>";
            FDetails += "<div class='arrv'>";
            FDetails += "<h5>" + getAirportCity(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString()) + " (" + drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString() + ")</h5>";

            string Arrival_Time = "";
            if (ts.Days > 0)
            {

                if (ts.Days == 1)
                {
                    Arrival_Time = "Next Day";
                }
                else
                {
                    Arrival_Time = "After " + ts.Days + "Days";
                }


            }
            else
            {
                if (ArrivalHours > 3)
                {
                    Arrival_Time = FormatTime((ArrivalHours - 3).ToString().PadLeft(2, '0') + "00") + " - " + FormatTime((ArrivalHours + 3).ToString().PadLeft(2, '0') + "00");
                    // Arrival_Time = (ArrivalHours - 3).ToString().PadLeft(2, '0') + ":00 - " + (ArrivalHours + 3).ToString().PadLeft(2, '0') + ":00";
                }
                else
                {
                    Arrival_Time = FormatTime("0000") + " - " + FormatTime((ArrivalHours + 3).ToString().PadLeft(2, '0') + "00");
                    // Arrival_Time = "00:00 - " + (ArrivalHours + 3).ToString().PadLeft(2, '0') + ":00";

                }

            }



            FDetails += "<span class='travel-dates'>Arrives " + Arrival_Time + "</span>";
            FDetails += "</div>";
            FDetails += "</div>";
            FDetails += "</div>";
            FDetails += "<div class='col-md-2 col-sm-2 col-xs-12'>";
            FDetails += "<div class='fltdtails-time'>";
            FDetails += "<span class='travel-dates'><i class='fa fa-plane'></i> Stops</span>";


            string strStops = "";

            int noOFStops = Convert.ToInt32(drAAFlightsOut.Length) - 1;
            if (noOFStops == 0)
            {
                strStops = "Non-Stop";
            }
            else if (noOFStops == 1)
            {
                strStops = noOFStops + " Stop";
            }
            else
            {
                strStops = noOFStops + " Stops";
            }

            FDetails += "<h5 style='margin-top:3px;'>" + strStops + "</h5>";
            FDetails += "</div>";
            FDetails += "</div>";


            FDetails += "</div>";

            FDetails1 += "<div class='bargainFare-result-a'>" +
                                    "<h5 class='bargainFare-dep'><i class='fa fa-plane'></i> " + deptDate.ToString("ddd, MMM d, yyyy") + " Departure to " + getAirportCity(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString()) + " (" + drAAFlightsOut[0]["DEP_ARP"].ToString() + ")</h5>" +

                                    "<div class='bargainFare-segment-depart'>" +
                                        "<div class='bargainFare-code-time'>" + getAirportCity(drAAFlightsOut[0]["DEP_ARP"].ToString()) + " <span class='blue'>(" + drAAFlightsOut[0]["DEP_ARP"].ToString() + ")</span></div>" +
                                        "<p>Depart: " + Departs_Time + "</p>" +
                                    "</div>" +
                                    "<div class='bargainFare-segment-duration'>" +
                                        "<div class='bargainFare-duration'>Layover</div>" +
                                        "<img src='Design/images/detail-durationicon.png' alt='duration Icon'>" +
                                        "<p class='grey'>" + strStops + "</p>" +
                                    "</div>" +
                                    "<div class='bargainFare-segment-arriv'>" +
                                        "<div class='bargainFare-code-time'> " + getAirportCity(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString()) + " <span class='blue'>(" + drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString() + ")</span></div>" +
                                        "<p> Arrive: " + Arrival_Time + "</p>" +
                                    "</div>" +
                                "</div>";

            if (Session["JType" + sid].ToString() == "2")
            {


                deptDate = DateTime.ParseExact(drAAFlightsIn[0]["DEP_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
                arrDate = DateTime.ParseExact(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
                ts = arrDate.Subtract(deptDate);


                DepartureHours = Convert.ToInt32(drAAFlightsIn[0]["DEP_TIM"].ToString()) / 100;
                ArrivalHours = Convert.ToInt32(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_TIM"].ToString()) / 100;




                FDetails += "<div class='row'><div class='col-md-12'><hr></div></div>";





                FDetails += "<div class='row'>";


                FDetails += "<div class='col-md-12'>";
                FDetails += "<h4>" + deptDate.ToString("ddd, MMM d") + " Departure to " + getAirportCity(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString()) + " (" + drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString() + ")</h4>";
                FDetails += "</div>";

                FDetails += "<div class='col-md-9 col-sm-9 col-xs-12'>";
                FDetails += "<div class='flight-details'>";
                FDetails += "<div class='deprt'>";
                FDetails += "<h5>" + getAirportCity(drAAFlightsIn[0]["DEP_ARP"].ToString()) + " (" + drAAFlightsIn[0]["DEP_ARP"].ToString() + ")</h5>";


                Departs_Time = "";
                if (DepartureHours > 3)
                {
                    Departs_Time = FormatTime((DepartureHours - 3).ToString().PadLeft(2, '0') + "00") + " - " + FormatTime((DepartureHours + 3).ToString().PadLeft(2, '0') + "00");
                    // Departs_Time = (DepartureHours - 3).ToString().PadLeft(2, '0') + ":00 - " + (DepartureHours + 3).ToString().PadLeft(2, '0') + ":00";
                }
                else
                {
                    Departs_Time = FormatTime("0000") + " - " + FormatTime((DepartureHours + 3).ToString().PadLeft(2, '0') + "00");
                    //  Departs_Time = "00:00 - " + (DepartureHours + 3).ToString().PadLeft(2, '0') + ":00";

                }



                FDetails += "<span class='travel-dates'>Depart: " + Departs_Time + "</span>";
                FDetails += "</div>";
                FDetails += "<span class='arrow'>→</span>";
                FDetails += "<div class='arrv'>";
                FDetails += "<h5>" + getAirportCity(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString()) + " (" + drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString() + ")</h5>";

                Arrival_Time = "";
                if (ts.Days > 0)
                {

                    if (ts.Days == 1)
                    {
                        Arrival_Time = "Next Day";
                    }
                    else
                    {
                        Arrival_Time = "After " + ts.Days + "Days";
                    }


                }
                else
                {
                    if (ArrivalHours > 3)
                    {
                        Arrival_Time = FormatTime((ArrivalHours - 3).ToString().PadLeft(2, '0') + "00") + " - " + FormatTime((ArrivalHours + 3).ToString().PadLeft(2, '0') + "00");
                        // Arrival_Time = (ArrivalHours - 3).ToString().PadLeft(2, '0') + ":00 - " + (ArrivalHours + 3).ToString().PadLeft(2, '0') + ":00";
                    }
                    else
                    {
                        Arrival_Time = FormatTime("0000") + " - " + FormatTime((ArrivalHours + 3).ToString().PadLeft(2, '0') + "00");
                        // Arrival_Time = "00:00 - " + (ArrivalHours + 3).ToString().PadLeft(2, '0') + ":00";

                    }

                }



                FDetails += "<span class='travel-dates'>Arrives " + Arrival_Time + "</span>";
                FDetails += "</div>";
                FDetails += "</div>";
                FDetails += "</div>";
                FDetails += "<div class='col-md-2 col-sm-2 col-xs-12'>";
                FDetails += "<div class='fltdtails-time'>";
                FDetails += "<span class='travel-dates'><i class='fa fa-plane'></i> Stops</span>";


                strStops = "";

                noOFStops = Convert.ToInt32(drAAFlightsIn.Length) - 1;
                if (noOFStops == 0)
                {
                    strStops = "Non-Stop";
                }
                else if (noOFStops == 1)
                {
                    strStops = noOFStops + " Stop";
                }
                else
                {
                    strStops = noOFStops + " Stops";
                }

                FDetails += "<h5 style='margin-top:3px;'>" + strStops + "</h5>";
                FDetails += "</div>";
                FDetails += "</div>";


                FDetails += "</div>";

                FDetails1 += "<div class='bargainFare-result-a'>" +
                                    "<h5 class='bargainFare-dep'><i class='fa fa-plane'></i> " + deptDate.ToString("ddd, MMM d, yyyy") + " Departure to " + getAirportCity(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString()) + " (" + drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString() + ")</h5>" +

                                    "<div class='bargainFare-segment-depart'>" +
                                        "<div class='bargainFare-code-time'>" + getAirportCity(drAAFlightsIn[0]["DEP_ARP"].ToString()) + " <span class='blue'>(" + drAAFlightsIn[0]["DEP_ARP"].ToString() + ")</span></div>" +
                                        "<p>Depart: " + Departs_Time + "</p>" +
                                    "</div>" +
                                    "<div class='bargainFare-segment-duration'>" +
                                        "<div class='bargainFare-duration'>Layover</div>" +
                                        "<img src='Design/images/detail-durationicon.png' alt='duration Icon'>" +
                                        "<p class='grey'>" + strStops + "</p>" +
                                    "</div>" +
                                    "<div class='bargainFare-segment-arriv'>" +
                                        "<div class='bargainFare-code-time'> " + getAirportCity(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString()) + " <span class='blue'>(" + drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_ARP"].ToString() + ")</span></div>" +
                                        "<p> Arrive: " + Arrival_Time + "</p>" +
                                    "</div>" +
                                "</div>";
            }




            FDetails += "<div class='row'>";
            FDetails += "<div class='col-md-12'>";
            FDetails += "<div class='space'></div>";
            FDetails += "<div class='bg-primary'>";
            FDetails += "<strong>" + drMask["mask_airlinename"].ToString() + ":</strong><p>" + drMask["Description"].ToString() + "</p>";
            FDetails += "<h4><i class='fa fa-exclamation-circle'></i>  Important Flight Information </h4>";
            FDetails += "<ul class='list-unstyled'>";
            FDetails += "<li> Flights are usually scheduled to depart sometime between 6am and 10pm.</li>";
            FDetails += "<li> Exact flight details, including airline, will be available in your itinerary after booking.</li>";
            FDetails += "<li> Your trip may include a change of aircraft or airline.</li>";
            FDetails += "<li> Additional fees for checked baggage or other services may apply</li>";
            FDetails += "</ul><a href='/Termsandconditions.aspx' target='_blank'>Read Terms and Conditions</a>";
            FDetails += "</div>";
            FDetails += "<div class='space'></div>";
            FDetails += "</div>";
            FDetails += "</div>";
            FDetails += "</div>";
            FDetails1 += "</div>";

            FDetails1 += "<div class='pm-bag-info'>" +
                                "<div class='pm-bag-info-cont'>" +
                                    "<ul>" +
                                        "<li>* All the times are local to Airports.</li>" +
                                        "<li class='pull-right'><span class='font12'>In-Flight services and amenities may vary and are subject to change.</span></li>" +
                                    "</ul>" +
                                "</div>" +
                            "</div>";
            FDetails1 += "<div class='pm-bag-info'>" +
                               "<div class='pm-bag-info-cont'>" +
                                   "<ul>" +
                                       "<li>All the Dearture and arrival times provided are in between the time slots provided.</li>" +
                                   "</ul>" +
                               "</div>" +
                           "</div>";

            FDetails1 += "</div>";
            FDetails1 += "</div>";
            divFlightDetails.InnerHtml = FDetails1;
            Session["divFlightDetails" + ssid] = FDetails1;
        }






        // Passenger Details


        string PaxDetails = "";
        PaxDetails += "<div class='pm-head-title'>" +
                            "<div class='pm-content'>" +
                                "<div class='travelerTitle faa-parent animated-hover'>" +
                                    "<img src='Design/images/passenger-icon.png' alt='Passenger icon' class='title-icons faa-passing'>" +
                                    "Passenger Details" +
                                    "<p>All travelers must provide their full name, date of birth that match exactly with the Passport or Government issued photo ID.</p><p>(Note : A Transit Visa may be required while connecting at International gateway.)</p>" +
                                "</div>" +
                            "</div>" +
                        "</div>";
        //PaxDetails += "<div class='pm-error warning'>" +
        //                    "<ul>" +
        //                        "<li><strong>Important :</strong> All travelers must provide their full name, date of birth that match exactly with the Passport or Government issued photo ID..</li>";
        //if (isTransitVisaRequired)
        //{
        //    PaxDetails += "<li><strong>Note :</strong> A Transit Visa may be required while connecting at International gateway.</li>";
        //}
        //PaxDetails += "</ul>" +
        //                "</div>";
        string PDetails = "";

        //PDetails += "<div class='important'>";
        //PDetails += "<strong>Important:</strong> All travelers must provide their full name, date of birth that match exactly with the Passport or Government issued photo ID.";



        //if (isTransitVisaRequired)
        //{
        //    PDetails += "<br/><br/><strong>Note :</strong> A Transit Visa may be required while connecting at International gateway.";
        //}


        PDetails += "</div>";
        PDetails += "<div class='space'></div>";





        _adtcount.Value = TotalAdults.ToString();
        _chdcount.Value = TotalChilds.ToString();
        _infcount.Value = TotalInfants.ToString();
        _paxcount.Value = TotalPax.ToString();


        string PaxType = "ADT";
        string PaxTypeName = "Adult";
        //PaxDetails += "<div class='pm-content'><div class='alert alert-warning'>" +
        //                                "<ul>" +
        //                                    "<li><strong>Important:</strong> All travelers must provide their full name, date of birth that match exactly with the Passport or Government issued photo ID.</li>" +
        //                                    "<li><strong>Note :</strong> A Transit Visa may be required while connecting at International gateway.</li>" +
        //                                "</ul>" +
        //                            "</div></div>";
        int serialno = 0;
        PaxDetails += "<div class='content'>";
        PaxDetails += "<div class=''>";
        string daterange = "";
        DateTime arrDaterange = new DateTime();
        if (JType.Value == "2")
        {
            arrDaterange = DateTime.ParseExact(drAAFlightsIn[drAAFlightsIn.Length - 1]["ARR_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            arrivedate = arrDaterange.ToShortDateString();
        }
        else
        {
            arrDaterange = DateTime.ParseExact(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            arrivedate = arrDaterange.ToShortDateString();
        }
        string rangemonth = "";
        string rangeyear = "";
        for (int paxcnt = 1; paxcnt <= TotalPax; paxcnt++)
        {
            PaxDetails += "<div class='tripdetails-seg-wrap'>";
            if (paxcnt == TotalAdults + 1 && TotalChilds > 0)
            {
                serialno = 0;
                PaxType = "CHD";
                PaxTypeName = "Child";

                DateTime startDate = arrDaterange.AddYears(-2)/*.AddDays(1)*/;
                DateTime EndDate = arrDaterange.AddYears(-11).AddDays(1);
                //daterange = drAAFlightsIn[drAAFlightsIn.Length-1]["STR_ARR_DAT"].ToString();
                daterange = "(Born Between " + EndDate.ToString("MMM d,  yyyy") + " to " + startDate.ToString("MMM d,  yyyy") + ")";
            }
            else if (paxcnt == TotalAdults + TotalChilds + 1 && TotalInfants > 0)
            {
                serialno = 0;
                PaxType = "INF";
                PaxTypeName = "Infant in lap";

                DateTime startDate = arrDaterange/*.AddDays(1)*/;
                DateTime EndDate = arrDaterange.AddYears(-2).AddDays(1);
                //daterange = drAAFlightsIn[drAAFlightsIn.Length - 1]["STR_ARR_DAT"].ToString();
                daterange = "(Born After - " + EndDate.ToString("MMM d,  yyyy") + ")";
            }
            //else
            //{
            //    DateTime startDate = arrDaterange.AddYears(-2)/*.AddDays(1)*/;
            //    DateTime EndDate = arrDaterange.AddYears(-11).AddDays(+1);
            //    //daterange = drAAFlightsIn[drAAFlightsIn.Length - 1]["STR_ARR_DAT"].ToString();
            //    daterange = "(Born Before - " + EndDate.ToString("MMM d,  yyyy") +")";
            //}


            PaxDetails += "<div class='PassengerTitle'>" +
                                "Passenger " + (paxcnt) + " - " + PaxTypeName +
                            "</div>";








            PaxDetails += "<div class='row'>";
            PaxDetails += "<div class='col-md-12'>";
            PaxDetails += "<div class='smSelect tool-main-wrap'>" +
                                        "<label>Title: <span class='starMark'>*</span></label>" +
                                        "<div class='selectbx tooltip' id='ptitlehint" + paxcnt + "'>" +
                                            "<select class='' onblur='val(this)' data-placement='bottom' name='ptitle" + paxcnt + "' id='ptitle" + paxcnt + "' data-msg-required=''>";
            PaxDetails += "<option value=''>Title</option>";
            if (PaxType == "ADT")
            {
                if (Session["PTitle" + paxcnt + sid] != null)
                {
                    if (Session["PTitle" + paxcnt + sid].ToString() == "Mr")
                    {
                        PaxDetails += "<option value='Mr' selected>Mr</option>";
                        PaxDetails += "<option value='Ms'>Ms</option>";
                        PaxDetails += "<option value='Mrs'>Mrs</option>";
                    }
                    else if (Session["PTitle" + paxcnt + sid].ToString() == "Ms")
                    {
                        PaxDetails += "<option value='Mr'>Mr</option>";
                        PaxDetails += "<option value='Ms' >Ms</option>";
                        PaxDetails += "<option value='Mrs'>Mrs</option>";
                    }
                    else if (Session["PTitle" + paxcnt + sid].ToString() == "Mrs")
                    {
                        PaxDetails += "<option value='Mr'>Mr</option>";
                        PaxDetails += "<option value='Ms'>Ms</option>";
                        PaxDetails += "<option value='Mrs' >Mrs</option>";
                    }
                    else
                    {

                        PaxDetails += "<option value='Mr'>Mr</option>";
                        PaxDetails += "<option value='Ms'>Ms</option>";
                        PaxDetails += "<option value='Mrs'>Mrs</option>";

                    }
                }
                else
                {

                    PaxDetails += "<option value='Mr'>Mr</option>";
                    PaxDetails += "<option value='Ms'>Ms</option>";
                    PaxDetails += "<option value='Mrs'>Mrs</option>";

                }
            }
            else if (PaxType == "CHD" || PaxType == "INF")
            {
                if (Session["PTitle" + paxcnt + sid] != null)
                {
                    if (Session["PTitle" + paxcnt + sid].ToString() == "Mstr")
                    {
                        PaxDetails += "<option value='Mstr' >Mstr</option>";
                        PaxDetails += "<option value='Miss'>Miss</option>";
                    }
                    else if (Session["PTitle" + paxcnt + sid].ToString() == "Miss")
                    {
                        PaxDetails += "<option value='Mstr'>Mstr</option>";
                        PaxDetails += "<option value='Miss' >Miss</option>";
                    }
                    else
                    {
                        PaxDetails += "<option value='Mstr'>Mstr</option>";
                        PaxDetails += "<option value='Miss'>Miss</option>";
                    }
                }
                else
                {

                    PaxDetails += "<option value='Mstr'>Mstr</option>";
                    PaxDetails += "<option value='Miss'>Miss</option>";
                }



            }
            PaxDetails += "</select>" +
                                        "</div>" +
                                    "</div>";

            PaxDetails += "<div class='mdInput tool-main-wrap'>" +
                                "<label for='First Name'>First Name: <span class='starMark'>*</span></label>" +
                                "<div class='inputbx tooltip' id='fnamehint" + paxcnt + "'>";
            if (Session["FName" + paxcnt + sid] != null)
            {
                PaxDetails += "<input value='" + Session["FName" + paxcnt + sid] + "' onblur='val(this)' data-placement='bottom' type='text' id='fname" + paxcnt + "' name='fname" + paxcnt + "' placeholder='Enter First Name'>";
            }
            else
            {
                PaxDetails += "<input type='text' id='fname" + paxcnt + "' onblur='val(this)' data-placement='bottom' name='fname" + paxcnt + "'  placeholder='Enter First Name'>";
            }
            //PaxDetails+="<span class='form_hint'>The given + middle name has to be exactly the same as stated on your passport or government issued photo ID, including the middle name on the day of travel.\nName changes are not allowed following ticketing.<p></p><p>&nbsp;</p><p style='font-size: 16px'>e.g Middle Name included:</p><p style='font-size: 14px'>'Mary Jane Kathy' will be entered as 'MaryJaneKathy'</p><p style='font-size: 14px'>Note: If you have multiple first names, the airline will remove the space.</p><p>&nbsp;</p><p style='font-size: 16px'>Do not use punctuation or special characters:</p><p style='font-size: 14px'>'JosÃ©' becomes 'Jose'</p><p style='font-size: 14px'>'Anna-Louise' becomes 'Annalouise'</p></span>";
            PaxDetails += "</div>" +
                            "</div>";

            PaxDetails += "<div class='mdInput tool-main-wrap'>" +
                                "<label for='Middle Name'>Middle Name (Optional)</label>" +
                                "<div class='inputbx tooltip' id='mnamehint" + paxcnt + "'>";

            if (Session["MName" + paxcnt + sid] != null)
            {
                PaxDetails += "<input type='text' id='mname" + paxcnt + "' onblur='val(this)' data-placement='bottom' name='mname" + paxcnt + "'  placeholder='Enter Middle Name' value='" + Session["MName" + paxcnt + sid].ToString() + "'>";
            }
            else
            {
                PaxDetails += "<input type='text' id='mname" + paxcnt + "' onblur='val(this)' data-placement='bottom' name='mname" + paxcnt + "'  placeholder='Enter Middle Name'>";
            }
            PaxDetails += "</div>" +
                             "</div>";

            PaxDetails += "<div class='mdInput tool-main-wrap'>" +
                                "<label for='Last Name'>Last Name: <span class='starMark'>*</span></label>" +
                                "<div class='inputbx tooltip' id='lnamehint" + paxcnt + "'>";
            if (Session["PLName" + paxcnt + sid] != null)
            {
                PaxDetails += "<input type='text' id='lname" + paxcnt + "' onblur='val(this)' data-placement='bottom' name='lname" + paxcnt + "'  placeholder='Enter Last Name' value='" + Session["PLName" + paxcnt + sid] + "' >";
            }
            else
            {
                PaxDetails += "<input type='text' id='lname" + paxcnt + "' onblur='val(this)' data-placement='bottom' name='lname" + paxcnt + "'  placeholder='Enter Last Name'>";
            }
            PaxDetails += "</div>" +
                            "</div>";
            PaxDetails += "</div>";
            PaxDetails += "</div>";

            PaxDetails += "<div class='row'>" +
                            "<div class='col-md-12'>" +
                                "<div class='dob'>" +
                                    "<label>Date of Birth: <span class='starMark'>*</span> <span class=''>" + daterange + "</span></label>" +
                                    "<div class='passengerDate tool-main-wrap'>" +

                                    "<div class='selectbx tooltip' id='ddayhint" + paxcnt + "'>" +
                                        "<select class='' onblur='validate(" + paxcnt + ",\"" + PaxType + "\",this)' data-placement='bottom' name='dday" + paxcnt + "' id='dday" + paxcnt + "'  data-msg-required='Please Select the Date for Pax.'>";
            PaxDetails += "<option value=''>Day</option>";
            for (int i = 1; i <= 31; i++)
            {
                if (Session["PDOBD" + paxcnt + sid] != null)
                {
                    if (Session["PDOBD" + paxcnt + sid].ToString() == i.ToString().PadLeft(2, '0'))
                    {
                        PaxDetails += "<option value='" + i.ToString().PadLeft(2, '0') + "' selected>" + i.ToString().PadLeft(2, '0') + "</option>";
                    }
                    else
                    {
                        PaxDetails += "<option value='" + i.ToString().PadLeft(2, '0') + "'>" + i.ToString().PadLeft(2, '0') + "</option>";
                    }
                }
                else
                {
                    PaxDetails += "<option value='" + i.ToString().PadLeft(2, '0') + "'>" + i.ToString().PadLeft(2, '0') + "</option>";

                }
            }
            PaxDetails += "</select>";

            PaxDetails += "</div>";
            PaxDetails += "</div>";


            string[] monthInt = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            string[] monthString = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            PaxDetails += "<div class='passengerMonth tool-main-wrap'>" +
                                    "<div class='selectbx tooltip' id='dmonthhint" + paxcnt + "'>" +
                                        "<select class='' onblur='validate(" + paxcnt + ",\"" + PaxType + "\",this)' data-placement='bottom' name='dmonth" + paxcnt + "' id='dmonth" + paxcnt + "' data-msg-required='Please Select the Month for Pax.'>";
            PaxDetails += "<option value=''>Month</option>";
            for (int i = 0; i < monthInt.Length; i++)
            {
                if (Session["PDOBM" + paxcnt + sid] != null)
                {
                    if (Session["PDOBM" + paxcnt + sid].ToString() == monthInt[i])
                    {
                        PaxDetails += "<option value='" + monthInt[i] + "' selected>" + monthString[i] + "</option>";
                    }
                    else
                    {
                        PaxDetails += "<option value='" + monthInt[i] + "' >" + monthString[i] + "</option>";
                    }
                }
                else
                {
                    PaxDetails += "<option value='" + monthInt[i] + "' >" + monthString[i] + "</option>";
                }
            }

            PaxDetails += "</select>";
            PaxDetails += "</div>";
            PaxDetails += "</div>";




            PaxDetails += "<div class='passengerYear tool-main-wrap'>" +
                                    "<div class='selectbx tooltip' id='dyearhint" + paxcnt + "'>" +
                                        "<select class='' onblur='validate(" + paxcnt + ",\"" + PaxType + "\",this)' data-placement='bottom' name='dyear" + paxcnt + "' id='dyear" + paxcnt + "' >";

            int FromYears = 11;
            int PastYears = 100;

            if (PaxType == "CHD")
            {
                FromYears = 2;
                PastYears = 12;
            }
            if (PaxType == "INF")
            {
                FromYears = 0;
                PastYears = 3;
            }
            PaxDetails += "<option value=''>Year</option>";
            for (int i = FromYears; i < PastYears; i++)
            {
                if (Session["PDOBY" + paxcnt + sid] != null)
                {

                    if (Session["PDOBY" + paxcnt + sid].ToString() == (DateTime.Now.Year - i).ToString())
                    {
                        PaxDetails += "<option value='" + (DateTime.Now.Year - i) + "' selected>" + (DateTime.Now.Year - i) + "</option>";
                    }
                    else
                    {
                        PaxDetails += "<option value='" + (DateTime.Now.Year - i) + "'>" + (DateTime.Now.Year - i) + "</option>";
                    }
                }
                else
                {
                    PaxDetails += "<option value='" + (DateTime.Now.Year - i) + "'>" + (DateTime.Now.Year - i) + "</option>";
                }
            }
            PaxDetails += "</select>";
            PaxDetails += "</div>";
            PaxDetails += "</div>";
            PaxDetails += "</div>";
            if (!isMaskedAirline)
                PaxDetails += "<a class='accordion-toggle AddRequestBtn' data-toggle='collapse' data-parent='#accordion' href='#AdditionalRequests" + paxcnt + "' >Frequent Flyer Airline (optional) and meal request  <i class='fa fa-plus'></i></a>";
            PaxDetails += "</div>";

            if (!isMaskedAirline)
            {
                PaxDetails += "<div id='accordion'>";
                PaxDetails += "<div id='AdditionalRequests" + paxcnt + "' class='panel-collapse collapse' aria-expanded='false' >";
                PaxDetails += "<div class='AddRequest'>";


                SqlParameter[] paramSeatPref = { new SqlParameter("@special_service_request_type_id", 1) };
                DataTable dtSeatPrefernces = DataLayer.GetData("special_request_selection_by_typeid", paramSeatPref, ConfigurationManager.AppSettings["sqlcn"].ToString());


                PaxDetails += "<div class='row'>";
                PaxDetails += "<div class='col-md-12'>";

                PaxDetails += "<div class='smSelect'>" +
                                            "<label>Seat Preference:</label>" +
                                            "<div class='selectbx'>";


                if (PaxType == "INF")
                {
                    PaxDetails += "<select class='' name='Seat-" + paxcnt + "' id='Seat-" + paxcnt + "' style='width:100%;' disabled>";

                }
                else
                {
                    PaxDetails += "<select class='' name='Seat-" + paxcnt + "' id='Seat-" + paxcnt + "' style='width:100%;'>";
                }
                if (dtSeatPrefernces != null)
                {
                    if (dtSeatPrefernces.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSeatPrefernces.Rows.Count; i++)
                        {

                            bool isSelected = false;

                            if (Session["Seat" + paxcnt + sid] != null)
                            {
                                if (Session["Seat" + paxcnt + sid].ToString() == dtSeatPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString())
                                {

                                    isSelected = true;
                                }
                            }


                            if (isSelected)
                            {

                                if (dtSeatPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                {
                                    PaxDetails += "<option value='' selected>" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }
                                else
                                {
                                    PaxDetails += "<option value='" + dtSeatPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "' selected>" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }

                            }
                            else
                            {
                                if (dtSeatPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                {
                                    PaxDetails += "<option value=''>" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }
                                else
                                {
                                    PaxDetails += "<option value='" + dtSeatPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "'>" + dtSeatPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }
                            }
                        }
                    }
                }


                PaxDetails += "</select>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";

                PaxDetails += "<div class='smSelect'>" +
                                            "<label>Meal Preference:</label>" +
                                            "<div class='selectbx'>";

                SqlParameter[] paramMealPref = { new SqlParameter("@special_service_request_type_id", 2) };
                DataTable dtMealPrefernces = DataLayer.GetData("special_request_selection_by_typeid", paramMealPref, ConfigurationManager.AppSettings["sqlcn"].ToString());

                PaxDetails += "<select class='' name='Meal-" + paxcnt + "' id='Meal-" + paxcnt + "' data-msg-required='Please select the meal preference'>";
                if (dtMealPrefernces != null)
                {
                    if (dtMealPrefernces.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtMealPrefernces.Rows.Count; i++)
                        {



                            bool isSelected = false;

                            if (Session["Meal" + paxcnt + sid] != null)
                            {
                                if (Session["Meal" + paxcnt + sid].ToString() == dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString())
                                {

                                    isSelected = true;
                                }
                            }




                            if (PaxType == "INF")
                            {


                                if (isSelected)
                                {
                                    if (dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                    {
                                        PaxDetails += "<option value='' selected>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                    else if (dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "BBML")
                                    {
                                        PaxDetails += "<option value='" + dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "' selected>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                }
                                else
                                {

                                    if (dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                    {
                                        PaxDetails += "<option value=''>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                    else if (dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "BBML")
                                    {
                                        PaxDetails += "<option value='" + dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "'>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                }
                            }
                            else
                            {
                                if (isSelected)
                                {
                                    if (dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                    {
                                        PaxDetails += "<option value='' selected>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                    else
                                    {
                                        PaxDetails += "<option value='" + dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "' selected>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                }
                                else
                                {
                                    if (dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                    {
                                        PaxDetails += "<option value=''>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }
                                    else
                                    {
                                        PaxDetails += "<option value='" + dtMealPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "'>" + dtMealPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                    }

                                }
                            }
                        }
                    }
                }

                PaxDetails += "</select>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";


                SqlParameter[] paramSpecialPref = { new SqlParameter("@special_service_request_type_id", 3) };
                DataTable dtSpecialPrefernces = DataLayer.GetData("special_request_selection_by_typeid", paramSpecialPref, ConfigurationManager.AppSettings["sqlcn"].ToString());



                PaxDetails += "<div class='smSelect'>" +
                                            "<label>Special Service:</label>" +
                                            "<div class='selectbx'>";
                if (PaxType == "INF")
                {
                    PaxDetails += "<select class='' name='Spe_ser_rq-" + paxcnt + "' id='Spe_ser_rq-" + paxcnt + "' data-msg-required='Please select the special service' disabled>";
                }
                else
                {
                    PaxDetails += "<select class='' name='Spe_ser_rq-" + paxcnt + "' id='Spe_ser_rq-" + paxcnt + "' data-msg-required='Please select the special service'>";
                }

                if (dtSpecialPrefernces != null)
                {
                    if (dtSpecialPrefernces.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSpecialPrefernces.Rows.Count; i++)
                        {

                            bool isSelected = false;

                            if (Session["SpecialService" + paxcnt + sid] != null)
                            {
                                if (Session["SpecialService" + paxcnt + sid].ToString() == dtSpecialPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString())
                                {

                                    isSelected = true;
                                }
                            }


                            if (isSelected)
                            {
                                if (dtSpecialPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                {
                                    PaxDetails += "<option value='' selected>" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }
                                else
                                {
                                    PaxDetails += "<option value='" + dtSpecialPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "' selected>" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }

                            }
                            else
                            {
                                if (dtSpecialPrefernces.Rows[i]["IATA_Request_CODE"].ToString() == "")
                                {
                                    PaxDetails += "<option value=''>" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }
                                else
                                {
                                    PaxDetails += "<option value='" + dtSpecialPrefernces.Rows[i]["IATA_Request_CODE"].ToString() + "-" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_id"].ToString() + "'>" + dtSpecialPrefernces.Rows[i]["special_service_request_type_sub_txt"].ToString() + "</option>";
                                }
                            }
                        }
                    }
                }


                PaxDetails += "</select>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";





                PaxDetails += "<div class='row'>";
                PaxDetails += "<div class='col-md-12'>" +
                                        "<h4>Frequent Flyer:</h4>";

                int index = 0;
                string FFNumber = "";
                string[] splitFFlyerNum = null;
                if (Session["FrequentFlyer" + paxcnt + sid] != null)
                {
                    string Frequent_Flyer_Number = Session["FrequentFlyer" + paxcnt + sid].ToString();
                    splitFFlyerNum = Frequent_Flyer_Number.Split('@');
                }

                foreach (DictionaryEntry de in htAirLines)
                {


                    if (splitFFlyerNum != null)
                    {
                        if (splitFFlyerNum.Length > 0)
                        {
                            if (index < splitFFlyerNum.Length)
                            {
                                FFNumber = splitFFlyerNum[index].Split('-')[1].ToString();
                            }
                        }

                    }
                    if (index > 0 && (index % 3) == 0)
                    {
                        //PDetails += "</div>";
                        //PDetails += "</div>";
                        //PDetails += "<div class='row'>";
                        //PDetails += "<div class='col-md-12'>";
                    }
                    PaxDetails += "<div class='mdInput'>" +
                                        "<label for='" + getAirline(de.Key.ToString()) + "'>" + getAirline(de.Key.ToString()) + "</label>" +
                                        "<div class='inputbx toolSpan'>";
                    if (PaxType == "INF")
                    {
                        PaxDetails += "<input type='text' name='Fre_fly-" + de.Key.ToString() + "-" + paxcnt + "' class='' id='Fre_fly-" + de.Key.ToString() + "-" + paxcnt + "' value='' placeholder='flyer number' disabled>";
                    }
                    else
                    {
                        if (FFNumber != "")
                        {
                            PaxDetails += "<input type='text'  value='" + FFNumber + "' name='Fre_fly-" + de.Key.ToString() + "-" + paxcnt + "' class='' id='Fre_fly-" + de.Key.ToString() + "-" + paxcnt + "'  placeholder='flyer number'>";

                        }
                        else
                        {
                            PaxDetails += "<input type='text' name='Fre_fly-" + de.Key.ToString() + "-" + paxcnt + "' class='' id='Fre_fly-" + de.Key.ToString() + "-" + paxcnt + "' value='' placeholder='flyer number'>";
                        }
                    }
                    PaxDetails += "</div>";
                    PaxDetails += "</div>";
                    index++;

                }

                if (index % 3 != 0)
                {
                    while (index % 3 != 0)
                    {
                        //PDetails += "<div class='col-md-4 col-sm-4 col-xs-12'>";
                        //PDetails += "</div>";
                        index++;
                    }

                }

                PaxDetails += "</div>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";
                PaxDetails += "</div>";

            }
            PaxDetails += "</div>";
            PaxDetails += "</div>";

        }

        PaxDetails += "</div>";
        PaxDetails += "</div>";


        divPassengerDetails.InnerHtml = PaxDetails;
        //divPassengerDetails1.InnerHtml = PaxDetails;


        // 1


        //chktext.InnerHtml = "By clicking “Pay Now” button, you agree to our <a href='Termsandconditions.aspx' target='_blank' class='blue'>Terms and Conditions</a> including refund and cancellation policy of this booking.";

        tripsummery.InnerHtml = "<div class='pm-content'>" +
                                        "<div class='pm-left pm-footer-price'>" +
                                            "<p class='pm-footer-tp'>Total price</p>" +
                                            "<h4 id='totalAmount4'> " + String.Format("{0:0.00}", TtAmount) + " <small>USD</small></h4>" +
                                            "<p>* inc. all taxes and fees</p>" +
                                        "</div>" +
                                        "<div class='pm-right text-right'>" +
                                            "<button type='button' id='btnbooknow' class='pm-payment-btn btn p-now' onclick='return btn_Submit();'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                                        "</div>" +
                                    "</div>";



        divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
                                            "<div class='pm-content'>" +
                                                "<div class='pm-payDetails'>" +
                                                "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
                                                    "Price Details (USD)" +
                                                    "<p>Confirm the price details of your itinerary for all passengers.</p>" +
                                                "</div>" +
                                            "</div>" +
                                        "</div>" +
                                        "<div class='pm-content'>" +

                                            "<div class='fareDetails'>" +
                                                "<div class='fareDetailsBody'>" +
                                                    "<div class='fareDetailsLeft'>" +
                                                        "<div class='ccodeContent'>" +
                                                            "<label for='Promo Code'>Promo Code:</label>";

        if (Session["VoucherCode" + ssid] != null)
            divPriceDetails.InnerHtml += "<span id='Voucher_Ihint1' class='red'>Voucher is valid for " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " USD</span>";
        else
            divPriceDetails.InnerHtml += "<span id='Voucher_Ihint1' class='red'></span>";

        divPriceDetails.InnerHtml += "<div id='promo-code-input'>" +
                                                                "<div class='input-group col-md-12'>";
        if (Session["VoucherCode" + ssid] != null)
            divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id1' id='Voucher_Id1'>";
        else
            divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id1' id='Voucher_Id1'>";
        divPriceDetails.InnerHtml += "<span class='input-group-btn'>" +
                                                                        "<button class='btn btn-lg' type='button' id='btn_Voucher' onclick='ValidateVoucher()'>Apply</button>" +
                                                                    "</span>" +
                                                                "</div>" +
                                                            "</div>" +
                                                        "</div>" +
                                                    "</div>" +
                                                    "<div class='fareDetailsRight'>" +
                                                        "<div class='fareQuoted'>" +
                                                            "<div class='fareQuoteLeft'>Trip Amount</div> " +
                                                            "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmountCalc)) + " <small>USD</small></div>" +
                                                        "</div>";
       // divPriceDetails.InnerHtml += "<div class='fareQuoted green'>";
       // //divPriceDetails.InnerHtml += "<div class='fareQuoteLeft'>24 Hours Cancellation Fee</div>";
       // if (Session["ExtendedCancellation" + ssid] != null)
       //     divPriceDetails.InnerHtml += "<div class='fareQuoteLeft' id='dPD1'>24 Hours Cancellation Fee - Opted</div> <div class='fareQuoteRight' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
       //// else
       //     //divPriceDetails.InnerHtml += "<div class='fareQuoteLeft' id='dPD1'>24 Hours Cancellation Fee - Not Opted</div><div class='fareQuoteRight' id='CancellationFee'> 0.00 <small>USD</small></div>";
       // divPriceDetails.InnerHtml += "</div>";



        if (Session["ExtendedCancellation" + ssid] != null)
        {
            divPriceDetails.InnerHtml += "<div class='fareQuoted green' id='dPD1' runat='server' style='display:' >";
            divPriceDetails.InnerHtml += "<div class='fareQuoteLeft' >Lower Price Finder and  </br>24 Hours Cancellation Fee</div> <div class='fareQuoteRight' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
            divPriceDetails.InnerHtml += "</div>";
        }
        else 
        {
            divPriceDetails.InnerHtml += "<div class='fareQuoted green' id='dPD1' runat='server' style='display:none'>";
            divPriceDetails.InnerHtml += "<div class='fareQuoteLeft' >Lower Price Finder and </br>24 Hours Cancellation Fee</div> <div class='fareQuoteRight' id='CancellationFee'> 0.00<small>USD</small></div>";
            divPriceDetails.InnerHtml += "</div>";
        }



                                                         divPriceDetails.InnerHtml += "<div class='fareQuoted offerCode'>" +
                                                            "<div class='fareQuoteLeft '>Promocode</div>";

        if (Session["VoucherAmount" + ssid] != null)
            divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
        else
            divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";

        //divPriceDetails.InnerHtml += "</div>" +
        //                                                "<div class='fareQuoted green'>" +
        //                                                    "<div class='fareQuoteLeft '>CSA Travel Protection</div>";

        //if (Session["InsuranceAmount" + ssid] != null)
        //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='Insurance'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["InsuranceAmount" + ssid].ToString())) + " <small>USD</small></div>";
        //else
        //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='Insurance'> 0.00 <small>USD</small></div>";

        divPriceDetails.InnerHtml += "</div>" +
                                                    "</div>" +
                                                "</div>" +
                                                "<div class='fareDetailsFooter'>" +
                                                    "<div class='tlprice'>" +
                                                        "<div class='tlpriceLeft'>Total</div>" +
                                                        "<div class='tlpriceRight' id='totalAmount'>" +
                                                            "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " USD</h4>" +
                                                            "<p>incl. all taxes and fees</p>" +
                                                        "</div>" +
                                                    "</div>" +
                                                "</div>" +
                                                "<div class='fareNote'>" +
                                                    "<p><strong>Please Note:</strong> All fares are quoted in USD. Some airlines may charge " +
                                                    "<a  href='/ViewBaggageInfo.aspx' target='_blank'>baggage fees</a>. Your credit/debit card may be billed in multiple charges totaling the final total price. </p>" +
                                                "</div>" +
                                            "</div>" +
                                        "</div>";







        divPriceDetails1.InnerHtml = "";

        try
        {
            if (HttpContext.Current.Session["fingerprint"] != null)
            {
                SqlParameter[] param1 = { new SqlParameter("@Device_id", HttpContext.Current.Session["fingerprint"].ToString()) };
                DataTable dt = DataLayer.GetData("Select_Recent_Search_1", param1, ConfigurationManager.AppSettings["sqlcn_track_data"].ToString());

                int cnt_res = 0;

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {


                        divPriceDetails1.InnerHtml += "<div class='rcs-wrap'>";
                        divPriceDetails1.InnerHtml += "<div class='rcs-header'>";
                        divPriceDetails1.InnerHtml += "<h4>Recent searches</h4>";
                        divPriceDetails1.InnerHtml += "</div>";
                        divPriceDetails1.InnerHtml += "<div class='rcs-wrap-list hideContent'>";



                        foreach (DataRow dr in dt.Rows)
                        {



                            string url = "aff_link.aspx?conf=" + dr["conf"] + "&out=" + dr["outid"] + "&in=" + dr["inid"] + "&afid=travelmerry&aid=2";

                            if (cnt_res > 0)
                            {

                                divPriceDetails1.InnerHtml += "<div class='rcs-wrap-list-item'>";
                                divPriceDetails1.InnerHtml += "<a href='" + url + "' target='_blank'>";
                                divPriceDetails1.InnerHtml += "<div class='rcs-dest-title'>";
                                divPriceDetails1.InnerHtml += "<span class='rcs-title'><i class='fa fa-plane'></i> " + dr["depppt"] + " to " + dr["dessst"] + "</span>";
                                divPriceDetails1.InnerHtml += "</div>";


                                DateTime Dep_Date = DateTime.ParseExact(dr["DepDate"].ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);




                                if (dr["Jtype"].ToString() == "1")
                                {
                                    divPriceDetails1.InnerHtml += "<div class='rcs-dest-date'>" + Dep_Date.ToString("ddd, dd MMM yyyy") + "</div>";

                                }
                                else
                                {
                                    DateTime Arr_Date = DateTime.ParseExact(dr["ArrDate"].ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                                    divPriceDetails1.InnerHtml += "<div class='rcs-dest-date'>" + Dep_Date.ToString("ddd, dd MMM yyyy") + " - " + Arr_Date.ToString("ddd, dd MMM yyyy") + "</div>";
                                }
                                string Jtype = "";

                                if (dr["Jtype"].ToString() == "2")
                                {
                                    Jtype = "Round Trip";
                                }
                                else if (dr["Jtype"].ToString() == "1")
                                {
                                    Jtype = "One Way";
                                }

                                int pax = 0;
                                string spax = "Travellers";

                                if (dr["Adults"].ToString() != "")
                                {
                                    pax += Convert.ToInt32(dr["Adults"].ToString());
                                }
                                if (dr["Children"].ToString() != "")
                                {
                                    pax += Convert.ToInt32(dr["Children"].ToString());
                                }
                                if (dr["Infants"].ToString() != "")
                                {
                                    pax += Convert.ToInt32(dr["Infants"].ToString());
                                }

                                if (pax == 1)
                                {
                                    spax = "Traveller";
                                }

                                divPriceDetails1.InnerHtml += "<div class='rcs-traveler-type'>" + Jtype + ", " + pax + " " + spax + "</div>";




                                divPriceDetails1.InnerHtml += "<div class='rcs-footer'>";
                                divPriceDetails1.InnerHtml += "<div class='rcs-airline'>";
                                if (dr["Airlines"] != "")
                                {
                                    string airline_name = getAirline(dr["Airlines"].ToString());
                                    divPriceDetails1.InnerHtml += "<img src='Design/images/airline-logos/" + dr["Airlines"] + ".gif' alt='" + airline_name + "'>";
                                    divPriceDetails1.InnerHtml += "<p>" + airline_name + "</p>";
                                }
                                divPriceDetails1.InnerHtml += "</div>";


                                divPriceDetails1.InnerHtml += "<div class='rcs-price'> <h4>" + string.Format("{0:0.00}", Convert.ToDouble(dr["TtFare"])) + "<span>USD</span></h4></div>";
                                divPriceDetails1.InnerHtml += "</div>";
                                divPriceDetails1.InnerHtml += "</div>";
                                divPriceDetails1.InnerHtml += "</a>";

                            }
                            cnt_res++;
                        }


                        divPriceDetails1.InnerHtml += "</div>";

                        if (dt.Rows.Count > 3)
                        {
                            divPriceDetails1.InnerHtml += "<div class='show-more'>";
                            divPriceDetails1.InnerHtml += "<a href='#'>Show more</a>";
                            divPriceDetails1.InnerHtml += "</div>";

                        }
                        divPriceDetails1.InnerHtml += "</div>";


                    }
                }


            }
        }
        catch
        {
            divPriceDetails1.InnerHtml = "";
        }


        divPriceDetails1.InnerHtml += "<div class='faresummery-title'>" +
                                            "<h4>Price Details (USD)</h4>" +
                                        "</div>" +
                                        "<div class='faresummery-body'>" +
                                            "<div class='faresummery-seg-wrap'>" +
                                            "<div class='price-details'>" +
                                                "<span class='pull-left'>Trip Amount</span> <span class='pull-right totalprice' id=''>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmountCalc)) + " <small>USD</small></span>" +
                                            "</div>";

        //divPriceDetails1.InnerHtml += "<div class='price-details green'>";
        //                                        //divPriceDetails1.InnerHtml += "<span class='pull-left'>24 Hours Cancellation Fee</span>";
        //if (Session["ExtendedCancellation" + ssid] != null)
        //    divPriceDetails1.InnerHtml += "  <span class='pull-left' id='dPD'>24 Hours Cancellation Fee - Opted</span><span class='pull-right ' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
        //else
        //    divPriceDetails1.InnerHtml += " <span class='pull-left' id='dPD'>24 Hours Cancellation Fee - Not Opted</span><span class='pull-right ' id='CancellationFee1'> 0.00 <small>USD</small></span>";

        //divPriceDetails1.InnerHtml += "</div>" +



        //divPriceDetails1.InnerHtml += "<span class='pull-left'>24 Hours Cancellation Fee</span>";
        if (Session["ExtendedCancellation" + ssid] != null)
        {
            divPriceDetails1.InnerHtml += "<div class='price-details green' id='dPD' runat='server' style='display:'>";
            divPriceDetails1.InnerHtml += "  <span class='pull-left green'>Lower Price Finder and </br>24 Hours Cancellation Fee </span><span class='pull-right dicPrice' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
            divPriceDetails1.InnerHtml += "</div>";
        }
        else
        {

            divPriceDetails1.InnerHtml += "<div class='price-details green' id='dPD' runat='server' style='display:none'>";
            divPriceDetails1.InnerHtml += "  <span class='pull-left green'>Lower Price Finder and </br>24 Hours Cancellation Fee </span><span class='pull-right dicPrice' id='CancellationFee1'>0.00<small>USD</small></span>";
            divPriceDetails1.InnerHtml += "</div>";
        
        }





        divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                                                "<span class='pull-left dicPrice'>Promocode</span> ";

        if (Session["VoucherAmount" + ssid] != null)
            divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
        else
            divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";
        //divPriceDetails1.InnerHtml += "</div>" +

        //                                    "<div class='price-details green'>" +
        //                                        "<span class='pull-left'>CSA Travel Protection</span> ";
        //if (Session["InsuranceAmount" + ssid] != null)
        //    divPriceDetails1.InnerHtml += "<span class='pull-right' id='Insurance1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["InsuranceAmount" + ssid].ToString())) + " <small>USD</small></span>";
        //else
        //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='Insurance1'> 0.00 <small>USD</small></span>";

        divPriceDetails1.InnerHtml += "</div>" +
                                        "</div>" +
                                        "<div class='faresummery-seg-wrap promoBg'>" +
                                            "<label for='Promo Code'>Promo Code:</label>";

        if (Session["VoucherCode" + ssid] != null)
            divPriceDetails1.InnerHtml += "<span id='Voucher_Ihint' class='red'>Voucher is valid for " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " USD</span>";
        else
            divPriceDetails1.InnerHtml += "<span id='Voucher_Ihint' class='red'></span>";

        divPriceDetails1.InnerHtml += "<div id='promo-code-input'>" +
                                                "<div class='input-group col-md-12'>";
        if (Session["VoucherCode" + ssid] != null)
            divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id' id='Voucher_Id'>";
        else
            divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id' id='Voucher_Id'>";

        divPriceDetails1.InnerHtml += "<span class='input-group-btn'>" +
                                                        "<button class='btn btn-lg' type='button' id='btn_Voucher1' onclick='ValidateVoucher1()'>Apply</button>" +
                                                    "</span>" +
                                                "</div>" +
                                            "</div>" +
                                            "<div class='faresummery-seg-wrap'>" +
                                                "<div class='totalAmount' id='totalAmount2'>" +
                                                    "<p>Total Price:</p>" +
                                                    "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></h3>" +
                                                    "<p>incl. all taxes and fees</p>" +
                                                "</div>" +
                                                "<button type='button' id='btnbooknow1' class='pm-payment-btn btn' onclick='return btn_Submit();'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                                          // "<p class='blue text-center mt-10'> Ref : " + dtAAFares.Rows[0]["search_fares_id"].ToString() + "</p>" +
                                          

                                                "<span class='tcktleft' id='visitors'></button>" +
                                            

                                            "</div>" +
                                        "</div>" +
                                    "</div>";
    }
    // 2

    private string getTrasitVisaDetails(DataTable dtAAFares, DataTable dtAAFlights)
    {
        string URL = "http://www.timaticweb2.com/integration/external?ref=699d6f2fe8d16c221031d4c46c58695b";
        try
        {
            string departure = dtAAFlights.Rows[0]["DEP_ARP"].ToString();
            string destination = dtAAFlights.Rows[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString();

            string dept_date = dtAAFlights.Rows[0]["DEP_DAT"].ToString();
            string arr_date = dtAAFlights.Rows[dtAAFlights.Rows.Count - 1]["ARR_DAT"].ToString();

            string Airline = dtAAFares.Rows[0]["AirCode"].ToString();
            Session["Airline" + ssid] = Airline;
            Session["DEP_ARP" + ssid] = drAAFlightsOut[0]["DEP_ARP"].ToString();
            Session["ARR_ARP" + ssid] = drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString();



            

            string departure_country = getCountryCode(drAAFlightsOut[0]["DEP_ARP"].ToString());
            string destination_country = getCountryCode(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString());

            deptCountry = departure_country;
            destCountry = destination_country;
            DateTime depDate = DateTime.ParseExact(dept_date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime arrDate = DateTime.ParseExact(arr_date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = arrDate.Subtract(depDate);


            string duration_type = "days";
            string purpose_of_stay = "vacation";

            URL += "&rule=/dc=" + destination_country + "";
            URL += "/dec=" + destination + "";
            URL += "/dcc=" + departure_country + "";
            URL += "/dac=" + departure + "";
            URL += "/ad=" + arrDate.ToString("yyyy-MM-dd") + "";
            URL += "/sd=" + ts.Days + "";
            URL += "/sdt=" + duration_type + "";
            URL += "/st=" + purpose_of_stay + "";
            URL += "/ac=" + Airline + "";


            Hashtable htAirport = new Hashtable();
            int tcCount = 0;
            for (int i = 0; i < dtAAFlights.Rows.Count; i++)
            {
                if (!htAirport.Contains(dtAAFlights.Rows[i]["ARR_ARP"].ToString()) && dtAAFlights.Rows[i]["ARR_ARP"].ToString() != departure && dtAAFlights.Rows[i]["ARR_ARP"].ToString() != destination)
                {
                    string tcCountry = getCountryCode(dtAAFlights.Rows[i]["ARR_ARP"].ToString());

                    if (tcCountry != departure_country && tcCountry != destination_country)
                    {
                        htAirport.Add(dtAAFlights.Rows[i]["ARR_ARP"].ToString(), string.Empty);

                        URL += "/tc" + (tcCount + 1) + "=" + tcCountry;

                        DateTime tcdepDate = DateTime.ParseExact(dtAAFlights.Rows[i + 1]["DEP_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime tcarrDate = DateTime.ParseExact(dtAAFlights.Rows[i]["ARR_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);

                        URL += "/tcad" + (tcCount + 1) + "=" + tcarrDate.ToString("yyyy-MM-dd");
                        URL += "/tcdd" + (tcCount + 1) + "=" + tcdepDate.ToString("yyyy-MM-dd");

                        tcCount++;
                    }

                }

            }





        }
        catch
        {
            URL = "http://www.timaticweb2.com/integration/external?ref=699d6f2fe8d16c221031d4c46c58695b";
        }
        return URL;
    }

    private string getCountryCode(string air)
    {
        SqlParameter[] parameters = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("select_tblCityCodes", parameters, dbAccess.connString);
        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["CountryCode"].ToString();

        }
        else
        {
            return air;

        }
    }


    private string getCountryCode1(string city, string Country)
    {
        SqlParameter[] parameters = { new SqlParameter("@city", city), new SqlParameter("@country", Country) };
        DataTable dtAirCode = DataLayer.GetData("Get_CountryCode_1", parameters, ConfigurationManager.AppSettings["sqlcn_ip"].ToString());
        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["Country_Code"].ToString();
        }
        else
        {
            return city;
        }
    }
    private string getCountryCode2(string Country)
    {
        SqlParameter[] parameters = { new SqlParameter("@country", Country) };
        DataTable dtAirCode = DataLayer.GetData("Get_CountryCode_2", parameters, ConfigurationManager.AppSettings["sqlcn_ip"].ToString());
        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["Country_Code"].ToString();
        }
        else
        {
            return city;
        }
    }


    // csa state restriction 
    
    private DataTable getcityname(string CityCode)
    {
        SqlParameter[] parameters_city = { new SqlParameter("@CityCode", CityCode) };
        DataTable dtAirCode_city = DataLayer.GetData("tblCityCodes_get_by_airport_Csa_Restriction", parameters_city, ConfigurationManager.AppSettings["sqlcn"].ToString());
        //if (dtAirCode_city.Rows.Count > 0)
        //{
        //    return dtAirCode_city;
        //}
        return dtAirCode_city;
    }





    private string getHoursMinutes(string p)
    {
        string time = "";
        int Hours = Convert.ToInt32(p) / 60;
        int minutes = Convert.ToInt32(p) % 60;

        time = Hours.ToString().PadLeft(2, '0') + "h " + minutes.ToString().PadLeft(2, '0') + "m";
        return time;
    }


    private bool isInterNationalCheck(string p, string p_2)
    {
        bool flag = false;
        try
        {
            SqlParameter[] paramDept = { new SqlParameter("@AirportCode", p) };
            DataTable dtDeptAirportCode = DataLayer.GetData("tblDisplayData_selection", paramDept, dbAccess.connString);

            SqlParameter[] paramArr = { new SqlParameter("@AirportCode", p_2) };
            DataTable dtArrAirportCode = DataLayer.GetData("tblDisplayData_selection", paramArr, dbAccess.connString);
            if (dtArrAirportCode != null && dtDeptAirportCode != null)
            {
                if (dtDeptAirportCode.Rows.Count > 0 && dtArrAirportCode.Rows.Count > 0)
                {
                    if (dtDeptAirportCode.Rows[0]["Country"].ToString() != dtArrAirportCode.Rows[0]["Country"].ToString())
                    {
                        flag = true;

                    }
                }

            }

        }
        catch
        {

        }
        return flag;
    }

    private string getEquipmentType(string p)
    {
        return "";
    }

    public string getGender(string title)
    {
        string gender = "";

        switch (title)
        {
            case "Mr":
                gender = "M";
                break;
            case "Mrs":
                gender = "F";
                break;
            case "Miss":
                gender = "F";
                break;
            case "Ms":
                gender = "F";
                break;
            case "Mstr":
                gender = "M";
                break;
        }

        return gender;
    }


    private string getCabinClass(string p)
    {
        string cabin = "";
        if (p.ToUpper() == "F")
        {
            cabin = "First Class";
        }
        else if (p.ToUpper() == "C")
        {
            cabin = "Business";
        }
        else if (p.ToUpper() == "Y")
        {
            cabin = "Economy/Coach";
        }
        else if (p.ToUpper() == "J")
        {
            cabin = "Premium Business";
        }
        else if (p.ToUpper() == "P")
        {
            cabin = "Premium First";
        }
        else if (p.ToUpper() == "S")
        {
            cabin = "Premium Economy";
        }
        return cabin;
    }





    protected string getAirport(string air, out string country)
    {
        //DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblDisplayData where AirportCode='" + air + "'", dbAccess.connString);

        SqlParameter[] param = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", param, dbAccess.connString);

        if (dtAirCode.Rows.Count > 0)
        {
            country = dtAirCode.Rows[0]["Country"].ToString();
            return dtAirCode.Rows[0]["City"].ToString();
            // return "" + dtAirCode.Rows[0]["Airport"];
        }
        else
        {
            country = dtAirCode.Rows[0]["Country"].ToString();
            return air;

        }
    }

    public static string getAirportCity(string air)
    {
        // DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblDisplayData where AirportCode='" + air + "'", dbAccess.connString);

        SqlParameter[] param = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", param, dbAccess.connString);


        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["City"].ToString();

        }
        else
        {
            return air;
        }
    }
    protected string getAirportCountry(string air)
    {
        // DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblDisplayData where AirportCode='" + air + "'", dbAccess.connString);

        SqlParameter[] param = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", param, dbAccess.connString);


        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["Country"].ToString();
            // return "" + dtAirCode.Rows[0]["Airport"];
        }
        else
        {
            return air;

        }
    }

    protected string getAirline(string air)
    {
        if (air.ToUpper() != "FR")
        {
            SqlParameter[] parameters = { new SqlParameter("@AirlineCode", air) };
            DataTable dtAirCode = DataLayer.GetData("tblAirlineCodes_selection", parameters, dbAccess.connString);


            //  DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblAirlineCodes where AirlineCode='" + air + "'", dbAccess.connString);
            if (dtAirCode != null)
            {
                if (dtAirCode.Rows.Count > 0)
                {
                    return dtAirCode.Rows[0]["AirlineShortName"].ToString();
                }
                else
                {
                    return air;

                }
            }
            else
            {
                return air;
            }
        }
        else
        {
            return "Low Cost Carrier";

        }
    }


    private string getAirlinimage(string air)
    {
        if (air == "FR")
        {
            return "LCC.gif";
        }
        else
        {
            return "" + air + ".gif";
        }
    }


    private string getAirCode(string air)
    {
        if (air == "FR")
        {
            return "LCC";
        }
        else
        {
            return air;
        }
    }

    private string getEquipmentText(string p)
    {
        string equ_text = p;

        try
        {
            SqlParameter[] Param = { new SqlParameter("@IATA", p) };
            DataTable dt = DataLayer.GetData("Equipment_select", Param, ConfigurationManager.AppSettings["save_search_data"].ToString());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    equ_text = dt.Rows[0]["Manufacturer"].ToString() + " - " + dt.Rows[0]["Type_Model"].ToString();
                }
            }
        }
        catch
        {

        }
        return equ_text;
    }

    public string getFlexible(string flexi, string jtype)
    {
        if (jtype == "2")
        {
            string[] splitFlexi = flexi.Split('#');
            if (splitFlexi[0] == "0" && splitFlexi[1] == "0")
            {
                return "Exact";
            }
            else
            {
                return "Flexible";
            }
        }
        else
        {
            if (flexi == "0")
            {
                return "Exact";
            }
            else
            {

                return "Flexible";
            }


        }

    }



    protected string GetMonth(string strMonth)
    {
        string dateMonth = "";
        if (strMonth == "JAN")
        {
            dateMonth = "1";
        }
        else if (strMonth == "FEB")
        {
            dateMonth = "2";
        }
        else if (strMonth == "MAR")
        {
            dateMonth = "3";
        }
        else if (strMonth == "APR")
        {
            dateMonth = "4";
        }
        else if (strMonth == "MAY")
        {
            dateMonth = "5";
        }
        else if (strMonth == "JUN")
        {
            dateMonth = "6";
        }
        else if (strMonth == "JUL")
        {
            dateMonth = "7";
        }
        else if (strMonth == "AUG")
        {
            dateMonth = "8";
        }
        else if (strMonth == "SEP")
        {
            dateMonth = "9";
        }
        else if (strMonth == "OCT")
        {
            dateMonth = "10";
        }
        else if (strMonth == "NOV")
        {
            dateMonth = "11";
        }
        else if (strMonth == "DEC")
        {
            dateMonth = "12";
        }
        else
        {
            dateMonth = "0";
        }
        return dateMonth;
    }
    protected string GetFullDate(int dateDay, int dateMonth)
    {
        int preDateDay = Convert.ToInt32(DateTime.Now.Day);
        int preDateMonth = Convert.ToInt32(DateTime.Now.Month);
        int preDateYear = Convert.ToInt32(DateTime.Now.Year);
        string fullDate = "";
        if (dateMonth == preDateMonth)
        {
            if (dateDay < preDateDay)
            {
                fullDate = dateMonth.ToString() + "/" + dateDay.ToString() + "/" + (preDateYear + 1).ToString();
            }
            else
            {
                fullDate = dateMonth.ToString() + "/" + dateDay.ToString() + "/" + preDateYear.ToString();
            }
        }
        if (dateMonth > preDateMonth)
        {
            fullDate = dateMonth.ToString() + "/" + dateDay.ToString() + "/" + preDateYear.ToString();
        }
        if (dateMonth < preDateMonth)
        {
            fullDate = dateMonth.ToString() + "/" + dateDay.ToString() + "/" + (preDateYear + 1).ToString();
        }
        return fullDate;
    }
    protected string GetMonthString(string strMonth)
    {
        string dateMonth = "";
        if (strMonth == "01")
        {
            dateMonth = "JAN";
        }
        else if (strMonth == "02")
        {
            dateMonth = "FEB";
        }
        else if (strMonth == "03")
        {
            dateMonth = "MAR";
        }
        else if (strMonth == "04")
        {
            dateMonth = "APR";
        }
        else if (strMonth == "05")
        {
            dateMonth = "MAY";
        }
        else if (strMonth == "06")
        {
            dateMonth = "JUN";
        }
        else if (strMonth == "07")
        {
            dateMonth = "JUL";
        }
        else if (strMonth == "08")
        {
            dateMonth = "AUG";
        }
        else if (strMonth == "09")
        {
            dateMonth = "SEP";
        }
        else if (strMonth == "10")
        {
            dateMonth = "OCT";
        }
        else if (strMonth == "11")
        {
            dateMonth = "NOV";
        }
        else if (strMonth == "12")
        {
            dateMonth = "DEC";
        }
        else
        {
            dateMonth = "";
        }
        return dateMonth;
    }


    private int CalculateAge(string DateOfBirth)
    {
        int age = 0;
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime PaxDob = Convert.ToDateTime(DateOfBirth);

            string limitDate = dtAAFlights.Rows[dtAAFlights.Rows.Count - 1]["ARR_DAT"].ToString();
            DateTime now = DateTime.Parse(limitDate.Substring(0, 2) + "/" + limitDate.Substring(2, 2) + "/20" + limitDate.Substring(4, 2), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));


            age = now.Year - PaxDob.Year;

            if (now.Month >= PaxDob.Month)
            {
                if (now.Month == PaxDob.Month)
                {
                    if (now.Day >= PaxDob.Day)
                    {
                        age = age + 1;
                    }

                }
                else
                {
                    age = age + 1;
                }
            }
        }
        catch { }
        return age;
    }

    private string checkAdultAge(string DOB, int paxID)
    {
        string result = "";
        try
        {
            DateTime adultDOB = DateTime.Parse(DOB, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            int age = CalculateAge(DOB);
            if (age <= 11)

                result += "Adults age should be greater than 11 years( adults - " + paxID + " )";

        }
        catch
        {
            result += "Enter valid Date of birth of Adult-" + paxID;
        }

        return result;
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
                    ipcity = dt1.Rows[0]["City"].ToString();// + " (" + dt1.Rows[0]["AirportCode"].ToString() + ") : " + dt1.Rows[0]["Country"].ToString();
                    ipstate = dt1.Rows[0]["City"].ToString();// + " (" + dt1.Rows[0]["AirportCode"].ToString() + ") : " + dt1.Rows[0]["Country"].ToString();
                    ipCountry = dt1.Rows[0]["Country"].ToString();// + " (" + dt1.Rows[0]["AirportCode"].ToString() + ") : " + dt1.Rows[0]["Country"].ToString();
                }
            }
        }

        return city;
    }


    private string checkChildAge(string DOB, int paxID)
    {
        string result = "";
        try
        {
            DateTime childDOB = DateTime.Parse(DOB, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            int age = CalculateAge(DOB);
            if (age > 11 || age < 3)

                result += "Childs age should be greater than 2 years and less than 11 years ( child - " + (paxID - TotalAdults) + " )";

        }
        catch
        {
            result += "Enter valid Date of birth of Child-" + (paxID - TotalAdults);
        }

        return result;
    }


    private string checkInfantAge(string DOB, int paxID)
    {
        string result = "";
        try
        {
            DateTime infantDOB = DateTime.Parse(DOB, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            int age = CalculateAge(DOB);
            if (age > 2)

                result += "Infants age should be less than 2 years ( Infant - " + (paxID - TotalAdults - TotalChilds) + " )";


        }
        catch
        {
            result += "Enter valid Date of birth of Infant-" + (paxID - TotalAdults - TotalChilds);
        }
        return result;
    }



    private string CalculateDuration(string outduration, string induration)
    {
        string outHours = outduration.Substring(0, 2);
        string outMinutes = outduration.Substring(2, 2);

        string inHours = induration.Substring(0, 2);
        string inMinutes = induration.Substring(2, 2);


        int hours = 0;
        int minutes = 0;

        hours = Convert.ToInt32(outHours) + Convert.ToInt32(inHours);
        minutes = Convert.ToInt32(outMinutes) + Convert.ToInt32(inMinutes);

        if (minutes % 59 > 0)
        {
            minutes = minutes % 60;
            hours += 1;
        }

        return hours.ToString().PadLeft(2, '0') + minutes.ToString().PadLeft(2, '0');
    }
    private string CalculateDuration(string outduration)
    {

        if (outduration.Length < 4)
            outduration = "0" + outduration.ToString();
        string outHours = outduration.Substring(0, 2);
        string outMinutes = outduration.Substring(2, 2);

        int hours = 0;
        int minutes = 0;

        hours = Convert.ToInt32(outHours);
        minutes = Convert.ToInt32(outMinutes);

        if (minutes % 59 > 0)
        {
            minutes = minutes % 60;
        }

        return hours.ToString().PadLeft(2, '0') + "h " + minutes.ToString().PadLeft(2, '0') + "m";
    }

    protected string getIPCountryCode(string ipAddress)
    {
        string country = "-";
        try
        {
            string[] strIPSplit = ipAddress.Split('.');
            long ipNumber1 = Convert.ToInt64(strIPSplit[0]) * 256 * 256 * 256;
            long ipNumber2 = Convert.ToInt64(strIPSplit[1]) * 256 * 256;
            long ipNumber3 = Convert.ToInt64(strIPSplit[2]) * 256;
            long ipNumber4 = Convert.ToInt64(strIPSplit[3]);
            long ipNumber = ipNumber1 + ipNumber2 + ipNumber3 + ipNumber4;
            string qr = "SELECT country_code,country_name FROM ip_master where ip_from<=" + ipNumber + " and ip_to>=" + ipNumber;
            DataTable dt = dbAccess.GetSpecificData(qr, ConfigurationManager.AppSettings["sqlcn_ip"].ToString());
            if (dt.Rows.Count > 0)
            {
                //country = dt.Rows[0]["country_code"].ToString() + "#" + dt.Rows[0]["country_name"].ToString() + "#" + dt.Rows[0]["region"].ToString() + "#" + dt.Rows[0]["city"].ToString() + "#" + dt.Rows[0]["zipcode"].ToString();
                country = dt.Rows[0]["country_code"].ToString();
            }

        }
        catch
        {

        }
        return country;
    }

    protected string getIPCityCode(string ipAddress)
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
                //return dt.Rows[0]["country_code"].ToString();

            }
            else
            {
                city = "";
               // return "country_code";
            }
        }
        catch
        {
            city = "";
            return "";
        }

        return city;
    }




    // $50 popup 
    public bool paylater_limitation()
    {

        


        bool flag = false;

       DateTime depDate1 = Convert.ToDateTime(dtSearchData.Rows[0]["deptDate"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        DateTime arrDate1 = Convert.ToDateTime(dtSearchData.Rows[0]["retDate"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));


        string dt_dept = depDate1.ToString("yyyy-MM-dd");
        string dt_dest = arrDate1.ToString("yyyy-MM-dd");

        
        SqlParameter[] paylater_limit =
        {
            new SqlParameter("@dept",dtSearchData.Rows[0]["dept"].ToString()),
           new SqlParameter("@dest",dtSearchData.Rows[0]["dest"].ToString()),
           new SqlParameter("@pax", Convert.ToInt32(TotalPax)),
           new SqlParameter("@dept_country",""),
           new SqlParameter("@dest_country",""),
           new SqlParameter("@dept_date",dt_dept),
           new SqlParameter("@dest_date",dtAAFares.Rows[0]["arrDate"].ToString()),
           new SqlParameter("@amount",TtAmount),
           new SqlParameter("@faretype", ""),

           new SqlParameter("@airline",dtAAFares.Rows[0]["AirCode"].ToString() ),
           new SqlParameter("@markup",Convert.ToDouble(dtAAFares.Rows[0]["MarkUp"].ToString())),
           //new SqlParameter("@airline",dtAAFares.Rows[0]["AirCode"].ToString() ), depDate arrDate
           //new SqlParameter("@markup",Convert.ToDouble(dtAAFares.Rows[0]["MarkUp"].ToString())),
      };


        try
        {
             dt_paylater = DataLayer.GetData("get_paylater_limitations", paylater_limit, ConfigurationManager.AppSettings["sqlcn"].ToString());

           

            if (dt_paylater.Rows.Count > 0)
            {
                flag = true;
            }
        }catch(Exception ex) { }


        return flag; 
    }


}
