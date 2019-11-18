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
using System.IO;
using System.Net.Mail;
using System.Xml;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Serialization;

public partial class index : System.Web.UI.Page
{
    public string ssid = "";
    public string sid = "";
    public int labelCount = 1;
    string orderno = "";
    string PNR = "";
    string HotelRefId = "";
    public string cardCharges = "";
    public double TtAmountCalc = 0;
    private double TAmount = 0;
    bool isPnr = false;
    bool isBooking = false;
    bool isTravel = false;
    private double TtTax = 0;
    private double TtAmount = 0;
    private double ttam1 = 0;
    public string Cancel24 = "yes";
    public string Insu = "no";
    public static string timestamp = DateTime.UtcNow.ToString();
    public string cancel = "no";
    public string insurance = "no";
    public string Promo = "no";
    public string PromoAm = "0";
    public int TotalPax = 0;
    public string DeptDate = "";
    public string ArrDate = "";
    public int TotalAdults = 1;
    public int TotalChilds = 0;
    public int TotalInfants = 0;
    public string class1 = "";
    public bool paymentdone = false;

    public string Returndate_csa = "";
    public string departdate_csa = "";
    public double price_insurance = 0;
   public string csa_price_perpax = "";
    public string international_booking = "yes";

    public string productclass_type_domostic_intrnl = "";
   // public string productclass_domostic = "AIRD02";


    //TotalAdults TotalChilds TotalInfants adultfare childfare infantfare
    //sift Science Keys  START
    public static string siftsceince_URL = "https://api.siftscience.com/v203/events";
    //TEST
    //public static string JavaScriptKey = "7f43f4afc3";
    //public static string AppScriptKey = "054f8ed92b7a2cb2";


    //LIVE
    public static string JavaScriptKey = "50c4a37d9b";
    public static string AppScriptKey = "c6b3518fe62010e8";

    //sift Science Keys  END
    public string _sessionid = "";
    public string username = "";

    public double paylinkcancellation = 0;
    DataTable dtbookingpax = null;
    private void RedirectCS()
    {

    }



    encriptonanddecription enc = new encriptonanddecription();
    string gds_id = "";

    [System.Web.Services.WebMethod]
    public static void UpdateCancellation(string cancel, string am)
    {
        if (cancel == "yes")
        {
            //HttpContext.Current.Session["ExtendedCancellation"] = null;
            HttpContext.Current.Session["ExtendedCancellation"] = am;
            HttpContext.Current.Session["Extended"] = "yes";
        }
        else
        {
            HttpContext.Current.Session["ExtendedCancellation"] = null;
            HttpContext.Current.Session["Extended"] = "no";
        }

    }


    [System.Web.Services.WebMethod]
    public static void updateInsurance(string insurance, string am_ins)
    {

        string s = HttpContext.Current.Request.UserHostAddress.ToString();


        if (insurance == "yes")
        {
            //HttpContext.Current.Session["insurance"] = null;

            HttpContext.Current.Session["Insurance"] = am_ins;
            HttpContext.Current.Session["Ins"] = "yes";
        }
        else
        {
            HttpContext.Current.Session["Insurance"] = null;
            HttpContext.Current.Session["Ins"] = "no";
        }


        //if (HttpContext.Current.Session["Insurance" ] != null)
        //{

            
        //}
        //else
        //{
           
        //}




    }


    [System.Web.Services.WebMethod]
    public static void UpdatePromoCode(string id, string am, string status)
    {
        if (status == "yes")
        {
            HttpContext.Current.Session["VoucherAmount"] = am; 
            HttpContext.Current.Session["VoucherCode"] = id;
            HttpContext.Current.Session["Voucher"] = "yes";
        }
        else
        {
            HttpContext.Current.Session["VoucherAmount"] = null;
            HttpContext.Current.Session["VoucherCode"] = null;
            HttpContext.Current.Session["Voucher"] = "no";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Request.IsSecureConnection)
        //{
        //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
        //    Response.Redirect(redirectUrl);
        //    Response.End();
        //}
        bool valideReqNo = false;

        if (Request.QueryString["reqno"] != null)
        { 
            if (Request.QueryString["reqno"].ToString() != "")
            {
                valideReqNo = true;
            }
        }

        if (Request.QueryString["pnrno"] != null)
        {
            if (Request.QueryString["pnrno"].ToString() != "")
            {
                valideReqNo = true;
            }
        }

        if (valideReqNo == false)
        {
            divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                            "<p><i class='fa fa-chain-broken'></i></p>"+
                                            "<p>It seems to be the URL you have entered or using is no longer available or valid. Please call our support team to get a valid payment link to complete the payment for your booking.</p></br>" +
                                            "<h2><a href='tel:" + ContactDetails.Phone_support + "'>" + ContactDetails.Phone_support + "</a></h2>" +
                                        "</div>";
            divBookingId.Attributes.Add("style", "Display:none");
            divBookingDesc.Attributes.Add("style", "Display:none");
            divPassengerInfo.Attributes.Add("style", "Display:none");
            divItineraryInfo.Attributes.Add("style", "Display:none");
            divBaggageInfo.Attributes.Add("style", "Display:none");
            divPaymentDetails1.Attributes.Add("style", "Display:none");
            divPaymentDetails2.Attributes.Add("style", "Display:none");
            divPriceDetails.Attributes.Add("style", "Display:none");
            divPriceDetails1.Attributes.Add("style", "Display:none");
            Psdetailscheck.Attributes.Add("style", "Display:none");
            Cancelpolicycheck.Attributes.Add("style", "Display:none");
            insurance_csa.Attributes.Add("style", "Display:none");
            divBookingError.Attributes.Add("style", "Display:block");
            pnlPayment.Visible = true;
        }

        if (valideReqNo)
        {

            try
            {

                username = "";
                _sessionid = RandomPassword.Generate();
                Session["_sessionid"] = _sessionid;



                tmPNR.Value = "";
                tmHPNR.Value = "";
                string[] strPNRsplit = null;
                DataTable dtPay = GetSpecificData(enc.Decrypt(Request.QueryString["reqno"].ToString()), ConfigurationManager.ConnectionStrings["RConnectionString_new"].ToString());
                if(dtPay.Rows[0]["ins_status"].ToString()== "True")
                {
                    Insu = "yes";
                }
                else
                {
                    Insu = "no";
                }

                double paylinkHours = 0;
                if (dtPay.Rows[0]["PLink_DateTime"].ToString() != "")
                {
                    string departureDateTime = Convert.ToDateTime(dtPay.Rows[0]["PLink_DateTime"]).ToString();
                    paylinkHours = (Convert.ToDateTime(departureDateTime) - DateTime.Now).TotalHours;
                }
                if (paylinkHours <= 24)
                {
                    if (dtPay.Rows.Count > 0)
                    {
                        string booking_stat = "";
                        if (dtPay.Rows[0]["pnrno"].ToString() != "")
                        {
                            strPNRsplit = dtPay.Rows[0]["pnrno"].ToString().Split('-');
                            Session["spnr"] = dtPay.Rows[0]["pnrno"].ToString();
                            tmPNR.Value = dtPay.Rows[0]["pnrno"].ToString();
                            booking_stat = GetBookingstatus(strPNRsplit[0].ToString());
                        }
                        else
                        {
                            strPNRsplit = dtPay.Rows[0]["HotelRefID"].ToString().Split('-');
                            Session["shref"] = dtPay.Rows[0]["HotelRefID"].ToString();
                            tmHPNR.Value = dtPay.Rows[0]["HotelRefID"].ToString();
                            booking_stat = GetBookingstatus_Hotel(strPNRsplit[0].ToString());
                            HotelRefId = dtPay.Rows[0]["HotelRefID"].ToString();
                        }

                        string result = "";
                        if (booking_stat != "18")
                        {
                            bool isPnr = false;
                            bool isHref = false;


                            strPNRsplit = dtPay.Rows[0]["pnrno"].ToString().Split('-');

                            SqlParameter[] paramBooking = { new SqlParameter("@Booking_ID", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@PNR", dtPay.Rows[0]["pnrno"].ToString()) };
                            DataTable dtPNR_ID = DataLayer.GetData("select_booking_booking_pnr_PNRID", paramBooking, ConfigurationManager.AppSettings["sqlcn"].ToString());
                            if (dtPNR_ID != null)
                            {
                                if (dtPNR_ID.Rows.Count > 0)
                                {
                                    SqlParameter[] paramPNR = { new SqlParameter("@PNR_ID", dtPNR_ID.Rows[0]["PNR_ID"].ToString()) };
                                    DataTable dtPNR = DataLayer.GetData("select_PNR", paramPNR, ConfigurationManager.AppSettings["sqlcn"].ToString());

                                    if (dtPNR != null)
                                    {
                                        if (dtPNR.Rows.Count > 0)
                                        {

                                            if (dtPNR.Rows[0]["GDS"].ToString() != "")
                                            {
                                                string tempGDS = dtPNR.Rows[0]["GDS"].ToString();
                                                string[] splitGDS = tempGDS.Split('-');

                                                if (splitGDS.Length > 1)
                                                {
                                                    try
                                                    {
                                                        SqlParameter[] paramGDS = { new SqlParameter("@GDS_Name", splitGDS[0]), new SqlParameter("@Pseudo", splitGDS[1]) };
                                                        DataTable dtGDS = DataLayer.GetData("Api_GDS_ID_ByNameAndPseudo", paramGDS, ConfigurationManager.ConnectionStrings["RConnectionStringWS_wsdata"].ToString());

                                                        if (dtGDS != null)
                                                        {
                                                            if (dtGDS.Rows.Count > 0)
                                                            {
                                                                gds_id = dtGDS.Rows[0]["id"].ToString();
                                                            }
                                                        }

                                                    }
                                                    catch
                                                    {


                                                    }
                                                }
                                                else
                                                {
                                                    if (tempGDS == "SABRE")
                                                    {
                                                        gds_id = "104";
                                                    }
                                                    

                                                    else if (tempGDS == "WSPAN")
                                                    {
                                                        gds_id = "1";
                                                    }

                                                }

                                            }


                                        }
                                    }

                                }

                            }

                            //TotalAdults TotalChilds TotalInfants

                            SqlParameter[] paraBooking = { new SqlParameter("@Booking_ID", Convert.ToInt64(getBookingID(dtPay.Rows[0]["pnrno"].ToString()))) };
                            DataTable dtBooking = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "select_Booking", paraBooking);

                            if (dtBooking != null)
                            {
                                if (dtBooking.Rows.Count > 0)
                                {

// find booking type  Domestic or international 
                                    string dept_csa = dtBooking.Rows[0]["Dept"].ToString();

                                    SqlParameter[] csa_dept = { new SqlParameter("@param", dept_csa) };
                                    DataTable dept_country = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "get_Country_from_dept", csa_dept);


                                    string dest_csa = dtBooking.Rows[0]["Dest"].ToString();

                                    SqlParameter[] csa_dest = { new SqlParameter("@param", dest_csa) };
                                    DataTable dest_country = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "get_Country_from_dept", csa_dest);
                                    // Domostic
                                    string strDate_csa = DateTime.Now.ToString("MM/dd/yyyy");

                                    if (dept_country.Rows[0]["Country"].ToString() == dest_country.Rows[0]["Country"].ToString())
                                    {

                                        productclass_type_domostic_intrnl = "AIRD02";

                                        termsandCondition.InnerHtml= "<a href = 'https://www.csatravelprotection.com/certpolicy.do?product=G-AIRD02&appdate="+ strDate_csa + "' target = '_blank'> View description of coverage or policy for terms and conditions.</a>";




                                               booking_type.InnerHtml = "<ul>" +
                                             "<li class='includeIcon' >Trip Cancellation <span class='bspan'>100%</span>" + "<p>of trip cost insured*</p> </li>" +"<li>Travel Delay <span class='bspan'>$500*</span> <p>($100 per day)* per person</p></li>" + "<li>Trip Interruption <span class='bspan'>150%</span> <p>of trip cost insured*</p></li>" + "<li>Baggage Delay <span class='bspan'>$200*</span> <p>per person</p></li></ul>";


                                 additionalCoverageLink.InnerHtml = " <p class='limits-brd'></p> " +
                                 "<p>*Maximum benefits listed are per person.Plan limits also apply. </p> "+
                                       "<a href = 'https://www.csatravelprotection.com/certpolicy.do?product=G-AIRD02&appdate=" + strDate_csa + "' target='_blank' > See additional coverage included!</a>";


                                    }
           // international 
                                    else
                                    {

                                        productclass_type_domostic_intrnl = "AIRI02";
                                        termsandCondition.InnerHtml = "<a href = 'https://www.csatravelprotection.com/certpolicy.do?product=G-AIRI02&appdate=" + strDate_csa + "' target = '_blank'> View description of coverage or policy for terms and conditions.</a>";



                                        booking_type.InnerHtml = "<ul>" +
                                         "<li class='includeIcon' >Trip Cancellation <span class='bspan'>100%</span>" +
                                        "<p>of trip cost insured*</p> </li>" +
                                         "<li>Medical and Dental Coverage <span class='bspan'>$50,000*</span> </li>" +
                                         "<li>Travel Delay <span class='bspan'>$500*</span> <p>($100 per day)* per person</p></li>" +
                                         "<li>Trip Interruption <span class='bspan'>150%</span> <p>of trip cost insured*</p></li>" +
                                          "<li>Emergency Assistance <span class='bspan'>$25,000*</span></li>" +
                                         "<li>Baggage Delay <span class='bspan'>$200*</span> <p>per person</p></li></ul>";

                                        additionalCoverageLink.InnerHtml = " <p class='limits-brd'></p> " +
                                 "<p>*Maximum benefits listed are per person.Plan limits also apply. </p> " +
                                       "<a href = 'https://www.csatravelprotection.com/certpolicy.do?product=G-AIRI02&appdate=" + strDate_csa + "' target='_blank' > See additional coverage included!</a>";

                                    }


                                    DateTime Dt = DateTime.Now;
                                    IFormatProvider mFomatter = new System.Globalization.CultureInfo("en-US");

                                    Dt = Convert.ToDateTime(dtBooking.Rows[0]["Dept_date"].ToString());
                                    departdate_csa = Dt.ToString("yyyy-MM-dd");

                                    Dt = Convert.ToDateTime(dtBooking.Rows[0]["Return_date"].ToString());
                                    Returndate_csa = Dt.ToString("yyyy-MM-dd");

                                    if(Returndate_csa == "1900-01-01")
                                    {
                                        Returndate_csa = departdate_csa;
                                    }

                                    




                                    string bookStatus = dtBooking.Rows[0]["Booking_Type_Id"].ToString();
                                    if (bookStatus == "2" || bookStatus == "5")
                                    {
                                        
                                        Cancel24 = "no";
                                        Cancelpolicycheck.Attributes.Add("style", "Display:none");

                                        Insu = "no";
                                        insurance_csa.Attributes.Add("style", "Display:none");
                                    }
                                }
                            }

                            dtbookingpax = dbAccess.GetSpecificData("select * from Booking_Pax b left join PNR p on  b.PNR_ID=p.PNR_ID where p.PNR = '" + strPNRsplit[0] + "'", dbAccess.connString);
                            TotalAdults = Convert.ToInt32(dtbookingpax.Select("Pax_Type_ID = 3").Length);
                            TotalChilds = Convert.ToInt32(dtbookingpax.Select("Pax_Type_ID = 1").Length);
                            TotalInfants = Convert.ToInt32(dtbookingpax.Select("Pax_Type_ID = 2").Length);

                            TotalPax = TotalAdults + TotalChilds + TotalInfants;
                            Session["totalPax"] = TotalPax;
                            if (!IsPostBack)
                            {
                                SqlParameter[] parampromo = { new SqlParameter("@Booking_ID", getBookingID(dtPay.Rows[0]["pnrno"].ToString())) };
                                DataTable dtpromo = dbAccess.GetSpecificData_SP(dbAccess.connString, "select_tbVoucher_Voucherid_paymentlink", parampromo);

                                SqlParameter[] parampromo1 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "27") };
                                DataTable dtpromo1 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", parampromo1);




                                if (dtpromo != null)
                                {
                                    if (dtpromo.Rows.Count > 0)
                                    {
                                      
                                        HttpContext.Current.Session["VoucherAmount"] = String.Format("{0:0.00}", Convert.ToDouble(dtpromo.Rows[0]["Voucher_Amount"].ToString()));
                                        HttpContext.Current.Session["VoucherCode"] = dtpromo.Rows[0]["Voucher_Id"].ToString();
                                        HttpContext.Current.Session["Voucher"] = "yes";
                                        HttpContext.Current.Session["VoucherAmount_id" + ssid] = dtpromo1.Rows[0]["Booking_Product_id"].ToString();
                                    }
                                    else
                                    {
                                        HttpContext.Current.Session["VoucherAmount"] = null;
                                        HttpContext.Current.Session["VoucherCode"] = null;
                                        HttpContext.Current.Session["Voucher"] = "no";
                                        HttpContext.Current.Session["VoucherAmount_id" + ssid] = null;
                                    }
                                }
                            }
                            Countryhint.InnerHtml = "<select class='' onblur='val(this)' data-placement='bottom' data-msg-required='Please select the country of the billing address.' name='ddlCountry' id='ddlCountry' runat='server'>" +
                "<option value=''>Select country Name</option>";
                            DataTable dtCountry = DataLayer.GetData("tblDisplayData_Country_selection", dbAccess.connString);


                            if (dtCountry.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtCountry.Rows.Count; i++)
                                {
                                    Countryhint.InnerHtml += "<option value='" + dtCountry.Rows[i]["Country"].ToString() + "'>" + dtCountry.Rows[i]["Country"].ToString() + "</option>";
                                }
                            }

                             Countryhint.InnerHtml += "</select>";
                            if (!IsPostBack)
                            {
                                if (Session["Extended"] != null)
                                {
                                    if (Session["Extended"].ToString() != "no")
                                    {
                                        SqlParameter[] paramExtended = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "26") };
                                        DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);
                                        if (dtExtend != null)
                                        {
                                            if (dtExtend.Rows.Count > 0)
                                            {
                                                HttpContext.Current.Session["ExtendedCancellation"] = String.Format("{0:0.00}", Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                                                HttpContext.Current.Session["Extended"] = "yes";
                                                HttpContext.Current.Session["ExtendedCancellation_id" + ssid] = dtExtend.Rows[0]["Booking_Product_id"].ToString();

                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());

                                            }
                                            else
                                            {
                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        SqlParameter[] paramExtended = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "26") };
                                        DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);
                                        if (dtExtend != null)
                                        {
                                            if (dtExtend.Rows.Count > 0)
                                            {
                                                HttpContext.Current.Session["ExtendedCancellation"] = String.Format("{0:0.00}", Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                                                HttpContext.Current.Session["Extended"] = "yes";
                                                HttpContext.Current.Session["ExtendedCancellation_id" + ssid] = dtExtend.Rows[0]["Booking_Product_id"].ToString();

                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                            else
                                            {
                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    SqlParameter[] paramExtended = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "26") };
                                    DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);
                                    if (dtExtend != null)
                                    {
                                        if (dtExtend.Rows.Count > 0)
                                        {
                                            HttpContext.Current.Session["ExtendedCancellation"] = String.Format("{0:0.00}", Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                                            HttpContext.Current.Session["Extended"] = "yes";
                                            HttpContext.Current.Session["ExtendedCancellation_id" + ssid] = dtExtend.Rows[0]["Booking_Product_id"].ToString();

                                            TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                        }
                                        else
                                        {
                                            TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                    }
                                }
                            }

                            // csa 

                           // if ( Insu == "no")
                           // {


                                if (!IsPostBack)
                                {
                                    if (Session["Ins"] != null)
                                    {
                                        if (Session["Ins"].ToString() != "no")
                                        {
                                            SqlParameter[] paramExtended3 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "28") };
                                            DataTable dtExtend3 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended3);
                                            if (dtExtend3 != null)
                                            {
                                                if (dtExtend3.Rows.Count > 0)
                                                {
                                                    HttpContext.Current.Session["Insurance"] = String.Format("{0:0.00}", Convert.ToDouble(dtExtend3.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend3.Rows[0]["Units"].ToString()));
                                                    HttpContext.Current.Session["Ins"] = "yes";
                                                    HttpContext.Current.Session["Insurance_id" + ssid] = dtExtend3.Rows[0]["Booking_Product_id"].ToString();

                                                    TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());

                                                }
                                                else
                                                {
                                                    TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                                }
                                            }
                                            else
                                            {
                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            SqlParameter[] paramExtended3 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "28") };
                                            DataTable dtExtend3 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended3);
                                            if (dtExtend3 != null)
                                            {
                                                if (dtExtend3.Rows.Count > 0)
                                                {
                                                    // HttpContext.Current.Session["Insurance"] = String.Format("{0:0.00}", Convert.ToDouble(dtExtend3.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend3.Rows[0]["Units"].ToString()));
                                                    HttpContext.Current.Session["Ins"] = "yes";
                                                    //
                                                    HttpContext.Current.Session["Insurance_id" + ssid] = dtExtend3.Rows[0]["Booking_Product_id"].ToString();

                                                    TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                                }
                                                else
                                                {
                                                    TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                                }
                                            }
                                            else
                                            {
                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SqlParameter[] paramExtended3 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "28") };
                                        DataTable dtExtend3 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended3);
                                        if (dtExtend3 != null)
                                        {
                                            if (dtExtend3.Rows.Count > 0)
                                            {
                                                HttpContext.Current.Session["Insurance"] = String.Format("{0:0.00}", Convert.ToDouble(dtExtend3.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend3.Rows[0]["Units"].ToString()));
                                                HttpContext.Current.Session["Ins"] = "yes";
                                                HttpContext.Current.Session["Insurance_id" + ssid] = dtExtend3.Rows[0]["Booking_Product_id"].ToString();

                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                            else
                                            {
                                                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                                        }
                                    }
                                }


                           // }

                            DataTable dtAmount = GetSpecificDataAmount(enc.Decrypt(Request.QueryString["reqno"].ToString()), ConfigurationManager.AppSettings["sqlcn_pay"].ToString());
                            if (dtAmount.Rows.Count <= 0)
                            {
                                DataTable dtPNR = GetSpecificData(enc.Decrypt(Request.QueryString["reqno"].ToString()), ConfigurationManager.AppSettings["sqlcn_pay"].ToString());

                                if (dtPNR.Rows.Count > 0)
                                {
                                    divBookingId.Attributes.Add("style", "Display:block");
                                    divBookingDesc.Attributes.Add("style", "Display:block");
                                    divPassengerInfo.Attributes.Add("style", "Display:block");
                                    divItineraryInfo.Attributes.Add("style", "Display:block");
                                    divBaggageInfo.Attributes.Add("style", "Display:block");
                                    divPaymentDetails1.Attributes.Add("style", "Display:block");
                                    divPaymentDetails2.Attributes.Add("style", "Display:block");
                                    divPriceDetails.Attributes.Add("style", "Display:block");
                                    divPriceDetails1.Attributes.Add("style", "Display:block");
                                    Psdetailscheck.Attributes.Add("style", "Display:block");
                                    Cancelpolicycheck.Attributes.Add("style", "Display:block");

                                    if (Insu != "no")
                                    {
                                        insurance_csa.Attributes.Add("style", "Display:block");
                                    }
                                    else
                                    {
                                        insurance_csa.Attributes.Add("style", "Display:none");
                                    }
                                    divBookingError.Attributes.Add("style", "Display:none");
                                    if (dtPNR.Rows[0]["pnrno"] != null)
                                    {
                                        if (dtPNR.Rows[0]["pnrno"].ToString() != "")
                                        {
                                            tmPNR.Value = dtPNR.Rows[0]["pnrno"].ToString();
                                            PNR = dtPNR.Rows[0]["pnrno"].ToString();
                                        }
                                    }
                                    if (dtPNR.Rows[0]["HotelRefID"] != null)
                                    {
                                        if (dtPNR.Rows[0]["HotelRefID"].ToString() != "")
                                        {
                                            tmHPNR.Value = dtPNR.Rows[0]["HotelRefID"].ToString();
                                            HotelRefId = dtPNR.Rows[0]["HotelRefID"].ToString();
                                        }
                                    }

                                    if (Request.QueryString["errMsg"] != null)
                                    {
                                        divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                                                        "<div class='pm-error warning' style=''>" +
                                                                            "<ul>" +
                                                                                "<li class=''>" +
                                                                                    enc.Decrypt(Request.QueryString["errMsg"].ToString()) +
                                                                            "</li>" +
                                                                            "</ul>" +
                                                                        "</div>" +
                                                                    "</div>";
                                        divBookingError.Attributes.Add("style", "Display:block");
                                    }
                                    TAmount = Convert.ToDouble(dtPNR.Rows[0]["Amount"]);
                                    TtAmount = TAmount;
                                    TtTax = 0;

                                    if (Insu != "no") { 
                                        
 // added new                             

         
                                        response results_csa = addinsurance_csa.CSAInsurance(departdate_csa, Returndate_csa, Convert.ToString(TtAmount), productclass_type_domostic_intrnl, Convert.ToString(getBookingID(dtPay.Rows[0]["pnrno"].ToString())));

                                        //addinsurance_csa.SearchResXmlSave(results_csa,Convert.ToString( getBookingID(dtPay.Rows[0]["pnrno"].ToString()));



                                        if (results_csa.quoteresponse[0].price.ToString() != "")
                                        {
                                            price_insurance = Convert.ToDouble(results_csa.quoteresponse[0].price.ToString());
                                             csa_price_perpax= results_csa.quoteresponse[0].price.ToString();
                                            // HttpContext.Current.Session["Insurance"] = price_insurance;

                                        }
                                    }

                                }
                                else
                                {

                                    divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                                                    "<p><i class='fa fa-chain-broken'></i></p>" +
                                                                    "<p>It seems to be the URL you have entered or using is no longer available or valid. Please call our support team to get a valid payment link to complete the payment for your booking.</p></br>" +
                                                                    "<h2><a href='tel:" + ContactDetails.Phone_support + "'>" + ContactDetails.Phone_support + "</a></h2>" +
                                                                "</div>";
                                    divBookingId.Attributes.Add("style", "Display:none");
                                    divBookingDesc.Attributes.Add("style", "Display:none");
                                    divPassengerInfo.Attributes.Add("style", "Display:none");
                                    divItineraryInfo.Attributes.Add("style", "Display:none");
                                    divBaggageInfo.Attributes.Add("style", "Display:none");
                                    divPaymentDetails1.Attributes.Add("style", "Display:none");
                                    divPaymentDetails2.Attributes.Add("style", "Display:none");
                                    divPriceDetails.Attributes.Add("style", "Display:none");
                                    divPriceDetails1.Attributes.Add("style", "Display:none");
                                    Psdetailscheck.Attributes.Add("style", "Display:none");
                                    Cancelpolicycheck.Attributes.Add("style", "Display:none");
                                    divBookingError.Attributes.Add("style", "Display:block");
                                    pnlPayment.Visible = true;
                                }

                            }
                            else
                            {
                                DataTable dtPNR = GetSpecificData(enc.Decrypt(Request.QueryString["reqno"].ToString()), ConfigurationManager.AppSettings["sqlcn_pay"].ToString());
                                TAmount = Convert.ToDouble(dtPNR.Rows[0]["Amount"]);
                                TtAmount = TAmount;
                                TtTax = 0;
                                paymentdone = true;
                                divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                                                "<p>You have already made the payment for this <b>Booking Reference</b>. To know more, please call us</p>" +
                                                                "<h2><a href='tel:" + ContactDetails.Phone_support + "'>" + ContactDetails.Phone_support + "</a></h2>" +
                                                            "</div>";
                                divBookingId.Attributes.Add("style", "Display:block");
                                divBookingDesc.Attributes.Add("style", "Display:block");
                                divPassengerInfo.Attributes.Add("style", "Display:block");
                                divItineraryInfo.Attributes.Add("style", "Display:block");
                                divBaggageInfo.Attributes.Add("style", "Display:block");
                                divPaymentDetails1.Attributes.Add("style", "Display:none");
                                divPaymentDetails2.Attributes.Add("style", "Display:none");
                                divPriceDetails.Attributes.Add("style", "Display:none");
                                divPriceDetails1.Attributes.Add("style", "Display:none");
                                Psdetailscheck.Attributes.Add("style", "Display:none");
                                insurance_csa.Attributes.Add("style", "Display:none");
                                Cancelpolicycheck.Attributes.Add("style", "Display:none");
                                divBookingError.Attributes.Add("style", "Display:block");
                                pnlPayment.Visible = true;


                                Psdetailscheck.InnerHtml = "";
                                Cancelpolicycheck.InnerHtml = "";
                                divPriceDetails.InnerHtml = "";
                                divPaymentDetails1.Visible = false;
                                divPaymentDetails2.Visible = false;
                                divPriceDetails1.InnerHtml = "";
                            }
                            if (dtPay.Rows[0]["GDS"].ToString().Contains("SABRE"))
                            {
                                gds_id = "139";

                                result = getSabrePNR(strPNRsplit[0], gds_id);
                                DisplayData_1S_New(result, dtPay);
                                isPnr = true;
                            }
                            else if (dtPay.Rows[0]["GDS"].ToString().Contains("WSPAN"))
                            {
                                string response = get_WSPAN_PNR(strPNRsplit[0], gds_id);

                                DPW8 objDPW8 = DPW8_XML_Object.ResXml(response);

                                displayItinerary_1P(objDPW8, dtPay);

                                isPnr = true;

                            }
                            if (dtPay.Rows[0]["HGDS"].ToString() == "HB" || dtPay.Rows[0]["HGDS"].ToString() == "MH"
                                || dtPay.Rows[0]["HGDS"].ToString() == "LCB" || dtPay.Rows[0]["HGDS"].ToString() == "YT"
                                || dtPay.Rows[0]["HGDS"].ToString() == "HBK" || dtPay.Rows[0]["HGDS"].ToString() == "SUN"
                                || dtPay.Rows[0]["HGDS"].ToString() == "TF")
                            {
                                string q = "select * from HotelBooking where HotelRfID='" + dtPay.Rows[0]["HotelRefID"].ToString() + "' and GDS='" + dtPay.Rows[0]["HGDS"] + "' order by HotelBooking_ID desc";
                                DataTable dtHotelBooking = dbAccess.GetSpecificData(q, ConfigurationManager.AppSettings["sqlcn"].ToString());
                                if (dtHotelBooking.Rows.Count > 0)
                                {
                                    DisplayDataHotel(dtHotelBooking, dtPay);
                                }
                                isHref = true;
                            }
                        }
                        else
                        {
                            Response.Redirect("sessionexipred.aspx?errMsg=" + strPNRsplit[0].ToString() + "");
                        }
                    }
                    else
                    {
                        divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                                        "<p><i class='fa fa-chain-broken'></i></p>" +
                                                        "<p>It seems to be the URL you have entered or using is no longer available or valid. Please call our support team to get a valid payment link to complete the payment for your booking.</p></br>" +
                                                        "<h2><a href='tel:" + ContactDetails.Phone_support + "'>" + ContactDetails.Phone_support + "</a></h2>" +
                                                    "</div>";
                        divBookingId.Attributes.Add("style", "Display:none");
                        divBookingDesc.Attributes.Add("style", "Display:none");
                        divPassengerInfo.Attributes.Add("style", "Display:none");
                        divItineraryInfo.Attributes.Add("style", "Display:none");
                        divBaggageInfo.Attributes.Add("style", "Display:none");
                        divPaymentDetails1.Attributes.Add("style", "Display:none");
                        divPaymentDetails2.Attributes.Add("style", "Display:none");
                        divPriceDetails.Attributes.Add("style", "Display:none");
                        divPriceDetails1.Attributes.Add("style", "Display:none");
                        Psdetailscheck.Attributes.Add("style", "Display:none");
                        Cancelpolicycheck.Attributes.Add("style", "Display:none");
                        divBookingError.Attributes.Add("style", "Display:block");
                        pnlPayment.Visible = true;
                    }
                }
                else
                {
                    divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                                    "<p><i class='fa fa-chain-broken'></i></p>" +
                                                    "<p>It seems to be the URL you have entered or using is no longer available or valid. Please call our support team to get a valid payment link to complete the payment for your booking.</p></br>" +
                                                    "<h2><a href='tel:" + ContactDetails.Phone_support + "'>" + ContactDetails.Phone_support + "</a></h2>" +
                                                "</div>";
                    divBookingId.Attributes.Add("style", "Display:none");
                    divBookingDesc.Attributes.Add("style", "Display:none");
                    divPassengerInfo.Attributes.Add("style", "Display:none");
                    divItineraryInfo.Attributes.Add("style", "Display:none");
                    divBaggageInfo.Attributes.Add("style", "Display:none");
                    divPaymentDetails1.Attributes.Add("style", "Display:none");
                    divPaymentDetails2.Attributes.Add("style", "Display:none");
                    divPriceDetails.Attributes.Add("style", "Display:none");
                    divPriceDetails1.Attributes.Add("style", "Display:none");
                    Psdetailscheck.Attributes.Add("style", "Display:none");
                    insurance_csa.Attributes.Add("style", "Display:none");
                    Cancelpolicycheck.Attributes.Add("style", "Display:none");
                    divBookingError.Attributes.Add("style", "Display:block");
                    pnlPayment.Visible = true;
                }
                if (IsPostBack)
                {
                    btnBookNow();
                }

         }
            catch (Exception exNew)
            {
                Email_sender1.SendEmail("Display Itinerary error", exNew.Message.ToString() + "-" + exNew.StackTrace.ToString());
                divBookingError.InnerHtml = "<div class='cust-crus'>" +
                                                 "<p><i class='fa fa-chain-broken'></i></p>" +
                                                "<p>It seems to be the URL you have entered or using is no longer available or valid. Please call our support team to get a valid payment link to complete the payment for your booking.</p></br>" +
                                                 "<h2><a href='tel:" + ContactDetails.Phone_support + "'>" + ContactDetails.Phone_support + "</a></h2>" +
                                             "</div>";
                divBookingId.Attributes.Add("style", "Display:none");
                divBookingDesc.Attributes.Add("style", "Display:none");
                divPassengerInfo.Attributes.Add("style", "Display:none");
                divItineraryInfo.Attributes.Add("style", "Display:none");
                divBaggageInfo.Attributes.Add("style", "Display:none");
                divPaymentDetails1.Attributes.Add("style", "Display:none");
                divPaymentDetails2.Attributes.Add("style", "Display:none");
                divPriceDetails.Attributes.Add("style", "Display:none");
                divPriceDetails1.Attributes.Add("style", "Display:none");
                Psdetailscheck.Attributes.Add("style", "Display:none");
                Cancelpolicycheck.Attributes.Add("style", "Display:none");
                insurance_csa.Attributes.Add("style", "Display:none");
                divBookingError.Attributes.Add("style", "Display:block");
            }

        }
    }
    public int getMinutes(string s)
    {
        int minutes = 0;
        try
        {
            s = s.ToString().PadLeft(4, '0');
            minutes = Convert.ToInt32(s.Substring(0, 2)) * 60 + Convert.ToInt32(s.Substring(2, 2));
        }
        catch
        {

        }
        return minutes;
    }
    private string getHoursMinutes(string p)
    {
        string time = "";
        int Hours = Convert.ToInt32(p) / 60;
        int minutes = Convert.ToInt32(p) % 60;

        time = Hours.ToString().PadLeft(2, '0') + "h " + minutes.ToString().PadLeft(2, '0') + "m";
        return time;
    }

    //wspan
    public void displayItinerary_1P(DPW8 obj, DataTable dtPay)
    {
        try
        {
            //if (obj.AIR_SEG_INF != null)
            //{
            if (Session["accepts" + ssid] != null)
            {
                if (Session["accepts" + ssid].ToString() == "accepted")
                {
                    //Fare = Convert.ToDouble(Session["AcceptedFare" + ssid].ToString());
                }
            }

            DataTable tblitinerary = new DataTable();

            tblitinerary.Columns.Add("Segment_No", typeof(int));
            tblitinerary.Columns.Add("Leg_No", typeof(int));
            tblitinerary.Columns.Add("Airline", typeof(string));
            tblitinerary.Columns.Add("Flight_No", typeof(string));
            tblitinerary.Columns.Add("Codeshare_Airline", typeof(string));
            tblitinerary.Columns.Add("Class", typeof(string));
            tblitinerary.Columns.Add("Departure", typeof(string));
            tblitinerary.Columns.Add("Destination", typeof(string));
            tblitinerary.Columns.Add("Dept_date", typeof(string));
            tblitinerary.Columns.Add("Dept_time", typeof(string));
            tblitinerary.Columns.Add("Arrival_date", typeof(string));
            tblitinerary.Columns.Add("Arrival_time", typeof(string));
            tblitinerary.Columns.Add("Nextday", typeof(int));
            tblitinerary.Columns.Add("Equipment", typeof(string));
            tblitinerary.Columns.Add("Baggage", typeof(string));
            tblitinerary.Columns.Add("Dept_Terminal", typeof(string));
            tblitinerary.Columns.Add("Arrival_Terminal", typeof(string));
            tblitinerary.Columns.Add("Airline_PNR", typeof(string));
            tblitinerary.Columns.Add("StopOvers", typeof(string));
            tblitinerary.Columns.Add("MealTypes", typeof(string));
            tblitinerary.Columns.Add("ElapsedTime", typeof(string));









            DataTable tblBaggage = CreateBaggageTable();

            int TotalPaxCnt = 0;
            DataTable tblPax = new DataTable();
            tblPax.Columns.Add("Title", typeof(string));
            tblPax.Columns.Add("LastName", typeof(string));
            tblPax.Columns.Add("FirstName", typeof(string));
            tblPax.Columns.Add("PaxType", typeof(string));

            string PNR = dtPay.Rows[0]["pnrno"].ToString();

            string OutBoundOrigin = "";
            string OutBoundDestination = "";
            string InBoundOrigin = "";
            string InBoundDestination = "";

            string OutBoundOriginNew = "";
            string OutBoundDestinationNew = "";
            string InBoundOriginNew = "";
            string InBoundDestinationNew = "";




            SqlParameter[] paramBooking = { new SqlParameter("@Booking_ID", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@PNR", dtPay.Rows[0]["pnrno"].ToString()) };
            DataTable dtPNR_ID = DataLayer.GetData("select_booking_booking_pnr_PNRID", paramBooking, ConfigurationManager.AppSettings["sqlcn"].ToString());
            if (dtPNR_ID != null)
            {
                if (dtPNR_ID.Rows.Count > 0)
                {
                    SqlParameter[] paramPNR = { new SqlParameter("@PNR_ID", dtPNR_ID.Rows[0]["PNR_ID"].ToString()) };
                    DataTable dtPNR = DataLayer.GetData("select_PNR", paramPNR, ConfigurationManager.AppSettings["sqlcn"].ToString());

                    if (dtPNR != null)
                    {
                        if (dtPNR.Rows.Count > 0)
                        {

                            OutBoundOrigin = dtPNR.Rows[0]["Depature"].ToString();
                            OutBoundDestination = dtPNR.Rows[0]["Destination"].ToString();
                            InBoundOrigin = dtPNR.Rows[0]["Return_Depature_From"].ToString();
                            InBoundDestination = dtPNR.Rows[0]["Return_Depature_To"].ToString();

                            OutBoundOriginNew = dtPNR.Rows[0]["Depature"].ToString();
                            OutBoundDestinationNew = dtPNR.Rows[0]["Destination"].ToString();
                            InBoundOriginNew = dtPNR.Rows[0]["Return_Depature_From"].ToString();
                            InBoundDestinationNew = dtPNR.Rows[0]["Return_Depature_To"].ToString();


                        }
                    }

                }

            }


            if (obj != null)
            {

                //if (obj.REC_LOC != null)
                //{
                string StrBookingId = "";
                string StrBookingIdConfirm = "";

                SqlParameter[] paramExtended = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "26") };
                DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);
                ttam1 = TtAmount;
                double cancellation = 0;
                double Promotion = 0;
                double insurance_total = 0;
                if (dtExtend.Rows.Count > 0)
                {
                    if (Session["Extended"] != null)
                    {
                        

                        if (Session["Extended"].ToString() == "no")
                        {
                            //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                            // TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        }
                        else if (Session["Extended"].ToString() == "yes")
                        {
                            ttam1 = ttam1 + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                            cancellation = (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        }
                    }
                    else
                        Session["Extended"] = "no";
                }
                else
                {
                    if (Session["Extended"] != null)
                    {
                        if (Session["Extended"].ToString() == "yes")
                        {
                            ttam1 = ttam1 + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                            //TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                            cancellation = (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                        }
                        else if (Session["Extended"].ToString() == "no")
                        {
                            //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        }
                    }
                    else
                        Session["Extended"] = "no";
                }


                if (Insu == "yes")
                {

                    SqlParameter[] paramExtended2 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "28") };
                    DataTable dtExtend2 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended2);
                    ttam1 = TtAmount;
                    if (dtExtend2.Rows.Count > 0)
                    {
                        if (Session["Ins"] != null)
                        {


                            if (Session["Ins"].ToString() == "no")
                            {
                                //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                                //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend2.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend2.Rows[0]["Units"].ToString()));
                            }
                            else if (Session["Ins"].ToString() == "yes")
                            {
                                insurance_total = (Convert.ToDouble(dtExtend2.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend2.Rows[0]["Units"].ToString()));
                                ttam1 = ttam1 + (Convert.ToDouble(Session["Insurance"].ToString()));
                            }
                        }
                        else
                            Session["Ins"] = "no";
                    }
                    else
                    {
                        if (Session["Ins"] != null)
                        {
                            if (Session["Ins"].ToString() == "yes")
                            {
                                ttam1 = ttam1 + (Convert.ToDouble(Session["Insurance"].ToString()));
                                //TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["Insurance"].ToString()));
                                insurance_total = (Convert.ToDouble(Session["Insurance"].ToString()));
                            }
                            else if (Session["Ins"].ToString() == "no")
                            {
                                //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                            }
                        }
                        else
                            Session["Ins"] = "no";
                    }

                }
                

                if (Session["VoucherAmount" + ssid] != null)
                {
                    Promo = "yes";
                    ttam1 = ttam1 - (Convert.ToDouble(Session["VoucherAmount"].ToString()));
                    PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
                                                                                                                      //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
                    Promotion = Convert.ToDouble(PromoAm)*(-1);
                }


                if (Session["Extended"] != null)
                {
                    if (Session["Extended"].ToString() == "yes")
                        cancel = "yes";
                    else
                        cancel = "no";
                    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
                }
                if (Session["Ins"] != null)
                {
                    if (Session["Ins"].ToString() == "yes")
                        insurance = "yes";
                    else
                        insurance = "no";
                    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
                }


                StrBookingId += "<div class='pm-content'>" +
                                            "<div class='pm-left'>" +
                                                "<p class='bkid-icon'>" +
                                                    "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                    " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                                "</p>" +
                                            "</div>" +
                                            "<div class='pm-right pm-tl-fare'>" +
                                                "<p class='pm-tl-text'>Total price</p>" +
                                                    "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " <small>USD</small></h4>" +
                                                    "<p>* inc. all taxes and fees</p>" +
                                            "</div>" +
                                        "</div>";

                StrBookingIdConfirm += "<div class='confirm-head-Left'>" +
                                            "<h5>Check Your itinerary <small>(* All the times are local to Airports)</small></h5>" +
                                        "</div>" +
                                        "<div class='pull-right text-right'>" +
                                            "<p>Total price</p>" +
                                            "<div class='tplfare bold'>" + String.Format("{0:0.00}", ttam1) + " <small>USD</small></div>" +
                                            "<p>inc. all taxes and fees</p>" +
                                        "</div>";
                divBookingId.InnerHtml = "" + StrBookingId + "";
                Session["divBookingId" + ssid] = StrBookingId;
                Session["StrBookingIdConfirm" + ssid] = StrBookingIdConfirm;
                //}

                pricesumfoot.InnerHtml = "<div class='pm-content'>" +
                    "<div class='pm-left pm-footer-price'>" +
                    "<p class='pm-footer-tp'>Total price</p>" +
                    "<h4 id='totalAmount4'>" +
                    "<span>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " </span>" +
                    "<small>USD</small></h4>" +
                    "<p>* inc. all taxes and fees</p>" +
                    "</div>" +
                    "<div class='pm-right text-right pm-paynowbtn'>" +
                    "<span>" +
                    "<input type='submit' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
                    "</div>" +
                    "</div>";
                divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
                                               "<div class='pm-content'>" +
                                                   "<div class='pm-payDetails'>" +
                                                   "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
                                                       "Price Details (USD)" +
                                                       "<p>Confirm the price details of your round trip itinerary for all passengers.</p>" +
                                                   "</div>" +
                                               "</div>" +
                                           "</div>" +
                                           "<div class='pm-content'>" +

                                               "<div class='fareDetails'>" +
                                                   "<div class='fareDetailsBody'>" +
                                                       "<div class='fareDetailsLeft'>" +
                                                           "<div class='ccodeContent'>" +
                                                               "<label for='Promo Code'>Promo Code:</label>" +
                                                               "<span id='Voucher_Ihint1' class='red'></span>" +
                                                               "<div id='promo-code-input'>" +
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
                                                                    "<div class='fareQuoteLeft'>Total Amount</div> " +
                                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></div>" +
                                                                "</div>";
                //                                                "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Adults x " + TotalAdults + "</div>" +
                //                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>";
                //if (TotalChilds != 0)
                //{
                //    divPriceDetails.InnerHtml += "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Child x " + TotalChilds + "</div>" +
                //                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>";
                //}
                //if (TotalInfants != 0)
                //{
                //    divPriceDetails.InnerHtml += "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Infant x " + TotalInfants + "</div>" +
                //                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>";
                //}

                divPriceDetails.InnerHtml += "<div class='fareQuoted '>" +
                                                                    "<div class='fareQuoteLeft green'>Lower Price Finder and </br>24 Hours Cancellation Fee</div>";
                if (Session["ExtendedCancellation" + ssid] != null)
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div></div>";
                else
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='CancellationFee'> 0.00 <small>USD</small></div></div>";




                if (Insu == "yes")
                {

                    divPriceDetails.InnerHtml += "<div class='fareQuoted '>" +
                                                                        "<div class='fareQuoteLeft green'>CSA Travel Protection </div>";
                    if ( HttpContext.Current.Session["Insurance" + ssid] != null)
                    {
                        divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='InsuranceFee'> " + String.Format("{0:0.00}", Convert.ToDouble(HttpContext.Current.Session["Insurance" + ssid].ToString())) + " <small>USD</small></div></div>";
                    }
                    else
                    {
                        divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='InsuranceFee'> 0.00 <small>USD</small></div></div>";
                    }

                }



                divPriceDetails.InnerHtml +=  "<div class='fareQuoted offerCode'>" +
                                                                    "<div class='fareQuoteLeft '>Promocode</div>";
                if (Session["VoucherAmount" + ssid] != null)
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
                else
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";
                divPriceDetails.InnerHtml += "</div>" +
                                                                "<div class='fareQuoted offerCode'>" +
                                                                    "<div class='fareQuoteLeft '>Discount</div>" +
                                                                    "<div class='fareQuoteRight' id='Discount'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                        "</div>" +
                                                        "<div class='fareDetailsFooter'>" +
                                                            "<div class='tlprice'>" +
                                                                "<div class='tlpriceLeft'>Total</div>" +
                                                                "<div class='tlpriceRight' id='totalAmount'>" +
                                                                    "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation + Promotion + insurance_total)) + " USD</h4>" +
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

                string PaymentHeader = "<div class='pm-right pm-tl-fare'>" +
                                               "<p class='pm-tl-text'>Total price</p>" +
                                               "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " <small>USD</small></h4>" +
                                               "<p>* inc. all taxes and fees</p>" +
                                           "</div>";

                divPriceDetails1.InnerHtml = "<div class='faresummery-title'>" +
                                               "<h4>Price Details (USD)</h4>" +
                                           "</div>" +
                                           "<div class='faresummery-body'>" +
                                               "<div class='faresummery-seg-wrap'>" +
                                                   "<div class=''>" +
                                               "<div class='price-details'>" +
                                                    "<span class='pull-left'>Total Amount</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></span>" +
                                                "</div>";
                //                                       "<span class='pull-left'>Adults x " + TotalAdults + "</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(adultfare)) + " <small>USD</small></span>" +
                //                                   "</div>";
                //if (TotalChilds != 0)
                //{
                //    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                //                                            "<span class='pull-left'>Child x " + TotalChilds + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(childfare)) + " <small>USD</small></span>" +
                //                                        "</div>";
                //}

                //if (TotalInfants != 0)
                //{
                //    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                //                                            "<span class='pull-left'>Infant x " + TotalInfants + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(infantfare)) + " <small>USD</small></span>" +
                //                                        "</div>";
                //}

                divPriceDetails1.InnerHtml += "<div class='price-details green'>" +
                                                        "<span class='pull-left'>Lower Price Finder and </br>24 Hours Cancellation Fee</span>";
                if (Session["ExtendedCancellation" + ssid] != null)
                    divPriceDetails1.InnerHtml += " <span class='pull-right green' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span></div>";
                else
                    divPriceDetails1.InnerHtml += " <span class='pull-right green' id='CancellationFee1'> 0.00 <small>USD</small></span></div>";

                // csa

                if (Insu == "yes")
                {
                    divPriceDetails1.InnerHtml += "<div class='price-details '>" +
                                                                            "<span class='pull-left green'>CSA Travel Protection </span>";
                    if (Session["Insurance" + ssid] != null)
                        divPriceDetails1.InnerHtml += " <span class='pull-right green ' id='InsuranceFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["Insurance" + ssid].ToString())) + " <small>USD</small></span></div>";
                    else
                        divPriceDetails1.InnerHtml += " <span class='pull-right green' id='InsuranceFee1'> 0.00 <small>USD</small></span></div>";

                }

                divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                                                        "<span class='pull-left'>Discount</span> <span class='pull-right dicPrice'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></span>" +
                                                    "</div>" +
                                                    "<div class='price-details'>" +
                                                        "<span class='pull-left'>Promocode</span> ";
                if (Session["VoucherAmount" + ssid] != null)
                    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
                else
                    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";

                divPriceDetails1.InnerHtml += "</div>" +
                                                    "<div class='price-details'>" +
                                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' id='totalAmount1'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation + Promotion + insurance_total)) + " <small>USD</small></span>" +
                                                    "</div>" +
                                                "</div>" +
                                                "<div class='faresummery-seg-wrap promoBg'>" +
                                                    "<label for='Promo Code'>Promo Code:</label>" +
                                                    "<span id='Voucher_Ihint' class='red'></span>" +
                                                    "<div id='promo-code-input'>" +
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
                                                            "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation + Promotion + insurance_total)) + " <small>USD</small></h3>" +
                                                            "<p>incl. all taxes and fees</p>" +
                                                        "</div>" +
                                                        "<button type='submit' id='' class='pm-payment-btn btn btn-block' onclick='return btn_Submit(this);'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                                                    "<span class='tcktleft' id='visitors'></button>" +
                                                    "</div>" +
                                                "</div>" +
                                                "</div>" +
                                            "</div>";

                paymentHeader.InnerHtml = PaymentHeader;



                if (obj.PAX_INF != null)
                {
                    if (obj.PAX_INF.NME_ITM != null)
                    {
                        if (obj.PAX_INF.NME_ITM.Length > 0)
                        {





                            for (int paxcnt = 0; paxcnt < obj.PAX_INF.NME_ITM.Length; paxcnt++)
                            {


                                string paxname = obj.PAX_INF.NME_ITM[paxcnt].PAX_NME;
                                string[] splitPaxName = paxname.Split('/');
                                string[] splitPaxNameTitle = splitPaxName[1].Split('.');
                                DataRow drPax = tblPax.NewRow();
                                if (splitPaxNameTitle.Length > 1)
                                    drPax["Title"] = splitPaxNameTitle[splitPaxNameTitle.Length - 1];
                                else
                                    drPax["Title"] = "";


                                drPax["LastName"] = splitPaxName[0];
                                if (splitPaxNameTitle.Length > 2)
                                {
                                    drPax["FirstName"] = splitPaxNameTitle[0] + " " + splitPaxNameTitle[1];
                                }
                                else
                                {
                                    drPax["FirstName"] = splitPaxNameTitle[0];
                                }
                                drPax["PaxType"] = obj.PAX_INF.NME_ITM[paxcnt].PTC;
                                tblPax.Rows.Add(drPax);
                            }

                        }
                    }
                }

                //calculating Total pax, Adult, Child, Infant -start

                if (tblPax != null)
                {
                    if (tblPax.Rows.Count > 0)
                    {
                        TotalPaxCnt = tblPax.Rows.Count;
                    }
                }


                //divPassengerInfo -start

                string StrPassengerInfo = "";
                string StrPassengerInfoConfirm = "";

                if (tblPax != null)
                {
                    if (tblPax.Rows.Count > 0)
                    {
                        //StrPassengerInfo += "<div class='col-md-12'>";
                        //StrPassengerInfo += "<h4><i class='fa fa-user'></i> Traveler Details</h4>";
                        //StrPassengerInfo += "</div>";

                        StrPassengerInfo += "<div class='pm-head-title'>" +
                                               "<div class='pm-content'>" +
                                                    "<div class='travelerTitle faa-parent animated-hover'>" +
                                                        "<img src='Design/images/passenger-icon.png' alt='Passenger icon' class='title-icons faa-passing'> Traveller Details <P>(Title, Last Name, First Name, Passenger Type)</P>" +
                                                    "</div>" +
                                                "</div>" +
                                            "</div>" +
                                            "<div class='pm-content'>" +
                                                "<div class='pm-traveler-list'>" +
                                                    "<ul>";
                        StrPassengerInfoConfirm += "<ul>";
                        labelCount++;
                        int i = 1;
                        //if (dtbookingpax.Rows.Count <= 0)
                        //{
                        foreach (DataRow drtblPax in tblPax.Rows)
                        {
                            ////pnlpassenger.Controls.Add(new LiteralControl("<tr>"));
                            ////pnlpassenger.Controls.Add(new LiteralControl("<td height='20' align='left' valign='middle' style='text-transform: capitalize;'><table width='200px' height='15px'><tr><td width='20%'><img src='images/adult-icon.png' /></td><td width='80%'> " + drtblPax["LastName"].ToString().ToLower() + " " + drtblPax["FirstName"].ToString().ToLower() + "</td></tr></table></td>"));
                            ////pnlpassenger.Controls.Add(new LiteralControl("</tr>"));
                            //StrPassengerInfo += "<div class='col-md-4'>";
                            //StrPassengerInfo += "<p>" + i + "." + drtblPax["LastName"].ToString().ToLower() + " " + drtblPax["FirstName"].ToString().ToLower() + "</p>";
                            //StrPassengerInfo += "</div>";
                            //TotalAdults TotalChilds TotalInfants
                            string PaxType = "";
                            if (drtblPax["PaxType"].ToString().ToUpper() == "ADT")
                            {
                                PaxType = "(ADULT)";
                                TotalAdults++;
                            }
                            else if (drtblPax["PaxType"].ToString().ToUpper() == "JCB")
                            {
                                PaxType = "(ADULT)";
                                TotalAdults++;
                            }
                            else if (drtblPax["PaxType"].ToString().ToUpper() == "JNN")
                            {
                                PaxType = "(CHILD)";
                                TotalChilds++;
                            }
                            else if (drtblPax["PaxType"].ToString().ToUpper() == "CNN")
                            {
                                PaxType = "(CHILD)";
                                TotalChilds++;
                            }
                            else if (drtblPax["PaxType"].ToString().ToUpper() == "INF")
                            {
                                PaxType = "(INFANT WITHOUT SEAT)";
                                TotalInfants++;
                            }
                            else if (drtblPax["PaxType"].ToString().ToUpper() == "CNF")
                            {
                                PaxType = "(INFANT WITHOUT SEAT)";
                                TotalInfants++;
                            }
                            else
                            {
                                PaxType = drtblPax["title"].ToString().ToUpper();
                            }

                            StrPassengerInfo += "<li>" + i + ". " + drtblPax["title"].ToString().ToUpper() + " " + drtblPax["LastName"].ToString().ToUpper() + " " + drtblPax["FirstName"].ToString().ToUpper() + " " + PaxType + " </li>";
                            StrPassengerInfoConfirm += "<li>" + i + ". " + drtblPax["title"].ToString().ToUpper() + " " + drtblPax["LastName"].ToString().ToUpper() + " " + drtblPax["FirstName"].ToString().ToUpper() + " " + PaxType + " </li>";

                            i++;
                        }
                        //}
                        //else
                        //{
                        //    foreach (DataRow drtblPax in dtbookingpax.Rows)
                        //    {
                        //        string PaxType = "";
                        //        if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "1")
                        //            PaxType = "ADULT";
                        //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "1")
                        //            PaxType = "ADULT";
                        //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "2")
                        //            PaxType = "CHILD";
                        //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "2")
                        //            PaxType = "CHILD";
                        //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "3")
                        //            PaxType = "INFANT WITHOUT SEAT";
                        //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "3")
                        //            PaxType = "INFANT WITHOUT SEAT";
                        //        else
                        //            PaxType = drtblPax["title"].ToString().ToUpper();


                        //        StrPassengerInfo += "<li>" + i + ". " + drtblPax["Pax_Title"].ToString().ToUpper() + " " + drtblPax["Pax_Surname"].ToString().ToUpper() + " " + drtblPax["Pax_FirstName"].ToString().ToUpper() + " (" + PaxType + ") </li>";
                        //        StrPassengerInfoConfirm += "<li>" + i + ". " + drtblPax["Pax_Title"].ToString().ToUpper() + " " + drtblPax["Pax_Surname"].ToString().ToUpper() + " " + drtblPax["Pax_FirstName"].ToString().ToUpper() + " (" + PaxType + ") </li>";
                        //        i++;
                        //    }
                        //}
                        StrPassengerInfo += "</ul>" +
                                                    "</div>" +
                                                "</div>";
                        StrPassengerInfoConfirm += "</ul>";

                        divPassengerInfo.InnerHtml = "" + StrPassengerInfo + "";
                        //divPassengerInfo.Attributes.CssStyle.Add("display", "block");
                        Session["divPassengerInfo" + ssid] = StrPassengerInfo;
                        Session["StrPassengerInfoConfirm" + ssid] = StrPassengerInfoConfirm;
                    }
                    else
                    {
                        divPassengerInfo.Attributes.Add("style", "Display:none");
                        //divBookingError.Attributes.Add("style", "Display:none");
                    }
                }
                else
                {
                    divPassengerInfo.Attributes.Add("style", "Display:none");
                    //divBookingError.Attributes.Add("style", "Display:none");
                }
                //divPassengerInfo -end









                if (obj.AIR_SEG_INF != null)
                {
                    if (obj.AIR_SEG_INF.AIR_ITM != null)
                    {
                        if (obj.AIR_SEG_INF.AIR_ITM.Length > 0)
                        {
                            for (int segCnt = 0; segCnt < obj.AIR_SEG_INF.AIR_ITM.Length; segCnt++)
                            {

                                DataRow drItinerary = tblitinerary.NewRow();

                                drItinerary["Segment_No"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].SEG_NUM;
                                drItinerary["Leg_No"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].SEG_NUM;
                                drItinerary["Airline"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ARL_COD;
                                drItinerary["Flight_No"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].FLI_NUM;
                                if (obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC != null)
                                {
                                    if (obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].COM != null)
                                    {
                                        if (obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].COM.COD_SHA_INF != null)
                                        {
                                            drItinerary["Codeshare_Airline"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].COM.COD_SHA_INF;
                                        }
                                    }

                                    if (obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].EQP_TYP != null)
                                    {
                                        drItinerary["Equipment"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].EQP_TYP;
                                    }

                                    if (obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].MEA_TYP != null)
                                    {
                                        drItinerary["MealTypes"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].MEA_TYP;
                                    }


                                    //if (obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].ELA_FLI_TIM != null)
                                    //{
                                    int time_in_mints = Convert.ToInt32(obj.AIR_SEG_INF.AIR_ITM[segCnt].ADD_FLI_SVC[0].ELA_FLI_TIM);


                                    drItinerary["ElapsedTime"] = getMinutes(time_in_mints.ToString());
                                    //}
                                }
                                drItinerary["Class"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].CLA_COD;
                                drItinerary["Departure"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].DEP_ARP;
                                drItinerary["Destination"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ARR_ARP;


                                DateTime depDate = DateTime.ParseExact(obj.AIR_SEG_INF.AIR_ITM[segCnt].FLI_DAT, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                                DateTime arrDate = DateTime.ParseExact(obj.AIR_SEG_INF.AIR_ITM[segCnt].ARR_DAT, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                                TimeSpan ts = arrDate.Subtract(depDate);


                                drItinerary["Dept_date"] = depDate.ToString("yyyy/MM/dd");
                                drItinerary["Dept_time"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].DEP_TIM;
                                drItinerary["Arrival_date"] = arrDate.ToString("yyyy/MM/dd");
                                drItinerary["Arrival_time"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ARR_TIM;
                                drItinerary["Nextday"] = ts.Days;
                                drItinerary["StopOvers"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].NUM_STO;
                                drItinerary["Dept_Terminal"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].DEP_TER;
                                drItinerary["Arrival_Terminal"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].ARR_TER;
                                drItinerary["Airline_PNR"] = obj.AIR_SEG_INF.AIR_ITM[segCnt].DRC_RES_LOC;

                                tblitinerary.Rows.Add(drItinerary);
                            }

                        }

                        string depCity = "";
                        string desCity = "";
                        string arrCity = "";
                        string retCity = "";

                        string strFlyFrom = "";
                        string strFlyTo = "";
                        string strDepDate = "";
                        string strRetDate = "";


                        string StrItineraryInfo = "";

                        if (tblitinerary != null)
                        {
                            if (tblitinerary.Rows.Count > 0)
                            {
                                //int OutDurationNew = 0;
                                //int InDurationNew = 0;

                                StrItineraryInfo += "<div class='pm-head-title'>" +
                                                        "<div class='pm-content'>" +
                                                            "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Flight details" +
                                                        "</div>" +
                                                    "</div>";
                                labelCount++;
                                StrItineraryInfo += "<div class='pm-content'>";
                                for (int SegCnt = 0; SegCnt < tblitinerary.Rows.Count; SegCnt++)
                                {
                                    /*****************************************/
                                    DataTable dtFlight = FlightCode(tblitinerary.Rows[SegCnt]["Airline"].ToString());
                                    string strAirline = "";
                                    string strAirlineimg = "";
                                    if (dtFlight.Rows.Count > 0)
                                    {
                                        strAirline = dtFlight.Rows[0]["AirlineShortName"].ToString();
                                        strAirlineimg = "Design/shortlogo/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";
                                    }
                                    else
                                    {
                                        strAirline = tblitinerary.Rows[SegCnt]["Airline"].ToString();
                                        strAirlineimg = "Design/shortlogo/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";
                                    }
                                    DataTable dtDepArp = GetDara(tblitinerary.Rows[SegCnt]["Departure"].ToString());
                                    string strDepCity1 = "";
                                    string strDepCity = "";
                                    string strDepCountry = "";
                                    string strDepAirport = "";
                                    if (dtDepArp.Rows.Count > 0)
                                    {
                                        strDepAirport = dtDepArp.Rows[0]["Airport"].ToString() + ", " + dtDepArp.Rows[0]["City"].ToString();
                                        strDepCity1 = dtDepArp.Rows[0]["City"].ToString();
                                        strDepCity = dtDepArp.Rows[0]["Airport"].ToString()/* + ", " + dtDepArp.Rows[0]["City"].ToString()*/;
                                        strDepCountry = dtDepArp.Rows[0]["Country"].ToString();
                                    }
                                    else
                                    {
                                        strDepCity1 = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                                        strDepCity = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                                        strDepAirport = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                                        strDepCountry = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                                    }
                                    string dtDay1 = "";
                                    string dtMonth1 = "";
                                    string dtYear1 = "";
                                    string dtDay2 = "";
                                    string dtMonth2 = "";
                                    string dtYear2 = "";
                                    dtDay1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(8, 2);
                                    dtMonth1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(5, 2);
                                    dtYear1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(0, 4);

                                    dtDay2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(8, 2);
                                    dtMonth2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(5, 2);
                                    dtYear2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(0, 4);

                                    DataTable dtArrArp = GetDara(tblitinerary.Rows[SegCnt]["Destination"].ToString());
                                    string strArrCity1 = "";
                                    string strArrCity = "";
                                    string strArrCountry = "";
                                    string strArrAirport = "";
                                    if (dtArrArp.Rows.Count > 0)
                                    {
                                        strArrAirport = dtArrArp.Rows[0]["Airport"].ToString() + ", " + dtArrArp.Rows[0]["City"].ToString();
                                        strArrCity1 = dtArrArp.Rows[0]["City"].ToString();
                                        strArrCity = dtArrArp.Rows[0]["Airport"].ToString();
                                        strArrCountry = dtArrArp.Rows[0]["Country"].ToString();

                                    }
                                    else
                                    {
                                        strArrCity1 = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                                        strArrCity = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                                        strArrAirport = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                                        strArrCountry = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                                    }
                                    /************************************************************/
                                    if (SegCnt == 0)
                                    {
                                        Session["DEP_DAT" + ssid] = tblitinerary.Rows[SegCnt]["Dept_date"].ToString();
                                        if (OutBoundOriginNew != "" && OutBoundDestinationNew != "")
                                        {

                                            string depDat = "";
                                            string retDat = "";
                                            int stopsCnt = 0;
                                            TimeSpan TtElapsedTimeOut = TimeSpan.Parse("00:00");
                                            /***********Calculating Inbound Duration****************/
                                            for (int dSegCnt = SegCnt; dSegCnt < tblitinerary.Rows.Count; dSegCnt++)
                                            {
                                                if (dSegCnt == 0)
                                                {
                                                    depDat = tblitinerary.Rows[dSegCnt]["Dept_date"].ToString();
                                                    retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                                }

                                                //if (tblitinerary.Rows[dSegCnt]["InBoundOrigin"].ToString() != "")
                                                if (InBoundOriginNew == tblitinerary.Rows[dSegCnt]["Destination"].ToString())
                                                {
                                                    if (dSegCnt == 0)
                                                        retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                                    else
                                                        retDat = tblitinerary.Rows[dSegCnt - 1]["Arrival_date"].ToString();
                                                }
                                                //calculate ElapsedTime  
                                                if (tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString() != "")
                                                {

                                                }


                                                //calculate ElapsedTime
                                                if (dSegCnt > 0 && dSegCnt < tblitinerary.Rows.Count)
                                                {
                                                    //if (tblitinerary.Rows[dSegCnt]["OutBoundOrigin"].ToString() == "")
                                                    if (InBoundOriginNew == tblitinerary.Rows[dSegCnt]["Destination"].ToString())
                                                    {
                                                        stopsCnt++;

                                                        break;
                                                    }
                                                }
                                            }
                                            /***********Calculating Inbound Duration****************/
                                            DataTable dtOutOrg = GetDara(OutBoundOriginNew);
                                            if (dtOutOrg.Rows.Count > 0)
                                                OutBoundOrigin = dtOutOrg.Rows[0]["City"].ToString();
                                            DataTable dtOutDest = GetDara(OutBoundDestinationNew);
                                            if (dtOutDest.Rows.Count > 0)
                                                OutBoundDestination = dtOutDest.Rows[0]["City"].ToString();

                                            strDepDate = DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();
                                            strRetDate = DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();

                                            StrItineraryInfo += "<div class='pm-triptype'>" +
                                                                    "<div class='pm-left'>" +
                                                                        "<div class='pm-ObTitle pm-outbound pm-citi-code-time'><i class='fa fa-plane'></i> <span class='blue'>" + OutBoundOrigin + " - " + OutBoundDestination + "</span>  " + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</div>" +
                                                                    "</div>" +
                                                                "</div>";
                                            StrItineraryInfo += "<div class='pm-seg-wrap'>";

                                            string ArrivalDay = "";
                                            DateTime dep1 = DateTime.Parse(depDat.Substring(5, 2) + "/" + depDat.Substring(8, 2) + "/" + depDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                            DateTime ret2 = DateTime.Parse(retDat.Substring(5, 2) + "/" + retDat.Substring(8, 2) + "/" + retDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                            //TimeSpan ts = TimeSpan.Parse((ret2 - ret2));
                                            double days = (ret2 - dep1).TotalDays;
                                            if (days == 0)
                                                ArrivalDay = " - Same Day Arrival";
                                            else
                                                ArrivalDay = " - " + days + " Day Arrival";
                                            string StrStops = "";
                                            if (stopsCnt == 0)
                                                StrStops = " - Direct Flight";
                                            else
                                                StrStops = " - " + stopsCnt + " stop";

                                            //OutDurationNew += Convert.ToInt32(tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString());
                                        }
                                    }

                                    if (SegCnt > 0)
                                    {
                                        if (InBoundOriginNew == tblitinerary.Rows[SegCnt]["Departure"].ToString())
                                        {
                                            if (InBoundOriginNew != "" && InBoundDestinationNew != "")
                                            {
                                                string depDat = "";
                                                string retDat = "";
                                                int stopsCnt = 0;
                                                TimeSpan TtElapsedTimeIn = TimeSpan.Parse("00:00");
                                                /***********Calculating Inbound Duration****************/
                                                for (int dSegCnt = SegCnt; dSegCnt < tblitinerary.Rows.Count; dSegCnt++)
                                                {
                                                    if (dSegCnt == SegCnt)
                                                    {
                                                        depDat = tblitinerary.Rows[dSegCnt]["Dept_date"].ToString();
                                                        retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                                    }
                                                    //calculate ElapsedTime                                                                                 

                                                    if (tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString() != "")
                                                    {

                                                    }

                                                    //calculate ElapsedTime
                                                    if (dSegCnt > SegCnt && dSegCnt < tblitinerary.Rows.Count)
                                                    {
                                                        retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                                        stopsCnt++;
                                                    }
                                                }
                                                /***********Calculating Inbound Duration****************/

                                                DataTable dtInOrg = GetDara(InBoundOriginNew);
                                                if (dtInOrg.Rows.Count > 0)
                                                    InBoundOrigin = dtInOrg.Rows[0]["City"].ToString();
                                                DataTable dtInDest = GetDara(InBoundDestinationNew);
                                                if (dtInDest.Rows.Count > 0)
                                                    InBoundDestination = dtInDest.Rows[0]["City"].ToString();
                                                StrItineraryInfo += "</div>";

                                                StrItineraryInfo += "<div class='pm-triptype mt-15'>" +
                                                                    "<div class='pm-left'>" +
                                                                        "<div class='pm-ObTitle pm-inbond pm-citi-code-time'><i class='fa fa-plane'></i> <span class='blue'>" + InBoundOrigin + " - " + InBoundDestination + "</span>  " + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</div>" +
                                                                    "</div>" +
                                                                "</div>";
                                                StrItineraryInfo += "<div class='pm-seg-wrap'>";

                                                string ArrivalDay = "";
                                                DateTime dep1 = DateTime.Parse(depDat.Substring(5, 2) + "/" + depDat.Substring(8, 2) + "/" + depDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                                DateTime ret2 = DateTime.Parse(retDat.Substring(5, 2) + "/" + retDat.Substring(8, 2) + "/" + retDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                                //TimeSpan ts = TimeSpan.Parse((ret2 - ret2));
                                                double days = (ret2 - dep1).TotalDays;
                                                if (days == 0)
                                                    ArrivalDay = " - Same Day Arrival";
                                                else
                                                    ArrivalDay = " - " + days + " Day Arrival";
                                                string StrStops = "";
                                                if (stopsCnt == 0)
                                                    StrStops = " - Direct Flight";
                                                else
                                                    StrStops = " - " + stopsCnt + " stop";
                                                //InDurationNew += Convert.ToInt32(tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString());
                                            }
                                        }
                                    }

                                    if (SegCnt > 0 && SegCnt < tblitinerary.Rows.Count)
                                    {
                                        if (InBoundOriginNew != tblitinerary.Rows[SegCnt]["Departure"].ToString())
                                        {

                                            string StopOverTime = CalculateStopOver(tblitinerary.Rows[SegCnt - 1]["Arrival_date"].ToString() + "~" + tblitinerary.Rows[SegCnt - 1]["Arrival_time"].ToString(), tblitinerary.Rows[SegCnt]["Dept_date"].ToString() + "~" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString());
                                            DataTable dtLayoverAirport = GetDara(tblitinerary.Rows[SegCnt - 1]["Destination"].ToString());

                                            //OutDurationNew += FlightConnectionTime.getConnectionDurationMinutes(tblitinerary.Rows[SegCnt - 1]["Arrival_date"].ToString() + " " + tblitinerary.Rows[SegCnt - 1]["Arrival_time"].ToString().Substring(0, 2) + ":" + tblitinerary.Rows[SegCnt - 1]["Arrival_time"].ToString().Substring(2, 2), tblitinerary.Rows[SegCnt]["Dept_date"].ToString() + " " + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().Substring(0, 2) + ":" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().Substring(2, 2));
                                            if (dtLayoverAirport != null)
                                            {

                                                if (dtLayoverAirport.Rows.Count > 0)
                                                {
                                                    StrItineraryInfo += "<div class='pm-layover'>" +
                                                                                        "<div class='pm-bag-info-cont text-center'>" +
                                                                                            "<p><i class='icofont icofont-clock-time'></i> " + dtLayoverAirport.Rows[0]["Airport"] + ", " + dtLayoverAirport.Rows[0]["City"] + " (" + tblitinerary.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min Layover</span></p>" +
                                                                                        "</div>" +
                                                                                    "</div>";
                                                }
                                                else
                                                {
                                                    StrItineraryInfo += "<div class='pm-layover'>" +
                                                                                        "<div class='pm-bag-info-cont text-center'>" +
                                                                                            "<p><i class='icofont icofont-clock-time'></i> (" + tblitinerary.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min Layover</span></p>" +
                                                                                        "</div>" +
                                                                                    "</div>";
                                                }
                                            }
                                            else
                                            {
                                                StrItineraryInfo += "<div class='pm-layover'>" +
                                                                                        "<div class='pm-bag-info-cont text-center'>" +
                                                                                            "<p><i class='icofont icofont-clock-time'></i> (" + tblitinerary.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min Layover</span></p>" +
                                                                                        "</div>" +
                                                                                    "</div>";
                                            }
                                        }
                                    }
                                    /************************************************************/


                                    StrItineraryInfo += "<div class='pm-seg-content'>";

                                    #region pm-air-logo
                                    StrItineraryInfo += "<div class='pm-air-logo'>" +
                                                            "<img src='" + strAirlineimg + "' alt='" + strAirlineimg + "'>" +
                                                            "<p><span class='mr-15'>" + strAirline + "</span>	" +
                                                            "<span class='mr-15'>(" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + String.Format("{0:0000}", Convert.ToInt32(tblitinerary.Rows[SegCnt]["Flight_No"].ToString())) + ")</span>	" +
                                                            "<span class='mr-15'>(Aircraft: " + tblitinerary.Rows[SegCnt]["Equipment"].ToString() + ")</span>";
                                    #endregion
                                    ///***Change************************/
                                        string strAirlineCodeSHare = "";
                                    if (tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString() != "")
                                    {
                                        DataTable dtFlightCodeShare = FlightCode(tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString());
                                        if (dtFlightCodeShare.Rows.Count > 0)
                                        {
                                            strAirlineCodeSHare = dtFlightCodeShare.Rows[0]["AirlineShortName"].ToString();
                                        }
                                        else
                                        {
                                            strAirlineCodeSHare = tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString();
                                        }
                                        if (strAirlineCodeSHare != "")
                                            strAirlineCodeSHare = "<p> Operated B" + strAirlineCodeSHare + "</p>";
                                    }

                                    StrItineraryInfo += "</p>"+ strAirlineCodeSHare + "</div>";
                                    #region pm-seg-A
                                    StrItineraryInfo += "<div class='pm-seg-A'>";

                                    #region pm-seg-from
                                    StrItineraryInfo += "<div class='pm-seg-from'>";
                                    StrItineraryInfo += "<div class='pm-citi-code-time'>" + strDepCity1 + "<span class='blue tootlTip' data-toggle='tooltip' title='" + strDepAirport + "' data-original-title='" + strDepAirport + "'> (" + tblitinerary.Rows[SegCnt]["Departure"].ToString() + ")</span> " +
                                        FormatTime(tblitinerary.Rows[SegCnt]["Dept_time"].ToString()) +
                                        "" /*+ tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[3] */+ "</div>";
                                    StrItineraryInfo += "<p class='tootlTip' data-toggle='tooltip' title='" + strDepAirport + "' data-original-title='" + strDepAirport + "'>" + strDepCity + ",</p>" +
                                                            "<p>" + strDepCountry + "</p>" +
                                                            "<p>" + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</p>";
                                    StrItineraryInfo += "</div>";
                                    #endregion

                                    #region pm-duration
                                    if (tblitinerary.Rows[SegCnt]["ElapsedTime"] != null)
                                    {
                                        if (tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString() != "")
                                        {
                                            StrItineraryInfo += "<div class='pm-duration'>" +
                                                                    "<span class='pm-dur-time'>" + getHoursMinutes(tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString()) +
                                                                "</span>" +
                                                                "<img src='Design/images/grey_durationicon.png' alt='Duration Icon'>" +
                                                                    "<span class='pm-dur-text'>Duration</span>" +
                                                                "</div>";
                                        }
                                    }
                                    #endregion

                                    #region pm-seg-to
                                    StrItineraryInfo += "<div class='pm-seg-to'>" +
                                                            "<div class='pm-citi-code-time'>" +
                                                            "" + strArrCity1 + "<span class='blue tootlTip' data-toggle='tooltip' title='" + strArrAirport + "' data-original-title='" + strArrAirport + "'> (" + tblitinerary.Rows[SegCnt]["Destination"].ToString() +
                                                            ")</span> " +
                                                            FormatTime(tblitinerary.Rows[SegCnt]["Arrival_time"].ToString()) +
                                                            //"" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[3] + 
                                                            "</div>" +
                                                           "<p class='tootlTip' data-toggle='tooltip' title='" + strArrAirport + "' data-original-title='" + strArrAirport + "'>" + strArrCity + ",</p>" +
                                                            "<p>" + strArrCountry + "</p>" +
                                                            "<p>" + DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</p>" +
                                                        "</div>";
                                    #endregion

                                    #region pm-fl-info
                                    StrItineraryInfo += "<div class='pm-fl-info'>";

                                    if (tblitinerary.Rows[SegCnt]["Dept_Terminal"] != null)
                                    {
                                        if (tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() != "")
                                        {
                                            StrItineraryInfo += "<p>Departure Terminal: " + tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() + "</p>";
                                        }
                                    }
                                    if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"] != null)
                                    {
                                        if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() != "")
                                        {
                                            StrItineraryInfo += "<p>Arrival Terminal: " + tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() + "</p>";
                                        }
                                    }
                                    if (tblitinerary.Rows[SegCnt]["Equipment"] != null)
                                    {
                                        if (tblitinerary.Rows[SegCnt]["Equipment"].ToString() != "")
                                        {
                                            string EqpType = getEquipment(tblitinerary.Rows[SegCnt]["Equipment"].ToString());
                                            if (EqpType != "")
                                            {
                                                StrItineraryInfo += "<p>" + EqpType + "</p>";
                                            }
                                        }
                                    }
                                    if (tblitinerary.Rows[SegCnt]["MealTypes"] != null)
                                    {
                                        if (tblitinerary.Rows[SegCnt]["MealTypes"].ToString() != "")
                                        {
                                            string MealType = getMealTypes(tblitinerary.Rows[SegCnt]["MealTypes"].ToString());
                                            if (MealType != "")
                                            {
                                                StrItineraryInfo += "<p>Meals Types:" + tblitinerary.Rows[SegCnt]["MealTypes"].ToString() + "</p>";
                                            }
                                        }
                                    }
                                    if (tblitinerary.Rows[SegCnt]["Airline_PNR"] != null)
                                    {
                                        if (tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() != "")
                                        {
                                            StrItineraryInfo += "<p class='blue'>Airline PNR: " + tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() + "</p>";
                                        }
                                    }

                                    StrItineraryInfo += "</div>";
                                    #endregion

                                    StrItineraryInfo += "</div>";
                                    #endregion

                                    ////segment -end                    



                                    StrItineraryInfo += "</div>";
                                }
                                StrItineraryInfo += "</div>";
                                StrItineraryInfo += "<div class='mt-10'><span class='font12'> * All the times are local to Airports<span class='pm-right font12'>In-Flight services and amenities may vary and are subject to change.</span></div>";

                                StrItineraryInfo += "</div>";
                                divItineraryInfo.InnerHtml = "" + StrItineraryInfo + "";

                                Session["divItineraryInfo" + ssid] = StrItineraryInfo;

                            }
                        }

                        string StrBaggageInfo = "";

                        ////Fare Rules -Start

                        StrBaggageInfo += "<div class='pm-head-title'>" +
                                                                "<div class='pm-content'>" +
                                                                    "<div class='pm-payDetails'>" +
                                                                        "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Fare Rules" +
                                                                    "</div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                            "<div class='pm-content'>";
                        labelCount++;

                        StrBaggageInfo += "<div class='pm-payment'>" +
                                                "<p><strong>Fare Rules:</strong></p>" +
                                                "<p>Cancellation Fee</p>" +
                                                "<p>" +
                                                    "Airline Fee* - This is a non-refundable ticket.<br>" +
                                                    "Travel Merry Admin Fee - USD 150 per passenger where cancelation permitted.<br>" +
                                                    "Partly utilized tickets cannot be cancelled." +
                                                "</p>" +
                                                    "<p><strong>Change Fee</strong></p>" +
                                                    "<p>" +
                                                        "Airline Fee* - This is not changeable ticket.<br>" +
                                                        "Travel Merry Admin Fee - USD 150 per passenger where change permitted." +
                                                    "</p>" +
                                                    "<p>" +
                                                    "*Airlines stop accepting cancellation/change requests 4 - 72 hours before departure of the flight, depending on the airline." +
                                                    "The airline fee is indicative based on an automated interpretation of airline fare rules." +
                                                    "Travel Merry doesn't guarantee the accuracy of this information." +
                                                    "The change/cancellation fee may also vary based on fluctuations in currency conversion rates." +
                                                    "For exact cancellation/change fee, please call us at our customer care number." +
                                                "</p>" +
                                            "</div>";

                        StrBaggageInfo += "<div class='pm-payment'>" +
                                                "<p><strong>Details</strong></p>" +
                                                "<p>Fare is not refundable, name change is not permitted and ticket is not transferable.</p>" +
                                                "<p>" +
                                                    "Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee)" +
                                                    "and is based on availability of flight at the time of change." +
                                                "</p>" +
                                                "<p> " +
                                                    "Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change)." +
                                                "</p>" +
                                                "<p>" +
                                                    "Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p>" +
                                                "<p> Contact our 24/7 toll free customer service to make any changes.</p>" +
                                                "<p>" +
                                                    "Prior to completing the booking in the Terms and Conditions, you should review our service fees for exchanges, changes, refunds and cancellations." +
                                                "</p>" +
                                            "</div>";

                        StrBaggageInfo += "</div>";

                        ////Fare Rules -end







                        if (tblBaggage.Rows.Count > 0)
                        { }

                        StrBaggageInfo = "<div class='pm-head-title'>" +
                                                                "<div class='pm-content'>" +
                                                                    "<div class='pm-payDetails'>" +
                                                                        "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Fare Rules" +
                                                                    "</div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                            "<div class='pm-content'>" +
                                                                "<div class='pm-payDetails'>" +
                                                                    "<div class=''><h5><strong>Booking Rules:</strong></h5><p>Fare is non-refundable, name change is not permitted and ticket is non-transferable.</p><p>Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee) and is based on availability of flight at the time of change.</p><p>Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change).</p><p>Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p><h5><strong>Contact our 24/7 customer service to make any changes.</strong></h5><p>Prior to completing the booking in the <a href='/Termsandconditions.aspx' target='_blank' class='blue'>Terms and Conditions</a>, you should review our service fees for exchanges, changes, refunds and cancellations.</p></div></div></div>";

                        divBaggageInfo.InnerHtml = StrBaggageInfo;
                        Session["divBaggageInfo" + ssid] = StrBaggageInfo;


                    }
                    else
                    {
                        divPassengerInfo.Attributes.Add("style", "Display:none");
                        divBookingDesc.Attributes.Add("style", "Display:none");
                        divBaggageInfo.Attributes.Add("style", "Display:none");
                        divItineraryInfo.Attributes.Add("style", "Display:none");
                        Psdetailscheck.Attributes.Add("style", "Display:none");
                        Psdetailscheck.Attributes.Add("style", "Display:none");
                        Cancelpolicycheck.Attributes.Add("style", "Display:none");
                        divPriceDetails.Attributes.Add("style", "Display:none");
                        divPaymentDetails1.Attributes.Add("style", "Display:none");
                        divPriceDetails1.Attributes.Add("style", "Display:none");
                        divPaymentDetails2.Attributes.Add("style", "Display:none");
                        TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                        displayItinerary_1P_Database(getBookingID(dtPay.Rows[0]["pnrno"].ToString()).ToString(), dtPay, cancellation, Promotion,insurance_total);
                        //if (Session["VoucherAmount" + ssid] != null)
                        //{
                        //    Promo = "yes";
                        //    PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));
                        //}
                        //if (Session["ExtendedCancellation" + ssid] != null)
                        //{
                        //    cancel = "yes";
                        //}
                        StrBookingId = "<div class='pm-content'>" +
                                                           "<div class='pm-left'>" +
                                                               "<p class='bkid-icon'>" +
                                                                   "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                                   " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                                               "</p>" +
                                                           "</div>" +
                                                           "<div class='pm-right pm-tl-fare'>" +
                                                               "<p class='pm-tl-text'>Total price</p>" +
                                                                   "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " <small>USD</small></h4>" +
                                                                   "<p>* inc. all taxes and fees</p>" +
                                                           "</div>" +
                                                       "</div>";

                        StrBookingIdConfirm = "<div class='confirm-head-Left'>" +
                                                            "<h5>Check Your itinerary <small>(* All the times are local to Airports)</small></h5>" +
                                                        "</div>" +
                                                        "<div class='pull-right text-right'>" +
                                                            "<p>Total price</p>" +
                                                            "<div class='tplfare bold'>" + String.Format("{0:0.00}", ttam1) + " <small>USD</small></div>" +
                                                            "<p>inc. all taxes and fees</p>" +
                                                        "</div>";
                        divBookingId.InnerHtml = "" + StrBookingId + "";
                        Session["divBookingId" + ssid] = StrBookingId;
                        Session["StrBookingIdConfirm" + ssid] = StrBookingIdConfirm;
                        ////divBookingId -end

                        //pricesumfoot.InnerHtml = "<div class='pm-content'>" +
                        //       "<div class='pm-left pm-footer-price'>" +
                        //       "<p class='pm-footer-tp'>Total price</p>" +
                        //       "<h4 id='totalAmount4'>" +
                        //       "<span>" + String.Format("{0:0.00}", TtAmount + cancellation) + " </span>" +
                        //       "<small>USD</small></h4>" +
                        //       "<p>* inc. all taxes and fees</p>" +
                        //       "</div>" +
                        //       "<div class='pm-right text-right pm-paynowbtn'>" +
                        //       "<span>" +
                        //       "<input type='button' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
                        //       "</div>" +
                        //       "</div>";
                        ////PNRStatus = false;

                        //divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
                        //                                "<div class='pm-content'>" +
                        //                                    "<div class='pm-payDetails'>" +
                        //                                    "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
                        //                                        "Price Details (USD)" +
                        //                                        "<p>Confirm the price details of your round trip itinerary for all passengers.</p>" +
                        //                                    "</div>" +
                        //                                "</div>" +
                        //                            "</div>" +
                        //                            "<div class='pm-content'>" +

                        //                                "<div class='fareDetails'>" +
                        //                                    "<div class='fareDetailsBody'>" +
                        //                                        "<div class='fareDetailsLeft'>" +
                        //                                            "<div class='ccodeContent'>" +
                        //                                                "<label for='Promo Code'>Promo Code:</label>" +
                        //                                                "<span id='Voucher_Ihint1' class='red'></span>" +
                        //                                                "<div id='promo-code-input'>" +
                        //                                                    "<div class='input-group col-md-12'>";
                        //if (Session["VoucherCode" + ssid] != null)
                        //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id1' id='Voucher_Id1'>";
                        //else
                        //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id1' id='Voucher_Id1'>";
                        //divPriceDetails.InnerHtml += "<span class='input-group-btn'>" +
                        //                                                                "<button class='btn btn-lg' type='button' id='btn_Voucher' onclick='ValidateVoucher()'>Apply</button>" +
                        //                                                            "</span>" +
                        //                                                        "</div>" +
                        //                                                    "</div>" +
                        //                                                "</div>" +
                        //                                            "</div>" +
                        //                                            "<div class='fareDetailsRight'>" +
                        //                                                "<div class='fareQuoted'>" +
                        //                                                    "<div class='fareQuoteLeft'>Total Amount</div> " +
                        //                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></div>" +
                        //                                                "</div>";
                        ////                                                "<div class='fareQuoted'>" +
                        ////                                                    "<div class='fareQuoteLeft'>Adults x " + TotalAdults + "</div>" +
                        ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                        ////                                                "</div>";
                        ////if (TotalChilds != 0)
                        ////{
                        ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
                        ////                                                    "<div class='fareQuoteLeft'>Child x " + TotalChilds + "</div>" +
                        ////                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                        ////                                                "</div>";
                        ////}
                        ////if (TotalInfants != 0)
                        ////{
                        ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
                        ////                                                    "<div class='fareQuoteLeft'>Infant x " + TotalInfants + "</div>" +
                        ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                        ////                                                "</div>";
                        ////}

                        //divPriceDetails.InnerHtml += "<div class='fareQuoted green'>" +
                        //                                                    "<div class='fareQuoteLeft'>24 Hours Cancellation Fee</div>";
                        //if (Session["ExtendedCancellation" + ssid] != null)
                        //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
                        //else
                        //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> 0.00 <small>USD</small></div>";
                        //divPriceDetails.InnerHtml += "</div>" +
                        //                                                "<div class='fareQuoted offerCode'>" +
                        //                                                    "<div class='fareQuoteLeft '>Promocode</div>";
                        //if (Session["VoucherAmount" + ssid] != null)
                        //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
                        //else
                        //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";
                        //divPriceDetails.InnerHtml += "</div>" +
                        //                                                "<div class='fareQuoted offerCode'>" +
                        //                                                    "<div class='fareQuoteLeft '>Discount</div>" +
                        //                                                    "<div class='fareQuoteRight' id='Discount'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                        //                                                "</div>" +
                        //                                            "</div>" +
                        //                                        "</div>" +
                        //                                        "<div class='fareDetailsFooter'>" +
                        //                                            "<div class='tlprice'>" +
                        //                                                "<div class='tlpriceLeft'>Total</div>" +
                        //                                                "<div class='tlpriceRight' id='totalAmount'>" +
                        //                                                    "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " USD</h4>" +
                        //                                                    "<p>incl. all taxes and fees</p>" +
                        //                                                "</div>" +
                        //                                            "</div>" +
                        //                                        "</div>" +
                        //                                        "<div class='fareNote'>" +
                        //                                            "<p><strong>Please Note:</strong> All fares are quoted in USD. Some airlines may charge " +
                        //                                            "<a  href='/ViewBaggageInfo.aspx' target='_blank'>baggage fees</a>. Your credit/debit card may be billed in multiple charges totaling the final total price. </p>" +
                        //                                        "</div>" +
                        //                                    "</div>" +
                        //                                "</div>";
                        string PaymentHeader1 = "<div class='pm-right pm-tl-fare'>" +
                                                       "<p class='pm-tl-text'>Total price</p>" +
                                                       "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " <small>USD</small></h4>" +
                                                       "<p>* inc. all taxes and fees</p>" +
                                                   "</div>";
                        paymentHeader.InnerHtml = PaymentHeader;
                        //divPriceDetails1.InnerHtml = "<div class='faresummery-title'>" +
                        //                               "<h4>Price Details (USD)</h4>" +
                        //                           "</div>" +
                        //                           "<div class='faresummery-body'>" +
                        //                               "<div class='faresummery-seg-wrap'>" +
                        //                                   "<div class=''>" +
                        //                       "<div class='price-details'>" +
                        //                            "<span class='pull-left'>Total Amount</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></span>" +
                        //                        "</div>";
                        ////                                       "<span class='pull-left'>Adults x " + TotalAdults + "</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(adultfare)) + " <small>USD</small></span>" +
                        ////                                   "</div>";
                        ////if (TotalChilds != 0)
                        ////{
                        ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                        ////                                            "<span class='pull-left'>Child x " + TotalChilds + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(childfare)) + " <small>USD</small></span>" +
                        ////                                        "</div>";
                        ////}

                        ////if (TotalInfants != 0)
                        ////{
                        ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                        ////                                            "<span class='pull-left'>Infant x " + TotalInfants + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(infantfare)) + " <small>USD</small></span>" +
                        ////                                        "</div>";
                        ////}

                        //divPriceDetails1.InnerHtml += "<div class='price-details green'>" +
                        //                                        "<span class='pull-left'>24 Hours Cancellation Fee</span>";
                        //if (Session["ExtendedCancellation" + ssid] != null)
                        //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
                        //else
                        //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> 0.00 <small>USD</small></span>";

                        //divPriceDetails1.InnerHtml += "</div>" +
                        //                                    "<div class='price-details'>" +
                        //                                        "<span class='pull-left'>Discount</span> <span class='pull-right dicPrice'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></span>" +
                        //                                    "</div>" +
                        //                                    "<div class='price-details'>" +
                        //                                        "<span class='pull-left'>Promocode</span> ";
                        //if (Session["VoucherAmount" + ssid] != null)
                        //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
                        //else
                        //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";

                        //divPriceDetails1.InnerHtml += "</div>" +
                        //                                    "<div class='price-details'>" +
                        //                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' id='totalAmount1'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></span>" +
                        //                                    "</div>" +
                        //                                "</div>" +
                        //                                "<div class='faresummery-seg-wrap promoBg'>" +
                        //                                    "<label for='Promo Code'>Promo Code:</label>" +
                        //                                    "<span id='Voucher_Ihint' class='red'></span>" +
                        //                                    "<div id='promo-code-input'>" +
                        //                                        "<div class='input-group col-md-12'>";
                        //if (Session["VoucherCode" + ssid] != null)
                        //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id' id='Voucher_Id'>";
                        //else
                        //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id' id='Voucher_Id'>";

                        //divPriceDetails1.InnerHtml += "<span class='input-group-btn'>" +
                        //                                                "<button class='btn btn-lg' type='button' id='btn_Voucher1' onclick='ValidateVoucher1()'>Apply</button>" +
                        //                                            "</span>" +
                        //                                        "</div>" +
                        //                                    "</div>" +
                        //                                    "<div class='faresummery-seg-wrap'>" +
                        //                                        "<div class='totalAmount' id='totalAmount2'>" +
                        //                                            "<p>Total Price:</p>" +
                        //                                            "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></h3>" +
                        //                                            "<p>incl. all taxes and fees</p>" +
                        //                                        "</div>" +
                        //                                        "<button type='button' id='' class='pm-payment-btn btn btn-block' onclick='return btn_Submit(this);'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                        //                                    "<span class='tcktleft' id='visitors'></button>" +
                        //                                    "</div>" +
                        //                                "</div>" +
                        //                                "</div>" +
                        //                            "</div>";
                        RedirectCS();
                        //}
                        string error = "<div class='pm-content'>" +
                   "<div class='pm-error warning' style=''>" +
                   "<ul>" +
                   "<li class=''>" +
                                           "Please contact our support team to help you to complete the Payment for this booking <span class='bold' ><a href='tel:" + ContactDetails.Phone_support + "' ><i class='fa fa-phone'></i>" + ContactDetails.Phone_support + "</a></span>" +
                   "</li>" +
                   "</ul>" +
                   "</div>" +
                   "</div>";
                        if (paymentdone)
                        {
                            divBookingError.InnerHtml = "";
                            divBookingError.Attributes.Add("style", "Display:none");

                            divpayfailederror.InnerHtml = error;
                            divpayfailederror.Attributes.Add("style", "Display:block");

                            insurance_csa.Attributes.Add("style", "Display:none");
                        }
                        else
                        {
                            divpayfailederror.InnerHtml = error;
                            divpayfailederror.Attributes.Add("style", "Display:block");
                        }
                    }
                }
                else
                {
                    divPassengerInfo.Attributes.Add("style", "Display:none");
                    divBookingDesc.Attributes.Add("style", "Display:none");
                    divBaggageInfo.Attributes.Add("style", "Display:none");
                    divItineraryInfo.Attributes.Add("style", "Display:none");
                    Psdetailscheck.Attributes.Add("style", "Display:none");
                    Psdetailscheck.Attributes.Add("style", "Display:none");
                    Cancelpolicycheck.Attributes.Add("style", "Display:none");
                    insurance_csa.Attributes.Add("style", "Display:none");
                    divPriceDetails.Attributes.Add("style", "Display:none");
                    divPaymentDetails1.Attributes.Add("style", "Display:none");
                    divPriceDetails1.Attributes.Add("style", "Display:none");
                    divPaymentDetails2.Attributes.Add("style", "Display:none");
                    //string StrBookingId = "";
                    //string StrBookingIdConfirm = "";
                    displayItinerary_1P_Database(getBookingID(dtPay.Rows[0]["pnrno"].ToString()).ToString(), dtPay, cancellation, Promotion, insurance_total);
                    TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                    //if (Session["VoucherAmount" + ssid] != null)
                    //{
                    //    Promo = "yes";
                    //    PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
                    //                                                                                                      //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
                    //}
                    //if (Session["ExtendedCancellation" + ssid] != null)
                    //{
                    //    cancel = "yes";
                    //    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
                    //}
                    StrBookingId = "<div class='pm-content'>" +
                                                       "<div class='pm-left'>" +
                                                           "<p class='bkid-icon'>" +
                                                               "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                               " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                                           "</p>" +
                                                       "</div>" +
                                                       "<div class='pm-right pm-tl-fare'>" +
                                                           "<p class='pm-tl-text'>Total price</p>" +
                                                               "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion+insurance_total) + " <small>USD</small></h4>" +
                                                               "<p>* inc. all taxes and fees</p>" +
                                                       "</div>" +
                                                   "</div>";

                    StrBookingIdConfirm = "<div class='confirm-head-Left'>" +
                                                        "<h5>Check Your itinerary <small>(* All the times are local to Airports)</small></h5>" +
                                                    "</div>" +
                                                    "<div class='pull-right text-right'>" +
                                                        "<p>Total price</p>" +
                                                        "<div class='tplfare bold'>" + String.Format("{0:0.00}", ttam1) + " <small>USD</small></div>" +
                                                        "<p>inc. all taxes and fees</p>" +
                                                    "</div>";
                    divBookingId.InnerHtml = "" + StrBookingId + "";
                    Session["divBookingId" + ssid] = StrBookingId;
                    Session["StrBookingIdConfirm" + ssid] = StrBookingIdConfirm;
                    ////divBookingId -end

                    //pricesumfoot.InnerHtml = "<div class='pm-content'>" +
                    //       "<div class='pm-left pm-footer-price'>" +
                    //       "<p class='pm-footer-tp'>Total price</p>" +
                    //       "<h4 id='totalAmount4'>" +
                    //       "<span>" + String.Format("{0:0.00}", TtAmount + cancellation) + " </span>" +
                    //       "<small>USD</small></h4>" +
                    //       "<p>* inc. all taxes and fees</p>" +
                    //       "</div>" +
                    //       "<div class='pm-right text-right pm-paynowbtn'>" +
                    //       "<span>" +
                    //       "<input type='button' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
                    //       "</div>" +
                    //       "</div>";
                    ////PNRStatus = false;

                    //divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
                    //                                "<div class='pm-content'>" +
                    //                                    "<div class='pm-payDetails'>" +
                    //                                    "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
                    //                                        "Price Details (USD)" +
                    //                                        "<p>Confirm the price details of your round trip itinerary for all passengers.</p>" +
                    //                                    "</div>" +
                    //                                "</div>" +
                    //                            "</div>" +
                    //                            "<div class='pm-content'>" +

                    //                                "<div class='fareDetails'>" +
                    //                                    "<div class='fareDetailsBody'>" +
                    //                                        "<div class='fareDetailsLeft'>" +
                    //                                            "<div class='ccodeContent'>" +
                    //                                                "<label for='Promo Code'>Promo Code:</label>" +
                    //                                                "<span id='Voucher_Ihint1' class='red'></span>" +
                    //                                                "<div id='promo-code-input'>" +
                    //                                                    "<div class='input-group col-md-12'>";
                    //if (Session["VoucherCode" + ssid] != null)
                    //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id1' id='Voucher_Id1'>";
                    //else
                    //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id1' id='Voucher_Id1'>";
                    //divPriceDetails.InnerHtml += "<span class='input-group-btn'>" +
                    //                                                                "<button class='btn btn-lg' type='button' id='btn_Voucher' onclick='ValidateVoucher()'>Apply</button>" +
                    //                                                            "</span>" +
                    //                                                        "</div>" +
                    //                                                    "</div>" +
                    //                                                "</div>" +
                    //                                            "</div>" +
                    //                                            "<div class='fareDetailsRight'>" +
                    //                                            "<div class='fareQuoted'>" +
                    //                                                "<div class='fareQuoteLeft'>Total Amount</div> " +
                    //                                                "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></div>" +
                    //                                            "</div>";
                    ////                                                "<div class='fareQuoted'>" +
                    ////                                                    "<div class='fareQuoteLeft'>Adults x " + TotalAdults + "</div>" +
                    ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                    ////                                                "</div>";
                    ////if (TotalChilds != 0)
                    ////{
                    ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
                    ////                                                    "<div class='fareQuoteLeft'>Child x " + TotalChilds + "</div>" +
                    ////                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                    ////                                                "</div>";
                    ////}
                    ////if (TotalInfants != 0)
                    ////{
                    ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
                    ////                                                    "<div class='fareQuoteLeft'>Infant x " + TotalInfants + "</div>" +
                    ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                    ////                                                "</div>";
                    ////}

                    //divPriceDetails.InnerHtml += "<div class='fareQuoted green'>" +
                    //                                                    "<div class='fareQuoteLeft'>24 Hours Cancellation Fee</div>";
                    //if (Session["ExtendedCancellation" + ssid] != null)
                    //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
                    //else
                    //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> 0.00 <small>USD</small></div>";
                    //divPriceDetails.InnerHtml += "</div>" +
                    //                                                "<div class='fareQuoted offerCode'>" +
                    //                                                    "<div class='fareQuoteLeft '>Promocode</div>";
                    //if (Session["VoucherAmount" + ssid] != null)
                    //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
                    //else
                    //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";
                    //divPriceDetails.InnerHtml += "</div>" +
                    //                                                "<div class='fareQuoted offerCode'>" +
                    //                                                    "<div class='fareQuoteLeft '>Discount</div>" +
                    //                                                    "<div class='fareQuoteRight' id='Discount'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                    //                                                "</div>" +
                    //                                            "</div>" +
                    //                                        "</div>" +
                    //                                        "<div class='fareDetailsFooter'>" +
                    //                                            "<div class='tlprice'>" +
                    //                                                "<div class='tlpriceLeft'>Total</div>" +
                    //                                                "<div class='tlpriceRight' id='totalAmount'>" +
                    //                                                    "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " USD</h4>" +
                    //                                                    "<p>incl. all taxes and fees</p>" +
                    //                                                "</div>" +
                    //                                            "</div>" +
                    //                                        "</div>" +
                    //                                        "<div class='fareNote'>" +
                    //                                            "<p><strong>Please Note:</strong> All fares are quoted in USD. Some airlines may charge " +
                    //                                            "<a  href='/ViewBaggageInfo.aspx' target='_blank'>baggage fees</a>. Your credit/debit card may be billed in multiple charges totaling the final total price. </p>" +
                    //                                        "</div>" +
                    //                                    "</div>" +
                    //                                "</div>";
                    string PaymentHeader1 = "<div class='pm-right pm-tl-fare'>" +
                                                   "<p class='pm-tl-text'>Total price</p>" +
                                                   "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount + cancellation + Promotion +insurance_total) + " <small>USD</small></h4>" +
                                                   "<p>* inc. all taxes and fees</p>" +
                                               "</div>";
                    paymentHeader.InnerHtml = PaymentHeader1;
                    //divPriceDetails1.InnerHtml = "<div class='faresummery-title'>" +
                    //                               "<h4>Price Details (USD)</h4>" +
                    //                           "</div>" +
                    //                           "<div class='faresummery-body'>" +
                    //                               "<div class='faresummery-seg-wrap'>" +
                    //                                   "<div class=''>" +
                    //                           "<div class='price-details'>" +
                    //                                "<span class='pull-left'>Total Amount</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></span>" +
                    //                            "</div>";
                    ////                                       "<span class='pull-left'>Adults x " + TotalAdults + "</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(adultfare)) + " <small>USD</small></span>" +
                    ////                                   "</div>";
                    ////if (TotalChilds != 0)
                    ////{
                    ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                    ////                                            "<span class='pull-left'>Child x " + TotalChilds + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(childfare)) + " <small>USD</small></span>" +
                    ////                                        "</div>";
                    ////}

                    ////if (TotalInfants != 0)
                    ////{
                    ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                    ////                                            "<span class='pull-left'>Infant x " + TotalInfants + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(infantfare)) + " <small>USD</small></span>" +
                    ////                                        "</div>";
                    ////}

                    //divPriceDetails1.InnerHtml += "<div class='price-details green'>" +
                    //                                        "<span class='pull-left'>24 Hours Cancellation Fee</span>";
                    //if (Session["ExtendedCancellation" + ssid] != null)
                    //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
                    //else
                    //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> 0.00 <small>USD</small></span>";

                    //divPriceDetails1.InnerHtml += "</div>" +
                    //                                    "<div class='price-details'>" +
                    //                                        "<span class='pull-left'>Discount</span> <span class='pull-right dicPrice'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></span>" +
                    //                                    "</div>" +
                    //                                    "<div class='price-details'>" +
                    //                                        "<span class='pull-left'>Promocode</span> ";
                    //if (Session["VoucherAmount" + ssid] != null)
                    //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
                    //else
                    //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";

                    //divPriceDetails1.InnerHtml += "</div>" +
                    //                                    "<div class='price-details'>" +
                    //                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' id='totalAmount1'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></span>" +
                    //                                    "</div>" +
                    //                                "</div>" +
                    //                                "<div class='faresummery-seg-wrap promoBg'>" +
                    //                                    "<label for='Promo Code'>Promo Code:</label>" +
                    //                                    "<span id='Voucher_Ihint' class='red'></span>" +
                    //                                    "<div id='promo-code-input'>" +
                    //                                        "<div class='input-group col-md-12'>";
                    //if (Session["VoucherCode" + ssid] != null)
                    //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id' id='Voucher_Id'>";
                    //else
                    //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id' id='Voucher_Id'>";

                    //divPriceDetails1.InnerHtml += "<span class='input-group-btn'>" +
                    //                                                "<button class='btn btn-lg' type='button' id='btn_Voucher1' onclick='ValidateVoucher1()'>Apply</button>" +
                    //                                            "</span>" +
                    //                                        "</div>" +
                    //                                    "</div>" +
                    //                                    "<div class='faresummery-seg-wrap'>" +
                    //                                        "<div class='totalAmount' id='totalAmount2'>" +
                    //                                            "<p>Total Price:</p>" +
                    //                                            "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></h3>" +
                    //                                            "<p>incl. all taxes and fees</p>" +
                    //                                        "</div>" +
                    //                                        "<button type='button' id='' class='pm-payment-btn btn btn-block' onclick='return btn_Submit(this);'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                    //                                    "<span class='tcktleft' id='visitors'></button>" +
                    //                                    "</div>" +
                    //                                "</div>" +
                    //                                "</div>" +
                    //                            "</div>";
                    string error = "<div class='pm-content'>" +
                   "<div class='pm-error warning' style=''>" +
                   "<ul>" +
                   "<li class=''>" +
                                           "Please contact our support team to help you to complete the Payment for this booking <span class='bold' ><a href='tel:" + ContactDetails.Phone_support + "' ><i class='fa fa-phone'></i>" + ContactDetails.Phone_support + "</a></span>" +
                   "</li>" +
                   "</ul>" +
                   "</div>" +
                   "</div>";
                    if (paymentdone)
                    {
                        divBookingError.InnerHtml = "";
                        divBookingError.Attributes.Add("style", "Display:none");

                        insurance_csa.Attributes.Add("style", "Display:none");

                        divpayfailederror.InnerHtml = error;
                        divpayfailederror.Attributes.Add("style", "Display:block");
                    }
                    else
                    {
                        divpayfailederror.InnerHtml = error;
                        divpayfailederror.Attributes.Add("style", "Display:block");
                    }
                    RedirectCS();
                    //}
                }
            }
            //}
            //else
            //{
            //    //PNRStatus = false;
            //}
        }
        catch (Exception exc)
        {

        }
    }

    public void displayItinerary_1P_Database(string bookingId, DataTable dtPay, double cancellation, double promotion , double insurance_total)
    {
        string OutBoundOrigin = "";
        string OutBoundDestination = "";
        string InBoundOrigin = "";
        string InBoundDestination = "";

        string OutBoundOriginNew = "";
        string OutBoundDestinationNew = "";
        string InBoundOriginNew = "";
        string InBoundDestinationNew = "";
        string StrItineraryInfo = "";
        string StrItineraryInfo1 = "";
        string ItineraryInfo = "";
        
        string strDepDate = "";
        string strRetDate = "";
        string strDepDate1 = "";
        string strRetDate1 = "";
        string JType = "2";
        SqlParameter[] para_booking_pnr = { new SqlParameter("@Booking_ID", Convert.ToInt64(bookingId)) };
        DataTable dtbooking_pnr = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "select_booking_pnr-Booking_ID", para_booking_pnr);

        SqlParameter[] para_PNR = { new SqlParameter("@PNR_ID", Convert.ToInt64(dtbooking_pnr.Rows[0]["PNR_ID"])) };
        DataTable dtPNR = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "select_PNR", para_PNR);

        if (dtPNR != null)
        {
            if (dtPNR.Rows.Count > 0)
            {
                OutBoundOrigin = dtPNR.Rows[0]["Depature"].ToString();
                OutBoundDestination = dtPNR.Rows[0]["Destination"].ToString();
                InBoundOrigin = dtPNR.Rows[0]["Return_Depature_From"].ToString();
                InBoundDestination = dtPNR.Rows[0]["Return_Depature_To"].ToString();

                OutBoundOriginNew = dtPNR.Rows[0]["Depature"].ToString();
                OutBoundDestinationNew = dtPNR.Rows[0]["Destination"].ToString();
                InBoundOriginNew = dtPNR.Rows[0]["Return_Depature_From"].ToString();
                InBoundDestinationNew = dtPNR.Rows[0]["Return_Depature_To"].ToString();
                if (dtPNR.Rows[0]["Return_Depature_From"].ToString() == "")
                {
                    JType = "1";
                }
            }
        }
        string StrBookingId = "";
        string StrBookingIdConfirm = "";
        SqlParameter[] para_PNR_Details = { new SqlParameter("@PNR_ID", dtPNR.Rows[0]["PNR_ID"]) };
        DataTable dtPNRDetails = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "select_PNR_Details", para_PNR_Details);

        SqlParameter[] para_Booking_Product = { new SqlParameter("@Booking_ID", Convert.ToInt64(bookingId)) };
        DataTable dtBooking_Product = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "select_bookingproduct", para_Booking_Product);

        double TtAmount = 0;
        foreach (DataRow drBooking_Product in dtBooking_Product.Rows)
        {
            TtAmount += Math.Round((Convert.ToDouble(drBooking_Product["Fare"]) + Convert.ToDouble(drBooking_Product["Tax"])) * Convert.ToDouble(drBooking_Product["Units"]), 2);
        }
        if (dtPNRDetails != null)
        {
            if (dtPNRDetails.Rows.Count > 0)
            {
                string StrPassengerInfo = "";

                SqlParameter[] para_PNR1 = { new SqlParameter("@PNR_ID", Convert.ToInt64(dtbooking_pnr.Rows[0]["PNR_ID"])) };
                DataTable dtBooking_pax = dbAccess.GetSpecificData_SP(ConfigurationManager.AppSettings["sqlcn"].ToString(), "Booking_Pax_get_details", para_PNR1);
                if (dtBooking_pax != null)
                {
                    if (dtBooking_pax.Rows.Count > 0)
                    {

                        StrPassengerInfo += "<div class='pm-head-title'>" +
                                                    "<div class='pm-content'>" +
                                                        "<div class='travelerTitle faa-parent animated-hover'>" +
                                                            "<img src='Design/images/passenger-icon.png' alt='Passenger icon' class='title-icons faa-passing'> Traveller Details <P>(Title, Last Name, First Name, Passenger Type)</P>" +
                                                        "</div>" +
                                                    "</div>" +
                                                "</div>" +
                                                "<div class='pm-content'>" +
                                                    "<div class='pm-traveler-list'>" +
                                                        "<ul>";
                        labelCount++;
                        int i = 1;

                        //if (dtbookingpax.Rows.Count <= 0)
                        //{
                        foreach (DataRow drtblPax in dtBooking_pax.Rows)
                        {
                            string PaxType = "";
                            if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "1")
                                PaxType = "(ADULT)";
                            else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "2")
                                PaxType = "(CHILD)";
                            else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "3")
                                PaxType = "(INFANT WITHOUT SEAT)";
                            else
                                PaxType = drtblPax["Pax_Title"].ToString().ToUpper();


                            StrPassengerInfo += "<li>" + i + ". " + drtblPax["Pax_Title"].ToString().ToUpper() + " " + drtblPax["Pax_Surname"].ToString().ToUpper() + " " + drtblPax["Pax_FirstName"].ToString().ToUpper() + " " + PaxType + " </li>";
                           
                            i++;
                        }
                       
                        StrPassengerInfo += "</ul>" +
                                                    "</div>" +
                                                "</div>";
                        

                        divPassengerInfo.InnerHtml = "" + StrPassengerInfo + "";
                        divPassengerInfo.Attributes.Add("style", "Display:block");

                    }
                }

                StrBookingId += "<div class='pm-content'>" +
                                           "<div class='pm-left'>" +
                                               "<p class='bkid-icon'>" +
                                                   "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                   " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                               "</p>" +
                                           "</div>" +
                                           "<div class='pm-right pm-tl-fare'>" +
                                               "<p class='pm-tl-text'>Total price</p>" +
                                                   "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + promotion + insurance_total) + " <small>USD</small></h4>" +
                                                   "<p>* inc. all taxes and fees</p>" +
                                           "</div>" +
                                       "</div>";
                divBookingId.InnerHtml = "" + StrBookingId + "";

               
                ItineraryInfo +=        "<div class='pm-head-title'>" +
                                            "<div class='pm-content'>" +
                                                "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Flight details" +
                                            "</div>" +
                                        "</div>";
                labelCount++;
                ItineraryInfo +=        "<div class='pm-content'>";

                for (int SegCnt = 0; SegCnt < dtPNRDetails.Rows.Count; SegCnt++)
                {
                    try
                    {

                        /*****************************************/

                        DataTable dtFlight = FlightCode(dtPNRDetails.Rows[SegCnt]["Airline"].ToString());
                        string strAirline = "";
                        string strAirlineimg = "";
                        if (dtFlight.Rows.Count > 0)
                        {
                            strAirline = dtFlight.Rows[0]["AirlineShortName"].ToString();
                            strAirlineimg = "Design/shortlogo/" + dtPNRDetails.Rows[SegCnt]["Airline"].ToString() + ".gif";

                        }
                        else
                        {
                            strAirline = dtPNRDetails.Rows[SegCnt]["Airline"].ToString();
                            strAirlineimg = "Design/shortlogo/" + dtPNRDetails.Rows[SegCnt]["Airline"].ToString() + ".gif";
                        }

                        DataTable dtDepArp = GetDara(dtPNRDetails.Rows[SegCnt]["Departure"].ToString());
                        string strDepCity1 = "";
                        string strDepCity = "";
                        string strDepCountry = "";
                        if (dtDepArp.Rows.Count > 0)
                        {
                            strDepCity1 = dtDepArp.Rows[0]["City"].ToString();
                            strDepCity = dtDepArp.Rows[0]["Airport"].ToString() + ", " + dtDepArp.Rows[0]["City"].ToString();
                            strDepCountry = dtDepArp.Rows[0]["Country"].ToString();
                        }
                        else
                        {
                            strDepCity1 = dtPNRDetails.Rows[SegCnt]["Departure"].ToString();
                            strDepCity = dtPNRDetails.Rows[SegCnt]["Departure"].ToString();
                            strDepCountry = dtPNRDetails.Rows[SegCnt]["Departure"].ToString();
                        }
                        string dtDay1 = "";
                        string dtMonth1 = "";
                        string dtYear1 = "";
                        string dtDay2 = "";
                        string dtMonth2 = "";
                        string dtYear2 = "";

                        string dasd = DateTime.Parse(dtPNRDetails.Rows[SegCnt]["Dept_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy/MM/dd/").ToUpper();
                        dtDay1 = dasd.Substring(8, 2);
                        dtMonth1 = dasd.Substring(5, 2);
                        dtYear1 = dasd.Substring(0, 4);

                        dasd = DateTime.Parse(dtPNRDetails.Rows[SegCnt]["Arrival_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("yyyy/MM/dd/").ToUpper();
                        dtDay2 = dasd.Substring(8, 2);
                        dtMonth2 = dasd.Substring(5, 2);
                        dtYear2 = dasd.Substring(0, 4);


                        DataTable dtArrArp = GetDara(dtPNRDetails.Rows[SegCnt]["Destination"].ToString());
                        string strArrCity1 = "";
                        string strArrCity = "";
                        string strArrCountry = "";
                        if (dtArrArp.Rows.Count > 0)
                        {
                            strArrCity1 = dtArrArp.Rows[0]["City"].ToString();
                            strArrCity = dtArrArp.Rows[0]["Airport"].ToString() + ", " + dtArrArp.Rows[0]["City"].ToString();
                            strArrCountry = dtArrArp.Rows[0]["Country"].ToString();

                        }
                        else
                        {
                            strArrCity1 = dtPNRDetails.Rows[SegCnt]["Destination"].ToString();
                            strArrCity = dtPNRDetails.Rows[SegCnt]["Destination"].ToString();
                            strArrCountry = dtPNRDetails.Rows[SegCnt]["Destination"].ToString();
                        }


                        /************************************************************/
                        if (SegCnt == 0)
                        {
                            if (OutBoundOriginNew != "" && OutBoundDestinationNew != "")
                            {
                                ItineraryInfo += "<div class='pm-triptype'>" +
                                                                    "<div class='pm-left'>" +
                                                                        "<div class='pm-ObTitle pm-outbound pm-citi-code-time'><i class='fa fa-plane'></i> <span class='blue'> Outbound" +
                                                                        //"" + OutBoundOrigin + " - " + OutBoundDestination + "</span>  " + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "" +
                                                                        "</div>" +
                                                                    "</div>" +
                                                                "</div>";
                            }
                        }

                        if (SegCnt > 0)
                        {
                            if (InBoundOriginNew == dtPNRDetails.Rows[SegCnt]["Departure"].ToString())
                            {
                                if (InBoundOriginNew != "" && InBoundDestinationNew != "")
                                {
                                    ItineraryInfo += "<div class='pm-triptype mt-15'>" +
                                                        "<div class='pm-left'>" +
                                                            "<div class='pm-ObTitle pm-inbond pm-citi-code-time'><i class='fa fa-plane'></i> <span class='blue'> Inbound" +
                                                            //"" + InBoundOrigin + " - " + InBoundDestination + "</span>  " + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "" +
                                                            "</div>" +
                                                        "</div>" +
                                                    "</div>";
                                }
                            }
                        }

                        if (SegCnt > 0 && SegCnt < dtPNRDetails.Rows.Count)
                        {
                            if (InBoundOriginNew != dtPNRDetails.Rows[SegCnt]["Departure"].ToString())
                            {
                                //string d = dtPNRDetails.Rows[SegCnt - 1]["Arrival_date"].ToString().Split(' ')[0].Split('/')[2] + "/" + dtPNRDetails.Rows[SegCnt - 1]["Arrival_date"].ToString().Split(' ')[0].Split('/')[0] + "/" + dtPNRDetails.Rows[SegCnt - 1]["Arrival_date"].ToString().Split(' ')[0].Split('/')[1];

                                //string d1 = dtPNRDetails.Rows[SegCnt]["Dept_date"].ToString().Split(' ')[0].Split('/')[2] + "/" + dtPNRDetails.Rows[SegCnt]["Dept_date"].ToString().Split(' ')[0].Split('/')[0] + "/" + dtPNRDetails.Rows[SegCnt]["Dept_date"].ToString().Split(' ')[0].Split('/')[1];

                                //string StopOverTime = CalculateStopOver(d + "~" + dtPNRDetails.Rows[SegCnt - 1]["Arrival_time"].ToString(), d1 + "~" + dtPNRDetails.Rows[SegCnt]["Dept_time"].ToString());

                                DataTable dtLayoverAirport = GetDara(dtPNRDetails.Rows[SegCnt - 1]["Destination"].ToString());
                                if (dtLayoverAirport != null)
                                {
                                    if (dtLayoverAirport.Rows.Count > 0)
                                    {
                                        ItineraryInfo += "<div class='pm-layover'>" +
                                                                                        "<div class='pm-bag-info-cont text-center'>" +
                                                                                            "<p>" +
                                                                                            "<i class='icofont icofont-clock-time'></i> " + dtLayoverAirport.Rows[0]["Airport"] + ", " + dtLayoverAirport.Rows[0]["City"] + " (" + dtPNRDetails.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" +
                                                                                            //"" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min " +
                                                                                            "Layover</span>"+
                                                                                            "</p>" +
                                                                                        "</div>" +
                                                                                    "</div>";
                                    }
                                    else
                                    {
                                        ItineraryInfo += "<div class='pm-layover'>" +
                                                                                        "<div class='pm-bag-info-cont text-center'>" +
                                                                                            "<p>" +
                                                                                            "<i class='icofont icofont-clock-time'></i> (" + dtPNRDetails.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" +
                                                                                            //"" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min " +
                                                                                            "Layover</span>"+
                                                                                            "</p>" +
                                                                                        "</div>" +
                                                                                    "</div>";
                                    }
                                }
                                else
                                {
                                    ItineraryInfo += "<div class='pm-layover'>" +
                                                                                        "<div class='pm-bag-info-cont text-center'>" +
                                                                                            "<p>" +
                                                                                            "<i class='icofont icofont-clock-time'></i> (" + dtPNRDetails.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" +
                                                                                            //"/*" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min*/" +
                                                                                            " Layover</span>"+
                                                                                            "</p>" +
                                                                                        "</div>" +
                                                                                    "</div>";

                                }
                            }
                        }
                        /************************************************************/
                        ItineraryInfo +=        "<div class='pm-seg-wrap'>";
                        ItineraryInfo +=            "<div class='pm-seg-content'>";
                        string strAirlineCodeSHare = "";

                        if (dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString() != "")
                        {
                            DataTable dtFlightCodeShare = FlightCode(dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString());
                            if (dtFlightCodeShare.Rows.Count > 0)
                            {
                                strAirlineCodeSHare = dtFlightCodeShare.Rows[0]["AirlineShortName"].ToString();

                            }
                            else
                            {
                                strAirlineCodeSHare = dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString();
                            }
                            if (strAirlineCodeSHare != "")
                                strAirlineCodeSHare = "<p>" + strAirlineCodeSHare + "</p>";
                        }
                        #region pm-air-logo
                        ItineraryInfo +=                "<div class='pm-air-logo'>" +
                                                            "<img src='" + strAirlineimg + "' alt='" + strAirlineimg + "'>" +
                                                            "<p><span class='mr-15'>" + strAirline + "</span>	<span class='mr-15'>(" + dtPNRDetails.Rows[SegCnt]["Airline"].ToString() + String.Format("{0:0000}", Convert.ToInt32(dtPNRDetails.Rows[SegCnt]["Flight_No"].ToString())) + ")</span>	<span class='mr-15'>(Aircraft: " + dtPNRDetails.Rows[SegCnt]["Equipment"].ToString() + ")</span></p>" + strAirlineCodeSHare + "" +
                                                        "</div>";
                        #endregion

                        
                        #region pm-seg-A
                        ItineraryInfo +=                "<div class='pm-seg-A'>";

                        #region pm-seg-from
                        ItineraryInfo +=                    "<div class='pm-seg-from'>";
                        ItineraryInfo +=                        "<div class='pm-citi-code-time'>" + strDepCity.Split(',')[1] + " <span class='blue tootlTip' data-toggle='tooltip' title='" + strDepCity + "' data-original-title='" + strDepCity + "'>(" + dtPNRDetails.Rows[SegCnt]["Departure"].ToString() + ")</span> " + dtPNRDetails.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[0] + "" + dtPNRDetails.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[1] + ":" + dtPNRDetails.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[2] + "" + dtPNRDetails.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[3] + "</div>";
                        ItineraryInfo +=                        "<p class='tootlTip' data-toggle='tooltip' title='" + strDepCity + "' data-original-title='" + strDepCity + "'>" + strDepCity + ",</p>" +
                                                                "<p>" + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</p>";
                        //if (dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString() != "")
                        //    ItineraryInfo +=                    "<p class='tootlTip' data-toggle='tooltip' title='" + getAirline(dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString()) + "' data-original-title='" + getAirline(dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString()) + "'>Operated By " + getAirline(dtPNRDetails.Rows[SegCnt]["Codeshare_Airline"].ToString()) + "</p>";
                        ItineraryInfo +=                    "</div>";
                        #endregion

                        #region pm-duration
                        //if (dtPNRDetails.Rows[SegCnt]["ElapsedTime"] != null)
                        //{
                        //    if (dtPNRDetails.Rows[SegCnt]["ElapsedTime"].ToString() != "")
                        //    {
                        ItineraryInfo +=                    "<div class='pm-duration'>" +
                                                                "<span class='pm-dur-time'>" /*+ dtPNRDetails.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[0] + "h " + dtPNRDetails.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[1]m*/ + "</span>" +
                                                                "<img src='Design/images/grey_durationicon.png' alt='Duration Icon'>" +
                                                                "<span class='pm-dur-text'></span>" +
                                                            "</div>";
                        //    }
                        //}
                        #endregion

                        #region pm-seg-to
                        ItineraryInfo +=                    "<div class='pm-seg-to'>" +
                                                                "<div class='pm-citi-code-time'>" + strArrCity.Split(',')[1] + " <span class='blue tootlTip' data-toggle='tooltip' title='" + strArrCity + "' data-original-title='" + strArrCity + "'>(" + dtPNRDetails.Rows[SegCnt]["Destination"].ToString() + ")</span> " + dtPNRDetails.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[0] + "" + dtPNRDetails.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[1] + ":" + dtPNRDetails.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[2] + "" + dtPNRDetails.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[3] + "</div>" +
                                                                "<p class='tootlTip' data-toggle='tooltip' title='" + strArrCity + "' data-original-title='" + strArrCity + "'>" + strArrCity + ",</p>" +
                                                                //"<p>San Francisco, CA, United States</p>" +
                                                                "<p>" + DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</p>";

                        ItineraryInfo +=                    "</div>";
                        #endregion

                        #region pm-fl-info
                        ItineraryInfo +=                    "<div class='pm-fl-info'>";

                        if (dtPNRDetails.Rows[SegCnt]["Dept_Terminal"] != null)
                        {
                            if (dtPNRDetails.Rows[SegCnt]["Dept_Terminal"].ToString() != "")
                            {
                                ItineraryInfo +=                "<p>Departure Terminal: " + dtPNRDetails.Rows[SegCnt]["Dept_Terminal"].ToString() + "</p>";
                            }
                        }
                        if (dtPNRDetails.Rows[SegCnt]["Arrival_Terminal"] != null)
                        {
                            if (dtPNRDetails.Rows[SegCnt]["Arrival_Terminal"].ToString() != "")
                            {
                                ItineraryInfo +=                "<p>Arrival Terminal: " + dtPNRDetails.Rows[SegCnt]["Arrival_Terminal"].ToString() + "</p>";
                            }
                        }
                        if (dtPNRDetails.Rows[SegCnt]["Equipment"] != null)
                        {
                            if (dtPNRDetails.Rows[SegCnt]["Equipment"].ToString() != "")
                            {
                                string EqpType = getEquipment(dtPNRDetails.Rows[SegCnt]["Equipment"].ToString());
                                if (EqpType != "")
                                {
                                    ItineraryInfo +=            "<p>" + EqpType + "</p>";
                                }
                            }
                        }
                        if (dtPNRDetails.Rows[SegCnt]["Airline_PNR"] != null)
                        {
                            if (dtPNRDetails.Rows[SegCnt]["Airline_PNR"].ToString() != "")
                            {
                                ItineraryInfo +=                "<p class='blue'>Airline PNR: " + dtPNRDetails.Rows[SegCnt]["Airline_PNR"].ToString() + "</p>";
                            }
                        }

                        ItineraryInfo +=                    "</div>";
                        #endregion

                        ItineraryInfo +=                "</div>";

                        #endregion
                        ItineraryInfo +=            "</div>";
                        ItineraryInfo +=        "</div>";


                    }
                    catch (Exception ec)
                    {

                    }
                }
                        
                ItineraryInfo += "<div class='mt-10'><span class='font12'> * All the times are local to Airports<span class='pm-right font12'>In-Flight services and amenities may vary and are subject to change.</span></div>";
                ItineraryInfo += "</div>";

                StrItineraryInfo1 = StrItineraryInfo1 + StrItineraryInfo;
                divItineraryInfo.InnerHtml = ItineraryInfo;
                divItineraryInfo.Attributes.Add("style", "Display:block");

                //if (isBooking)
                //    divItineraryInfoCancelled.InnerHtml += "" + StrItineraryInfo1 + "";
                //else if (isPnr)
                //    divItineraryInfoCompleted.InnerHtml += "" + StrItineraryInfo1 + "";
                //else { }
                //divItineraryInfoUpcoming.InnerHtml += "" + StrItineraryInfo1 + "";


                //if (tblBaggage.Rows.Count > 0)
                //{ }

                //divBaggageInfo.InnerHtml = StrBaggageInfo;
                //Session["divBaggageInfo"] = StrBaggageInfo;

            }
        }
        divPriceDetails.InnerHtml = "";
        divPriceDetails1.InnerHtml = "";
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Vouchers(string term, string minimumBasket, string trackid)
    {

        string ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ipaddress == "" || ipaddress == null)
            ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();
        string city = "";
        getIPCountryCode(ipaddress, out city);

        SqlParameter[] para_tbVoucher = {
                                            new SqlParameter("@voucherText", term),
                                            new SqlParameter("@searchType", "") ,
                                            new SqlParameter("@afid", ""),
                                            new SqlParameter("@validFrom", ""),
                                            new SqlParameter("@validTo", ""),
                                            new SqlParameter("@minimumBasket", minimumBasket),
                                            new SqlParameter("@Status", "y"),
                                            new SqlParameter("@IpCity", city)
                                        };
        DataTable dtVoucherCode = dbAccess.GetSpecificData_SP(dbAccess.connString, "[dbo].[Select_Deals]", para_tbVoucher);
        if (dtVoucherCode.Rows.Count > 0)
        {
            if (dtVoucherCode.Columns.Count > 1)
            {

                if (trackid != "")
                {
                    DataTable dttt = dbAccess.GetSpecificData("Select * from booking where device_id = '" + trackid + "' and Voucher_Code='" + term + "'", dbAccess.connString);
                    if (dttt != null)
                    {
                        if (dttt.Rows.Count > 0)
                        {
                            return "Promo Code Not valid";
                        }
                    }
                }



                if (dtVoucherCode.Rows[0]["Status"].ToString().ToUpper() != "N")
                {
                    term = "Voucher is valid for " + Math.Round(Convert.ToDouble(dtVoucherCode.Rows[0]["Voucher_Amount"]), 2) + " USD^" + Math.Round(Convert.ToDouble(dtVoucherCode.Rows[0]["Voucher_Amount"]), 2);
                }
                else
                {
                    term = "Promo Code already used";
                }
            }
            else
            {
                if (dtVoucherCode.Rows[0]["MinimumBasket"] != null)
                {
                    if (dtVoucherCode.Rows[0]["MinimumBasket"].ToString() != "")
                    {
                        term = "Minimum Cart Value is : " + String.Format("{0:0.00}", Convert.ToDouble(dtVoucherCode.Rows[0]["MinimumBasket"].ToString())) + " USD";
                    }
                }
            }
        }
        else
        {
            term = "Promo Code Not valid";
        }
        return term;
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


    protected void DisplayDataHotel(DataTable dtHotelBooking, DataTable dtPay)
    {

        double amountTT = 0;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string strBuild = "";
        DataTable strArrArp = GetDara(dtHotelBooking.Rows[0]["Destination"].ToString());
        if (strArrArp.Rows.Count > 0)
        {
            strBuild = strArrArp.Rows[0]["City"].ToString();
        }
        else
        {
            strBuild = dtHotelBooking.Rows[0]["Destination"].ToString();
        }
        if (dtPay.Rows[0]["GDS"].ToString() == "")
        {


            //divPassengerInfo -start

            string StrPassengerInfo = "";

            //pnlpassenger.Controls.Add(new LiteralControl("<table width='100%' border='0' cellspacing='1' cellpadding='1'>"));
            //pnlpassenger.Controls.Add(new LiteralControl("<tr>"));
            //pnlpassenger.Controls.Add(new LiteralControl("<td width='100%' height='26' align='left' valign='middle' bgcolor='#deedf8' class='indenttext'>Passenger Name(SurName  FirstName )</td>"));
            //pnlpassenger.Controls.Add(new LiteralControl("</tr>"));
            StrPassengerInfo += "<div class='col-md-12'>";
            StrPassengerInfo += "<h4><i class='fa fa-user'></i> Traveler Details</h4>";
            StrPassengerInfo += "</div>";


            int i = 1;
            string qrHotelPax = "select * from Hotel_Pax where HotelBooking_ID=" + Convert.ToInt64(dtHotelBooking.Rows[0]["HotelBooking_ID"]);
            DataTable dtHotelPax = dbAccess.GetSpecificData(qrHotelPax, ConfigurationManager.AppSettings["sqlcn"].ToString());
            foreach (DataRow drHotelPax in dtHotelPax.Rows)
            {
                //pnlpassenger.Controls.Add(new LiteralControl("<tr>"));
                //pnlpassenger.Controls.Add(new LiteralControl("<td height='25' align='left' valign='middle' bgcolor='#FFFFFF' class='textindentnormal'>" + drHotelPax["Pax_SurName"].ToString() + " " + drHotelPax["Pax_FirstName"].ToString() + "</td>"));
                //pnlpassenger.Controls.Add(new LiteralControl("</tr>"));

                StrPassengerInfo += "<div class='col-md-4'>";
                StrPassengerInfo += "<p>" + i + "." + drHotelPax["Pax_SurName"].ToString() + " " + drHotelPax["Pax_FirstName"].ToString() + "</p>";
                StrPassengerInfo += "</div>";
                i++;
            }
            divPassengerInfo.InnerHtml = "" + StrPassengerInfo + "";
            Session["divPassengerInfo"] = StrPassengerInfo;

            //divPassengerInfo -end

            amountTT = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
            //lbltotalPrice.Text = String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]);
            //HotelPrice_hidden.Value = amountTT.ToString();
        }


        double tx = 0;
        amountTT = amountTT + tx;


        //divItineraryInfo -start


        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'left'>" + dtHotelBooking.Rows[0]["HotelRfID"] + "</td>")); //hotel refid
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + dtHotelBooking.Rows[0]["Hotel_Name"].ToString() + "</td>")); //hotel name
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + strBuild + "</td>")); //destination
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'> " + dtHotelBooking.Rows[0]["HotelAddess"].ToString() + " </td>")); //location
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + DateTime.Parse(dtHotelBooking.Rows[0]["check_in_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddMMMyyyy").ToUpper() + "</td>")); //Flight Number
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + DateTime.Parse(dtHotelBooking.Rows[0]["check_out_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddMMMyyyy").ToUpper() + "</td>")); //Reservation Number
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + dtHotelBooking.Rows[0]["Hotel_Night_Count"] + "</td>")); //destination
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + dtHotelBooking.Rows[0]["Hotel_Room_Count"] + "</td></tr></table>")); //destination
        /*******************************************************************/
        string HotelRefenceCode_D = dtHotelBooking.Rows[0]["Hotel_Code"].ToString();
        var numAlpha_D = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
        var match_D = numAlpha_D.Match(HotelRefenceCode_D);
        var Source_Ref_D = match_D.Groups["Alpha"].Value;
        var Service_Ref_D = match_D.Groups["Numeric"].Value;

        string holidayERB = dtHotelBooking.Rows[0]["GDS"].ToString();
        Hotels_Service.ServiceSoapClient srv = new Hotels_Service.ServiceSoapClient();
        string response_HH = "";
        if (dbAccess.RequestType_Hotel == "TEST")
        {
            response_HH = srv.HotelDetails(dbAccess.H_User_Test, dbAccess.H_Pass_Test, Service_Ref_D, holidayERB, "TEST");
        }
        else if (dbAccess.RequestType_Hotel == "LIVE")
        {
            response_HH = srv.HotelDetails(dbAccess.H_User_Live, dbAccess.H_Pass_Live, Service_Ref_D, holidayERB, "LIVE");
        }

        SqlParameter[] paramDesc = { new SqlParameter("@HotelCode", Service_Ref_D), new SqlParameter("@Supplier", holidayERB) };
        DataTable dtHotelDescription = DataLayer.GetData("select_Hotel_Details", paramDesc, dbAccess.hotel_search_data);

        SqlParameter[] paramImages = { new SqlParameter("@HotelCode", Service_Ref_D), new SqlParameter("@Supplier", holidayERB) };
        DataTable dt_images = DataLayer.GetData("Hotel_ImageURLs_Select", paramImages, dbAccess.hotel_search_data);

        SqlParameter[] paramAmenities = { new SqlParameter("@HotelCode", Service_Ref_D), new SqlParameter("@Supplier", holidayERB) };
        DataTable dt_amenity = DataLayer.GetData("Hotel_Amenities_Select", paramAmenities, dbAccess.hotel_search_data);

        SqlParameter[] paramFecility = { new SqlParameter("@HotelCode", Service_Ref_D), new SqlParameter("@Supplier", holidayERB) };
        DataTable dt_fecility = DataLayer.GetData("Hotel_FacilityInfo_Select", paramAmenities, dbAccess.hotel_search_data);

        int imagesCount = 1;
        if (dt_images != null)
        {
            if (dt_images.Rows.Count > 0)
            {
                imagesCount = dt_images.Rows.Count;
            }
        }


        DateTime DateCheckIn = new DateTime();
        DateTime DateCheckOut = new DateTime();
        DateCheckIn = Convert.ToDateTime(dtHotelBooking.Rows[0]["check_in_date"].ToString());
        DateCheckOut = Convert.ToDateTime(dtHotelBooking.Rows[0]["check_out_date"].ToString());
        Double Duration = (DateCheckOut - DateCheckIn).TotalDays;
        Session["DEP_DAT"] = DateCheckIn.ToString();
        /*******************************************************************/

        string StrItineraryInfo = "";

        //StrItineraryInfo += "<div class='row'>";
        StrItineraryInfo += "<div class='col-md-12'>";

        StrItineraryInfo += "<div class='row'>";
        StrItineraryInfo += "<div class='col-md-8'> <h4><i class='fa fa-hotel'></i> Hotel details</h4></div>";
        StrItineraryInfo += "<div class='col-md-4 pull-right text-right'><h4><small>Total price</small> <strong class='text-success'>$<label id='lblItenaryTtPrice'>" + String.Format("{0:0.00}", amountTT + 0) + "</label></strong></h4></div>";
        StrItineraryInfo += "</div>";

        StrItineraryInfo += "<ul class='hotels-list'>";
        StrItineraryInfo += "<li>";
        StrItineraryInfo += "<div class='hotel-item'>";


        //Hotels_Service details -strat
        StrItineraryInfo += "<div class='row'>";

        StrItineraryInfo += "<div class='col-md-3'>";
        StrItineraryInfo += "<div class='hotel-item-img-wrap'>";
        StrItineraryInfo += "<img src='" + dtHotelDescription.Rows[0]["Image1"].ToString() + "' alt='Image Alternative text' title='" + dtHotelBooking.Rows[0]["Hotel_Name"].ToString() + "' class='img-responsive'>";
        StrItineraryInfo += "<div class='hotel-item-img-num'><i class='fa fa-picture-o'></i> " + imagesCount + "</div>";
        StrItineraryInfo += "</div>";
        StrItineraryInfo += "</div>";
        StrItineraryInfo += "<div class='col-md-6'>";
        StrItineraryInfo += "<h3 class='hotel-item-title'>" + dtHotelBooking.Rows[0]["Hotel_Name"].ToString();
        StrItineraryInfo += "<ul class='hotel-icon-group hotel-item-rating-stars'>";
        string strRatng = "";
        try
        {
            for (int i = 0; i < Convert.ToInt32(dtHotelBooking.Rows[0]["Category_Code"].ToString()); i++)
            {
                strRatng += "<li><i class='fa fa-star'></i></li>";
            }
        }
        catch
        {
            strRatng = "";
        }

        //StrItineraryInfo += "<li><i class='fa fa-star'></i></li>";
        //StrItineraryInfo += "<li><i class='fa fa-star'></i></li>";
        //StrItineraryInfo += " <li><i class='fa fa-star'></i></li>";
        //StrItineraryInfo += " <li><i class='fa fa-star'></i></li>";
        //StrItineraryInfo += "<li><i class='fa fa-star-o'></i></li>";
        StrItineraryInfo += "</ul></h3>";
        StrItineraryInfo += "<p class='hotel-item-address'><i class='fa fa-map-marker'></i>" + dtHotelDescription.Rows[0]["Region"].ToString() + "</p>";
        StrItineraryInfo += " <p class='hotel-item-type'>" + dtHotelBooking.Rows[0]["Hotel_Night_Count"].ToString() + " Nights | " + dtHotelBooking.Rows[0]["Board_Type"].ToString() + "</p>";
        StrItineraryInfo += "<p class='hotel-item-type'><strong>Room Type :</strong> " + dtHotelBooking.Rows[0]["Room_Type"].ToString() + "</p>";
        StrItineraryInfo += "<p class='hotel-item-type'><strong><i class='fa fa-clock-o'></i> Check in :</strong> " + DateCheckIn.ToString("dd/MM/yyyy ") + "  ";
        StrItineraryInfo += "<strong class='padlft'><i class='fa fa-clock-o'></i> Check out :</strong> " + DateCheckOut.ToString("dd/MM/yyyy") + "</p>";
        StrItineraryInfo += "</div>";

        //StrItineraryInfo += "<div class='col-md-3 text-right'>";
        //StrItineraryInfo += "<span class=' hotel-item-rating-number'>";
        // StrItineraryInfo += "<strong class='text-success'><i class='fa fa-thumbs-o-up'></i> Superb! 4.6/5</strong>";
        //StrItineraryInfo += " <small>(944 reviews)</small> </span>";
        //StrItineraryInfo += "</div>";      

        StrItineraryInfo += "</div>";
        //Hotels_Service details -end



        //Hotels_Service Collaspe Tab -strat
        StrItineraryInfo += "<div class='row'>";
        StrItineraryInfo += "<div class='col-md-12'>";

        StrItineraryInfo += "<div class='row'>";
        StrItineraryInfo += "<div class='col-md-12'>";
        StrItineraryInfo += " <a class='accordion-toggle collapsed text-grey btn btn-default btn-sm pull-right' data-toggle='collapse' data-parent='#accordion1' data-target='#HotelDetails'>";
        StrItineraryInfo += " <i class='fa fa-plus'></i> Hotel Details</a>";
        StrItineraryInfo += "</div>";
        StrItineraryInfo += "</div>";

        /////////////////////
        StrItineraryInfo += "<div id='HotelDetails' class='panel-collapse collapse mrgtop'>";

        //main image -start
        StrItineraryInfo += "<div class='row'>";
        StrItineraryInfo += "<div class='col-md-12' id='slider'>";

        StrItineraryInfo += "<div id='carousel-bounding-box'>";
        StrItineraryInfo += "<div id='myCarousel' class='carousel slide'>";

        StrItineraryInfo += "<div class='carousel-inner'>";

        bool imgTbl = false;
        if (dt_images != null)
        {
            if (dt_images.Rows.Count > 0)
            {
                for (int iCnt = 0; iCnt < dt_images.Rows.Count; iCnt++)
                {
                    if (iCnt == 0)
                        StrItineraryInfo += "<div class='active item' data-slide-number='" + iCnt + "'>";
                    else
                        StrItineraryInfo += "<div class='item' data-slide-number='" + iCnt + "'>";
                    StrItineraryInfo += "<img src='" + dt_images.Rows[iCnt]["ImageUrl"].ToString() + "' class='img-responsive carousel-lg-img'>";
                    StrItineraryInfo += "</div>";
                }
                imgTbl = true;
            }
        }
        if (!imgTbl)
        {
            if (dtHotelDescription.Rows.Count > 0)
            {
                for (int iCnt = 0; iCnt < dtHotelDescription.Rows.Count; iCnt++)
                {
                    if (dtHotelDescription.Rows[iCnt]["Image" + (iCnt + 1)] != null)
                    {
                        if (iCnt == 0)
                            StrItineraryInfo += "<div class='active item' data-slide-number='" + iCnt + "'>";
                        else
                            StrItineraryInfo += "<div class='item' data-slide-number='" + iCnt + "'>";
                        StrItineraryInfo += "<img src='" + dtHotelDescription.Rows[iCnt]["Image" + (iCnt + 1)].ToString() + "' class='img-responsive carousel-lg-img'>";
                        StrItineraryInfo += "</div>";

                    }
                }
            }
        }


        StrItineraryInfo += "</div>";
        StrItineraryInfo += " <a class='carousel-control left' href='#myCarousel' data-slide='prev'>‹</a>";

        StrItineraryInfo += "<a class='carousel-control right' href='#myCarousel' data-slide='next'>›</a>";
        StrItineraryInfo += "</div>";
        StrItineraryInfo += " </div>";

        StrItineraryInfo += " </div> ";
        StrItineraryInfo += "</div>";
        //main image -end

        //small image -start
        StrItineraryInfo += "<div class='row'>";
        StrItineraryInfo += "<div class='col-md-12 hidden-sm hidden-xs text-center' id='slider-thumbs'>";


        StrItineraryInfo += "<ul class='list-inline'>";

        bool imgTbl1 = false;
        if (dt_images != null)
        {
            if (dt_images.Rows.Count > 0)
            {
                for (int iCnt = 0; iCnt < dt_images.Rows.Count; iCnt++)
                {
                    if (iCnt == 0)
                        StrItineraryInfo += "<li> <a id='carousel-selector-" + iCnt + "' class='selected'>";
                    else
                        StrItineraryInfo += "<li> <a id='carousel-selector-" + iCnt + "'>";
                    StrItineraryInfo += " <img src='" + dt_images.Rows[iCnt]["ImageUrl"].ToString() + "' class='img-responsive carousel-sm-thumb'>";
                    StrItineraryInfo += "</a></li>";
                }
                imgTbl1 = true;
            }
        }
        if (!imgTbl1)
        {
            if (dtHotelDescription.Rows.Count > 0)
            {
                for (int iCnt = 0; iCnt < dtHotelDescription.Rows.Count; iCnt++)
                {
                    if (dtHotelDescription.Rows[iCnt]["Image" + (iCnt + 1)] != null)
                    {
                        if (iCnt == 0)
                            StrItineraryInfo += "<li> <a id='carousel-selector-" + iCnt + "' class='selected'>";
                        else
                            StrItineraryInfo += "<li> <a id='carousel-selector-" + iCnt + "'>";
                        StrItineraryInfo += " <img src='" + dtHotelDescription.Rows[iCnt]["Image" + (iCnt + 1)].ToString() + "' class='img-responsive carousel-sm-thumb'>";
                        StrItineraryInfo += "</a></li>";
                    }
                }
            }
        }
        //StrItineraryInfo += "<li> <a id='carousel-selector-0' class='selected'>";
        //StrItineraryInfo += " <img src='http://placehold.it/80x60&amp;text=one' class='img-responsive carousel-sm-thumb'>";
        //StrItineraryInfo += "</a></li>";
        //StrItineraryInfo += "<li> <a id='carousel-selector-1'>";
        StrItineraryInfo += "</ul>";

        StrItineraryInfo += "</div>";
        StrItineraryInfo += "</div>";

        //small image -start

        if (dtHotelDescription.Rows[0]["Description"] != null || dtHotelDescription.Rows[0]["Additional_Description"] != null || dtHotelDescription.Rows[0]["Location_Description"] != null || dtHotelDescription.Rows[0]["Room_Description"] != null || dtHotelDescription.Rows[0]["Sport_Description"] != null || dt_amenity != null || dtHotelDescription.Rows[0]["Latitude"] != null || dtHotelDescription.Rows[0]["Longitude"] != null)
        {
            if (dtHotelDescription.Rows[0]["Description"].ToString() != "" || dtHotelDescription.Rows[0]["Additional_Description"].ToString() != "" || dtHotelDescription.Rows[0]["Location_Description"].ToString() != "" || dtHotelDescription.Rows[0]["Room_Description"].ToString() != "" || dtHotelDescription.Rows[0]["Sport_Description"].ToString() != "" || dt_amenity.Rows.Count > 0 || dtHotelDescription.Rows[0]["Latitude"] != "" || dtHotelDescription.Rows[0]["Longitude"] != "")
            {

                //description,amenities,map -start
                StrItineraryInfo += "<div class='row'>";
                StrItineraryInfo += "<div class='col-md-12'>";
                StrItineraryInfo += "<h4>About " + dtHotelBooking.Rows[0]["Hotel_Name"].ToString() + " </h4>";
                StrItineraryInfo += "<hr></hr>";

                StrItineraryInfo += "<div class='mediabox'>";
                if (dtHotelDescription.Rows[0]["Description"] != null)
                {
                    if (dtHotelDescription.Rows[0]["Description"].ToString() != "")
                    {
                        StrItineraryInfo += "<strong>Description:</strong>";
                        StrItineraryInfo += "<p>" + dtHotelDescription.Rows[0]["Description"].ToString() + "</p>";
                    }
                }
                if (dtHotelDescription.Rows[0]["Additional_Description"] != null)
                {
                    if (dtHotelDescription.Rows[0]["Additional_Description"].ToString() != "")
                    {
                        StrItineraryInfo += "<strong>Additional Description:</strong>";
                        StrItineraryInfo += "<p>" + dtHotelDescription.Rows[0]["Additional_Description"].ToString() + "</p>";
                    }
                }
                if (dtHotelDescription.Rows[0]["Location_Description"] != null)
                {
                    if (dtHotelDescription.Rows[0]["Location_Description"].ToString() != "")
                    {
                        StrItineraryInfo += "<strong>Location Description:</strong>";
                        StrItineraryInfo += "<p>" + dtHotelDescription.Rows[0]["Location_Description"].ToString() + "</p>";
                    }
                }
                if (dtHotelDescription.Rows[0]["Room_Description"] != null)
                {
                    if (dtHotelDescription.Rows[0]["Room_Description"].ToString() != "")
                    {
                        StrItineraryInfo += "<strong>Room Description:</strong>";
                        StrItineraryInfo += "<p>" + dtHotelDescription.Rows[0]["Room_Description"].ToString() + "</p>";
                    }
                }
                if (dtHotelDescription.Rows[0]["Sport_Description"] != null)
                {
                    if (dtHotelDescription.Rows[0]["Sport_Description"].ToString() != "")
                    {
                        StrItineraryInfo += "<strong>Sport Description:</strong>";
                        StrItineraryInfo += "<p>" + dtHotelDescription.Rows[0]["Sport_Description"].ToString() + "</p>";
                    }
                }

                if (dt_amenity != null)
                {
                    if (dt_amenity.Rows.Count > 0)
                    {
                        //Amenities -start
                        StrItineraryInfo += "<div class='row'>";
                        StrItineraryInfo += "<div class='col-md-12'>";
                        StrItineraryInfo += "<strong class='pull-left'><i class='fa fa-bed'></i>Amenities</strong>";
                        StrItineraryInfo += "</div>";
                        int ccc = 0;
                        for (int amtC = 0; amtC < dt_amenity.Rows.Count; amtC++)
                        {
                            if (ccc <= 4)
                            {
                                if (ccc == 0)
                                    StrItineraryInfo += "<div class='col-md-4'><ul>";
                                StrItineraryInfo += "<li><p><i class='fa fa-check text-success spacert'></i> " + dt_amenity.Rows[amtC]["HotelAmenity"].ToString() + "</p></li>";
                                ccc++;
                                if (ccc == 4 || amtC == dt_amenity.Rows.Count)
                                {
                                    ccc = 0;
                                    StrItineraryInfo += "</ul>";
                                    StrItineraryInfo += "</div>";
                                }

                            }
                        }
                        StrItineraryInfo += "</div>";
                        //Amenities -end
                    }
                }


                StrItineraryInfo += "</div>";

                //map -start
                //StrItineraryInfo += " <div class='row'>";
                //StrItineraryInfo += "<div class='col-md-12'>";
                //StrItineraryInfo += "<h4><i class='fa fa-map-marker'></i> Map</h4>";
                //StrItineraryInfo += "<div id='map-canvas'></div>";
                //StrItineraryInfo += "</div>";
                //StrItineraryInfo += "</div>";
                //map -end




                StrItineraryInfo += "</div>";
                StrItineraryInfo += "</div>";

                //description,amenities,map -end
            }
        }

        StrItineraryInfo += "</div>";
        ////////////////////

        StrItineraryInfo += "</div>";
        StrItineraryInfo += "</div>";
        //Hotels_Service Collaspe Tab -end

        StrItineraryInfo += "</div>";
        StrItineraryInfo += "</li>";
        StrItineraryInfo += "</ul>";



        //StrItineraryInfo += "</div>";
        StrItineraryInfo += "</div>";



        divItineraryInfo.InnerHtml = StrItineraryInfo;
        Session["divItineraryInfo"] = StrItineraryInfo;

        //divItineraryInfo -end


        //divBookingId -start

        string StrBookingId = "";

        StrBookingId += "<div class='col-md-12'>";
        StrBookingId += "<h4 class='blue-text' style='margin-top:0px; margin-bottom:0px;'>Hotel Booking reference: " + HotelRefId + "</h4>";
        StrBookingId += "</div>";

        divBookingId.InnerHtml = "" + StrBookingId + "";
        Session["divBookingId"] = StrBookingId;
        //divBookingId -end










        ////<!-- start of Flights--> 
        //pnlhotelinfo.Controls.Add(new LiteralControl("<table width='942' border='0' cellpadding='1' cellspacing='1'><tr>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width = '70' height='35' align='left' valign='middle' bgcolor='#deedf8' class='indenttext'>Hotel RefId</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='165' align='center' valign='middle' bgcolor='#deedf8' class='indenttext'>Hotel Name</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='62' align='center' valign='middle' bgcolor='#deedf8' class='indenttext'>Destination</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='195' align='center' valign='middle' bgcolor='#deedf8' class='indenttext'>Location</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='60' align='center' valign='middle' bgcolor='#deedf8' class='indenttext'>CheckInDate</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='60' align='left' valign='middle' bgcolor='#deedf8' class='indenttext'>CheckOutDate</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='75' align='center' valign='middle' bgcolor='#deedf8' class='indenttext'>Number Of Night(s)</td>"));
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td width='60' align='center' valign='middle' bgcolor='#deedf8' class='indenttext'>Number Of Room(s)</td></tr>"));

        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'left'>" + dtHotelBooking.Rows[0]["HotelRfID"] + "</td>")); //hotel refid
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + dtHotelBooking.Rows[0]["Hotel_Name"].ToString() + "</td>")); //hotel name
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + strBuild + "</td>")); //destination
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'> " + dtHotelBooking.Rows[0]["HotelAddess"].ToString() + " </td>")); //location
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + DateTime.Parse(dtHotelBooking.Rows[0]["check_in_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddMMMyyyy").ToUpper() + "</td>")); //Flight Number
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + DateTime.Parse(dtHotelBooking.Rows[0]["check_out_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddMMMyyyy").ToUpper() + "</td>")); //Reservation Number
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + dtHotelBooking.Rows[0]["Hotel_Night_Count"] + "</td>")); //destination
        //pnlhotelinfo.Controls.Add(new LiteralControl("<td bgcolor='#FFFFFF' class='textindentnormal' align = 'center'>" + dtHotelBooking.Rows[0]["Hotel_Room_Count"] + "</td></tr></table>")); //destination

    }

    protected void DisplayData_1S(string resultPNR1, DataTable dtPay)   
    {
        DataTable tblPax = CreatePaxTable();
        DataTable tblitinerary = CreateItinTable();

        string PNR = "";



        int start = resultPNR1.IndexOf("<TravelItineraryReadRS");
        int end = resultPNR1.IndexOf("</TravelItineraryReadRS>");

        resultPNR1 = resultPNR1.Substring(start, end - start) + "</TravelItineraryReadRS>";

        resultPNR1 = resultPNR1.Replace("CustomerName", "PersonName");

        TravelItineraryReadRS objTravelItineraryReadRS = TravelItineraryReadRS_XML.ResXml(resultPNR1);


        if (objTravelItineraryReadRS != null)
        {
            if (objTravelItineraryReadRS.TravelItinerary != null)
            {




                if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo != null)
                {
                    if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName.Length > 0)
                        {
                            for (int paxcnt = 0; paxcnt < objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName.Length; paxcnt++)
                            {
                                DataRow drPax = tblPax.NewRow();
                                //if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType == "INF" || objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType == "JNF")
                                //{
                                drPax["Title"] = "";
                                //}
                                //else
                                //{
                                //    drPax["Title"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString().Split(' ')[1];
                                //}
                                drPax["LastName"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].Surname.ToString();
                                drPax["FirstName"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString();
                                drPax["PaxType"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType;

                                tblPax.Rows.Add(drPax);
                            }
                        }
                        else
                        {
                            RedirectCS();
                        }
                    }
                    else
                    {
                        RedirectCS();
                    }
                }
                else
                {
                    RedirectCS();
                }


                if (objTravelItineraryReadRS.TravelItinerary.ItineraryRef != null)
                {
                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryRef.ID != null)
                    {
                        PNR = objTravelItineraryReadRS.TravelItinerary.ItineraryRef.ID;
                    }
                }


                /***********************************************/
                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[0].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length > 0)
                {
                    for (int ssgCnt = 0; ssgCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[0].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length; ssgCnt++)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[0].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[ssgCnt].ConnectionInd == "O")
                        {

                        }
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[0].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[ssgCnt].ConnectionInd == "X")
                        {

                        }
                    }
                }
                /***********************************************/


                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo != null)
                {
                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length > 0)
                        {
                            for (int segno = 0; segno < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length; segno++)
                            {

                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment != null)
                                {

                                    DataRow drSegment = tblitinerary.NewRow();

                                    //objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.

                                    drSegment["PNR_ID"] = PNR;
                                    drSegment["Segment_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SegmentNumber;
                                    drSegment["Airline"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].MarketingAirline.Code;
                                    drSegment["Flight_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].MarketingAirline.FlightNumber;
                                    drSegment["Codeshare_Airline"] = "";
                                    drSegment["Class"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ResBookDesigCode;
                                    drSegment["Departure"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
                                    drSegment["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;


                                    string dept_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[0];
                                    string dept_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[1];

                                    string arr_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[0];
                                    string arr_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[1];



                                    string[] strSplitDepDate = dept_date.Split('-');
                                    string[] strSplitArrDate = arr_date.Split('-');
                                    int ArrDateYear = Convert.ToInt32(strSplitDepDate[0]);
                                    if (strSplitDepDate[1] == "12" && strSplitDepDate[2] == "31" && strSplitArrDate[0] == "01" && strSplitArrDate[1] == "01")
                                    {
                                        ArrDateYear += 1;
                                    }


                                    drSegment["Dept_date"] = dept_date;
                                    drSegment["Dept_time"] = dept_time.Replace(":", "");
                                    drSegment["Arrival_date"] = ArrDateYear + "-" + arr_date;
                                    drSegment["Arrival_time"] = arr_time.Replace(":", "");

                                    TimeSpan ts = (DateTime.Parse(arr_date.Substring(0, 2) + "/" + arr_date.Substring(3, 2) + "/" + ArrDateYear, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))) - DateTime.Parse(dept_date.Substring(5, 2) + "/" + dept_date.Substring(8, 2) + "/" + dept_date.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));


                                    /*********************/
                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ElapsedTime != null)
                                    {
                                        drSegment["ElapsedTime"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ElapsedTime.ToString();
                                    }

                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal != null)
                                    {
                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal.Length > 0)
                                        {
                                            drSegment["MealTypes"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal[0].Code.ToString();
                                        }
                                    }
                                    /*********************/



                                    drSegment["Nextday"] = ts.Days;

                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Equipment != null)
                                    {
                                        drSegment["Equipment"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Equipment.AirEquipType;
                                    }
                                    else
                                    {
                                        drSegment["Equipment"] = "";

                                    }
                                    /****************************************/
                                    drSegment["Baggage"] = "";
                                    drSegment["Dept_Terminal"] = "";
                                    drSegment["Arrival_Terminal"] = "";


                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.TerminalCode != null)
                                        drSegment["Dept_Terminal"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.TerminalCode.ToString();
                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.TerminalCode != null)
                                        drSegment["Arrival_Terminal"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.TerminalCode.ToString();

                                    /********************************************/


                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef != null)
                                    {
                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID != null)
                                        {
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID != "")
                                            {

                                                string[] Airline_PNRSplit = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID.Split('*');
                                                if (Airline_PNRSplit.Length > 1)
                                                {
                                                    drSegment["Airline_PNR"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID.Split('*')[1];
                                                }
                                                else
                                                {
                                                    drSegment["Airline_PNR"] = "";

                                                }
                                            }
                                            else
                                            {
                                                drSegment["Airline_PNR"] = "";

                                            }
                                        }
                                        else
                                        {
                                            drSegment["Airline_PNR"] = "";

                                        }
                                    }
                                    else
                                    {
                                        drSegment["Airline_PNR"] = "";

                                    }
                                    drSegment["StopOvers"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].StopQuantity;


                                    tblitinerary.Rows.Add(drSegment);
                                }
                            }
                        }
                        else
                        {
                            RedirectCS();
                        }
                    }
                    else
                    {
                        RedirectCS();
                    }
                }
                else
                {
                    RedirectCS();
                }


            }
            else
            {
                RedirectCS();
            }
        }
        else
        {
            RedirectCS();
        }


        //calculating Total pax, Adult, Child, Infant -start
        int TotalPaxCnt = 0;
        if (tblPax != null)
        {
            if (tblPax.Rows.Count > 0)
            {
                TotalPaxCnt = tblPax.Rows.Count;
            }
        }
        //calculating Total pax, Adult, Child, Infant -end



        //New York,United States
        string departure = "";
        //Bangkok,Thailand
        string destination = "";
        //<time class='comment-date' datetime='07/14/2015'><i class='fa fa-clock-o'></i>Jul 14 , 2015</time> <i class='fa fa-arrow-circle-right'></i><time class='comment-date padlft-rgt' datetime='07/16/2015'><i class='fa fa-clock-o'></i> Jul 16 , 2015</time>
        string depDestTimings = "";



        //divItineraryInfo -strat


        string strFlyFrom = "";
        string strFlyTo = "";
        string strDepDate = "";
        string strRetDate = "";

        string StrItineraryInfo = "";

        if (tblitinerary != null)
        {
            if (tblitinerary.Rows.Count > 0)
            {

                StrItineraryInfo += "<div class='col-md-12'>";

                StrItineraryInfo += "<div class='row'>";
                StrItineraryInfo += "<div class='col-md-8'> <h4><img src='Design/images/passenger-icon.png' alt='Passenger icon' class='title-icons'> Flight details</h4></div>";
                StrItineraryInfo += "<div class='col-md-4 pull-right text-right'><h4 style='margin-bottom:0px;'><small>Total price</small> <strong class='text-success'>$" + String.Format("{0:0.00}", TtAmount) + "</strong></h4>";
                StrItineraryInfo += "<small style='font-size:9px;'>inc. of all taxes</small></div>";
                StrItineraryInfo += "</div>";
                StrItineraryInfo += "<hr></hr>";

                StrItineraryInfo += "<div class='tbl-info'>";
                //foreach (DataRow drItinerary in tblitinerary.Rows)
                //{

                string OutBoundHeader = "";
                string InBoundHeader = "";
                string depC = "";

                for (int SegCnt = 0; SegCnt < tblitinerary.Rows.Count; SegCnt++)
                {

                    /*****************************************/

                    DataTable dtFlight = FlightCode(tblitinerary.Rows[SegCnt]["Airline"].ToString());
                    string strAirline = "";
                    string strAirlineimg = "";
                    if (dtFlight.Rows.Count > 0)
                    {
                        strAirline = dtFlight.Rows[0]["AirlineShortName"].ToString();
                        strAirlineimg = "images/airline-logos/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";

                    }
                    else
                    {
                        strAirline = tblitinerary.Rows[SegCnt]["Airline"].ToString();
                        strAirlineimg = "images/airline-logos/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";
                    }

                    DataTable dtDepArp = GetDara(tblitinerary.Rows[SegCnt]["Departure"].ToString());
                    string strDepCity = "";
                    string strDepCountry = "";
                    if (dtDepArp.Rows.Count > 0)
                    {
                        strDepCity = dtDepArp.Rows[0]["City"].ToString() + ", " + dtDepArp.Rows[0]["Airport"].ToString();
                        strDepCountry = dtDepArp.Rows[0]["Country"].ToString();
                    }
                    else
                    {
                        strDepCity = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                        strDepCountry = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                    }
                    string dtDay1 = "";
                    string dtMonth1 = "";
                    string dtYear1 = "";
                    string dtDay2 = "";
                    string dtMonth2 = "";
                    string dtYear2 = "";
                    dtDay1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(8, 2);
                    dtMonth1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(5, 2);
                    dtYear1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(0, 4);

                    dtDay2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(8, 2);
                    dtMonth2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(5, 2);
                    dtYear2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(0, 4);



                    DataTable dtArrArp = GetDara(tblitinerary.Rows[SegCnt]["Destination"].ToString());
                    string strArrCity = "";
                    string strArrCountry = "";
                    if (dtArrArp.Rows.Count > 0)
                    {
                        strArrCity = dtArrArp.Rows[0]["City"].ToString() + ", " + dtArrArp.Rows[0]["Airport"].ToString();
                        strArrCountry = dtArrArp.Rows[0]["Country"].ToString();

                    }
                    else
                    {
                        strArrCity = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                        strArrCountry = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                    }



                    /*****************************************/
                    if (SegCnt > 0)
                    {

                        if (depC == tblitinerary.Rows[SegCnt]["Destination"].ToString())
                        {
                            StrItineraryInfo += "<div class='thead-main thead-main-dep'>";
                            StrItineraryInfo += "<div class='th'>";
                            StrItineraryInfo += "<h4 class='flight-head'><i class='fa fa-plane'></i> In Bound</h4>";
                            StrItineraryInfo += "<p class='time-stop-diff'></p>";
                            StrItineraryInfo += "</div>";
                            StrItineraryInfo += "</div>";

                            StrItineraryInfo += "<div class='fl-table'>";
                            StrItineraryInfo += "<div class='fl-tr sub-thead'>";
                            StrItineraryInfo += "<div class='fl-th col-flight'>Flight</div>";
                            StrItineraryInfo += "<div class='fl-th col-departure'>Depart</div>";
                            StrItineraryInfo += "<div class='fl-th col-arrival '>Arrive</div>";
                            StrItineraryInfo += "<div class='fl-th col-info'>Info</div>";
                            StrItineraryInfo += "</div>";
                            StrItineraryInfo += "</div>";
                        }
                    }
                    depC = tblitinerary.Rows[SegCnt]["Departure"].ToString();

                    if (SegCnt == 0)
                    {

                        strFlyFrom = strDepCity;
                        strDepDate = DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();
                        strRetDate = DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();


                        StrItineraryInfo += "<div class='thead-main thead-main-dep'>";
                        StrItineraryInfo += "<div class='th'>";
                        StrItineraryInfo += "<h4 class='flight-head'><i class='fa fa-plane'></i> Out Bound</h4>";
                        StrItineraryInfo += "<p class='time-stop-diff'>14hrs 30min - 1 stop - Same Day Arrival</p>";
                        StrItineraryInfo += "</div>";
                        StrItineraryInfo += "</div>";

                        StrItineraryInfo += "<div class='fl-table'>";
                        StrItineraryInfo += "<div class='fl-tr sub-thead'>";
                        StrItineraryInfo += "<div class='fl-th col-flight'>Flight</div>";
                        StrItineraryInfo += "<div class='fl-th col-departure'>Depart</div>";
                        StrItineraryInfo += "<div class='fl-th col-arrival '>Arrive</div>";
                        StrItineraryInfo += "<div class='fl-th col-info'>Info</div>";
                        StrItineraryInfo += "</div>";
                        StrItineraryInfo += "</div>";
                    }

                    if (SegCnt > 0 && SegCnt < tblitinerary.Rows.Count)
                    {
                        //Stop Over -start
                        StrItineraryInfo += "<div class='row'>";
                        StrItineraryInfo += "<div class='col-md-12'>";
                        StrItineraryInfo += "<div class='flight-connection'>";
                        StrItineraryInfo += "<div class='flight-connection-row'>";
                        StrItineraryInfo += "<div class='flight-connec-layover'>";
                        StrItineraryInfo += "<span class='flight-layover-brd'>";
                        StrItineraryInfo += "<span class='spacert'><i class='fa fa-clock-o'></i> Layover: 17hr 35min</span>";
                        //StrItineraryInfo += "<span class='text-danger spacert'><i class='fa fa-plane'></i> Flight Change</span>";
                        //StrItineraryInfo += "<span class='text-danger'><i class='fa fa-moon-o'></i> Overnight Required</span>";
                        StrItineraryInfo += "</span>";
                        StrItineraryInfo += "</div>";
                        StrItineraryInfo += "</div>";
                        StrItineraryInfo += "</div>";
                        StrItineraryInfo += "</div>";
                        StrItineraryInfo += "</div>";
                        //Stop Over -end
                    }



                    //segment -star
                    StrItineraryInfo += "<div class='row'>";
                    StrItineraryInfo += "<div class='col-md-12'>";
                    StrItineraryInfo += "<div class='sigment1 seg1'>";
                    StrItineraryInfo += "<div class='flight-list-box-wrap'>";
                    StrItineraryInfo += "<div class='airlinelogo'>";
                    StrItineraryInfo += "<div class='flight-airlinelogo'>";
                    StrItineraryInfo += "<img src='" + strAirlineimg + "' class='img-responsive img-thumbnail'>";
                    StrItineraryInfo += "</div>";
                    StrItineraryInfo += "<span>" + strAirline + " </span>";
                    StrItineraryInfo += "<p>" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + String.Format("{0:0000}", Convert.ToInt32(tblitinerary.Rows[SegCnt]["Flight_No"].ToString())) + "</p>";
                    StrItineraryInfo += "</div>";
                    StrItineraryInfo += "<div class='direction' style='width:63%;'>";
                    StrItineraryInfo += "<div class='departure'>";
                    StrItineraryInfo += "<span class='travel-dates'>" + strDepCity + "</span>";
                    StrItineraryInfo += "<h5><a data-original-title='" + strDepCity + "' href='#' data-toggle='tooltip' title=''>" + tblitinerary.Rows[SegCnt]["Departure"].ToString() + "</a>";
                    StrItineraryInfo += "<span class=''> " + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[3] + "</span></h5>";
                    StrItineraryInfo += "<span class='travel-dates'>" + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</span>";
                    StrItineraryInfo += "</div>";

                    StrItineraryInfo += "<span class='arrow'>→</span>";

                    StrItineraryInfo += "<div class='arrive'>";
                    StrItineraryInfo += "<span class='travel-dates'>" + strArrCity + "</span>";
                    StrItineraryInfo += "<h5><a data-original-title='" + strArrCity + "' href='#' data-toggle='tooltip' title=''>" + tblitinerary.Rows[SegCnt]["Destination"].ToString() + "</a>";
                    StrItineraryInfo += "<span class=''> " + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[3] + "</span></h5>";
                    StrItineraryInfo += "<span class='travel-dates'>" + DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</span>";
                    StrItineraryInfo += "</div>";
                    StrItineraryInfo += "</div>";

                    StrItineraryInfo += "<div class='flt-baggage'>";
                    if (tblitinerary.Rows[SegCnt]["ElapsedTime"] != null)
                    {
                        if (tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString() != "")
                            StrItineraryInfo += "<span class='amenties-span'>" + tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[0] + "h " + tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[1] + "m</span>";
                    }
                    if (tblitinerary.Rows[SegCnt]["Dept_Terminal"] != null)
                    {
                        if (tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() != "")
                            StrItineraryInfo += "<span class='amenties-span'>Departure Terminal: " + tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() + "</span>";
                    }
                    if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"] != null)
                    {
                        if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() != "")
                            StrItineraryInfo += "<span class='amenties-span'>Arrival Terminal: " + tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() + "</span>";
                    }
                    if (tblitinerary.Rows[SegCnt]["Equipment"] != null)
                    {
                        if (tblitinerary.Rows[SegCnt]["Equipment"].ToString() != "")
                        {
                            string EqpType = getEquipment(tblitinerary.Rows[SegCnt]["Equipment"].ToString());
                            if (EqpType != "")
                            {
                                StrItineraryInfo += "<span class='amenties-span'>" + EqpType + "</span>";
                            }
                        }
                    }
                    if (tblitinerary.Rows[SegCnt]["MealTypes"] != null)
                    {
                        if (tblitinerary.Rows[SegCnt]["MealTypes"].ToString() != "")
                        {
                            string MealType = getMealTypes(tblitinerary.Rows[SegCnt]["MealTypes"].ToString());
                            if (MealType != "")
                                StrItineraryInfo += "<span class='amenties-span'>Meals Types:" + tblitinerary.Rows[SegCnt]["MealTypes"].ToString() + "</span>";
                        }
                    }
                    if (tblitinerary.Rows[SegCnt]["Airline_PNR"] != null)
                    {
                        if (tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() != "")
                            StrItineraryInfo += "<span class='amenties-span blue-text'>Airline PNR: " + tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() + "</span>";
                    }
                    StrItineraryInfo += "</div>";

                    StrItineraryInfo += "</div>";
                    StrItineraryInfo += "</div>";
                    StrItineraryInfo += "</div>";
                    StrItineraryInfo += "</div>";
                    //segment -end                    
                }

                ////note -start                
                //StrItineraryInfo += "<div class='thead-main thead-main-arr'>";
                //StrItineraryInfo += "<div class='th'>";
                //StrItineraryInfo += "<strong class='pull-left'>* All the times are local to Airports</strong>";
                //StrItineraryInfo += "</div>";
                //StrItineraryInfo += "</div>";                
                ////note -end


                ////Fare Rules -Start

                //<div class='airfare-costs'>
                //                <h4><i class='fa fa-dollar'></i> Fare Rules:</h4>

                //                <div class='text-justify'>
                //                <p><h5>Cancellation Fee</h5>
                //                Airline Fee* - This is a non-refundable ticket.<br>
                //                TravelMerry Fee - USD 50 per passenger.<br>
                //                Partly utilized tickets cannot be cancelled.</p>

                //                <p><h5>Change Fee</h5>
                //                Airline Fee* - USD 167 per passenger + fare difference (if applicable).<br>
                //                TravelMerry Fee - USD 50 per passenger.</p>

                //                <p class='text-justify'>*Airlines stop accepting cancellation/change requests 4 - 72 hours before departure of the flight, depending on the airline. The airline fee is indicative based on an automated interpretation of airline fare rules. TravelMerry doesn't guarantee the accuracy of this information. The change/cancellation fee may also vary based on fluctuations in currency conversion rates. For exact cancellation/change fee, please call us at our customer care number. </p>
                //               </div>






                //               <div class='text-justify'>
                //               <h5>Booking Rules:</h5>
                //               <p><h5>Book with confidence: We offer a 4 hour Full Refund Guarantee.  </h5>

                //               <p><h5>Details</h5>
                //               Fare is non-refundable, name change is not permitted and ticket is non-transferable.</p>

                //               <p class='text-justify'>Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee) and is based on availability of flight at the time of change.</p>

                //               <p class='text-justify'> Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change).</p>

                //               <p class='text-justify'>Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p>

                //               <p><h5> Contact our 24/7 toll free customer service to make any changes.</h5>
                //               Prior to completing the booking in the <a href='/termsandconditions.aspx' target='_blank' class='text-blue'>Terms and Conditions</a>, you should review our service fees for exchanges, changes, refunds and cancellations.</p>

                //                </div>



                //                </div> 

                ////Fare Rules -end




                ////Baggage -Start

                //<div class='airfare-costs'>
                //                <h4><i class='fa fa-suitcase'></i> Baggage Information:</h4>
                //        <table class='table-responsive'>
                //            <thead>
                //            <tr>
                //             <th width='40%'><span>Sector/Flight</span></th>
                //             <th width='40%'><span>Check-in Baggage</span></th>
                //             <th width='20%'><span>Cabin Baggage</span></th>
                //            </tr>
                //            </thead>

                //            <tr>
                //             <td><span>LHR - BKK (9W122)</span></td>
                //             <td><span><i class='fa fa-suitcase'></i> 30 Kgs (Adult) </span></td>
                //             <td><span><i class='fa fa-suitcase'></i> 3kgs(Adult)</span></td>
                //            </tr>

                //            <tr>
                //             <td><span>BKK - LHR (9W122)</span></td>
                //             <td><span><i class='fa fa-suitcase'></i> 30 Kgs (Adult) </span></td>
                //             <td><span><i class='fa fa-suitcase'></i> 7kgs (Adult) </span></td>
                //            </tr>
                //        </table>
                //       <hr></hr>
                //        <p>Information not available<span class='text-danger'>*</span></p>
                //        <p class='text-justify'>The information presented above is as obtained from the airline reservation system. TravelMerry does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</p>
                //        </div>

                ////Baggage -end






                //}
                StrItineraryInfo += "</div>";

                StrItineraryInfo += " </div>";
            }
        }

        divItineraryInfo.InnerHtml = "" + StrItineraryInfo + "";
        //divItineraryInfo -end







        //divItineraryHead -strat


        string StrItineraryHead = "";

        StrItineraryHead += "<div class='row'>";
        StrItineraryHead += "<div class='col-md-12'>";
        StrItineraryHead += "<div class='result-bg' style='margin-bottom:15px;'>";
        StrItineraryHead += "<div class='result-content'>";
        StrItineraryHead += "<div class='row'>";
        StrItineraryHead += "<div class='col-md-9'>";
        StrItineraryHead += "<div class='result-title'>" + strFlyFrom + " <i class='fa fa-arrow-circle-right'></i> " + strFlyTo + "</div>";
        StrItineraryHead += "<div class='result-desc'>depart" + strDepDate + "" + "<span><i class='fa fa-user'></i>  Travelers: " + TotalPaxCnt + "</span></div>";
        StrItineraryHead += "</div>";
        StrItineraryHead += "<div class='col-md-3'>";
        StrItineraryHead += "<div class='hotel-item-price'>$" + String.Format("{0:0.00}", TtAmount) + " <span class='hotel-item-price-from'>Total Price</span></div>";
        StrItineraryHead += "<button type='button' class='btn btn-sky text-capitalize btn-md pull-right' style='margin-top:12px;'>Pay Now</button>";
        StrItineraryHead += "</div>";

        StrItineraryHead += "</div>";

        StrItineraryHead += "</div>";
        StrItineraryHead += "</div>";

        StrItineraryHead += "</div>";
        StrItineraryHead += "</div>";

        //lbltotalPrice.Text = String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]);
        //divItineraryHead.InnerHtml = StrItineraryHead;

        //divItineraryHead -end



        //divBookingId -start

        string StrBookingId = "";

        StrBookingId += "<div class='col-md-12'>";
        StrBookingId += "<h4 class='blue-text' style='margin-top:0px; margin-bottom:0px;'>PNR: " + PNR + "</h4>";
        StrBookingId += "</div>";

        divBookingId.InnerHtml = "" + StrBookingId + "";

        //divBookingId -end


        //divPassengerInfo -start

        string StrPassengerInfo = "";

        if (tblPax != null)
        {
            if (tblPax.Rows.Count > 0)
            {
                StrPassengerInfo += "<div class='col-md-12'>";
                StrPassengerInfo += "<h4><i class='fa fa-user'></i> Traveler Details</h4>";
                StrPassengerInfo += "</div>";

                int i = 1;
                foreach (DataRow drtblPax in tblPax.Rows)
                {
                    //pnlpassenger.Controls.Add(new LiteralControl("<tr>"));
                    //pnlpassenger.Controls.Add(new LiteralControl("<td height='20' align='left' valign='middle' style='text-transform: capitalize;'><table width='200px' height='15px'><tr><td width='20%'><img src='images/adult-icon.png' /></td><td width='80%'> " + drtblPax["LastName"].ToString().ToLower() + " " + drtblPax["FirstName"].ToString().ToLower() + "</td></tr></table></td>"));
                    //pnlpassenger.Controls.Add(new LiteralControl("</tr>"));
                    StrPassengerInfo += "<div class='col-md-4'>";
                    StrPassengerInfo += "<p>" + i + "." + drtblPax["LastName"].ToString().ToLower() + " " + drtblPax["FirstName"].ToString().ToLower() + "</p>";
                    StrPassengerInfo += "</div>";
                    i++;
                }

                divPassengerInfo.InnerHtml = "" + StrPassengerInfo + "";
            }
        }

        //divPassengerInfo -end


    }

    //protected void DisplayData_1S_New(string resultPNR1, DataTable dtPay)
    //{
    //    bool flag = false;
    //    DataTable tblPax = CreatePaxTable();
    //    DataTable tblitinerary = CreateItinTable();
    //    DataTable tblBaggage = CreateBaggageTable();

    //    string PNR = "";

    //    string OutBoundOrigin = "";
    //    string OutBoundDestination = "";
    //    string InBoundOrigin = "";
    //    string InBoundDestination = "";

    //    string OutBoundOriginNew = "";
    //    string OutBoundDestinationNew = "";
    //    string InBoundOriginNew = "";
    //    string InBoundDestinationNew = "";


    //    int start = resultPNR1.IndexOf("<TravelItineraryReadRS");
    //    int end = resultPNR1.IndexOf("</TravelItineraryReadRS>");

    //    resultPNR1 = resultPNR1.Substring(start, end - start) + "</TravelItineraryReadRS>";

    //    resultPNR1 = resultPNR1.Replace("CustomerName", "PersonName");

    //    TravelItineraryReadRS objTravelItineraryReadRS = TravelItineraryReadRS_XML.ResXml(resultPNR1);





    //    SqlParameter[] paramBooking = { new SqlParameter("@Booking_ID", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@PNR", dtPay.Rows[0]["pnrno"].ToString()) };
    //    DataTable dtPNR_ID = DataLayer.GetData("select_booking_booking_pnr_PNRID", paramBooking, ConfigurationManager.AppSettings["sqlcn"].ToString());
    //    if (dtPNR_ID != null)
    //    {
    //        if (dtPNR_ID.Rows.Count > 0)
    //        {
    //            SqlParameter[] paramPNR = { new SqlParameter("@PNR_ID", dtPNR_ID.Rows[0]["PNR_ID"].ToString()) };
    //            DataTable dtPNR = DataLayer.GetData("select_PNR", paramPNR, ConfigurationManager.AppSettings["sqlcn"].ToString());

    //            if (dtPNR != null)
    //            {
    //                if (dtPNR.Rows.Count > 0)
    //                {

    //                    OutBoundOrigin = dtPNR.Rows[0]["Depature"].ToString();
    //                    OutBoundDestination = dtPNR.Rows[0]["Destination"].ToString();
    //                    InBoundOrigin = dtPNR.Rows[0]["Return_Depature_From"].ToString();
    //                    InBoundDestination = dtPNR.Rows[0]["Return_Depature_To"].ToString();

    //                    OutBoundOriginNew = dtPNR.Rows[0]["Depature"].ToString();
    //                    OutBoundDestinationNew = dtPNR.Rows[0]["Destination"].ToString();
    //                    InBoundOriginNew = dtPNR.Rows[0]["Return_Depature_From"].ToString();
    //                    InBoundDestinationNew = dtPNR.Rows[0]["Return_Depature_To"].ToString();


    //                }
    //            }

    //        }

    //    }







    //    if (objTravelItineraryReadRS != null)
    //    {
    //        if (objTravelItineraryReadRS.TravelItinerary != null)
    //        {
    //            if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo != null)
    //            {
    //                if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName != null)
    //                {
    //                    if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName.Length > 0)
    //                    {
    //                        for (int paxcnt = 0; paxcnt < objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName.Length; paxcnt++)
    //                        {
    //                            DataRow drPax = tblPax.NewRow();
    //                            //if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType == "INF" || objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType == "JNF")
    //                            //{
    //                            drPax["Title"] = "";
    //                            //}
    //                            //else
    //                            //{
    //                            //    drPax["Title"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString().Split(' ')[1];
    //                            //}
    //                            drPax["LastName"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].Surname.ToString();
    //                            drPax["FirstName"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString();
    //                            drPax["PaxType"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType;

    //                            tblPax.Rows.Add(drPax);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        RedirectCS();
    //                    }
    //                }
    //                else
    //                {
    //                    RedirectCS();
    //                }
    //            }
    //            else
    //            {
    //                RedirectCS();
    //            }


    //            if (objTravelItineraryReadRS.TravelItinerary.ItineraryRef != null)
    //            {
    //                if (objTravelItineraryReadRS.TravelItinerary.ItineraryRef.ID != null)
    //                {
    //                    PNR = objTravelItineraryReadRS.TravelItinerary.ItineraryRef.ID;
    //                }
    //            }

    //            //Baggage -start
    //            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo != null)
    //            {
    //                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing != null)
    //                {
    //                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote != null)
    //                    {
    //                        for (int ItCnt = 0; ItCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote.Length; ItCnt++)
    //                        {
    //                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt] != null)
    //                            {
    //                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary != null)
    //                                {
    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo != null)
    //                                    {
    //                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PassengerTypeQuantity != null)
    //                                        {
    //                                            string PaxType = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PassengerTypeQuantity[0].Code;
    //                                            if (PaxType == "JCB")
    //                                                PaxType = "ADT";
    //                                            if (PaxType == "JNN")
    //                                                PaxType = "CNN";
    //                                            if (PaxType == "JNF")
    //                                                PaxType = "INF";

    //                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown != null)
    //                                            {
    //                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment != null)
    //                                                {
    //                                                    int scg = 0;
    //                                                    for (int sCnt = 0; sCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length; sCnt++)
    //                                                    {

    //                                                        ///*******************************/
    //                                                        //if (sCnt == 0)
    //                                                        //{
    //                                                        //    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd == "O")
    //                                                        //    {

    //                                                        //        OutBoundOriginNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
    //                                                        //    }
    //                                                        //    if (sCnt == objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length - 1)
    //                                                        //    {
    //                                                        //        OutBoundDestinationNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
    //                                                        //    }
    //                                                        //}

    //                                                        //if (sCnt > 0)
    //                                                        //{
    //                                                        //    if (!flag && objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd == "O")
    //                                                        //    {
    //                                                        //        flag = true;
    //                                                        //        OutBoundDestinationNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
    //                                                        //        InBoundOriginNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
    //                                                        //    }
    //                                                        //    if (sCnt == objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length - 1)
    //                                                        //    {
    //                                                        //        InBoundDestinationNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
    //                                                        //    }
    //                                                        //}
    //                                                        ///***-----------------------*************************/

    //                                                        string DepBag = "";
    //                                                        string DesBag = "";
    //                                                        if (sCnt == 0)
    //                                                        {
    //                                                            DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                        }
    //                                                        /*if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd == "O")
    //                                                        {
    //                                                            DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                        }
    //                                                        else
    //                                                        {
    //                                                            DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                            if (tblBaggage.Rows.Count > 0)
    //                                                                tblBaggage.Rows[tblBaggage.Rows.Count - 1]["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                        }*/
    //                                                        if (sCnt > 0)
    //                                                        {
    //                                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd != "O")
    //                                                                DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                            else
    //                                                                DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                            if (tblBaggage.Rows.Count > 0)
    //                                                                tblBaggage.Rows[tblBaggage.Rows.Count - 1]["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
    //                                                        }


    //                                                        scg++;
    //                                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].BaggageAllowance != null)
    //                                                        {
    //                                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].BaggageAllowance.Number != null)
    //                                                            {
    //                                                                DataRow drBag = tblBaggage.NewRow();
    //                                                                string bag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].BaggageAllowance.Number.ToString();


    //                                                                drBag["Segment_No"] = scg;
    //                                                                drBag["Airline"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].MarketingAirline.Code;
    //                                                                drBag["Flight_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].FlightNumber;
    //                                                                drBag["Departure"] = DepBag;
    //                                                                drBag["Checkin_Bag"] = bag;
    //                                                                drBag["PaxType"] = PaxType;
    //                                                                //tblBagg.Columns.Add("Segment_No", typeof(int));
    //                                                                //tblBagg.Columns.Add("Airline", typeof(string));
    //                                                                //tblBagg.Columns.Add("Flight_No", typeof(string));
    //                                                                //tblBagg.Columns.Add("Departure", typeof(string));
    //                                                                //tblBagg.Columns.Add("Destination", typeof(string));
    //                                                                //tblBagg.Columns.Add("Checkin_Bag", typeof(string));
    //                                                                //tblBagg.Columns.Add("Cabin_Bag", typeof(string));
    //                                                                //tblBagg.Columns.Add("PaxType", typeof(string));
    //                                                                tblBaggage.Rows.Add(drBag);
    //                                                            }
    //                                                        }

    //                                                    }
    //                                                }
    //                                            }

    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }

    //                    }
    //                }
    //            }
    //            //Baggage -end

    //            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo != null)
    //            {
    //                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems != null)
    //                {
    //                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length > 0)
    //                    {
    //                        int segLength = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length;
    //                        for (int segno = 0; segno < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length; segno++)
    //                        {

    //                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment != null)
    //                            {
    //                                if (!objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Status.ToString().Contains("WK"))
    //                                {
    //                                    DataRow drSegment = tblitinerary.NewRow();

    //                                    /*******************************/
    //                                    if (segno == 0)
    //                                    {
    //                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ConnectionInd == "O")
    //                                        {
    //                                            drSegment["OutBoundOrigin"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
    //                                            OutBoundOrigin = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
    //                                        }
    //                                        if (segno == segLength - 1)
    //                                        {
    //                                            drSegment["OutBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
    //                                            OutBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
    //                                        }
    //                                    }

    //                                    if (segno > 0)
    //                                    {
    //                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ConnectionInd == "O")
    //                                        {
    //                                            //drSegment["OutBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno - 1].FlightSegment[0].DestinationLocation.LocationCode;
    //                                            if (tblitinerary != null)
    //                                            {
    //                                                if (tblitinerary.Rows.Count > 0)
    //                                                {
    //                                                    OutBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno - 1].FlightSegment[0].DestinationLocation.LocationCode;
    //                                                    tblitinerary.Rows[tblitinerary.Rows.Count - 1]["OutBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno - 1].FlightSegment[0].DestinationLocation.LocationCode;
    //                                                }
    //                                            }
    //                                            InBoundOrigin = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
    //                                            drSegment["InBoundOrigin"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
    //                                            //if (segno == segLength - 1)
    //                                            //{
    //                                            //    drSegment["InBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
    //                                            //    InBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
    //                                            //}                                            
    //                                        }
    //                                        if (segno == segLength - 1)
    //                                        {
    //                                            drSegment["InBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
    //                                            InBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
    //                                        }
    //                                    }

    //                                    /*******************************/

    //                                    drSegment["PNR_ID"] = PNR;
    //                                    drSegment["Segment_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SegmentNumber;
    //                                    drSegment["Airline"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].MarketingAirline.Code;
    //                                    drSegment["Flight_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].MarketingAirline.FlightNumber;
    //                                    /***Change ******************/
    //                                    string StrCodeshare = "";
    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OperatingAirline != null)
    //                                    {
    //                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OperatingAirline[0].Code != null)
    //                                        {
    //                                            StrCodeshare = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OperatingAirline[0].Code;
    //                                        }
    //                                    }
    //                                    drSegment["Codeshare_Airline"] = StrCodeshare;
    //                                    /***Change ******************/
    //                                    drSegment["Class"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ResBookDesigCode;
    //                                    drSegment["Departure"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
    //                                    drSegment["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;



    //                                    string dept_date = "";
    //                                    string dept_time = "";

    //                                    string dept_date_updated = "";
    //                                    string arr_date_updated = "";


    //                                    string arr_date = "";
    //                                    string arr_time = "";

    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime != null)
    //                                    {
    //                                        dept_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime.Split('T')[1];
    //                                        dept_date_updated = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime.Split('T')[0];
    //                                        dept_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[0];

    //                                    }
    //                                    else
    //                                    {
    //                                        dept_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[1];
    //                                        dept_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[0];

    //                                    }
    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime != null)
    //                                    {
    //                                        arr_date_updated = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime.Split('T')[0];
    //                                        arr_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime.Split('T')[1];
    //                                        arr_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[0];


    //                                    }
    //                                    else
    //                                    {
    //                                        arr_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[1];
    //                                        arr_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[0];

    //                                    }






    //                                    string[] strSplitDepDate = dept_date.Split('-');
    //                                    string[] strSplitArrDate = arr_date.Split('-');
    //                                    int DepDateYear = Convert.ToInt32(strSplitDepDate[0]);
    //                                    int ArrDateYear = Convert.ToInt32(strSplitDepDate[0]);
    //                                    if (strSplitDepDate[1] == "12" && strSplitDepDate[2] == "31" && strSplitArrDate[0] == "01" && strSplitArrDate[1] == "01")
    //                                    {
    //                                        ArrDateYear += 1;
    //                                    }


    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime != null)
    //                                    {
    //                                        dept_date = DepDateYear + "-" + dept_date_updated;

    //                                    }

    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime != null)
    //                                    {
    //                                        arr_date = arr_date_updated;
    //                                    }

    //                                    drSegment["Dept_date"] = dept_date;
    //                                    drSegment["Dept_time"] = dept_time.Replace(":", "");
    //                                    drSegment["Arrival_date"] = ArrDateYear + "-" + arr_date;
    //                                    drSegment["Arrival_time"] = arr_time.Replace(":", "");

    //                                    TimeSpan ts = (DateTime.Parse(arr_date.Substring(0, 2) + "/" + arr_date.Substring(3, 2) + "/" + ArrDateYear, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))) - DateTime.Parse(dept_date.Substring(5, 2) + "/" + dept_date.Substring(8, 2) + "/" + dept_date.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

    //                                    /*********************/
    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ElapsedTime != null)
    //                                    {
    //                                        drSegment["ElapsedTime"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ElapsedTime.ToString();
    //                                    }

    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal != null)
    //                                    {
    //                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal.Length > 0)
    //                                        {
    //                                            drSegment["MealTypes"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal[0].Code.ToString();
    //                                        }
    //                                    }
    //                                    /*********************/

    //                                    drSegment["Nextday"] = ts.Days;

    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Equipment != null)
    //                                    {
    //                                        drSegment["Equipment"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Equipment.AirEquipType;
    //                                    }
    //                                    else
    //                                    {
    //                                        drSegment["Equipment"] = "";

    //                                    }
    //                                    drSegment["Baggage"] = "";
    //                                    drSegment["Dept_Terminal"] = "";
    //                                    drSegment["Arrival_Terminal"] = "";

    //                                    /********************************************/
    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.TerminalCode != null)
    //                                        drSegment["Dept_Terminal"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.TerminalCode.ToString();
    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.TerminalCode != null)
    //                                        drSegment["Arrival_Terminal"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.TerminalCode.ToString();
    //                                    /********************************************/

    //                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef != null)
    //                                    {
    //                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID != null)
    //                                        {
    //                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID != "")
    //                                            {

    //                                                string[] Airline_PNRSplit = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID.Split('*');
    //                                                if (Airline_PNRSplit.Length > 1)
    //                                                {
    //                                                    drSegment["Airline_PNR"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID.Split('*')[1];
    //                                                }
    //                                                else
    //                                                {
    //                                                    drSegment["Airline_PNR"] = "";

    //                                                }
    //                                            }
    //                                            else
    //                                            {
    //                                                drSegment["Airline_PNR"] = "";

    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            drSegment["Airline_PNR"] = "";

    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        drSegment["Airline_PNR"] = "";

    //                                    }
    //                                    drSegment["StopOvers"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].StopQuantity;


    //                                    tblitinerary.Rows.Add(drSegment);
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        RedirectCS();
    //                    }
    //                }
    //                else
    //                {
    //                    RedirectCS();
    //                }
    //            }
    //            else
    //            {
    //                RedirectCS();
    //            }


    //        }
    //        else
    //        {
    //            RedirectCS();
    //        }
    //    }
    //    else
    //    {
    //        RedirectCS();
    //    }


    //    //calculating Total pax, Adult, Child, Infant -start
    //    int TotalPaxCnt = 0;
    //    if (tblPax != null)
    //    {
    //        if (tblPax.Rows.Count > 0)
    //        {
    //            TotalPaxCnt = tblPax.Rows.Count;
    //        }
    //    }
    //    //calculating Total pax, Adult, Child, Infant -end



    //    //New York,United States
    //    string departure = "";
    //    //Bangkok,Thailand
    //    string destination = "";
    //    //<time class='comment-date' datetime='07/14/2015'><i class='fa fa-clock-o'></i>Jul 14 , 2015</time> <i class='fa fa-arrow-circle-right'></i><time class='comment-date padlft-rgt' datetime='07/16/2015'><i class='fa fa-clock-o'></i> Jul 16 , 2015</time>
    //    string depDestTimings = "";


    //    //divItineraryInfo -strat


    //    string depCity = "";
    //    string desCity = "";
    //    string arrCity = "";
    //    string retCity = "";

    //    string strFlyFrom = "";
    //    string strFlyTo = "";
    //    string strDepDate = "";
    //    string strRetDate = "";

    //    string StrItineraryInfo = "";

    //    if (tblitinerary != null)
    //    {
    //        if (tblitinerary.Rows.Count > 0)
    //        {

    //            StrItineraryInfo += "<div class='col-md-12'>";

    //            StrItineraryInfo += "<div class='row'>";
    //            StrItineraryInfo += "<div class='col-md-8'> <h4><i class='fa fa-plane'></i> Flight details</h4></div>";
    //            StrItineraryInfo += "<div class='col-md-4 pull-right text-right'><h4 style='margin-bottom:0px;'><small>Total price</small> <strong class='text-success'>$" + String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]) + "</strong></h4>";
    //            StrItineraryInfo += "<small style='font-size:9px;'>inc. of all taxes</small></div>";
    //            StrItineraryInfo += "</div>";
    //            StrItineraryInfo += "<hr></hr>";

    //            StrItineraryInfo += "<div class='tbl-info'>";
    //            //foreach (DataRow drItinerary in tblitinerary.Rows)
    //            //{


    //            for (int SegCnt = 0; SegCnt < tblitinerary.Rows.Count; SegCnt++)
    //            {

    //                /*****************************************/

    //                DataTable dtFlight = FlightCode(tblitinerary.Rows[SegCnt]["Airline"].ToString());
    //                string strAirline = "";
    //                string strAirlineimg = "";
    //                if (dtFlight.Rows.Count > 0)
    //                {
    //                    strAirline = dtFlight.Rows[0]["AirlineShortName"].ToString();
    //                    strAirlineimg = "images/airline-logos/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";

    //                }
    //                else
    //                {
    //                    strAirline = tblitinerary.Rows[SegCnt]["Airline"].ToString();
    //                    strAirlineimg = "images/airline-logos/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";
    //                }

    //                DataTable dtDepArp = GetDara(tblitinerary.Rows[SegCnt]["Departure"].ToString());
    //                string strDepCity1 = "";
    //                string strDepCity = "";
    //                string strDepCountry = "";
    //                if (dtDepArp.Rows.Count > 0)
    //                {
    //                    strDepCity1 = dtDepArp.Rows[0]["City"].ToString();
    //                    strDepCity = dtDepArp.Rows[0]["City"].ToString() + ", " + dtDepArp.Rows[0]["Airport"].ToString();
    //                    strDepCountry = dtDepArp.Rows[0]["Country"].ToString();
    //                }
    //                else
    //                {
    //                    strDepCity1 = tblitinerary.Rows[SegCnt]["Departure"].ToString();
    //                    strDepCity = tblitinerary.Rows[SegCnt]["Departure"].ToString();
    //                    strDepCountry = tblitinerary.Rows[SegCnt]["Departure"].ToString();
    //                }
    //                string dtDay1 = "";
    //                string dtMonth1 = "";
    //                string dtYear1 = "";
    //                string dtDay2 = "";
    //                string dtMonth2 = "";
    //                string dtYear2 = "";
    //                dtDay1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(8, 2);
    //                dtMonth1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(5, 2);
    //                dtYear1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(0, 4);

    //                dtDay2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(8, 2);
    //                dtMonth2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(5, 2);
    //                dtYear2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(0, 4);



    //                DataTable dtArrArp = GetDara(tblitinerary.Rows[SegCnt]["Destination"].ToString());
    //                string strArrCity1 = "";
    //                string strArrCity = "";
    //                string strArrCountry = "";
    //                if (dtArrArp.Rows.Count > 0)
    //                {
    //                    strArrCity1 = dtArrArp.Rows[0]["City"].ToString();
    //                    strArrCity = dtArrArp.Rows[0]["City"].ToString() + ", " + dtArrArp.Rows[0]["Airport"].ToString();
    //                    strArrCountry = dtArrArp.Rows[0]["Country"].ToString();

    //                }
    //                else
    //                {
    //                    strArrCity1 = tblitinerary.Rows[SegCnt]["Destination"].ToString();
    //                    strArrCity = tblitinerary.Rows[SegCnt]["Destination"].ToString();
    //                    strArrCountry = tblitinerary.Rows[SegCnt]["Destination"].ToString();
    //                }


    //                /************************************************************/
    //                if (SegCnt == 0)
    //                {
    //                    Session["DEP_DAT"] = tblitinerary.Rows[SegCnt]["Dept_date"].ToString();
    //                    //if (tblitinerary.Rows[SegCnt]["OutBoundOrigin"].ToString() != "")
    //                    //{
    //                    if (OutBoundOriginNew != "" && OutBoundDestinationNew != "")
    //                    {

    //                        string depDat = "";
    //                        string retDat = "";
    //                        int stopsCnt = 0;
    //                        TimeSpan TtElapsedTimeOut = TimeSpan.Parse("00:00");
    //                        /***********Calculating Inbound Duration****************/
    //                        for (int dSegCnt = SegCnt; dSegCnt < tblitinerary.Rows.Count; dSegCnt++)
    //                        {
    //                            if (dSegCnt == 0)
    //                            {
    //                                depDat = tblitinerary.Rows[dSegCnt]["Dept_date"].ToString();
    //                                retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
    //                            }

    //                            //if (tblitinerary.Rows[dSegCnt]["InBoundOrigin"].ToString() != "")
    //                            if (InBoundOriginNew == tblitinerary.Rows[dSegCnt]["Destination"].ToString())
    //                            {
    //                                if (dSegCnt == 0)
    //                                    retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
    //                                else
    //                                    retDat = tblitinerary.Rows[dSegCnt - 1]["Arrival_date"].ToString();
    //                            }
    //                            //calculate ElapsedTime    
    //                            if (tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString() != "")
    //                                TtElapsedTimeOut = TtElapsedTimeOut.Add(TimeSpan.Parse(Convert.ToInt32(tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.')[0]) / 24 + ":" + tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.')[0] + ":" + tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.')[1]));
    //                            //calculate ElapsedTime
    //                            if (dSegCnt > 0 && dSegCnt < tblitinerary.Rows.Count)
    //                            {
    //                                //if (tblitinerary.Rows[dSegCnt]["OutBoundOrigin"].ToString() == "")
    //                                if (InBoundOriginNew == tblitinerary.Rows[dSegCnt]["Destination"].ToString())
    //                                {
    //                                    stopsCnt++;
    //                                    string StopOverTime = CalculateStopOver(tblitinerary.Rows[dSegCnt - 1]["Arrival_date"].ToString() + "~" + tblitinerary.Rows[dSegCnt - 1]["Arrival_time"].ToString(), tblitinerary.Rows[dSegCnt]["Dept_date"].ToString() + "~" + tblitinerary.Rows[dSegCnt]["Dept_time"].ToString());
    //                                    TtElapsedTimeOut = TtElapsedTimeOut.Add(TimeSpan.Parse(StopOverTime));
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                        /***********Calculating Inbound Duration****************/
    //                        DataTable dtOutOrg = GetDara(OutBoundOriginNew);
    //                        if (dtOutOrg.Rows.Count > 0)
    //                            OutBoundOrigin = dtOutOrg.Rows[0]["City"].ToString();
    //                        DataTable dtOutDest = GetDara(OutBoundDestinationNew);
    //                        if (dtOutDest.Rows.Count > 0)
    //                            OutBoundDestination = dtOutDest.Rows[0]["City"].ToString();

    //                        strDepDate = DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();
    //                        strRetDate = DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();


    //                        StrItineraryInfo += "<div class='thead-main thead-main-dep'>";
    //                        StrItineraryInfo += "<div class='th'>";
    //                        StrItineraryInfo += "<h4 class='flight-head'><i class='fa fa-plane'></i> " + OutBoundOrigin + " to " + OutBoundDestination + "</h4>";
    //                        string ArrivalDay = "";
    //                        DateTime dep1 = DateTime.Parse(depDat.Substring(5, 2) + "/" + depDat.Substring(8, 2) + "/" + depDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
    //                        DateTime ret2 = DateTime.Parse(retDat.Substring(5, 2) + "/" + retDat.Substring(8, 2) + "/" + retDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
    //                        //TimeSpan ts = TimeSpan.Parse((ret2 - ret2));
    //                        double days = (ret2 - dep1).TotalDays;
    //                        if (days == 0)
    //                            ArrivalDay = " - Same Day Arrival";
    //                        else
    //                            ArrivalDay = " - " + days + " Day Arrival";
    //                        string StrStops = "";
    //                        if (stopsCnt == 0)
    //                            StrStops = " - Direct Flight";
    //                        else
    //                            StrStops = " - " + stopsCnt + " stop";

    //                        int TtHrsOut = TtElapsedTimeOut.Hours;
    //                        if (TtElapsedTimeOut.Days > 0)
    //                        {
    //                            TtHrsOut += TtElapsedTimeOut.Days * 24;
    //                        }

    //                        StrItineraryInfo += "<p class='time-stop-diff badge pull-right'>" + TtHrsOut + "hrs " + TtElapsedTimeOut.Minutes + "min" + StrStops + "" + ArrivalDay + "</p>";//14hrs 30min - 1 stop - Same Day Arrival
    //                        StrItineraryInfo += "</div>";
    //                        StrItineraryInfo += "</div>";

    //                        StrItineraryInfo += "<div class='fl-table'>";
    //                        StrItineraryInfo += "<div class='fl-tr sub-thead'>";
    //                        StrItineraryInfo += "<div class='fl-th col-flight'>Flight</div>";
    //                        StrItineraryInfo += "<div class='fl-th col-departure'>Depart</div>";
    //                        StrItineraryInfo += "<div class='fl-th col-arrival '>Arrive</div>";
    //                        StrItineraryInfo += "<div class='fl-th col-info'>Info</div>";
    //                        StrItineraryInfo += "</div>";
    //                        StrItineraryInfo += "</div>";
    //                    }
    //                    //}
    //                }

    //                if (SegCnt > 0)
    //                {
    //                    if (InBoundOriginNew == tblitinerary.Rows[SegCnt]["Departure"].ToString())
    //                    {
    //                        if (InBoundOriginNew != "" && InBoundDestinationNew != "")
    //                        {
    //                            string depDat = "";
    //                            string retDat = "";
    //                            int stopsCnt = 0;
    //                            TimeSpan TtElapsedTimeIn = TimeSpan.Parse("00:00");
    //                            /***********Calculating Inbound Duration****************/
    //                            for (int dSegCnt = SegCnt; dSegCnt < tblitinerary.Rows.Count; dSegCnt++)
    //                            {
    //                                if (dSegCnt == SegCnt)
    //                                {
    //                                    depDat = tblitinerary.Rows[dSegCnt]["Dept_date"].ToString();
    //                                    retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
    //                                }
    //                                //calculate ElapsedTime   
    //                                if (tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.').Length > 1)
    //                                    TtElapsedTimeIn = TtElapsedTimeIn.Add(TimeSpan.Parse(tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.')[0] + ":" + tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.')[1]));
    //                                //calculate ElapsedTime
    //                                if (dSegCnt > SegCnt && dSegCnt < tblitinerary.Rows.Count)
    //                                {
    //                                    retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
    //                                    //if (tblitinerary.Rows[SegCnt]["Departure"].ToString() == InBoundOriginNew)
    //                                    //{
    //                                    stopsCnt++;
    //                                    string StopOverTime = CalculateStopOver(tblitinerary.Rows[dSegCnt - 1]["Arrival_date"].ToString() + "~" + tblitinerary.Rows[dSegCnt - 1]["Arrival_time"].ToString(), tblitinerary.Rows[dSegCnt]["Dept_date"].ToString() + "~" + tblitinerary.Rows[dSegCnt]["Dept_time"].ToString());

    //                                    try
    //                                    {
    //                                        TtElapsedTimeIn = TtElapsedTimeIn.Add(TimeSpan.Parse(StopOverTime));
    //                                    }
    //                                    catch
    //                                    { }
    //                                    //}
    //                                }
    //                            }
    //                            /***********Calculating Inbound Duration****************/

    //                            DataTable dtInOrg = GetDara(InBoundOriginNew);
    //                            if (dtInOrg.Rows.Count > 0)
    //                                InBoundOrigin = dtInOrg.Rows[0]["City"].ToString();
    //                            DataTable dtInDest = GetDara(InBoundDestinationNew);
    //                            if (dtInDest.Rows.Count > 0)
    //                                InBoundDestination = dtInDest.Rows[0]["City"].ToString();

    //                            StrItineraryInfo += "<div class='thead-main thead-main-dep'>";
    //                            StrItineraryInfo += "<div class='th'>";
    //                            StrItineraryInfo += "<h4 class='flight-head'><i class='fa fa-plane'></i> " + InBoundOrigin + " to " + InBoundDestination + "</h4>";
    //                            string ArrivalDay = "";
    //                            DateTime dep1 = DateTime.Parse(depDat.Substring(5, 2) + "/" + depDat.Substring(8, 2) + "/" + depDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
    //                            DateTime ret2 = DateTime.Parse(retDat.Substring(5, 2) + "/" + retDat.Substring(8, 2) + "/" + retDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
    //                            //TimeSpan ts = TimeSpan.Parse((ret2 - ret2));
    //                            double days = (ret2 - dep1).TotalDays;
    //                            if (days == 0)
    //                                ArrivalDay = " - Same Day Arrival";
    //                            else
    //                                ArrivalDay = " - " + days + " Day Arrival";
    //                            string StrStops = "";
    //                            if (stopsCnt == 0)
    //                                StrStops = " - Direct Flight";
    //                            else
    //                                StrStops = " - " + stopsCnt + " stop";

    //                            int TtHrsIn = TtElapsedTimeIn.Hours;
    //                            if (TtElapsedTimeIn.Days > 0)
    //                            {
    //                                TtHrsIn += TtElapsedTimeIn.Days * 24;
    //                            }
    //                            StrItineraryInfo += "<p class='time-stop-diff badge pull-right'>" + TtHrsIn + "hrs " + TtElapsedTimeIn.Minutes + "min" + StrStops + "" + ArrivalDay + "</p>";//14hrs 30min - 1 stop - Same Day Arrival
    //                            StrItineraryInfo += "</div>";
    //                            StrItineraryInfo += "</div>";

    //                            StrItineraryInfo += "<div class='fl-table'>";
    //                            StrItineraryInfo += "<div class='fl-tr sub-thead'>";
    //                            StrItineraryInfo += "<div class='fl-th col-flight'>Flight</div>";
    //                            StrItineraryInfo += "<div class='fl-th col-departure'>Depart</div>";
    //                            StrItineraryInfo += "<div class='fl-th col-arrival '>Arrive</div>";
    //                            StrItineraryInfo += "<div class='fl-th col-info'>Info</div>";
    //                            StrItineraryInfo += "</div>";
    //                            StrItineraryInfo += "</div>";
    //                        }
    //                    }
    //                }

    //                if (SegCnt > 0 && SegCnt < tblitinerary.Rows.Count)
    //                {
    //                    if (InBoundOriginNew != tblitinerary.Rows[SegCnt]["Departure"].ToString())
    //                    {

    //                        string StopOverTime = CalculateStopOver(tblitinerary.Rows[SegCnt - 1]["Arrival_date"].ToString() + "~" + tblitinerary.Rows[SegCnt - 1]["Arrival_time"].ToString(), tblitinerary.Rows[SegCnt]["Dept_date"].ToString() + "~" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString());

    //                        //Stop Over -start
    //                        StrItineraryInfo += "<div class='row'>";
    //                        StrItineraryInfo += "<div class='col-md-12'>";
    //                        StrItineraryInfo += "<div class='flight-connection'>";
    //                        StrItineraryInfo += "<div class='flight-connection-row'>";
    //                        StrItineraryInfo += "<div class='flight-connec-layover'>";
    //                        StrItineraryInfo += "<span class='flight-layover-brd'>";
    //                        StrItineraryInfo += "<span class='spacert'><i class='fa fa-clock-o'></i> Stopover: " + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min</span>";//Layover
    //                        //StrItineraryInfo += "<span class='text-danger spacert'><i class='fa fa-plane'></i> Flight Change</span>";
    //                        //StrItineraryInfo += "<span class='text-danger'><i class='fa fa-moon-o'></i> Overnight Required</span>";
    //                        StrItineraryInfo += "</span>";
    //                        StrItineraryInfo += "</div>";
    //                        StrItineraryInfo += "</div>";
    //                        StrItineraryInfo += "</div>";
    //                        StrItineraryInfo += "</div>";
    //                        StrItineraryInfo += "</div>";
    //                        //Stop Over -end
    //                    }
    //                }
    //                /************************************************************/


    //                //segment -star
    //                StrItineraryInfo += "<div class='row'>";
    //                StrItineraryInfo += "<div class='col-md-12'>";
    //                StrItineraryInfo += "<div class='sigment1 seg1'>";
    //                StrItineraryInfo += "<div class='flight-list-box-wrap'>";
    //                StrItineraryInfo += "<div class='airlinelogo'>";
    //                StrItineraryInfo += "<div class='flight-airlinelogo'>";
    //                StrItineraryInfo += "<img src='" + strAirlineimg + "' class='img-responsive'>";// img-thumbnail
    //                StrItineraryInfo += "</div>";
    //                //StrItineraryInfo += "<span>" + strAirline + " </span>";
    //                StrItineraryInfo += "<div>" + strAirline + " </div>";
    //                StrItineraryInfo += "<p>" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + String.Format("{0:0000}", Convert.ToInt32(tblitinerary.Rows[SegCnt]["Flight_No"].ToString())) + "</p>";
    //                /***Change************************/
    //                if (tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString() != "")
    //                {
    //                    DataTable dtFlightCodeShare = FlightCode(tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString());
    //                    string strAirlineCodeSHare = "";
    //                    if (dtFlightCodeShare.Rows.Count > 0)
    //                    {
    //                        strAirlineCodeSHare = dtFlightCodeShare.Rows[0]["AirlineShortName"].ToString();
    //                        //strAirlineimg = "images/airline-logos/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";

    //                    }
    //                    else
    //                    {
    //                        strAirlineCodeSHare = tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString();
    //                    }
    //                    if (strAirlineCodeSHare != "")
    //                        StrItineraryInfo += "<p>Operated by " + strAirlineCodeSHare + "</p>";
    //                }
    //                /***Change************************/
    //                StrItineraryInfo += "</div>";
    //                StrItineraryInfo += "<div class='direction' style='width:63%;'>";
    //                StrItineraryInfo += "<div class='departure'>";
    //                StrItineraryInfo += "<span class='travel-dates'>" + strDepCity1 + "</span>";
    //                StrItineraryInfo += "<h5><a data-original-title='" + strDepCity + "' href='#' data-toggle='tooltip' title=''>" + tblitinerary.Rows[SegCnt]["Departure"].ToString() + "</a>";
    //                StrItineraryInfo += "<span class=''> " + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[3] + "</span></h5>";
    //                StrItineraryInfo += "<span class='travel-dates'>" + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</span>";
    //                StrItineraryInfo += "</div>";

    //                StrItineraryInfo += "<span class='arrow'>→</span>";

    //                StrItineraryInfo += "<div class='arrive'>";
    //                StrItineraryInfo += "<span class='travel-dates'>" + strArrCity1 + "</span>";
    //                StrItineraryInfo += "<h5><a data-original-title='" + strArrCity + "' href='#' data-toggle='tooltip' title=''>" + tblitinerary.Rows[SegCnt]["Destination"].ToString() + "</a>";
    //                StrItineraryInfo += "<span class=''> " + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[3] + "</span></h5>";
    //                StrItineraryInfo += "<span class='travel-dates'>" + DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</span>";
    //                StrItineraryInfo += "</div>";
    //                StrItineraryInfo += "</div>";

    //                StrItineraryInfo += "<div class='flt-baggage'>";
    //                if (tblitinerary.Rows[SegCnt]["ElapsedTime"] != null)
    //                {
    //                    if (tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString() != "")
    //                        StrItineraryInfo += "<span class='amenties-span'>" + tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[0] + "h " + tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[1] + "m</span>";
    //                }
    //                if (tblitinerary.Rows[SegCnt]["Dept_Terminal"] != null)
    //                {
    //                    if (tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() != "")
    //                        StrItineraryInfo += "<span class='amenties-span'>Departure Terminal: " + tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() + "</span>";
    //                }
    //                if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"] != null)
    //                {
    //                    if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() != "")
    //                        StrItineraryInfo += "<span class='amenties-span'>Arrival Terminal: " + tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() + "</span>";
    //                }
    //                if (tblitinerary.Rows[SegCnt]["Equipment"] != null)
    //                {
    //                    if (tblitinerary.Rows[SegCnt]["Equipment"].ToString() != "")
    //                    {
    //                        string EqpType = getEquipment(tblitinerary.Rows[SegCnt]["Equipment"].ToString());
    //                        if (EqpType != "")
    //                        {
    //                            StrItineraryInfo += "<span class='amenties-span'>" + EqpType + "</span>";
    //                        }
    //                    }
    //                }
    //                if (tblitinerary.Rows[SegCnt]["MealTypes"] != null)
    //                {
    //                    if (tblitinerary.Rows[SegCnt]["MealTypes"].ToString() != "")
    //                    {
    //                        string MealType = getMealTypes(tblitinerary.Rows[SegCnt]["MealTypes"].ToString());
    //                        if (MealType != "")
    //                            StrItineraryInfo += "<span class='amenties-span'>Meals Types:" + tblitinerary.Rows[SegCnt]["MealTypes"].ToString() + "</span>";
    //                    }
    //                }
    //                if (tblitinerary.Rows[SegCnt]["Airline_PNR"] != null)
    //                {
    //                    if (tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() != "")
    //                        StrItineraryInfo += "<span class='amenties-span blue-text'>Airline PNR: " + tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() + "</span>";
    //                }
    //                StrItineraryInfo += "</div>";

    //                StrItineraryInfo += "</div>";
    //                StrItineraryInfo += "</div>";
    //                StrItineraryInfo += "</div>";
    //                StrItineraryInfo += "</div>";
    //                //segment -end                    
    //            }

    //            //note -start                
    //            StrItineraryInfo += "<div class='thead-main thead-main-arr'>";
    //            StrItineraryInfo += "<div class='th'>";
    //            StrItineraryInfo += "<strong class='pull-left'>* All the times are local to Airports</strong>";
    //            StrItineraryInfo += "</div>";
    //            StrItineraryInfo += "</div>";
    //            //note -end


    //            ////Fare Rules -Start

    //            StrItineraryInfo += "<div class='airfare-costs'>";
    //            StrItineraryInfo += "<h4><i class='fa fa-dollar'></i> Fare Rules:</h4>";

    //            StrItineraryInfo += "<div class='text-justify'>";
    //            StrItineraryInfo += "<p><h5>Cancellation Fee</h5>";
    //            StrItineraryInfo += "Airline Fee* - This is a non-refundable ticket.<br>";
    //            StrItineraryInfo += "TravelMerry Fee - USD 100 per passenger.<br>";
    //            StrItineraryInfo += "Partly utilized tickets cannot be cancelled.</p>";
    //            //StrItineraryInfo += "<p><h5>Change Fee</h5>";
    //            //StrItineraryInfo += "Airline Fee* - USD 167 per passenger + fare difference (if applicable).<br>";
    //            //StrItineraryInfo += "TravelMerry Fee - USD 50 per passenger.</p>";

    //            StrItineraryInfo += "<p class='text-justify'>*Airlines stop accepting cancellation/change requests 4 - 72 hours before departure of the flight, depending on the airline. The airline fee is indicative based on an automated interpretation of airline fare rules. TravelMerry doesn't guarantee the accuracy of this information. The change/cancellation fee may also vary based on fluctuations in currency conversion rates. For exact cancellation/change fee, please call us at our customer care number. </p>";
    //            StrItineraryInfo += "</div>";






    //            StrItineraryInfo += "<div class='text-justify'>";
    //            StrItineraryInfo += "<h5>Booking Rules:</h5>";
    //            StrItineraryInfo += "<p><h5>Book with confidence: We offer a 4 hour Full Refund Guarantee.  </h5>";

    //            StrItineraryInfo += "<p><h5>Details</h5>";
    //            StrItineraryInfo += "Fare is non-refundable, name change is not permitted and ticket is non-transferable.</p>";

    //            StrItineraryInfo += "<p class='text-justify'>Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee) and is based on availability of flight at the time of change.</p>";

    //            StrItineraryInfo += "<p class='text-justify'> Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change).</p>";

    //            StrItineraryInfo += "<p class='text-justify'>Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p>";

    //            StrItineraryInfo += "<p><h5> Contact our 24/7 toll free customer service to make any changes.</h5>";
    //            StrItineraryInfo += "Prior to completing the booking in the <a href='/termsandconditions.aspx' target='_blank' class='text-blue'>Terms and Conditions</a>, you should review our service fees for exchanges, changes, refunds and cancellations.</p>";

    //            StrItineraryInfo += "</div>";



    //            StrItineraryInfo += "</div>";

    //            ////Fare Rules -end




    //            ////Baggage -Start

    //            string StrBaggageInfo = "";

    //            if (tblBaggage.Rows.Count > 0)
    //            {
    //                StrBaggageInfo += "<div class='airfare-costs'>";
    //                StrBaggageInfo += "<h4><i class='fa fa-suitcase'></i> Baggage Information:</h4>";
    //                StrBaggageInfo += "<table class='table-responsive'>";
    //                StrBaggageInfo += "<thead>";
    //                StrBaggageInfo += "<tr>";
    //                StrBaggageInfo += "<th width='40%'><span>Sector/Flight</span></th>";
    //                StrBaggageInfo += "<th width='40%'><span>Check-in Baggage</span></th>";
    //                StrBaggageInfo += "<th width='20%'><span>Cabin Baggage</span></th>";
    //                StrBaggageInfo += "</tr>";
    //                StrBaggageInfo += "</thead>";

    //                //tblBagg.Columns.Add("Segment_No", typeof(int));
    //                //tblBagg.Columns.Add("Airline", typeof(string));
    //                //tblBagg.Columns.Add("Flight_No", typeof(string));
    //                //tblBagg.Columns.Add("Departure", typeof(string));
    //                //tblBagg.Columns.Add("Destination", typeof(string));
    //                //tblBagg.Columns.Add("Checkin_Bag", typeof(string));
    //                //tblBagg.Columns.Add("Cabin_Bag", typeof(string));
    //                //tblBagg.Columns.Add("PaxType", typeof(string));
    //                //<tr>
    //                // <td><span>LHR - BKK (9W122)</span></td>
    //                // <td><span><i class='fa fa-suitcase'></i> 30 Kgs (Adult) </span></td>
    //                // <td><span><i class='fa fa-suitcase'></i> 3kgs(Adult)</span></td>
    //                //</tr>

    //                //DataTable dtBB = new DataTable();
    //                string airName = "";
    //                DataRow[] drBagAdt = null;
    //                DataRow[] drBagCnn = null;
    //                DataRow[] drBagInf = null;
    //                for (int bagCnt = 0; bagCnt < tblitinerary.Rows.Count; bagCnt++)
    //                {
    //                    //if (drBagAdt == null)                        
    //                    drBagAdt = tblBaggage.Select("PaxType='ADT' and Segment_No='" + tblitinerary.Rows[bagCnt]["Segment_No"] + "'", "Segment_No asc");
    //                    //if (drBagCnn == null)
    //                    drBagCnn = tblBaggage.Select("PaxType='CNN' and Segment_No='" + tblitinerary.Rows[bagCnt]["Segment_No"] + "'", "Segment_No asc");
    //                    //if (drBagInf == null)
    //                    drBagInf = tblBaggage.Select("PaxType='INF' and Segment_No='" + tblitinerary.Rows[bagCnt]["Segment_No"] + "'", "Segment_No asc");

    //                    if (drBagAdt != null)
    //                    {
    //                        if (drBagAdt.Length > 0)
    //                        {
    //                            string StrSectorFlight = "";
    //                            string StrCheckInBageADT = "";
    //                            string StrCheckInBageCNN = "";
    //                            string StrCheckInBageINF = "";
    //                            foreach (DataRow dr in drBagAdt)
    //                            {
    //                                StrSectorFlight = dr["Departure"] + " - " + dr["Destination"] + " (" + dr["Airline"] + "" + dr["Flight_No"] + ")";
    //                                StrCheckInBageADT = "" + dr["Checkin_Bag"] + "(Adult)";
    //                            }
    //                            if (drBagCnn != null)
    //                            {
    //                                if (drBagCnn.Length > 0)
    //                                {
    //                                    foreach (DataRow drC in drBagCnn)
    //                                    {
    //                                        StrCheckInBageCNN = "" + drC["Checkin_Bag"] + "(Child)";
    //                                    }
    //                                }
    //                            }
    //                            if (drBagCnn != null)
    //                            {
    //                                if (drBagCnn.Length > 0)
    //                                {
    //                                    foreach (DataRow drInf in drBagInf)
    //                                    {
    //                                        StrCheckInBageINF = "" + drInf["Checkin_Bag"] + "(Infant)";
    //                                    }
    //                                }
    //                            }

    //                            if (airName == StrSectorFlight)
    //                            {

    //                            }
    //                            else
    //                            {
    //                                airName = StrSectorFlight;

    //                                StrBaggageInfo += "<tr>";
    //                                StrBaggageInfo += "<td><span>" + StrSectorFlight + "</span></td>";
    //                                StrBaggageInfo += "<td><span><i class='fa fa-suitcase'></i> " + StrCheckInBageADT + "" + StrCheckInBageCNN + "" + StrCheckInBageINF + " </span></td>";
    //                                StrBaggageInfo += " <td><span><i class='fa fa-suitcase'></i>7kgs </span></td>";
    //                                StrBaggageInfo += "</tr>";
    //                            }
    //                        }
    //                    }
    //                    if (drBagAdt == null && drBagCnn != null)
    //                    {
    //                        if (drBagAdt.Length <= 0 && drBagCnn.Length > 0)
    //                        {
    //                            string StrSectorFlight = "";
    //                            string StrCheckInBageADT = "";
    //                            string StrCheckInBageCNN = "";
    //                            string StrCheckInBageINF = "";
    //                            if (drBagAdt != null)
    //                            {
    //                                if (drBagAdt.Length > 0)
    //                                {
    //                                    foreach (DataRow dr in drBagAdt)
    //                                    {
    //                                        StrSectorFlight = dr["Departure"] + " - " + dr["Destination"] + " (" + dr["Flight_No"] + ")";
    //                                        StrCheckInBageADT = "" + dr["Checkin_Bag"] + "(Adult)";
    //                                    }
    //                                }
    //                            }
    //                            if (drBagCnn != null)
    //                            {
    //                                if (drBagCnn.Length > 0)
    //                                {
    //                                    foreach (DataRow drC in drBagCnn)
    //                                    {
    //                                        StrSectorFlight = drC["Departure"] + " - " + drC["Destination"] + " (" + drC["Airline"] + "" + drC["Flight_No"] + ")";
    //                                        StrCheckInBageCNN = "" + drC["Checkin_Bag"] + "(Child)";
    //                                    }
    //                                }
    //                            }
    //                            if (drBagCnn != null)
    //                            {
    //                                if (drBagCnn.Length > 0)
    //                                {
    //                                    foreach (DataRow drInf in drBagInf)
    //                                    {
    //                                        StrCheckInBageINF = "" + drInf["Checkin_Bag"] + "(Infant)";
    //                                    }
    //                                }
    //                            }

    //                            if (airName == StrSectorFlight)
    //                            {
    //                                StrBaggageInfo += "<tr>";
    //                                StrBaggageInfo += "<td><span>" + StrSectorFlight + "</span></td>";
    //                                StrBaggageInfo += "<td><span><i class='fa fa-suitcase'></i> " + StrCheckInBageADT + "" + StrCheckInBageCNN + "" + StrCheckInBageINF + " </span></td>";
    //                                //StrBaggageInfo += " <td><span><i class='fa fa-suitcase'></i> </span></td>";
    //                                StrBaggageInfo += " <td><span></span></td>";
    //                                StrBaggageInfo += "</tr>";
    //                            }
    //                            else
    //                            {
    //                                airName = StrSectorFlight;
    //                            }
    //                        }
    //                    }

    //                }



    //                StrBaggageInfo += "</table>";
    //                StrBaggageInfo += "<hr></hr>";
    //                StrBaggageInfo += "<p>Information not available<span class='text-danger'>*</span></p>";
    //                StrBaggageInfo += "<p class='text-justify'>The information presented above is as obtained from the airline reservation system. TravelMerry does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs, connecting flights and changes in airline rules.</p>";
    //                StrBaggageInfo += "</div>";
    //            }
    //            ////Baggage -end




    //            StrItineraryInfo += StrBaggageInfo;

    //            //}
    //            StrItineraryInfo += "</div>";

    //            StrItineraryInfo += " </div>";
    //        }
    //    }

    //    divItineraryInfo.InnerHtml = "" + StrItineraryInfo + "";

    //    Session["divItineraryInfo"] = StrItineraryInfo;
    //    //divItineraryInfo -end







    //    //divItineraryHead -strat

    //    lbltotalPrice.Text = String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]);
    //    string StrItineraryHead = "";

    //    StrItineraryHead += "<div class='row'>";
    //    StrItineraryHead += "<div class='col-md-12'>";
    //    StrItineraryHead += "<div class='result-bg' style='margin-bottom:15px;'>";
    //    StrItineraryHead += "<div class='result-content'>";
    //    StrItineraryHead += "<div class='row'>";
    //    StrItineraryHead += "<div class='col-md-9'>";
    //    StrItineraryHead += "<div class='result-title'>" + depCity + " <i class='fa fa-arrow-circle-right'></i> " + arrCity + "</div>";
    //    StrItineraryHead += "<div class='result-desc'>depart" + strDepDate + "" + "<span><i class='fa fa-user'></i>  Travelers: " + TotalPaxCnt + "</span></div>";
    //    StrItineraryHead += "</div>";
    //    StrItineraryHead += "<div class='col-md-3'>";
    //    StrItineraryHead += "<div class='hotel-item-price'>$" + String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]) + " <span class='hotel-item-price-from'>Total Price</span></div>";
    //    StrItineraryHead += "<button type='button' class='btn btn-sky text-capitalize btn-md pull-right' style='margin-top:12px;'>Pay Now</button>";
    //    StrItineraryHead += "</div>";

    //    StrItineraryHead += "</div>";

    //    StrItineraryHead += "</div>";
    //    StrItineraryHead += "</div>";

    //    StrItineraryHead += "</div>";
    //    StrItineraryHead += "</div>";

    //    //lbltotalPrice.Text = String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]);

    //    //divItineraryHead.InnerHtml = StrItineraryHead;

    //    //divItineraryHead -end



    //    //divBookingId -start

    //    string StrBookingId = "";

    //    StrBookingId += "<div class='col-md-12'>";
    //    StrBookingId += "<h4 class='blue-text' style='margin-top:0px; margin-bottom:0px;'>PNR: " + PNR + "</h4>";
    //    StrBookingId += "</div>";

    //    divBookingId.InnerHtml = "" + StrBookingId + "";
    //    Session["divBookingId"] = StrBookingId;
    //    //divBookingId -end


    //    //divPassengerInfo -start

    //    string StrPassengerInfo = "";

    //    if (tblPax != null)
    //    {
    //        if (tblPax.Rows.Count > 0)
    //        {
    //            StrPassengerInfo += "<div class='col-md-12'>";
    //            StrPassengerInfo += "<h4><i class='fa fa-user'></i> Traveler Details</h4>";
    //            StrPassengerInfo += "</div>";

    //            int i = 1;
    //            foreach (DataRow drtblPax in tblPax.Rows)
    //            {
    //                //pnlpassenger.Controls.Add(new LiteralControl("<tr>"));
    //                //pnlpassenger.Controls.Add(new LiteralControl("<td height='20' align='left' valign='middle' style='text-transform: capitalize;'><table width='200px' height='15px'><tr><td width='20%'><img src='images/adult-icon.png' /></td><td width='80%'> " + drtblPax["LastName"].ToString().ToLower() + " " + drtblPax["FirstName"].ToString().ToLower() + "</td></tr></table></td>"));
    //                //pnlpassenger.Controls.Add(new LiteralControl("</tr>"));
    //                StrPassengerInfo += "<div class='col-md-4'>";
    //                StrPassengerInfo += "<p>" + i + "." + drtblPax["LastName"].ToString().ToLower() + " " + drtblPax["FirstName"].ToString().ToLower() + "</p>";
    //                StrPassengerInfo += "</div>";
    //                i++;
    //            }

    //            divPassengerInfo.InnerHtml = "" + StrPassengerInfo + "";
    //            Session["divPassengerInfo"] = StrPassengerInfo;
    //        }
    //    }

    //    //divPassengerInfo -end





    //}
    protected void DisplayData_1S_New(string resultPNR1, DataTable dtPay)
    {

        //string amm = HttpContext.Current.Session["Insurance" ].ToString();
        //if (Session["Insurance"+ ssid] != null)
        //{
        //    string amm2 = HttpContext.Current.Session["Insurance" + ssid].ToString();
        //}
        //if (Session["Ins"].ToString() == "no")
        //{
        //    string amm3 = HttpContext.Current.Session["Insurance" + ssid].ToString();
        //}

        //if (Session["Insurance" + ssid] != null)
        //{
        //    string ammn = HttpContext.Current.Session["Insurance" + ssid].ToString();

        //}


            double cancellation = 0;
        double Promotion = 0;
        double insurance_total = 0;
        if (resultPNR1 != "")
        {


            SqlParameter[] paramExtended = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "26") };
            DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);
            ttam1 = TtAmount;
            if (dtExtend.Rows.Count > 0)
            {
                if (Session["Extended"] != null)
                {
                    

                    if (Session["Extended"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    }
                    else if (Session["Extended"].ToString() == "yes")
                    {
                        cancellation = (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        ttam1 = ttam1 + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                    }
                }
                else
                    Session["Extended"] = "no";
            }
            else
            {
                if (Session["Extended"] != null)
                {
                    if (Session["Extended"].ToString() == "yes")
                    {
                        ttam1 = ttam1 + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                        //TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                        cancellation = (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                    }
                    else if (Session["Extended"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    }
                }
                else
                    Session["Extended"] = "no";
            }




            SqlParameter[] paramExtended2 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "28") };
            DataTable dtExtend2 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended2);
            ttam1 = TtAmount;
            if (dtExtend2.Rows.Count > 0)
            {
                if (Session["Ins"] != null)
                {
                    

                    if (Session["Ins"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend2.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend2.Rows[0]["Units"].ToString()));
                    }
                    else if (Session["Ins"].ToString() == "yes")
                        {
                        insurance_total = (Convert.ToDouble(dtExtend2.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend2.Rows[0]["Units"].ToString()));
                        if (Session["Insurance"] != null)
                        {
                            ttam1 = ttam1 + (Convert.ToDouble(Session["Insurance"].ToString()));
                        }
                       }
                }
                else
                    Session["Ins"] = "no";
            }
            else
            {
                if (Session["Ins"] != null)
                {
                    if (Session["Ins"].ToString() == "yes")
                    {
                        ttam1 = ttam1 + (Convert.ToDouble(Session["Insurance"].ToString()));
                        //TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["Insurance"].ToString()));
                        insurance_total = (Convert.ToDouble(Session["Insurance"].ToString()));
                    }
                    else if (Session["Ins"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    }
                }
                else
                    Session["Ins"] = "no";
            }

















            if (Session["VoucherAmount" + ssid] != null)
            {
                ttam1 = ttam1 - (Convert.ToDouble(Session["VoucherAmount"].ToString()));
                Promo = "yes";
                PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
                                                                                                                  //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
                Promotion = Convert.ToDouble(PromoAm) * (-1);
            }
            if (Session["Extended"] != null)
            {
                if (Session["Extended"].ToString() == "yes")
                    cancel = "yes";
                else
                    cancel = "no";
                //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
            }


            if (Session["Ins"] != null)
            {
                if (Session["Ins"].ToString() == "yes")
                    insurance = "yes";
                else
                    insurance = "no";
                //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
            }


            if (Session["accepts" + ssid] != null)
            {
                if (Session["accepts" + ssid].ToString() == "accepted")
                {
                }
            }
            bool flag = false;
            DataTable tblPax = CreatePaxTable();
            DataTable tblitinerary = CreateItinTable();
            DataTable tblBaggage = CreateBaggageTable();

            string PNR = dtPay.Rows[0]["pnrno"].ToString();

            string OutBoundOrigin = "";
            string OutBoundDestination = "";
            string InBoundOrigin = "";
            string InBoundDestination = "";

            string OutBoundOriginNew = "";
            string OutBoundDestinationNew = "";
            string InBoundOriginNew = "";
            string InBoundDestinationNew = "";


            int start = resultPNR1.IndexOf("<TravelItineraryReadRS");
            int end = resultPNR1.IndexOf("</TravelItineraryReadRS>");

            resultPNR1 = resultPNR1.Substring(start, end - start) + "</TravelItineraryReadRS>";

            resultPNR1 = resultPNR1.Replace("CustomerName", "PersonName");

            TravelItineraryReadRS objTravelItineraryReadRS = TravelItineraryReadRS_XML.ResXml(resultPNR1);





            SqlParameter[] paramBooking = { new SqlParameter("@Booking_ID", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@PNR", dtPay.Rows[0]["pnrno"].ToString()) };
            DataTable dtPNR_ID = DataLayer.GetData("select_booking_booking_pnr_PNRID", paramBooking, ConfigurationManager.AppSettings["sqlcn"].ToString());
            if (dtPNR_ID != null)
            {
                if (dtPNR_ID.Rows.Count > 0)
                {
                    SqlParameter[] paramPNR = { new SqlParameter("@PNR_ID", dtPNR_ID.Rows[0]["PNR_ID"].ToString()) };
                    DataTable dtPNR = DataLayer.GetData("select_PNR", paramPNR, ConfigurationManager.AppSettings["sqlcn"].ToString());

                    if (dtPNR != null)
                    {
                        if (dtPNR.Rows.Count > 0)
                        {

                            OutBoundOrigin = dtPNR.Rows[0]["Depature"].ToString();
                            OutBoundDestination = dtPNR.Rows[0]["Destination"].ToString();
                            InBoundOrigin = dtPNR.Rows[0]["Return_Depature_From"].ToString();
                            InBoundDestination = dtPNR.Rows[0]["Return_Depature_To"].ToString();

                            OutBoundOriginNew = dtPNR.Rows[0]["Depature"].ToString();
                            OutBoundDestinationNew = dtPNR.Rows[0]["Destination"].ToString();
                            InBoundOriginNew = dtPNR.Rows[0]["Return_Depature_From"].ToString();
                            InBoundDestinationNew = dtPNR.Rows[0]["Return_Depature_To"].ToString();


                        }
                    }

                }

            }







            if (objTravelItineraryReadRS != null)
            {
                if (objTravelItineraryReadRS.TravelItinerary != null)
                {
                    if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName != null)
                        {
                            if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName.Length > 0)
                            {
                                for (int paxcnt = 0; paxcnt < objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName.Length; paxcnt++)
                                {
                                    DataRow drPax = tblPax.NewRow();
                                    Regex reg = new Regex(@"\bM(?:RS?|R|S|ISS|STR)\b");

                                    MatchCollection matches = reg.Matches(objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString());

                                    //var sfsd = reg.Match(objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString());
                                    //alert(str.match(pattern));
                                    if (matches.Count > 0)
                                        foreach (Match match in matches)
                                            drPax["Title"] = match.Value;
                                    else
                                        drPax["Title"] = "";

                                    if (drPax["Title"].ToString().ToUpper() == "")
                                        drPax["Title"] = dtbookingpax.Rows[paxcnt]["Pax_Title"];
                                    

                                    drPax["LastName"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].Surname.ToString();
                                    drPax["FirstName"] = reg.Replace(objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].GivenName.ToString(), "");

                                    if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType != null)
                                        drPax["PaxType"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].PassengerType;
                                    else
                                    {
                                        if (objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].Infant == "true")
                                            drPax["PaxType"] = "INF";
                                        else
                                            drPax["PaxType"] = objTravelItineraryReadRS.TravelItinerary.CustomerInfo.PersonName[paxcnt].NameReference;
                                    }
                                    tblPax.Rows.Add(drPax);
                                }
                            }
                            else
                            {
                                //PNRStatus = false;
                                RedirectCS();
                            }
                        }
                        else
                        {
                            //PNRStatus = false;
                            RedirectCS();
                        }
                    }
                    else
                    {
                        //PNRStatus = false;
                        RedirectCS();
                    }


                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryRef != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryRef.ID != null)
                        {
                            PNR = objTravelItineraryReadRS.TravelItinerary.ItineraryRef.ID;
                        }
                    }

                    //Baggage -start
                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing != null)
                        {
                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote != null)
                            {
                                for (int ItCnt = 0; ItCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote.Length; ItCnt++)
                                {
                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt] != null)
                                    {
                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary != null)
                                        {
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo != null)
                                            {
                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PassengerTypeQuantity != null)
                                                {
                                                    string PaxType = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PassengerTypeQuantity[0].Code;
                                                    if (PaxType == "JCB")
                                                        PaxType = "ADT";
                                                    if (PaxType == "JNN")
                                                        PaxType = "CNN";
                                                    if (PaxType == "JNF")
                                                        PaxType = "INF";

                                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown != null)
                                                    {
                                                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment != null)
                                                        {
                                                            int scg = 0;
                                                            for (int sCnt = 0; sCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length; sCnt++)
                                                            {

                                                                ///*******************************/
                                                                //if (sCnt == 0)
                                                                //{
                                                                //    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd == "O")
                                                                //    {

                                                                //        OutBoundOriginNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
                                                                //    }
                                                                //    if (sCnt == objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length - 1)
                                                                //    {
                                                                //        OutBoundDestinationNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
                                                                //    }
                                                                //}

                                                                //if (sCnt > 0)
                                                                //{
                                                                //    if (!flag && objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd == "O")
                                                                //    {
                                                                //        flag = true;
                                                                //        OutBoundDestinationNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
                                                                //        InBoundOriginNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
                                                                //    }
                                                                //    if (sCnt == objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment.Length - 1)
                                                                //    {
                                                                //        InBoundDestinationNew = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode;
                                                                //    }
                                                                //}
                                                                ///***-----------------------*************************/

                                                                string DepBag = "";
                                                                string DesBag = "";
                                                                if (sCnt == 0)
                                                                {
                                                                    DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                }
                                                                /*if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd == "O")
                                                                {
                                                                    DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                }
                                                                else
                                                                {
                                                                    DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                    if (tblBaggage.Rows.Count > 0)
                                                                        tblBaggage.Rows[tblBaggage.Rows.Count - 1]["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                }*/
                                                                if (sCnt > 0)
                                                                {
                                                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].ConnectionInd != "O")
                                                                        DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                    else
                                                                        DepBag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                    if (tblBaggage.Rows.Count > 0)
                                                                        tblBaggage.Rows[tblBaggage.Rows.Count - 1]["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].OriginLocation.LocationCode.ToString();
                                                                }


                                                                scg++;
                                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].BaggageAllowance != null)
                                                                {
                                                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].BaggageAllowance.Number != null)
                                                                    {
                                                                        DataRow drBag = tblBaggage.NewRow();
                                                                        string bag = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].BaggageAllowance.Number.ToString();


                                                                        drBag["Segment_No"] = scg;
                                                                        drBag["Airline"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].MarketingAirline.Code;
                                                                        drBag["Flight_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[ItCnt].PricedItinerary[0].AirItineraryPricingInfo.PTC_FareBreakdown[0].FlightSegment[sCnt].FlightNumber;
                                                                        drBag["Departure"] = DepBag;
                                                                        drBag["Checkin_Bag"] = bag;
                                                                        drBag["PaxType"] = PaxType;
                                                                        //tblBagg.Columns.Add("Segment_No", typeof(int));
                                                                        //tblBagg.Columns.Add("Airline", typeof(string));
                                                                        //tblBagg.Columns.Add("Flight_No", typeof(string));
                                                                        //tblBagg.Columns.Add("Departure", typeof(string));
                                                                        //tblBagg.Columns.Add("Destination", typeof(string));
                                                                        //tblBagg.Columns.Add("Checkin_Bag", typeof(string));
                                                                        //tblBagg.Columns.Add("Cabin_Bag", typeof(string));
                                                                        //tblBagg.Columns.Add("PaxType", typeof(string));
                                                                        tblBaggage.Rows.Add(drBag);
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                    //Baggage -end

                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems != null)
                        {
                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length > 0)
                            {
                                int segLength = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length;
                                for (int segno = 0; segno < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length; segno++)
                                {

                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment != null)
                                    {
                                        if (!objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Status.ToString().Contains("WK"))
                                        {
                                            DataRow drSegment = tblitinerary.NewRow();

                                            /*******************************/
                                            if (segno == 0)
                                            {
                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ConnectionInd == "O")
                                                {
                                                    drSegment["OutBoundOrigin"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
                                                    OutBoundOrigin = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
                                                }
                                                if (segno == segLength - 1)
                                                {
                                                    drSegment["OutBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
                                                    OutBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
                                                }
                                            }

                                            if (segno > 0)
                                            {
                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ConnectionInd == "O")
                                                {
                                                    //drSegment["OutBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno - 1].FlightSegment[0].DestinationLocation.LocationCode;
                                                    if (tblitinerary != null)
                                                    {
                                                        if (tblitinerary.Rows.Count > 0)
                                                        {
                                                            OutBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno - 1].FlightSegment[0].DestinationLocation.LocationCode;
                                                            tblitinerary.Rows[tblitinerary.Rows.Count - 1]["OutBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno - 1].FlightSegment[0].DestinationLocation.LocationCode;
                                                        }
                                                    }
                                                    InBoundOrigin = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
                                                    drSegment["InBoundOrigin"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
                                                    //if (segno == segLength - 1)
                                                    //{
                                                    //    drSegment["InBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
                                                    //    InBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
                                                    //}                                            
                                                }
                                                if (segno == segLength - 1)
                                                {
                                                    drSegment["InBoundDestination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
                                                    InBoundDestination = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;
                                                }
                                            }

                                            /*******************************/

                                            drSegment["PNR_ID"] = PNR;
                                            drSegment["Segment_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SegmentNumber;
                                            drSegment["Airline"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].MarketingAirline.Code;
                                            drSegment["Flight_No"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].MarketingAirline.FlightNumber;
                                            /***Change ******************/
                                            string StrCodeshare = "";
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OperatingAirline != null)
                                            {
                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OperatingAirline[0].Code != null)
                                                {
                                                    StrCodeshare = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OperatingAirline[0].Code;
                                                }
                                            }
                                            drSegment["Codeshare_Airline"] = StrCodeshare;
                                            /***Change ******************/
                                            drSegment["Class"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ResBookDesigCode;
                                            drSegment["Departure"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.LocationCode;
                                            drSegment["Destination"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.LocationCode;



                                            string dept_date = "";
                                            string dept_time = "";

                                            string dept_date_updated = "";
                                            string arr_date_updated = "";


                                            string arr_date = "";
                                            string arr_time = "";

                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime != null)
                                            {
                                                dept_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime.Split('T')[1];
                                                dept_date_updated = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime.Split('T')[0];
                                                dept_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[0];

                                            }
                                            else
                                            {
                                                dept_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[1];
                                                dept_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DepartureDateTime.Split('T')[0];

                                            }
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime != null)
                                            {
                                                arr_date_updated = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime.Split('T')[0];
                                                arr_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime.Split('T')[1];
                                                arr_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[0];


                                            }
                                            else
                                            {
                                                arr_time = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[1];
                                                arr_date = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ArrivalDateTime.Split('T')[0];

                                            }






                                            string[] strSplitDepDate = dept_date.Split('-');
                                            string[] strSplitArrDate = arr_date.Split('-');
                                            int DepDateYear = Convert.ToInt32(strSplitDepDate[0]);
                                            int ArrDateYear = Convert.ToInt32(strSplitDepDate[0]);
                                            if (strSplitDepDate[1] == "12" && strSplitDepDate[2] == "31" && strSplitArrDate[0] == "01" && strSplitArrDate[1] == "01")
                                            {
                                                ArrDateYear += 1;
                                            }


                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedDepartureTime != null)
                                            {
                                                dept_date = DepDateYear + "-" + dept_date_updated;

                                            }

                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].UpdatedArrivalTime != null)
                                            {
                                                arr_date = arr_date_updated;
                                            }

                                            drSegment["Dept_date"] = dept_date;
                                            drSegment["Dept_time"] = dept_time.Replace(":", "");
                                            drSegment["Arrival_date"] = ArrDateYear + "-" + arr_date;
                                            drSegment["Arrival_time"] = arr_time.Replace(":", "");

                                            TimeSpan ts = (DateTime.Parse(arr_date.Substring(0, 2) + "/" + arr_date.Substring(3, 2) + "/" + ArrDateYear, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))) - DateTime.Parse(dept_date.Substring(5, 2) + "/" + dept_date.Substring(8, 2) + "/" + dept_date.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

                                            /*********************/
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ElapsedTime != null)
                                            {
                                                drSegment["ElapsedTime"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].ElapsedTime.ToString();
                                            }

                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal != null)
                                            {
                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal.Length > 0)
                                                {
                                                    drSegment["MealTypes"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Meal[0].Code.ToString();
                                                }
                                            }
                                            /*********************/

                                            drSegment["Nextday"] = ts.Days;

                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Equipment != null)
                                            {
                                                drSegment["Equipment"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].Equipment.AirEquipType;
                                            }
                                            else
                                            {
                                                drSegment["Equipment"] = "";

                                            }
                                            drSegment["Baggage"] = "";
                                            drSegment["Dept_Terminal"] = "";
                                            drSegment["Arrival_Terminal"] = "";

                                            /********************************************/
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.TerminalCode != null)
                                                drSegment["Dept_Terminal"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].OriginLocation.TerminalCode.ToString();
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.TerminalCode != null)
                                                drSegment["Arrival_Terminal"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].DestinationLocation.TerminalCode.ToString();
                                            /********************************************/

                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef != null)
                                            {
                                                if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID != null)
                                                {
                                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID != "")
                                                    {

                                                        string[] Airline_PNRSplit = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID.Split('*');
                                                        if (Airline_PNRSplit.Length > 1)
                                                        {
                                                            drSegment["Airline_PNR"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].SupplierRef.ID.Split('*')[1];
                                                        }
                                                        else
                                                        {
                                                            drSegment["Airline_PNR"] = "";

                                                        }
                                                    }
                                                    else
                                                    {
                                                        drSegment["Airline_PNR"] = "";

                                                    }
                                                }
                                                else
                                                {
                                                    drSegment["Airline_PNR"] = "";

                                                }
                                            }
                                            else
                                            {
                                                drSegment["Airline_PNR"] = "";

                                            }
                                            drSegment["StopOvers"] = objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems[segno].FlightSegment[0].StopQuantity;


                                            tblitinerary.Rows.Add(drSegment);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //PNRStatus = false;
                                RedirectCS();
                            }
                        }
                        else
                        {
                            //PNRStatus = false;
                            RedirectCS();
                        }
                    }
                    else
                    {
                        //PNRStatus = false;
                        RedirectCS();
                    }


                }
                else
                {
                    //PNRStatus = false;
                    RedirectCS();
                }
            }
            else
            {
                //PNRStatus = false;
                RedirectCS();
            }


            //calculating Total pax, Adult, Child, Infant -start
            int TotalPaxCnt = 0;
            if (tblPax != null)
            {
                if (tblPax.Rows.Count > 0)
                {
                    TotalPaxCnt = tblPax.Rows.Count;
                }
            }
            //calculating Total pax, Adult, Child, Infant -end

            //divPassengerInfo -start

            string StrPassengerInfo = "";
            string StrPassengerInfoConfirm = "";

            if (tblPax != null)
            {
                if (tblPax.Rows.Count > 0)
                {

                    StrPassengerInfo +=     "<div class='pm-head-title'>" +
                                                "<div class='pm-content'>" +
                                                    "<div class='travelerTitle faa-parent animated-hover'>" +
                                                        "<img src='Design/images/passenger-icon.png' alt='Passenger icon' class='title-icons faa-passing'> Traveller Details <P>(Title, Last Name, First Name, Passenger Type)</P>" +
                                                    "</div>" +
                                                "</div>" +
                                            "</div>" +
                                            "<div class='pm-content'>" +
                                                "<div class='pm-traveler-list'>" +
                                                    "<ul>";
                    StrPassengerInfoConfirm += "<ul>";
                    labelCount++;
                    int i = 1;

                    //if (dtbookingpax.Rows.Count <= 0)
                    //{
                    foreach (DataRow drtblPax in tblPax.Rows)
                    {
                        string PaxType = "";
                        if (drtblPax["PaxType"].ToString().ToUpper() == "ADT")
                            PaxType = "(ADULT)";
                        else if (drtblPax["PaxType"].ToString().ToUpper() == "JCB")
                            PaxType = "(ADULT)";
                        else if (drtblPax["PaxType"].ToString().ToUpper() == "JNN")
                            PaxType = "(CHILD)";
                        else if (drtblPax["PaxType"].ToString().ToUpper() == "CNN")
                            PaxType = "(CHILD)";
                        else if (drtblPax["PaxType"].ToString().ToUpper() == "INF")
                            PaxType = "(INFANT WITHOUT SEAT)";
                        else if (drtblPax["PaxType"].ToString().ToUpper() == "CNF")
                            PaxType = "(INFANT WITHOUT SEAT)";
                        else
                            PaxType = drtblPax["title"].ToString().ToUpper();


                        StrPassengerInfo += "<li>" + i + ". " + drtblPax["title"].ToString().ToUpper() + " " + drtblPax["LastName"].ToString().ToUpper() + " " + drtblPax["FirstName"].ToString().ToUpper() + " " + PaxType + " </li>";
                        StrPassengerInfoConfirm += "<li>" + i + ". " + drtblPax["title"].ToString().ToUpper() + " " + drtblPax["LastName"].ToString().ToUpper() + " " + drtblPax["FirstName"].ToString().ToUpper() + " " + PaxType + " </li>";
                        i++;
                    }
                    //}
                    //else
                    //{
                    //    foreach (DataRow drtblPax in dtbookingpax.Rows)
                    //    {
                    //        string PaxType = "";
                    //        if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "1")
                    //            PaxType = "ADULT";
                    //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "1")
                    //            PaxType = "ADULT";
                    //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "2")
                    //            PaxType = "CHILD";
                    //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "2")
                    //            PaxType = "CHILD";
                    //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "3")
                    //            PaxType = "INFANT WITHOUT SEAT";
                    //        else if (drtblPax["Pax_Type_Id"].ToString().ToUpper() == "3")
                    //            PaxType = "INFANT WITHOUT SEAT";
                    //        else
                    //            PaxType = drtblPax["title"].ToString().ToUpper();


                    //        StrPassengerInfo += "<li>" + i + ". " + drtblPax["Pax_Title"].ToString().ToUpper() + " " + drtblPax["Pax_Surname"].ToString().ToUpper() + " " + drtblPax["Pax_FirstName"].ToString().ToUpper() + " (" + PaxType + ") </li>";
                    //        StrPassengerInfoConfirm += "<li>" + i + ". " + drtblPax["Pax_Title"].ToString().ToUpper() + " " + drtblPax["Pax_Surname"].ToString().ToUpper() + " " + drtblPax["Pax_FirstName"].ToString().ToUpper() + " (" + PaxType + ") </li>";
                    //        i++;
                    //    }
                    //}
                    StrPassengerInfo += "</ul>" +
                                                "</div>" +
                                            "</div>";
                    StrPassengerInfoConfirm += "</ul>";

                    divPassengerInfo.InnerHtml = "" + StrPassengerInfo + "";
                    Session["divPassengerInfo" + ssid] = StrPassengerInfo;
                    Session["StrPassengerInfoConfirm" + ssid] = StrPassengerInfoConfirm;
                }
            }

            //divPassengerInfo -end

            //New York,United States
            string departure = "";
            //Bangkok,Thailand
            string destination = "";
            //<time class='comment-date' datetime='07/14/2015'><i class='fa fa-clock-o'></i>Jul 14 , 2015</time> <i class='fa fa-arrow-circle-right'></i><time class='comment-date padlft-rgt' datetime='07/16/2015'><i class='fa fa-clock-o'></i> Jul 16 , 2015</time>
            string depDestTimings = "";


            //divItineraryInfo -strat


            string depCity = "";
            string desCity = "";
            string arrCity = "";
            string retCity = "";

            string strFlyFrom = "";
            string strFlyTo = "";
            string strDepDate = "";
            string strRetDate = "";

            string StrItineraryInfo = "";

            if (tblitinerary != null)
            {
                if (tblitinerary.Rows.Count > 0)
                {
                    StrItineraryInfo += "<div class='pm-head-title'>" +
                                                        "<div class='pm-content'>" +
                                                            "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Flight details" +
                                                        "</div>" +
                                                    "</div>";
                    labelCount++;
                    StrItineraryInfo += "<div class='pm-content'>";

                    for (int SegCnt = 0; SegCnt < tblitinerary.Rows.Count; SegCnt++)
                    {
                        try
                        {
                            /*****************************************/

                            DataTable dtFlight = FlightCode(tblitinerary.Rows[SegCnt]["Airline"].ToString());
                            string strAirline = "";
                            string strAirlineimg = "";
                            if (dtFlight.Rows.Count > 0)
                            {
                                strAirline = dtFlight.Rows[0]["AirlineShortName"].ToString();
                                strAirlineimg = "Design/shortlogo/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";

                            }
                            else
                            {
                                strAirline = tblitinerary.Rows[SegCnt]["Airline"].ToString();
                                strAirlineimg = "Design/shortlogo/" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + ".gif";
                            }

                            DataTable dtDepArp = GetDara(tblitinerary.Rows[SegCnt]["Departure"].ToString());
                            string strDepCity1 = "";
                            string strDepCity = "";
                            string strDepCountry = "";
                            if (dtDepArp.Rows.Count > 0)
                            {
                                strDepCity1 = dtDepArp.Rows[0]["City"].ToString();
                                strDepCity = dtDepArp.Rows[0]["Airport"].ToString() + ", " + dtDepArp.Rows[0]["City"].ToString();
                                strDepCountry = dtDepArp.Rows[0]["Country"].ToString();
                            }
                            else
                            {
                                strDepCity1 = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                                strDepCity = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                                strDepCountry = tblitinerary.Rows[SegCnt]["Departure"].ToString();
                            }
                            string dtDay1 = "";
                            string dtMonth1 = "";
                            string dtYear1 = "";
                            string dtDay2 = "";
                            string dtMonth2 = "";
                            string dtYear2 = "";
                            dtDay1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(8, 2);
                            dtMonth1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(5, 2);
                            dtYear1 = tblitinerary.Rows[SegCnt]["Dept_date"].ToString().Substring(0, 4);

                            dtDay2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(8, 2);
                            dtMonth2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(5, 2);
                            dtYear2 = tblitinerary.Rows[SegCnt]["Arrival_date"].ToString().Substring(0, 4);



                            DataTable dtArrArp = GetDara(tblitinerary.Rows[SegCnt]["Destination"].ToString());
                            string strArrCity1 = "";
                            string strArrCity = "";
                            string strArrCountry = "";
                            if (dtArrArp.Rows.Count > 0)
                            {
                                strArrCity1 = dtArrArp.Rows[0]["City"].ToString();
                                strArrCity = dtArrArp.Rows[0]["Airport"].ToString() + ", " + dtArrArp.Rows[0]["City"].ToString();
                                strArrCountry = dtArrArp.Rows[0]["Country"].ToString();

                            }
                            else
                            {
                                strArrCity1 = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                                strArrCity = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                                strArrCountry = tblitinerary.Rows[SegCnt]["Destination"].ToString();
                            }


                            /************************************************************/
                            if (SegCnt == 0)
                            {
                                Session["DEP_DAT" + ssid] = tblitinerary.Rows[SegCnt]["Dept_date"].ToString();
                                //if (tblitinerary.Rows[SegCnt]["OutBoundOrigin"].ToString() != "")
                                //{
                                if (OutBoundOriginNew != "" && OutBoundDestinationNew != "")
                                {

                                    string depDat = "";
                                    string retDat = "";
                                    int stopsCnt = 0;
                                    TimeSpan TtElapsedTimeOut = TimeSpan.Parse("00:00");
                                    /***********Calculating Inbound Duration****************/
                                    for (int dSegCnt = SegCnt; dSegCnt < tblitinerary.Rows.Count; dSegCnt++)
                                    {
                                        if (dSegCnt == 0)
                                        {
                                            depDat = tblitinerary.Rows[dSegCnt]["Dept_date"].ToString();
                                            retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                        }

                                        //if (tblitinerary.Rows[dSegCnt]["InBoundOrigin"].ToString() != "")
                                        if (InBoundOriginNew == tblitinerary.Rows[dSegCnt]["Destination"].ToString())
                                        {
                                            if (dSegCnt == 0)
                                                retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                            else
                                                retDat = tblitinerary.Rows[dSegCnt - 1]["Arrival_date"].ToString();
                                        }
                                        //calculate ElapsedTime    
                                        if (tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString() != "")

                                            //calculate ElapsedTime
                                            if (dSegCnt > 0 && dSegCnt < tblitinerary.Rows.Count)
                                            {
                                                if (InBoundOriginNew == tblitinerary.Rows[dSegCnt]["Destination"].ToString())
                                                {
                                                    stopsCnt++;

                                                    break;
                                                }
                                            }
                                    }
                                    /***********Calculating Inbound Duration****************/
                                    DataTable dtOutOrg = GetDara(OutBoundOriginNew);
                                    if (dtOutOrg.Rows.Count > 0)
                                        OutBoundOrigin = dtOutOrg.Rows[0]["City"].ToString();
                                    DataTable dtOutDest = GetDara(OutBoundDestinationNew);
                                    if (dtOutDest.Rows.Count > 0)
                                        OutBoundDestination = dtOutDest.Rows[0]["City"].ToString();

                                    strDepDate = DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();
                                    strRetDate = DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper();


                                    StrItineraryInfo += "<div class='pm-triptype'>" +
                                                                        "<div class='pm-left'>" +
                                                                            "<div class='pm-ObTitle pm-outbound pm-citi-code-time '><i class='fa fa-plane'></i> <span class='blue'>" + OutBoundOrigin + " - " + OutBoundDestination + "</span> " + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</div>" +
                                                                        "</div>" +
                                                                    "</div>";
                                    string ArrivalDay = "";
                                    DateTime dep1 = DateTime.Parse(depDat.Substring(5, 2) + "/" + depDat.Substring(8, 2) + "/" + depDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                    DateTime ret2 = DateTime.Parse(retDat.Substring(5, 2) + "/" + retDat.Substring(8, 2) + "/" + retDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                    double days = (ret2 - dep1).TotalDays;
                                    if (days == 0)
                                        ArrivalDay = " - Same Day Arrival";
                                    else
                                        ArrivalDay = " - " + days + " Day Arrival";
                                    string StrStops = "";
                                    if (stopsCnt == 0)
                                        StrStops = " - Direct Flight";
                                    else
                                        StrStops = " - " + stopsCnt + " stop";

                                }
                            }

                            if (SegCnt > 0)
                            {
                                if (InBoundOriginNew == tblitinerary.Rows[SegCnt]["Departure"].ToString())
                                {
                                    if (InBoundOriginNew != "" && InBoundDestinationNew != "")
                                    {
                                        string depDat = "";
                                        string retDat = "";
                                        int stopsCnt = 0;
                                        TimeSpan TtElapsedTimeIn = TimeSpan.Parse("00:00");
                                        /***********Calculating Inbound Duration****************/
                                        for (int dSegCnt = SegCnt; dSegCnt < tblitinerary.Rows.Count; dSegCnt++)
                                        {
                                            if (dSegCnt == SegCnt)
                                            {
                                                depDat = tblitinerary.Rows[dSegCnt]["Dept_date"].ToString();
                                                retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                            }
                                            //calculate ElapsedTime   
                                            //if (tblitinerary.Rows[dSegCnt]["ElapsedTime"].ToString().Split('.').Length > 1)

                                            //calculate ElapsedTime
                                            if (dSegCnt > SegCnt && dSegCnt < tblitinerary.Rows.Count)
                                            {
                                                retDat = tblitinerary.Rows[dSegCnt]["Arrival_date"].ToString();
                                                stopsCnt++;


                                                try
                                                {
                                                }
                                                catch
                                                { }
                                            }
                                        }
                                        /***********Calculating Inbound Duration****************/

                                        DataTable dtInOrg = GetDara(InBoundOriginNew);
                                        if (dtInOrg.Rows.Count > 0)
                                            InBoundOrigin = dtInOrg.Rows[0]["City"].ToString();
                                        DataTable dtInDest = GetDara(InBoundDestinationNew);
                                        if (dtInDest.Rows.Count > 0)
                                            InBoundDestination = dtInDest.Rows[0]["City"].ToString();

                                        StrItineraryInfo += "<div class='pm-triptype mt-15'>" +
                                                                        "<div class='pm-left'>" +
                                                                            "<div class='pm-ObTitle pm-inbond pm-citi-code-time'><i class='fa fa-plane'></i> <span class='blue'>" + InBoundOrigin + " - " + InBoundDestination + "</span>  " + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</div>" +
                                                                        "</div>" +

                                                                    "</div>";
                                        string ArrivalDay = "";
                                        DateTime dep1 = DateTime.Parse(depDat.Substring(5, 2) + "/" + depDat.Substring(8, 2) + "/" + depDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                        DateTime ret2 = DateTime.Parse(retDat.Substring(5, 2) + "/" + retDat.Substring(8, 2) + "/" + retDat.Substring(0, 4), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                                        double days = (ret2 - dep1).TotalDays;
                                        if (days == 0)
                                            ArrivalDay = " - Same Day Arrival";
                                        else
                                            ArrivalDay = " - " + days + " Day Arrival";
                                        string StrStops = "";
                                        if (stopsCnt == 0)
                                            StrStops = " - Direct Flight";
                                        else
                                            StrStops = " - " + stopsCnt + " stop";
                                    }
                                }
                            }

                            if (SegCnt > 0 && SegCnt < tblitinerary.Rows.Count)
                            {
                                if (InBoundOriginNew != tblitinerary.Rows[SegCnt]["Departure"].ToString())
                                {

                                    string StopOverTime = CalculateStopOver(tblitinerary.Rows[SegCnt - 1]["Arrival_date"].ToString() + "~" + tblitinerary.Rows[SegCnt - 1]["Arrival_time"].ToString(), tblitinerary.Rows[SegCnt]["Dept_date"].ToString() + "~" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString());

                                    DataTable dtLayoverAirport = GetDara(tblitinerary.Rows[SegCnt - 1]["Destination"].ToString());
                                    if (dtLayoverAirport != null)
                                    {
                                        if (dtLayoverAirport.Rows.Count > 0)
                                        {
                                            StrItineraryInfo += "<div class='pm-layover'>" +
                                                                                "<div class='pm-bag-info-cont text-center'>" +
                                                                                    "<p><i class='icofont icofont-clock-time'></i> " +
                                                                                    "" + dtLayoverAirport.Rows[0]["Airport"] + ", " + dtLayoverAirport.Rows[0]["City"] + " (" + tblitinerary.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" +
                                                                                    StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] +
                                                                                    "min Layover</span></p>" +
                                                                                "</div>" +
                                                                            "</div>";
                                        }
                                        else
                                        {
                                            StrItineraryInfo += "<div class='pm-layover'>" +
                                                                                "<div class='pm-bag-info-cont text-center'>" +
                                                                                    "<p><i class='icofont icofont-clock-time'></i> (" + tblitinerary.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" +
                                                                                    StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] +
                                                                                    "min Layover</span></p>" +
                                                                                "</div>" +
                                                                            "</div>";
                                        }
                                    }
                                    else
                                    {
                                        StrItineraryInfo += "<div class='pm-layover'>" +
                                                                                "<div class='pm-bag-info-cont text-center'>" +
                                                                                    "<p><i class='icofont icofont-clock-time'></i> (" + tblitinerary.Rows[SegCnt - 1]["Destination"].ToString() + ") - <span class='red'>" + StopOverTime.Split(':')[0] + "hrs " + StopOverTime.Split(':')[1] + "min Layover</span></p>" +
                                                                                "</div>" +
                                                                            "</div>";
                                    }
                                }
                            }
                            /************************************************************/
                            StrItineraryInfo += "<div class='pm-seg-wrap'>";
                            StrItineraryInfo += "<div class='pm-seg-content'>";
                            string strAirlineCodeSHare = "";
                            if (tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString() != "")
                            {
                                DataTable dtFlightCodeShare = FlightCode(tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString());
                                if (dtFlightCodeShare.Rows.Count > 0)
                                {
                                    strAirlineCodeSHare = dtFlightCodeShare.Rows[0]["AirlineShortName"].ToString();

                                }
                                else
                                {
                                    strAirlineCodeSHare = tblitinerary.Rows[SegCnt]["Codeshare_Airline"].ToString();
                                }
                                if (strAirlineCodeSHare != "")
                                    strAirlineCodeSHare = "<p> Opearated By " + strAirlineCodeSHare + "</p>";
                            }
                            #region pm-air-logo
                            StrItineraryInfo += "<div class='pm-air-logo'>" +
                                                    "<img src='" + strAirlineimg + "' alt='" + strAirlineimg + "'>" +
                                                    "<p><span class='mr-15'>" + strAirline + "</span>	" +
                                                    "<span class='mr-15'>(" + tblitinerary.Rows[SegCnt]["Airline"].ToString() + String.Format("{0:0000}", Convert.ToInt32(tblitinerary.Rows[SegCnt]["Flight_No"].ToString())) + ")</span>	" +
                                                    "<span class='mr-15'>(Aircraft: " + tblitinerary.Rows[SegCnt]["Equipment"].ToString() + ")</span>";

                            #endregion

                            
                            #region pm-seg-A
                            StrItineraryInfo += "</p>"+ strAirlineCodeSHare + "</div>";
                            StrItineraryInfo += "<div class='pm-seg-A'>";

                            #region pm-seg-from
                            StrItineraryInfo += "<div class='pm-seg-from'>";
                            StrItineraryInfo += "<div class='pm-citi-code-time'><span class='blue tootlTip' data-toggle='tooltip' title='" + strDepCity + "' data-original-title='" + strDepCity + "'>" + tblitinerary.Rows[SegCnt]["Departure"].ToString() + "</span> " +
                                FormatTime(tblitinerary.Rows[SegCnt]["Dept_time"].ToString()) +
                                //"" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Dept_time"].ToString().ToCharArray()[3] + 
                                "</div>";
                            StrItineraryInfo += "<p class='tootlTip' data-toggle='tooltip' title='" + strDepCity + "' data-original-title='" + strDepCity + "'>" + strDepCity + ",</p>" +
                                                    //"<p>San Francisco, CA, United States</p>" +
                                                    "<p>" + DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</p>";
                            StrItineraryInfo += "</div>";
                            #endregion

                            #region pm-duration
                            if (tblitinerary.Rows[SegCnt]["ElapsedTime"] != null)
                            {
                                if (tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString() != "")
                                {
                                    StrItineraryInfo += "<div class='pm-duration'>" +
                                                            "<span class='pm-dur-time'>" + tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[0] + "h " + tblitinerary.Rows[SegCnt]["ElapsedTime"].ToString().Split('.')[1] + "m</span>" +
                                                            "<img src='Design/images/grey_durationicon.png' alt='Duration Icon'>" +
                                                            "<span class='pm-dur-text'>Duration</span>" +
                                                        "</div>";
                                }
                            }
                            #endregion

                            #region pm-seg-to
                            StrItineraryInfo += "<div class='pm-seg-to'>" +
                                                    "<div class='pm-citi-code-time'><span class='blue tootlTip' data-toggle='tooltip' title='" + strDepCity + "' data-original-title='" + strDepCity + "'>" + tblitinerary.Rows[SegCnt]["Destination"].ToString() + "</span> " +
                                                    FormatTime(tblitinerary.Rows[SegCnt]["Arrival_time"].ToString()) +
                                                    //"" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[0] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[1] + ":" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[2] + "" + tblitinerary.Rows[SegCnt]["Arrival_time"].ToString().ToCharArray()[3] + 
                                                    "</div>" +
                                                    "<p class='tootlTip' data-toggle='tooltip' title='" + strDepCity + "' data-original-title='" + strDepCity + "'>" + strArrCity + ",</p>" +
                                                    //"<p>San Francisco, CA, United States</p>" +
                                                    "<p>" + DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")).ToString("ddd, MMM dd yyyy").ToUpper() + "</p>" +
                                                "</div>";
                            #endregion

                            #region pm-fl-info
                            StrItineraryInfo += "<div class='pm-fl-info'>";

                            if (tblitinerary.Rows[SegCnt]["Dept_Terminal"] != null)
                            {
                                if (tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() != "")
                                {
                                    StrItineraryInfo += "<p>Departure Terminal: " + tblitinerary.Rows[SegCnt]["Dept_Terminal"].ToString() + "</p>";
                                }
                            }
                            if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"] != null)
                            {
                                if (tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() != "")
                                {
                                    StrItineraryInfo += "<p>Arrival Terminal: " + tblitinerary.Rows[SegCnt]["Arrival_Terminal"].ToString() + "</p>";
                                }
                            }
                            if (tblitinerary.Rows[SegCnt]["Equipment"] != null)
                            {
                                if (tblitinerary.Rows[SegCnt]["Equipment"].ToString() != "")
                                {
                                    string EqpType = getEquipment(tblitinerary.Rows[SegCnt]["Equipment"].ToString());
                                    if (EqpType != "")
                                    {
                                        StrItineraryInfo += "<p>" + EqpType + "</p>";
                                    }
                                }
                            }
                            if (tblitinerary.Rows[SegCnt]["MealTypes"] != null)
                            {
                                if (tblitinerary.Rows[SegCnt]["MealTypes"].ToString() != "")
                                {
                                    string MealType = getMealTypes(tblitinerary.Rows[SegCnt]["MealTypes"].ToString());
                                    if (MealType != "")
                                    {
                                        StrItineraryInfo += "<p>Meals Types:" + tblitinerary.Rows[SegCnt]["MealTypes"].ToString() + "</p>";
                                    }
                                }
                            }
                            if (tblitinerary.Rows[SegCnt]["Airline_PNR"] != null)
                            {
                                if (tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() != "")
                                {
                                    StrItineraryInfo += "<p class='blue'>Airline PNR: " + tblitinerary.Rows[SegCnt]["Airline_PNR"].ToString() + "</p>";
                                }
                            }

                            StrItineraryInfo += "</div>";
                            #endregion

                            StrItineraryInfo += "</div>";
                            #endregion

                            StrItineraryInfo += "</div>";


                            if (tblBaggage.Rows.Count > 0)
                            {
                                if (tblBaggage.Rows.Count == tblitinerary.Rows.Count)
                                {
                                    if ((SegCnt + 1) == Convert.ToInt32(tblBaggage.Rows[SegCnt]["Segment_No"]))
                                    {
                                        //StrItineraryInfo += "<div class='pm-bag-info'>" +
                                        //                        "<div class='pm-bag-info-cont'>" +
                                        //                            "<div class='pm-left pm-bag-txt'><i class='fa fa-suitcase'></i> " + tblBaggage.Rows[SegCnt]["Checkin_Bag"] + " Check-In Baggage, 7 Kgs Hand Baggage</div>" +
                                        //                        //"<div class='pm-right'>" +
                                        //                        //    "<a href='' class='tootlTip pm-meal-txt' data-toggle='tooltip' title='' data-original-title='Free meal' style=''>" +
                                        //                        //    "<i class='icofont icofont-restaurant'></i> Free Meal" +
                                        //                        //    "</a>" +
                                        //                        //"</div>" +
                                        //                        "</div>" +
                                        //                    "</div>";
                                    }
                                }
                                else
                                {
                                    if (tblBaggage.Rows.Count > SegCnt)
                                    {
                                        if ((SegCnt + 1) == Convert.ToInt32(tblBaggage.Rows[SegCnt]["Segment_No"]))
                                        {
                                            //StrItineraryInfo += "<div class='pm-bag-info'>" +
                                            //                    "<div class='pm-bag-info-cont'>" +
                                            //                        "<div class='pm-left pm-bag-txt'><i class='fa fa-suitcase'></i> " + tblBaggage.Rows[SegCnt]["Checkin_Bag"] + " Check-In Baggage, 7 Kgs Hand Baggage</div>" +
                                            //                    //"<div class='pm-right'>" +
                                            //                    //    "<a href='' class='tootlTip pm-meal-txt' data-toggle='tooltip' title='' data-original-title='Free meal' style=''>" +
                                            //                    //    "<i class='icofont icofont-restaurant'></i> Free Meal" +
                                            //                    //    "</a>" +
                                            //                    //"</div>" +
                                            //                    "</div>" +
                                            //                "</div>";
                                        }
                                    }
                                }
                            }
                            StrItineraryInfo += "</div>";
                            //segment -end                    
                        }
                        catch (Exception ec)
                        {

                        }
                    }
                    StrItineraryInfo += "<div class='mt-10'><span class='font12'> * All the times are local to Airports<span class='pm-right font12'>In-Flight services and amenities may vary and are subject to change.</span></div>";

                    StrItineraryInfo += "</div>";
                    ////note -end
                    
                    string StrBaggageInfo = "";

                    ////Fare Rules -Start

                    //StrBaggageInfo += "<div class='pm-head-title'>" +
                    //                                        "<div class='pm-content'>" +
                    //                                            "<div class='pm-payDetails'>" +
                    //                                                "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Fare Rules" +
                    //                                            "</div>" +
                    //                                        "</div>" +
                    //                                    "</div>" +
                    //                                    "<div class='pm-content'>";
                    //labelCount++;

                    //StrBaggageInfo += "<div class='pm-payment'>" +
                    //                        "<p><strong>Fare Rules:</strong></p>" +
                    //                        "<p>Cancellation Fee</p>" +
                    //                        "<p>" +
                    //                            "Airline Fee* - This is a non-refundable ticket.<br>" +
                    //                            "Travel Merry Admin Fee - USD 150 per passenger where cancelation permitted.<br>" +
                    //                            "Partly utilized tickets cannot be cancelled." +
                    //                        "</p>" +
                    //                            "<p><strong>Change Fee</strong></p>" +
                    //                            "<p>" +
                    //                                "Airline Fee* - This is not changeable ticket.<br>" +
                    //                                "Travel Merry Admin Fee - USD 150 per passenger where change permitted." +
                    //                            "</p>" +
                    //                            "<p>" +
                    //                            "*Airlines stop accepting cancellation/change requests 4 - 72 hours before departure of the flight, depending on the airline." +
                    //                            "The airline fee is indicative based on an automated interpretation of airline fare rules." +
                    //                            "Travel Merry doesn't guarantee the accuracy of this information." +
                    //                            "The change/cancellation fee may also vary based on fluctuations in currency conversion rates." +
                    //                            "For exact cancellation/change fee, please call us at our customer care number." +
                    //                        "</p>" +
                    //                    "</div>";

                    //StrBaggageInfo += "<div class='pm-payment'>" +
                    //                        //"<p><strong>Booking Rules:</strong></p>" +
                    //                        //"<p>Book with confidence: We offer a 4 hour Full Refund Guarantee. </p>" +
                    //                        "<p><strong>Details</strong></p>" +
                    //                        "<p>Fare is not refundable, name change is not permitted and ticket is not transferable.</p>" +
                    //                        "<p>" +
                    //                            "Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee)" +
                    //                            "and is based on availability of flight at the time of change." +
                    //                        "</p>" +
                    //                        "<p> " +
                    //                            "Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change)." +
                    //                        "</p>" +
                    //                        "<p>" +
                    //                            "Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p>" +
                    //                        "<p> Contact our 24/7 toll free customer service to make any changes.</p>" +
                    //                        "<p>" +
                    //                            "Prior to completing the booking in the Terms and Conditions, you should review our service fees for exchanges, changes, refunds and cancellations." +
                    //                        "</p>" +
                    //                    "</div>";

                    //StrBaggageInfo += "</div>";
                    //StrBaggageInfo += "</div>";

                    ////Fare Rules -end







                    if (tblBaggage.Rows.Count > 0)
                    { }

                    //StrBaggageInfo = "<div class='pm-head-title'>" +
                    //                                        "<div class='pm-content'>" +
                    //                                            "<div class='pm-payDetails'>" +
                    //                                                "<img src='Design/images/flight-details.png' alt='Passenger icon' class='title-icons'> Fare Rules" +
                    //                                            "</div>" +
                    //                                        "</div>" +
                    //                                    "</div>" +
                    //                                    "<div class='pm-content'>" +
                    //                                        "<div class='pm-payDetails'>" +
                    //                                            "<div class=''><h5><strong>Booking Rules:</strong></h5><p>Fare is non-refundable, name change is not permitted and ticket is non-transferable.</p><p>Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee) and is based on availability of flight at the time of change.</p><p>Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change).</p><p>Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p><p><strong>Contact our 24/7 customer service to make any changes.</strong></p><p>Prior to completing the booking in the <a href='/Termsandconditions.aspx' target='_blank' class='blue'>Terms and Conditions</a>, you should review our service fees for exchanges, changes, refunds and cancellations.</p></div></div></div>";

                    //divBaggageInfo.InnerHtml = StrBaggageInfo;
                    //Session["divBaggageInfo" + ssid] = StrBaggageInfo;

                }
            }
            divItineraryInfo.InnerHtml = "" + StrItineraryInfo + "";

            Session["divItineraryInfo" + ssid] = StrItineraryInfo;
            //divItineraryInfo -end


            if (tblitinerary.Rows.Count > 0)
            {


                //lbltotalPrice.Text = String.Format("{0:0.00}", dtPay.Rows[0]["Amount"]);

                //divBookingId -start
                string StrBookingId = "";
                string StrBookingIdConfirm = "";
                //TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                //if (Session["VoucherAmount" + ssid] != null)
                //{
                //    Promo = "yes";
                //    PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
                //                                                                                                      //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
                //}
                //if (Session["ExtendedCancellation" + ssid] != null)
                //{
                //    cancel = "yes";
                //    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
                //}



               

                StrBookingId += "<div class='pm-content'>" +
                                                   "<div class='pm-left'>" +
                                                       "<p class='bkid-icon'>" +
                                                           "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                           " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                                       "</p>" +
                                                   "</div>" +
                                                   "<div class='pm-right pm-tl-fare'>" +
                                                       "<p class='pm-tl-text'>Total price</p>" +
                                                           "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " <small>USD</small></h4>" +
                                                           "<p>* inc. all taxes and fees</p>" +
                                                   "</div>" +
                                               "</div>";

                StrBookingIdConfirm += "<div class='confirm-head-Left'>" +
                                                    "<h5>Check Your itinerary <small>(* All the times are local to Airports)</small></h5>" +
                                                "</div>" +
                                                "<div class='pull-right text-right'>" +
                                                    "<p>Total price</p>" +
                                                    "<div class='tplfare bold'>" + String.Format("{0:0.00}", ttam1) + " <small>USD</small></div>" +
                                                    "<p>inc. all taxes and fees</p>" +
                                                "</div>";
                divBookingId.InnerHtml = "" + StrBookingId + "";
                Session["divBookingId" + ssid] = StrBookingId;
                Session["StrBookingIdConfirm" + ssid] = StrBookingIdConfirm;
                ////divBookingId -end

                pricesumfoot.InnerHtml = "<div class='pm-content'>" +
                       "<div class='pm-left pm-footer-price'>" +
                       "<p class='pm-footer-tp'>Total price</p>" +
                       "<h4 id='totalAmount4'>" +
                       "<span>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion+ insurance_total) + " </span>" +
                       "<small>USD</small></h4>" +
                       "<p>* inc. all taxes and fees</p>" +
                       "</div>" +
                       "<div class='pm-right text-right pm-paynowbtn'>" +
                       "<span>" +
                       "<input type='submit' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
                       "</div>" +
                       "</div>";

                divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
                                               "<div class='pm-content'>" +
                                                   "<div class='pm-payDetails'>" +
                                                   "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
                                                       "Price Details (USD)" +
                                                       "<p>Confirm the price details of your round trip itinerary for all passengers.</p>" +
                                                   "</div>" +
                                               "</div>" +
                                           "</div>" +
                                           "<div class='pm-content'>" +

                                               "<div class='fareDetails'>" +
                                                   "<div class='fareDetailsBody'>" +
                                                       "<div class='fareDetailsLeft'>" +
                                                           "<div class='ccodeContent'>" +
                                                               "<label for='Promo Code'>Promo Code:</label>" +
                                                               "<span id='Voucher_Ihint1' class='red'></span>" +
                                                               "<div id='promo-code-input'>" +
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
                                                                    "<div class='fareQuoteLeft'>Total Amount</div> " +
                                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></div>" +
                                                                "</div>";
                //                                                "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Adults x " + TotalAdults + "</div>" +
                //                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>";
                //if (TotalChilds != 0)
                //{
                //    divPriceDetails.InnerHtml += "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Child x " + TotalChilds + "</div>" +
                //                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>";
                //}
                //if (TotalInfants != 0)
                //{
                //    divPriceDetails.InnerHtml += "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Infant x " + TotalInfants + "</div>" +
                //                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>";
                //}

                divPriceDetails.InnerHtml += "<div class='fareQuoted green'>" +
                                                                    "<div class='fareQuoteLeft'>Lower Price Finder and </br>24 Hours Cancellation Fee</div>";
                if (Session["ExtendedCancellation" + ssid] != null)
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
                else
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='CancellationFee'> 0.00 <small>USD</small></div>";
                divPriceDetails.InnerHtml += "</div>";

                

                    // csa 
                    if (Insu == "yes")
                   {
                    divPriceDetails.InnerHtml += "<div class='fareQuoted'>" +
                    "<div class='fareQuoteLeft green'>CSA Travel Protection </div>";


               // if (Session["Insurance" + ssid] != null)
                if (Session["Insurance" + ssid] != null)
                {
                        divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='InsuranceFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["Insurance" + ssid].ToString())) + "<small>USD</small></div>";
                    }
                    else
                    {
                        divPriceDetails.InnerHtml += "<div class='fareQuoteRight green' id='InsuranceFee'> 0.00 <small>USD</small></div>";

                    }


                    divPriceDetails.InnerHtml += "</div>";
                }





                divPriceDetails.InnerHtml += "<div class='fareQuoted offerCode'>" +
                                                                    "<div class='fareQuoteLeft '>Promocode</div>";
                if (Session["VoucherAmount" + ssid] != null)
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
                else
                    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";
                divPriceDetails.InnerHtml += "</div>" +
                                                                "<div class='fareQuoted offerCode'>" +
                                                                    "<div class='fareQuoteLeft '>Discount</div>" +
                                                                    "<div class='fareQuoteRight' id='Discount'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                        "</div>" +
                                                        "<div class='fareDetailsFooter'>" +
                                                            "<div class='tlprice'>" +
                                                                "<div class='tlpriceLeft'>Total</div>" +
                                                                "<div class='tlpriceRight' id='totalAmount'>" +
                                                                    "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation + Promotion + insurance_total)) + " USD</h4>" +
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
                string PaymentHeader = "<div class='pm-right pm-tl-fare'>" +
                                               "<p class='pm-tl-text'>Total price</p>" +
                                               "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount + cancellation + Promotion+ insurance_total ) + " <small>USD</small></h4>" +
                                               "<p>* inc. all taxes and fees</p>" +
                                           "</div>";
                paymentHeader.InnerHtml = PaymentHeader;
                divPriceDetails1.InnerHtml = "<div class='faresummery-title'>" +
                                               "<h4>Price Details (USD)</h4>" +
                                           "</div>" +
                                           "<div class='faresummery-body'>" +
                                               "<div class='faresummery-seg-wrap'>" +
                                                   "<div class=''>" +
                                               "<div class='price-details'>" +
                                                    "<span class='pull-left'>Total Amount</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></span>" +
                                                "</div>";
                //                                       "<span class='pull-left'>Adults x " + TotalAdults + "</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(adultfare)) + " <small>USD</small></span>" +
                //                                   "</div>";
                //if (TotalChilds != 0)
                //{
                //    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                //                                            "<span class='pull-left'>Child x " + TotalChilds + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(childfare)) + " <small>USD</small></span>" +
                //                                        "</div>";
                //}

                //if (TotalInfants != 0)
                //{
                //    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                //                                            "<span class='pull-left'>Infant x " + TotalInfants + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(infantfare)) + " <small>USD</small></span>" +
                //                                        "</div>";
                //}
// small amount div 
                divPriceDetails1.InnerHtml += "<div class='price-details green'>" +
                                                        "<span class='pull-left'>Lower Price Finder and </br>24 Hours Cancellation Fee</span>";
                if (Session["ExtendedCancellation" + ssid] != null)
                    divPriceDetails1.InnerHtml += " <span class='pull-right green' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
                else
                    divPriceDetails1.InnerHtml += " <span class='pull-right green' id='CancellationFee1'> 0.00 <small>USD</small></span>";

                if (Insu == "yes")
                {

                    divPriceDetails1.InnerHtml += "<div class='price-details '>" +
                                                            "<span class='pull-left green'>CSA Travel Protection</span>";
                    if (Session["Insurance" + ssid ] != null)
                        divPriceDetails1.InnerHtml += " <span class='pull-right green' id='InsuranceFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["Insurance" + ssid].ToString())) + " <small>USD</small></span>";
                    else
                        divPriceDetails1.InnerHtml += " <span class='pull-right green' id='InsuranceFee1'> 0.00 <small>USD</small></span>";

                }


                divPriceDetails1.InnerHtml += "</div>" +
                                                    "<div class='price-details'>" +
                                                        "<span class='pull-left'>Discount</span> <span class='pull-right dicPrice'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></span>" +
                                                    "</div>" +
                                                    "<div class='price-details'>" +
                                                        "<span class='pull-left'>Promocode</span> ";
                if (Session["VoucherAmount" + ssid] != null)
                    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
                else
                    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";

                divPriceDetails1.InnerHtml += "</div>" +
                                                    "<div class='price-details'>" +
                                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' id='totalAmount1'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation + Promotion + insurance_total)) + " <small>USD</small></span>" +
                                                    "</div>" +
                                                "</div>" +
                                                "<div class='faresummery-seg-wrap promoBg'>" +
                                                    "<label for='Promo Code'>Promo Code:</label>" +
                                                    "<span id='Voucher_Ihint' class='red'></span>" +
                                                    "<div id='promo-code-input'>" +
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
                                                            "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation + Promotion + insurance_total )) + " <small>USD</small></h3>" +
                                                            "<p>incl. all taxes and fees</p>" +
                                                        "</div>" +
                                                        "<button type='submit' id='' class='pm-payment-btn btn btn-block' onclick='return btn_Submit(this);'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                                                    "<span class='tcktleft' id='visitors'></button>" +
                                                    "</div>" +
                                                "</div>" +
                                                "</div>" +
                                            "</div>";
                //divBookingError.Attributes.Add("style", "Display:none");
            }
            else
            {
                //divBookingError.Attributes.Add("style", "Display:none");
                //divPassengerInfo.Attributes.Add("style", "Display:none");
                //divBookingDesc.Attributes.Add("style", "Display:none");
                //divBaggageInfo.Attributes.Add("style", "Display:none");
                //divItineraryInfo.Attributes.Add("style", "Display:none");
                //Psdetailscheck.Attributes.Add("style", "Display:none");
                //Psdetailscheck.Attributes.Add("style", "Display:none");
                //Cancelpolicycheck.Attributes.Add("style", "Display:none");
                //divPriceDetails.Attributes.Add("style", "Display:none");
                //divPaymentDetails1.Attributes.Add("style", "Display:none");
                //divPriceDetails1.Attributes.Add("style", "Display:none");
                //divPaymentDetails2.Attributes.Add("style", "Display:none");
                string StrBookingId = "";
                string StrBookingIdConfirm = "";
                TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
                displayItinerary_1P_Database(getBookingID(dtPay.Rows[0]["pnrno"].ToString()).ToString(), dtPay, cancellation, Promotion ,insurance_total);
                //if (Session["VoucherAmount" + ssid] != null)
                //{
                //    Promo = "yes";
                //    PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
                //                                                                                                      //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
                //}
                //if (Session["ExtendedCancellation" + ssid] != null)
                //{
                //    cancel = "yes";
                //    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
                //}
                StrBookingId += "<div class='pm-content'>" +
                                                   "<div class='pm-left'>" +
                                                       "<p class='bkid-icon'>" +
                                                           "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                           " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                                       "</p>" +
                                                   "</div>" +
                                                   "<div class='pm-right pm-tl-fare'>" +
                                                       "<p class='pm-tl-text'>Total price</p>" +
                                                           "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion+ insurance_total) + " <small>USD</small></h4>" +
                                                           "<p>* inc. all taxes and fees</p>" +
                                                   "</div>" +
                                               "</div>";

                StrBookingIdConfirm += "<div class='confirm-head-Left'>" +
                                                    "<h5>Check Your itinerary <small>(* All the times are local to Airports)</small></h5>" +
                                                "</div>" +
                                                "<div class='pull-right text-right'>" +
                                                    "<p>Total price</p>" +
                                                    "<div class='tplfare bold'>" + String.Format("{0:0.00}", ttam1) + " <small>USD</small></div>" +
                                                    "<p>inc. all taxes and fees</p>" +
                                                "</div>";
                divBookingId.InnerHtml = "" + StrBookingId + "";
                Session["divBookingId" + ssid] = StrBookingId;
                Session["StrBookingIdConfirm" + ssid] = StrBookingIdConfirm;
                ////divBookingId -end

                //pricesumfoot.InnerHtml = "<div class='pm-content'>" +
                //       "<div class='pm-left pm-footer-price'>" +
                //       "<p class='pm-footer-tp'>Total price</p>" +
                //       "<h4 id='totalAmount4'>" +
                //       "<span>" + String.Format("{0:0.00}", TtAmount + cancellation) + " </span>" +
                //       "<small>USD</small></h4>" +
                //       "<p>* inc. all taxes and fees</p>" +
                //       "</div>" +
                //       "<div class='pm-right text-right pm-paynowbtn'>" +
                //       "<span>" +
                //       "<input type='button' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
                //       "<p class='mt-10'>Make payment immediately and avoid price increases. </p>" +
                //       "</div>" +
                //       "</div>";
                ////PNRStatus = false;

                //divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
                //                                "<div class='pm-content'>" +
                //                                    "<div class='pm-payDetails'>" +
                //                                    "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
                //                                        "Price Details (USD)" +
                //                                        "<p>Confirm the price details of your round trip itinerary for all passengers.</p>" +
                //                                    "</div>" +
                //                                "</div>" +
                //                            "</div>" +
                //                            "<div class='pm-content'>" +

                //                                "<div class='fareDetails'>" +
                //                                    "<div class='fareDetailsBody'>" +
                //                                        "<div class='fareDetailsLeft'>" +
                //                                            "<div class='ccodeContent'>" +
                //                                                "<label for='Promo Code'>Promo Code:</label>" +
                //                                                "<span id='Voucher_Ihint1' class='red'></span>" +
                //                                                "<div id='promo-code-input'>" +
                //                                                    "<div class='input-group col-md-12'>";
                //if (Session["VoucherCode" + ssid] != null)
                //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id1' id='Voucher_Id1'>";
                //else
                //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id1' id='Voucher_Id1'>";
                //divPriceDetails.InnerHtml += "<span class='input-group-btn'>" +
                //                                                                "<button class='btn btn-lg' type='button' id='btn_Voucher' onclick='ValidateVoucher()'>Apply</button>" +
                //                                                            "</span>" +
                //                                                        "</div>" +
                //                                                    "</div>" +
                //                                                "</div>" +
                //                                            "</div>" +
                //                                            "<div class='fareDetailsRight'>" +
                //                                                "<div class='fareQuoted'>" +
                //                                                    "<div class='fareQuoteLeft'>Total Amount</div> " +
                //                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></div>" +
                //                                                "</div>";
                ////                                                "<div class='fareQuoted'>" +
                ////                                                    "<div class='fareQuoteLeft'>Adults x " + TotalAdults + "</div>" +
                ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                ////                                                "</div>";
                ////if (TotalChilds != 0)
                ////{
                ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
                ////                                                    "<div class='fareQuoteLeft'>Child x " + TotalChilds + "</div>" +
                ////                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                ////                                                "</div>";
                ////}
                ////if (TotalInfants != 0)
                ////{
                ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
                ////                                                    "<div class='fareQuoteLeft'>Infant x " + TotalInfants + "</div>" +
                ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                ////                                                "</div>";
                ////}

                //divPriceDetails.InnerHtml += "<div class='fareQuoted green'>" +
                //                                                    "<div class='fareQuoteLeft'>24 Hours Cancellation Fee</div>";
                //if (Session["ExtendedCancellation" + ssid] != null)
                //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
                //else
                //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> 0.00 <small>USD</small></div>";
                //divPriceDetails.InnerHtml += "</div>" +
                //                                                "<div class='fareQuoted offerCode'>" +
                //                                                    "<div class='fareQuoteLeft '>Promocode</div>";
                //if (Session["VoucherAmount" + ssid] != null)
                //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
                //else
                //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";
                //divPriceDetails.InnerHtml += "</div>" +
                //                                                "<div class='fareQuoted offerCode'>" +
                //                                                    "<div class='fareQuoteLeft '>Discount</div>" +
                //                                                    "<div class='fareQuoteRight' id='Discount'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
                //                                                "</div>" +
                //                                            "</div>" +
                //                                        "</div>" +
                //                                        "<div class='fareDetailsFooter'>" +
                //                                            "<div class='tlprice'>" +
                //                                                "<div class='tlpriceLeft'>Total</div>" +
                //                                                "<div class='tlpriceRight' id='totalAmount'>" +
                //                                                    "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " USD</h4>" +
                //                                                    "<p>incl. all taxes and fees</p>" +
                //                                                "</div>" +
                //                                            "</div>" +
                //                                        "</div>" +
                //                                        "<div class='fareNote'>" +
                //                                            "<p><strong>Please Note:</strong> All fares are quoted in USD. Some airlines may charge " +
                //                                            "<a  href='/ViewBaggageInfo.aspx' target='_blank'>baggage fees</a>. Your credit/debit card may be billed in multiple charges totaling the final total price. </p>" +
                //                                        "</div>" +
                //                                    "</div>" +
                //                                "</div>";
                string PaymentHeader = "<div class='pm-right pm-tl-fare'>" +
                                               "<p class='pm-tl-text'>Total price</p>" +
                                               "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount + cancellation + Promotion+ insurance_total) + " <small>USD</small></h4>" +
                                               "<p>* inc. all taxes and fees</p>" +
                                           "</div>";
                paymentHeader.InnerHtml = PaymentHeader;
                //divPriceDetails1.InnerHtml = "<div class='faresummery-title'>" +
                //                               "<h4>Price Details (USD)</h4>" +
                //                           "</div>" +
                //                           "<div class='faresummery-body'>" +
                //                               "<div class='faresummery-seg-wrap'>" +
                //                                   "<div class=''>" +
                //                               "<div class='price-details'>" +
                //                                    "<span class='pull-left'>Total Amount</span> <span class='pull-right totalprice' >" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></span>" +
                //                                "</div>";
                ////                                       "<span class='pull-left'>Adults x " + TotalAdults + "</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(adultfare)) + " <small>USD</small></span>" +
                ////                                   "</div>";
                ////if (TotalChilds != 0)
                ////{
                ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                ////                                            "<span class='pull-left'>Child x " + TotalChilds + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(childfare)) + " <small>USD</small></span>" +
                ////                                        "</div>";
                ////}

                ////if (TotalInfants != 0)
                ////{
                ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
                ////                                            "<span class='pull-left'>Infant x " + TotalInfants + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(infantfare)) + " <small>USD</small></span>" +
                ////                                        "</div>";
                ////}

                //divPriceDetails1.InnerHtml += "<div class='price-details green'>" +
                //                                        "<span class='pull-left'>24 Hours Cancellation Fee</span>";
                //if (Session["ExtendedCancellation" + ssid] != null)
                //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
                //else
                //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> 0.00 <small>USD</small></span>";

                //divPriceDetails1.InnerHtml += "</div>" +
                //                                    "<div class='price-details'>" +
                //                                        "<span class='pull-left'>Discount</span> <span class='pull-right dicPrice'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></span>" +
                //                                    "</div>" +
                //                                    "<div class='price-details'>" +
                //                                        "<span class='pull-left'>Promocode</span> ";
                //if (Session["VoucherAmount" + ssid] != null)
                //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
                //else
                //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";

                //divPriceDetails1.InnerHtml += "</div>" +
                //                                    "<div class='price-details'>" +
                //                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' id='totalAmount1'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></span>" +
                //                                    "</div>" +
                //                                "</div>" +
                //                                "<div class='faresummery-seg-wrap promoBg'>" +
                //                                    "<label for='Promo Code'>Promo Code:</label>" +
                //                                    "<span id='Voucher_Ihint' class='red'></span>" +
                //                                    "<div id='promo-code-input'>" +
                //                                        "<div class='input-group col-md-12'>";
                //if (Session["VoucherCode" + ssid] != null)
                //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id' id='Voucher_Id'>";
                //else
                //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id' id='Voucher_Id'>";

                //divPriceDetails1.InnerHtml += "<span class='input-group-btn'>" +
                //                                                "<button class='btn btn-lg' type='button' id='btn_Voucher1' onclick='ValidateVoucher1()'>Apply</button>" +
                //                                            "</span>" +
                //                                        "</div>" +
                //                                    "</div>" +
                //                                    "<div class='faresummery-seg-wrap'>" +
                //                                        "<div class='totalAmount' id='totalAmount2'>" +
                //                                            "<p>Total Price:</p>" +
                //                                            "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></h3>" +
                //                                            "<p>incl. all taxes and fees</p>" +
                //                                        "</div>" +
                //                                        "<button type='button' id='' class='pm-payment-btn btn btn-block' onclick='return btn_Submit(this);'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
                //                                    "<span class='tcktleft' id='visitors'></button>" +
                //                                    "</div>" +
                //                                "</div>" +
                //                                "</div>" +
                //                            "</div>";
                string error = "<div class='pm-content'>" +
                   "<div class='pm-error warning' style=''>" +
                   "<ul>" +
                   "<li class=''>" +
                                           "Please contact our support team to help you to complete the Payment for this booking <span class='bold' ><a href='tel:" + ContactDetails.Phone_support + "' ><i class='fa fa-phone'></i>" + ContactDetails.Phone_support + "</a></span>" +
                   "</li>" +
                   "</ul>" +
                   "</div>" +
                   "</div>";
                if (paymentdone)
                {
                    divBookingError.InnerHtml = "";
                    divBookingError.Attributes.Add("style", "Display:none");
                    insurance_csa.Attributes.Add("style", "Display:none");

                    divpayfailederror.InnerHtml = error;
                    divpayfailederror.Attributes.Add("style", "Display:block");
                }
                else
                {
                    //divpayfailederror.InnerHtml = error;
                    //divpayfailederror.Attributes.Add("style", "Display:block");
                }
                RedirectCS();


                pricesumfoot.InnerHtml = "<div class='pm-content'>" +
                     "<div class='pm-left pm-footer-price'>" +
                     "<p class='pm-footer-tp'>Total price</p>" +
                     "<h4 id='totalAmount4'>" +
                     "<span>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " </span>" +
                     "<small>USD</small></h4>" +
                     "<p>* inc. all taxes and fees</p>" +
                     "</div>" +
                     "<div class='pm-right text-right pm-paynowbtn'>" +
                     "<span>" +
                     "<input type='submit' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
                     "</div>" +
                     "</div>";


            }

        }
        else
        {
            SqlParameter[] paramExtended = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "26") };
            DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);
            ttam1 = TtAmount;
            if (dtExtend.Rows.Count > 0)
            {
                if (Session["Extended"] != null)
                {
                    cancellation = (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));

                    if (Session["Extended"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    }
                    else if (Session["Extended"].ToString() == "yes")
                    {
                        ttam1 = ttam1 + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                    }
                }
                else
                    Session["Extended"] = "no";
            }
            else
            {
                if (Session["Extended"] != null)
                {
                    if (Session["Extended"].ToString() == "yes")
                    {
                        ttam1 = ttam1 + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                        TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                        cancellation = (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                    }
                    else if (Session["Extended"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    }
                }
                else
                    Session["Extended"] = "no";
            }





            SqlParameter[] paramExtended2 = { new SqlParameter("@booking_id", getBookingID(dtPay.Rows[0]["pnrno"].ToString())), new SqlParameter("@Product_Type_ID", "28") };
            DataTable dtExtend2 = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended2);
            ttam1 = TtAmount;
            if (dtExtend2.Rows.Count > 0)
            {
                if (Session["Ins"] != null)
                {


                    if (Session["Ins"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                        //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend2.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend2.Rows[0]["Units"].ToString()));
                    }
                    else if (Session["Ins"].ToString() == "yes")
                    {
                        insurance_total = (Convert.ToDouble(dtExtend2.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend2.Rows[0]["Units"].ToString()));
                        ttam1 = ttam1 + (Convert.ToDouble(Session["Insurance"].ToString()));
                    }
                }
                else
                    Session["Ins"] = "no";
            }
            else
            {
                if (Session["Ins"] != null)
                {
                    if (Session["Ins"].ToString() == "yes")
                    {
                        ttam1 = ttam1 + (Convert.ToDouble(Session["Insurance"].ToString()));
                        //TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["Insurance"].ToString()));
                        insurance_total = (Convert.ToDouble(Session["Insurance"].ToString()));
                    }
                    else if (Session["Ins"].ToString() == "no")
                    {
                        //ttam1 = ttam1 - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    }
                }
                else
                    Session["Ins"] = "no";
            }




            if (Session["VoucherAmount" + ssid] != null)
            {
                Promo = "yes";
                PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
                                                                                                                  //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
            }


            if (Session["Extended"] != null)
            {
                if (Session["Extended"].ToString() == "yes")
                    cancel = "yes";
                else
                    cancel = "no";
                //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
            }


            if (Session["Ins"] != null)
            {
                if (Session["Ins"].ToString() == "yes")
                    insurance = "yes";
                else
                    insurance = "no";
                //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
            }



            //divBookingError.Attributes.Add("style", "Display:none");
            divPassengerInfo.Attributes.Add("style", "Display:none");
            divBookingDesc.Attributes.Add("style", "Display:none");
            divBaggageInfo.Attributes.Add("style", "Display:none");
            divItineraryInfo.Attributes.Add("style", "Display:none");
            Psdetailscheck.Attributes.Add("style", "Display:none");
            Psdetailscheck.Attributes.Add("style", "Display:none");
            Cancelpolicycheck.Attributes.Add("style", "Display:none");
            insurance_csa.Attributes.Add("style", "Display:none");
            divPriceDetails.Attributes.Add("style", "Display:none");
            divPaymentDetails1.Attributes.Add("style", "Display:none");
            divPriceDetails1.Attributes.Add("style", "Display:none");
            divPaymentDetails2.Attributes.Add("style", "Display:none");
            string StrBookingId = "";
            string StrBookingIdConfirm = "";
            TtAmountCalc = Convert.ToDouble(dtPay.Rows[0]["Amount"].ToString());
            displayItinerary_1P_Database(getBookingID(dtPay.Rows[0]["pnrno"].ToString()).ToString(), dtPay, cancellation, Promotion,insurance_total);
            //if (Session["VoucherAmount" + ssid] != null)
            //{
            //    Promo = "yes";
            //    PromoAm = String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString()));// Convert.ToDouble(Session["VoucherAmount" + ssid].ToString());
            //    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) - Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())).ToString());
            //}
            //if (Session["ExtendedCancellation" + ssid] != null)
            //{
            //    cancel = "yes";
            //    //TtAmount = Convert.ToDouble((Convert.ToDouble(TtAmount) + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())).ToString());
            //}
            StrBookingId += "<div class='pm-content'>" +
                                               "<div class='pm-left'>" +
                                                   "<p class='bkid-icon'>" +
                                                       "<img src='Design/images/bookid-icon.png' alt='Booking id icon'>" +
                                                       " Booking Reference: <span class='blue'>" + getBookingID(dtPay.Rows[0]["pnrno"].ToString()) + "</span>" +
                                                   "</p>" +
                                               "</div>" +
                                               "<div class='pm-right pm-tl-fare'>" +
                                                   "<p class='pm-tl-text'>Total price</p>" +
                                                       "<h4 id='totalAmount3'>" + String.Format("{0:0.00}", TtAmount + cancellation + Promotion + insurance_total) + " <small>USD</small></h4>" +
                                                       "<p>* inc. all taxes and fees</p>" +
                                               "</div>" +
                                           "</div>";

            StrBookingIdConfirm += "<div class='confirm-head-Left'>" +
                                                "<h5>Check Your itinerary <small>(* All the times are local to Airports)</small></h5>" +
                                            "</div>" +
                                            "<div class='pull-right text-right'>" +
                                                "<p>Total price</p>" +
                                                "<div class='tplfare bold'>" + String.Format("{0:0.00}", ttam1) + " <small>USD</small></div>" +
                                                "<p>inc. all taxes and fees</p>" +
                                            "</div>";
            divBookingId.InnerHtml = "" + StrBookingId + "";
            Session["divBookingId" + ssid] = StrBookingId;
            Session["StrBookingIdConfirm" + ssid] = StrBookingIdConfirm;
            ////divBookingId -end

            //pricesumfoot.InnerHtml = "<div class='pm-content'>" +
            //       "<div class='pm-left pm-footer-price'>" +
            //       "<p class='pm-footer-tp'>Total price</p>" +
            //       "<h4 id='totalAmount4'>" +
            //       "<span>" + String.Format("{0:0.00}", TtAmount + cancellation) + " </span>" +
            //       "<small>USD</small></h4>" +
            //       "<p>* inc. all taxes and fees</p>" +
            //       "</div>" +
            //       "<div class='pm-right text-right pm-paynowbtn'>" +
            //       "<span>" +
            //       "<input type='button' name='Button1' value='Pay Now' onclick='return btn_Submit(this);' id='Button1' class='pm-payment-btn btn'></span>" +
            //       "<p class='mt-10'>Make payment immediately and avoid price increases. </p>" +
            //       "</div>" +
            //       "</div>";
            ////PNRStatus = false;

            //divPriceDetails.InnerHtml = "<div class='pm-head-title'>" +
            //                                "<div class='pm-content'>" +
            //                                    "<div class='pm-payDetails'>" +
            //                                    "<img src='Design/images/pricedetails-icon.png' alt='Passenger icon' class='title-icons'>" +
            //                                        "Price Details (USD)" +
            //                                        "<p>Confirm the price details of your round trip itinerary for all passengers.</p>" +
            //                                    "</div>" +
            //                                "</div>" +
            //                            "</div>" +
            //                            "<div class='pm-content'>" +

            //                                "<div class='fareDetails'>" +
            //                                    "<div class='fareDetailsBody'>" +
            //                                        "<div class='fareDetailsLeft'>" +
            //                                            "<div class='ccodeContent'>" +
            //                                                "<label for='Promo Code'>Promo Code:</label>" +
            //                                                "<span id='Voucher_Ihint1' class='red'></span>" +
            //                                                "<div id='promo-code-input'>" +
            //                                                    "<div class='input-group col-md-12'>";
            //if (Session["VoucherCode" + ssid] != null)
            //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id1' id='Voucher_Id1'>";
            //else
            //    divPriceDetails.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id1' id='Voucher_Id1'>";
            //divPriceDetails.InnerHtml += "<span class='input-group-btn'>" +
            //                                                                "<button class='btn btn-lg' type='button' id='btn_Voucher' onclick='ValidateVoucher()'>Apply</button>" +
            //                                                            "</span>" +
            //                                                        "</div>" +
            //                                                    "</div>" +
            //                                                "</div>" +
            //                                            "</div>" +
            //                                            "<div class='fareDetailsRight'>" +
            //                                                    "<div class='fareQuoted'>" +
            //                                                        "<div class='fareQuoteLeft'>Total Amount</div> " +
            //                                                        "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></div>" +
            //                                                    "</div>";
            ////                                                "<div class='fareQuoted'>" +
            ////                                                    "<div class='fareQuoteLeft'>Adults x " + TotalAdults + "</div>" +
            ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
            ////                                                "</div>";
            ////if (TotalChilds != 0)
            ////{
            ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
            ////                                                    "<div class='fareQuoteLeft'>Child x " + TotalChilds + "</div>" +
            ////                                                    "<div class='fareQuoteRight'>" + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
            ////                                                "</div>";
            ////}
            ////if (TotalInfants != 0)
            ////{
            ////    divPriceDetails.InnerHtml +=                "<div class='fareQuoted'>" +
            ////                                                    "<div class='fareQuoteLeft'>Infant x " + TotalInfants + "</div>" +
            ////                                                    "<div class='fareQuoteRight'> " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
            ////                                                "</div>";
            ////}

            //divPriceDetails.InnerHtml += "<div class='fareQuoted green'>" +
            //                                                    "<div class='fareQuoteLeft'>24 Hours Cancellation Fee</div>";
            //if (Session["ExtendedCancellation" + ssid] != null)
            //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></div>";
            //else
            //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='CancellationFee'> 0.00 <small>USD</small></div>";
            //divPriceDetails.InnerHtml += "</div>" +
            //                                                "<div class='fareQuoted offerCode'>" +
            //                                                    "<div class='fareQuoteLeft '>Promocode</div>";
            //if (Session["VoucherAmount" + ssid] != null)
            //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></div>";
            //else
            //    divPriceDetails.InnerHtml += "<div class='fareQuoteRight' id='promocode'>- 0.00 <small>USD</small></div>";
            //divPriceDetails.InnerHtml += "</div>" +
            //                                                "<div class='fareQuoted offerCode'>" +
            //                                                    "<div class='fareQuoteLeft '>Discount</div>" +
            //                                                    "<div class='fareQuoteRight' id='Discount'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></div>" +
            //                                                "</div>" +
            //                                            "</div>" +
            //                                        "</div>" +
            //                                        "<div class='fareDetailsFooter'>" +
            //                                            "<div class='tlprice'>" +
            //                                                "<div class='tlpriceLeft'>Total</div>" +
            //                                                "<div class='tlpriceRight' id='totalAmount'>" +
            //                                                    "<h4> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " USD</h4>" +
            //                                                    "<p>incl. all taxes and fees</p>" +
            //                                                "</div>" +
            //                                            "</div>" +
            //                                        "</div>" +
            //                                        "<div class='fareNote'>" +
            //                                            "<p><strong>Please Note:</strong> All fares are quoted in USD. Some airlines may charge " +
            //                                            "<a  href='/ViewBaggageInfo.aspx' target='_blank'>baggage fees</a>. Your credit/debit card may be billed in multiple charges totaling the final total price. </p>" +
            //                                        "</div>" +
            //                                    "</div>" +
            //                                "</div>";
            string PaymentHeader = "<div class='pm-right pm-tl-fare'>" +
                                           "<p class='pm-tl-text'>Total price</p>" +
                                           "<h4 id='totalAmount5'> " + String.Format("{0:0.00}", TtAmount + cancellation + Promotion+ insurance_total) + " <small>USD</small></h4>" +
                                           "<p>* inc. all taxes and fees</p>" +
                                       "</div>";
            paymentHeader.InnerHtml = PaymentHeader;
            //divPriceDetails1.InnerHtml = "<div class='faresummery-title'>" +
            //                               "<h4>Price Details (USD)</h4>" +
            //                           "</div>" +
            //                           "<div class='faresummery-body'>" +
            //                               "<div class='faresummery-seg-wrap'>" +
            //                                   "<div class=''>" +
            //                                   "<div class='price-details'>" +
            //                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' >" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount)) + " <small>USD</small></span>" +
            //                                    "</div>";
            ////                                       "<span class='pull-left'>Adults x " + TotalAdults + "</span> <span class='pull-right'>" + String.Format("{0:0.00}", Convert.ToDouble(adultfare)) + " <small>USD</small></span>" +
            ////                                   "</div>";
            ////if (TotalChilds != 0)
            ////{
            ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
            ////                                            "<span class='pull-left'>Child x " + TotalChilds + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(childfare)) + " <small>USD</small></span>" +
            ////                                        "</div>";
            ////}

            ////if (TotalInfants != 0)
            ////{
            ////    divPriceDetails1.InnerHtml += "<div class='price-details'>" +
            ////                                            "<span class='pull-left'>Infant x " + TotalInfants + "</span> <span class='pull-right'> " + String.Format("{0:0.00}", Convert.ToDouble(infantfare)) + " <small>USD</small></span>" +
            ////                                        "</div>";
            ////}

            //divPriceDetails1.InnerHtml += "<div class='price-details green'>" +
            //                                        "<span class='pull-left'>24 Hours Cancellation Fee</span>";
            //if (Session["ExtendedCancellation" + ssid] != null)
            //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> " + String.Format("{0:0.00}", Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString())) + " <small>USD</small></span>";
            //else
            //    divPriceDetails1.InnerHtml += " <span class='pull-right ' id='CancellationFee1'> 0.00 <small>USD</small></span>";

            //divPriceDetails1.InnerHtml += "</div>" +
            //                                    "<div class='price-details'>" +
            //                                        "<span class='pull-left'>Discount</span> <span class='pull-right dicPrice'>- " + String.Format("{0:0.00}", Convert.ToDouble(0)) + " <small>USD</small></span>" +
            //                                    "</div>" +
            //                                    "<div class='price-details'>" +
            //                                        "<span class='pull-left'>Promocode</span> ";
            //if (Session["VoucherAmount" + ssid] != null)
            //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- " + String.Format("{0:0.00}", Convert.ToDouble(Session["VoucherAmount" + ssid].ToString())) + " <small>USD</small></span>";
            //else
            //    divPriceDetails1.InnerHtml += "<span class='pull-right dicPrice' id='promocode1'>- 0.00 <small>USD</small></span>";

            //divPriceDetails1.InnerHtml += "</div>" +
            //                                    "<div class='price-details'>" +
            //                                        "<span class='pull-left'>Total Amount</span> <span class='pull-right' id='totalAmount1'>" + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></span>" +
            //                                    "</div>" +
            //                                "</div>" +
            //                                "<div class='faresummery-seg-wrap promoBg'>" +
            //                                    "<label for='Promo Code'>Promo Code:</label>" +
            //                                    "<span id='Voucher_Ihint' class='red'></span>" +
            //                                    "<div id='promo-code-input'>" +
            //                                        "<div class='input-group col-md-12'>";
            //if (Session["VoucherCode" + ssid] != null)
            //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' value='" + Session["VoucherCode" + ssid].ToString() + "' name='Voucher_Id' id='Voucher_Id'>";
            //else
            //    divPriceDetails1.InnerHtml += "<input class='form-control input-lg' placeholder='Enter promo code' type='text' name='Voucher_Id' id='Voucher_Id'>";

            //divPriceDetails1.InnerHtml += "<span class='input-group-btn'>" +
            //                                                "<button class='btn btn-lg' type='button' id='btn_Voucher1' onclick='ValidateVoucher1()'>Apply</button>" +
            //                                            "</span>" +
            //                                        "</div>" +
            //                                    "</div>" +
            //                                    "<div class='faresummery-seg-wrap'>" +
            //                                        "<div class='totalAmount' id='totalAmount2'>" +
            //                                            "<p>Total Price:</p>" +
            //                                            "<h3> " + String.Format("{0:0.00}", Convert.ToDouble(TtAmount + cancellation)) + " <small>USD</small></h3>" +
            //                                            "<p>incl. all taxes and fees</p>" +
            //                                        "</div>" +
            //                                        "<button type='button' id='' class='pm-payment-btn btn btn-block' onclick='return btn_Submit(this);'>Pay Now <i class='fa fa-plane planeIcon'></i></button>" +
            //                                    "<span class='tcktleft' id='visitors'></button>" +
            //                                    "</div>" +
            //                                "</div>" +
            //                                "</div>" +
            //                            "</div>";

            string error = "<div class='pm-content'>" +
                   "<div class='pm-error warning' style=''>" +
                   "<ul>" +
                   "<li class=''>" +
                                           "Please contact our support team to help you to complete the Payment for this booking <span class='bold' ><a href='tel:" + ContactDetails.Phone_support + "' ><i class='fa fa-phone'></i>" + ContactDetails.Phone_support + "</a></span>" +
                   "</li>" +
                   "</ul>" +
                   "</div>" +
                   "</div>";
            if (paymentdone)
            {
                divBookingError.InnerHtml = "";
                divBookingError.Attributes.Add("style", "Display:none");
                insurance_csa.Attributes.Add("style", "Display:none");

                divpayfailederror.InnerHtml = error;
                divpayfailederror.Attributes.Add("style", "Display:block");
            }
            else
            {
                divpayfailederror.InnerHtml = error;
                divpayfailederror.Attributes.Add("style", "Display:block");
            }
            //$("#divpayfailederror").append(error);
            //$('#divpayfailederror').css('display', 'block');
            RedirectCS();
        }
    }

    protected void btnBookNow()
    {


        bool flag = true;
        //string validation = "";
        //lblError.Text = "";                
        //if (validation != "")
        //{
        //    flag = false;
        //    trErrMsg.Attributes.Add("style", "Display:''");
        //    lblError.Text += validation.TrimEnd('-');
        //}
        lblError.Text = "";
        trErrMsg.Style.Add("display", "none");





        if (flag)
        {


            //var -start
            string strVendorTxCode = "";
            //var -end


            // VendoeTxCode
            //Randomize();
            string strPNRValue = "";
            if (tmPNR.Value != "")
            {
                strPNRValue = tmPNR.Value;
            }
            else
            {
                strPNRValue = tmHPNR.Value;
            }
            strPNRValue =
            strVendorTxCode = strPNRValue + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
            DateTime dtCardReUse = DateTime.Now.AddDays(-60);
            try
            {
                string qr = "select Pay_Date_Time,CreditCard_Number from Receipt_Confirm where CreditCard_Number='" + Request.Form["txtCardNumber"].ToString() + "' and Pay_Date_Time>='" + dtCardReUse.ToString("MM/dd/yyyy") + "'";
                DataTable dtCards = dbAccess.GetSpecificData(qr, ConfigurationManager.AppSettings["sqlcn_db"].ToString());
                if (dtCards.Rows.Count > 0)
                {
                    strVendorTxCode = "R-" + strVendorTxCode;
                }
            }
            catch
            {

            }
            try
            {
                string ipCountryCode = getIPCountryCode(Request.UserHostAddress.ToString());
                if (ipCountryCode != "UK")
                {
                    strVendorTxCode = "O-" + strVendorTxCode;
                }
            }
            catch(Exception ex)
            {

            }
            strVendorTxCode = strVendorTxCode.Replace(".", "");
            strVendorTxCode = strVendorTxCode.Replace(" ", "");
            if (strVendorTxCode.Length > 40)
            {
                strVendorTxCode = strVendorTxCode.Substring(0, 40);
            }
            //
            //strVendorTxCode = "464657";
            long BokingID = 0;
            if (Request.QueryString["pnrno"] != null)
            {
                if (Request.QueryString["pnrno"].ToString() != "")
                {
                    BokingID = getBookingID(PNR);
                    Session["Booking_id"] = BokingID.ToString();
                }
            }

            if (BokingID == 0)
            {
                if (Session["shref"] != null)
                {
                    if (Session["shref"].ToString() != "")
                    {
                        string StrHrf = getHotel_Ref(Session["shref"].ToString());
                        if (StrHrf != "")
                        {
                            BokingID = getBookingID_Hotel(StrHrf);
                            if (BokingID > 0)
                                Session["Booking_id"] = BokingID.ToString();
                        }
                    }
                }
                //orderno = DateTime.Now.ToString("ddMMyyhhmmss");
            }
            else
            {
                orderno = BokingID.ToString();
            }

            SqlParameter[] paramExtended = { new SqlParameter("@booking_id", Session["Booking_id"].ToString()), new SqlParameter("@Product_Type_ID", "26") };
            DataTable dtExtend = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramExtended);

            if (dtExtend.Rows.Count > 0)
            {
                if (Session["Extended"].ToString() == "no")
                {
                    //TtAmount = TtAmount /*- (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()))*/;
                    //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                }
                else if (Session["Extended"].ToString() == "yes")
                {
                    TtAmount = TtAmount + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                    TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                }
            }
            else
            {
                if (Session["Extended"].ToString() == "yes")
                {
                    TtAmount = TtAmount + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                    TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["ExtendedCancellation"].ToString()));
                }
                else if (Session["Extended"].ToString() == "no")
                {
                    //TtAmount = TtAmount - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                }
            }


//csa
            SqlParameter[] paramcsa = { new SqlParameter("@booking_id", Session["Booking_id"].ToString()), new SqlParameter("@Product_Type_ID", "28") };
            DataTable dtcsa = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", paramcsa);

            if (dtcsa.Rows.Count > 0)
            {
                if (Session["Ins"].ToString() == "no")
                {
                    //TtAmount = TtAmount /*- (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()))*/;
                    //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                }
                else if (Session["Ins"].ToString() == "yes")
                {
                    TtAmount = TtAmount + (Convert.ToDouble(Session["Insurance"].ToString()));
                    TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["Insurance"].ToString()));
                }
            }
            else
            {
                if (Session["Ins"].ToString() == "yes")
                {
                    TtAmount = TtAmount + (Convert.ToDouble(Session["Insurance"].ToString()));
                    TtAmountCalc = TtAmountCalc + (Convert.ToDouble(Session["Insurance"].ToString()));
                }
                else if (Session["Ins"].ToString() == "no")
                {
                    //TtAmount = TtAmount - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                    //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtExtend.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtExtend.Rows[0]["Units"].ToString()));
                }
            }





            //HttpContext.Current.Session["VoucherAmount"] = String.Format("{0:0.00}", Convert.ToDouble(dtpromo.Rows[0]["Voucher_Amount"].ToString()));
            //HttpContext.Current.Session["VoucherCode"] = dtpromo.Rows[0]["Voucher_Id"].ToString();
            //HttpContext.Current.Session["Voucher"] = "yes";
            //HttpContext.Current.Session["VoucherAmount_id" + ssid] = dtpromo1.Rows[0]["Booking_Product_id"].ToString();
            SqlParameter[] parampromo = { new SqlParameter("@booking_id", Session["Booking_id"].ToString()), new SqlParameter("@Product_Type_ID", "27") };
            DataTable dtpromo = dbAccess.GetSpecificData_SP(dbAccess.connString, "get_code_booking_product", parampromo);

            if (dtpromo.Rows.Count > 0)
            {
                if (Session["Voucher"].ToString() == "no")
                {
                    //TtAmount = TtAmount /*- (Convert.ToDouble(dtpromo.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtpromo.Rows[0]["Units"].ToString()))*/;
                    //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtpromo.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtpromo.Rows[0]["Units"].ToString()));
                }
                else if (Session["Voucher"].ToString() == "yes")
                {
                    TtAmount = TtAmount - (Convert.ToDouble(Session["VoucherAmount"].ToString()));
                    TtAmountCalc = TtAmountCalc - (Convert.ToDouble(Session["VoucherAmount"].ToString()));
                }
            }
            else
            {
                if (Session["Voucher"].ToString() == "yes")
                {
                    TtAmount = TtAmount - (Convert.ToDouble(Session["VoucherAmount"].ToString()));
                    TtAmountCalc = TtAmountCalc - (Convert.ToDouble(Session["VoucherAmount"].ToString()));
                }
                else if (Session["Voucher"].ToString() == "no")
                {
                    //TtAmount = TtAmount - (Convert.ToDouble(dtpromo.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtpromo.Rows[0]["Units"].ToString()));
                    //TtAmountCalc = TtAmountCalc - (Convert.ToDouble(dtpromo.Rows[0]["Fare"].ToString()) * Convert.ToDouble(dtpromo.Rows[0]["Units"].ToString()));
                }
            }

            if (Session["Extended"] != null)
            {
                if (Session["Extended"].ToString() == "no")
                {
                    //SqlParameter[] parameters = { new SqlParameter("@ReqNo", enc.Decrypt(Request.QueryString["reqno"].ToString())), new SqlParameter("@Cancellation", "0") };
                    //addBooking.updateRecords("Update_tblLinkmaster_Cancellation", ConfigurationManager.AppSettings["sqlcn_pay"].ToString(), parameters);
                }
            }
            Session["orderno"] = orderno;

            //Sessions
            Session["VendorTxCode"] = strVendorTxCode;
            Session["reqno"] = enc.Decrypt(Request.QueryString["reqno"].ToString());
            Session["pnrno"] = strPNRValue.Split('-')[0];// strPNRValue;
                                                         //--Session["Title"] = ddlTitle.SelectedItem.ToString();
            Session["FirstName"] = Request.Form["txtCCFName"].ToString();
            //--Session["LastName"] = txtCCLName.Text.ToString();
            Session["CardType"] = Request.Form["ddlCardType"].ToString();
            Session["CardNumber"] = Request.Form["txtCardNumber"].ToString().Replace(" ", "");
            //--Session["IssueNumber"] = txtINum.Text.ToString();
            Session["CVVNumber"] = Request.Form["txtCVValue"].ToString();
            //Session["ValidFrom"] = ddlVFMonth.SelectedItem.Value.ToString() + "" + ddlVFYear.SelectedItem.Value.ToString();

            //if (ddlCardType.SelectedValue != "AMEX")
            //{
            //    Session["ValidFrom"] = Request.Form["ddlVFMonth"].ToString() + Request.Form["ddlVFYear"].ToString();
            //}
            //else
            //{
            //    Session["ValidFrom"] = "";
            //}
            Session["CardCV2"] = Request.Form["txtCVValue"].ToString();
            Session["CardExpiryMonth"] = Request.Form["ddlVTMonth"].ToString();
            Session["CardExpiryYear"] = Request.Form["ddlVTYear"].ToString();
            Session["ValidTo"] = Request.Form["ddlVTMonth"].ToString() + "" + Request.Form["ddlVTYear"].ToString();
            Session["CardExpiry"] = Request.Form["ddlVTMonth"].ToString() + "" + Request.Form["ddlVTYear"].ToString().Substring(Request.Form["ddlVTYear"].ToString().Length - 2, 2);
            Session["Email"] = Request.Form["txtEmail"].ToString();
            Session["PhNumber"] = Request.Form["txtPhone"].ToString().Replace("+", "");
            Session["Address"] = Request.Form["txtAddLine"].ToString().Replace("'", "");
            Session["City"] = Request.Form["txtCity"].ToString().Replace("'", "");
            Session["PostCode"] = Request.Form["txtPostCode"].ToString().Replace("'", "");
            Session["Country"] = Request.Form["ddlCountry"].ToString().Replace("'", "");
            Session["PhoneNo"] = Request.Form["txtPhone"].ToString();
            Session["State"] = Request.Form["txtCounty"].ToString();
            Session["Amount"] = TtAmount;
            //end of sessions

            try
            {
                if (Request.QueryString["pnrno"] != null)
                {
                    if (enc.Decrypt(Request.QueryString["pnrno"].ToString()) != "")
                    {
                        //sift science
                        create_order_siftSceience(BokingID);
                    }
                }
            }
            catch
            {

            }


            try
            {
                //Ivoiation
                CheckTransactionDetailsResponse result = check_transaction_request();

            }
            catch
            {
            }

            string sssa = Session["CardExpiryMonth"].ToString();
            string sssa1 = Session["CardExpiryYear"].ToString();

            string test = "";
            test = txtTitle.Value;
            test = Request.Form["txtCCFName"];
            test = Request.Form["txtCCLName"];
            test = Request.Form["ddlCardType"];//ddlCardType.SelectedItem.Value;
            test = Request.Form["txtAddLine"];
            test = Request.Form["txtCity"];

            test = Request.Form["txtCountry"];
            test = Request.Form["txtCountry"];
            test = Request.Form["txtPostCode"];
            test = Request.Form["txtCardNumber"].Trim();

            test = Request.Form["ddlVTMonth"].ToString() + Request.Form["ddlVTYear"].ToString();
            test = Request.Form["txtCVValue"];
            test = txtINum.Value;
            test = cardCharges.ToString();
            test = "0.0";
            test = TtAmount.ToString();









            //wa_idno = 0;


            try
            {
                if (true)//Session["wa_idno" + ssid] == null
                {
                    //wa_idno = website_activity.activity_Insert(Session["PTitle1" + ssid].ToString() + "." + Session["PFName1" + ssid].ToString() + " " + Session["PLName1" + ssid].ToString(), "", Session["Departure" + sid].ToString(), Session["Destination" + sid].ToString(), Session["depDateTextValue" + sid].ToString(), Session["arrDateTextValue" + sid].ToString(), Session["JType" + sid].ToString(), TotalPax, Convert.ToDouble(Session["TtAmount" + ssid]), "Phone No : " + Session["HPhoneC" + ssid].ToString() + " " + Session["HPHoneNo" + ssid].ToString() + "; Mobile : " + Session["PMobileNo" + ssid].ToString() + "; Email : " + Session["PEmail" + ssid].ToString(), "", "", Session["afid" + sid].ToString(), dtHotels.Rows[0]["InfoVia"].ToString(), Session["SearchType" + sid].ToString());
                    dbAccess.updateFollowUp(Convert.ToInt64(HttpContext.Current.Session["booking_id"]), "", "Passenger accepted the \"Terms and Conditions\" for Payment Link");
                }
                else
                {
                    //wa_idno = Convert.ToInt64(Session["wa_idno" + ssid].ToString());
                }
            }
            catch
            {
            }

            //if (wa_idno != 0)
            //Session["wa_idno" + ssid] = wa_idno;


            IFPayment.Attributes["src"] = "PaymentRedirect.aspx?ssid=" + enc.Encrypt(ssid) + "&sid=" + enc.Encrypt(sid) + "&ssid=" + enc.Encrypt(ssid);

            //divPayment.Attributes.Add("style", "Display:none");
            //div3DSecure.Attributes.Add("style", "Display:''"); 


            pnlPayment.Visible = false;
            pnl3DSecure.Visible = true;


        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool flag = true;
        //string validation = "";
        //lblError.Text = "";                
        //if (validation != "")
        //{
        //    flag = false;
        //    trErrMsg.Attributes.Add("style", "Display:''");
        //    lblError.Text += validation.TrimEnd('-');
        //}
        lblError.Text = "";
        trErrMsg.Style.Add("display", "none");


        if (flag)
        {


            //var -start
            string strVendorTxCode = "";
            //var -end


            // VendoeTxCode
            //Randomize();
            string strPNRValue = "";
            if (tmPNR.Value != "")
            {
                strPNRValue = tmPNR.Value;
            }
            else
            {
                strPNRValue = tmHPNR.Value;
            }
            strPNRValue =
            strVendorTxCode = strPNRValue + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();
            DateTime dtCardReUse = DateTime.Now.AddDays(-60);
            try
            {
                string qr = "select Pay_Date_Time,CreditCard_Number from Receipt_Confirm where CreditCard_Number='" + Request.Form["txtCardNumber"].ToString() + "' and Pay_Date_Time>='" + dtCardReUse.ToString("MM/dd/yyyy") + "'";
                DataTable dtCards = dbAccess.GetSpecificData(qr, ConfigurationManager.AppSettings["sqlcn_db"].ToString());
                if (dtCards.Rows.Count > 0)
                {
                    strVendorTxCode = "R-" + strVendorTxCode;
                }
            }
            catch
            {

            }
            try
            {
                string ipCountryCode = getIPCountryCode(Request.UserHostAddress.ToString());
                if (ipCountryCode != "UK")
                {
                    strVendorTxCode = "O-" + strVendorTxCode;
                }
            }
            catch
            {

            }
            strVendorTxCode = strVendorTxCode.Replace(".", "");
            strVendorTxCode = strVendorTxCode.Replace(" ", "");
            if (strVendorTxCode.Length > 40)
            {
                strVendorTxCode = strVendorTxCode.Substring(0, 40);
            }
            //
            //strVendorTxCode = "464657";
            long BokingID = 0;
            if (Request.QueryString["pnrno"] != null)
            {
                if (Request.QueryString["pnrno"].ToString() != "")
                {
                    BokingID = getBookingID(PNR);
                    Session["Booking_id"] = BokingID.ToString();
                }
            }

            if (BokingID == 0)
            {
                if (Session["shref"] != null)
                {
                    if (Session["shref"].ToString() != "")
                    {
                        string StrHrf = getHotel_Ref(Session["shref"].ToString());
                        if (StrHrf != "")
                        {
                            BokingID = getBookingID_Hotel(StrHrf);
                            if (BokingID > 0)
                                Session["Booking_id"] = BokingID.ToString();
                        }
                    }
                }
                //orderno = DateTime.Now.ToString("ddMMyyhhmmss");
            }
            else
            {
                orderno = BokingID.ToString();
            }
            Session["orderno"] = orderno;

            //Sessions
            Session["VendorTxCode"] = strVendorTxCode;
            Session["reqno"] = enc.Decrypt(Request.QueryString["reqno"].ToString());
            Session["pnrno"] = strPNRValue.Split('-')[0];// strPNRValue;
            //--Session["Title"] = ddlTitle.SelectedItem.ToString();
            Session["FirstName"] = Request.Form["txtCCFName"].ToString();
            //--Session["LastName"] = txtCCLName.Text.ToString();
            Session["CardType"] = Request.Form["ddlCardType"].ToString();
            Session["CardNumber"] = Request.Form["txtCardNumber"].ToString();
            //--Session["IssueNumber"] = txtINum.Text.ToString();
            Session["CVVNumber"] = Request.Form["txtCVValue"].ToString();
            //Session["ValidFrom"] = ddlVFMonth.SelectedItem.Value.ToString() + "" + ddlVFYear.SelectedItem.Value.ToString();

            //if (ddlCardType.SelectedValue != "AMEX")
            //{
            //    Session["ValidFrom"] = Request.Form["ddlVFMonth"].ToString() + Request.Form["ddlVFYear"].ToString();
            //}
            //else
            //{
            //    Session["ValidFrom"] = "";
            //}
            Session["CardCV2"] = Request.Form["txtCVValue"].ToString();
            Session["CardExpiryMonth"] = Request.Form["ddlVTMonth"].ToString();
            Session["CardExpiryYear"] = Request.Form["ddlVTYear"].ToString();
            Session["ValidTo"] = Request.Form["ddlVTMonth"].ToString() + "" + Request.Form["ddlVTYear"].ToString();
            Session["CardExpiry"] = Request.Form["ddlVTMonth"].ToString() + "" + Request.Form["ddlVTYear"].ToString().Substring(Request.Form["ddlVTYear"].ToString().Length - 2, 2);
            Session["Email"] = Request.Form["txtEmail"].ToString();
            Session["PhNumber"] = Request.Form["txtPhone"].ToString().Replace("+", "");
            Session["Address"] = Request.Form["txtAddLine"].ToString().Replace("'", "");
            Session["City"] = Request.Form["txtCity"].ToString().Replace("'", "");
            Session["PostCode"] = Request.Form["txtPostCode"].ToString().Replace("'", "");
            Session["Country"] = Request.Form["ddlCountry"].ToString().Replace("'", "");
            Session["PhoneNo"] = Request.Form["txtPhone"].ToString();
            Session["State"] = Request.Form["txtCounty"].ToString();
            Session["Amount"] = TtAmount;
            //end of sessions

            try
            {
                if (Request.QueryString["pnrno"] != null)
                {
                    if (Request.QueryString["pnrno"].ToString() != "")
                    {
                        //sift science
                        create_order_siftSceience(BokingID);
                    }
                }
            }
            catch
            {

            }


            try
            {
                //Ivoiation
                CheckTransactionDetailsResponse result = check_transaction_request();

            }
            catch
            {
            }

            string sssa = Session["CardExpiryMonth"].ToString();
            string sssa1 = Session["CardExpiryYear"].ToString();

            string test = "";
            test = txtTitle.Value;
            test = Request.Form["txtCCFName"];
            test = Request.Form["txtCCLName"];
            test = Request.Form["ddlCardType"];//ddlCardType.SelectedItem.Value;
            test = Request.Form["txtAddLine"];
            test = Request.Form["txtCity"];

            test = Request.Form["txtCountry"];
            test = Request.Form["txtCountry"];
            test = Request.Form["txtPostCode"];
            test = Request.Form["txtCardNumber"].Trim();

            test = Request.Form["ddlVTMonth"].ToString() + Request.Form["ddlVTYear"].ToString();
            test = Request.Form["txtCVValue"];
            test = txtINum.Value;
            test = cardCharges.ToString();
            test = "0.0";
            test = TtAmount.ToString();









            //wa_idno = 0;


            try
            {
                if (true)//Session["wa_idno" + ssid] == null
                {
                    //wa_idno = website_activity.activity_Insert(Session["PTitle1" + ssid].ToString() + "." + Session["PFName1" + ssid].ToString() + " " + Session["PLName1" + ssid].ToString(), "", Session["Departure" + sid].ToString(), Session["Destination" + sid].ToString(), Session["depDateTextValue" + sid].ToString(), Session["arrDateTextValue" + sid].ToString(), Session["JType" + sid].ToString(), TotalPax, Convert.ToDouble(Session["TtAmount" + ssid]), "Phone No : " + Session["HPhoneC" + ssid].ToString() + " " + Session["HPHoneNo" + ssid].ToString() + "; Mobile : " + Session["PMobileNo" + ssid].ToString() + "; Email : " + Session["PEmail" + ssid].ToString(), "", "", Session["afid" + sid].ToString(), dtHotels.Rows[0]["InfoVia"].ToString(), Session["SearchType" + sid].ToString());

                }
                else
                {
                    //wa_idno = Convert.ToInt64(Session["wa_idno" + ssid].ToString());
                }
            }
            catch
            {
            }

            //if (wa_idno != 0)
            //Session["wa_idno" + ssid] = wa_idno;


            IFPayment.Attributes["src"] = "PaymentRedirect.aspx?ssid=" + enc.Encrypt(ssid) + "&sid=" + enc.Encrypt(sid);

            //divPayment.Attributes.Add("style", "Display:none");
            //div3DSecure.Attributes.Add("style", "Display:''"); 


            pnlPayment.Visible = false;
            pnl3DSecure.Visible = true;


        }

    }

    protected string getSabrePNR(string pnr, string gds_id)
    {


        if (gds_id == "2")
        {
            gds_id = "23";
        }


        //if (gds_id == "2")
        //{
        //    gds_id = "139";
        //}
        //else
        //{
        //    gds_id = "139";
        //}

        sabreReq objSabre = new sabreReq(gds_id);




        string result1 = "";
        string result = "";
        string query = "";

        try
        {
            query = " <SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/'  xmlns:eb='http://www.ebxml.org/namespaces/messageHeader'      xmlns:xlink='http://www.w3.org/1999/xlink'  xmlns:xsd='http://www.w3.org/1999/XMLSchema'>"
                        + "<SOAP-ENV:Header>"
                        + " <eb:MessageHeader SOAP-ENV:mustUnderstand='1' eb:version='2.0'>"
                       + "<eb:ConversationId>" + objSabre.conversionId + "</eb:ConversationId>"
                       + "<eb:From>"
                        + "<eb:PartyId type='urn:x12.org:IO5:01'>" + objSabre.partyIdFrom + "</eb:PartyId>"
                          + "  </eb:From>"
                            + "<eb:To>"
                            + "<eb:PartyId type='urn:x12.org:IO5:01'> " + objSabre.partyIdTo + "</eb:PartyId> "
                              + "</eb:To>"
                             + "<eb:CPAId>" + objSabre.ipcc + "</eb:CPAId>"
                             + "<eb:Service eb:type='sabreXML'>Session</eb:Service> "
                             + "<eb:Action>SessionCreateRQ</eb:Action>"
                             + "<eb:MessageData>"
                             + "<eb:MessageId>" + objSabre.MessageId + "</eb:MessageId>"
                             + "<eb:Timestamp>" + timestamp + "Z</eb:Timestamp>"
                             + "   </eb:MessageData> "
                          + "</eb:MessageHeader>"

                           + "<wsse:Security xmlns:wsse='http://schemas.xmlsoap.org/ws/2002/12/secext' xmlns:wsu='http://schemas.xmlsoap.org/ws/2002/12/utility'>"
                             + "<wsse:UsernameToken>"
                          + " <wsse:Username>" + objSabre.username + "</wsse:Username>"
                            + "   <wsse:Password>" + objSabre.password + "</wsse:Password>"
                              + " <Organization>" + objSabre.ipcc + "</Organization>"
                               + "<Domain>DEFAULT</Domain> "
                            + "</wsse:UsernameToken>"
                          + "</wsse:Security>"
                        + "</SOAP-ENV:Header> "
                         + "<SOAP-ENV:Body>"
                          + "<eb:Manifest SOAP-ENV:mustUnderstand='1' eb:version='2.0'>"
                          + "<eb:Reference xmlns:xlink='http://www.w3.org/1999/xlink' xlink:type='simple'/>"
                         + " </eb:Manifest> "
                        + "</SOAP-ENV:Body> "
                      + "</SOAP-ENV:Envelope>";

            result = objSabre.SendQuery(query);
            if (result != "")
            {
                DataSet dssession = new DataSet();
                try
                {
                    StringReader stream = new StringReader(result);
                    dssession.ReadXml(stream);
                }
                catch (Exception ex)
                { }
                if (dssession.Tables["BinarySecurityToken"] != null)
                {
                    DataTable dtsecuritytoken = dssession.Tables["BinarySecurityToken"];


                    DataTable dtMessageinfo = dssession.Tables["MessageData"];

                    DataTable dtmsgheader = dssession.Tables["MessageHeader"];



                    if (objSabre.ipcc != objSabre.ipccB)
                    {

                        string queryOTA_ContextChangeRQ = "";
                        queryOTA_ContextChangeRQ += "   <SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' xmlns:SOAP-ENC='http://schemas.xmlsoap.org/soap/encoding/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>"
                                      + "<SOAP-ENV:Header>"
                                        + "<eb:MessageHeader  xmlns:eb='http://www.ebxml.org/namespaces/messageHeader' SOAP-ENV:mustUnderstand='1' eb:version='2.0'>"
                                          + "<eb:From>"
                                            + "<eb:PartyId type='urn:x12.org:IO5:01'>" + objSabre.partyIdFrom + "</eb:PartyId>"
                                            + " </eb:From>"
                                          + "<eb:To>"
                                            + " <eb:PartyId type='urn:x12.org:IO5:01'>" + objSabre.partyIdTo + "</eb:PartyId>"
                                            + "</eb:To>"
                                          + " <eb:CPAId>" + objSabre.ipcc + "</eb:CPAId>"
                                          + " <eb:ConversationId>" + objSabre.conversionId + "</eb:ConversationId>"
                                          + " <eb:Service eb:type='OTA'>Air Shopping Service</eb:Service>"
                                        + "<eb:Action>ContextChangeLLSRQ</eb:Action>"

                                           + "<eb:MessageData>"
                                            + " <eb:MessageId>" + objSabre.MessageId + "</eb:MessageId>"
                                            + "<eb:Timestamp>" + timestamp + "</eb:Timestamp>"
                                            + "      </eb:MessageData>"
                                            + "<eb:RefToMessageId>" + dtMessageinfo.Rows[0]["ReftoMessageId"].ToString() + "</eb:RefToMessageId> "

                                            + "<eb:DuplicateElimination /> "
                                            + "    </eb:MessageHeader>"
                                        + "<wsse:Security xmlns:wsse='http://schemas.xmlsoap.org/ws/2002/12/secext' xmlns:wsu='http://schemas.xmlsoap.org/ws/2002/12/utility'>"
                                          + "<wsse:BinarySecurityToken valueType='String' EncodingType='wsse:Base64Binary'>" + dtsecuritytoken.Rows[0]["BinarySecurityToken_Text"].ToString() + "</wsse:BinarySecurityToken>"
                                          + "</wsse:Security>"
                                        + "</SOAP-ENV:Header>"
                                    + "<SOAP-ENV:Body>";

                        queryOTA_ContextChangeRQ += "<ContextChangeRQ xmlns='http://webservices.sabre.com/sabreXML/2011/10' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' Version='2.0.0'>"
            + "<ChangeAAA PseudoCityCode='" + objSabre.ipccB + "'/>"
            + "</ContextChangeRQ>"
                        + "</SOAP-ENV:Body>"
                                             + "</SOAP-ENV:Envelope>";
                        string changeContext = objSabre.SendQuery(queryOTA_ContextChangeRQ);

                    }







                    //XmlDocument xmlDoc = new XmlDocument();
                    //xmlDoc.Load(Server.MapPath("BargainFinderMaxRQ_Payload.xml"));
                    //StringWriter sw = new StringWriter();
                    //XmlTextWriter xw = new XmlTextWriter(sw);
                    //xmlDoc.WriteTo(xw);
                    //string strPost = sw.ToString();
                    timestamp = DateTime.UtcNow.ToString();



                    query = "<TravelItineraryReadRQ xmlns='http://webservices.sabre.com/sabreXML/2011/10' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' ReturnHostCommand='false' TimeStamp='" + DateTime.Now.Year + "-" + DateTime.UtcNow.Month.ToString().PadLeft(2, '0') + "-" + DateTime.UtcNow.Day.ToString().PadLeft(2, '0') + "T" + DateTime.UtcNow.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.UtcNow.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.UtcNow.Second.ToString().PadLeft(2, '0') + "' Version='2.0.0'>";

                    //<!-- Mandatory -->
                    //<!-- Repeat Factor=0 -->
                    query += "<MessagingDetails>";
                    //<!-- Optional -->
                    //<!-- Repeat Factor=0 -->
                    //<!--
                    //"ApplicationID" is not applicable for external SWS subscribers.
                    //-->
                    //<ApplicationID>DEF456</ApplicationID>
                    //<!-- Mandatory -->
                    //<!-- Repeat Factor=0-99 -->
                    //<!--
                    //"Code" is used to specify SDS transaction code definitions
                    //-->
                    //<!--
                    //Acceptable values for Code: GEN, PAX, FNR, FFT, PHN, AFD, GFD, ADR, CDR, ACR, PPT, HRM, FQP, GRM, BSD, AIT, HOT, CAR, TIN, AX1, AX2, TRM, TFS, FOP, WSR, PH4, FPL, FBL, ACC, ITN, ITR, INV, DDR, PTD, SIP.
                    //-->
                    //<!--
                    //Note: to redisplay the contents of a work area (AAA) please pass "PNR".
                    //-->
                    //<!-- Equivalent Sabre host command: JX PNR/ACR*ABCDEF -->
                    query += "<Transaction Code='PNR'/>";
                    //<Transaction Code="ACR"/>
                    query += "</MessagingDetails>";
                    //<!-- Optional -->
                    //<!-- Repeat Factor=0 -->
                    //<!-- "ID" is used to specify the record locator. -->
                    //<!-- Equivalent Sabre host command: JX PNR*ABCDEF -->
                    query += " <UniqueID ID='" + pnr + "'/>";
                    query += "</TravelItineraryReadRQ>";




                    query = objSabre.AddSOAPheader(query, objSabre.conversionId, objSabre.MessageId, dtMessageinfo.Rows[0]["ReftoMessageId"].ToString(), "TravelItineraryReadLLSRQ", dtsecuritytoken.Rows[0]["BinarySecurityToken_Text"].ToString());
                    result1 = objSabre.SendQuery(query);

                    query = " <SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/'  xmlns:eb='http://www.ebxml.org/namespaces/messageHeader'      xmlns:xlink='http://www.w3.org/1999/xlink'  xmlns:xsd='http://www.w3.org/1999/XMLSchema'>"
                    + "<SOAP-ENV:Header>"
                    + " <eb:MessageHeader SOAP-ENV:mustUnderstand='1' eb:version='2.0'>"
                   + "<eb:ConversationId>" + objSabre.conversionId + "</eb:ConversationId>"
                   + "<eb:From>"
                    + "<eb:PartyId type='urn:x12.org:IO5:01'>" + objSabre.partyIdFrom + "</eb:PartyId>"
                      + "  </eb:From>"
                        + "<eb:To>"
                        + "<eb:PartyId type='urn:x12.org:IO5:01'> " + objSabre.partyIdTo + "</eb:PartyId> "
                          + "</eb:To>"
                         + "<eb:CPAId>" + objSabre.ipcc + "</eb:CPAId>"
                         + "<eb:Service eb:type='sabreXML'>Session</eb:Service> "
                         + "<eb:Action>SessionCloseRQ</eb:Action>"
                         + "<eb:MessageData>"
                         + "<eb:MessageId>" + objSabre.MessageId + "</eb:MessageId>"
                         + "<eb:Timestamp>" + timestamp + "Z</eb:Timestamp>"
                         + "   </eb:MessageData> "
                      + "</eb:MessageHeader>"
                         + "<wsse:Security xmlns:wsse='http://schemas.xmlsoap.org/ws/2002/12/secext'>"
                          + "<wsse:BinarySecurityToken valueType='String' EncodingType='wsse:Base64Binary'>" + dtsecuritytoken.Rows[0]["BinarySecurityToken_Text"].ToString() + "</wsse:BinarySecurityToken>"
                        + "</wsse:Security>"
                    + "</SOAP-ENV:Header> "
                     + "<SOAP-ENV:Body>"
                      + "<eb:Manifest SOAP-ENV:mustUnderstand='1' eb:version='2.0'>"
                      + "<eb:Reference xmlns:xlink='http://www.w3.org/1999/xlink' xlink:type='simple'/>"
                      + "<SessionCloseRQ>"
                        + "<POS>"
                        + "<Source PseudoCityCode='" + objSabre.ipcc + "'/>"
                         + "</POS>"
                         + "</SessionCloseRQ>"
                      + " </eb:Manifest> "
                    + "</SOAP-ENV:Body> "
                  + "</SOAP-ENV:Envelope>";

                    result = objSabre.SendQuery(query);





                }
            }

        }
        catch (Exception ex)
        {

            Email_sender1.SendEmail("Error - display Itinerary", ex.Message.ToString());
            result1 = "";
        }

        return result1;

    }



    protected string get_WSPAN_PNR(string PNR, string gds_id)
    {
        string strTC = "<tc><iden u='travelmerry' p='travelmerry0000' /><provider session='W3XML' pcc='OCR'>Worldspan</provider><trace></trace></tc>";


        try
        {
            SqlParameter[] paramGDS = { new SqlParameter("@gds_ID", gds_id) };
            DataTable dtGDS = DataLayer.GetData("Api_GDS_SelectByID_1P_1", paramGDS, ConfigurationManager.ConnectionStrings["RConnectionStringWS_wsdata"].ToString());

            if (dtGDS != null)
            {
                if (dtGDS.Rows.Count > 0)
                {


                    strTC = "<tc><iden u='" + dtGDS.Rows[0]["UserName"].ToString() + "' p='" + dtGDS.Rows[0]["Password"].ToString() + "' /><provider session='" + dtGDS.Rows[0]["Session"].ToString() + "' pcc='" + dtGDS.Rows[0]["Pseudo_Login"].ToString() + "'>Worldspan</provider><trace></trace></tc>";
                }
            }

        }
        catch
        {


        }






        string request = "<DPC8><REC_LOC>" + PNR + "</REC_LOC><ETR_INF>Y</ETR_INF><ALL_PNR_INF>Y</ALL_PNR_INF></DPC8>";


        XXDotNetClient netClient = new XXDotNetClient(); // create the object
        netClient.set_Host("xmlpro.wspan.com");
        netClient.set_Port(8800);
        netClient.set_Encrypt(false);
        netClient.set_SSL(false);
        netClient.set_Native(true);
        netClient.set_TC(strTC);

        string response = netClient.Transaction(request);




        return response;
    }




    private double getPnrPrice(string result)
    {
        double TtPrice = 0.0;

        try
        {
            int start = result.IndexOf("<TravelItineraryReadRS");
            int end = result.IndexOf("</TravelItineraryReadRS>");

            result = result.Substring(start, end - start) + "</TravelItineraryReadRS>";

            result = result.Replace("CustomerName", "PersonName");

            TravelItineraryReadRS objTravelItineraryReadRS = TravelItineraryReadRS_XML.ResXml(result);

            if (objTravelItineraryReadRS != null)
            {
                if (objTravelItineraryReadRS.TravelItinerary != null)
                {
                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo != null)
                    {
                        if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote != null)
                        {
                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote != null)
                            {
                                for (int pcQCnt = 0; pcQCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote.Length; pcQCnt++)
                                {
                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[pcQCnt].PricedItinerary != null)
                                    {
                                        for (int prcdItCnt = 0; prcdItCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[pcQCnt].PricedItinerary.Length; prcdItCnt++)
                                        {
                                            if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[pcQCnt].PricedItinerary[prcdItCnt].AirItineraryPricingInfo.ItinTotalFare != null)
                                            {
                                                for (int ItinCnt = 0; ItinCnt < objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[pcQCnt].PricedItinerary[prcdItCnt].AirItineraryPricingInfo.ItinTotalFare.Length; ItinCnt++)
                                                {
                                                    if (objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[pcQCnt].PricedItinerary[prcdItCnt].AirItineraryPricingInfo.ItinTotalFare[ItinCnt].Totals != null)
                                                        TtPrice += Convert.ToDouble(objTravelItineraryReadRS.TravelItinerary.ItineraryInfo.ItineraryPricing.PriceQuote[pcQCnt].PricedItinerary[prcdItCnt].AirItineraryPricingInfo.ItinTotalFare[ItinCnt].Totals.TotalFare.Amount.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
        catch (Exception exNew)
        {
            Email_sender1.SendEmail("Display Price Check error", exNew.Message.ToString() + "-" + exNew.StackTrace.ToString());
        }


        return TtPrice;
    }

    private string getMealTypes(string MType)
    {
        string strMType = "";

        return strMType;
    }

    private string getEquipment(string EQPType)
    {
        string strEQPType = "";

        return strEQPType;
    }

    protected string GetBookingstatus_Hotel(string HRef)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcn"].ToString());
        con.Open();
        //SqlDataAdapter da = new SqlDataAdapter("select b.booking_status_id from Booking as b, PNR as p, booking_pnr as bp where bp.booking_id = b.Booking_ID and p.PNR_ID = bp.pnr_id and p.PNR = '" + pnr + "'", con);
        SqlDataAdapter da = new SqlDataAdapter("select b.booking_status_id  from Booking as b, booking_HotelBooking as bh, HotelBooking as hb where bh.booking_id = b.Booking_ID and hb.HotelBooking_ID = bh.HotelBooking_ID and hb.HotelRfID='" + HRef + "'", con);

        DataTable dt = new DataTable();
        da.Fill(dt);
        string status = "";
        if (dt.Rows.Count > 0)
        {
            status = dt.Rows[0]["booking_status_id"].ToString();
        }
        else
        {
            //status = GetBookingstatus1(Session["spnr"].ToString());
            status = GetBookingstatus1_Hotel(Session["shref"].ToString());
        }
        con.Close();
        return status;
    }

    protected string GetBookingstatus1_Hotel(string HRef)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcn"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select b.booking_status_id from Booking as b, HotelBooking as h, booking_HotelBooking as bh where bh.booking_id = b.Booking_ID and h.HotelBooking_id = bh.HotelBooking_id and h.HotelRfID = '" + HRef + "' ", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string status = dt.Rows[0]["booking_status_id"].ToString();

        con.Close();
        return status;
    }

    protected string GetBookingstatus(string pnr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcn"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select b.booking_status_id from Booking as b, PNR as p, booking_pnr as bp where bp.booking_id = b.Booking_ID and p.PNR_ID = bp.pnr_id and p.PNR = '" + pnr + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string status = "";
        if (dt.Rows.Count > 0)
        {
            status = dt.Rows[0]["booking_status_id"].ToString();
        }
        else
        {
            status = GetBookingstatus1(Session["spnr"].ToString());
        }
        con.Close();
        return status;
    }

    protected string GetBookingstatus1(string pnr)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcn"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select b.booking_status_id from Booking as b, PNR as p, booking_pnr as bp where bp.booking_id = b.Booking_ID and p.PNR_ID = bp.pnr_id and p.PNR = '" + pnr + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        string status = dt.Rows[0]["booking_status_id"].ToString();

        con.Close();
        return status;
    }

    public static long getBookingID(string pnr)
    {
        long ID = 0;

        try
        {
            DataTable dtBookingPnr = dbAccess.GetSpecificData("SELECT b.booking_id FROM PNR p,booking_pnr b where p.PNR_ID=b.pnr_id and p.PNR='" + pnr + "'", ConfigurationManager.AppSettings["sqlcn_db"].ToString());
            if (dtBookingPnr != null)
            {
                if (dtBookingPnr.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtBookingPnr.Rows[0]["booking_id"].ToString());
                    //  Email.SendEmail("BookingID", dtBookingPnr.Rows[0]["booking_id"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Email_sender1.SendEmail("Error -PaymentFailed Page ", ex.Message.ToString());
        }
        return ID;
    }

    public string getHotel_Ref(string Strreqno)
    {
        string HotelRf = "";

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcn_pay"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT HotelRefID FROM [dbtmc_Pay].[dbo].[tblLinkMaster] where reqno='" + Strreqno + "' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            HotelRf = dt.Rows[0]["HotelRefID"].ToString();

            con.Close();
        }
        catch
        {

        }


        return HotelRefId;
    }

    public static long getBookingID_Hotel(string HRef)
    {
        long Id = 0;

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["sqlcn"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select b.booking_id from Booking as b, HotelBooking as h, booking_HotelBooking as bh where bh.booking_id = b.Booking_ID and h.HotelBooking_id = bh.HotelBooking_id and h.HotelRfID = '" + HRef + "' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Id = Convert.ToInt64(dt.Rows[0]["booking_id"].ToString());

            con.Close();
        }
        catch (Exception ex)
        {
            Email_sender1.SendEmail("Error -PaymentFailed Page ", ex.Message.ToString());
        }

        return Id;
    }
    public static string getAirline(string air)
    {
        SqlParameter[] parameters = { new SqlParameter("@AirlineCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblAirlineCodes_selection", parameters, dbAccess.connString);


        //  DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblAirlineCodes where AirlineCode='" + air + "'", dbAccess.connString);


        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["AirlineShortName"].ToString();
        }
        else
        {
            return air;

        }
    }
    protected string getIPCountryCode(string ipAddress)
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
            return dt.Rows[0]["country_code"].ToString();
        }
        else
        {
            return "country_code";
        }
    }

    private DataTable GetSpecificDataAmount(string reqno, string ConnString)
    {
        string conStr = ConnString;// ConfigurationManager.AppSettings["sqlcn"].ToString();
        DataTable dt = new DataTable();
        try
        {
            string q = "Select * from tblPayment where reqno='" + reqno + "' order by PayDateTime desc";
            SqlConnection cn = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter(q, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "d");
            dt = ds.Tables["d"];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return dt;
            }

        }
        catch (Exception ex2)
        {
            return dt;
        }
        finally
        {

        }
    }

    private DataTable GetSpecificData(string reqno, string connString)
    {
        string conStr = connString;// ConfigurationManager.AppSettings["sqlcn"].ToString();
        DataTable dt = new DataTable();
        try
        {   //select * from tblLinkMaster where reqno=
            string q = "Select * from  tblLinkMaster where reqno='" + reqno + "'";
            SqlConnection cn = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter(q, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "d");
            dt = ds.Tables["d"];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return dt;
            }

        }
        catch (Exception ex2)
        {
            return dt;
        }
        finally
        {

        }
    }

    private DataTable GetDara(string str1)
    {
        string conStr = ConfigurationManager.AppSettings["sqlcn"].ToString();
        DataTable dt = new DataTable();
        try
        {
            string q = "Select * from tblDisplayData where AirportCode='" + str1 + "'";
            SqlConnection cn = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter(q, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "d");
            dt = ds.Tables["d"];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return dt;
            }

        }
        catch (Exception ex2)
        {
            return dt;
        }
        finally
        {
        }
    }

    private DataTable FlightCode(string strCode)
    {
        string conStr = ConfigurationManager.AppSettings["sqlcn"].ToString();
        DataTable dt = new DataTable();
        try
        {
            string q = "Select * from tblAirlineCodes where AirlineCode='" + strCode + "'";
            SqlConnection cn = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter(q, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "d");
            dt = ds.Tables["d"];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return dt;
            }

        }
        catch (Exception ex2)
        {
            return dt;
        }
        finally
        {

        }
    }

    protected DataTable CreateItinTable()
    {
        DataTable tblitinerary = new DataTable();
        tblitinerary.Columns.Add("PNR_ID", typeof(string));
        tblitinerary.Columns.Add("Segment_No", typeof(int));
        tblitinerary.Columns.Add("Airline", typeof(string));
        tblitinerary.Columns.Add("Flight_No", typeof(string));
        tblitinerary.Columns.Add("Codeshare_Airline", typeof(string));
        tblitinerary.Columns.Add("Class", typeof(string));
        tblitinerary.Columns.Add("Departure", typeof(string));
        tblitinerary.Columns.Add("Destination", typeof(string));
        tblitinerary.Columns.Add("Dept_date", typeof(string));
        tblitinerary.Columns.Add("Dept_time", typeof(string));
        tblitinerary.Columns.Add("Arrival_date", typeof(string));
        tblitinerary.Columns.Add("Arrival_time", typeof(string));
        tblitinerary.Columns.Add("Nextday", typeof(int));
        tblitinerary.Columns.Add("Equipment", typeof(string));
        tblitinerary.Columns.Add("Baggage", typeof(string));
        tblitinerary.Columns.Add("Dept_Terminal", typeof(string));
        tblitinerary.Columns.Add("Arrival_Terminal", typeof(string));
        tblitinerary.Columns.Add("Airline_PNR", typeof(string));
        tblitinerary.Columns.Add("StopOvers", typeof(string));
        tblitinerary.Columns.Add("MealTypes", typeof(string));
        tblitinerary.Columns.Add("ElapsedTime", typeof(string));
        tblitinerary.Columns.Add("OutBoundOrigin", typeof(string));
        tblitinerary.Columns.Add("OutBoundDestination", typeof(string));
        tblitinerary.Columns.Add("InBoundOrigin", typeof(string));
        tblitinerary.Columns.Add("InBoundDestination", typeof(string));

        return tblitinerary;
    }

    protected DataTable CreatePaxTable()
    {
        DataTable tblPax = new DataTable();
        tblPax.Columns.Add("Title", typeof(string));
        tblPax.Columns.Add("LastName", typeof(string));
        tblPax.Columns.Add("FirstName", typeof(string));
        tblPax.Columns.Add("PaxType", typeof(string));
        return tblPax;
    }

    protected DataTable CreateBaggageTable()
    {
        DataTable tblBagg = new DataTable();
        tblBagg.Columns.Add("Segment_No", typeof(int));
        tblBagg.Columns.Add("Airline", typeof(string));
        tblBagg.Columns.Add("Flight_No", typeof(string));
        tblBagg.Columns.Add("Departure", typeof(string));
        tblBagg.Columns.Add("Destination", typeof(string));
        tblBagg.Columns.Add("Checkin_Bag", typeof(string));
        tblBagg.Columns.Add("Cabin_Bag", typeof(string));
        tblBagg.Columns.Add("PaxType", typeof(string));


        return tblBagg;
    }

    private TimeSpan getTotalElapsedTime(string Time1, string Time2)
    {
        TimeSpan t3 = TimeSpan.Parse("00:00");
        try
        {
            TimeSpan t1 = TimeSpan.Parse(Time1);
            TimeSpan t2 = TimeSpan.Parse(Time2);
            t3 = t1.Add(t2);

            return t3;
        }
        catch
        {
            return t3;
        }

        return t3;
    }

    private string CalculateStopOver(string Date1, string Date2)
    {
        string strTime = "";
        //"MM/dd/yyyy hh:mm:sszzz"
        //yyyy/mm/dd
        string[] strDate1 = Date1.Split('~');
        string[] strDate2 = Date2.Split('~');

        string sDate = strDate1[0].ToString();
        string eDate = strDate2[0].ToString();
        string sTime = strDate1[1].ToString().ToCharArray()[0] + "" + strDate1[1].ToString().ToCharArray()[1] + ":" + strDate1[1].ToString().ToCharArray()[2] + "" + strDate1[1].ToString().ToCharArray()[3];
        string eTime = strDate2[1].ToString().ToCharArray()[0] + "" + strDate2[1].ToString().ToCharArray()[1] + ":" + strDate2[1].ToString().ToCharArray()[2] + "" + strDate2[1].ToString().ToCharArray()[3];

        string dtDay1 = "";
        string dtMonth1 = "";
        string dtYear1 = "";
        string dtDay2 = "";
        string dtMonth2 = "";
        string dtYear2 = "";
        dtDay1 = sDate.Substring(8, 2);
        dtMonth1 = sDate.Substring(5, 2);
        dtYear1 = sDate.Substring(0, 4);

        dtDay2 = eDate.Substring(8, 2);
        dtMonth2 = eDate.Substring(5, 2);
        dtYear2 = eDate.Substring(0, 4);

        DateTime startDate = DateTime.Parse(dtMonth1 + "/" + dtDay1 + "/" + dtYear1 + " " + sTime, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
        DateTime endRetDate = DateTime.Parse(dtMonth2 + "/" + dtDay2 + "/" + dtYear2 + " " + eTime, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

        TimeSpan span = (endRetDate - startDate);
        strTime = span.Hours + ":" + span.Minutes;

        //String.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
        //    span.Days, span.Hours, span.Minutes, span.Seconds);
        return strTime;
    }

    private void create_order_siftSceience(long BookingID)
    {
        try
        {

            //Email_To.SendEmail("Sift error", "1-1", "venkateswarlu.g@9flights.co.uk");
            SqlParameter[] param = { new SqlParameter("@BookingID", BookingID) };
            DataTable dtPNR = DataLayer.GetData("GetPNR_By_BookingID", param, ConfigurationManager.AppSettings["sqlcn_db"].ToString());
            //drAAFlightsOut = dtAAFlights.Select("LegNo=1", "search_flight_id ASC");
            //drAAFlightsIn = dtAAFlights.Select("LegNo=2", "search_flight_id ASC");


            //p.PNR,
            //p.Depature,
            //p.Destination,
            //p.Return_Depature_From,
            //p.Return_Depature_To,p.Airline,b.cabin_class

            SqlParameter[] param_pnr_new = { new SqlParameter("@BookingID", BookingID) };
            DataTable dtPNR_N = DataLayer.GetData("GetPNR", param_pnr_new, ConfigurationManager.AppSettings["sqlcn_db"].ToString());

            SqlParameter[] param_pnr_details = { new SqlParameter("@PNR_ID", Convert.ToInt64(dtPNR_N.Rows[0]["PNR_ID"].ToString())) };
            DataTable dtPNR_DETAILS = DataLayer.GetData("GetPNR_Details_By_PNR_ID", param_pnr_details, ConfigurationManager.AppSettings["sqlcn_db"].ToString());


            DateTime depDate = Convert.ToDateTime(dtPNR_DETAILS.Rows[0]["Dept_Date"].ToString());

            Session["DepDate"] = depDate.ToString("ddMMyy");

            //DateTime depDate = DateTime.ParseExact(dtPNR_DETAILS.Rows[0]["Dept_Date"].ToString(), "MM/dd/yy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = depDate.Subtract(DateTime.Now);
            //Email_To.SendEmail("Sift error", "2", "venkateswarlu.g@9flights.co.uk");

            DateTime depDate1 = DateTime.ParseExact(depDate.ToString("dd/MM/yyyy") + " " + dtPNR_DETAILS.Rows[0]["Dept_Time"].ToString().Substring(0, 2) + ":" + dtPNR_DETAILS.Rows[0]["Dept_Time"].ToString().Substring(2, 2), "dd/MM/yyyy H:mm", System.Globalization.CultureInfo.InvariantCulture);

            Int32 unixTimestamp = (Int32)(depDate1.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;





            string create_order = "{";
            create_order += "\"$type\": \"$create_order\",";
            create_order += "\"$api_key\": \"" + AppScriptKey + "\",";
            // create_order += " \"$user_id\": \"" + Session["booking_id" + ssid].ToString() + "\",";
            create_order += " \"$user_id\": \"" + Session["Email"] + "\",";
            create_order += "\"$session_id\": \"" + Session["_sessionid"].ToString() + "\",";
            create_order += "\"$order_id\": \"" + BookingID.ToString() + "\",";//Session["booking_id" + ssid].ToString()
            create_order += "\"$user_email\": \"" + Session["Email"] + "\",";

            //  1 cent = 10,000 micros. $1.23 USD = 123 cents = 1,230,000 micros.
            long amount_micros = Convert.ToInt64(Convert.ToDouble(Session["Amount"]) * 1000000);//Session["TtAmount"].ToString()


            create_order += "\"$amount\": \"" + amount_micros + "\",";
            create_order += "\"$currency_code\": \"USD\",";
            create_order += "\"$billing_address\": {";
            create_order += "\"$name\": \"" + Session["FirstName"].ToString().ToLower() + "\",";//Session["nameOnCard"]
            create_order += "\"$phone\": \"" + Session["PhNumber"].ToString() + "\",";//Session["MobileNo"].ToString()
            create_order += "\"$address_1\": \"" + Session["Address"].ToString() + "\",";
            create_order += "\"$address_2\": \"\",";
            create_order += "\"$city\": \"" + Session["City"].ToString() + "\",";
            create_order += "\"$region\": \"" + Session["State"].ToString() + "\",";
            create_order += "\"$country\": \"" + Session["Country"].ToString() + "\",";
            create_order += "\"$zipcode\": \"" + Session["PostCode"].ToString() + "\"";
            create_order += "},";
            create_order += "\"$payment_methods\": [";
            create_order += "{";
            create_order += "\"$payment_type\": \"$credit_card\",";
            create_order += "\"$payment_gateway\": \"$Cardinel commerce\",";
            create_order += "\"$card_bin\": \"" + Session["CardNumber"].ToString().Substring(0, 6) + "\",";
            create_order += "\"$card_last4\": \"" + Session["CardNumber"].ToString().Substring(Session["CardNumber"].ToString().Length - 4, 4) + "\"";
            create_order += "}";
            create_order += "],";
            //create_order += "\"$shipping_address\": {";
            //create_order += "\"$address_1\": \"" + Session["PAddress1" + ssid].ToString() + "\",";
            //create_order += "\"$address_2\": \"\",";
            //create_order += "\"$city\": \"" + Session["PCity" + ssid].ToString() + "\",";
            //create_order += "\"$region\": \"" + Session["PState" + ssid].ToString() + "\",";
            //create_order += "\"$country\": \"" + Session["PCountry" + ssid].ToString() + "\",";
            //create_order += "\"$zipcode\": \"" + Session["PPostCode" + ssid].ToString() + "\"";
            //create_order += "},";

            create_order += "\"$shipping_address\": {";
            create_order += "\"$address_1\": \"" + getAirport(dtPNR.Rows[0]["Depature"].ToString(), "Airport") + "\",";
            create_order += "\"$address_2\": \"\",";
            create_order += "\"$city\": \"" + getAirport(dtPNR.Rows[0]["Depature"].ToString(), "City") + "\",";
            create_order += "\"$region\": \"" + getAirport(dtPNR.Rows[0]["Depature"].ToString(), "City") + "\",";
            create_order += "\"$country\": \"" + getAirport(dtPNR.Rows[0]["Depature"].ToString(), "Country") + "\",";
            create_order += "\"$zipcode\": \"\"";
            create_order += "},";



            create_order += "\"is_first_time_buyer\": true,";
            create_order += "\"website_brand\": \"TravelMerry.com\",";
            create_order += "\"order_source\": \"Web\",";
            create_order += "\"flight_days_to_departure\": \"" + ts.Days + "\",";


            //DateTime dat_dep = Convert.ToDateTime(dtPNR_DETAILS.Rows[0]["Dept_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

            //DateTime dat_ret = new DateTime();
            //if (dtPNR_DETAILS.Rows.Count == 1)
            //    dat_ret = Convert.ToDateTime(dtPNR_DETAILS.Rows[0]["Arrival_Date"].ToString());
            //else
            //    dat_ret = Convert.ToDateTime(dtPNR_DETAILS.Rows[dtPNR_DETAILS.Rows.Count - 1]["Arrival_Date"].ToString());

            //TimeSpan timeDuration = dat_ret.Subtract(dat_dep);

            int tripduration = 0;//Convert.ToInt16(timeDuration);
            //Convert.ToInt32(dtAAFares.Rows[0]["out_trip_duration"].ToString().PadLeft(4, '0').Substring(0, 2)) + Convert.ToInt32(dtAAFares.Rows[0]["in_trip_duration"].ToString().PadLeft(4, '0').Substring(0, 2));



            create_order += "\"flight_duration\": \"" + tripduration + "\",";
            create_order += "\"flight_route\": \"" + dtPNR.Rows[0]["Depature"].ToString() + ":" + dtPNR.Rows[0]["Destination"].ToString() + "\",";


            string departure_country = getCountryCode(dtPNR.Rows[0]["Depature"].ToString());
            string destination_country = getCountryCode(dtPNR.Rows[0]["Destination"].ToString());
            create_order += "\"flight_departure_time\": \"" + unixTimestamp + "\",";
            create_order += "\"flight_departure_country\": \"" + departure_country + "\",";
            create_order += "\"flight_destination_country\": \"" + destination_country + "\",";
            create_order += "\"flight_country_pair\": \"" + departure_country + ":" + destination_country + "\",";
            create_order += "\"flight_departure_airport\": \"" + dtPNR.Rows[0]["Depature"].ToString() + "\",";
            create_order += "\"flight_arrival_airport\": \"" + dtPNR.Rows[0]["Destination"].ToString() + "\",";
            create_order += "\"flight_num_segments\": \"" + dtPNR_DETAILS.Rows.Count + "\"";
            create_order += "}";





            string response = PostDataToServer(siftsceince_URL, create_order);

            string ress = "";
            if (response != "")
                ress = response;
            //Email_To.SendEmail("Sift response", ress, "venkateswarlu.g@9flights.co.uk");

            try
            {
                SqlParameter[] paramSift ={//Convert.ToInt64(Session["booking_id" + ssid].ToString())
                new SqlParameter("@booking_id",BookingID),
                new SqlParameter("@userid",Session["Email"].ToString())
                };
                DataLayer.insertData("sift_booking_user_insert", paramSift, ConfigurationManager.AppSettings["siftscience"].ToString());

            }
            catch
            {
            }

        }
        catch (Exception ex)
        {
            Email_sender1.SendEmail("Sift error", "venkateswarlu.g@9flights.co.uk", ex.ToString());
            //SendMail("Sift error", ex.ToString());

        }
    }
    private string PostDataToServer(string URL, string postData)
    {

        string Response_from_server = "";
        try
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(URL);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json; charset=UTF-8";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();

            // Display the status.
            //  Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            Response_from_server = reader.ReadToEnd();


            // Display the content.
            //  Console.WriteLine(responseFromServer);


            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

        }
        catch
        {


        }

        return Response_from_server;
    }
    protected string getAirport(string air, string returnresult)
    {

        SqlParameter[] parameters = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", parameters, ConfigurationManager.AppSettings["sqlcn_db"].ToString());
        if (dtAirCode.Rows.Count > 0)
        {
            if (returnresult == "Airport")
                return "" + dtAirCode.Rows[0]["Airport"];
            else if (returnresult == "City")
                return "" + dtAirCode.Rows[0]["City"];
            else if (returnresult == "Country")
                return "" + dtAirCode.Rows[0]["Country"];
            else
                return "" + dtAirCode.Rows[0]["AirportCode"];
        }
        else
        {
            return air;

        }
    }
    private string getCountryCode(string air)
    {
        SqlParameter[] parameters = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("select_tblCityCodes", parameters, ConfigurationManager.AppSettings["sqlcn_db"].ToString());
        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["CountryCode"].ToString();

        }
        else
        {
            return air;

        }
    }

    protected CheckTransactionDetailsResponse check_transaction_request()
    {
        CheckTransactionDetailsResponse response = new CheckTransactionDetailsResponse();

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
 (SecurityProtocolType)768 | (SecurityProtocolType)3072;


        string ipaddress;

        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ipaddress == "" || ipaddress == null)

            ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();


        ////TEST
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
        request.accountcode = Session["Email"].ToString();
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
        ti_values[0].value = Session["Email"].ToString();

        ti_values[1] = new CheckTransactionDetailsProperty();
        ti_values[1].name = "ValueAmount";
        ti_values[1].value = String.Format("{0:00.00}", Convert.ToDouble(Session["Amount"].ToString()));

        ti_values[2] = new CheckTransactionDetailsProperty();
        ti_values[2].name = "ValueCurrency";
        ti_values[2].value = "USD";

        ti_values[3] = new CheckTransactionDetailsProperty();
        ti_values[3].name = "MobilePhoneNumber";
        ti_values[3].value = "+" + Session["PhoneNo"].ToString();

        ti_values[4] = new CheckTransactionDetailsProperty();
        ti_values[4].name = "BillingStreet";
        ti_values[4].value = Session["Address"].ToString();

        ti_values[5] = new CheckTransactionDetailsProperty();
        ti_values[5].name = "BillingCity";
        ti_values[5].value = Session["City"].ToString();

        ti_values[6] = new CheckTransactionDetailsProperty();
        ti_values[6].name = "BillingRegion";
        ti_values[6].value = Session["City"].ToString();

        ti_values[7] = new CheckTransactionDetailsProperty();
        ti_values[7].name = "BillingCountry";
        ti_values[7].value = Session["Country"].ToString();

        ti_values[8] = new CheckTransactionDetailsProperty();
        ti_values[8].name = "BillingPostalCode";
        ti_values[8].value = Session["PostCode"].ToString();

        ti_values[9] = new CheckTransactionDetailsProperty();
        ti_values[9].name = "ShippingStreet";
        ti_values[9].value = Session["Address"].ToString();

        ti_values[10] = new CheckTransactionDetailsProperty();
        ti_values[10].name = "ShippingCity";
        ti_values[10].value = Session["City"].ToString();

        ti_values[11] = new CheckTransactionDetailsProperty();
        ti_values[11].name = "ShippingRegion";
        ti_values[11].value = Session["City"].ToString();

        ti_values[12] = new CheckTransactionDetailsProperty();
        ti_values[12].name = "ShippingCountry";
        ti_values[12].value = Session["Country"].ToString();

        ti_values[13] = new CheckTransactionDetailsProperty();
        ti_values[13].name = "ShippingPostalCode";
        ti_values[13].value = Session["PostCode"].ToString();

        ti_values[14] = new CheckTransactionDetailsProperty();
        ti_values[14].name = "billingShippingMismatch";
        ti_values[14].value = "0";

        ti_values[15] = new CheckTransactionDetailsProperty();
        ti_values[15].name = "CreditCardBin";
        ti_values[15].value = Session["CardNumber"].ToString().Substring(0, 6);//"400000";

        request.txn_properties = ti_values;


        try
        {
            // call check login and see if we allow login
            response = client.CheckTransactionDetails(request);
        }
        catch (Exception excp)
        {
            //callResult = -1;
        }

        try
        {
            if (response != null)
            {
                //long Boking_ID = dbAccess.getBookingID(PNR);
                //orderno = BokingID.ToString();

                SqlParameter[] paramIovation = {
                                               new SqlParameter("@tracknumber",response.trackingnumber.ToString()),
                                               new SqlParameter("@result",response.result.ToString()),
                                               new SqlParameter("@reason",response.reason.ToString()),
                                               new SqlParameter("@Email",Session["Email"].ToString()),
                                               new SqlParameter("@bookingid", Convert.ToInt64(Session["Booking_id"].ToString()))
                                               };


                long i = DataLayer.insertData("iovation_track_insert", paramIovation, ConfigurationManager.AppSettings["Iovation"].ToString());
                Session["Iovation_trackID" + ssid] = i;
            }
        }
        catch
        {

        }


        return response;
    }

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



    // add imsurance


    // suresh csa 

    //public response CSAInsurance(string departdate_csa, string Returndate_csa, string TtAmount)
    //{
    //    string totalpax_csa = "1";

    //    string responsefromserver = "<quoterequest>";
    //    responsefromserver += "<aff>airtest1</aff>";
    //    responsefromserver += "<producer>airtest1</producer>";
    //    responsefromserver += "<productclass>airi02</productclass>";
    //    responsefromserver += "<numinsured>" + totalpax_csa + "</numinsured>";
    //    responsefromserver += " <tripcost>" + TtAmount + "</tripcost>";
    //    responsefromserver += " <departdate>" + departdate_csa + "</departdate>";
    //    responsefromserver += " <returndate>" + Returndate_csa + "</returndate>";
    //    responsefromserver += "</quoterequest>";


    //    quoterequest RqSearch = ReqSearch(responsefromserver);
    //    response results_csa = Search_csa(RqSearch);
    //    return results_csa;

    //}

    //public response Search_csa(quoterequest RqSearch)
    //{
    //    var request = (HttpWebRequest)WebRequest.Create("http://staging.csatravelprotection.com/ws/policyrequest");

    //    string Todaydate = DateTime.Now.ToString();
    //    DateTime Dt = DateTime.Now;
    //    IFormatProvider mFomatter = new System.Globalization.CultureInfo("en-US");

    //    Dt = Convert.ToDateTime(RqSearch.departdate.ToString());
    //    string departdate = Dt.ToString("yyyy-MM-dd");

    //    Dt = Convert.ToDateTime(RqSearch.returndate.ToString());
    //    string returndate = Dt.ToString("yyyy-MM-dd");


    //    var postdata = "xmlrequeststring=<quoterequest>";

    //    postdata += "<aff>" + RqSearch.aff + "</aff>";
    //    postdata += "<producer>" + RqSearch.producer + "</producer>";
    //    postdata += "<productclass>" + RqSearch.productclass + "</productclass>";
    //    postdata += "<numinsured>" + RqSearch.numinsured + "</numinsured>";
    //    postdata += "<departdate>" + departdate + "</departdate>";
    //    postdata += "<returndate>" + returndate + "</returndate>";
    //    postdata += "<tripcost>" + RqSearch.tripcost + "</tripcost>";
    //    postdata += "</quoterequest>";
    //    var data = Encoding.ASCII.GetBytes(postdata);

    //    request.Method = "POST";
    //    request.ContentType = "application/x-www-form-urlencoded";
    //    request.ContentLength = data.Length;

    //    using (var stream = request.GetRequestStream())
    //    {
    //        stream.Write(data, 0, data.Length);
    //    }

    //    var response = (HttpWebResponse)request.GetResponse();





    //    string serachRes = new StreamReader(response.GetResponseStream()).ReadToEnd();

    //    SearchResXmlSave(serachRes, Convert.ToDouble(RqSearch.tripcost.ToString()));

    //    response obj_result = SearchResxml(serachRes);

    //    return obj_result;




    //    //price_insurance = Convert.ToDouble(obj_result.quoteresponse[0].price.ToString());

    //}




    //public quoterequest ReqSearch(string responseFromServer)
    //{
    //    quoterequest Objquoterequest = new quoterequest();

    //    StringReader read = new StringReader(responseFromServer);

    //    XmlSerializer serializer = new XmlSerializer(Objquoterequest.GetType());

    //    XmlReader reader = new XmlTextReader(read);

    //    try
    //    {

    //        Objquoterequest = (quoterequest)serializer.Deserialize(reader);

    //    }

    //    catch (Exception ex)
    //    {

    //    }

    //    finally
    //    {

    //        reader.Close();

    //        read.Close();

    //        read.Dispose();

    //    }

    //    return Objquoterequest;

    //}

    //public response SearchResxml(string serachRes)
    //{
    //    response objSearchResponse = new response();

    //    StringReader read2 = new StringReader(serachRes);

    //    XmlSerializer serializer = new XmlSerializer(objSearchResponse.GetType());

    //    XmlReader reader2 = new XmlTextReader(read2);

    //    try
    //    {
    //        objSearchResponse = (response)serializer.Deserialize(reader2);









    //    }



    //    catch (Exception ex)
    //    {

    //    }

    //    finally
    //    {

    //        reader2.Close();

    //        read2.Close();

    //        read2.Dispose();
    //    }
    //    return objSearchResponse;
    //}


    //public void SearchResXmlSave(string respSearch, double tripcost)
    //{
    //    try
    //    {
    //        string folderPath = Server.MapPath("~/Csa_Srh_RS/");

    //        //Check whether Directory (Folder) exists.
    //        if (!Directory.Exists(folderPath))
    //        {
    //            //If Directory (Folder) does not exists. Create it.
    //            Directory.CreateDirectory(folderPath);
    //        }


    //        //  string FileNamePS = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();


    //        XmlDocument docPSReq = new XmlDocument();
    //        docPSReq.LoadXml(respSearch);
    //        docPSReq.PreserveWhitespace = true;
    //        docPSReq.Save(Server.MapPath("Csa_Srh_RS/" + tripcost + "-SrhRS.xml"));
    //    }
    //    catch (Exception exc)
    //    {
    //    }
    //}










}
