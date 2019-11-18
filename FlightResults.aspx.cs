using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Configuration;
using System.IO;

public partial class FlightResults : System.Web.UI.Page
{
    public static string cnstr_track_data = ConfigurationManager.AppSettings["sqlcn_track_data"].ToString();
    public string username = "";
    public string _sessionid = "";

    public string SearchID = "";
    public string message = "";
    public string ssid = "";
    public string searchType = "";
    public int timeout = 20;
    //Added For Book Engine Show Previous search - start
    public string conf = "";
    public string inid = "";
    public string outid = "";

    public string DepartureText = "";
    public string DepartureCity = "";
    public string DestinationText = "";
    public string DestinationCity = "";
    public string depDateTextValue = "";
    public string arrDateTextValue = "";
    public string Departure = "";
    public string Destination = "";
    public string depDateValue = "";
    public string arrDateValue = "";
    public string Aircode = "";
    public int JType = 2;
    public int AdultsCnt = 1;
    public int ChildsCnt = 0;
    public int InfantsCnt = 0;
    public string SearchType = "FO";
    public int Class = 0;
    public string ClassText = "";
    public string DirectFlight = "false";
    public int totPax = 0;
    //Added For Book Engine Show Previous search - End
    public string newsearch = "";
    string selectedFid = "";

    public static long track_id = 0;
    public static string trid = "";

    [System.Web.Services.WebMethod]
    public static void Update_Device_ID(string fingerprint, string track_id)
    {
        if (track_id != "0")
        {
            HttpContext.Current.Session["fingerprint"] = fingerprint;


            SqlParameter[] param_track_fare = {new SqlParameter("@fingerprint",fingerprint),
                                                      new SqlParameter("@trID",Convert.ToInt64(track_id))};
            DataLayer.UpdateData("afid_track_data_update_fingerprint", param_track_fare, cnstr_track_data);

        }

        //return fingerprint;
    }

    protected string getAirportCity(string air)
    {
        // DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblDisplayData where AirportCode='" + air + "'", dbAccess.connString);


        SqlParameter[] param = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", param, dbAccess.connString);



        if (dtAirCode.Rows.Count > 0)
        {
            return dtAirCode.Rows[0]["City"].ToString();
            // return "" + dtAirCode.Rows[0]["Airport"];
        }
        else
        {
            return air;

        }
    }

    public static string getAirportDetails(string air)
    {
        // DataTable dtAirCode = dbAccess.GetSpecificData("Select * from tblDisplayData where AirportCode='" + air + "'", dbAccess.connString);

        SqlParameter[] param = { new SqlParameter("@AirportCode", air) };
        DataTable dtAirCode = DataLayer.GetData("tblDisplayData_selection", param, dbAccess.connString);


        if (dtAirCode.Rows.Count > 0)
        {
            return "" + dtAirCode.Rows[0]["City"] + "," + dtAirCode.Rows[0]["Country"];
            // return "" + dtAirCode.Rows[0]["Airport"];
        }
        else
        {
            return air;

        }
    }


    public void bindvalues()
    {
        try
        {
            DepartureText = Session["DepartureText" + SearchID].ToString();
            DestinationText = Session["DestinationText" + SearchID].ToString();
            if (Session["conf" + SearchID] != null)
            {
                conf = Session["conf" + SearchID].ToString();
                inid = Session["inid" + SearchID].ToString();
                outid = Session["outid" + SearchID].ToString();

                Refresh1.InnerHtml = "<div class='modal-dialog' role='document'>" +
                                                "<div class='modal-content'>" +
                                                    "<div class='modal-header text-center'>" +
                                                        "<button type='button' id='RefreshExpired2' class='close' onclick='searchagain12(\"" + conf + "\",\"" + inid + "\",\"" + outid + "\")' aria-hidden='true'>×</button>" +
                                                        "<h4 class='modal-title' id='myModalLabel1'>Session Expired </h4>" +
                                                    "</div>" +
                                                    "<div class='modal-body text-center'>" +
                                                        "<div class='refreshWrap'>" +
                                                            "<div class='timer'><i class='fa fa-3x icofont icofont-clock-time faa-tada animated'></i></div>" +
                                                            "<h3 class='red'>Timed Out</h3>" +

                                                            "<p><strong>Still interested in flying from " + DepartureCity + " to " + DestinationCity + "?  </strong></p>" +
                                                            "<p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>" +

                                                            "<a href='Index.aspx' class='btn button newSearchbtn mr-15'>New Search </a>" +
                                                            "<button type='submit' class='btn button refreshBtn' onclick='searchagain12(\"" + conf + "\",\"" + inid + "\",\"" + outid + "\")' id='RefreshExpired1'>Refresh  </button>" +
                                                            "<h5 class='red'>Your search results have expired!</h5>" +
                                                        "</div>" +
                                                    "</div>" +
                                                "</div>" +
                                            "</div>";
            }

            Departure = Session["Departure" + SearchID].ToString();
            Destination = Session["Destination" + SearchID].ToString();
            DepartureCity = getAirportCity(Session["Departure" + SearchID].ToString());
            DestinationCity = getAirportCity(Session["Destination" + SearchID].ToString());
            DateTime depDateTemp = DateTime.Parse(Session["depDateTextValue" + SearchID].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            JType = Convert.ToInt32(Session["JType" + SearchID].ToString());
            DateTime arrDateTemp = new DateTime();
            if (JType != 1)
            {
                arrDateTemp = DateTime.Parse(Session["arrDateTextValue" + SearchID].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            }
            else
            {
                arrDateTemp = DateTime.Parse(Session["depDateTextValue" + SearchID].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
            }
            depDateTextValue = depDateTemp.ToString("MMM dd , yyyy");
            arrDateTextValue = arrDateTemp.ToString("MMM dd , yyyy");
            depDateValue = depDateTemp.ToString("MM/dd/yyyy");
            arrDateValue = arrDateTemp.ToString("MM/dd/yyyy");
            JType = Convert.ToInt32(Session["JType" + SearchID].ToString());
            AdultsCnt = Convert.ToInt32(Session["AdultsCnt" + SearchID].ToString());
            ChildsCnt = Convert.ToInt32(Session["ChildsCnt" + SearchID].ToString());
            InfantsCnt = Convert.ToInt32(Session["InfantsCnt" + SearchID].ToString());
            SearchType = Session["SearchType" + SearchID].ToString();
            totPax = AdultsCnt + ChildsCnt + InfantsCnt;
            string asdf = Session["Class" + SearchID].ToString();
            Class = Convert.ToInt32(Session["Class" + SearchID].ToString());
            Aircode = Session["Aircode" + SearchID].ToString();
            if (Aircode == "")
                Aircode = "ALL";

            if (Class != 0)
            {
                Class = Class - 1;
            }
            if (Class == 0)
            {
                ClassText = "Economy Class";
            }
            else if (Class == 1)
            {
                ClassText = "Business Class";
            }
            else if (Class == 2)
            {
                ClassText = "First Class";
            }
            //var image = "url(../images/pax_Back/" + Destination + ".jpg)";
            //BackImgcity.Style.Add("background-image", image);

            string path = Server.MapPath("~/images/pax_back");
            string path1 = "";
            if (File.Exists(path + path1 + "\\" + Destination + ".jpg"))
            {
                var image = "url(../images/pax_back/" + Destination + ".jpg)";
                BackImgcity.Style.Add("background-image", image);
            }
            else
            {
                var image = "url()";
                BackImgcity.Style.Add("background-image", image);
            }

            DirectFlight = Session["DirectFlight" + SearchID].ToString();

            string searchcriteria = "<div class='result-title'>";
            searchcriteria += "" + getAirportDetails(Departure) + " ";
            searchcriteria += "<i class='fa fa-arrow-circle-right'></i>";
            searchcriteria += " " + getAirportDetails(Destination);
            searchcriteria += "</div>";
            searchcriteria += "<div class='result-desc'>";
            searchcriteria += "<time class='comment-date' datetime='" + depDateValue + "'><i class='fa fa-clock-o'></i>" + depDateTextValue + "</time>";
            if (JType == 2)
            {
                searchcriteria += " <i class='fa fa-arrow-circle-right'></i>";

                searchcriteria += "<time class='comment-date padlft-rgt' datetime='" + arrDateValue + "'><i class='fa fa-clock-o'></i> " + arrDateTextValue + "</time>";
            }

            string travelers = "";
            if (AdultsCnt > 0 && AdultsCnt < 2)
                travelers += AdultsCnt + " Adult";
            else if (AdultsCnt > 1)
            {
                travelers += AdultsCnt + " Adults";
            }

            if (ChildsCnt > 0 && ChildsCnt < 2)
            {
                travelers += ", " + ChildsCnt + " Child";
            }
            else if (ChildsCnt > 1)
            {
                travelers += ", " + ChildsCnt + " Children";
            }

            if (InfantsCnt > 0 && InfantsCnt < 2)
                travelers += ", " + InfantsCnt + " Infant";
            else if (InfantsCnt > 1)
            {
                travelers += ", " + InfantsCnt + " Infants";
            }

            searchcriteria += "<span><i class='fa fa-user'></i>  Travelers: " + travelers + "</span>";
            searchcriteria += "</div>";

            divsearchcriteria.InnerHtml = searchcriteria;

            if (JType == 2)
            {
                modalpopup.InnerHtml = "<p>We will alert you when price goes down for your trip from </p>" +
                            "<span>" + getAirportCity(Departure) + " (" + Departure + ")</span>  <span class='pcTo'><i class='fa fa-angle-right'></i></span><span>" + getAirportCity(Destination) + " (" + Destination + ")</span>";
                modalpopup1.InnerHtml = "Even better deals from " + getAirportCity(Departure) + " to " + getAirportCity(Destination) + " may be available. Call our Customer Support to find out more";
                //modalpopup.InnerHtml =  "<div class='loadingFlights-cell'>" +
                //                            "<h4>" + getAirportCity(Departure) + " (" + Departure + ")</h4>" +
                //                            "<p>" + depDateTextValue + "</p>" +
                //                        "</div>" +
                //                        "<div class='loadingFlights-to'>" +
                //                            "<img src='Design/images/roundtrip-grey.png' alt='Roundtrip icon'>" +
                //                            "</div>" +
                //                        "<div class='loadingFlights-cell'>" +
                //                            "<h4>" + getAirportCity(Destination) + " (" + Destination + ")</h4>" +
                //                            "<p>" + arrDateTextValue + "</p>" +
                //                        "</div>";
            }
            else
            {
                modalpopup.InnerHtml = "<p>We will alert you when price goes down for your trip from </p>" +
                            "<span>" + getAirportCity(Departure) + " (" + Departure + ")</span>  <span class='pcTo'><i class='fa fa-angle-right'></i></span><span>" + getAirportCity(Destination) + " (" + Destination + ")</span>";
                modalpopup1.InnerHtml = "Even better deals from " + getAirportCity(Departure) + " to " + getAirportCity(Destination) + " may be available. Call our Customer Support to find out more";
                //modalpopup.InnerHtml = "<div class='loadingFlights-cell'>" +
                //                            "<h4>" + getAirportDetails(Departure) + " (" + Departure + ")</h4>" +
                //                        "</div>" +
                //                        "<div class='loadingFlights-to'>" +
                //                            "<img src='Design/images/onewaytrip-grey.png' alt='Roundtrip icon'>" +
                //                            "<p>" + depDateTextValue + "</p>" +
                //                        "</div>" +
                //                        "<div class='loadingFlights-cell'>" +
                //                            "<h4>" + getAirportDetails(Destination) + " (" + Destination + ")</h4>" +
                //                        "</div>";
            }
            //if (qstring[7].split('=')[1] == "2")
            //{
            //    modalpopup.innerHTML = '<div class="loadingFlights-cell">' +
            //        '<h4>' + qstring[24].split('=')[1].replace(/% 20 / g, " ") + '</h4>' +
            //        '<p>' + s + '</p>' +
            //        '</div>' +
            //        '<div class="loadingFlights-to"><img src="Design/images/roundtrip-grey.png" alt="Roundtrip icon"></div> ' +
            //        '<div class="loadingFlights-cell">' +
            //        '<h4>' + qstring[25].split('=')[1].replace(/% 20 / g, " ") + '</h4>' +
            //        '<p>' + s1 + '</p>' +
            //        '</div>';
            //}
            //else
            //{
            //    modalpopup.innerHTML = '<div class="loadingFlights-cell">' +
            //        '<h4>' + qstring[24].split('=')[1].replace(/% 20 / g, " ") + '</h4>' +
            //        '</div>' +
            //        '<div class="loadingFlights-to">' +
            //        '<img src="Design/images/onewaytrip-grey.png" alt="oneway icon">' +
            //        '<p class="loading-oneway">' + s + '</p>' +
            //        '</div> ' +
            //        '<div class="loadingFlights-cell">' +
            //        '<h4>' + qstring[25].split('=')[1].replace(/% 20 / g, " ") + '</h4>' +
            //        '</div>';
            //}

            searchcriteria = "<ul>" +
                                "<li class='deskHide'>" +
                                    "<div class='filterIcon'>" +
                                        "<button type='button' id='mbOpenFilter'><i class='fa fa-filter'></i></button>" +
                                    "</div>" +
                                "</li>" +
                                "<li class='deskHide'>" +
                                    "<div class='fromTo-type'>" +
                                        "<h5><span>" + getAirportDetails(Departure).Split(',')[0] + "</span> to <span>" + getAirportDetails(Destination).Split(',')[0] + "</span></h5>" +
                                        "<p>" + depDateTextValue;
            if (JType == 2)
                searchcriteria += " - " + arrDateTextValue;

            searchcriteria += "</p></div>" +
                                "</li>" +
                                "<li class='deskHide'>" +
                                    "<div class='Changesearch-btn'>" +
                                        "<button type='button' id='mbOpen'><i class='fa fa-edit'></i></button>" +
                                    "</div>" +
                                "</li>" +
                            "</ul>";
            searchcriteria += "<div class='cs-mainwrap csbox'><a href='#' class='close csr-close cs-toggle' onclick='csToggle()' id='closeChSearch'><span aria-hidden='true'>×</span><span class='sr-only'>Close</span></a>" +
                                    "<form>" +
                                        "<div class='tab-content'>" +
                                            "<div role='tabpanel' class='tab-pane active'>" +
                                                "<div class='from' id='from'>" +
                                                    "<input type='text' name='Departure From' value='" + getAirportDetails(Departure) + "(" + Departure + ")" + "' class='dpIcon' required id='Departure' onfocus='this.select();' placeholder='Enter city or airport'>" +
                                                    "<div class='switch-places' id='switchplaces'></div>" +
                                                "</div>" +
                                                "<div class='to' id='to'>" +
                                                    "<input type='text' name='Destination' value='" + getAirportDetails(Destination) + "(" + Destination + ")" + "' class='ArvIcon' required id='Destination' onfocus='this.select();' placeholder='Enter city or airport'>" +
                                                "</div>" +
                                                "<div class='deprtDate' id='depDate'>" +
                                                    "<input type='text' name='DepartureDate' value='" + depDateValue + "' class='dpDateIcon' required id='DepartureDate' readonly>" +
                                                "</div>";
            if (JType == 2)
            {

                searchcriteria += "<div class='arrivDate' id='retDate'>" +
                                                        "<input type='text' name='ReturnDate'  value='" + arrDateValue + "' class='ArvDateIcon' required id='ReturnDate' readonly>" +
                                                    "</div>";
            }
            else
            {
                searchcriteria += "<div class='arrivDate' id='retDate'>" +
                                                    "<input type='text' name='ReturnDate'  value='' class='ArvDateIcon' required id='ReturnDate' placeholder='Return' readonly disabled>" +
                                                "</div>";
            }

            searchcriteria += "<div class='travellers' id='traveler'>" +
                                                    "<input type='text' class='travellerIcon' name='Travellers' id='traveller-bx' required placeholder='1 Person, Economy' readonly >" +
                                                    "<div class='options-popup traveler-pop' id='travelerpop'>" +
                                                        "<div class='options-popup-header'>" +
                                                            "<span>Travellers</span>" +
                                                            "<span class='options-popup-close'><i class='fa fa-times'></i></span>" +
                                                        "</div>" +
                                                        "<div class='passengers'>" +
                                                            "<div class='row'>" +
                                                                "<div class='buttons'>" +
                                                                    "<div id='minAdult' class='minus' onclick='ChildDecrease(this)' tabindex='-1'></div>" +
                                                                    "<div id='countAdult' class='count ps-list' title='Adults'>" + AdultsCnt + "</div>" +
                                                                    "<div id='plAdult' class='plus' onclick='ChildIncrease(this)' tabindex='-1'></div>" +
                                                                "</div>" +
                                                                "<div class='description'>" +
                                                                    "<div class='name'>Adults</div>" +
                                                                    "<div class='age'>(11+ Years)</div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                            "<div class='row'>" +
                                                                "<div class='buttons'>" +
                                                                    "<div id='minChild' class='minus' onclick='ChildDecrease(this)' tabindex='-1'></div>" +
                                                                    "<div id='countChild' class='count'>" + ChildsCnt + "</div>" +
                                                                    "<div id='plChild' class='plus' onclick='ChildIncrease(this)' tabindex='-1'></div>" +
                                                                "</div>" +
                                                                "<div class='description'>" +
                                                                    "<div class='name'>Children</div>" +
                                                                    "<div class='age'>(2-11 Years)</div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                            "<div class='row'>" +
                                                                "<div class='buttons'>" +
                                                                    "<div id='minInfant' class='minus' onclick='ChildDecrease(this)' tabindex='-1'></div>" +
                                                                    "<div id='countInfant' class='count'>" + InfantsCnt + "</div>" +
                                                                    "<div id='plInfant' class='plus' onclick='ChildIncrease(this)' tabindex='-1' data-metric='options infants-plus'></div>" +
                                                                "</div>" +
                                                                "<div class='description'>" +
                                                                    "<div class='name'>Infant in lap</div>" +
                                                                    "<div class='age'>(0-2 Years)</div>" +
                                                                "</div>" +
                                                            "</div>" +
                                                            "<div class='row' id='error' style='display: none'>" +
                                                            "</div>" +
                                                        "</div>" +
                                                        "<div class='trip-class'>" +
                                                            "<span>Flight Class</span>" +
                                                            "<div class='radio' tabindex='-1' data-metric='options business-type'>" +
                                                                "<input checked='checked' name='ClassType' onchange='class1change(this)' id='Economy' class='radio-custom' onclick='' style='margin: 0px' type='radio'>" +
                                                                "<label class='radio-custom-label' for='Economy'>Economy</label>" +
                                                            "</div>" +
                                                            "<div class='radio' tabindex='-1' data-metric='options business-type'>" +
                                                                "<input class='radio-custom' id='Business' onchange='class1change(this)' name='ClassType' style='margin: 0px' type='radio'>" +
                                                                "<label class='radio-custom-label' for='Business'>Business</label>" +
                                                            "</div>" +
                                                            "<div class='radio' tabindex='-1' data-metric='options business-type'>" +
                                                                "<input class='radio-custom' id='First' onchange='class1change(this)' name='ClassType' style='margin: 0px' type='radio'>" +
                                                                "<label class='radio-custom-label' for='First'>First</label>" +
                                                            "</div>" +
                                                        "</div>" +
                                                        "<div class='row'>" +
                                                            "<div class='col-md-12 text-center'>" +
                                                                "<button type='button' class='btn doneBtn options-popup-close mb-10' id='optionspopupclose1'>Done</button>" +
                                                            "</div>" +
                                                        "</div>" +
                                                    "</div>" +
                                                "</div>" +
                                                "<div class='csBtn-wrap'>" +
                                                    "<button class='btn csBtn' type='submit' id='searchbtn' onclick='return validate(this)'>" +
                                                        "Search Flights" +
                                                        "<i class='fa fa-plane'></i>" +
                                                    "</button>" +
                                                "</div>" +
                                                "<div class='csBtn-add-wrap' id='toggle'>" +
                                                    "<a href='' class='btn add-expBtn' ><i class='fa fa-plus'></i></a>" +
                                                    "<span class='ch-show'>Show more options</span>" +
                                                "</div>" +
                                                "<div class='advoption'>" +
                                        "<div id='AdvancedOptions' style='display: none;'>" +
                                        "<div id='oneroundtrip' class='oneroundtrip' onclick='ChangeRound()'>" +
                                            "<span>";
            if (JType == 2)
            {
                searchcriteria += "<input id='RoundTrip' value='Roundtrip' class='radio-custom' checked='checked' name='Triptype' type='radio'>";
            }
            else
            {
                searchcriteria += "<input id='RoundTrip' value='Roundtrip' class='radio-custom' name='Triptype' type='radio'>";

            }
            searchcriteria += "<label for='RoundTrip' class='radio-custom-label'> Round Trip</label>" +
                                            "</span>" +
                                            "<span>";
            if (JType != 2)
            {
                searchcriteria += "<input id='Oneway' class='radio-custom' value='Oneway' checked='checked' name='Triptype' type='radio'>";
            }
            else
            {
                searchcriteria += "<input id='Oneway' class='radio-custom' value='Oneway' name='Triptype' type='radio'>";
            }
            searchcriteria += "<label for='Oneway' class='radio-custom-label'> One way</label>" +
                                            "</span>" +
                                            "<span>" +
                                            "<input id='directFlights' class='checkbox-custom' name='directFlights' type='checkbox'>" +
                                            "<label for='directFlights' class='checkbox-custom-label'> Direct flights only</label>" +
                                        "</span>" +
                                        "</div>" +
                                            "<div class='airlines'>" +
                                                "<label>Airlines:</label>" +
                                                "<div class='selectbx'>" +
                                                    "<select class='' name='AirlinesFlexibility' id='Airlines' data-msg-required='Please Select the Airline' data-rule-required='true'>" +
                                                        "<option value='ALL'>Any Airline</option>" +
                                                        "<option value='I8'>Aboriginal Air</option>" +
                                                        "<option value='9B'>AccesRail</option>" +
                                                        "<option value='ZY'>Ada Air</option>" +
                                                        "<option value='JP'>Adria Airways</option>" +
                                                        "<option value='DF'>Aebal</option>" +
                                                        "<option value='A3'>Aegean Air</option>" +
                                                        "<option value='RE'>Aer Arann</option>" +
                                                        "<option value='EI'>Aer Lingus</option>" +
                                                        "<option value='EE'>Aero Airlines</option>" +
                                                        "<option value='E4'>Aero Asia</option>" +
                                                        "<option value='EM'>Aero Benin</option>" +
                                                        "<option value='JR'>Aero California</option>" +
                                                        "<option value='QA'>Aero Caribe</option>" +
                                                        "<option value='P4'>Aero Lineas Sosa</option>" +
                                                        "<option value='M0'>Aero Mongolia</option>" +
                                                        "<option value='DW'>Aero-Charter</option>" +
                                                        "<option value='AJ'>Aerocontractors</option>" +
                                                        "<option value='SU'>Aeroflot</option>" +
                                                        "<option value='2K'>Aerogal Aerolina</option>" +
                                                        "<option value='KG'>Aerogaviota</option>" +
                                                        "<option value='AR'>Aerolineas Argen</option>" +
                                                        "<option value='VW'>Aeromar</option>" +
                                                        "<option value='BQ'>Aeromar Air</option>" +
                                                        "<option value='AM'>Aeromexico</option>" +
                                                        "<option value='5D'>Aeromexico</option>" +
                                                        "<option value='OT'>Aeropelican</option>" +
                                                        "<option value='VH'>Aeropostal</option>" +
                                                        "<option value='P5'>AeroRepublica</option>" +
                                                        "<option value='5L'>Aerosur</option>" +
                                                        "<option value='VV'>Aerosvit</option>" +
                                                        "<option value='6I'>AeroSyncro</option>" +
                                                        "<option value='XU'>African Express</option>" +
                                                        "<option value='ML'>African Transprt</option>" +
                                                        "<option value='8U'>Afriqiyah</option>" +
                                                        "<option value='X5'>Afrique Airlines</option>" +
                                                        "<option value='ZI'>Aigle Azur</option>" +
                                                        "<option value='AH'>Air Algerie</option>" +
                                                        "<option value='A6'>Air Alps</option>" +
                                                        "<option value='2Y'>Air Andaman</option>" +
                                                        "<option value='G9'>Air Arabia</option>" +
                                                        "<option value='KC'>Air Astana</option>" +
                                                        "<option value='UU'>Air Austral</option>" +
                                                        "<option value='W9'>Air Bagan</option>" +
                                                        "<option value='BT'>Air Baltic</option>" +
                                                        "<option value='AB'>Air Berlin</option>" +
                                                        "<option value='YL'>Air Bissau</option>" +
                                                        "<option value='BP'>Air Botswana</option>" +
                                                        "<option value='2J'>Air Burkina</option>" +
                                                        "<option value='TY'>Air Caledonie</option>" +
                                                        "<option value='SB'>Air Calin</option>" +
                                                        "<option value='AC'>Air Canada</option>" +
                                                        "<option value='QK'>Air Canada Jazz</option>" +
                                                        "<option value='TX'>Air Caraibes</option>" +
                                                        "<option value='NV'>Air Central</option>" +
                                                        "<option value='CV'>Air Chatham</option>" +
                                                        "<option value='CA'>Air China</option>" +
                                                        "<option value='4F'>Air City</option>" +
                                                        "<option value='A7'>Air Comet</option>" +
                                                        "<option value='DV'>Air Company SCAT</option>" +
                                                        "<option value='QC'>Air Corridor</option>" +
                                                        "<option value='YN'>Air Creebec</option>" +
                                                        "<option value='DN'>Air Deccan</option>" +
                                                        "<option value='EN'>Air Dolomiti</option>" +
                                                        "<option value='UX'>Air Europa</option>" +
                                                        "<option value='PE'>Air Europe</option>" +
                                                        "<option value='PC'>Air Fiji</option>" +
                                                        "<option value='OF'>Air Finland</option>" +
                                                        "<option value='AF'>Air France</option>" +
                                                        "<option value='GL'>Air Greenland</option>" +
                                                        "<option value='LQ'>Air Guinea</option>" +
                                                        "<option value='3S'>Air Guyane</option>" +
                                                        "<option value='NY'>Air Iceland</option>" +
                                                        "<option value='AI'>Air India</option>" +
                                                        "<option value='IX'>Air India Exp</option>" +
                                                        "<option value='3H'>Air Inuit</option>" +
                                                        "<option value='I9'>Air Italy</option>" +
                                                        "<option value='VU'>Air Ivoire</option>" +
                                                        "<option value='JM'>Air Jamaica</option>" +
                                                        "<option value='NQ'>Air Japan</option>" +
                                                        "<option value='JS'>Air Koryo</option>" +
                                                        "<option value='L4'>Air Liaison</option>" +
                                                        "<option value='DR'>Air Link</option>" +
                                                        "<option value='NX'>Air Macau</option>" +
                                                        "<option value='MD'>Air Madagascar</option>" +
                                                        "<option value='QM'>Air Malawi</option>" +
                                                        "<option value='KM'>Air Malta</option>" +
                                                        "<option value='6T'>Air Mandalay</option>" +
                                                        "<option value='MK'>Air Mauritius</option>" +
                                                        "<option value='ZV'>Air Midwest</option>" +
                                                        "<option value='9U'>Air Moldova</option>" +
                                                        "<option value='SW'>Air Namibia</option>" +
                                                        "<option value='XN'>Air Nepal</option>" +
                                                        "<option value='NZ'>Air New Zealand</option>" +
                                                        "<option value='7A'>Air Next</option>" +
                                                        "<option value='EH'>Air Nippon Netwk</option>" +
                                                        "<option value='PX'>Air Niugini</option>" +
                                                        "<option value='4N'>Air North</option>" +
                                                        "<option value='TL'>Air North Region</option>" +
                                                        "<option value='YW'>Air Nostrum</option>" +
                                                        "<option value='AP'>Air One</option>" +
                                                        "<option value='FJ'>Air Pacific</option>" +
                                                        "<option value='TP'>Air Portugal</option>" +
                                                        "<option value='GZ'>Air Rarotonga</option>" +
                                                        "<option value='PJ'>Air Saint-Pierre</option>" +
                                                        "<option value='V7'>Air Senegal</option>" +
                                                        "<option value='X7'>Air Service</option>" +
                                                        "<option value='HM'>Air Seychelles</option>" +
                                                        "<option value='4D'>Air Sinai</option>" +
                                                        "<option value='GM'>Air Slovakia</option>" +
                                                        "<option value='SZ'>Air Southwest</option>" +
                                                        "<option value='ZP'>Air St Thomas</option>" +
                                                        "<option value='YI'>Air Sunshine</option>" +
                                                        "<option value='VT'>Air Tahiti</option>" +
                                                        "<option value='TN'>Air Tahiti Nui</option>" +
                                                        "<option value='TC'>Air Tanzania</option>" +
                                                        "<option value='8T'>Air Tindi</option>" +
                                                        "<option value='YT'>Air Togo</option>" +
                                                        "<option value='TS'>Air Transat</option>" +
                                                        "<option value='JY'>Air Turks Caicos</option>" +
                                                        "<option value='U7'>Air Uganda</option>" +
                                                        "<option value='DO'>Air Vallee</option>" +
                                                        "<option value='NF'>Air Vanuatu</option>" +
                                                        "<option value='ZW'>Air Wisconsin</option>" +
                                                        "<option value='UM'>Air Zimbabwe</option>" +
                                                        "<option value='AK'>AirAsia</option>" +
                                                        "<option value='D7'>AirAsia X</option>" +
                                                        "<option value='BM'>Airbee</option>" +
                                                        "<option value='YQ'>Aircompany Polet</option>" +
                                                        "<option value='4C'>Aires</option>" +
                                                        "<option value='P2'>AirKenya Express</option>" +
                                                        "<option value='A5'>Airlinair</option>" +
                                                        "<option value='FL'>Airtran Air</option>" +
                                                        "<option value='6L'>Aklak Air</option>" +
                                                        "<option value='AS'>Alaska Airlines</option>" +
                                                        "<option value='KO'>Alaska Central</option>" +
                                                        "<option value='J5'>Alaska Seaplane</option>" +
                                                        "<option value='LV'>Albanian Air</option>" +
                                                        "<option value='ZR'>Alexandria Air</option>" +
                                                        "<option value='D4'>Alidaunia</option>" +
                                                        "<option value='AZ'>Alitalia</option>" +
                                                        "<option value='XM'>Alitalia Express</option>" +
                                                        "<option value='YY'>All Airlines</option>" +
                                                        "<option value='NH'>All Nippon</option>" +
                                                        "<option value='G4'>Allegiant Air</option>" +
                                                        "<option value='CD'>Alliance Air</option>" +
                                                        "<option value='QQ'>Alliance Airline</option>" +
                                                        "<option value='3A'>Alliance Chicago</option>" +
                                                        "<option value='YJ'>AMC Airlines</option>" +
                                                        "<option value='AA'>American</option>" +
                                                        "<option value='AX'>American Connexn</option>" +
                                                        "<option value='MQ'>American Eagle</option>" +
                                                        "<option value='2V'>Amtrak</option>" +
                                                        "<option value='OY'>Andes Lineas</option>" +
                                                        "<option value='G6'>Angkor Airways</option>" +
                                                        "<option value='O4'>Antrak Air</option>" +
                                                        "<option value='7P'>APA Intl Air</option>" +
                                                        "<option value='5F'>Arctic Circle</option>" +
                                                        "<option value='FG'>Ariana Afghan</option>" +
                                                        "<option value='W3'>Arik Air</option>" +
                                                        "<option value='5N'>Arkhangelsk Airl</option>" +
                                                        "<option value='IZ'>Arkia Israeli</option>" +
                                                        "<option value='U8'>Armavia Airline</option>" +
                                                        "<option value='MV'>Armenian Intl</option>" +
                                                        "<option value='7S'>Artic</option>" +
                                                        "<option value='R7'>Aserca Air</option>" +
                                                        "<option value='6K'>Asian Spirit</option>" +
                                                        "<option value='OZ'>Asiana Airlines</option>" +
                                                        "<option value='ZA'>Astair</option>" +
                                                        "<option value='5W'>Astraeus</option>" +
                                                        "<option value='RC'>Atlantic Faroe</option>" +
                                                        "<option value='EV'>Atlantic SE</option>" +
                                                        "<option value='TD'>Atlantis Europea</option>" +
                                                        "<option value='8A'>Atlas Blue</option>" +
                                                        "<option value='KK'>Atlas Jet Intl</option>" +
                                                        "<option value='IP'>Atyrau Airways</option>" +
                                                        "<option value='IQ'>Augsburg Airways</option>" +
                                                        "<option value='GR'>Aurigny Air Svc</option>" +
                                                        "<option value='AU'>Austral Lineas</option>" +
                                                        "<option value='OS'>Austrian Airline</option>" +
                                                        "<option value='AV'>Avianca</option>" +
                                                        "<option value='WR'>Aviaprad</option>" +
                                                        "<option value='GU'>Aviateca</option>" +
                                                        "<option value='U3'>Avies LTD</option>" +
                                                        "<option value='M4'>Aviompex</option>" +
                                                        "<option value='9V'>Avior Airlines</option>" +
                                                        "<option value='2Q'>Avitrans Nordi</option>" +
                                                        "<option value='J2'>Azerbaijan Airl</option>" +
                                                        "<option value='JA'>B and H Airlines</option>" +
                                                        "<option value='CJ'>BA City Flyer</option>" +
                                                        "<option value='UP'>Bahamasair</option>" +
                                                        "<option value='2B'>Bahrain Air</option>" +
                                                        "<option value='PG'>Bangkok Airways</option>" +
                                                        "<option value='V9'>Bashkir Airlines</option>" +
                                                        "<option value='JV'>Bearskin Airline</option>" +
                                                        "<option value='4T'>Belair</option>" +
                                                        "<option value='B2'>Belavia</option>" +
                                                        "<option value='LZ'>Belle Air</option>" +
                                                        "<option value='B3'>Bellview</option>" +
                                                        "<option value='O3'>Bellview Airline</option>" +
                                                        "<option value='CH'>Bemidji</option>" +
                                                        "<option value='A8'>Benin Golf Air</option>" +
                                                        "<option value='8E'>Bering Air</option>" +
                                                        "<option value='J8'>Berjaya Air</option>" +
                                                        "<option value='5Q'>Best Air</option>" +
                                                        "<option value='BH'>BH Airlines</option>" +
                                                        "<option value='BG'>Biman Bangladesh</option>" +
                                                        "<option value='NT'>Binter Canarias</option>" +
                                                        "<option value='4V'>Birdy Airlines</option>" +
                                                        "<option value='5Z'>Bismillah Air</option>" +
                                                        "<option value='BV'>Blue Panorama</option>" +
                                                        "<option value='QW'>Blue Wings</option>" +
                                                        "<option value='KF'>Blue1</option>" +
                                                        "<option value='BD'>Bmi British</option>" +
                                                        "<option value='WW'>Bmibaby</option>" +
                                                        "<option value='Z2'>Bravo Air Congo</option>" +
                                                        "<option value='FQ'>Brindabella Air</option>" +
                                                        "<option value='DB'>Brit Air</option>" +
                                                        "<option value='BA'>British Airways</option>" +
                                                        "<option value='TH'>British Citiexpr</option>" +
                                                        "<option value='BS'>British Intl</option>" +
                                                        "<option value='SN'>Brussels Airline</option>" +
                                                        "<option value='FB'>Bulgaria Air</option>" +
                                                        "<option value='UZ'>Buraq Air</option>" +
                                                        "<option value='XD'>Bus</option>" +
                                                        "<option value='MO'>Calm Air Intl</option>" +
                                                        "<option value='R9'>Camai Air</option>" +
                                                        "<option value='9O'>Cameroon Air</option>" +
                                                        "<option value='5T'>Canadian North</option>" +
                                                        "<option value='C6'>Canjet</option>" +
                                                        "<option value='9K'>Cape Air</option>" +
                                                        "<option value='6C'>Cape Smythe Air</option>" +
                                                        "<option value='3Q'>Carib Aviation</option>" +
                                                        "<option value='BW'>Caribbean Air</option>" +
                                                        "<option value='V3'>Carpatair</option>" +
                                                        "<option value='C4'>Carriacou</option>" +
                                                        "<option value='XP'>Casino Express</option>" +
                                                        "<option value='RV'>Caspian Airlines</option>" +
                                                        "<option value='CX'>Cathay Pacific</option>" +
                                                        "<option value='KX'>Cayman Airways</option>" +
                                                        "<option value='XK'>CCM Airlines</option>" +
                                                        "<option value='5J'>Cebu Pacific Air</option>" +
                                                        "<option value='C2'>CEIBA Intercon</option>" +
                                                        "<option value='9M'>Central Mtn Air</option>" +
                                                        "<option value='C0'>Centralwings</option>" +
                                                        "<option value='J7'>Centre-Avia</option>" +
                                                        "<option value='OP'>Chalk's Airlines</option>" +
                                                        "<option value='5U'>Challenge Aero</option>" +
                                                        "<option value='XI'>Charter</option>" +
                                                        "<option value='RP'>Chautauqua</option>" +
                                                        "<option value='CI'>China Airlines</option>" +
                                                        "<option value='MU'>China Eastern</option>" +
                                                        "<option value='G5'>China Express</option>" +
                                                        "<option value='CZ'>China Southern</option>" +
                                                        "<option value='PN'>China West Air</option>" +
                                                        "<option value='OQ'>Chongqing Air</option>" +
                                                        "<option value='QI'>Cimber Sterling</option>" +
                                                        "<option value='C9'>Cirrus Airlines</option>" +
                                                        "<option value='CF'>City Airline</option>" +
                                                        "<option value='WX'>City Jet</option>" +
                                                        "<option value='XG'>Clickair</option>" +
                                                        "<option value='6P'>Clubair Sixgo</option>" +
                                                        "<option value='9L'>Colgan Air</option>" +
                                                        "<option value='OH'>Comair Inc.</option>" +
                                                        "<option value='C5'>CommutAir</option>" +
                                                        "<option value='KR'>Comores Aviation</option>" +
                                                        "<option value='I5'>Compagnie Mali</option>" +
                                                        "<option value='CP'>Compass Airlines</option>" +
                                                        "<option value='DE'>Condor</option>" +
                                                        "<option value='C3'>Contact Air</option>" +
                                                        "<option value='CO'>Continental</option>" +
                                                        "<option value='CM'>Copa Airlines</option>" +
                                                        "<option value='3C'>Corp Express</option>" +
                                                        "<option value='SS'>Corsair</option>" +
                                                        "<option value='DQ'>Costal Air</option>" +
                                                        "<option value='OU'>Croatia Airlines</option>" +
                                                        "<option value='QE'>Crossair Europe</option>" +
                                                        "<option value='CU'>Cubana Airlines</option>" +
                                                        "<option value='CY'>Cyprus Airways</option>" +
                                                        "<option value='OK'>Czech Airlines</option>" +
                                                        "<option value='D3'>Daallo Airlines</option>" +
                                                        "<option value='N2'>Dagestan Air</option>" +
                                                        "<option value='DX'>Danish Air</option>" +
                                                        "<option value='D5'>Dauair</option>" +
                                                        "<option value='JD'>Deer Air</option>" +
                                                        "<option value='DL'>Delta</option>" +
                                                        "<option value='3D'>Denim Air</option>" +
                                                        "<option value='2A'>Deutsche Bahn</option>" +
                                                        "<option value='ES'>DHL Intl</option>" +
                                                        "<option value='D8'>Djibouti Airl</option>" +
                                                        "<option value='Z6'>Dnieproavia</option>" +
                                                        "<option value='7D'>Donbassaero</option>" +
                                                        "<option value='R6'>DOT LT</option>" +
                                                        "<option value='KA'>Dragonair</option>" +
                                                        "<option value='Y3'>Driessen Svcs</option>" +
                                                        "<option value='KB'>Druk Air</option>" +
                                                        "<option value='9H'>Dutch Antilles</option>" +
                                                        "<option value='H7'>Eagle Uganda</option>" +
                                                        "<option value='3E'>East Asia</option>" +
                                                        "<option value='T3'>Eastern Airways</option>" +
                                                        "<option value='U2'>Easyjet Airline</option>" +
                                                        "<option value='EU'>Ecuatoriana</option>" +
                                                        "<option value='WK'>Edelweiss Air</option>" +
                                                        "<option value='MS'>Egyptair</option>" +
                                                        "<option value='LY'>El Al Israel</option>" +
                                                        "<option value='A0'>Elysair</option>" +
                                                        "<option value='EK'>Emirates Air</option>" +
                                                        "<option value='7H'>Era Aviation</option>" +
                                                        "<option value='YE'>Eram Airlines</option>" +
                                                        "<option value='B8'>Eritrean Airline</option>" +
                                                        "<option value='K9'>Esen Air</option>" +
                                                        "<option value='OV'>Estonian Air</option>" +
                                                        "<option value='ET'>Ethiopian Air</option>" +
                                                        "<option value='EY'>Etihad Airways</option>" +
                                                        "<option value='5B'>Euro- Asia Air</option>" +
                                                        "<option value='UI'>Eurocypria Air</option>" +
                                                        "<option value='GJ'>Eurofly</option>" +
                                                        "<option value='K2'>Eurolot S.A.</option>" +
                                                        "<option value='5O'>Europe Airpost</option>" +
                                                        "<option value='QY'>European Air</option>" +
                                                        "<option value='EA'>European Express</option>" +
                                                        "<option value='9F'>Eurostar</option>" +
                                                        "<option value='EW'>Eurowings</option>" +
                                                        "<option value='BR'>EVA Airways</option>" +
                                                        "<option value='3Z'>Everts Air</option>" +
                                                        "<option value='JN'>Excel Airways</option>" +
                                                        "<option value='OW'>Executive Air</option>" +
                                                        "<option value='XE'>Express Jet</option>" +
                                                        "<option value='IH'>Falcon Air</option>" +
                                                        "<option value='EF'>Far Eastern Air</option>" +
                                                        "<option value='AY'>Finnair</option>" +
                                                        "<option value='FC'>Finncomm Air</option>" +
                                                        "<option value='FY'>Firefly</option>" +
                                                        "<option value='7F'>First Air</option>" +
                                                        "<option value='DP'>First Choice Air</option>" +
                                                        "<option value='5H'>Five Fourty</option>" +
                                                        "<option value='RF'>Florida West</option>" +
                                                        "<option value='E7'>Fly European</option>" +
                                                        "<option value='F3'>Fly Excellent</option>" +
                                                        "<option value='F7'>Flybaboo</option>" +
                                                        "<option value='BE'>Flybe</option>" +
                                                        "<option value='7Y'>Flying Carpet</option>" +
                                                        "<option value='LF'>FlyNordic</option>" +
                                                        "<option value='Q5'>Forty Mile Air</option>" +
                                                        "<option value='FP'>Freedom Air Guam</option>" +
                                                        "<option value='F8'>Freedom Airlines</option>" +
                                                        "<option value='F9'>Frontier</option>" +
                                                        "<option value='2F'>Frontier Flying</option>" +
                                                        "<option value='FH'>Futura Intl Air</option>" +
                                                        "<option value='GY'>Gabon Airlines</option>" +
                                                        "<option value='GP'>Gadair</option>" +
                                                        "<option value='GA'>Garuda</option>" +
                                                        "<option value='4G'>Gazpromavia</option>" +
                                                        "<option value='GT'>GB Airways</option>" +
                                                        "<option value='A9'>Georgian Airways</option>" +
                                                        "<option value='QB'>Georgian Natl</option>" +
                                                        "<option value='4U'>Germanwings</option>" +
                                                        "<option value='G0'>Ghana Intl</option>" +
                                                        "<option value='Z5'>GMG Airlines</option>" +
                                                        "<option value='G3'>Gol Trans</option>" +
                                                        "<option value='DC'>Golden Air</option>" +
                                                        "<option value='CN'>Grand China Air</option>" +
                                                        "<option value='GS'>Grand China Exp</option>" +
                                                        "<option value='GV'>Grant Aviation</option>" +
                                                        "<option value='ZK'>Great Lakes</option>" +
                                                        "<option value='IJ'>GREAT WALL</option>" +
                                                        "<option value='3R'>Gromov Air</option>" +
                                                        "<option value='GF'>Gulf Air</option>" +
                                                        "<option value='6G'>Gulfstream</option>" +
                                                        "<option value='3M'>Gulfstream Intl</option>" +
                                                        "<option value='H6'>Hageland</option>" +
                                                        "<option value='HR'>Hahn Air</option>" +
                                                        "<option value='HU'>Hainan Airlines</option>" +
                                                        "<option value='7Z'>Halcyon Air</option>" +
                                                        "<option value='4R'>Hamburg Intl</option>" +
                                                        "<option value='HQ'>Harmony Airways</option>" +
                                                        "<option value='HA'>Hawaiian</option>" +
                                                        "<option value='HN'>Heavylift Cargo</option>" +
                                                        "<option value='H4'>Heli Securite</option>" +
                                                        "<option value='YO'>Heli-Air Monaco</option>" +
                                                        "<option value='JB'>Helijet Intl</option>" +
                                                        "<option value='L5'>Helikopter Svc</option>" +
                                                        "<option value='ZU'>Helios Airways</option>" +
                                                        "<option value='2L'>Helvetic Airways</option>" +
                                                        "<option value='EO'>Hewa Bora</option>" +
                                                        "<option value='UD'>Hex Air</option>" +
                                                        "<option value='8H'>Highland Airways</option>" +
                                                        "<option value='HD'>Hokkaido Intl</option>" +
                                                        "<option value='HB'>Homer Air</option>" +
                                                        "<option value='HX'>Hong Kong Air</option>" +
                                                        "<option value='UO'>Hong Kong Expres</option>" +
                                                        "<option value='QX'>Horizon Air</option>" +
                                                        "<option value='IB'>Iberia</option>" +
                                                        "<option value='FW'>IBEX Airlines</option>" +
                                                        "<option value='X8'>Icaro</option>" +
                                                        "<option value='FI'>Icelandair</option>" +
                                                        "<option value='V8'>Iliamna Air Taxi</option>" +
                                                        "<option value='IK'>Imair</option>" +
                                                        "<option value='DH'>Independence Air</option>" +
                                                        "<option value='IC'>Indian Airlines</option>" +
                                                        "<option value='6E'>Indigo Air</option>" +
                                                        "<option value='QZ'>Indonesia Air</option>" +
                                                        "<option value='7N'>Inland Aviation</option>" +
                                                        "<option value='7I'>Insel Air</option>" +
                                                        "<option value='D6'>Interair</option>" +
                                                        "<option value='IO'>Intercontinental</option>" +
                                                        "<option value='4O'>Interjet</option>" +
                                                        "<option value='ID'>Interlink Air</option>" +
                                                        "<option value='3L'>Intersky</option>" +
                                                        "<option value='IR'>Iran Air</option>" +
                                                        "<option value='EP'>Iran Asseman</option>" +
                                                        "<option value='IA'>Iraqi Airways</option>" +
                                                        "<option value='2O'>Island Air</option>" +
                                                        "<option value='WP'>Island Air Hawai</option>" +
                                                        "<option value='IS'>Island Air Nantk</option>" +
                                                        "<option value='HH'>Islandsflug</option>" +
                                                        "<option value='IF'>Islas Airways</option>" +
                                                        "<option value='WC'>Islena Airlines</option>" +
                                                        "<option value='6H'>Israir</option>" +
                                                        "<option value='FS'>Itali Airlines</option>" +
                                                        "<option value='GI'>Itek Air</option>" +
                                                        "<option value='I3'>Ivoirienne Trans</option>" +
                                                        "<option value='4I'>IzAir</option>" +
                                                        "<option value='JC'>JAL Express</option>" +
                                                        "<option value='JO'>JALways</option>" +
                                                        "<option value='JL'>Japan Airlines</option>" +
                                                        "<option value='3X'>Japan Commuter</option>" +
                                                        "<option value='NU'>Japan Trans Air</option>" +
                                                        "<option value='JU'>JAT Airways</option>" +
                                                        "<option value='VJ'>Jatayu Airlines</option>" +
                                                        "<option value='J9'>Jazeera Airways</option>" +
                                                        "<option value='O2'>Jet Air</option>" +
                                                        "<option value='9W'>Jet Airways</option>" +
                                                        "<option value='PP'>Jet Aviation</option>" +
                                                        "<option value='S2'>Jet Lite</option>" +
                                                        "<option value='LS'>Jet2.com</option>" +
                                                        "<option value='B6'>JetBlue</option>" +
                                                        "<option value='J0'>Jetlink Express</option>" +
                                                        "<option value='JQ'>Jetstar Airways</option>" +
                                                        "<option value='3K'>Jetstar Asia Air</option>" +
                                                        "<option value='BL'>Jetstar Pacific</option>" +
                                                        "<option value='GX'>JetX</option>" +
                                                        "<option value='LJ'>Jin Air</option>" +
                                                        "<option value='3B'>Job Air Central</option>" +
                                                        "<option value='6J'>Jubba Airways</option>" +
                                                        "<option value='HO'>Juneyao Airlines</option>" +
                                                        "<option value='XC'>K D Air</option>" +
                                                        "<option value='RQ'>Kam Air</option>" +
                                                        "<option value='KT'>Katmai Air</option>" +
                                                        "<option value='KV'>Kavminvodyavia</option>" +
                                                        "<option value='KD'>KD Avia</option>" +
                                                        "<option value='FK'>Keewatin Air</option>" +
                                                        "<option value='M5'>Kenmore Air</option>" +
                                                        "<option value='4K'>Kenn Borek Air</option>" +
                                                        "<option value='KQ'>Kenya Airways</option>" +
                                                        "<option value='BZ'>Keystone Air</option>" +
                                                        "<option value='YK'>Kibris Turkish</option>" +
                                                        "<option value='IT'>Kingfisher Air</option>" +
                                                        "<option value='Y9'>Kish Airlines</option>" +
                                                        "<option value='KP'>Kiwi Intl</option>" +
                                                        "<option value='KL'>KLM</option>" +
                                                        "<option value='WA'>KLM Cityhopper</option>" +
                                                        "<option value='KE'>Korean Air</option>" +
                                                        "<option value='7B'>Krasnoyarsk</option>" +
                                                        "<option value='K4'>Kronflyg</option>" +
                                                        "<option value='MN'>Kulula air</option>" +
                                                        "<option value='KU'>Kuwait Airways</option>" +
                                                        "<option value='QH'>Kyrgystan</option>" +
                                                        "<option value='JF'>L A B Flying</option>" +
                                                        "<option value='WJ'>Labrador</option>" +
                                                        "<option value='LR'>Lacsa</option>" +
                                                        "<option value='N7'>Lagun Air</option>" +
                                                        "<option value='TM'>LAM</option>" +
                                                        "<option value='LA'>Lan Airlines</option>" +
                                                        "<option value='4M'>Lan Argentina</option>" +
                                                        "<option value='UC'>Lan Chile Cargo</option>" +
                                                        "<option value='XL'>Lan Ecuador</option>" +
                                                        "<option value='LU'>Lan Express</option>" +
                                                        "<option value='LP'>Lan Peru</option>" +
                                                        "<option value='QV'>Lao Aviation</option>" +
                                                        "<option value='J6'>Larry's Flying</option>" +
                                                        "<option value='QL'>Laser Airlines</option>" +
                                                        "<option value='6Y'>Latcharter Air</option>" +
                                                        "<option value='QJ'>Latpass Airlines</option>" +
                                                        "<option value='NG'>Lauda Air</option>" +
                                                        "<option value='HE'>LGW</option>" +
                                                        "<option value='LI'>Liat Ltd</option>" +
                                                        "<option value='LN'>Libyan Airlines</option>" +
                                                        "<option value='JT'>Lion Air</option>" +
                                                        "<option value='LM'>Livingston</option>" +
                                                        "<option value='LB'>Lloyd Aereo Boli</option>" +
                                                        "<option value='LO'>LOT Polish</option>" +
                                                        "<option value='XO'>LTE Intl Airways</option>" +
                                                        "<option value='LH'>Lufthansa</option>" +
                                                        "<option value='CL'>Lufthansa Expr</option>" +
                                                        "<option value='LE'>Lugansk Airlines</option>" +
                                                        "<option value='LG'>Luxair</option>" +
                                                        "<option value='5V'>Lviv Airlines</option>" +
                                                        "<option value='I2'>Magenta Air</option>" +
                                                        "<option value='W5'>Mahan Air</option>" +
                                                        "<option value='M2'>Mahfooz</option>" +
                                                        "<option value='MH'>Malaysia</option>" +
                                                        "<option value='MA'>Malev Hungarian</option>" +
                                                        "<option value='TF'>Malmo Aviation</option>" +
                                                        "<option value='R5'>Malta Air</option>" +
                                                        "<option value='JE'>Mango</option>" +
                                                        "<option value='NM'>Manx2</option>" +
                                                        "<option value='M7'>Marsland Aviat</option>" +
                                                        "<option value='MP'>Martinair</option>" +
                                                        "<option value='FZ'>Master Airways</option>" +
                                                        "<option value='YD'>Mauritania Air</option>" +
                                                        "<option value='MM'>Medellin Aeronau</option>" +
                                                        "<option value='IG'>Meridiana</option>" +
                                                        "<option value='YV'>Mesa Airlines</option>" +
                                                        "<option value='XJ'>Mesaba Aviation</option>" +
                                                        "<option value='MX'>Mexicana</option>" +
                                                        "<option value='CS'>Micronesia</option>" +
                                                        "<option value='ME'>Middle East</option>" +
                                                        "<option value='YX'>Midwest Airlines</option>" +
                                                        "<option value='MJ'>Mihin Lanka</option>" +
                                                        "<option value='MW'>Mokulele Airline</option>" +
                                                        "<option value='2M'>Moldavian</option>" +
                                                        "<option value='OM'>Mongolian</option>" +
                                                        "<option value='UJ'>Montair Avaition</option>" +
                                                        "<option value='YM'>Montenegro</option>" +
                                                        "<option value='M9'>Motor Sich</option>" +
                                                        "<option value='XV'>MR Lines</option>" +
                                                        "<option value='VZ'>My TravelLite</option>" +
                                                        "<option value='8M'>Myanmar Airways</option>" +
                                                        "<option value='C8'>NAC Air</option>" +
                                                        "<option value='T2'>Nakina Air</option>" +
                                                        "<option value='UE'>Nasair</option>" +
                                                        "<option value='NC'>National Jet</option>" +
                                                        "<option value='CE'>Nationwide Air</option>" +
                                                        "<option value='4J'>Nationwide Air</option>" +
                                                        "<option value='5C'>Nature Air</option>" +
                                                        "<option value='ZN'>Naysa</option>" +
                                                        "<option value='NO'>Neos</option>" +
                                                        "<option value='9X'>New Axis Airways</option>" +
                                                        "<option value='EJ'>New England</option>" +
                                                        "<option value='N9'>Niger Air</option>" +
                                                        "<option value='HG'>Niki Luftfahrt</option>" +
                                                        "<option value='DD'>Nok Air</option>" +
                                                        "<option value='JH'>Nordeste-Linhas</option>" +
                                                        "<option value='6N'>Nordic Regional</option>" +
                                                        "<option value='8N'>Nordkalottflyg</option>" +
                                                        "<option value='M3'>North Flying</option>" +
                                                        "<option value='NS'>Northeastern Air</option>" +
                                                        "<option value='NW'>Northwest</option>" +
                                                        "<option value='2G'>Northwest Seapln</option>" +
                                                        "<option value='J3'>Northwestern Air</option>" +
                                                        "<option value='HW'>North-Wright</option>" +
                                                        "<option value='9E'>Nortwest Airlink</option>" +
                                                        "<option value='DY'>Norwegian Air</option>" +
                                                        "<option value='BJ'>Nouvelair Tunisi</option>" +
                                                        "<option value='N6'>Nuevo Continente</option>" +
                                                        "<option value='O8'>Oasis</option>" +
                                                        "<option value='VC'>Ocean Airlines</option>" +
                                                        "<option value='5K'>Odessa Airlines</option>" +
                                                        "<option value='OA'>Olympic Airlines</option>" +
                                                        "<option value='WY'>Oman Aviation</option>" +
                                                        "<option value='OC'>Omni</option>" +
                                                        "<option value='N3'>Omskavia</option>" +
                                                        "<option value='EC'>Openskies</option>" +
                                                        "<option value='R2'>Orenair</option>" +
                                                        "<option value='OX'>Orient Thai</option>" +
                                                        "<option value='QO'>Origin Pacific</option>" +
                                                        "<option value='OL'>Ostfriesisc Air</option>" +
                                                        "<option value='XH'>Other Travel</option>" +
                                                        "<option value='ON'>Our Airline</option>" +
                                                        "<option value='OJ'>Overland Airways</option>" +
                                                        "<option value='O7'>OzJet</option>" +
                                                        "<option value='P8'>P.L.A.S.</option>" +
                                                        "<option value='Y5'>Pace Airlines</option>" +
                                                        "<option value='3F'>Pacific Airways</option>" +
                                                        "<option value='8P'>Pacific Coastal</option>" +
                                                        "<option value='9J'>Pacific Island</option>" +
                                                        "<option value='LW'>Pacific Wings</option>" +
                                                        "<option value='PK'>Pakistan Intl</option>" +
                                                        "<option value='9P'>Palau Airlines</option>" +
                                                        "<option value='PF'>Palestinian Airl</option>" +
                                                        "<option value='NR'>Pamir Air</option>" +
                                                        "<option value='PQ'>PANAF</option>" +
                                                        "<option value='7E'>Panagra</option>" +
                                                        "<option value='HI'>Papillon</option>" +
                                                        "<option value='CG'>Papua New Guinea</option>" +
                                                        "<option value='I7'>Paramount Air</option>" +
                                                        "<option value='P6'>Pascan Aviation</option>" +
                                                        "<option value='9Q'>PB Air Company</option>" +
                                                        "<option value='3O'>Peau Vava U</option>" +
                                                        "<option value='H9'>Pegasus Airlines</option>" +
                                                        "<option value='7V'>Pelican Air</option>" +
                                                        "<option value='KS'>PenAir</option>" +
                                                        "<option value='4B'>Perimeter</option>" +
                                                        "<option value='P9'>Perm Airlines</option>" +
                                                        "<option value='PR'>Philippine</option>" +
                                                        "<option value='PU'>Pluna</option>" +
                                                        "<option value='Z3'>PM Air LLC</option>" +
                                                        "<option value='U4'>PMT Air</option>" +
                                                        "<option value='PO'>Polar Air Cargo</option>" +
                                                        "<option value='PH'>Polynesian</option>" +
                                                        "<option value='PD'>Porter Airlines</option>" +
                                                        "<option value='PW'>Precisionair</option>" +
                                                        "<option value='FE'>Primaris Airline</option>" +
                                                        "<option value='8W'>Private Wings</option>" +
                                                        "<option value='5S'>Profesionales</option>" +
                                                        "<option value='P0'>Proflight</option>" +
                                                        "<option value='PB'>Provincial Airl</option>" +
                                                        "<option value='RI'>PT. Mandella</option>" +
                                                        "<option value='EB'>Pullmantur Air</option>" +
                                                        "<option value='QF'>Qantas Airways</option>" +
                                                        "<option value='QR'>Qatar Airways</option>" +
                                                        "<option value='QG'>Qualiflyer Group</option>" +
                                                        "<option value='Q0'>Quebecair Expres</option>" +
                                                        "<option value='XA'>Railroad</option>" +
                                                        "<option value='RT'>RAK Airways</option>" +
                                                        "<option value='RW'>RAS Fluggesellsc</option>" +
                                                        "<option value='7R'>Red Sea Air</option>" +
                                                        "<option value='RM'>Regional Air</option>" +
                                                        "<option value='FN'>Regional Air MA</option>" +
                                                        "<option value='YS'>Regional CAE</option>" +
                                                        "<option value='ZL'>Regional Express</option>" +
                                                        "<option value='QT'>Regional Pacific</option>" +
                                                        "<option value='SL'>Rio Sul</option>" +
                                                        "<option value='RH'>Robin Hood</option>" +
                                                        "<option value='RR'>Royal Air Force</option>" +
                                                        "<option value='AT'>Royal Air Maroc</option>" +
                                                        "<option value='V5'>Royal Aruban</option>" +
                                                        "<option value='4A'>Royal Bengal Air</option>" +
                                                        "<option value='BI'>Royal Brunei</option>" +
                                                        "<option value='RJ'>Royal Jordanian</option>" +
                                                        "<option value='RA'>Royal Nepal</option>" +
                                                        "<option value='RY'>Royal Wings</option>" +
                                                        "<option value='FV'>Russian Airlines</option>" +
                                                        "<option value='WB'>Rwandair Express</option>" +
                                                        "<option value='FR'>RyanAir</option>" +
                                                        "<option value='YB'>S African Expres</option>" +
                                                        "<option value='4Q'>Safi Airways</option>" +
                                                        "<option value='PV'>Saint Barth</option>" +
                                                        "<option value='HZ'>Sakhalinskie</option>" +
                                                        "<option value='S6'>Salmon Air</option>" +
                                                        "<option value='ZS'>Sama</option>" +
                                                        "<option value='JI'>San Juan Aviatio</option>" +
                                                        "<option value='S3'>Santa Barbara</option>" +
                                                        "<option value='EX'>Santo Domingo</option>" +
                                                        "<option value='SK'>SAS</option>" +
                                                        "<option value='BU'>SAS Norway</option>" +
                                                        "<option value='SP'>SATA Air Acores</option>" +
                                                        "<option value='S4'>SATA Intl</option>" +
                                                        "<option value='9N'>SATENA</option>" +
                                                        "<option value='SV'>Saudi Arabian</option>" +
                                                        "<option value='YR'>Scenic Airlines</option>" +
                                                        "<option value='CB'>ScotAirways</option>" +
                                                        "<option value='BB'>Seaborne</option>" +
                                                        "<option value='8D'>Servant Air</option>" +
                                                        "<option value='UG'>Sevenair</option>" +
                                                        "<option value='D2'>Severstal</option>" +
                                                        "<option value='NL'>Shaheen Air Intl</option>" +
                                                        "<option value='SC'>Shandong</option>" +
                                                        "<option value='FM'>Shanghai</option>" +
                                                        "<option value='ZH'>Shenzhen Airline</option>" +
                                                        "<option value='S5'>Shuttle America</option>" +
                                                        "<option value='S7'>Siberian Air</option>" +
                                                        "<option value='3U'>Sichuan Airlines</option>" +
                                                        "<option value='FT'>Siem Reap</option>" +
                                                        "<option value='MI'>SilkAir</option>" +
                                                        "<option value='SQ'>Singapore Airl</option>" +
                                                        "<option value='JW'>Skippers</option>" +
                                                        "<option value='H2'>Sky Airline</option>" +
                                                        "<option value='XW'>Sky Express</option>" +
                                                        "<option value='NE'>SkyEurope</option>" +
                                                        "<option value='5P'>SkyEurope Air</option>" +
                                                        "<option value='SI'>Skynet Airlines</option>" +
                                                        "<option value='5G'>Skyservice</option>" +
                                                        "<option value='XT'>Skystar Airways</option>" +
                                                        "<option value='AL'>Skyway Airlines</option>" +
                                                        "<option value='JZ'>Skyways</option>" +
                                                        "<option value='OO'>Skywest (Utah)</option>" +
                                                        "<option value='XR'>Skywest Air (AU)</option>" +
                                                        "<option value='S0'>Slok Air Intl</option>" +
                                                        "<option value='6Q'>Slovak Air</option>" +
                                                        "<option value='QS'>Smartwings</option>" +
                                                        "<option value='2E'>Smokey Bay Air</option>" +
                                                        "<option value='2C'>SNCF</option>" +
                                                        "<option value='IE'>Solomon</option>" +
                                                        "<option value='SA'>South African</option>" +
                                                        "<option value='XZ'>South AfricanEx</option>" +
                                                        "<option value='YG'>South Airlines</option>" +
                                                        "<option value='DG'>South East Asian</option>" +
                                                        "<option value='PL'>Southern Air Cht</option>" +
                                                        "<option value='A4'>Southern Winds</option>" +
                                                        "<option value='WN'>Southwest</option>" +
                                                        "<option value='JK'>Spanair</option>" +
                                                        "<option value='SG'>Spice Jet</option>" +
                                                        "<option value='NK'>Spirit Airlines</option>" +
                                                        "<option value='UL'>SriLankan</option>" +
                                                        "<option value='SJ'>Sriwijaya Air</option>" +
                                                        "<option value='2I'>Star Up</option>" +
                                                        "<option value='NB'>Sterling</option>" +
                                                        "<option value='DM'>Sterling Blue</option>" +
                                                        "<option value='8F'>STP Airways</option>" +
                                                        "<option value='SD'>Sudan Airways</option>" +
                                                        "<option value='EZ'>Sun Air Scandina</option>" +
                                                        "<option value='SY'>Sun Country</option>" +
                                                        "<option value='2U'>Sun d'OR Airline</option>" +
                                                        "<option value='XQ'>SunExpress</option>" +
                                                        "<option value='CQ'>Sunshine Express</option>" +
                                                        "<option value='WG'>Sunwing</option>" +
                                                        "<option value='PY'>Surinam Airways</option>" +
                                                        "<option value='HS'>Svenska Direktfl</option>" +
                                                        "<option value='Q4'>Swazi Express</option>" +
                                                        "<option value='SM'>Swedline</option>" +
                                                        "<option value='LX'>SWISS</option>" +
                                                        "<option value='RB'>Syrian Arab Air</option>" +
                                                        "<option value='DT'>TAAG Angola</option>" +
                                                        "<option value='TA'>Taca Intl</option>" +
                                                        "<option value='VR'>TACV Cabo Verde</option>" +
                                                        "<option value='7J'>Tajikair</option>" +
                                                        "<option value='PZ'>TAM del Mercosur</option>" +
                                                        "<option value='JJ'>TAM Linhas Aerea</option>" +
                                                        "<option value='TQ'>Tandem Aero</option>" +
                                                        "<option value='K3'>Taquan Air Svc</option>" +
                                                        "<option value='RO'>Tarom-Romanian</option>" +
                                                        "<option value='SF'>Tassili Air</option>" +
                                                        "<option value='U9'>Tatarstan</option>" +
                                                        "<option value='L6'>Tbilaviamsheni</option>" +
                                                        "<option value='RU'>TCI Skyking</option>" +
                                                        "<option value='L9'>Teamline Air Luf</option>" +
                                                        "<option value='FD'>Thai AirAsia</option>" +
                                                        "<option value='TG'>Thai Intl</option>" +
                                                        "<option value='MT'>Thomas Cook Air</option>" +
                                                        "<option value='BY'>Thomsonfly</option>" +
                                                        "<option value='7Q'>Tibesti AirLibya</option>" +
                                                        "<option value='TR'>Tiger Airways</option>" +
                                                        "<option value='TT'>Tiger Airways</option>" +
                                                        "<option value='9D'>Toumai Air Tchad</option>" +
                                                        "<option value='WI'>Tradewinds</option>" +
                                                        "<option value='T8'>Tran African Air</option>" +
                                                        "<option value='N4'>Trans Air Benin</option>" +
                                                        "<option value='Q8'>Trans Air Congo</option>" +
                                                        "<option value='T9'>Trans Meridian</option>" +
                                                        "<option value='7T'>Trans North</option>" +
                                                        "<option value='UN'>Transaero</option>" +
                                                        "<option value='GE'>TransAsia</option>" +
                                                        "<option value='HV'>Transavia</option>" +
                                                        "<option value='9T'>Travelspan</option>" +
                                                        "<option value='OR'>Tui Airlines</option>" +
                                                        "<option value='TB'>Tui Airlines</option>" +
                                                        "<option value='X3'>Tuifly</option>" +
                                                        "<option value='R4'>Tulpar Avia</option>" +
                                                        "<option value='TU'>Tunis Air</option>" +
                                                        "<option value='TK'>Turkish Airlines</option>" +
                                                        "<option value='T5'>Turkmenistan</option>" +
                                                        "<option value='T7'>Twin Jet</option>" +
                                                        "<option value='VO'>Tyrolean</option>" +
                                                        "<option value='UH'>U.S. Helicopter</option>" +
                                                        "<option value='PS'>Ukraine Intl</option>" +
                                                        "<option value='UF'>Ukranian Mediter</option>" +
                                                        "<option value='B7'>Uni Airways</option>" +
                                                        "<option value='UA'>United</option>" +
                                                        "<option value='8X'>United (Passive)</option>" +
                                                        "<option value='4H'>United Airways</option>" +
                                                        "<option value='UW'>Universal</option>" +
                                                        "<option value='3Y'>UNIWAYS</option>" +
                                                        "<option value='U6'>Ural Airlines</option>" +
                                                        "<option value='US'>US Airways</option>" +
                                                        "<option value='U5'>USA 3000</option>" +
                                                        "<option value='UT'>UTAir</option>" +
                                                        "<option value='HY'>Uzbekistan</option>" +
                                                        "<option value='VF'>Valuair LTD</option>" +
                                                        "<option value='X4'>Vanair</option>" +
                                                        "<option value='VA'>V-Australia</option>" +
                                                        "<option value='2R'>Via Rail</option>" +
                                                        "<option value='VI'>Vieques Air Lnk</option>" +
                                                        "<option value='VN'>Vietnam Air</option>" +
                                                        "<option value='4P'>Viking Airlines</option>" +
                                                        "<option value='NN'>VIM Airlines</option>" +
                                                        "<option value='BF'>Vincent Aviation</option>" +
                                                        "<option value='VQ'>Vintage</option>" +
                                                        "<option value='V6'>VIP S.A</option>" +
                                                        "<option value='VX'>Virgin America</option>" +
                                                        "<option value='VS'>Virgin Atlantic</option>" +
                                                        "<option value='DJ'>Virgin Blue</option>" +
                                                        "<option value='VK'>Virgin Nigeria</option>" +
                                                        "<option value='ZG'>Viva Macau</option>" +
                                                        "<option value='XF'>Vladivostok Air</option>" +
                                                        "<option value='VG'>VLM Airlines</option>" +
                                                        "<option value='VE'>Volare</option>" +
                                                        "<option value='Y4'>Volaris</option>" +
                                                        "<option value='VY'>Vueling Airlines</option>" +
                                                        "<option value='4W'>Warbelows Air</option>" +
                                                        "<option value='WT'>Wasaya Airways</option>" +
                                                        "<option value='KW'>Wataniya Airline</option>" +
                                                        "<option value='2W'>Welcome Air</option>" +
                                                        "<option value='YH'>West Caribbean</option>" +
                                                        "<option value='8O'>West Coast Air</option>" +
                                                        "<option value='WS'>WestJet</option>" +
                                                        "<option value='WF'>Wideroes Flyvese</option>" +
                                                        "<option value='9C'>Wimbi Dira Air</option>" +
                                                        "<option value='IV'>Wind Jet</option>" +
                                                        "<option value='7W'>Wind Rose</option>" +
                                                        "<option value='WM'>Windward Island</option>" +
                                                        "<option value='IW'>Wings Air</option>" +
                                                        "<option value='K5'>Wings of Alaska</option>" +
                                                        "<option value='8Z'>Wizz Air</option>" +
                                                        "<option value='W6'>Wizz Air</option>" +
                                                        "<option value='WU'>Wizz Air Ukraine</option>" +
                                                        "<option value='WO'>World Airways</option>" +
                                                        "<option value='8V'>Wright Air Svc</option>" +
                                                        "<option value='2X'>Xexox</option>" +
                                                        "<option value='MF'>Xiamen Airlines</option>" +
                                                        "<option value='SE'>XL Airways</option>" +
                                                        "<option value='Y8'>Yakutia</option>" +
                                                        "<option value='YC'>Yamal Airlines</option>" +
                                                        "<option value='HK'>Yangon Airways</option>" +
                                                        "<option value='Y0'>Yellow Air Taxi</option>" +
                                                        "<option value='IY'>Yemenia Yemen</option>" +
                                                        "<option value='4Y'>Yute Air Alaska</option>" +
                                                        "<option value='ZJ'>Zambezi Airlines</option>" +
                                                        "<option value='K8'>Zambia Skyways</option>" +
                                                        "<option value='Q3'>Zambian Airways</option>" +
                                                        "<option value='B4'>Zan Air</option>" +
                                                        "<option value='Z4'>Zoom Airlines</option>" +
                                                        "<option value='ZX'>Zoom Airlines UK</option>" +

                                                    "</select>" +
                                                "</div>" +
                                            "</div>" +
                                            "<div class='flexibility'>" +
                                                "<label>Flexibility:</label>" +
                                                "<div class='selectbx'>" +
                                                    "<select class='' name='Flexibility' id='Flexibility' data-msg-required='Please Select the Flexibility' data-rule-required='true'>" +
                                                        "<option value='0'>Exact Dates</option>" +
                                                        "<option value='1'>+/- 1 day</option>" +
                                                        "<option value='2'>+/- 2 days</option>" +
                                                        "<option value='3'>+/- 3 days</option>" +
                                                    "</select>" +
                                                "</div>" +
                                            "</div>" +
                                        "</div>" +
                                    "</div>" +
                                            "</div>" +
                                        "</div>" +
                                    "</form>" +
                                "</div>";

            //searchcriteria += "<div id='chgsrc'>" +
            //                    "<div id='oneroundtrip' style='display: none'>" +
            //                        "<span>" +
            //                            "<input id='RoundTrip' value='Roundtrip' class='radio-custom' checked='checked' name='Triptype' type='radio'>" +
            //                            "<label for='RoundTrip' class='radio-custom-label'>Round Trip</label>" +
            //                        "</span>" +
            //                        "<span>" +
            //                            "<input id='Oneway' class='radio-custom' value='Oneway' name='Triptype' type='radio'>" +
            //                            "<label for='Oneway' class='radio-custom-label'>One way</label>" +
            //                        "</span>" +
            //                    "</div>" +
            //                    "<div class='fields1' id='From'>" +
            //                        "<p>FROM</p>" +
            //                        "<h5>" + getAirportDetails(Departure) + "(" + Departure + ")" + "</h5>" +
            //                    "</div>" +
            //                    "<div class='fieldsTo' id='Dirlogo'>" +
            //                        "<a href='#'>" +
            //                            "<img src='Design/images/directionIcon.png' alt='Directio icon'></a>" +
            //                    "</div>" +
            //                    "<div class='fields2' id='To'>" +
            //                        "<p>TO</p>" +
            //                        "<h5>" + getAirportDetails(Destination) + "(" + Destination + ")" + "</h5>" +
            //                    "</div>" +
            //                    "<div class='fields3' id='Fr_Date'>" +
            //                        "<p>DEPARTURE</p>" +
            //                        "<h5>" + depDateTextValue + "</h5>" + 
            //                    "</div>";
            //if (JType == 2)
            //    searchcriteria += "<div class='fields4' id='To_Date'>" +
            //                        "<p>RETURN</p>" +
            //                        "<h5>" + arrDateTextValue + "</h5>" +
            //                    "</div>";
            //searchcriteria += "<div class='fields5' id='A_Pax' data-init='pax'>" +
            //                        "<p>ADULT</p>" +
            //                        "<h5>" + AdultsCnt + "</h5>" +
            //                    "</div>" +
            //                    "<div class='fields5' id='C_Pax' data-init='pax'>" +
            //                        "<p>CHILD</p>" +
            //                        "<h5>" + ChildsCnt + "</h5>" +
            //                    "</div>" +
            //                    "<div class='fields5' id='I_Pax' data-init='pax'>" +
            //                        "<p>INFANT</p>" +
            //                        "<h5>" + InfantsCnt + "</h5>" +
            //                    "</div>" +
            //                    "<button type='submit' class='SrcClick btn' value='True'><span><i class='fa fa-edit'></i>Edit</span></button>" +
            //                "</div>";

            divsearchcriteria.InnerHtml = searchcriteria;
        }
        catch (Exception ex)
        {

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    //    if (!Request.IsSecureConnection)
    //    {
    //        string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
    //        Response.Redirect(redirectUrl);
    //    }
        //Timer chbxName = (Timer)sender;
        string ctrl = "";
        if (IsPostBack)
            ctrl = Request.Params["__EVENTTARGET"].ToString();
        if (ctrl != "Timer2")
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;

            //if (Request.QueryString["sid"] != null)
            if (Session["sidAir"] != null)
            {
                //SearchID = Request.QueryString["sid"].ToString();
                SearchID = Session["sidAir"].ToString();
                SearchRef.Value = SearchID;
                if (Session["trackid" + SearchID] != null)
                {
                    track_id = Convert.ToInt64(Session["trackid" + SearchID].ToString());
                }

                Session["RedirectURL"] = "Results.aspx?sid=" + SearchID;
                if (Session["newsearch" + SearchID] != null)
                {
                    newsearch = Session["newsearch" + SearchID].ToString();
                    Session["newsearch" + SearchID] = null;
                }
                else
                {
                    newsearch = "";
                }
                bindvalues();

            }
        }
        if (!IsPostBack)
        {


            if (Session["paxemail_tmcom_mybookings"] != null)
            {
                username = Session["paxemail_tmcom_mybookings"].ToString();
            }


            if (Session["_sessionid"] == null)
            {
                _sessionid = RandomPassword.GenerateStrong(50);

                Session["_sessionid"] = _sessionid;
            }


            if (Request.QueryString["ssid"] != null)
                ssid = Request.QueryString["ssid"].ToString();


            if (Session["SearchResult" + SearchID] == null)
            {
                Response.Redirect("sessionexpired.aspx?sid=" + SearchID + "&errMsg=No Search Results");
                Response.End();
            }

            searchType = Session["SearchType" + SearchID].ToString();


            try
            {
                bool alldone = false;
                if (Session["Thread_Count" + SearchID] != null)
                {
                    for (int i = 0; i < Convert.ToInt32(Session["Thread_Count" + SearchID]); i++)
                    {
                        if (Session["Thread" + i + SearchID] != null)
                        {
                            if (Session["Thread" + i + SearchID].ToString() == "End")
                            {
                                alldone = true;
                            }
                            else
                            {
                                alldone = false;
                                break;
                            }

                            if (Session["ThreadImg" + i + SearchID].ToString() == "End")
                            {
                                alldone = true;
                            }
                            else
                            {
                                alldone = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    alldone = true;
                }
                if (!alldone)
                {

                    isDone.Value = "0";
                    if (IsPostBack)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);

                    }

                }
                else
                {

                    Timer1.Enabled = false;
                    isDone.Value = "1";
                    if (IsPostBack)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);

                    }
                    if (Session["fareexpired" + SearchID] != null)
                    {

                        //errmsg.Attributes.Add("style", "display:");

                        if (Session["fareexpired" + SearchID] == "true")
                        {

                            message = "The fare shown is no longer available.Please find next available flight options 'or' search again.";
                            errmsg.InnerHtml = "<h4 class='text-center mrgtop'><i class='fa fa-exclamation-triangle'></i>" + message + "</h4>";

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                isDone.Value = "1";
            }
        }
        if (Request.Params["__EVENTTARGET"] != null)
        {
            if (Request.Params["__EVENTTARGET"].ToString() == "btnSelect")
            {
                btnSelect_Click();
            }
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {

            if (Session["conf" + SearchID] != null)
            {
                //Session["conf" + SearchID] = sortedDT.Rows[0]["ciid"].ToString();
                //Session["inid" + SearchID] = sortedDT.Rows[0]["inid"].ToString();
                //Session["outid" + SearchID] = sortedDT.Rows[0]["outid"].ToString();
                Refresh1.InnerHtml = "<div class='modal-dialog' role='document'>" +
                                            "<div class='modal-content'>" +
                                                "<div class='modal-header text-center'>" +
                                                    "<button type='button' id='RefreshExpired2' class='close' onclick='searchagain12(\"" + Session["conf" + SearchID].ToString() + "\",\"" + Session["inid" + SearchID].ToString() + "\",\"" + Session["outid" + SearchID].ToString() + "\")' aria-hidden='true'>×</button>" +
                                                    "<h4 class='modal-title' id='myModalLabel1'>Session Expired </h4>" +
                                                "</div>" +
                                                "<div class='modal-body text-center'>" +
                                                    "<div class='refreshWrap'>" +
                                                        "<div class='timer'><i class='fa fa-3x icofont icofont-clock-time faa-tada animated'></i></div>" +
                                                        "<h3 class='red'>Timed Out</h3>" +

                                                        "<p><strong>Still interested in flying from " + DepartureCity + " to " + DestinationCity + "?  </strong></p>" +
                                                        "<p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>" +

                                                        "<a href='Index.aspx' class='btn button newSearchbtn mr-15'>New Search </a>" +
                                                        "<button type='submit' class='btn button refreshBtn' onclick='searchagain12(\"" + Session["conf" + SearchID].ToString() + "\",\"" + Session["inid" + SearchID].ToString() + "\",\"" + Session["outid" + SearchID].ToString() + "\")' id='RefreshExpired1'>Refresh  </button>" +
                                                        "<h5 class='red'>Your search results have expired!</h5>" +
                                                    "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</div>";
            }


            bool alldone = false;
            if (Session["Thread_Count" + SearchID] != null)
            {
                for (int i = 0; i < Convert.ToInt32(Session["Thread_Count" + SearchID]); i++)
                {
                    if (Session["Thread" + i + SearchID] != null)
                    {
                        if (Session["Thread" + i + SearchID].ToString() == "End")
                        {
                            alldone = true;
                        }
                        else
                        {
                            alldone = false;
                            break;
                        }

                        if (Session["ThreadImg" + i + SearchID].ToString() == "End")
                        {
                            alldone = true;
                        }
                        else
                        {
                            alldone = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                alldone = true;
            }
            if (!alldone)
            {

                isDone.Value = "0";
                if (IsPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);

                }

            }
            else
            {
                if (Session["conf" + SearchID] == null)
                {
                    SqlParameter[] parametersFares = { new SqlParameter("@search_id", Convert.ToInt64(SearchID)) };
                    DataTable dtAAFares = DataLayer.GetData("flight_search_fares_selectBySearchID_3", parametersFares, dbAccess.conn_save_search_data);
                    if (dtAAFares.Rows.Count > 0)
                    {
                        DataView dv = dtAAFares.DefaultView;
                        dv.Sort = "ttfare asc";
                        DataTable sortedDT = dv.ToTable();

                        Session["conf" + SearchID] = sortedDT.Rows[0]["ciid"].ToString();
                        Session["inid" + SearchID] = sortedDT.Rows[0]["inid"].ToString();
                        Session["outid" + SearchID] = sortedDT.Rows[0]["outid"].ToString();
                        Refresh1.InnerHtml = "<div class='modal-dialog' role='document'>" +
                                                    "<div class='modal-content'>" +
                                                        "<div class='modal-header text-center'>" +
                                                            "<button type='button' id='RefreshExpired2' class='close' onclick='searchagain12(\"" + sortedDT.Rows[0]["ciid"].ToString() + "\",\"" + sortedDT.Rows[0]["inid"].ToString() + "\",\"" + sortedDT.Rows[0]["outid"].ToString() + "\")' aria-hidden='true'>×</button>" +
                                                            "<h4 class='modal-title' id='myModalLabel1'>Session Expired </h4>" +
                                                        "</div>" +
                                                        "<div class='modal-body text-center'>" +
                                                            "<div class='refreshWrap'>" +
                                                                "<div class='timer'><i class='fa fa-3x icofont icofont-clock-time faa-tada animated'></i></div>" +
                                                                "<h3 class='red'>Timed Out</h3>" +

                                                                "<p><strong>Still interested in flying from " + DepartureCity + " to " + DestinationCity + "?  </strong></p>" +
                                                                "<p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>" +

                                                                "<a href='Index.aspx' class='btn button newSearchbtn mr-15'>New Search </a>" +
                                                                "<button type='submit' class='btn button refreshBtn' onclick='searchagain12(\"" + sortedDT.Rows[0]["ciid"].ToString() + "\",\"" + sortedDT.Rows[0]["inid"].ToString() + "\",\"" + sortedDT.Rows[0]["outid"].ToString() + "\")' id='RefreshExpired1'>Refresh  </button>" +
                                                                "<h5 class='red'>Your search results have expired!</h5>" +
                                                            "</div>" +
                                                        "</div>" +
                                                    "</div>" +
                                                "</div>";
                    }
                }
                Timer1.Enabled = false;
                isDone.Value = "1";
                if (IsPostBack)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);
                }
                if (Session["fareexpired" + SearchID] != null)
                {
                    //errmsg.Attributes.Add("style", "display:");
                    if (Session["fareexpired" + SearchID] == "true")
                    {
                        message = "The fare shown is no longer available.Please find next available flight options 'or' search again.";
                        errmsg.InnerHtml = "<h4 class='text-center mrgtop'><i class='fa fa-exclamation-triangle'></i>" + message + "</h4>";
                    }

                }

            }

            //if (Session["Thread1S" + SearchID].ToString() == "End" && Session["Thread1SN" + SearchID].ToString() == "End" && Session["Thread1SFlex" + SearchID].ToString() == "End")
            //{
            //    isDone.Value = "1";
            //    Timer1.Enabled = false;
            //    //  System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);

            //}
            //if (Session["ThreadImg1S" + SearchID].ToString() == "End" && Session["ThreadImg1SN" + SearchID].ToString() == "End" && Session["ThreadImg1SFlex" + SearchID].ToString() == "End")
            //{

            //    isDone.Value = "1";
            //    Timer1.Enabled = false;
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);

            //}
            //else
            //{

            //    isDone.Value = "0";
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_getFlights();", true);

            //}
        }
        catch (Exception ex)
        {
            isDone.Value = "1";

        }

        //DataView dv = dtAAFares.DefaultView;
        //dv.Sort = "ttfare asc";
        //DataTable sortedDT = dv.ToTable();
        //if (sortedDT.Rows.Count > 0)
        //{
        //    if (track_id != null)
        //    {
        //        if (track_id != "")
        //        {
        //            //string trid = track_id.ToString();
        //            SqlParameter[] param_track_fare = {new SqlParameter("@Fare",sortedDT.Rows[0]["ttFare"].ToString()),
        //                                              new SqlParameter("@trID",Convert.ToInt64(track_id))};
        //            DataLayer.UpdateData("afid_track_data_update_fare", param_track_fare, ConfigurationManager.AppSettings["sqlcn_track_data"].ToString());
        //        }
        //    }
        //}


    }


    protected void Timer2_Tick(object sender, EventArgs e)
    {
        if (Request.QueryString["sid"] != null)
        {
            SearchID = Request.QueryString["sid"].ToString();
        }
        isExpired.Value = "no";
        if (Session["SearchResult" + SearchID] == null)
        {
            isExpired.Value = "yes";
            if (IsPostBack)
            {
                Timer2.Enabled = false;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "btn_ShowRefresh();", true);
            }
        }
    }

    protected void btnSelect_Click()
    {

        if (ssid == "")
        {
            ssid = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }





        bool Bookable = true;
        //if (searchType == "FO")
        //{
        //    Bookable = AddtoBasket(imgBtn.ID);
        //}
        Session["fid" + ssid] = Request.Params["__EVENTARGUMENT"].ToString();


        if (Bookable)
        {
            if (searchType == "FH")
            {
                Response.Redirect("SearchResultFH.aspx?sid=" + SearchID + "&ssid=" + ssid);
            }
            else
            {
                Response.Redirect("passengerDetails.aspx?sid=" + SearchID + "&ssid=" + ssid);
            }
        }
        else
        {
            Response.Redirect("error.aspx?ssid=" + ssid + "&sid=" + SearchID + "&ErrMsg=No Seats Available, please choose another flights");

        }



        //ProgressBarModalPopupExtender.Hide();

    }

    [System.Web.Services.WebMethod]
    public static void UpdateFare(string track_id, string fare, string ciid, string inid, string outid)
    {
        if (track_id != null)
        {
            if (track_id != "")
            {
                SqlParameter[] param_track_fare = {new SqlParameter("@Fare",fare.ToString()),
                                                      new SqlParameter("@trID",Convert.ToInt64(track_id))};
                DataLayer.UpdateData("afid_track_data_update_fare", param_track_fare, ConfigurationManager.AppSettings["sqlcn_track_data"].ToString());
                encriptonanddecription obj = new encriptonanddecription();
                SqlParameter[] param_track_fare1 = {
                                                            new SqlParameter("@conf",ciid),
                                                            new SqlParameter("@inid",inid),
                                                            new SqlParameter("@outid",outid),
                                                            new SqlParameter("@trID",Convert.ToInt64(track_id))
                                                        };
                DataLayer.UpdateData("afid_track_data_update_RedirectUrlSB", param_track_fare1, ConfigurationManager.AppSettings["sqlcn_track_data"].ToString());
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Check(string SearchID)
    {
        if (HttpContext.Current.Session["SearchResult" + SearchID] == null)
        {
            return "false";
        }
        else
            return "true";

    }
}
