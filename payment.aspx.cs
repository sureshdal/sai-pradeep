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
using System.Net;
using System.Text;
using System.IO;
using System.Net.Mail;
using CardinalCommerce;
using System.Web.Services;


public partial class payment : System.Web.UI.Page
{


    public string ssid = "";
    public string sid = "";
    public double TtAmount = 0;
    public double TAmount = 0;
    public string searchType = "";
    public int TtPaxCt = 0;
    public double TtTax = 0;
    public double TInsurance = 0;

    string[] strIDSplit = null;
    string[] splitID = null;

    public string TotalPax = "";
    public int TotalAdult = 0;
    public int TotalChild = 0;
    public int TotalInfant = 0;

    public string CENTINEL_MESSAGE_VERSION = "1.7";
    public string CENTINEL_PROCESSOR_ID = "202";
    public string CENTINEL_MERCHANT_ID = "50603";
    public string CENTINEL_TRANSACTION_PWD = "asdfgh123";


    //public string CENTINEL_TRANSACTION_URL = "https://centineltest.cardinalcommerce.com/maps/txns.asp";


    public string CENTINEL_TRANSACTION_URL = "https://centinel1000.cardinalcommerce.com/maps/txns.asp";


    public string strTransactionType = "";
    public string srtCompletion = "";
    public string errorNo = "";
    public string errorDesc = "";
    public string enrolled = "";
    public string payload = "";
    public string acsurl = "";
    public string transactionId = "";
    public string OrderId = "";
    public string ECIFlag = "";
    public string strVendorTxCode = "";
    public long ReqID = 0;
    public long ResID = 0;
    public string jtype = "";
    public DataTable dtAAFares = new DataTable();
    public DataTable dtAAFlights = new DataTable();

    DataRow[] drAAFares = null;
    DataRow[] drAAFlights = null;
    DataRow[] drAAFlightsOut = null;
    DataRow[] drAAFlightsIn = null;

    string orderno = "";
    string conn = ConfigurationManager.AppSettings["sqlcn"].ToString();
    private static string strconn = ConfigurationManager.AppSettings["save_search_data"].ToString();
    private static string strconnhotel = ConfigurationManager.AppSettings["sqlcn_hotel_search_data"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
       
            bool isError = false;


            sid = Request.QueryString["sid"].ToString();
            ssid = Request.QueryString["ssid"].ToString();

            IFPayment.Attributes["src"] = "3DRedirect.aspx?ssid=" + ssid + "&sid=" + sid;

            if (Session["SearchType" + sid] != null)
            {
                searchType = Session["SearchType" + sid].ToString();
            }
            else
            {
                Response.Redirect("sessionexpired.aspx?ssid=" + ssid + "&sid=" + sid);
                Response.End();
            }


            //if (!Request.IsSecureConnection)
            //{

            //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
            //    Response.Redirect(redirectUrl);

            //}

            if (Session["SearchResult" + sid] == null)
            {
                Response.Redirect("sessionexpired.aspx?ssid=" + ssid + "&sid=" + sid);
                Response.End();
            }

            if (Session["fid" + ssid] != null)
            {
                string id = Session["fid" + ssid].ToString();
                string[] strIDSplit = id.Split('-');
                string selectQuery = "";
                selectQuery = "select * from flight_search_fares_fv where search_id=" + Convert.ToInt64(strIDSplit[0]) + " and GDS='" + strIDSplit[1] + "' and RfNo=" + Convert.ToInt32(strIDSplit[2]) + "";
                dtAAFares = dbAccess.GetSpecificData(selectQuery, dbAccess.conn_save_search_data);
                selectQuery = "select * from flight_search_flights_fv where search_id=" + Convert.ToInt64(strIDSplit[0]) + " and GDS='" + strIDSplit[1] + "' and RfNo=" + Convert.ToInt32(strIDSplit[2]) + "";
                dtAAFlights = dbAccess.GetSpecificData(selectQuery, dbAccess.conn_save_search_data);

                string expFares = "search_id=" + Convert.ToInt64(strIDSplit[0]) + " and GDS='" + strIDSplit[1] + "' and RfNo=" + Convert.ToInt32(strIDSplit[2]) + "";
                drAAFares = dtAAFares.Select(expFares);
                string expFlights = "search_id=" + Convert.ToInt64(strIDSplit[0]) + " and GDS='" + strIDSplit[1] + "' and RfNo=" + Convert.ToInt32(strIDSplit[2]) + "";
                drAAFlights = dtAAFlights.Select(expFlights);
                TotalPax = (Convert.ToDouble(drAAFares[0]["Adult"]) + Convert.ToDouble(drAAFares[0]["Child"]) + Convert.ToDouble(drAAFares[0]["Infant"])).ToString();
                if (drAAFares.Length <= 0)
                {
                    Response.Redirect("sessionexpired.aspx?ssid=" + ssid + "&sid=" + sid);
                    Response.End();
                }
                if (Convert.ToInt32(drAAFares[0]["FliCount"]) != drAAFlights.Length)
                {
                    Response.Redirect("sessionexpired.aspx?ssid=" + ssid + "&sid=" + sid);
                    Response.End();
                }
            }

            if (Session["booking_id" + ssid] == null)
            {
                addBooking.insertBooking(2, sid, ssid);
            }
            if (Session["Extended" + ssid] != null)
            {
                if (Session["Extended" + ssid].ToString() == "no")
                {
                    if (HttpContext.Current.Session["ExtendedCancellation_id" + ssid] != null)
                    {
                        addBooking.updateBookingProduct(Session["booking_id" + ssid].ToString(), Session["ExtendedCancellation_id" + ssid].ToString());
                        dbAccess.updateFollowUp(Convert.ToInt64(HttpContext.Current.Session["booking_id" + ssid]), "", "Passenger has NOT Opted For Extended Cancellation Policy");
                        Session["ExtendedCancellation_id" + ssid] = null;
                    }
                    //else
                    //{
                    //    addBooking.insertExtended(sid, ssid, Session["booking_id" + ssid].ToString(), TotalPax);
                    //}
                }

                if (Session["Extended" + ssid].ToString() == "yes")
                {
                    if (HttpContext.Current.Session["ExtendedCancellation_id" + ssid] == null)
                    {
                        //    addBooking.updateBookingProduct(Session["booking_id" + ssid].ToString(), Session["ExtendedCancellation_id" + ssid].ToString());
                        //}
                        //else
                        //{
                        addBooking.insertExtended(sid, ssid, Session["booking_id" + ssid].ToString(), TotalPax);
                        dbAccess.updateFollowUp(Convert.ToInt64(HttpContext.Current.Session["booking_id" + ssid]), "", "Passenger Opted For Extended Cancellation Policy : " + Convert.ToDouble(Session["ExtendedCancellation" + ssid].ToString()) + " USD ");
                    }
                }
            }
            if (Session["Voucher" + ssid] != null)
            {
                if (Session["Voucher" + ssid].ToString() == "no")
                {
                    if (HttpContext.Current.Session["VoucherAmount_id" + ssid] != null)
                    {
                        addBooking.updateBookingProduct(Session["booking_id" + ssid].ToString(), Session["VoucherAmount_id" + ssid].ToString());
                        dbAccess.updateFollowUp(Convert.ToInt64(HttpContext.Current.Session["booking_id" + ssid]), "", "Passenger Dont have valid Promo Code");
                        Session["VoucherAmount_id" + ssid] = null;
                    }
                    //else
                    //{
                    //    addBooking.insertPromo(sid, ssid, Session["booking_id" + ssid].ToString(), TotalPax);
                    //}
                }
                if (Session["Voucher" + ssid].ToString() == "yes")
                {
                    if (HttpContext.Current.Session["VoucherAmount_id" + ssid] == null)
                    {
                        //    addBooking.updateBookingProduct(Session["booking_id" + ssid].ToString(), Session["VoucherAmount_id" + ssid].ToString());
                        //}
                        //else
                        //{
                        addBooking.insertPromo(sid, ssid, Session["booking_id" + ssid].ToString(), TotalPax);
                        double VoucherAmount = Convert.ToDouble(HttpContext.Current.Session["VoucherAmount" + ssid].ToString()) * -1;
                        dbAccess.updateFollowUp(Convert.ToInt64(HttpContext.Current.Session["booking_id" + ssid]), "", "Passenger Has valid Promo Code'" + Session["VoucherCode" + ssid].ToString() + "' & Amount: " + VoucherAmount.ToString() + " USD ");
                    }
                }
            }

            TtAmount = Convert.ToDouble(Session["TtAmount" + ssid]);
            string State = "";
            string ValidationExp = "";

            Session["authrisation" + ssid] = "N";


            string[] id_GDS = null;


            if (searchType == "FO")
                id_GDS = Session["fid" + ssid].ToString().Split('-');
            else if (searchType == "HO")
                id_GDS = Session["hid" + ssid].ToString().Split('-');


            string cnstr = ConfigurationManager.AppSettings["sqlcn"].ToString();
            try
            {
                website_activity.activity_update_cc(Convert.ToInt64(Session["wa_idno" + ssid]), Session["CCTitle" + ssid].ToString() + "." + Session["CCFName" + ssid].ToString() + " " + Session["CCLName" + ssid].ToString() + " (" + Session["PTitle1" + ssid].ToString() + "." + Session["PLName1" + ssid].ToString() + " " + Session["PFName1" + ssid].ToString() + ")", TtAmount);
            }
            catch(Exception ex)
            {
            // added by suresh
                errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "website_activity");
            }




            DateTime dtDep = new DateTime();

            try
            {
                string[] strSpitDate = Session["depDateTextValue" + sid].ToString().Split('/');
                dtDep = Convert.ToDateTime(DateTime.Parse(strSpitDate[1].ToString() + "/" + strSpitDate[0].ToString() + "/" + strSpitDate[2].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
            }
            catch (Exception ex)
            {

                errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "dtDep");
            }


            string strVendorTxCode = "";
            string strGiftAid = "";
            //string strAmount = "1.00";
            string strPost = "";
            string strSQL = "";
            //Var

            //string strPost, strSQL="";

            strGiftAid = "0";

            // VendoeTxCode
            //Randomize();
            //strVendorTxCode = Session["SessionPNR" + ssid].ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString() + "-" + Session["PLName1" + ssid].ToString() + "-" + Session["PFName1" + ssid].ToString();


            if (searchType == "FO")
            {
                strVendorTxCode = Session["SessionPNR" + ssid].ToString() + "-" + drAAFares[0]["GDS"].ToString() + "-" + drAAFares[0]["AirCode"].ToString() + "-" + dtDep.ToString("ddMMMyy") + "-" + Session["SesTTo" + sid].ToString() + "-" + drAAFares[0]["InfoVia"].ToString() + "-" + DateTime.Now.ToString("ddMMyyhhmmss");
            }
            else
            {
                strVendorTxCode = Session["nameOnCard" + ssid].ToString() + "-" + DateTime.Now.ToString("ddMMyyhhmmss");

                orderno = DateTime.Now.ToString("ddMMyyhhmmss");

            }
            DateTime dtCardReUse = DateTime.Now.AddDays(-60);
            try
            {
                string qr = "select Pay_Date_Time,CreditCard_Number from Receipt_Confirm where CreditCard_Number='" + Session["CardNumber" + ssid].ToString().Trim() + "' and Pay_Date_Time>='" + dtCardReUse.ToString("MM/dd/yyyy") + "'";
                DataTable dtCards = dbAccess.GetSpecificData(qr, ConfigurationManager.AppSettings["sqlcn"].ToString());
                if (dtCards.Rows.Count > 0)
                {
                    strVendorTxCode = "R-" + strVendorTxCode;
                }
            }
            catch (Exception ex)
            {

                errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "dtCards");
            }

            strVendorTxCode = strVendorTxCode.Replace(".", "");
            strVendorTxCode = strVendorTxCode.Replace(" ", "");
            if (strVendorTxCode.Length > 40)
            {
                strVendorTxCode = strVendorTxCode.Substring(0, 40);
            }
            try
            {
                Session["VendorTxCode" + ssid] = strVendorTxCode;
                if (Session["booking_id" + ssid] != null)
                {
                    pageTrace.pageTracing(Session["booking_id" + ssid].ToString(), "payment");
                }
                else
                {
                    pageTrace.pageTracing(strVendorTxCode, "payment");
                }
            }
            catch (Exception ex)
            {

                errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "strVendorTxCode");
            }





            string ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipaddress == "" || ipaddress == null)

                ipaddress = HttpContext.Current.Request.UserHostAddress.ToString();



            string Country = "";

            
            Country = getIPCountryCode(ipaddress);
            //}
            string card_number = Session["CardNumber" + ssid].ToString(); // replace with card number
            string card_expiry_date = Session["CardExpiry1" + ssid].ToString(); // replace with card expiry date(MMyy)

            double total_sale_amount = Convert.ToDouble(Session["TtAmount" + ssid].ToString()); //replace with total sale amount including card charges
                                                                                                // double total_sale_amount = 0.01; //replace with total sale amount including card charges

            string card_type = Session["CCType" + ssid].ToString(); //replace with card type(VI/MC)
            long booking_id = Convert.ToInt64(Session["booking_id" + ssid].ToString()); // replace with booking id

            DateTime depDate = DateTime.ParseExact(dtAAFlights.Rows[0]["DEP_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture); ; // replace with departure date
            string lead_pax_user_name = Session["PEmail" + ssid].ToString();  // replace with lead pax user name
            string lead_pax_pwd = ""; // replace with lead pax password


            try
            {

                SqlParameter[] paramPass = { new SqlParameter("@Email_Address", Session["PEmail" + ssid].ToString().Trim()) };
                DataTable dtLead_Pax_Password = DataLayer.GetData("Lead_Pax_Password_Select", paramPass, cnstr);

                if (dtLead_Pax_Password != null)
                {
                    if (dtLead_Pax_Password.Rows.Count > 0)
                    {
                        lead_pax_pwd = dtLead_Pax_Password.Rows[0]["Lead_Pax_Password"].ToString();
                    }
                }

                if (lead_pax_pwd == "")
                {
                    lead_pax_pwd = RandomPassword.Generate(8, 8);
                }

            }
            catch(Exception exDB)
            {

                errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", exDB.Message, exDB.StackTrace, "dtLead_Pax_Password");

                //Email1.SendEmail("Password database error", "Password");
            }







            double cc_amount = Convert.ToDouble(Session["CreditCardAmt" + ssid].ToString()); // replace with credit card charges
            string pax_first_name = Session["CCFName" + ssid].ToString();  // replace with card holder first name
            string pax_last_name = Session["CCLName" + ssid].ToString();  // replace with card holder last name
            string card_cv2 = Session["CardCV2" + ssid].ToString();  // replace with card cvv/cv2
            string card_address1 = Session["PAddress1" + ssid].ToString();  // replace with address
            string card_address2 = Session["PAddress1" + ssid].ToString();  // replace with address
            string card_city = Session["PCity" + ssid].ToString();  // replace with city
            string card_state = Session["PState" + ssid].ToString();  // replace with state
            string card_country = Session["PCountry" + ssid].ToString();  // replace with country
            string card_zip = Session["PPostCode" + ssid].ToString(); // replace with zip code
            int staff_id = Convert.ToInt32(Session["aid" + sid].ToString());// replace with staff id
            long lead_pax_id = Convert.ToInt64(Session["lead_pax_id" + ssid].ToString()); // replace with lead pax id


            bool enroll_status = false;
            string converge_acs_url = "";
            string converge_pareq = "";
            string converge_md = "";
            string errMsg = "";

            //cardinal_commerce.insert_receipt(card_number,card_type, card_expiry_date, total_sale_amount, pax_first_name, pax_last_name, card_cv2, card_address1, card_address2, card_city,card_state, card_country, card_zip, staff_id, lead_pax_id, lead_pax_user_name, lead_pax_pwd, booking_id, cc_amount, ssid);
            cybersource.insert_receipt(card_number, card_type, card_expiry_date, total_sale_amount, pax_first_name, pax_last_name, card_cv2, card_address1, card_address2, card_city, card_state, card_country, card_zip, staff_id, lead_pax_id, lead_pax_user_name, lead_pax_pwd, booking_id, cc_amount, ssid);

            // converge_pay_gateway.enroll_3d(ssid, booking_id, card_number, card_expiry_date, total_sale_amount, out enroll_status, out converge_acs_url, out converge_pareq, out converge_md, out errMsg);

            create_order_siftSceience();
            // cardinal_commerce.enroll_3d(ssid, booking_id, card_number, card_expiry_date, total_sale_amount, card_address1, card_address2, card_city, card_state, card_country, card_zip, ipaddress, out enroll_status, out converge_acs_url, out converge_pareq, out converge_md, out errMsg);

            cybersource.enroll_3d(ssid, booking_id, card_number, card_expiry_date, total_sale_amount, card_address1, card_address2, card_city, card_state, card_country, card_zip, ipaddress, out enroll_status, out converge_acs_url, out converge_pareq, out converge_md, out errMsg);





            if (errMsg == "")
            {
                if (enroll_status == true)
                {
                    Session["converge_acs_url" + ssid] = converge_acs_url;
                    Session["converge_pareq" + ssid] = converge_pareq;
                    Session["converge_md" + ssid] = converge_md;
                    Session["converge_card_type" + ssid] = card_type;
                    Response.Redirect("3DRedirect.aspx?sid=" + sid + "&ssid=" + ssid);
                }
                else
                {
                    if (DateTime.Now.AddDays(31) < depDate)
                    {
                        //errMsg = "";
                        //converge_pay_gateway.checkNon3DCard(depDate, ssid, booking_id, "ccsale", card_number, card_expiry_date, total_sale_amount, pax_first_name, pax_last_name, card_cv2, card_address1, card_address2, card_city, card_state, card_country, card_zip, staff_id, lead_pax_id, lead_pax_user_name, lead_pax_pwd, cc_amount, out errMsg);
                        //if (errMsg == "")
                        //{

                        bool isConfirmPNR = true;
                        if (Session["isConfirmPNR" + ssid] != null)
                        {
                            if (Session["isConfirmPNR" + ssid].ToString() == "false")
                            {
                                isConfirmPNR = false;
                            }

                        }



                        if (isConfirmPNR)
                        {
                            Response.Redirect(converge_pay_gateway.payment_success_url + "?ssid=" + ssid + "&sid=" + sid);
                            Response.End();
                        }
                        else
                        {
                            try
                            {
                                dbAccess.updateBookingStatus(9, Convert.ToInt64(Session["booking_id" + ssid].ToString()));
                            }
                            catch (Exception ex)
                            {

                                errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "dbAccess.updateBookingStatus");
                            }
                            Response.Redirect(converge_pay_gateway.payment_ack_url + "?ssid=" + ssid + "&sid=" + sid);
                            Response.End();
                        }
                        
                    }
                    else
                    {

                        try
                        {
                            dbAccess.updateBookingStatus(9, Convert.ToInt64(Session["booking_id" + ssid].ToString()));
                        }
                        catch(Exception ex)
                        {
                            errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "dbAccess.updateBookingStatus");
                        }

                        Response.Redirect(converge_pay_gateway.payment_ack_url + "?ssid=" + ssid + "&sid=" + sid);

                        // Response.Redirect(converge_pay_gateway.payment_failure_url + "?ssid=" + ssid + "&sid=" + sid + "&errMsg=Use 3D enrolled card.");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Redirect(converge_pay_gateway.payment_failure_url + "?ssid=" + ssid + "&sid=" + sid + "&errMsg=Payment failed.");
                Response.End();
            }



            


        }
       
   

    protected string getAirport(string air, string returnresult)
    {

        SqlParameter[] parameters = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", parameters, dbAccess.connString);
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

    private void create_order_siftSceience()
    {
        try
        {

            drAAFlightsOut = dtAAFlights.Select("LegNo=1", "search_flight_id ASC");
            drAAFlightsIn = dtAAFlights.Select("LegNo=2", "search_flight_id ASC");


            DateTime depDate = DateTime.ParseExact(dtAAFlights.Rows[0]["DEP_DAT"].ToString(), "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = depDate.Subtract(DateTime.Now);


            DateTime depDate1 = DateTime.ParseExact(depDate.ToString("dd/MM/yyyy") + " " + dtAAFlights.Rows[0]["DEP_TIM"].ToString().Substring(0, 2) + ":" + dtAAFlights.Rows[0]["DEP_TIM"].ToString().Substring(2, 2), "dd/MM/yyyy H:mm", System.Globalization.CultureInfo.InvariantCulture);

            Int32 unixTimestamp = (Int32)(depDate1.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;








            string create_order = "{";
            create_order += "\"$type\": \"$create_order\",";
            create_order += "\"$api_key\": \"" + RandomPassword.AppScriptKey + "\",";
            // create_order += " \"$user_id\": \"" + Session["booking_id" + ssid].ToString() + "\",";
            create_order += " \"$user_id\": \"" + Session["PEmail" + ssid].ToString().ToLower() + "\",";
            create_order += "\"$session_id\": \"" + Session["_sessionid"].ToString() + "\",";
            create_order += "\"$order_id\": \"" + Session["booking_id" + ssid].ToString() + "\",";
            create_order += "\"$user_email\": \"" + Session["PEmail" + ssid].ToString().ToLower() + "\",";

            //  1 cent = 10,000 micros. $1.23 USD = 123 cents = 1,230,000 micros.
            long amount_micros = Convert.ToInt64(Convert.ToDouble(Session["TtAmount" + ssid].ToString()) * 1000000);


            create_order += "\"$amount\": \"" + amount_micros + "\",";
            create_order += "\"$currency_code\": \"USD\",";
            create_order += "\"$billing_address\": {";
            create_order += "\"$name\": \"" + Session["nameOnCard" + ssid].ToString().ToLower() + "\",";
            create_order += "\"$phone\": \"" + Session["PMobileNo" + ssid].ToString() + "\",";
            create_order += "\"$address_1\": \"" + Session["PAddress1" + ssid].ToString() + "\",";
            create_order += "\"$address_2\": \"\",";
            create_order += "\"$city\": \"" + Session["PCity" + ssid].ToString() + "\",";
            create_order += "\"$region\": \"" + Session["PState" + ssid].ToString() + "\",";
            create_order += "\"$country\": \"" + Session["PCountry" + ssid].ToString() + "\",";
            create_order += "\"$zipcode\": \"" + Session["PPostCode" + ssid].ToString() + "\"";
            create_order += "},";
            create_order += "\"$payment_methods\": [";
            create_order += "{";
            create_order += "\"$payment_type\": \"$credit_card\",";
            create_order += "\"$payment_gateway\": \"$authorizenet\",";//Cardinel commerce
            create_order += "\"$card_bin\": \"" + Session["CardNumber" + ssid].ToString().Substring(0, 6) + "\",";
            create_order += "\"$card_last4\": \"" + Session["CardNumber" + ssid].ToString().Substring(Session["CardNumber" + ssid].ToString().Length - 4, 4) + "\"";
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

            //create_order += "\"$shipping_address\": {";
            //create_order += "\"$address_1\": \"" + getAirport(drAAFlightsOut[0]["DEP_ARP"].ToString(), "Airport") + "\",";
            //create_order += "\"$address_2\": \"\",";
            //create_order += "\"$city\": \"" + getAirport(drAAFlightsOut[0]["DEP_ARP"].ToString(), "City") + "\",";
            //create_order += "\"$region\": \"" + getAirport(drAAFlightsOut[0]["DEP_ARP"].ToString(), "City") + "\",";
            //create_order += "\"$country\": \"" + getAirport(drAAFlightsOut[0]["DEP_ARP"].ToString(), "Country") + "\",";
            //create_order += "\"$zipcode\": \"\"";
            //create_order += "},";



            create_order += "\"is_first_time_buyer\": true,";
            create_order += "\"website_brand\": \"TravelMerry.com\",";
            create_order += "\"order_source\": \"Web\",";
            create_order += "\"flight_days_to_departure\": \"" + ts.Days + "\",";


            int tripduration = Convert.ToInt32(dtAAFares.Rows[0]["out_trip_duration"].ToString().PadLeft(4, '0').Substring(0, 2)) + Convert.ToInt32(dtAAFares.Rows[0]["in_trip_duration"].ToString().PadLeft(4, '0').Substring(0, 2));



            create_order += "\"flight_duration\": \"" + tripduration + "\",";
            create_order += "\"flight_route\": \"" + drAAFlightsOut[0]["DEP_ARP"].ToString() + ":" + drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString() + "\",";


            string departure_country = getCountryCode(drAAFlightsOut[0]["DEP_ARP"].ToString());
            string destination_country = getCountryCode(drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString());
            create_order += "\"flight_departure_time\": \"" + unixTimestamp + "\",";
            create_order += "\"flight_departure_country\": \"" + departure_country + "\",";
            create_order += "\"flight_destination_country\": \"" + destination_country + "\",";
            create_order += "\"flight_country_pair\": \"" + departure_country + ":" + destination_country + "\",";
            create_order += "\"flight_departure_airport\": \"" + drAAFlightsOut[0]["DEP_ARP"].ToString() + "\",";
            create_order += "\"flight_arrival_airport\": \"" + drAAFlightsOut[drAAFlightsOut.Length - 1]["ARR_ARP"].ToString() + "\",";
            create_order += "\"flight_num_segments\": \"" + dtAAFlights.Rows.Count + "\"";
            create_order += "}";





            string response = PostDataToServer(RandomPassword.siftsceince_URL, create_order);



            try
            {
                SqlParameter[] paramSift ={
            new SqlParameter("@booking_id",Convert.ToInt64(Session["booking_id" + ssid].ToString())),
            new SqlParameter("@userid",Session["PEmail" + ssid].ToString())
            };
                DataLayer.insertData("sift_booking_user_insert", paramSift, ConfigurationManager.AppSettings["siftscience"].ToString());
            }
            catch
            {
            }

        }
        catch(Exception ex)
        {
            errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "DataLayer.insertData");

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
        catch(Exception ex)
        {

            errors_Insert_DB.errors("Travelmerry", "payment.aspx.cs", ex.Message, ex.StackTrace, "PostDataToServer");
        }

        return Response_from_server;
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







    public bool Validate_Card_Decision(string strDepDate)
    {
        bool confirm = false;
        try
        {
            DateTime depDate = DateTime.ParseExact(strDepDate, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = depDate.Subtract(DateTime.Now);
            if (ts.Days >= 31)
            {
                confirm = true;
            }
        }
        catch
        {
        }
        return confirm;
    }

    private DataTable CreatePaxTable()
    {
        DataTable dtpax = new DataTable();
        dtpax.Columns.Add("Title", typeof(string));
        dtpax.Columns.Add("FName", typeof(string));
        dtpax.Columns.Add("LName", typeof(string));
        dtpax.Columns.Add("Gender", typeof(string));
        dtpax.Columns.Add("DateOfBirth", typeof(string));
        dtpax.Columns.Add("Age", typeof(string));
        dtpax.Columns.Add("PaxType", typeof(string));

        return dtpax;
    }
    private int CalculateAge(string DateOfBirth)
    {
        int age = 0;
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime PaxDob = Convert.ToDateTime(DateOfBirth);
            DateTime now = DateTime.Today;


            age = now.Year - PaxDob.Year;

            if (now.Month < PaxDob.Month)
            {
                age = age - 1;
            }
            else
            {
                if (now.Month == PaxDob.Month)
                {
                    if (now.Day < PaxDob.Day)
                    {
                        age = age - 1;
                    }
                }
            }
        }
        catch { }
        return age;
    }



    protected string getAirport(string air)
    {

        SqlParameter[] parameters = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", parameters, dbAccess.connString);
        if (dtAirCode.Rows.Count > 0)
        {
            return "" + dtAirCode.Rows[0]["Airport"] + "(" + dtAirCode.Rows[0]["AirportCode"] + ")," + dtAirCode.Rows[0]["City"];
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
            return "Low Cost Carrier";

        }
    }


    private string getAirlinimage(string air)
    {
        if (air == "FR")
        {
            return "images/airline9025/LCC.gif";
        }
        else
        {
            return "images/airline9025/" + air + ".gif";
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







    protected void SendMail(string message, string subject)
    {
        try
        {
            MailMessage myMessage = new MailMessage();
            myMessage.From = new MailAddress("bookings@9hotels.co.uk");
            myMessage.To.Add("raghavendra.p@9flights.co.uk");
            myMessage.Subject = subject.ToString();
            myMessage.IsBodyHtml = true;
            myMessage.Body = message.ToString();
            SmtpClient mySmtpClient = new SmtpClient();
            System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential("bookings@9hotels.co.uk", "saidi18346");
            mySmtpClient.Host = "auth.smtp.1and1.co.uk";
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.Credentials = myCredential;
            mySmtpClient.ServicePoint.MaxIdleTime = 1;
            mySmtpClient.Send(myMessage);
            myMessage.Dispose();
        }
        catch
        {

        }
    }

    private bool Check_AuthenticateORAuthorize(string[] id_GDS)
    {
        bool Authorize = false;

        if (searchType == "FH" || searchType == "HO")
        {
            //DateTime checkin = DateTime.Parse(dtHotels.Rows[0]["CheckIn"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

            //if ((checkin - DateTime.Now).Days < 7)
            //{
            Authorize = true;
            //}
        }


        if (searchType == "FH" || searchType == "FO")
        {
            if (id_GDS[1] == "TD")
            {
                Authorize = true;
            }

        }
        //else
        //{
        //    DateTime checkin = DateTime.Parse(dtHotels.Rows[0]["CheckIn"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        //    if ((checkin - DateTime.Now).Days < 7)
        //    {
        //        Authorize = true;
        //    }
        //}



        return Authorize;
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





}

