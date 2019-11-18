<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlightResults.aspx.cs" Inherits="FlightResults" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE = edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>TravelMerry - US</title>
    <link rel="shortcut icon" href="Design/images/favicon.ico">


    <link rel="stylesheet" type="text/css" href="Design/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/tmStyle.css" />


    <link rel="stylesheet" type="text/css" href="Design/css/checkboxstyle.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/tm_vertical_style.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/tmpm_link.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/icofont.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/vertical-rsc-style.css" />

    <link type="text/css" rel="stylesheet" href="Design/css/font-awesome-animation.min.css" />



    <link rel="stylesheet" type="text/css" href="Design/build/css/caleran.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/omni-slider.css">
    <link rel="stylesheet" type="text/css" href="Design/css/jivo_over.css" />
    <!--[if lt IE 9]>
    <script src = "https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src = "https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <noscript>
        <div style="position: fixed; top: 0px; left: 0px; z-index: 3000; height: auto; width: 100%; text-align: center; padding: 20px; background-color: #eac551; border: 1px solid #d4aa28; color: #a81f15; font-family: Arial, Helvetica, sans-serif; font-size: 22px; font-weight: bold;">
            <p style="margin-left: 10px">JavaScript is not enabled.</p>
        </div>
    </noscript>
    <!-- 
<script src="js/google-analytics.js"></script> 
-->
    <script src="js/travelmerry_hotjar.js" type="text/javascript"></script>

    <script type="text/javascript">


        //SIFT SCIENCE

        var _user_id = '<%=username%>'; // IMPORTANT! Set to the user's ID, username, or email address, or '' if not yet known.


        var _session_id = '<%=_sessionid%>'; // Set to a unique session ID for the visitor's current browsing session.

        var _sift = window._sift = window._sift || [];
        _sift.push(['_setAccount', '<%=RandomPassword.JavaScriptKey %>']);
        _sift.push(['_setUserId', _user_id]);
        _sift.push(['_setSessionId', _session_id]);
        _sift.push(['_trackPageview']);
        (function () {
            function ls() {
                var e = document.createElement('script');
                e.type = 'text/javascript';
                e.async = true;
                e.src = ('https:' === document.location.protocol ? 'https://' : 'http://') + 'cdn.siftscience.com/s.js';
                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(e, s);
            }
            if (window.attachEvent) {
                window.attachEvent('onload', ls);
            } else {
                window.addEventListener('load', ls, false);
            }
        })();
    </script>
    <script type="text/javascript">
        function SessionExpireAlert(timeout) {
            var seconds = timeout / 1000;
            var asdf = (1200000 - 200000) * 1000;
            setInterval(function () {
                seconds--;
                secs = Math.round(seconds);
                var hours = Math.floor(secs / (60 * 60));

                var divisor_for_minutes = secs % (60 * 60);
                var minutes = Math.floor(divisor_for_minutes / 60);

                var divisor_for_seconds = divisor_for_minutes % 60;
                var seconds1 = Math.ceil(divisor_for_seconds);

                var obj = {
                    "h": hours,
                    "m": minutes,
                    "s": seconds1
                };
                //return obj;
                document.getElementById("time").innerHTML = minutes + ":" + seconds1;
            }, 1000);
            setTimeout(function () {
                //$("#Refresh").css("display", "block");
                document.getElementById("Refresh").style.display = "block";
                //document.getElementById("Refresh").dis ="display", "block");
            }, 1200000 - 120000);
            setTimeout(function () {
                document.getElementById("Refresh1").style.display = "block";
                document.getElementById("Refresh").style.display = "none";
            }, timeout);
        };
        function ResetSession() {
            //Redirect to refresh Session.
            window.location = window.location.href.split('#')[0];
        }
    </script>
    <!-- Google Tag Manager -->
    <script>     

        (function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-KGS238J');

    </script>
    <!-- End Google Tag Manager -->
</head>
<body id="BackImgcity" runat="server">

    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-KGS238J"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->
    <div id="price-change" class="modal fade in">
    </div>
    <div id="dialog-message" class="modal fade in hidden-lg hidden-xs hidden-sm hidden-md" role="dialog" aria-hidden="false">
        <div class="modal-backdrop fade in"></div>
        <div class="modal-backdrop fade in"></div>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="priceChange">
                    <button type="button" class="close" id="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <div class="PcIcon">
                        <img src="Design/images/priceChangeIcon.png" alt="Price change icon">
                    </div>
                    <div class="PcTitles">
                        <h3>Receive price change alerts</h3>
                        <p>Weekly email with favorite destinations and prices</p>
                    </div>
                    <div class="pcContentBrd">
                        <div class="pcSegment" id="modalpopup" runat="server">
                        </div>
                        <div id="custom-search-input" class="mt-15">
                            <form>
                                <div class="emailbx">
                                    <input class="form-control input-lg e-icon" placeholder="Get TravelMerry Emails" type="email" id="newsletteremail" type="email">
                                    <span class="fieldError fade-up-tt" id="news-message"></span>
                                </div>
                                <div class="subtbx">
                                    <button class="btn createAlertBtn" type="button" id="newsSignUP">Get me deal</button>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="pcFormSection">
                        <div class="call-to-deal">
                            <div class="call-to-deal-Icon">
                                <img src="Design/images/cusIcon.png" alt="Customer support icon">
                            </div>
                            <div class="call-to-deal-number">
                                <h5>Need Help?</h5>
                                <p id="modalpopup1" runat="server"></p>
                                <h3><a href="tel:<%=ContactDetails.Phone_support %>"><%=ContactDetails.Phone_support %></a></h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="modal-dialog">
            <div class="modal-content">
                <div class="loadingFlights">
                    <button type="button" class="close" id="close" aria-hidden="true">×</button>
                    <div class="loadingFlighticon">
                        <img src="Design/images/logo.png" alt="TravelMerry logo">
                    </div>
                    <div class="loadingFlightTitles">
                        <h3>We’re finding great fares for you</h3>
                    </div>

                    <div class="loadingFlightsbrd">
                        <div class="loadingFlights-Item" id="modalpopup" runat="server">
                        </div>
                    </div>
                    <div class="fareAlert-wrap">
                        <h4>Low airfare alert</h4>
                        <p>Be the first to know when fare drop</p>
                        <div id="custom-search-input">
                            <form>
                                <div class="emailbx">
                                    <input class="form-control input-lg e-icon" placeholder="Get TravelMerry Emails" id="newsletteremail" type="email">
                                    <span class="fieldError fade-up-tt" id="news-message"></span>
                                </div>
                                <div class="subtbx">
                                    <button class="btn createAlertBtn" type="button" id="newsSignUP">Sign Up</button>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="loadingFlights-sec">
                        <p>This will not interrupt your search</p>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <form id="formServer" name="formServer" runat="server">
        <input id="SelectedRef" type="hidden" runat="server" />
        <input id="SearchRef" type="hidden" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/FlightResults.asmx" />
            </Services>
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="3000">
                </asp:Timer>
                <input id="isDone" type="hidden" runat="server" />
            </ContentTemplate>

        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Interval="60000">
                </asp:Timer>
                <input id="isExpired" type="hidden" runat="server" />
            </ContentTemplate>

        </asp:UpdatePanel>




        <%-- </ContentTemplate>

        </asp:UpdatePanel>--%>
    </form>
    <form id="form1" name="form1">
        <div>
        </div>
    </form>


    <!-- #include file ="Header.aspx" -->


    <div id="fldetails-topbar" data-toggle="sticky-onscroll">
        <div class="cs-container">
            <nav class="navbar">
                <form method="post" name="flight" id="flight" novalidate action="FlightRequest.aspx">
                    <input type="hidden" name="CabinClass" id="CabinClass" value="" />
                    <input type="hidden" name="Adultss" id="Adultss" value="" />
                    <input type="hidden" name="Childss" id="Childss" value="" />
                    <input type="hidden" name="Infantss" id="Infantss" value="" />
                    <input id='Triptype' name='Triptype' type='hidden'>
                    <input id='chkDirectFlightsFO' name='chkDirectFlightsFO' type='hidden'>
                    <input id='Flexible' name='Flexible' type='hidden'>
                    <input id='txtFlyFromFO_hidden' name='txtFlyFromFO_hidden' type='hidden'>
                    <input id='txtFlyToFO_hidden' name='txtFlyToFO_hidden' type='hidden'>
                    <input id='dept' name='dept' type='hidden'>
                    <input id='dest' name='dest' type='hidden'>
                    <div class="fldetails-change" id="divsearchcriteria" runat="server">
                    </div>
                    <div class="modal fade in success-popup" id="Refresh" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" runat="server">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header text-center">
                                    <button type="button" class="close" onclick="ResetSession()" aria-hidden="true">×</button>
                                    <h4 class="modal-title" id="myModalLabel">Session Expired </h4>
                                </div>
                                <div class="modal-body text-center">
                                    <div class="refreshWrap">
                                        <div class="timer"><i class="fa fa-3x icofont icofont-clock-time faa-tada animated"></i></div>
                                        <h3 class="red">Time is running Out</h3>
                                        <h3 class="red" id="time"></h3>

                                        <p><strong>Still interested in flying from <%=DepartureCity %> to <%=DestinationCity %>?  </strong></p>
                                        <p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>

                                        <a href="Index.aspx" class="btn button newSearchbtn mr-15">New Search </a>
                                        <button type="submit" class="btn button refreshBtn" onclick="ResetSession()" id="RefreshExpired">Refresh  </button>
                                        <h5 class="red">Your search results have expired!</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade in success-popup" id="Refresh1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" runat="server">
                        <%--<div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header text-center">
                                    <button type="button" id="RefreshExpired2" class="close" onclick="searchagain12('<%=conf %>','<%=inid %>','<%=outid %>')" aria-hidden="true">×</button>
                                    <h4 class="modal-title" id="myModalLabel1">Session Expired </h4>
                                </div>
                                <div class="modal-body text-center">
                                    <div class="refreshWrap">
                                        <div class="timer"><i class="fa fa-3x icofont icofont-clock-time faa-tada animated"></i></div>
                                        <h3 class="red">Timed Out</h3>

                                        <p><strong>Still interested in flying from <%=DepartureCity %> to <%=DestinationCity %>?  </strong></p>
                                        <p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>

                                        <a href="Index.aspx" class="btn button newSearchbtn mr-15">New Search </a>
                                        <button type="submit" class="btn button refreshBtn" onclick="searchagain12('<%=conf %>','<%=inid %>','<%=outid %>')" id="RefreshExpired1">Refresh  </button>
                                        <h5 class="red">Your search results have expired!</h5>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </form>
            </nav>
        </div>
    </div>
    <div class="updatingIcon-overBg" id="Loading" style="display: none;">
        <div class="updatingIcon-wrap">
            <img src="Design/images/Rolling.gif" alt="Loading icon" />
            <p>Loading...</p>
        </div>
    </div>
    <section class="ver-wrap backbg-trans">
        <aside class="ver-side box">
            <div class="VerticalAccordion">
                <div id="overlay">
                    <div class="spinner"></div>
                </div>
                <div class="VerticalAccordion" id='Filters' style="pointer-events: none; cursor: not-allowed; opacity: 0.4;">

                    <div class="verHeadtitle">
                        <a href="#" class="close filterClose slide-toggle" id="closeFilter"><span aria-hidden="true">×</span><span class="sr-only">Close</span></a>
                    </div>
                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Price</h4>
                        <div class="accordion-content">
                            <div class="rangeslide-cont">
                                <div class="rangemin_max">
                                    <strong id="min5" class="min">$0</strong> <strong id="max5" class="max">$100</strong>
                                </div>
                                <div id="slider5" class="range-wrap">
                                    <div class="slider">
                                        <div class="handle handle-left slider-transition" style="margin-left: 0px; left: 0%;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="handle handle-right slider-transition" style="margin-right: 0px; right: 0%;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="slider-fill slider-transition" style="right: 0%; left: 0%;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Flight Stops</h4>
                        <div class="accordion-content">
                            <div class="tm-radio-buttons-wrapper">
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="0" id="stop-0" checked="">
                                <label for="stop-0">
                                    <p class="stopText bbline">0</p>
                                    <p class="stopPrice">$259</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="1" id="stop-1" disabled="">
                                <label for="stop-1">
                                    <p class="stopText bbline">1</p>
                                    <p class="stopPrice">$259</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="2" id="stop-2">
                                <label for="stop-2">
                                    <p class="stopText bbline">2</p>
                                    <p class="stopPrice">$259</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="3" id="stop-3">
                                <label for="stop-3">
                                    <p class="stopText bbline">3</p>
                                    <p class="stopPrice">$259</p>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Fare Type</h4>
                        <div class="accordion-content">
                            <ul>
                                <li>
                                    <input id="Refundable" class="checkbox-custom" name="Refundable" type="checkbox">
                                    <label for="Refundable" class="checkbox-custom-label">Refundable</label>
                                    <span class="vfText pull-right">70</span>
                                </li>

                                <li>
                                    <input id="NonRefundable" class="checkbox-custom" name="Non Refundable" type="checkbox">
                                    <label for="NonRefundable" class="checkbox-custom-label">Non Refundable</label>
                                    <span class="vfText pull-right">70</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Depart Time</h4>
                        <div class="accordion-content">
                            <div class="tm-radio-buttons-wrapper">
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="1" id="dp-em" checked="">
                                <label for="dp-em">
                                    <p class="stopText bbline"><i class="icofont icofont-sun-rise"></i></p>
                                    <p class="stopPrice">00 - 06</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="2" id="dp-mg">
                                <label for="dp-mg">
                                    <p class="stopText bbline"><i class="icofont icofont-sun-alt"></i></p>
                                    <p class="stopPrice">06 - 12</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="3" id="dp-noon">
                                <label for="dp-noon">
                                    <p class="stopText bbline"><i class="icofont icofont-sun-set"></i></p>
                                    <p class="stopPrice">12 - 18</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="4" id="dp-night">
                                <label for="dp-night">
                                    <p class="stopText bbline"><i class="icofont icofont-night"></i></p>
                                    <p class="stopPrice">18 - 00</p>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Arrive Time</h4>
                        <div class="accordion-content">
                            <div class="tm-radio-buttons-wrapper">
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="1" id="av-em" checked="">
                                <label for="av-em">
                                    <p class="stopText bbline"><i class="icofont icofont-sun-rise"></i></p>
                                    <p class="stopPrice">00 - 06</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="2" id="av-mg">
                                <label for="av-mg">
                                    <p class="stopText bbline"><i class="icofont icofont-sun-alt"></i></p>
                                    <p class="stopPrice">06 - 12</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="3" id="av-noon">
                                <label for="av-noon">
                                    <p class="stopText bbline"><i class="icofont icofont-sun-set"></i></p>
                                    <p class="stopPrice">12 - 18</p>
                                </label>
                                <input type="radio" class="tm-radio-button" name="radioButtonTest" value="4" id="av-night">
                                <label for="av-night">
                                    <p class="stopText bbline"><i class="icofont icofont-night"></i></p>
                                    <p class="stopPrice">18 - 00</p>
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Airlines</h4>
                        <div class="accordion-content">
                            <ul>
                                <div class="clear-all">
                                    <span class=""><a href="#">Select all</a></span>
                                    <span class=""><a href="#">Clear</a></span>
                                </div>
                                <li>
                                    <input id="AmericanAirlines" class="checkbox-custom" name="American Airlines" type="checkbox">
                                    <label for="AmericanAirlines" class="checkbox-custom-label">American Airlines</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="AlaskaAirlines" class="checkbox-custom" name="Alaska Airlines" type="checkbox">
                                    <label for="AlaskaAirlines" class="checkbox-custom-label">Alaska Airlines</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="AirIndia" class="checkbox-custom" name="Air India" type="checkbox">
                                    <label for="AirIndia" class="checkbox-custom-label">Air India</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="BritishAirways" class="checkbox-custom" name="British Airways" type="checkbox">
                                    <label for="BritishAirways" class="checkbox-custom-label">British Airways</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="CathayPacificAirways" class="checkbox-custom" name="Cathay Pacific Airways" type="checkbox">
                                    <label for="CathayPacificAirways" class="checkbox-custom-label">Cathay Pacific Airways</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="EtihadAirways" class="checkbox-custom" name="Etihad Airways" type="checkbox">
                                    <label for="EtihadAirways" class="checkbox-custom-label">Etihad Airways</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="TurkishAirlines" class="checkbox-custom" name="Turkish Airlines" type="checkbox">
                                    <label for="TurkishAirlines" class="checkbox-custom-label">Turkish Airlines</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="KLM" class="checkbox-custom" name="KLM" type="checkbox">
                                    <label for="KLM" class="checkbox-custom-label">KLM</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <li>
                                    <input id="QatarAirways" class="checkbox-custom" name="Qatar Airways" type="checkbox">
                                    <label for="QatarAirways" class="checkbox-custom-label">Qatar Airways</label>
                                    <span class="vfText pull-right">$259</span>
                                </li>

                                <div id="target-Airlines" style="display: none;">
                                    <li>
                                        <input id="Emirates" class="checkbox-custom" name="Emirates" type="checkbox">
                                        <label for="Emirates" class="checkbox-custom-label">Emirates</label>
                                        <span class="vfText pull-right">$259</span>
                                    </li>

                                    <li>
                                        <input id="GulfAir" class="checkbox-custom" name="Gulf Air" type="checkbox">
                                        <label for="GulfAir" class="checkbox-custom-label">Gulf Air</label>
                                        <span class="vfText pull-right">$259</span>
                                    </li>

                                    <li>
                                        <input id="AirCanada" class="checkbox-custom" name="AirCanada" type="checkbox">
                                        <label for="AirCanada" class="checkbox-custom-label">Air Canada</label>
                                        <span class="vfText pull-right">$259</span>
                                    </li>
                                </div>
                                <div class="showAll"><a onclick="$('#target-Airlines').toggle();">Show all 2 airlines</a></div>
                            </ul>
                        </div>
                    </div>

                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Refine By Alliance</h4>
                        <div class="accordion-content">
                            <ul>
                                <li>
                                    <input id="Oneworld" class="checkbox-custom" name="Oneworld" type="checkbox">
                                    <label for="Oneworld" class="checkbox-custom-label">Oneworld</label>
                                    <span class="vfText pull-right">18</span>
                                </li>

                                <li>
                                    <input id="SkyTeam" class="checkbox-custom" name="Sky Team" type="checkbox">
                                    <label for="SkyTeam" class="checkbox-custom-label">Sky Team</label>
                                    <span class="vfText pull-right">70</span>
                                </li>

                                <li>
                                    <input id="StarAlliance" class="checkbox-custom" name="Star Alliance" type="checkbox">
                                    <label for="StarAlliance" class="checkbox-custom-label">Star Alliance</label>
                                    <span class="vfText pull-right">70</span>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Layover Airports</h4>
                        <div class="accordion-content">
                            <ul>
                                <li>
                                    <input id="AbuDhabi" class="checkbox-custom" name="Abu Dhabi" type="checkbox">
                                    <label for="AbuDhabi" class="checkbox-custom-label">Abu Dhabi <small>(AUH)</small></label>
                                    <span class="vfText pull-right">18</span>
                                </li>

                                <li>
                                    <input id="Manchester" class="checkbox-custom" name="Manchester" type="checkbox">
                                    <label for="Manchester" class="checkbox-custom-label">Manchester <small>(MAN)</small></label>
                                    <span class="vfText pull-right">70</span>
                                </li>

                                <li>
                                    <input id="NewDelhi" class="checkbox-custom" name="New Delhi" type="checkbox">
                                    <label for="NewDelhi" class="checkbox-custom-label">New Delhi <small>(DEL)</small></label>
                                    <span class="vfText pull-right">70</span>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Duration</h4>
                        <div class="accordion-content">
                            <div class="rangeslide-cont">
                                <div class="htitle">
                                    <h5>Outbound Duration</h5>
                                    <p>5Hours - 9Hours</p>
                                </div>
                                <div class="rangemin_max">
                                    <strong id="min3" class="min">$0</strong> <strong id="max3" class="max">$100</strong>
                                </div>
                                <div id="outDuration" class="range-wrap">
                                    <div class="slider">
                                        <div class="handle handle-left slider-transition" style="margin-left: 0px;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="handle handle-right slider-transition" style="margin-right: 0px; right: 72.6027%;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="slider-fill slider-transition" style="right: 72.6027%;"></div>
                                    </div>
                                </div>
                            </div>

                            <div class="rangeslide-cont">
                                <div class="htitle">
                                    <h5>Inbound Duration</h5>
                                    <p>5Hours - 9Hours</p>
                                </div>
                                <div class="rangemin_max">
                                    <strong id="min3" class="min"></strong><strong id="max3" class="max"></strong>
                                </div>
                                <div id="inDuration" class="range-wrap">
                                    <div class="slider">
                                        <div class="handle handle-left slider-transition" style="margin-left: 0px;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="handle handle-right slider-transition" style="margin-right: 0px; right: 72.6027%;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="slider-fill slider-transition" style="right: 72.6027%;"></div>
                                    </div>
                                </div>
                            </div>

                            <div class="rangeslide-cont">
                                <div class="htitle">
                                    <h5>Total Duration </h5>
                                    <p>11Hours - 15Hours</p>
                                </div>
                                <div class="rangemin_max">
                                    <strong id="min3" class="min"></strong><strong id="max3" class="max"></strong>
                                </div>
                                <div id="totalDuration" class="range-wrap">
                                    <div class="slider">
                                        <div class="handle handle-left slider-transition" style="margin-left: 0px;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="handle handle-right slider-transition" style="margin-right: 0px; right: 72.6027%;">
                                            <div class="slider-circle"></div>
                                        </div>
                                        <div class="slider-fill slider-transition" style="right: 72.6027%;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="vAccnt">
                        <h4 class="accordion-toggle bold">Arrive Time</h4>
                        <div class="accordion-content">
                            <p>Cras malesuada ultrices augue molestie risus.</p>
                        </div>
                    </div>

                    <div class="vAccnt">
                        <h4 class="accordion-toggle">Arrive Time</h4>
                        <div class="accordion-content">
                            <p>Cras malesuada ultrices augue molestie risus.</p>
                        </div>
                    </div>
                </div>
            </div>
        </aside>
        <div class="ver-content">
            <div id="overlay-results" style="">
                <div class="spinner"></div>
            </div>
            <div class="sortby-wrap" id="FiltersssTop">
                <ul>
                    <li id="price" onclick="sortbyprice()" data-content="DESC" style="pointer-events: none; cursor: not-allowed; opacity: 0.3;">
                        <div class="sortby-inner" data-toggle="popover" data-content="Cheapest Fare Flights">
                            <div class="iconSort"><i id="pricesorticon" class="fa fa-sort-amount-asc" aria-hidden="true"></i></div>
                            <h5>Cheapest Price</h5>
                            <div class="sortby-list-hide">
                                <span id="minPrice" class="green fare">No Flights Available
                                </span>
                            </div>
                        </div>
                    </li>
                    <li id="Dura" onclick="sortbytripduration()" style="pointer-events: none; cursor: not-allowed; opacity: 0.3;">
                        <div class="sortby-inner" data-toggle="popover" data-content="Sort by Shortest/Direct duration">
                            <div class="iconSort"><i id="Durationsorticon" class="fa fa-clock-o" aria-hidden="true"></i></div>
                            <h5>Shortest Duration</h5>
                            <div class="sortby-list-hide">
                                <span id="minDuration" class="green fare">No Flights Available
                                </span>
                            </div>
                        </div>
                    </li>
                    <li id="exact" onclick="exact()" data-content="DESC" style="pointer-events: none; cursor: not-allowed; opacity: 0.3;">
                        <div class="sortby-inner" data-toggle="popover" data-content="Exact Dates">
                            <div class="iconSort"><i class="fa fa-calendar-check-o" aria-hidden="true"></i></div>
                            <h5>Exact Date</h5>
                            <div class="sortby-list-hide"><span id="minExact" class="green fare">No Exact Flights</span></div>
                        </div>
                    </li>
                    <li id="flexi" onclick="flexible()" data-content="DESC" style="pointer-events: none; cursor: not-allowed; opacity: 0.3;">
                        <div class="sortby-inner" data-toggle="popover" data-content="Flexible Dates">
                            <div class="iconSort"><i class="fa     fa-calendar-check-o" aria-hidden="true"></i></div>
                            <h5>Flexible Date</h5>
                            <div class="sortby-list-hide"><span id="minFlex" class="green fare">No Flexible Flights</span></div>
                        </div>
                    </li>
                </ul>
            </div>
            <p></p>
            <div class="" id="loading-bar" style="/* display: none; */">
                <div class="resultsLoading">
                    <p>Results Loading...</p>
                    <div class="loader">
                        <div class="load">
                            <div class="bar" style="left: 708.228px;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- <div class="bg-primary loading load-bg" id="loading-bar">
                <p>Results Loading...</p>
                <div class="loader">
                    <div class="load">
                        <div class="bar"></div>
                    </div>
                </div>
            </div>--%>


            <div class="alert alert-danger" id="errmsg" style="display: none" runat="server">

                <h4 class="text-center mrgtop">

                    <i class="fa fa-exclamation-triangle"></i>No results found for your selection.</h4>

            </div>

            

            <div class="panel-group" id="accordion">
                <div class="panel results-list-items" style="height: 300px; background-color: lightgray;">
                </div>
            </div>


            <div id="loadGf"></div>
        </div>
    </section>
    <div class="noteMessage" id="noteMessage">
    </div>

    <div id="foot"></div>

    <script type="text/javascript" src="Design/js/jquery.min.js"></script>
    <script src="Design/js/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" src="Design/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="Design/js/omni-slider.js"></script>

    <script type="text/javascript" src="Design/build/js/moment.min.js"></script>
    <script type="text/javascript" src="Design/build/js/caleran.min.js"></script>

    <script type="text/javascript" src="Design/js/indscript.js"></script>
    <script src="js/scratchpad.js" type="text/javascript"></script>
    <script type="text/javascript">

        if ("<%=newsearch%>" != '') {
            $('#dialog-message').modal({
                backdrop: 'true'
            });
        }


        var obj;
        var pricesortorder = "ASC";
        var tripdurationsortorder = "ASC";
        var sorttype = "price";
        var onlyFlexible = "All";
        var min = 0;
        var max = 0;

        var priceLowerLimit = 0;
        var priceUpperLimit = 0;

        var minHours = 1;
        var maxHours = 99;

        var durationLowerLimit = 0;
        var durationUpperLimit = 6000;

        var outminHours = 1;
        var outmaxHours = 99;

        var outdurationLowerLimit = 0;
        var outdurationUpperLimit = 6000;

        var inminHours = 1;
        var inmaxHours = 99;

        var indurationLowerLimit = 0;
        var indurationUpperLimit = 6000;

        var minOutDeptTime = 0;
        var maxOutDeptTime = 0;

        var outdepttimeLowerLimit = 0;
        var outdepttimeUpperLimit = 0;

        var minInDeptTime = 0;
        var maxInDeptTime = 0;

        var indepttimeLowerLimit = 0;
        var indepttimeUpperLimit = 0;

        var minOutDeptTime = 0;
        var maxOutDeptTime = 0;

        var outdepttimeLowerLimit = 0;
        var outdepttimeUpperLimit = 0;

        var minOutArrTime = 0;
        var maxOutArrTime = 0;

        var outarrtimeLowerLimit = 0;
        var outarrtimeUpperLimit = 0;

        var minInArrTime = 0;
        var maxInArrTime = 0;

        var inarrtimeLowerLimit = 0;
        var inarrtimeUpperLimit = 0;

        var pagecount = 0;

        var Jtype ='<%=JType %>';

        var pagesize = 10;
        var start = 0;
        var end = 10;
        var currentpage = 1;
        var resultscount = 0;
        var pagecount1 = 0;
        var searchesmade = [];

        $("#traveler").click(function (event) {
            event.stopImmediatePropagation();
        });
        $(document).click(function (dd) {
            var ddd = dd.target;
            if (ddd.parentElement != null) {
                if (ddd.parentElement.id != "traveler") {
                    if ($("#traveller-bx").hasClass('selected')) {
                        deselect($("#traveller-bx"));
                    }
                }
                var ddd = dd.target;
                if (ddd.parentElement.lastElementChild.id == "cal") {
                    if ($(".tripType-tab").hasClass("active")) {
                    }
                    var href = $(".tripType-tabs");
                    var hr = href.children.length;
                    var href1 = href[0].childNodes[1];
                    var href2 = href[0].childNodes[3];

                    if (href1.className != "tripType-tab active") {
                        if (href2.hash == "#Oneway") {
                            $("#cal1").caleran({
                                target: $("#departReturnDate"),
                                singleDate: true,
                                autoCloseOnSelect: true,
                                minDate: moment().subtract(1, "days").startOf("day"),
                                maxDate: moment().add(365, "days").endOf("day"),
                                addClass: "active",
                            });
                        }
                    }
                    else {
                        $("#cal1").caleran({
                            target: $("#departReturnDate"),
                            autoCloseOnSelect: true,
                            minDate: moment().subtract(1, "days").startOf("day"),
                            maxDate: moment().add(365, "days").endOf("day"),
                            addClass: "active",
                        });
                    }
                }
            }
        });

        window.onload = function () {
            <%--imagename = '<%=Destination%>';
            var imageurl = '/images/pax_Back/' + imagename + '.jpg';
            var image = 'url(../images/pax_Back/' + imagename + '.jpg)';
            isUrlExists(imageurl, function (status) {
                if (status === 200) {
                    $("#BackImgcity").css("background-image", image);
                }
                else if (status === 404) {
                    $("#BackImgcity").css("background-image", "url()");
                }
            });

            function isUrlExists(url, cb) {
                jQuery.ajax({
                    url: url,
                    dataType: 'text',
                    type: 'GET',
                    complete: function (xhr) {
                        if (typeof cb === 'function')
                            cb.apply(this, [xhr.status]);
                    }
                });
            }--%>

            //$("#Loading").css('display', 'block')
            document.getElementById("accordion").style.height = "300px";
            var TempScratch = getScratch("Scratch");
            if (TempScratch != "") {
                searchesmade = eval(TempScratch);
                if (!searchesmade) {
                    searchesmade = JSON.parse(result);
                }
            }
            if (searchesmade.length > 4) {
                removesearch();
            }
            //display(searchesmade);
            btn_getFlights();
            $("#foot").load("foot1.aspx");
            //bindvalues();
            //loadscratch();
        }

        function btn_getFlights() {
            ret = FlightResults.getFlights(document.getElementById('SearchRef').value, '<%=JType %>', '<%= track_id%>', OnCompleteCurrent, OnTimeOut, OnError);
        }

        function btn_ShowRefresh() {
            if (document.getElementById('isExpired').value == "yes") {
                $("#Refresh1").css("display", "block");
            }
            else {
                $("#Refresh1").css("display", "none");
            }
        }

        function OnCompleteCurrent(result) {
            if (result != "") {
                obj = eval('(' + result + ')');
                if (!obj) {
                    obj = JSON.parse(result);
                }
                var objFares = obj[0];
                var objResults = obj[1];
                var objAirlines = obj[2];
                var objAirports = obj[3];
                var objStops = obj[4];
                var objOutboundVia = obj[8];
                var objInboundVia = obj[9];
                var filters1 = "";

                if (objFares != null) {
                    if (objFares.Fares.length > 0) {
                        var filters = "";
                        if (document.getElementById('isDone').value == "1") {
                            document.getElementById("loading-bar").style.display = "none";
                            var foc = $("#newsletteremail").is(':focus');
                            //if ()
                            if ("<%=newsearch%>" != '') {
                                var dia = $('#dialog-message');
                                if (dia[0].className == "modal fade in") {
                                    if (!foc) {
                                        $('#dialog-message').modal('toggle');
                                        //clearInterval(interval);
                                        //clearInterval(interval1);
                                    }
                                    else {
                                        if ($("#newsletteremail").val() == "") {
                                            setTimeout(function () {
                                                if ($("#newsletteremail").val() == "") {
                                                    $('#dialog-message').modal('toggle');
                                                }
                                                else {
                                                    seconds1 = 10;
                                                    setInterval(function () {
                                                        seconds1--;
                                                        if (seconds1 == 0) {
                                                            $('#dialog-message').modal('toggle');
                                                        }
                                                    }, 1000);
                                                }
                                                //clearInterval(interval);
                                                //clearInterval(interval1);
                                            }, 5000);
                                        }
                                        else {
                                            seconds1 = 10;
                                            setInterval(function () {
                                                seconds1--;
                                                if (seconds1 == 0) {
                                                    $('#dialog-message').modal('toggle');
                                                }
                                            }, 1000);
                                        }
                                    }
                                }
                            }
                            if ("<%=message%>" != '') {
                                document.getElementById("errmsg").style.display = "";

                            }
                            var timeout = <%=timeout%>;
                            SessionExpireAlert(timeout);
                        }

                        //Price
                        if (document.getElementById('isDone').value == "1") {
                            if (objFares != null) {
                                if (objFares.Fares.length > 0) {

                                    filters1 += '<div class="verHeadtitle"><a class="close filterClose slide-toggle" id="closeFilter"><span aria-hidden="true" onclick="slidetogle()">×</span><span class="sr-only">Close</span></a></div>' +
                                        '<div class="vAccnt">' +
                                        '<h4 class="accordion-toggle bold" id="Priceslidtoggle" onclick="slidtoggle(this, Pricecontent)">Price</h4>' +
                                        '<div class="accordion-content" id="Pricecontent" style="display: block;">' +
                                        '<div class="rangeslide-cont">' +
                                        '<div class="rangemin_max">' +
                                        '<strong id="min5" class="min"></strong>' +
                                        '<strong id="max5" class="max"></strong>' +
                                        '</div>' +
                                        '<div id="slider5" class="range-wrap"></div>' +
                                        '</div>' +
                                        '</div>' +
                                        '</div>';
                                }
                            }
                        }

                        //Airlines Completed
                        if (objAirlines.Airlines.length > 0) {

                            filters1 += '<div class="vAccnt">' +
                                '<h4 class="accordion-toggle bold" id="Airlinesslidtoggle" onclick="slidtoggle(this,Airlinescontent)"> Airlines</h4>' +
                                '<div class="accordion-content" id="Airlinescontent" style="display: block;">' +
                                '<ul>' +
                                '<div class="clear-all">' +
                                '<span class="" onclick="selectallairlines()"><a>Select all</a></span>' +
                                '<span class="" onclick="clearallairlines()"><a>Clear</a></span>' +
                                '</div>';
                            for (var i = 0; i < objAirlines.Airlines.length; i++) {
                                var Airline = objAirlines.Airlines[i];
                                if (i < 5) {
                                    filters1 += '<li>' +
                                        '<input id="' + Airline.AirCode + '" checked="checked" class="checkbox-custom" onchange="Airline()" name="' + Airline.Airline + '" type="checkbox">' +
                                        '<label for="' + Airline.AirCode + '" class="checkbox-custom-label"> ' + Airline.Airline + '</label>' +
                                        '<span class="vfText pull-right">$' + Airline.TtFare + '</span>' +
                                        '</li>';
                                }
                                else {
                                    if (i == 5) {
                                        filters1 += '<div id="target-Airlines" style="display: block;">';
                                    }
                                    filters1 += '<li>' +
                                        '<input id="' + Airline.AirCode + '" checked="checked" class="checkbox-custom" onchange="Airline()" name="' + Airline.Airline + '" type="checkbox">' +
                                        '<label for="' + Airline.AirCode + '" class="checkbox-custom-label"> ' + Airline.Airline + '</label>' +
                                        '<span class="vfText pull-right">$' + Airline.TtFare + '</span>' +
                                        '</li>';

                                    if (i == objAirlines.Airlines.length - 1) {
                                        filters1 += '</div>';
                                    }
                                }
                                ////Airlines += '<li>' +
                                ////    '<input id="' + Airline.Airline + '" class="checkbox-custom" name="' + Airline.Airline + '" type="checkbox" >' +
                                ////    '<label for="' + Airline.Airline + '" class="checkbox-custom-label"> ' + Airline.Airline + '</label>' +
                                ////    '<span class="vfText pull-right">$' + Airline.TtFare + '</span>' +
                                ////    '</li>';
                            }
                            if (objAirlines.Airlines.length > 6) {
                                filters1 += "<div class='showAll'><a onclick='$(\"#target-Airlines\").toggle();$(this).text(($(this).text()==\"Show all airlines\")?\"Show less airlines\":\"Show all airlines\");'>Show less airlines</a></div>";
                            }
                            filters1 += '</ul></div></div>';
                        }

                        //Stops
                        if (objStops.Stops.length > 0) {
                            filters1 += '<div class="vAccnt">' +
                                '<h4 class="accordion-toggle bold" id="FlightStopsslidtoggle" onclick="slidtoggle(this,FlightStopscontent)">Flight Stops</h4>' +
                                '<div class="accordion-content" id="FlightStopscontent" style="display: block;">' +
                                '<div class="tm-radio-buttons-wrapper">';
                            objStops.Stops.sort(function (a, b) { return parseInt(a.stops) - parseInt(b.stops) });
                            var stopcount0 = 0;
                            var stopcount1 = 0;
                            var stopcount2 = 0;
                            var stopcount = 0;

                            var stopsss0 = "";
                            var stopsss1 = "";
                            var stopsss2 = "";
                            var stopsss3 = "";

                            for (var i = 0; i < objStops.Stops.length; i++) {
                                var Stop = objStops.Stops[i];
                                if (parseInt(Stop.stops) <= 2) {
                                    var stopname = "";
                                    if (Stop.stops == "0") {
                                        stopname = "0";
                                        stopsss0 = '<input type="checkbox" checked=checked class="tm-radio-button" name="radioButtonTest" id="' + Stop.stops + '"  onchange="Stop()" />' +
                                            '<label for="' + Stop.stops + '">' +
                                            '<p class="stopText bbline">' + stopname + '</p>' +
                                            '<p class="stopPrice">$' + Stop.TtFare + '</p>' +
                                            '</label>';
                                        stopcount0++;
                                    }
                                    else {
                                        if (stopcount0 == 0) {
                                            stopsss0 = '<input type="checkbox" class="tm-radio-button" name="0" id="0"  disabled/>' +
                                                '<label for="0">' +
                                                '<p class="stopText bbline">0</p>' +
                                                '<p class="stopPrice">-</p>' +
                                                '</label>';
                                        }
                                    }
                                    if (Stop.stops == "1") {
                                        stopname = "1";
                                        stopsss1 = '<input type="checkbox" checked=checked  class="tm-radio-button" name="radioButtonTest" id="' + Stop.stops + '" onchange="Stop()" />' +
                                            '<label for="' + Stop.stops + '">' +
                                            '<p class="stopText bbline">' + stopname + '</p>' +
                                            '<p class="stopPrice">$' + Stop.TtFare + '</p>' +
                                            '</label>';
                                        stopcount1++;
                                    }
                                    else {
                                        if (stopcount1 == 0) {
                                            stopsss1 = '<input type="checkbox"  class="tm-radio-button" name="1" id="1"  disabled/>' +
                                                '<label for="1">' +
                                                '<p class="stopText bbline">1</p>' +
                                                '<p class="stopPrice">-</p>' +
                                                '</label>';
                                        }
                                    }
                                    if (Stop.stops == "2") {
                                        stopname = "2";
                                        stopsss2 = '<input type="checkbox" checked=checked  class="tm-radio-button" name="radioButtonTest" id="' + Stop.stops + '"  onchange="Stop()" />' +
                                            '<label for="' + Stop.stops + '">' +
                                            '<p class="stopText bbline">' + stopname + '</p>' +
                                            '<p class="stopPrice">$' + Stop.TtFare + '</p>' +
                                            '</label>';
                                        stopcount2++;
                                    }
                                    else {
                                        if (stopcount2 == 0) {
                                            stopsss2 = '<input type="checkbox"  class="tm-radio-button" name="2" id="2"  disabled/>' +
                                                '<label for="2">' +
                                                '<p class="stopText bbline">2</p>' +
                                                '<p class="stopPrice">-</p>' +
                                                '</label>';
                                        }
                                    }
                                    if (Stop.stops > "2") {
                                        stopname = "2+";
                                        stopsss3 = '<input type="checkbox" checked=checked  class="tm-radio-button" name="radioButtonTest" id="' + Stop.stops + '"  onchange="Stop()" />' +
                                            '<label for="' + Stop.stops + '">' +
                                            '<p class="stopText bbline">' + stopname + '</p>' +
                                            '<p class="stopPrice">$' + Stop.TtFare + '</p>' +
                                            '</label>';
                                        stopcount++;
                                    }
                                    else {
                                        if (stopcount == 0) {
                                            stopsss3 = '<input type="checkbox"  class="tm-radio-button" name="2+" id="2+"  disabled/>' +
                                                '<label for="2+">' +
                                                '<p class="stopText bbline">2+</p>' +
                                                '<p class="stopPrice">-</p>' +
                                                '</label>';
                                        }
                                    }
                                }
                            }
                            filters1 += stopsss0 + stopsss1 + stopsss2 + stopsss3
                            filters1 += '</div>' +
                                '</div>' +
                                '</div>';
                        }

                        //Duration
                        if (document.getElementById('isDone').value == "1") {
                            if (objFares != null) {
                                if (objFares.Fares.length > 0) {
                                    filters1 += '<div class="vAccnt">' +
                                        '<h4 class="accordion-toggle bold"  id="Durationslidtoggle" onclick="slidtoggle(this,Durationcontent)"> Duration</h4>';
                                    filters1 += '<div class="accordion-content" id="Durationcontent" style="display: block;">' +
                                        '<div class="rangeslide-cont">' +
                                        '<div class="htitle" id="outdurationrangelimits" >' +

                                        '</div>' +
                                        '<div class="rangemin_max">' +
                                        '<strong id="min3" class="min">$0</strong>' +
                                        '<strong id="max3" class="max">$100</strong>' +
                                        '</div>' +
                                        '<div id="outDuration" class="range-wrap">' +

                                        '</div>' +
                                        '</div>';
                                    if (Jtype == "2") {
                                        filters1 += '<div class="rangeslide-cont">' +
                                            '<div class="htitle" id="indurationrangelimits">' +

                                            '</div> ' +
                                            '<div class="rangemin_max">' +
                                            '<strong id="min4" class="min"></strong> ' +
                                            '<strong id="max4" class="max"></strong>' +
                                            '</div>' +
                                            '<div id="inDuration" class="range-wrap">' +


                                            '</div>' +
                                            '</div> ' +
                                            '<div class="rangeslide-cont">' +
                                            '<div class="htitle" id="durationrangelimits">' +

                                            '</div>' +
                                            '<div class="rangemin_max">' +
                                            '<strong id="min6" class="min"></strong>' +
                                            '<strong id="max6" class="max"></strong>' +
                                            '</div>' +
                                            '<div id="totalDuration" class="range-wrap">' +

                                            '</div>' +
                                            '</div>';
                                    }
                                    filters1 += '</div>';
                                    filters1 += '</div>';
                                }
                            }
                        }

                        //Outbound Via Completed
                        if (objOutboundVia.OutboundVia.length > 0) {

                            filters1 += '<div class="vAccnt">' +
                                '<h4 class="accordion-toggle bold" id="OutboundViaPointsslidtoggle"  onclick="slidtoggle(this,OutboundViaPointscontent)"><%=DepartureCity%> - <%=DestinationCity%> Via Points</h4>' +
                                '<div class="accordion-content" id="OutboundViaPointscontent" style="display: block;">' +
                                '<ul>' +
                                '<div class="clear-all">' +
                                '<span class="" onclick="selectallOutboundVia()"><a>Select all</a></span>' +
                                '<span class="" onclick="clearallOutboundVia()"><a>Clear</a></span>' +
                                '</div>';

                            for (var i = 0; i < objOutboundVia.OutboundVia.length; i++) {
                                var OutboundVia = objOutboundVia.OutboundVia[i];
                                if (i < 5) {
                                    filters1 += '<li>' +
                                        '<input id="Out-' + OutboundVia.AirPortcode + '" checked="checked" class="checkbox-custom" onchange="OutBoundVia()" name="' + OutboundVia.AirPortcode + '" type="checkbox">' +
                                        '<label for="Out-' + OutboundVia.AirPortcode + '" class="checkbox-custom-label"> ' + OutboundVia.CityName.split(',')[1] + '</label>' +
                                        '<span class="vfText pull-right">' + OutboundVia.AirPortcode + '</span>' +
                                        '</li>';
                                }
                                else {
                                    if (i == 5) {
                                        filters1 += '<div id="target-OutboundVia" style="display: block;">';
                                    }
                                    filters1 += '<li>' +
                                        '<input id="Out-' + OutboundVia.AirPortcode + '" checked="checked" class="checkbox-custom" onchange="OutBoundVia()" name="' + OutboundVia.AirPortcode + '" type="checkbox">' +
                                        '<label for="Out-' + OutboundVia.AirPortcode + '" class="checkbox-custom-label"> ' + OutboundVia.CityName.split(',')[1] + '</label>' +
                                        '<span class="vfText pull-right">' + OutboundVia.AirPortcode + '</span>' +
                                        '</li>';

                                    if (i == objOutboundVia.OutboundVia.length - 1) {
                                        filters1 += '</div>';
                                    }
                                }
                            }
                            if (objOutboundVia.OutboundVia.length > 5) {
                                filters1 += "<div class=\"showAll\"><a onclick='$(\"#target-OutboundVia\").toggle();$(this).text(($(this).text()==\"Show all Outbound Via Points\")?\"Show less Outbound Via Points\":\"Show all Outbound Via Points\");'>Show less Outbound Via Points</a></div>";
                            }
                            filters1 += '</ul></div></div>';
                        }

                        //In boundVia Completed
                        if (objInboundVia.InboundVia.length > 0) {

                            filters1 += '<div class="vAccnt">' +
                                '<h4 class="accordion-toggle bold" id="InboundViaPointsslidtoggle" onclick="slidtoggle(this,InboundViaPointscontent)"> <%=DestinationCity%> - <%=DepartureCity%> Via Points</h4>' +
                                '<div class="accordion-content" id="InboundViaPointscontent" style="display: block;">' +
                                '<ul>' +
                                '<div class="clear-all">' +
                                '<span class="" onclick="selectallInboundVia()"><a>Select all</a></span>' +
                                '<span class="" onclick="clearallInboundVia()"><a>Clear</a></span>' +
                                '</div>';

                            for (var i = 0; i < objInboundVia.InboundVia.length; i++) {
                                var InboundVia = objInboundVia.InboundVia[i];
                                if (i < 5) {
                                    filters1 += '<li>' +
                                        '<input id="In-' + InboundVia.AirPortcode + '" checked="checked" class="checkbox-custom" onchange="InBoundVia()" name="' + InboundVia.AirPortcode + '" type="checkbox">' +
                                        '<label for="In-' + InboundVia.AirPortcode + '" class="checkbox-custom-label"> ' + InboundVia.CityName.split(',')[1] + '</label>' +
                                        '<span class="vfText pull-right">' + InboundVia.AirPortcode + '</span>' +
                                        '</li>';
                                }
                                else {
                                    if (i == 5) {
                                        filters1 += '<div id="target-InboundVia" style="display: block;">';
                                    }
                                    filters1 += '<li>' +
                                        '<input id="In-' + InboundVia.AirPortcode + '" checked="checked" class="checkbox-custom" onchange="InBoundVia()" name="' + InboundVia.AirPortcode + '" type="checkbox">' +
                                        '<label for="In-' + InboundVia.AirPortcode + '" class="checkbox-custom-label"> ' + InboundVia.CityName.split(',')[1] + '</label>' +
                                        '<span class="vfText pull-right">' + InboundVia.AirPortcode + '</span>' +
                                        '</li>';

                                    if (i == objInboundVia.InboundVia.length - 1) {
                                        filters1 += '</div>';
                                    }
                                }
                            }
                            if (objInboundVia.InboundVia.length > 5) {
                                filters1 += "<div class=\"showAll\"><a onclick='$(\"#target-InboundVia\").toggle();$(this).text(($(this).text()==\"Show all Inbound Via Points\")?\"Show less Inbound Via Points\":\"Show all Inbound Via Points\");'>Show less Inbound Via Points</a></div>";
                            }
                            filters1 += '</ul></div></div>';
                        }

                        //Depart Time
                        if (document.getElementById('isDone').value == "1") {
                            filters1 += '<div class="vAccnt">' +
                                '<h4 class="accordion-toggle bold" id="OutboundDepartTimeslidtoggle" onclick="slidtoggle(this,OutboundDepartTimecontent)">Take of at <%=DepartureCity%></h4>' +
                                '<div class="accordion-content" id="OutboundDepartTimecontent" style="display: block;">' +
                                '<div class="tm-radio-buttons-wrapper">' +
                                '<input type="checkbox" onchange="DepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest1" value="00-06" id="Outbound-em" checked />' +
                                '<label for="Outbound-em" data-toggle="tooltip" data-original-title="Late Night Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-sun-rise"></i></p>' +
                                '<p class="stopPrice">00 - 06</p>' +
                                '</label>' +
                                '<input type="checkbox" onchange="DepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest1" value="06-12" id="Outbound-mg" />' +
                                '<label for="Outbound-mg" data-toggle="tooltip" data-original-title="Show Morning Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-sun-alt"></i></p>' +
                                '<p class="stopPrice">06 - 12</p>' +
                                '</label>' +
                                '<input type="checkbox" onchange="DepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest1" value="12-18" id="Outbound-noon" />' +
                                '<label for="Outbound-noon" data-toggle="tooltip" data-original-title="Show Afternoon Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-sun-set"></i></p>' +
                                '<p class="stopPrice">12 - 18</p>' +
                                '</label>' +
                                '<input type="checkbox" onchange="DepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest1" value="18-24" id="Outbound-night" />' +
                                '<label for="Outbound-night" data-toggle="tooltip" data-original-title="Show Night Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-night"></i></p>' +
                                '<p class="stopPrice">18 - 00</p>' +
                                '</label>' +
                                '</div>' +
                                '</div>' +
                                '</div>';
                            if (Jtype == "2") {
                                filters1 += '<div class="vAccnt">' +
                                    '<h4 class="accordion-toggle bold" id="InboundDepartTimeslidtoggle" onclick="slidtoggle(this,InboundDepartTimecontent)">Take of at <%=DestinationCity%></h4>' +
                                    '<div class="accordion-content" id="InboundDepartTimecontent" style="display: block;">' +
                                    '<div class="tm-radio-buttons-wrapper">' +
                                    '<input type="checkbox" onchange="InDepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest4" value="00-06" id="Inbound-em" checked />' +
                                    '<label for="Inbound-em" data-toggle="tooltip" data-original-title="Late Night Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-sun-rise"></i></p>' +
                                    '<p class="stopPrice">00 - 06</p>' +
                                    '</label>' +
                                    '<input type="checkbox" onchange="InDepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest4" value="06-12" id="Inbound-mg" />' +
                                    '<label for="Inbound-mg" data-toggle="tooltip" data-original-title="Show Morning Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-sun-alt"></i></p>' +
                                    '<p class="stopPrice">06 - 12</p>' +
                                    '</label>' +
                                    '<input type="checkbox" onchange="InDepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest4" value="12-18" id="Inbound-noon" />' +
                                    '<label for="Inbound-noon" data-toggle="tooltip" data-original-title="Show Afternoon Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-sun-set"></i></p>' +
                                    '<p class="stopPrice">12 - 18</p>' +
                                    '</label>' +
                                    '<input type="checkbox" onchange="InDepartTime()" checked=checked class="tm-radio-button" name="radioButtonTest4" value="18-24" id="Inbound-night" />' +
                                    '<label for="Inbound-night" data-toggle="tooltip" data-original-title="Show Night Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-night"></i></p>' +
                                    '<p class="stopPrice">18 - 00</p>' +
                                    '</label>' +
                                    '</div>' +
                                    '</div>' +
                                    '</div>';
                            }
                        }

                        //Arrival Time
                        if (document.getElementById('isDone').value == "1") {
                            filters1 += '<div class="vAccnt">' +
                                '<h4 class="accordion-toggle bold" id="InboundDepartTimeslidtoggle" onclick="slidtoggle(this,InboundDepartTimecontent)">Landing at <%=DestinationCity%></h4>' +
                                '<div class="accordion-content" id="InboundDepartTimecontent" style="display: block;">' +
                                '<div class="tm-radio-buttons-wrapper">' +
                                '<input type="checkbox" onchange="ArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest2" value="00-06" id="Outboundav-em" checked />' +
                                '<label for="Outboundav-em" data-toggle="tooltip" data-original-title="Late Night Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-sun-rise"></i></p>' +
                                '<p class="stopPrice">00 - 06</p>' +
                                '</label>' +
                                '<input type="checkbox" onchange="ArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest2" value="06-12" id="Outboundav-mg" />' +
                                '<label for="Outboundav-mg" data-toggle="tooltip" data-original-title="Show Morning Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-sun-alt"></i></p>' +
                                '<p class="stopPrice">06 - 12</p>' +
                                '</label>' +
                                '<input type="checkbox" onchange="ArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest2" value="12-18" id="Outboundav-noon" />' +
                                '<label for="Outboundav-noon" data-toggle="tooltip" data-original-title="Show Afternoon Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-sun-set"></i></p>' +
                                '<p class="stopPrice">12 - 18</p>' +
                                '</label>' +
                                '<input type="checkbox" onchange="ArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest2" value="18-24" id="Outboundav-night" />' +
                                '<label for="Outboundav-night" data-toggle="tooltip" data-original-title="Show Night Flights">' +
                                '<p class="stopText bbline"><i class="icofont icofont-night"></i></p>' +
                                '<p class="stopPrice">18 - 00</p>' +
                                '</label>' +
                                '</div>' +
                                '</div>' +
                                '</div>';
                            if (Jtype == "2") {
                                filters1 += '<div class="vAccnt">' +
                                    '<h4 class="accordion-toggle bold" id="InboundArriveTimeslidtoggle" onclick="slidtoggle(this,InboundArriveTimecontent)">Landing at <%=DepartureCity%></h4>' +
                                    '<div class="accordion-content" id="InboundArriveTimecontent" style="display: block;">' +
                                    '<div class="tm-radio-buttons-wrapper">' +
                                    '<input type="checkbox" onchange="InArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest3" value="00-06" id="Inboundav-em" checked />' +
                                    '<label for="Inboundav-em" data-toggle="tooltip" data-original-title="Late Night Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-sun-rise"></i></p>' +
                                    '<p class="stopPrice">00 - 06</p>' +
                                    '</label>' +
                                    '<input type="checkbox" onchange="InArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest3" value="06-12" id="Inboundav-mg" />' +
                                    '<label for="Inboundav-mg" data-toggle="tooltip" data-original-title="Show Morning Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-sun-alt"></i></p>' +
                                    '<p class="stopPrice">06 - 12</p>' +
                                    '</label>' +
                                    '<input type="checkbox" onchange="InArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest3" value="12-18" id="Inboundav-noon" />' +
                                    '<label for="Inboundav-noon" data-toggle="tooltip" data-original-title="Show Afternoon Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-sun-set"></i></p>' +
                                    '<p class="stopPrice">12 - 18</p>' +
                                    '</label>' +
                                    '<input type="checkbox" onchange="InArriveTime()" checked=checked class="tm-radio-button" name="radioButtonTest3" value="18-24" id="Inboundav-night" />' +
                                    '<label for="Inboundav-night" data-toggle="tooltip" data-original-title="Show Night Flights">' +
                                    '<p class="stopText bbline"><i class="icofont icofont-night"></i></p>' +
                                    '<p class="stopPrice">18 - 00</p>' +
                                    '</label>' +
                                    '</div>' +
                                    '</div>' +
                                    '</div>';
                            }
                        }



                        document.getElementById("Filters").innerHTML = filters1;
                        change();

                        $("#mbOpen").unbind('click');
                        $("#mbOpen").click(function () {
                            $(".csbox").slideToggle();
                        });

                        $("#mbOpenFilter").unbind('click');
                        $("#mbOpenFilter").click(function () {
                            $(".box").slideToggle();
                        });





                        //$(".slide-toggle").click(function () {
                        //    $(".box").slideToggle();
                        //});
                        //$(".cs-toggle").click(function () {
                        //    $(".csbox").slideToggle();
                        //});

                        var SortingOptions = '<ul>' +
                            '<li id="price" onclick="sortbyprice()" data-content="DESC">' +
                            '<div class="sortby-inner" data-toggle="popover" data-content="Cheapest Fare Flights">' +
                            '<div class="iconSort"><i id="pricesorticon" class="fa fa-sort-amount-asc" aria-hidden="true"></i> </div>' +
                            '<h5>Cheapest Price</h5>' +

                            '<div class="sortby-list-hide">' +
                            '<span id="minPrice" class="green fare"></span>' +
                            //'<span id="minPriceAirlines" class="decName"></span>' +
                            '</div>' +
                            '</div>' +
                            '</li>' +
                            '<li id="Dura" onclick="sortbytripduration()">' +
                            '<div class="sortby-inner" data-toggle="popover" data-content="Sort by Shortest/Direct duration">' +
                            '<div class="iconSort"><i id="Durationsorticon" class="fa fa-clock-o" aria-hidden="true"></i> </div>' +
                            '<h5>Shortest Duration</h5>' +
                            '<div class="sortby-list-hide">' +
                            '<span id="minDuration" class="green fare"></span>' +
                            //'<span id="minDurationAirlines" class="decName"></span>' +
                            '</div>' +
                            '</div>' +
                            '</li>' +
                            '<li id="exact" onclick="exact()" data-content="DESC">' +
                            '<div class="sortby-inner" data-toggle="popover" data-content="Exact Dates">' +
                            '<div class="iconSort"><i class="fa fa-calendar-check-o" aria-hidden="true"></i> </div>' +
                            '<h5>Exact Date</h5>' +
                            '<div class="sortby-list-hide">' +
                            '<span id="minExact" class="green fare"></span>' +
                            //'<span id="minExactAirlines" class="decName"></span>' +
                            '</div>' +
                            '</div>' +
                            '</li>' +
                            '<li id="flexi" onclick="flexible()" data-content="DESC">' +
                            '<div class="sortby-inner" data-toggle="popover" data-content="Flexible Dates">' +
                            '<div class="iconSort"><i class="fa     fa-calendar-check-o" aria-hidden="true"></i> </div>' +
                            '<h5>Flexible Date</h5>' +
                            '<div class="sortby-list-hide">' +
                            '<span id="minFlex" class="green fare"></span>' +
                            //'<span id="minFlexAirlines" class="decName"></span>' +
                            '</div>' +
                            '</div>' +
                            '</li>' +
                            '</ul>';
                        document.getElementById("FiltersssTop").innerHTML = SortingOptions;
                        var splitDate = $("#DepartureDate").val().toString().split('/');
                        var weekday = new Array(7);
                        weekday[0] = "Sun";
                        weekday[1] = "Mon";
                        weekday[2] = "Tue";
                        weekday[3] = "Wed";
                        weekday[4] = "Thu";
                        weekday[5] = "Fri";
                        weekday[6] = "Sat";
                        var Month = new Array(12);
                        Month[0] = "Jan";
                        Month[1] = "Feb";
                        Month[2] = "Mar";
                        Month[3] = "Apr";
                        Month[4] = "May";
                        Month[5] = "Jun";
                        Month[6] = "Jul";
                        Month[7] = "Aug";
                        Month[8] = "Sep";
                        Month[9] = "Oct";
                        Month[10] = "Nov";
                        Month[11] = "Dec";
                        var Dept_Date = new Date(splitDate[2], parseInt(splitDate[0]) - 1, splitDate[1]);
                        document.getElementById("noteMessage").innerHTML = '<button type="button" class="close" data-dismiss="noteMessage">x</button>' +
                            '<div class="msgIcon"><i class="fa fa-plane"></i></div>' +
                            '<div class="msgTitle">' +
                            'Flights to ' + $("#Destination").val() + ' on ' + Dept_Date.getDate() + ' ' + Month[Dept_Date.getMonth()] + ', ' + Dept_Date.getFullYear() + ' are selling out quickly! Many of our cheapest fares have ' + objFares.Fares[0].SeatsAvail + ' seats left.' +
                            '</div>';
                        //document.getElementById("noteMessage").style.display = 'block';


                    }
                    else {
                        document.getElementById("Filters").innerHTML = filters1;
                    }
                }
                else {
                    document.getElementById("Filters").innerHTML = filters1;
                }
                if (objFares == null) {
                    pleasewait();
                }
                else {
                    if (objFares.Fares.length <= 0) {
                        pleasewait();
                    }
                    else {
                        priceorder("ASC", objFares.Fares);

                        min = parseInt(objFares.Fares[0].Amount);
                        max = parseInt(objFares.Fares[objFares.Fares.length - 1].Amount);

                        priceLowerLimit = parseInt(min) - 1;
                        priceUpperLimit = parseInt(max) + 1;

                        outdepttimeLowerLimit = 0 * 60;
                        outdepttimeUpperLimit = 24 * 60;

                        indepttimeLowerLimit = 0 * 60;
                        indepttimeUpperLimit = 24 * 60;

                        outarrtimeLowerLimit = 0 * 60;
                        outarrtimeUpperLimit = 24 * 60;

                        inarrtimeLowerLimit = 0 * 60;
                        inarrtimeUpperLimit = 24 * 60;
                        change();
                    }
                }

                if (document.getElementById('isDone').value == "1") {
                    if (objFares != null) {
                        if (objFares.Fares.length > 0) {

                            document.getElementById("Filters").className = '';

                            document.getElementById("Filters").style.opacity = "1";
                            document.getElementById("Filters").style.cursor = "";
                            document.getElementById("Filters").style.pointerEvents = "";
                            //pointer - events: none
                            //style = "pointer-events: none; cursor: not-allowed;
                            priceorder("ASC", objFares.Fares);
                            var minfare = parseFloat(objFares.Fares[0].Amount);
                            var ciid = objFares.Fares[0].ciid
                            var inid = objFares.Fares[0].inid
                            var outid = objFares.Fares[0].outid
                            min = parseInt(objFares.Fares[0].Amount);
                            max = parseInt(objFares.Fares[objFares.Fares.length - 1].Amount);

                            priceLowerLimit = parseInt(min) - 1;
                            priceUpperLimit = parseInt(max) + 1;

                            // -=-=-=-=- Price SLIDER 5 -=-=-=-=-
                            var slider5 = new Slider(document.getElementById('slider5'), {
                                isOneWay: false,
                                min: parseInt(parseInt(min) - 1),
                                max: parseInt(parseInt(max) + 1)
                            });
                            slider5.subscribe('moving', function (data) {
                                document.getElementById('min5').innerHTML = '$' + Math.round(parseInt(data.left) - 1);
                                document.getElementById('max5').innerHTML = '$' + Math.round(parseInt(data.right) + 1);
                            });
                            slider5.subscribe('stop', function (data) {
                                document.getElementById('min5').innerHTML = '$' + Math.round(data.left);
                                document.getElementById('max5').innerHTML = '$' + Math.round(data.right + 1);
                                priceLowerLimit = parseInt(data.left);
                                priceUpperLimit = parseInt(data.right) + 1;
                                change();
                            });
                            var min5 = document.getElementById('min5');
                            var max5 = document.getElementById('max5');
                            var slide6 = document.getElementById('slider5');
                            document.getElementById('min5').innerHTML = '$' + Math.round(slider5.getInfo().left);
                            document.getElementById('max5').innerHTML = '$' + Math.round(slider5.getInfo().right);

                            var search = {
                                Dept: document.getElementById("txtFlyFromFO_hidden").value,
                                Dest: document.getElementById("txtFlyToFO_hidden").value,
                                DepDate: document.getElementById("DepartureDate").value,
                                Jtype: Jtype,
                                ArrDate: document.getElementById("ReturnDate").value,
                                Adults: document.getElementById("Adultss").value,
                                Children: document.getElementById("Childss").value,
                                Infants: document.getElementById("Infantss").value,
                                Class: document.getElementById("CabinClass").value,
                                Airlines: document.getElementById("Airlines").value,
                                Flexibility: document.getElementById("Flexibility").value,
                            };
                            var search1 = {
                                Dept: document.getElementById("txtFlyFromFO_hidden").value,
                                Dest: document.getElementById("txtFlyToFO_hidden").value,
                                DepDate: document.getElementById("DepartureDate").value,
                                Jtype: Jtype,
                                ArrDate: document.getElementById("ReturnDate").value,
                                Adults: document.getElementById("Adultss").value,
                                Children: document.getElementById("Childss").value,
                                Infants: document.getElementById("Infantss").value,
                                Class: document.getElementById("CabinClass").value,
                                Airlines: document.getElementById("Airlines").value,
                                Flexibility: document.getElementById("Flexibility").value,
                                fare: min5
                            };
                            var index = CheckIndexof(searchesmade, search);
                            if (index != -1)
                                Update(searchesmade, search1, 365);
                            setScratch("Scratch", JSON.stringify(searchesmade), 365);
                            if (Jtype == "2") {
                                tripdurationorder("ASC", objFares.Fares);
                                var tripduration = parseInt(objFares.Fares[0].tripduration);
                                if (tripduration.length > 4) {
                                    tripduration = pad(tripduration.toString(), 5);
                                } else {
                                    tripduration = pad(tripduration.toString(), 4);
                                }
                                if (tripduration.length > 4) {
                                    minHours = parseInt(tripduration.substring(0, 3));
                                } else {
                                    minHours = parseInt(tripduration.substring(0, 2));
                                }
                                if (tripduration.length > 4) {
                                    minMinutes = parseInt(tripduration.substring(3, 5));
                                }
                                else {
                                    minMinutes = parseInt(tripduration.substring(2, 4));
                                }

                                tripduration = parseInt(objFares.Fares[objFares.Fares.length - 1].tripduration);;

                                if (tripduration.length > 4) {
                                    tripduration = pad(tripduration.toString(), 5);
                                } else {
                                    tripduration = pad(tripduration.toString(), 4);
                                }
                                if (tripduration.length > 4) {
                                    maxHours = parseInt(tripduration.substring(0, 3));
                                } else {
                                    maxHours = parseInt(tripduration.substring(0, 2));
                                }

                                if (tripduration.length > 4) {
                                    maxMinutes = parseInt(tripduration.substring(3, 5));
                                }
                                else {
                                    maxMinutes = parseInt(tripduration.substring(2, 4));
                                }

                                //maxHours = parseInt(tripduration.substring(0, 2));
                                //var maxMinutes = parseInt(tripduration.substring(2, 4));

                                $('#durationrangelimits').html('<h5>Total Duration</h5>' /*+ '<p>' + (parseInt(minHours) - 1).toString().substring(0, 2) + ' Hours - ' + (parseInt(maxHours) + 1).toString().substring(0, 2) + ' Hours</p>'*/);
                                //$('#durationrangelimits').html("" + (parseInt(minHours) - 1).toString().substring(0, 2) + "Hours - " + (parseInt(maxHours) + 1).toString().substring(0, 2) + "Hours");




                                durationLowerLimit = (parseInt(minHours)) * 60;
                                durationUpperLimit = (parseInt(maxHours) + 1) * 60;


                                // -=-=-=-=- totalDuration -=-=-=-=- min6 max6 min4 max4 outDuration inDuration
                                var totalDuration = new Slider(document.getElementById('totalDuration'), {
                                    isOneWay: false,
                                    min: durationLowerLimit,
                                    max: durationUpperLimit
                                });
                                totalDuration.subscribe('moving', function (data) {
                                    var lh = (data.left) / 60;
                                    lh = lh.toString().split('.')[0];
                                    var lr = (data.right) / 60;
                                    lr = lr.toString().split('.')[0];
                                    document.getElementById('min6').innerHTML = lh + " Hours";
                                    document.getElementById('max6').innerHTML = lr + " Hours";
                                });
                                totalDuration.subscribe('stop', function (data) {
                                    var lh = (data.left) / 60;
                                    lh = lh.toString().split('.')[0];
                                    var lr = (data.right) / 60;
                                    lr = lr.toString().split('.')[0];
                                    document.getElementById('min6').innerHTML = lh + " Hours";
                                    document.getElementById('max6').innerHTML = lr + " Hours";
                                    durationLowerLimit = parseInt(parseInt(lh) * 60);
                                    durationUpperLimit = (parseInt(parseInt(lr) + 1) * 60);
                                    change();
                                });
                                var min5 = document.getElementById('min6');
                                var max5 = document.getElementById('max6');
                                var slide6 = document.getElementById('totalDuration');
                                var lh = (totalDuration.getInfo().left) / 60;
                                lh = lh.toString().split('.')[0];
                                var lr = (totalDuration.getInfo().right) / 60;
                                lr = lr.toString().split('.')[0];
                                document.getElementById('min6').innerHTML = lh + " Hours";
                                document.getElementById('max6').innerHTML = lr + " Hours";
                            }
                            outtripdurationorder("ASC", objFares.Fares);


                            var outtripduration = parseInt(objFares.Fares[0].outtripduration);

                            if (outtripduration.length > 4) {
                                outtripduration = pad(outtripduration.toString(), 5);
                            } else {
                                outtripduration = pad(outtripduration.toString(), 4);
                            }
                            if (outtripduration.length > 4) {
                                outminHours = parseInt(outtripduration.substring(0, 3));
                            } else {
                                outminHours = parseInt(outtripduration.substring(0, 2));
                            }
                            if (outtripduration.length > 4) {
                                outminMinutes = parseInt(outtripduration.substring(3, 5));
                            }
                            else {
                                outminMinutes = parseInt(outtripduration.substring(2, 4));
                            }

                            //outtripduration = pad(outtripduration.toString(), 4);
                            //outminHours = parseInt(outtripduration.substring(0, 2));
                            //var outminMinutes = parseInt(outtripduration.substring(2, 4));


                            outtripduration = parseInt(objFares.Fares[objFares.Fares.length - 1].outtripduration);

                            if (outtripduration.length > 4) {
                                outtripduration = pad(outtripduration.toString(), 5);
                            } else {
                                outtripduration = pad(outtripduration.toString(), 4);
                            }
                            if (outtripduration.length > 4) {
                                outmaxHours = parseInt(outtripduration.substring(0, 3));
                            } else {
                                outmaxHours = parseInt(outtripduration.substring(0, 2));
                            }
                            if (outtripduration.length > 4) {
                                outmaxMinutes = parseInt(outtripduration.substring(3, 5));
                            }
                            else {
                                outmaxMinutes = parseInt(outtripduration.substring(2, 4));
                            }

                            //outtripduration = pad(outtripduration.toString(), 4);
                            //outmaxHours = parseInt(outtripduration.substring(0, 2));
                            //var outmaxMinutes = parseInt(outtripduration.substring(2, 4));

                            $('#outdurationrangelimits').html('<h5><%=DepartureCity%> - <%=DestinationCity%> Duration</h5>');

                            outdurationLowerLimit = (parseInt(outminHours)) * 60;
                            outdurationUpperLimit = (parseInt(outmaxHours) + 1) * 60;

                            // -=-=-=-=- outDuration -=-=-=-=- min3 max3 outDuration inDuration
                            var outDuration = new Slider(document.getElementById('outDuration'), {
                                isOneWay: false,
                                min: outdurationLowerLimit,
                                max: outdurationUpperLimit
                            });
                            outDuration.subscribe('moving', function (data) {
                                var lh = (data.left) / 60;
                                lh = lh.toString().split('.')[0];
                                var lr = (data.right) / 60;
                                lr = lr.toString().split('.')[0];
                                document.getElementById('min3').innerHTML = lh + " Hours";
                                document.getElementById('max3').innerHTML = lr + " Hours";
                            });
                            outDuration.subscribe('stop', function (data) {
                                var lh = (data.left) / 60;
                                lh = lh.toString().split('.')[0];
                                var lr = (data.right) / 60;
                                lr = lr.toString().split('.')[0];
                                document.getElementById('min3').innerHTML = lh + " Hours";
                                document.getElementById('max3').innerHTML = lr + " Hours";
                                outdurationLowerLimit = parseInt(parseInt(lh) * 60);
                                outdurationUpperLimit = (parseInt(parseInt(lr) + 1) * 60);
                                change();
                            });
                            var min5 = document.getElementById('min3');
                            var max5 = document.getElementById('max3');
                            var slide6 = document.getElementById('outDuration');
                            var lh = (outDuration.getInfo().left) / 60;
                            lh = lh.toString().split('.')[0];
                            var lr = (outDuration.getInfo().right) / 60;
                            lr = lr.toString().split('.')[0];
                            document.getElementById('min3').innerHTML = lh + " Hours";
                            document.getElementById('max3').innerHTML = lr + " Hours";


                            if (Jtype == "2") {

                                intripdurationorder("ASC", objFares.Fares);


                                var intripduration = parseInt(objFares.Fares[0].intripduration);
                                intripduration = pad(intripduration.toString(), 4);

                                inminHours = parseInt(intripduration.substring(0, 2));
                                var inminMinutes = parseInt(intripduration.substring(2, 4));

                                intripduration = parseInt(objFares.Fares[objFares.Fares.length - 1].intripduration);
                                intripduration = pad(intripduration.toString(), 4);


                                inmaxHours = parseInt(intripduration.substring(0, 2));
                                var inmaxMinutes = parseInt(intripduration.substring(2, 4));

                                $('#indurationrangelimits').html('<h5><%=DestinationCity%> - <%=DepartureCity%> Duration</h5>');

                                indurationLowerLimit = (parseInt(inminHours)) * 60;
                                indurationUpperLimit = (parseInt(inmaxHours) + 1) * 60;
                                // -=-=-=-=- inDuration -=-=-=-=- min4 max4 outDuration inDuration
                                var inDuration = new Slider(document.getElementById('inDuration'), {
                                    isOneWay: false,
                                    min: indurationLowerLimit,
                                    max: indurationUpperLimit
                                });
                                inDuration.subscribe('moving', function (data) {

                                    var lh = (data.left) / 60;
                                    lh = lh.toString().split('.')[0];
                                    var lr = (data.right) / 60;
                                    lr = lr.toString().split('.')[0];
                                    document.getElementById('min4').innerHTML = lh + " Hours";
                                    document.getElementById('max4').innerHTML = lr + " Hours";
                                });
                                inDuration.subscribe('stop', function (data) {
                                    var lh = (data.left) / 60;
                                    lh = lh.toString().split('.')[0];
                                    var lr = (data.right) / 60;
                                    lr = lr.toString().split('.')[0];
                                    document.getElementById('min4').innerHTML = lh + " Hours";
                                    document.getElementById('max4').innerHTML = lr + " Hours";
                                    indurationLowerLimit = parseInt(parseInt(lh) * 60);
                                    indurationUpperLimit = (parseInt(parseInt(lr) + 1) * 60);
                                    change();
                                });
                                var min5 = document.getElementById('min4');
                                var max5 = document.getElementById('max4');
                                var slide6 = document.getElementById('inDuration');
                                var lh = (inDuration.getInfo().left) / 60;
                                lh = lh.toString().split('.')[0];
                                var lr = (inDuration.getInfo().right) / 60;
                                lr = lr.toString().split('.')[0];
                                document.getElementById('min4').innerHTML = lh + " Hours";
                                document.getElementById('max4').innerHTML = lr + " Hours";
                            }


                            outdepttimeLowerLimit = 0 * 60;
                            outdepttimeUpperLimit = 24 * 60;

                            if (Jtype == "2") {
                                indepttimeLowerLimit = 0 * 60;
                                indepttimeUpperLimit = 24 * 60;

                            }
                            outarrtimeLowerLimit = 0 * 60;
                            outarrtimeUpperLimit = 24 * 60;
                            if (Jtype == "2") {
                                inarrtimeLowerLimit = 0 * 60;
                                inarrtimeUpperLimit = 24 * 60;
                            }
                            change();


                            var obj1 = {};
                            obj1.fare = minfare;
                            obj1.track_id = '<%= track_id%>';
                            obj1.ciid = ciid;
                            obj1.inid = inid;
                            obj1.outid = outid;

                            $.ajax({
                                type: "POST",
                                url: "flightresults.aspx/UpdateFare",
                                data: JSON.stringify(obj1),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    // alert(msg.d);
                                }
                            });
                        }
                    }
                }
            }
        }
        function OnError(result) {
        }

        function OnTimeOut(result) {

        }
        //select all airlines
        function slidtoggle(asd, asd1) {
            $(asd).next().slideToggle('600');
            $("#" + asd1).not($(asd).next()).slideUp('600');
        }
        $(document).ready(function () {
            function rotate(selector) {
                $(selector).animate({ left: $('.load').width() }, 5000, function () {
                    $(selector).css("left", -($(selector).width()) + "px");
                    rotate(selector);
                });
            }
            rotate('.bar');
        });
        function pleasewait() {
            var r = "";
            if (document.getElementById('isDone').value != "1") {
                r += '<div class="noResult-bg">' +
                    '<div class="noResult-cell">' +
                    '<img src="Design/images/searchingFlightsIcon.png" alt="Seaching Flights Icon"></div>' +
                    '<div class="noResult-Text-cell">' +
                    '<p>Please wait while we search the airlines to find the best deals</p>' +
                    '</div>' +
                    '</div>';
                $("#overlay").css('display', 'block');
                $("#overlay-results").css('display', 'block');
            }
            else {
                document.getElementById("loading-bar").style.display = "none";
                r = '<div class="noResult-bg">' +
                    '<div class="noResult-cell">' +
                    '<img src="Design/images/Flights-not-found-Icon.png" alt="Flights Not Found Icon">' +
                    '</div>' +
                    '<div class="noResult-Text-cell">' +
                    '<p>Flights not found for the selection. pleasewait</p>' +
                    '<p>Please change search criteria to see Flight Results</p>' +
                    '</div>' +
                    '</div>';

                $("#overlay").css('display', 'none');
                $("#overlay-results").css('display', 'none');
            }


            //overlay - results
            document.getElementById("accordion").innerHTML = r;

            document.getElementById("accordion").style.height = "300px";
        }

        function getMax(arr, prop) {
            var max;
            for (var i = 0; i < arr.length; i++) {
                if (!max || parseInt(arr[i][prop]) > parseInt(max[prop]))
                    max = arr[i];
            }
            return max;
        }
        function getMin(arr, prop) {
            var max;
            for (var i = 0; i < arr.length; i++) {
                if (!max || parseInt(arr[i][prop]) < parseInt(max[prop]))
                    max = arr[i];
            }
            return max;
        }
        var min = "";
        var resss = "";
        var res1 = "";
        count1 = 1;

        var counter1 = 0;

        var max = "";
        function change() {
            document.getElementById("accordion").style.height = "300px";
            var objFares = filterFares(obj[0]);
            if (sorttype == "price") {
                priceorder(pricesortorder, objFares);
            }
            else if (sorttype == "duration") {
                tripdurationorder(tripdurationsortorder, objFares);
            }
            var objResults = obj[1];
            var objAirlines = obj[2];
            var objAirports = obj[3];
            var objStops = obj[4];

            var objMask = obj[7];
            var pageid = "";
            $("#nofoResults").text("" + objFares.length + " of " + obj[0].Fares.length + " Flights!");

            document.getElementById('accordion').innerHTML = "";
            if (objFares.length > 0) {
                min = getMin(objFares, "Amount");
                max = getMax(objFares, "Amount");

                document.getElementById('minPrice').innerHTML = min.Amount + " USD ";

                tripdurationorder1("ASC", objFares);
                var trduration = parseInt(objFares[0].tripduration);
                if (sorttype == "duration") {
                    tripdurationorder(tripdurationsortorder, objFares);
                }
                else if (sorttype == "price") {
                    priceorder(pricesortorder, objFares);
                }
                trduration = pad(trduration.toString(), 4);

                var minDuration = parseInt(trduration.substring(0, 2));
                var minMinutes = parseInt(trduration.substring(2, 4));

                var minDuration1 = parseInt(objFares[0].tripduration);
                var minam = objFares[0].Amount;

                document.getElementById('minDuration').innerHTML = (parseInt(minDuration)).toString().substring(0, 2) + "h " + (parseInt(minMinutes)).toString().substring(0, 2) + " m";

                var flexxx = [];
                var exxxact = [];
                for (var i = 0; i < objFares.length; i++) {
                    var flex = getFlexible(objFares[i].Flexibility, Jtype)
                    if (flex == "Flexible") {
                        flexxx.push(objFares[i]);
                    }
                    else {
                        exxxact.push(objFares[i]);
                    }
                }
                if (flexxx.length > 0) {
                    minFlex = getMin(flexxx, "Amount");//PriceSlider[0];
                    document.getElementById('minFlex').innerHTML = (minFlex.Amount) + ' <small>USD </small>';
                    $('#flexi').css("pointer-events", "");
                    $('#flexi').css("cursor", "");
                    $('#flexi').css("opacity", "");
                }
                else {
                    document.getElementById('minFlex').innerHTML = "No Flexible Flights";
                    $('#flexi').css("pointer-events", "none");
                    $('#flexi').css("cursor", "not-allowed");
                    $('#flexi').css("opacity", "0.3");
                }
                if (exxxact.length > 0) {
                    minExact = getMin(exxxact, "Amount");//PriceSlider[0]; 
                    document.getElementById('minExact').innerHTML = (minExact.Amount) + ' <small>USD </small>';
                    $('#exact').css("pointer-events", "");
                    $('#exact').css("cursor", "");
                    $('#exact').css("opacity", "");
                }
                else {
                    document.getElementById('minExact').innerHTML = "No Exact Flights";
                    $('#exact').css("pointer-events", "none");
                    $('#exact').css("cursor", "not-allowed");
                    $('#exact').css("opacity", "0.3");
                }

                resultscount = objFares.length;
                pagecount1 = Math.round(objFares.length / pagesize);

                if (objFares.length - pagecount1 * pagesize > 0)
                    pagecount1++;

                if (pagecount1 == 0)
                    pagecount1 = 1;

                if (currentpage > pagecount1)
                    currentpage = pagecount1;

                pagesize = objFares.length;

                var start = (currentpage - 1) * pagesize;
                if (document.getElementById('isDone').value != "1") {
                    start = pagecount;
                }

                var end = start + pagesize;

                if (end > objFares.length)
                    end = objFares.length;

                var res = "";

                if (count1 == 1) {
                    count1++;
                }
                else if (start == 0 && count1 != 1) {
                    res1 = "";
                    resss = "";
                    count1++;
                }
                //else {
                //    if (document.getElementById('isDone').value != "1") {
                //        res1 = "";
                //    }
                //}
                for (var rescnt = start; rescnt < end; rescnt++) {

                    var s = "";

                    var Fare = objFares[rescnt];
                    var fareid = Fare.fareid;
                    var fareidsplit = fareid.split("-");

                    var Results = getResults(objResults, Fare.fid);

                    var flexibility = Fare.Flexibility;
                    var flexible = flexible = getFlexible(flexibility, '<%=JType %>');

                    if (fareidsplit[15].toString().toUpperCase() == "TRUE") {

                        var objMaskAirline = getMask(objMask, fareidsplit[0].toString())
                        if (Fare.fid == objMaskAirline.fid) {
                            res1 += '<div class="panel results-list-items" id="accordian-' + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + '">';
                            res1 += "<div class='bargainFare-wrap'>";
                            res1 += "<div class='bargainFare-header'>" +
                                "<div class='bargainFare-title'>" + objMaskAirline.DealName + ": </div>" +
                                        "<div class='bargainFare-callUs'><img src='Design/images/telephoneIcon.png' alt='Telephone Icon'>  <a href='tel:<%=ContactDetails.Phone_support %>'><%=ContactDetails.Phone_support %></a></div>" +
                                "</div>";

                            res1 += "<div class='bargainFare-body'>";

                            res1 += "<p>" + objMaskAirline.Description + ": </p>";

                            res1 += "<div class='bargainFare-result'>";
                            if (Jtype == "2") {
                                res1 += "<div class='bargainFare-result-left'>";
                                //res += "<hr>";
                            }
                            else {
                                res1 += "<div class='bargainFare-result-left bargainFare-result-oneWay'>";
                            }
                            for (var i = 0; i < Results.length; i++) {

                                var Result = Results[i];



                                var date1 = new Date(Result.DDate);
                                var date2 = new Date(Result.ADate);
                                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));




                                var Dep_Airport = getAirPortName(objAirports, Result.DARP);

                                var Arr_Airport = getAirPortName(objAirports, Result.AARP);


                                var depart_Hours = parseInt(parseInt(Result.DTime) / 100);
                                var depart_time = "";
                                if (depart_Hours > 3) {
                                    depart_time = formatTime(pad((depart_Hours - 3).toString(), 2) + "00") + " - " + formatTime(pad((depart_Hours + 3).toString(), 2) + "00");
                                    //depart_time = pad((depart_Hours - 3).toString(), 2) + ":00 - " + pad((depart_Hours + 3).toString(), 2) + ":00";
                                }
                                else {
                                    depart_time = formatTime("0000") + " - " + formatTime(pad((depart_Hours + 3).toString(), 2) + "00");
                                    //depart_time = "00:00 - " + pad((depart_Hours + 3).toString(), 2) + ":00";
                                }




                                var arrival_Hours = parseInt(parseInt(Result.ATime) / 100);
                                var arrival_time = "";

                                if (diffDays > 0) {
                                    if (diffDays > 0) {
                                        arrival_time = "Next Day";
                                    }
                                    else {
                                        arrival_time = "After " + diffDays + " Days";
                                    }
                                }
                                else {
                                    if (arrival_Hours > 3) {
                                        arrival_time = formatTime(pad((arrival_Hours - 3).toString(), 2) + "00") + " - " + formatTime(pad((arrival_Hours + 3).toString(), 2) + "00");
                                        //  arrival_time = pad((arrival_Hours - 3).toString(), 2) + ":00 - " + pad((arrival_Hours + 3).toString(), 2) + ":00";
                                    }
                                    else {
                                        arrival_time = "00:00 - " + formatTime(pad((arrival_Hours + 3).toString(), 2) + "00");
                                        // arrival_time = "00:00 - " + pad((arrival_Hours + 3).toString(), 2) + ":00";
                                    }
                                }




                                var stops = "";
                                if (Result.STOPS == "0") {
                                    stops = "non-stop";
                                }
                                else if (Result.STOPS == "1") {
                                    stops = "1-stop";
                                }
                                else {
                                    stops = Result.STOPS + "-stops";
                                }



                                if (Jtype == "2") {
                                    //res += "<hr>";
                                }

                                res1 += "<div class='bargainFare-result-a'>" +
                                    "<h5>" + Result.DDate + " Departure to " + Arr_Airport.ARPName + "</h5>" +
                                    "<div class='bargainFare-segment-depart'>" +
                                    "<div class='bargainFare-code-time'>" + Dep_Airport.ARPName + " <span class='blue'>(" + Result.DARP + ")</span></div>" +
                                    "<p>Depart: " + depart_time + "</p>" +
                                    "</div>" +
                                    "<div class='bargainFare-segment-duration'>" +
                                    "<div class='bargainFare-duration'>" + stops + "</div>" +
                                    "<img src='Design/images/detail-durationicon.png' alt='duration Icon'>" +
                                    "<p class='grey'>Flight Details</p>" +
                                    "</div>" +
                                    "<div class='bargainFare-segment-arriv'>" +
                                    "<div class='bargainFare-code-time'>" + Arr_Airport.ARPName + " <span class='blue'>(" + Result.AARP + ")</span></div>" +
                                    "<p>Arrive: " + arrival_time + "</p>" +
                                    "</div>" +
                                    "</div>";
                            }
                            res1 += "</div>";
                            var classfarebg = "";
                            if (Jtype == "2") {
                                //res += "<hr>";
                                classfarebg = "farebg";
                            }
                            else {
                                classfarebg = "farebg farebg-oneseg";
                            }

                            res1 += "<div class='bargainFare-result-right'>" +
                                "<div class='" + classfarebg + "'>" +
                                "<div class='price'>" +
                                "<h4>" + Fare.Amount + " <small>USD</small></h4>" +
                                "<p>* inc. all taxes and fees</p>" +
                                "</div>" +
                                "<button type='submit' class='bknowBtn btn' onclick='javascript: flightselect(\"btnSelect\",\"" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "\")'>Book now <i class='fa fa-plane' ></i></button>" +
                                "<div class='refCode'>" +
                                'Ref : ' + fareidsplit[18] + '</span>' +
                                "</div>" +
                                "</div>" +

                                "</div>";

                            res1 += "</div>";
                            res1 += "</div>";

                            if (objMaskAirline.BookingType == "TRUE") {
                                res += "<a href='javascript:flightselect(\"btnSelect\",\"" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "\")'><button class='btn btn-sky btn-md botton-size' type='button'>Book Now</button></a>";
                            }
                            else {
                                res += "<button class='btn btn-sky btn-md botton-size' type='button'>Call Now</button>";

                            }



                            //res += "<h4 style='color: #B00;font-weight: bold;font-size: 18px; margin-top:20px; margin-bottom:0px;'>";
                            //res += "<p style='color:#002B48; font-size:13px;'>Ref : </p>";
                            //res += fareidsplit[16].toString().toUpperCase() + "</h4>";
                            //res += "</div>";



                            //res += "</div>";


                            //res += "</div>";
                            //res += "</div>";


                            //res += "</div>";

                            //res += "</li>";



                            res1 += "</div>";
                            res1 += "</div>";

                            res1 += "</div>";
                            res1 += "</div>";
                        }
                    }
                    else {

                        if (rescnt == start) {
                            pageid = 'accordian-' + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14];
                        }
                        res1 += '<div class="panel results-list-items" id="accordian-' + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + '">' +
                            '<div class="rs-segment-wrap">' +
                            '<div class="rs-segment">';

                        if (flexible == "Flexible") {

                        }


                        if (Fare.CallOnlyDiscount != "") {
                            if (Fare.MessageCOD != "") {
                            }
                            else {
                            }
                        }
                        if (fareidsplit[17] != 0)
                            res1 += '<div class="cross-ribbon"><small>USD</small> ' + fareidsplit[17] + ' Discount </div>';

                        res1 += '<div class="rs-segment-info-block">';
                        res1 += "<a class='accordion-toggle active collapsed'  data-toggle='collapse'  data-parent='#accordian-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "' href='#Flight-" + fareidsplit[12] + "-" + fareidsplit[14] + "' onclick='flightdetails(\"" + Fare.itiid + "\",\"Flight-" + fareidsplit[12] + "-" + fareidsplit[14] + "^" + parseFloat(Fare.Amount) + "^" + Fare.AdultBaggage + "-" + Fare.ChildBaggage + "-" + Fare.InfantBaggage + "\",this,\"" + Fare.outtripduration + "^" + Fare.intripduration + "\",\"" + fareidsplit[13] + "\")'>";
                        res1 += '<div class="rs-segment-a">';
                        var fromcity = "";
                        var tocity = "";
                        for (var i = 0; i < Results.length; i++) {
                            var Result = Results[i];

                            //Airline
                            var objAirline = getAirCodeNew(obj[5], Result.airID)
                            var stops = "";
                            var stopsss = "";
                            var stopclass = "stops-info flight-with-stops";
                            if (Result.STOPS == "0") {
                                stops = "non-stop";
                                stopsss = "icon icon-directFlight";
                                stopclass = "stops-info flight-with-direct";
                            }
                            else if (Result.STOPS == "1") {
                                stops = "1-stop";
                                stopsss = "icon icon-oneStop";
                            }
                            else {
                                stops = Result.STOPS + "-stops";
                                stopsss = "icon icon-twoStops";
                            }

                            //Departure
                            var Airport = getAirPortName(objAirports, Result.DARP);
                            if (i == 0) {
                                fromcity = Airport.ARPName;
                            }
                            res1 += '<div class="rs-segment-item">' +
                                '<div class="rs-segment-airlogo">' +
                                '<img class="" data-toggle="tooltip" src="Design/ShortLogo/' + Result.airID + '.gif" alt="' + objAirline.Airline + '" data-original-title="' + objAirline.Airline + '">' +
                                '<div class="airline-name">' + objAirline.Airline + '</div>' +
                                '</div>' +
                                '<div class="rs-depart-info">' +
                                '<div class="rs-place-title">' +
                                '<div class="rs-city-title">' + Airport.ARPName + '</div>' +
                                '<span class="rs-city-code " data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">' + Result.DARP + ' </span>' +
                                '</div>' +
                                '</div>' +

                                '<div class="rs-depart-arriv-time">' +
                                '<div class="rs-depart-DateTime">' +
                                '<span class="placetitle fl-city-title"><span class="place-code" title="' + Airport.ARPFullName + '" data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">' + Result.DARP + ' </span> ' + Airport.ARPName + '</span>' +
                                '<span class="flight-time">' + formatTime(Result.DTime) + '</span>' +
                                '<span class="flight-date">' + Result.DDate + '</span>' +
                                '</div>' +

                                '<div class="rs-duration-info">' +
                                '<div class="' + stopclass + '">' + stops + '</div>' +
                                '<div class="' + stopsss + '"></div>' +
                                '<div class="flight-duration"> ' + formatDuration(Result.Dur) + '</div>' +
                                '</div>';
                            Airport = getAirPortName(objAirports, Result.AARP);
                            if (i == 0) {
                                tocity = Airport.ARPName;
                            }
                            res1 += '<div class="rs-arriv-DateTime">' +
                                '<span class="placetitle fl-city-title"><span class="place-code" title="' + Airport.ARPFullName + '" data-toggle="tooltip" data-original-title="Hyderabad">' + Result.AARP + ' </span> ' + Airport.ARPName + '</span>' +
                                '<span class="flight-time">' + formatTime(Result.ATime) + '</span>' +
                                '<span class="flight-date">' + Result.ADate + '</span>' +
                                '</div>' +
                                '</div>' +

                                '<div class="rs-arrive-info">' +
                                '<div class="rs-place-title">' +
                                '<div class="rs-city-title">' + Airport.ARPName + '</div>' +
                                '<span class="rs-city-code " data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">' + Result.AARP + '</span>' +
                                '</div>' +
                                '</div>';

                            //Arrival

                            res1 += '</div>';
                        }
                        res1 += '</div>';
                        res1 += "</a>";
                        var foottt = '';
                        if (Jtype == "1") {
                            foottt = 'OnewayFoot';
                        }
                        else {
                            res1 += '';
                        }

                        res1 += '<div class="rs-footer ' + foottt + '">' +
                            '<div class="rs-fl-dts">' +
                            "<a class='' data-toggle='collapse'  data-parent='#accordian-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "' href='#Flight-" + fareidsplit[12] + "-" + fareidsplit[14] + "' onclick='flightdetails(\"" + Fare.itiid + "\",\"Flight-" + fareidsplit[12] + "-" + fareidsplit[14] + "^" + parseFloat(Fare.Amount) + "^" + Fare.AdultBaggage + "-" + Fare.ChildBaggage + "-" + Fare.InfantBaggage + "\",this,\"" + Fare.outtripduration + "^" + Fare.intripduration + "\",\"" + fareidsplit[13] + "\")'>Flight Details</a>" +
                            "<a class='' data-toggle='collapse' data-parent='#accordian-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "' href='#Fare-" + fareidsplit[12] + "-" + fareidsplit[14] + "' onclick='farerules(\"" + Fare.itiid + "\",\"Fare-" + fareidsplit[12] + "-" + fareidsplit[14] + "^" + parseFloat(Fare.Amount) + "\",this,\"" + fareidsplit[13] + "\")'>Fare Rules</a>" +
                            "<a class='' data-toggle='collapse' style='display:none'  data-parent='#accordian-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "' href='#Baggage-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "' onclick='baggagedetails1(\"" + Fare.AdultBaggage + "\",\"" + Fare.ChildBaggage + "\",\"" + Fare.InfantBaggage + "\",\"" + fromcity + "\",\"" + tocity + "\",\"Baggage-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "^" + parseFloat(Fare.Amount) + "\",this)'>Baggage</a>" +
                            '</div>';
                        ////////////'<div class="rs-Includes" id="popover-Incl"> ' +
                        ////////////'<span><i class="icofont icofont-ui-wifi"></i></span>' +
                        ////////////'<span><i class="icofont icofont-plugin"></i></span>' +
                        ////////////'<span><i class="icofont icofont-play-alt-1"></i></span>' +

                        ////////////'<div id="popover-Incl-msg" class="text-left" style="display: none;">' +
                        ////////////'<ul class="flIncludes">' +
                        ////////////'<li><i class="fa fa-wifi"></i> Wi-Fi connectivity</li>' +
                        ////////////'<li><i class="fa fa-play-circle"></i> Personal Screen with on-demand option</li>' +
                        ////////////'<li><i class="fa fa-plug"></i> Power outlet: AC outlet</li>' +
                        ////////////'<li><i class="fa fa-cutlery"></i> Seat selection during checkout</li>' +
                        ////////////'</ul>' +
                        ////////////'</div>' +
                        ////////////'</div> ' +

                        //if (Fare.AdultBaggage == "")
                        //    res1 += '<div class="rs-Includes"><span class="tootlTip" data-toggle="tooltip" title="" data-original-title="Check In Baggage: ** PC/Adult"><img src="Design/images/Check-In-Baggage-icon.png" alt="Check in baggage"></span> ** PC/Adult | <span class="tootlTip" data-toggle="tooltip" title="" data-original-title="Hand Baggage: 7 Kg/Adult"><img src="Design/images/HandBaggage-icon.png" alt="Hand baggage"></span>: 7 Kg/Adult</div>';
                        //else
                        //    res1 += '<div class="rs-Includes"><span class="tootlTip" data-toggle="tooltip" title="" data-original-title="Check In Baggage: ' + Fare.AdultBaggage + '/Adult"><img src="Design/images/Check-In-Baggage-icon.png" alt="Check in baggage"></span>' + Fare.AdultBaggage + '/Adult | <span class="tootlTip" data-toggle="tooltip" title="" data-original-title="Hand Baggage: 7 Kg/Adult"><img src="Design/images/HandBaggage-icon.png" alt="Hand baggage"></span>: 7 Kg/Adult</div>';

                        if (Fare.AdultBaggage == "")
                            res1 += '<div class="rs-Includes">Check In Baggage: NIL Baggage/Adult | Hand Baggage: NIL Baggage/Adult</div>';
                        else
                            res1 += '<div class="rs-Includes">Check In Baggage: ' + Fare.AdultBaggage + '/Adult | Hand Baggage: 7 Kg/Adult</div>';

                        //////////////if (Fare.ChildBaggage == "")
                        //////////////    res1 += '<div class="rs-Includes"><p>Check In Baggage: ** PC/Child & Hand Baggage: 7 Kg/Child</p></div>';
                        //////////////else
                        //////////////    res1 += '<div class="rs-Includes"><p>Check In Baggage: ' + Fare.ChildBaggage + '/Child & Hand Baggage: 7 Kg/Child</p></div>';

                        //////////////if (Fare.InfantBaggage == "")
                        //////////////    res1 += '<div class="rs-Includes"><p>Check In Baggage: ** PC/Infant & Hand Baggage: 7 Kg/Infant</p></div>';
                        //////////////else
                        //////////////    res1 += '<div class="rs-Includes"><p>Check In Baggage: ' + Fare.InfantBaggage + '/Infant & Hand Baggage: 7 Kg/Infant</p></div>';

                        res1 += '<div class="pull-right freeMeal">' +
                            '<a href="" class="rs-shareIcon" data-toggle="modal" data-target="#squarespaceModal' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '"><i class="fa fa-share-alt-square"></i>     Share</a>' +
                            '</div>' +
                            '</div>';
                        res1 += "</div>";

                        if (Jtype == "1") {
                            res1 += '<div class="Oneway-buy-block">';
                        }
                        else {
                            res1 += '<div class="rs-segment-buy-block">';
                        }
                        var discount = "";

                        var seats = "";
                        if (Fare.SeatsAvail != null) {
                            seats = '<span class="ticketleft"> Only ' + Fare.SeatsAvail + ' Seats left</span>';
                        }
                        //if (Jtype == '2' && fareidsplit[17] != 0)
                        //    discount += '<div class="dicount-box two">';
                        //else if (Jtype == '1' && fareidsplit[17] != 0)
                        //    discount += '<div class="dicount-box one">';

                        //if (fareidsplit[17] != 0) {
                        //    discount += '<span class="on_dicount title_shop">' + fareidsplit[17] + ' USD <br> Discount </span>' +
                        //        '</div>';
                        //    //discount += fareidsplit[17] + " USD Discount";
                        //}
                        //else {
                        //    discount = "";
                        //}
                        res1 += discount + /*Only 2 tickets left*/
                            seats +
                            '<div class="rs-price-block"> ' +
                            '<span class="rs-segment-main-price">' +
                            parseFloat(Fare.Amount) + '<small> USD</small>' +
                            '</span>' +
                            '<p>* inc. all taxes and fees</p>' +
                            '</div> ' +
                            "<button type='submit' class='bknowBtn btn' onclick='javascript: flightselect(\"btnSelect\",\"" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "\")'>Book now <i class='fa fa-plane' ></i></button>";
                        res1 += '<p class="refCode"> Ref : ' + fareidsplit[18] + '</p>';//'<span class="ticketleft"> Reference ID : ' + fareidsplit[18] + '</span>';

                        res1 += '</div>';

                        res1 += '<div class="modal fade" id="squarespaceModal' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">' +
                            '<div class="modal-dialog modal-lg">' +
                            '<div class="modal-content">' +
                            '<div class="modal-header shModal-header">' +
                            '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>' +
                            '<h3 class="modal-title shModal-title" id="lineModalLabel">Share this flight result</h3>' +
                            '</div>' +
                            '<div class="modal-body shModal-body">' +
                            '<div class="col-md-5 col-sm-6 col-xs-12">' +
                            '<div class="shareImg"><img src="Design/images/shareImg.jpg" alt="Share Img"></div>' +

                            '<div class="from-to-fare">' +
                            '<div class="sh-dep">' +
                            '<h4 id="flFrom">' + Results[0].DARP + '</h4>' +
                            '<p>' + Results[0].DDate + '</p>' +
                            '</div>' +
                            '<div class="iconArrow">' +
                            '<i class="fa fa-angle-right"></i>' +
                            '</div>' +
                            '<div class="sh-arrv">' +
                            '<h4 id="flTo">' + Results[0].AARP + '</h4>';
                        if (Jtype == "2")
                            res1 += '<p>' + Results[0].ADate + '</p>';
                        else
                            res1 += '<p>' + Results[0].DDate + '</p>';

                        res1 += '</div>' +
                            '</div>' +

                            '<div class="sh-fare">' +
                            '<h5 id="flFare' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">' + parseFloat(Fare.Amount) + ' <small>USD</small></h5>' +
                            '</div>' +
                            '</div>' +
                            '<div class="col-md-7 col-sm-6 col-xs-12">' +
                            '<form>' +
                            '<div class="form-group">' +
                            '<label for="ShareAlink">Share a link</label>' +
                            '<input type="email" class=""  placeholder="Share a link" disabled value=' + Fare.inid + '>' +
                            '</div>' +
                            '<div class="form-group tool-main-wrap">' +
                            '<label for="FromemailAddress-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">From your email address</label>' +
                            '<div class="tooltip" id="fromEmailAddresshint-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">' +
                            '<input id="fromEmailAddress-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '" type="email" class=""  placeholder="Enter your email address">' +
                            '</div>' +
                            '</div>' +
                            '<div class="form-group tool-main-wrap">' +
                            '<label for="ToemailAddress-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">To email address</label>' +
                            '<div class="tooltip" id="ToemailAddresshint-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">' +
                            '<input id="ToemailAddress-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '" type="text" placeholder="Enter email address (comma-separate up to 5)">' +
                            '</div>' +
                            '</div>' +

                            '<div class="form-group tool-main-wrap">' +
                            '<label for="sharemessage-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">Message</label>' +
                            '<div class="tooltip" id="sharemessagehint-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '">' +
                            '<textarea id="sharemessage-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '" cols="50" rows="2" placeholder="Enter a message (optional)" name="message" ></textarea>' +
                            '</div>' +
                            '</div>' +

                            '<button type="button" class="btn shareBtn pull-right" id="share-' + fareidsplit[12] + '-' + fareidsplit[13] + '-' + fareidsplit[14] + '" onclick="ShareItinirary(this)">Share</button>' +
                            '</form>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>';

                        res1 += '</div>';
                        res1 += '<div class="tab-content">';
                        res1 += "<div id='Flight-" + fareidsplit[12] + "-" + fareidsplit[14] + "' class='panel-collapse collapse'></div>";
                        res1 += "<div id='Fare-" + fareidsplit[12] + "-" + fareidsplit[14] + "' class='panel-collapse collapse'></div>";
                        res1 += "<div id='Baggage-" + fareidsplit[12] + "-" + fareidsplit[13] + "-" + fareidsplit[14] + "' class='panel-collapse collapse'></div>";

                        res1 += '</div></div></div>';
                        res1 += '</div></div></div>';
                    }
                }

                //resss = '<div class="Pagination-wrap large" id="pagination' + pagecount + '">' +
                //    '<div class="pagination">' +
                //    '<ul>';
                //for (var ii = 0; ii <= pagecount1; ii++) {
                //    if (pagecount == ii)
                //        resss += '<li class="active"><a href="#' + pageid +'"> ' + (ii + 1) + '</a ></li > ';
                //    else
                //        resss += '<li><a href="#' + pageid +'">' + (ii + 1) + '</a></li>';
                //}

                //resss += '</ul>' +
                //    '</div>' +
                //    '</div>';
                //res1.replace('<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>', '');
                //if (pagecount1 > 1) {
                //    if (start == 0)
                //        res1 += '<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>';
                //    // res1 += '<div class="" id="loadGf"></div>';
                //}
                //else {

                //    //res1 += '<div class="" id="loadGf"></div>';
                //    res1 += '<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>End of results...</p></div></div>';
                //}

                //res1 += resss;
                //resss = "";
                document.getElementById("accordion").innerHTML = res1;
                $("#overlay").css('display', 'none');
                $("#overlay-results").css('display', 'none');
                $('#price').css("pointer-events", "");
                $('#price').css("cursor", "");
                $('#price').css("opacity", "");

                $('#Dura').css("pointer-events", "");
                $('#Dura').css("cursor", "");
                $('#Dura').css("opacity", "");


                setTimeout(function () {
                    document.getElementById("accordion").style.height = "auto";
                }, 0);
                $("[data-toggle='tooltip']").tooltip();
            }
            else {
                refinesearch();
            }
            var today = new Date();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();
            var ms = today.getMilliseconds();
        }

        //Flight Details

        function baggagedetails1(AdultBaggage, ChildBaggage, InfantBaggage, deptttt, destttt, id, idddd) {
            var bag1 = '<div class="panel-body">' +
                '<div >' +
                '<div class="infoBagg">' +
                '<div class="feeList">';

            var adbaggage = "**PC";
            var chbaggage = "**PC";
            var inbaggage = "**PC";
            if (AdultBaggage != "") {
                adbaggage = AdultBaggage;
            }
            if (ChildBaggage != "") {
                chbaggage = ChildBaggage;
            }
            if (InfantBaggage != "") {
                inbaggage = InfantBaggage;
            }

            bag1 += '<div class="feeList">' +
                '<div class="rs-segment-airlogo">' +
                '<span class="divSubTitle">' + deptttt + ' - ' + destttt + ' </span>' +
                '</div>' +
                '<div class="divTable">' +
                '<div class="divHeading">' +
                '<div class="divCell">' +
                '<p>Baggage Type</p>' +
                '</div>' +
                '<div class="divCell">' +
                '<p>Check-In Baggage</p>' +
                '</div>' +
                '<div class="divCell">' +
                '<p>Hand Baggage</p>' +
                '</div>' +
                '</div>';

            bag1 += '<div class="divRow">';
            bag1 += '<div class="divCell">' +
                '<p>Adult</p>' +
                '</div>' +
                '<div class="divCell">' +
                '<p><i class="fa fa-suitcase"></i> ' + adbaggage + ' /Adult</p>' +
                '</div>';
            bag1 += '<div class="divCell">' +
                '<p><i class="fa fa-suitcase"></i> 7 Kg/Person</p>' +
                '</div>';
            bag1 += '</div>';

            if ('<%=ChildsCnt %>' != "0") {
                bag1 += '<div class="divRow">';
                bag1 += '<div class="divCell">' +
                    '<p>Child</p>' +
                    '</div>' +
                    '<div class="divCell">' +
                    '<p><i class="fa fa-suitcase"></i> ' + chbaggage + ' /Child</p>' +
                    '</div>';
                bag1 += '<div class="divCell">' +
                    '<p><i class="fa fa-suitcase"></i> 7 Kg/Person</p>' +
                    '</div>';
                bag1 += '</div>';
            }
            if ('<%=InfantsCnt %>' != "0") {

                bag1 += '<div class="divRow">';
                bag1 += '<div class="divCell">' +
                    '<p>Infant</p>' +
                    '</div>' +
                    '<div class="divCell">' +
                    '<p><i class="fa fa-suitcase"></i> ' + inbaggage + ' /Infant</p>' +
                    '</div>';
                bag1 += '<div class="divCell">' +
                    '<p><i class="fa fa-suitcase"></i> 7 Kg/Person</p>' +
                    '</div>';
                bag1 += '</div>';
            }
            bag1 += '</div>';
            bag1 += '</div>';

            var ads = id.split('^')[0].split('-');
            bag1 += '<div class="feeList">' +
                '<h5><strong>Information not available <span class="red">*</span></strong></h5>' +
                '<p>' +
                'The information presented above is as obtained from the airline reservation system.' +
                'TravelMerry does not guarantee the accuracy of this information. The baggage allowance may vary according to stop-overs,' +
                'connecting flights and changes in airline rules.' +
                '</p>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '<div class="rs-dtsFooter">' +
                '<div class="dfp pull-left">' +
                '<div class="dfPrice">' + id.split('^')[1] + ' <small>USD</small></div>' +
                '<p>* inc. all taxes and fees</p>' +
                '</div>' +
                '<div class="pull-right">' +
                "<button type='submit' class='bknowBtn btn' onclick='javascript: flightselect(\"btnSelect\",\"" + ads[1] + "-" + ads[2] + "-" + ads[3] + "\")'>Book now <i class='fa fa-plane'></i></button>" +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';

            $("#" + id.split('^')[0]).html(bag1);
            cccc(idddd);
            //$("#" + ads[0] + "-" + ads[1] + "-" + ads[2] + "-" + ads[3]).removeClass('panel-collapse collapse in');
        }

        function farerules(Itiid, id, idddd, type) {
            var ads = id.split('^')[0].split('-');
            var fares = '<div class="panel-body">' +
                '<div>' +
                '<div class="infoBagg">' +
                '<div class="infoBagg">' +
                '<h5><strong>Booking Rules:</strong></h5>' +
                //'<p><strong>Book with confidence: We offer a 4 hour Full Refund Guarantee. </strong></p>' +
                '<p>Fare is non-refundable, name change is not permitted and ticket is non-transferable.</p>' +
                '<p>Date changes will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee) and is based on availability of flight at the time of change.</p>' +
                '<p>Date change after departure must be done by the airline directly (airline penalty plus any fare difference will apply and is based on availability of flight at the time of change).</p>' +
                '<p>Routing change will incur penalty and fees (airline penalty plus any fare difference plus our processing service fee will apply and is based on availability of flight at the time of change).</p>' +
                '<p><strong>Contact our 24/7 customer service to make any changes.</strong></p>' +
                '<p>Prior to completing the booking in the <a href="/Termsandconditions.aspx" target="_blank" class="blue">Terms and Conditions</a>, you should review our service fees for exchanges, changes, refunds and cancellations.</p>' +
                '</div>' +
                '</div>' +
                '<div class="rs-dtsFooter">' +
                '<div class="dfp pull-left">' +
                '<div class="dfPrice">' + id.split('^')[1] + ' <small>USD</small></div>' +
                '<p>* inc. all taxes and fees</p>' +
                '</div>' +
                '<div class="pull-right">' +
                "<button type='submit' class='bknowBtn btn' onclick='javascript: flightselect(\"btnSelect\",\"" + ads[1] + "-" + type + "-" + ads[2] + "\")'>Book now <i class='fa fa-plane'></i></button>" +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';
            $("#" + id.split('^')[0]).html(fares);
            cccc(idddd);
            //$("#" + ads[0] + "-" + ads[1] + "-" + ads[2] + "-" + ads[3]).removeClass('panel-collapse collapse in');
        }

        function flightdetails(Itiid, id, idddd, dur, type) {
            var flightssss = '';

            var objAirports = obj[3];
            var objAirlines = obj[5];
            var objEquipment = obj[6];
            var Itiids = Itiid.toString().split("@");

            var OutItiid = Itiids[0];
            var InItiid = null;
            if (Itiids.length == 2)
                InItiid = Itiids[1];

            flightssss += '<div class="tab-pane active">' +
                '<div class="panel-body">' +
                '<div>';

            var outItiids = OutItiid.toString().split("##");
            if (outItiids.length > 0) {


                var Itinerary1 = outItiids[0];
                var Itinerary2 = outItiids[outItiids.length - 1];

                var from = Itinerary1.split("-");
                var to = Itinerary2.toString().split("-");

                var fromAirport = getAirPortName(objAirports, from[3]);
                var toAirport = getAirPortName(objAirports, to[6]);

                flightssss += '<div class="pm-triptype">' +
                    '<div class="pm-left">' +
                    '<div class="pm-ObTitle bold">' +
                    '<i class="fa fa-plane"></i> <span class="blue">' + fromAirport.ARPName + ' - ' + toAirport.ARPName + '</span>  ' + from[4] + '' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                flightssss += '<div class="pm-seg-wrap">';
                for (var i = 0; i < outItiids.length; i++) {
                    var Airport = "";
                    var Itinerary = outItiids[i];
                    var splitIti = Itinerary.toString().split("-");
                    if (i == 0) {

                    }
                    if (i != 0 && i < outItiids.length) {
                        if (splitIti[12] != "") {

                            Airport = getAirPortName(objAirports, splitIti[3])

                            flightssss += '<div class="pm-layover">' +
                                '<div class="pm-bag-info-cont text-center">' +
                                '<p><i class="icofont icofont-clock-time"></i> ' + Airport.ARPName + ' (' + splitIti[3] + ') - <span class="red">' + splitIti[12] + ' Layover</span></p>' +
                                '</div>' +
                                '</div >';

                        }
                    }
                    var Equipment = getEquipment(objEquipment, splitIti[18]);
                    var objAirline = getAirCodeNew(objAirlines, splitIti[1]);

                    flightssss += '<div class="pm-seg-content">' +
                        '<div class="pm-air-logo">' +
                        '<img src="Design/ShortLogo/' + splitIti[1] + '.gif" data-toggle="tooltip" data-original-title="' + objAirline.Airline + '" alt="Delta Airline">' +
                        '<p><span class="mr-15">' + objAirline.Airline + '</span>	<span class="mr-15">(' + splitIti[1] + '-' + splitIti[2] + ')</span>	';
                    if (splitIti[11] != "") {
                        flightssss += '<div class="">Operated by ' + splitIti[11] + '</div>';
                    }
                    flightssss += '</p></div>';


                    Airport = getAirPortName(objAirports, splitIti[3]);



                    if (splitIti[11] != "") {

                    }

                    var Departter = "";
                    if (splitIti[19] != "") {
                        Departter = '<p>Departure Terminal : ' + splitIti[19] + ' </p>';
                    }
                    flightssss += '<div class="pm-seg-A">' +
                        '<div class="pm-seg-from">' +
                        '<div class="pm-citi-code-time">' + Airport.ARPName + ' <span class="blue" data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">(' + splitIti[3] + ')</span> ' + formatTime(splitIti[5]) + '</div>' +
                        '<p>' + Airport.ARPFullName.split(',')[0] + ',</p>' +
                        '<p>' + Airport.ARPFullName.split('(')[1].split(')')[0] + '</p>' +
                        '<p>' + splitIti[4] + '</p>' +
                        '<p>' + Departter + '</p>' +
                        '</div>';

                    flightssss += '<div class="pm-duration">' +
                        '<span class="pm-dur-time">' + getHoursMinute(splitIti[17]) + '</span>' +
                        '<img src="Design/images/grey_durationicon.png" alt="Duration Icon">' +
                        '<span class="pm-dur-text">Duration</span>' +
                        '</div>';

                    var Arriveter = "";

                    if (splitIti[20] != "") {
                        Arriveter = '<p>Arrival Terminal : ' + splitIti[20] + ' </p>';
                    }

                    Airport = getAirPortName(objAirports, splitIti[6]);
                    flightssss += '<div class="pm-seg-to">' +
                        '<div class="pm-citi-code-time">' + Airport.ARPName + ' <span class="blue" data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">(' + splitIti[6] + ')</span> ' + formatTime(splitIti[8]) + '</div>' +
                        '<p>' + Airport.ARPFullName.split(',')[0] + ', </p>' +
                        '<p>' + Airport.ARPFullName.split('(')[1].split(')')[0] + '</p>' +
                        '<p>' + splitIti[7] + '</p>' +
                        '<p>' + Arriveter + '</p>' +
                        '</div>';








                    if (Equipment != null) {
                    }



                    flightssss += '<div class="pm-fl-info">' +
                        '<p>' + getCabinclass(splitIti[13]) + '</p>' +
                        '<p>Aircraft ' + Equipment.Text + ' </p>' +
                        //'<p>Meals Types:Asian vegetarian</p>'+
                        //'<p class="blue">Airline PNR: DFGJ2</p>'+
                        //'<div class="">' +
                        //'<span class="" data-toggle="tooltip" title="" data-original-title="Wi-Fi connectivity"><i class="icofont icofont-ui-wifi"></i></span>' +
                        //'<span class="" data-toggle="tooltip" title="" data-original-title="Power outlet: AC outlet"><i class="icofont icofont-plugin"></i></span>' +
                        //'<span class="" data-toggle="tooltip" title="" data-original-title="Personal Screen with on-demand option"><i class="icofont icofont-play-alt-1"></i></span>' +
                        //'</div>' +
                        '</div>';


                    if (splitIti[11] != "") {
                    }

                    flightssss += '</div>';

                    flightssss += '</div>';
                    if (i == outItiids.length - 1) {
                        if (InItiid == "") {
                            flightssss += '<div class="pm-bag-info">' +
                                '<div class="pm-bag-info-cont">';
                            var Abafg = "";
                            var Cbafg = "";
                            var Ibafg = "";
                            if (i == outItiids.length - 1)
                                Abafg = id.split('^')[2].split('-')[0];
                            if (Abafg == "")
                                flightssss += '<div class=""><p>Check In Baggage: ** PC/Adult | Hand Baggage: ** Kg/Adult</p></div>';
                            else
                                flightssss += '<div class=""><p>Check In Baggage: ' + Abafg + '/Adult | Hand Baggage: 7 Kg/Adult</p></div>';

                            if (<%=ChildsCnt%> > 0) {
                                if (i == outItiids.length - 1)
                                    Cbafg = id.split('^')[2].split('-')[1];
                                if (Cbafg == "")
                                    flightssss += '<div class=""><p>Check In Baggage: ** PC/Child | Hand Baggage: ** Kg/Child</p></div>';
                                else
                                    flightssss += '<div class=""><p>Check In Baggage: ' + Cbafg + '/Child | Hand Baggage: 7 Kg/Child</p></div>';
                            }
                            if (<%=InfantsCnt%> > 0) {

                                if (i == outItiids.length - 1)
                                    Ibafg = id.split('^')[2].split('-')[2];
                                if (Ibafg == "")
                                    flightssss += '<div class=""><p>Check In Baggage: ** PC/Infant | Hand Baggage: ** Kg/Infant</p></div>';
                                else
                                    flightssss += '<div class=""><p>Check In Baggage: ' + Ibafg + '/Infant | Hand Baggage: 7 Kg/Infant</p></div>';
                            }
                            //flightssss += '<div class="pm-right pm-bag-txt"><p>Baggage - 1Pc (23Kg)</p></div>';
                            flightssss += '</div>' +
                                '</div>';
                        }
                    }
                    if (i == outItiids.length - 1) {
                        flightssss += '<div class="pm-bag-info">' +
                            '<div class="pm-bag-info-cont">';

                        if (i == outItiids.length - 1)
                            flightssss += '<div class="pm-right greenBold">Outbound Travel Duration : ' + formatDuration(dur.split('^')[0]) + '</div>';
                        //flightssss += "<span class='pull-right'>" + objFares.Fares[0].tripduration + "</span>";
                        flightssss += '</div>' +
                            '</div>';
                    }
                }
                flightssss += '</div>';
            }

            var InItiids;

            if (InItiid != null) {
                if (InItiid != "") {

                    InItiids = InItiid.toString().split("##");

                    if (InItiids.length > 0) {

                        var Itinerary1 = InItiids[0];
                        var Itinerary2 = InItiids[InItiids.length - 1];

                        var from = Itinerary1.split("-");
                        var to = Itinerary2.toString().split("-");

                        var fromAirport = getAirPortName(objAirports, from[3]);
                        var toAirport = getAirPortName(objAirports, to[6]);



                        flightssss += '<div class="pm-triptype mt-15">' +
                            '<div class="pm-left">' +
                            '<div class="pm-ObTitle pm-inbond bold">' +
                            '<i class="fa fa-plane"></i> <span class="blue">' + fromAirport.ARPName + ' - ' + toAirport.ARPName + '</span> ' + from[7] + '' +
                            '</div>' +
                            '</div>' +
                            '</div>';

                        flightssss += '<div class="pm-seg-wrap">';
                        for (var i = 0; i < InItiids.length; i++) {
                            var Airport = "";
                            var Itinerary = InItiids[i];

                            var splitIti = Itinerary.toString().split("-");
                            if (i == 0) {

                            }

                            if (i != 0 && i < InItiids.length) {
                                if (splitIti[12] != "") {

                                    Airport = getAirPortName(objAirports, splitIti[3])

                                    flightssss += '<div class="pm-layover">' +
                                        '<div class="pm-bag-info-cont text-center">' +
                                        '<p><i class="icofont icofont-clock-time"></i> ' + Airport.ARPName + ' (' + splitIti[3] + ') - <span class="red">' + splitIti[12] + ' Layover</span></p>' +
                                        '</div>' +
                                        '</div >';

                                }
                            }





                            Airport = getAirPortName(objAirports, splitIti[3]);
                            var objAirline = getAirCodeNew(objAirlines, splitIti[1]);

                            var Equipment = getEquipment(objEquipment, splitIti[18]);

                            flightssss += '<div class="pm-seg-content">' +
                                '<div class="pm-air-logo">' +
                                '<img src="Design/ShortLogo/' + splitIti[1] + '.gif" data-toggle="tooltip" data-original-title="' + objAirline.Airline + '" alt="Delta Airline">' +
                                '<p><span class="mr-15">' + objAirline.Airline + '</span>	<span class="mr-15">(' + splitIti[1] + '-' + splitIti[2] + ')</span>	';
                            if (splitIti[11] != "") {
                                flightssss += '<div class="">Operated by ' + splitIti[11] + '</div>';
                            }
                            flightssss += '</p></div>';



                            if (splitIti[11] != "") {

                            }
                            var Departter = "";
                            if (splitIti[19] != "") {
                                Departter = '<p>Departure Terminal : ' + splitIti[19] + ' </p>';
                            }
                            var Arriveter = "";
                            if (splitIti[20] != "") {
                                Arriveter = '<p>Arrival Terminal : ' + splitIti[20] + ' </p>';
                            }

                            flightssss += '<div class="pm-seg-A">' +
                                '<div class="pm-seg-from">' +
                                '<div class="pm-citi-code-time">' + Airport.ARPName + ' <span class="blue" data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">(' + splitIti[3] + ')</span> ' + formatTime(splitIti[5]) + '</div>' +
                                '<p>' + Airport.ARPFullName.split(',')[0] + ',</p>' +
                                '<p>' + Airport.ARPFullName.split('(')[1].split(')')[0] + '</p>' +
                                '<p>' + splitIti[4] + '</p>' +
                                '<p>' + Departter + '</p>' +
                                '</div>';

                            flightssss += '<div class="pm-duration">' +
                                '<span class="pm-dur-time">' + getHoursMinute(splitIti[17]) + '</span>' +
                                '<img src="Design/images/grey_durationicon.png" alt="Duration Icon">' +
                                '<span class="pm-dur-text">Duration</span>' +
                                '</div>';

                            Airport = getAirPortName(objAirports, splitIti[6]);

                            flightssss += '<div class="pm-seg-to">' +
                                '<div class="pm-citi-code-time">' + Airport.ARPName + ' <span class="blue" data-toggle="tooltip" data-original-title="' + Airport.ARPFullName + '">(' + splitIti[6] + ')</span> ' + formatTime(splitIti[8]) + '</div>' +
                                '<p>' + Airport.ARPFullName.split(',')[0] + ', </p>' +
                                '<p>' + Airport.ARPFullName.split('(')[1].split(')')[0] + '</p>' +
                                '<p>' + splitIti[7] + '</p>' +
                                '<p>' + Arriveter + '</p>' +
                                '</div>';



                            if (Equipment != null) {
                            }



                            flightssss += '<div class="pm-fl-info">' +
                                '<p>' + getCabinclass(splitIti[13]) + '</p>' +
                                '<p>Aircraft ' + Equipment.Text + ' </p>' +
                                //'<p>Meals Types:Asian vegetarian</p>'+
                                //'<p class="blue">Airline PNR: DFGJ2</p>'+
                                //'<div class="">' +
                                //'<span class="" data-toggle="tooltip" title="" data-original-title="Wi-Fi connectivity"><i class="icofont icofont-ui-wifi"></i></span>' +
                                //'<span class="" data-toggle="tooltip" title="" data-original-title="Power outlet: AC outlet"><i class="icofont icofont-plugin"></i></span>' +
                                //'<span class="" data-toggle="tooltip" title="" data-original-title="Personal Screen with on-demand option"><i class="icofont icofont-play-alt-1"></i></span>' +
                                //'</div>' +
                                '</div>';


                            if (splitIti[11] != "") {
                            }
                            flightssss += '</div>' +
                                '</div>';
                            //flightssss += '<div class="pm-bag-info">' +
                            //    '<div class="pm-bag-info-cont">';

                            //if (i == InItiids.length - 1) {
                            //    var bafg = id.split('^')[2];
                            //    if (bafg == "")
                            //        flightssss += '<div class=""><p>Check In Baggage: ** PC/Adult & Hand Baggage: 7 Kg/Adult</p></div>';
                            //    else
                            //        flightssss += '<div class=""><p>Check In Baggage: ' + bafg + '/Adult & Hand Baggage: 7 Kg/Adult</p></div>';
                            //}
                            //flightssss += '</div>' +
                            //    '</div>';

                            if (i == InItiids.length - 1) {
                                flightssss += '<div class="pm-bag-info">' +
                                    '<div class="pm-bag-info-cont">';
                                var Abafg = "";
                                var Cbafg = "";
                                var Ibafg = "";
                                if (i == InItiids.length - 1)
                                    Abafg = id.split('^')[2].split('-')[0];
                                if (Abafg == "")
                                    flightssss += '<div class=""><p>Check In Baggage: NIL Baggage/Adult | Hand Baggage: NIL Baggage/Adult</p></div>';
                                else
                                    flightssss += '<div class=""><p>Check In Baggage: ' + Abafg + '/Adult | Hand Baggage: 7 Kg/Adult</p></div>';
                                if (<%=ChildsCnt%> > 0) {
                                    if (i == InItiids.length - 1)
                                        Cbafg = id.split('^')[2].split('-')[1];
                                    if (Cbafg == "")
                                        flightssss += '<div class=""><p>Check In Baggage: NIL Baggage/Child | Hand Baggage: NIL Baggage/Child</p></div>';
                                    else
                                        flightssss += '<div class=""><p>Check In Baggage: ' + Cbafg + '/Child | Hand Baggage: 7 Kg/Child</p></div>';
                                }
                                if (<%=InfantsCnt%> > 0) {
                                    if (i == InItiids.length - 1)
                                        Ibafg = id.split('^')[2].split('-')[2];
                                    if (Ibafg == "")
                                        flightssss += '<div class=""><p>Check In Baggage: NIL Baggage/Infant | Hand Baggage: NIL Baggage/Infant</p></div>';
                                    else
                                        flightssss += '<div class=""><p>Check In Baggage: ' + Ibafg + '/Infant | Hand Baggage: 7 Kg/Infant</p></div>';
                                }
                                if (Abafg != "") {
                                    flightssss += '<div class="pm-right pm-bag-txt"><p></p></div>';
                                }
                                else {
                                    flightssss += '<div class="pm-right pm-bag-txt"><p>** - No Baggage or Check With Airlines</p></div>';
                                }
                                flightssss += '</div>' +
                                    '</div>';

                            }
                            if (i == InItiids.length - 1) {
                                flightssss += '<div class="pm-bag-info">' +
                                    '<div class="pm-bag-info-cont">';

                                if (i == InItiids.length - 1)
                                    flightssss += '<div class="pm-right greenBold">Inbound Travel Duration : ' + formatDuration(dur.split('^')[1]) + '</div>';

                                flightssss += '</div>' +
                                    '</div>';
                            }
                        }
                        //flightssss += '<div class="pm-bag-info">' +
                        //    '<div class="pm-bag-info-cont">' +
                        //    '<div class="pm-seg-from"><i class="fa fa-suitcase"></i> 2 Adult (s)	 	1PC Piece/Person</div>' +
                        //    '<div class="pm-seg-from"><i class="fa fa-suitcase"></i> 1 Child (s)	 	1PC Piece/Person</div>' +
                        //    '<div class="pm-seg-from"><i class="fa fa-suitcase"></i> 1 Infant (s)		1PC Piece/Person</div>' +
                        //    '</div>' +
                        //    '</div>';
                        flightssss += '</div>';

                    }
                }
            }

            var ads = id.split('^')[0].split('-');
            dur = pad(dur.toString(), 4);
            var hours = parseInt(dur.substring(0, 2));
            hours = (parseInt(hours)) * 60;
            hours = Math.round((hours) / 60) + " Hours";

            var mins = parseInt(dur.substring(2, 4));
            mins = (parseInt(mins)) * 60;
            mins = Math.round((mins) / 60) + " Mins";

            flightssss += '<div class="mt-10"><span class="font12">* All the times are local to Airports </span></div>' +
                '<div class="rs-dtsFooter">' +
                '<div class="dfp pull-left">' +
                '<div class="dfPrice">' + id.split('^')[1] + ' <small>USD</small></div>' +
                '<p>* inc. all taxes and fees</p>' +
                '</div>' +
                '<div class="pull-right">' +
                "<button type='submit' class='bknowBtn btn' onclick='javascript: flightselect(\"btnSelect\",\"" + ads[1] + "-" + type + "-" + ads[2] + "\")'>Book now <i class='fa fa-plane'></i></button>" +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';


            $("#" + id.split('^')[0]).html(flightssss);
            cccc(idddd);
            $("[data-toggle='tooltip']").tooltip();
            //$("#" + ads[0] + "-" + ads[1] + "-" + ads[2] + "-" + ads[3]).removeClass('collapse in');
            //$("#" + ads[0] + "-" + ads[1] + "-" + ads[2] + "-" + ads[3]).addClass('collapse');
            //baggagedetails(Itiid, id, idddd);
        }

        function getHoursMinute(minutes) {
            var hours = parseInt(parseInt(minutes) / 60);
            var mints = parseInt(minutes) % 60;

            return pad(hours, 2) + "h " + pad(mints, 2) + "m";

        }
        function getCabinclass(p) {

            if (p == 'Y')
                return 'Economy/Coach';
            else if (p == 'C')
                return 'Bussiness';
            else if (p == 'F')
                return 'First';
            else if (p == 'S')
                return 'Premium Economy';
            else if (p == 'P')
                return 'Premium First';
            else if (p == 'J')
                return 'Premium Bussiness';

        }
        //Equipment       
        function getEquipment(Data, Code) {
            for (var i = 0; i < Data.Equipment.length; i++) {
                var Equipment = Data.Equipment[i];
                if (Equipment.IATA == Code) {
                    return Equipment;

                }

            }

            return null;
        }
        function filterFares(Data) {

            var objFaresTemp = [];
            for (var i = 0; i < Data.Fares.length; i++) {
                try {
                    var FareTemp = Data.Fares[i];
                    if (document.getElementById('isDone').value == "1") {

                        var fareid = FareTemp.fareid;
                        var fareidsplit = fareid.split("-");

                        var outviaId = FareTemp.OutBoundVia;
                        var outviaIdSplit = outviaId.split('-');
                        var outflag = false;
                        if (outviaId != "") {
                            for (var jj = 0; jj < outviaIdSplit.length - 1; jj++) {
                                if (document.getElementById("Out-" + outviaIdSplit[jj]).checked == true)
                                    outflag = true;
                            }
                        }
                        else
                            outflag = true;

                        var inflag = false;
                        var inviaId = FareTemp.InBoundVia;
                        if (inviaId != "") {
                            if (Jtype == "2") {
                                var inviaIdSplit = inviaId.split('-');
                                for (var jj = 0; jj < inviaIdSplit.length - 1; jj++) {
                                    if (document.getElementById("In-" + inviaIdSplit[jj]).checked == true)
                                        inflag = true;
                                }
                            }
                        }
                        else
                            inflag = true;
                        var stop = fareidsplit[10];
                        var mints = "";
                        var gds = fareidsplit[13];
                        var outbound_take_offtime = 0;
                        if (gds == '1P') {
                            var outbound_take_offtime = formatTime1(parseInt(fareidsplit[2])) /*parseInt(parseInt(fareidsplit[2]) / 10000);*/
                            mints = parseInt(fareidsplit[2]) % 100;
                            outbound_take_offtime = (parseInt(outbound_take_offtime.toString().split(':')[0] * 60) + parseInt(outbound_take_offtime.toString().split(':')[1]));
                        }
                        else {
                            outbound_take_offtime = parseInt(parseInt(fareidsplit[2]) / 10000);
                            mints = parseInt(fareidsplit[2]) % 100;
                            outbound_take_offtime = pad(outbound_take_offtime.toString(), 2) + pad(mints.toString(), 2);
                            outbound_take_offtime = (parseInt(outbound_take_offtime.toString().substring(0, 2)) * 60) + parseInt(outbound_take_offtime.toString().substring(2, 4));
                        }
                        var OutTakeOffflag = checkoutboundTakeoff(outbound_take_offtime);
                        //var outtakoflower = "";
                        var inbound_take_offtime = 0;
                        if (Jtype == "2") {
                            if (gds == '1P') {
                                inbound_take_offtime = formatTime1(parseInt(fareidsplit[3]))/*parseInt(parseInt(fareidsplit[3]) / 10000);*/
                                mints = parseInt(fareidsplit[3]) % 100;
                                inbound_take_offtime = (parseInt(inbound_take_offtime.toString().split(':')[0] * 60) + parseInt(inbound_take_offtime.toString().split(':')[1]));
                            }
                            else {
                                inbound_take_offtime = parseInt(parseInt(fareidsplit[3]) / 10000);
                                mints = parseInt(fareidsplit[3]) % 100;
                                inbound_take_offtime = pad(inbound_take_offtime.toString(), 2) + pad(mints.toString(), 2);
                                inbound_take_offtime = (parseInt(inbound_take_offtime.toString().substring(0, 2)) * 60) + parseInt(inbound_take_offtime.toString().substring(2, 4));
                            }
                            //var 
                            var InTakeOffflag = checkinboundTakeoff(inbound_take_offtime);
                        }
                        var outbound_landing_time = 0;
                        if (gds == '1P') {
                            outbound_landing_time = formatTime1(parseInt(fareidsplit[4]))/* parseInt(parseInt(fareidsplit[4]) / 10000);*/
                            mints = parseInt(fareidsplit[4]) % 100;
                            outbound_landing_time = (parseInt(outbound_landing_time.toString().split(':')[0] * 60) + parseInt(outbound_landing_time.toString().split(':')[1]));
                        }
                        else {
                            outbound_landing_time = parseInt(parseInt(fareidsplit[4]) / 10000);
                            mints = parseInt(fareidsplit[4]) % 100;
                            outbound_landing_time = pad(outbound_landing_time.toString(), 2) + pad(mints.toString(), 2);
                            outbound_landing_time = (parseInt(outbound_landing_time.toString().substring(0, 2)) * 60) + parseInt(outbound_landing_time.toString().substring(2, 4));
                        }
                        var Outlandingflag = checkoutboundlanding(outbound_landing_time);
                        var inbound_landing_time = 0;
                        if (Jtype == "2") {
                            if (gds == '1P') {
                                inbound_landing_time = formatTime1(parseInt(fareidsplit[5]))/* parseInt(parseInt(fareidsplit[5]) / 10000)*/;
                                mints = parseInt(fareidsplit[5]) % 100;
                                inbound_landing_time = (parseInt(inbound_landing_time.toString().split(':')[0] * 60) + parseInt(inbound_landing_time.toString().split(':')[1]));
                            }
                            else {
                                inbound_landing_time = parseInt(parseInt(fareidsplit[5]) / 10000);
                                mints = parseInt(fareidsplit[5]) % 100;
                                inbound_landing_time = pad(inbound_landing_time.toString(), 2) + pad(mints.toString(), 2);
                                inbound_landing_time = (parseInt(inbound_landing_time.toString().substring(0, 2)) * 60) + parseInt(inbound_landing_time.toString().substring(2, 4));
                            }
                            var Inlandingflag = checkinboundlanding(inbound_landing_time);

                        }

                        var tripduration = parseInt(FareTemp.tripduration);

                        tripduration = pad(tripduration.toString(), 4);
                        tripduration = (parseInt(tripduration.toString().substring(0, 2)) * 60) + parseInt(tripduration.toString().substring(2, 4));


                        var outtripduration = parseInt(FareTemp.outtripduration);

                        outtripduration = pad(outtripduration.toString(), 4);
                        outtripduration = (parseInt(outtripduration.toString().substring(0, 2)) * 60) + parseInt(outtripduration.toString().substring(2, 4));

                        if (Jtype == "2") {
                            var intripduration = parseInt(FareTemp.intripduration);

                            intripduration = pad(intripduration.toString(), 4);
                            intripduration = (parseInt(intripduration.toString().substring(0, 2)) * 60) + parseInt(intripduration.toString().substring(2, 4));

                        }
                        if (parseInt(fareidsplit[10]) > 2) {

                            stop = "2";
                        }

                        var flexibility = FareTemp.Flexibility;
                        var flexiblecheck = "All"
                        if (onlyFlexible != "All") {
                            flexiblecheck = getFlexible(flexibility, '<%=JType %>');
                        }
                        var air = document.getElementById(fareidsplit[0]).checked;
                        var airst = document.getElementById(stop).checked;
                        var pl = parseInt(fareidsplit[1]);
                        if (Jtype == "2") {

                            if (outflag == true &&
                                inflag == true &&
                                air == true &&
                                airst == true &&
                                pl >= priceLowerLimit &&
                                pl < priceUpperLimit &&
                                onlyFlexible == flexiblecheck &&
                                tripduration >= durationLowerLimit &&
                                tripduration < durationUpperLimit &&
                                outtripduration >= outdurationLowerLimit &&
                                outtripduration < outdurationUpperLimit &&
                                intripduration >= indurationLowerLimit &&
                                intripduration < indurationUpperLimit &&
                                OutTakeOffflag &&
                                InTakeOffflag &&
                                Outlandingflag &&
                                Inlandingflag

                            ) {
                                objFaresTemp.push(FareTemp);
                            }
                            else {
                                //objFaresTemp.push(FareTemp);
                                var asd = FareTemp;
                            }
                        }
                        else {
                            if (outflag == true &&
                                document.getElementById(fareidsplit[0]).checked == true &&
                                document.getElementById(stop).checked == true &&
                                parseInt(fareidsplit[1]) >= priceLowerLimit &&
                                parseInt(fareidsplit[1]) < priceUpperLimit &&
                                onlyFlexible == flexiblecheck &&
                                tripduration >= durationLowerLimit &&
                                tripduration < durationUpperLimit &&
                                outtripduration >= outdurationLowerLimit &&
                                outtripduration < outdurationUpperLimit &&
                                OutTakeOffflag &&
                                Outlandingflag
                            ) {
                                objFaresTemp.push(FareTemp);
                            }
                            else {
                                //objFaresTemp.push(FareTemp);
                            }
                        }
                    }
                    else {
                        objFaresTemp.push(FareTemp);
                    }
                }
                catch (Error) {

                }

            }


            return objFaresTemp;
        }
        function checkoutboundTakeoff(outbound_take_offtime) {
            var flag = false;
            var outtakeoflower = 0;
            var outtakeofupper = 0;
            if (document.getElementById('Outbound-em').checked == true) {
                outtakeoflower = parseInt(document.getElementById('Outbound-em').value.split('-')[0]) * 60;
                outtakeofupper = parseInt(document.getElementById('Outbound-em').value.split('-')[1]) * 60;

                if (outbound_take_offtime >= outtakeoflower && outbound_take_offtime < outtakeofupper) {
                    flag = true;
                }
                else
                    flag = false;
            }
            if (!flag) {
                if (document.getElementById('Outbound-mg').checked == true) {
                    outtakeoflower = parseInt(document.getElementById('Outbound-mg').value.split('-')[0]) * 60;
                    outtakeofupper = parseInt(document.getElementById('Outbound-mg').value.split('-')[1]) * 60;

                    if (outbound_take_offtime >= outtakeoflower && outbound_take_offtime < outtakeofupper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Outbound-noon').checked == true) {
                    outtakeoflower = parseInt(document.getElementById('Outbound-noon').value.split('-')[0]) * 60;
                    outtakeofupper = parseInt(document.getElementById('Outbound-noon').value.split('-')[1]) * 60;

                    if (outbound_take_offtime >= outtakeoflower && outbound_take_offtime < outtakeofupper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Outbound-night').checked == true) {
                    outtakeoflower = parseInt(document.getElementById('Outbound-night').value.split('-')[0]) * 60;
                    outtakeofupper = parseInt(document.getElementById('Outbound-night').value.split('-')[1]) * 60;

                    if (outbound_take_offtime >= outtakeoflower && outbound_take_offtime < outtakeofupper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            return flag;
        }
        function checkinboundTakeoff(inbound_take_offtime) {
            var flag = false;
            var indepttimeLower = 0;
            var indepttimeUpper = 0;
            if (document.getElementById('Inbound-em').checked == true) {
                indepttimeLower = parseInt(document.getElementById('Inbound-em').value.split('-')[0]) * 60;
                indepttimeUpper = parseInt(document.getElementById('Inbound-em').value.split('-')[1]) * 60;

                if (inbound_take_offtime >= indepttimeLower && inbound_take_offtime < indepttimeUpper) {
                    flag = true;
                }
                else
                    flag = false;
            }
            if (!flag) {
                if (document.getElementById('Inbound-mg').checked == true) {
                    indepttimeLower = parseInt(document.getElementById('Inbound-mg').value.split('-')[0]) * 60;
                    indepttimeUpper = parseInt(document.getElementById('Inbound-mg').value.split('-')[1]) * 60;

                    if (inbound_take_offtime >= indepttimeLower && inbound_take_offtime < indepttimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Inbound-noon').checked == true) {
                    indepttimeLower = parseInt(document.getElementById('Inbound-noon').value.split('-')[0]) * 60;
                    indepttimeUpper = parseInt(document.getElementById('Inbound-noon').value.split('-')[1]) * 60;

                    if (inbound_take_offtime >= indepttimeLower && inbound_take_offtime < indepttimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Inbound-night').checked == true) {
                    indepttimeLower = parseInt(document.getElementById('Inbound-night').value.split('-')[0]) * 60;
                    indepttimeUpper = parseInt(document.getElementById('Inbound-night').value.split('-')[1]) * 60;

                    if (inbound_take_offtime >= indepttimeLower && inbound_take_offtime < indepttimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            return flag;
        }
        function checkoutboundlanding(outbound_landing_time) {
            var flag = false;
            var outlandtimeLower = 0;
            var outlandtimeUpper = 0;
            if (document.getElementById('Outboundav-em').checked == true) {
                outlandtimeLower = parseInt(document.getElementById('Outboundav-em').value.split('-')[0]) * 60;
                outlandtimeUpper = parseInt(document.getElementById('Outboundav-em').value.split('-')[1]) * 60;

                if (outbound_landing_time >= outlandtimeLower && outbound_landing_time < outlandtimeUpper) {
                    flag = true;
                }
                else
                    flag = false;
            }
            if (!flag) {
                if (document.getElementById('Outboundav-mg').checked == true) {
                    outlandtimeLower = parseInt(document.getElementById('Outboundav-mg').value.split('-')[0]) * 60;
                    outlandtimeUpper = parseInt(document.getElementById('Outboundav-mg').value.split('-')[1]) * 60;

                    if (outbound_landing_time >= outlandtimeLower && outbound_landing_time < outlandtimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Outboundav-noon').checked == true) {
                    outlandtimeLower = parseInt(document.getElementById('Outboundav-noon').value.split('-')[0]) * 60;
                    outlandtimeUpper = parseInt(document.getElementById('Outboundav-noon').value.split('-')[1]) * 60;

                    if (outbound_landing_time >= outlandtimeLower && outbound_landing_time < outlandtimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Outboundav-night').checked == true) {
                    outlandtimeLower = parseInt(document.getElementById('Outboundav-night').value.split('-')[0]) * 60;
                    outlandtimeUpper = parseInt(document.getElementById('Outboundav-night').value.split('-')[1]) * 60;

                    if (outbound_landing_time >= outlandtimeLower && outbound_landing_time < outlandtimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            return flag;
        }
        function checkinboundlanding(inbound_landing_time) {
            var flag = false;
            var inlandtimeLower = 0;
            var inlandtimeUpper = 0;
            if (document.getElementById('Inboundav-em').checked == true) {
                inlandtimeLower = parseInt(document.getElementById('Inboundav-em').value.split('-')[0]) * 60;
                inlandtimeUpper = parseInt(document.getElementById('Inboundav-em').value.split('-')[1]) * 60;

                if (inbound_landing_time >= inlandtimeLower && inbound_landing_time < inlandtimeUpper) {
                    flag = true;
                }
                else
                    flag = false;
            }
            if (!flag) {
                if (document.getElementById('Inboundav-mg').checked == true) {
                    inlandtimeLower = parseInt(document.getElementById('Inboundav-mg').value.split('-')[0]) * 60;
                    inlandtimeUpper = parseInt(document.getElementById('Inboundav-mg').value.split('-')[1]) * 60;

                    if (inbound_landing_time >= inlandtimeLower && inbound_landing_time < inlandtimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Inboundav-noon').checked == true) {
                    inlandtimeLower = parseInt(document.getElementById('Inboundav-noon').value.split('-')[0]) * 60;
                    inlandtimeUpper = parseInt(document.getElementById('Inboundav-noon').value.split('-')[1]) * 60;

                    if (inbound_landing_time >= inlandtimeLower && inbound_landing_time < inlandtimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            if (!flag) {
                if (document.getElementById('Inboundav-night').checked == true) {
                    inlandtimeLower = parseInt(document.getElementById('Inboundav-night').value.split('-')[0]) * 60;
                    inlandtimeUpper = parseInt(document.getElementById('Inboundav-night').value.split('-')[1]) * 60;

                    if (inbound_landing_time >= inlandtimeLower && inbound_landing_time < inlandtimeUpper) {
                        flag = true;
                    }
                    else
                        flag = false;
                }
            }
            return flag;
        }
        function refinesearch() {
            var objFares = obj[0];
            if (objFares == null) {
                pleasewait();

            }
            else {
                if (objFares.Fares.length > 0) {
                    var s = "";

                    var s = "<ul class='list-group'>";
                    s += "<li>";
                    s += "<div class='flight-list-details-container'>";
                    s += "<div class='row'>";
                    s += "<div class='col-md-12'>";



                    s += "<div style='height:280px; text-align:center;'>";
                    //s+="<img src='images/no-results.png' style='opacity:0.3;'>";
                    s += "<h4 class='text-center mrgtop'>";
                    s += "No results for your filter creteria, refine your filters or click here to <a onclick='showall()'>show all</a></h4>";
                    //s+="Change your filter settings to see results, or  <a class='text-blue'>show all 214 results.</a></h4>";
                    s += "</div>"



                    s += "</div></div></div></li></ui>";
                    //document.getElementById("accordion").innerHTML = s;

                    var noResult = '<div class="noResult-bg">' +
                        '<div class="noResult-cell">' +
                        '<img src="Design/images/Flights-not-found-Icon.png" alt="Flights Not Found Icon">' +
                        '</div>' +
                        '<div class="noResult-Text-cell">' +
                        '<p>Flights not found for the selection.</p>' +
                        '<p>Please change search criteria to see Flight Results</p>' +
                        '</div>' +
                        '</div>';
                    //noResult = '<div class="noResult-bg">' +
                    //    '<div class="noResult-cell">' +
                    //    '<img src="Design/images/searchingFlightsIcon.png" alt="Seaching Flights Icon"></div>' +
                    //    '<div class="noResult-Text-cell">' +
                    //    '<p>Please wait while we search the airlines to find the best deals </p>' +
                    //    '</div>' +
                    //    '</div>';


                    $('#price').css("pointer-events", "none");
                    $('#price').css("cursor", "not-allowed");
                    $('#price').css("opacity", "0.3");

                    $('#Dura').css("pointer-events", "none");
                    $('#Dura').css("cursor", "not-allowed");
                    $('#Dura').css("opacity", "0.3");

                    $('#exact').css("pointer-events", "none");
                    $('#exact').css("cursor", "not-allowed");
                    $('#exact').css("opacity", "0.3");

                    $('#flexi').css("pointer-events", "none");
                    $('#flexi').css("cursor", "not-allowed");
                    $('#flexi').css("opacity", "0.3");
                    document.getElementById("accordion").innerHTML = noResult;
                    document.getElementById("accordion").style.height = "300px";

                    $("#overlay").css('display', 'none');
                    $("#overlay-results").css('display', 'none');
                    //$("#Loading").css('display', 'none');
                    document.getElementById("accordion").style.height = "auto";
                }
                else {
                    pleasewait();
                }
            }

            //$("#Loading").css('display', 'none');
            document.getElementById("accordion").style.height = "auto";
        }
        function sortbyprice() {
            onlyFlexible = "All";
            sorttype = "price";
            if (pricesortorder == "ASC") {
                pricesortorder = "DESC";
                document.getElementById("pricesorticon").className = "fa fa-sort-amount-asc";
            }
            else {
                pricesortorder = "ASC";
                document.getElementById("pricesorticon").className = "fa fa-sort-amount-desc";
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }
        function sortbytripduration() {


            onlyFlexible = "All";



            sorttype = "duration";
            if (tripdurationsortorder == "ASC") {
                tripdurationsortorder = "DESC";
            }
            else {
                tripdurationsortorder = "ASC";
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }
        function flexible() {
            pricesortorder = "ASC";
            tripdurationsortorder = "DESC";
            onlyFlexible = "Flexible";
            //currentpage = 1;
            //pagecount = 0;
            change();
        }
        function exact() {
            pricesortorder = "ASC";
            tripdurationsortorder = "DESC";
            onlyFlexible = "Exact";
            //currentpage = 1;
            //pagecount = 0;
            change();
        }
        //Segment Details   
        function getResults(Data, fid) {
            var Results = [];
            for (var i = 0; i < Data.Results.length; i++) {
                var Result = Data.Results[i];
                if (Result.fid == fid) {
                    Results.push(Result);
                }

            }


            return Results;
        }
        function getFlexible(flexi, jtype) {
            if (jtype == "2") {
                var splitFlexi = flexi.split('#');
                if (splitFlexi[0] == "0" && splitFlexi[1] == "0") {
                    return "Exact";
                }
                else {
                    return "Flexible";
                }
            }
            else {
                if (flexi == "0") {
                    return "Exact";
                }
                else {

                    return "Flexible";
                }


            }

        }
        //Airline Name
        function getAirCodeNew(Data, airCode) {
            for (var i = 0; i < Data.AirlinesList.length; i++) {
                var Airline = Data.AirlinesList[i];
                if (Airline.AirCode == airCode) {
                    return Airline;

                }

            }

            return null;
        }
        //Airport Name       
        function getAirPortName(Data, airportCode) {
            for (var i = 0; i < Data.Airports.length; i++) {
                var Airport = Data.Airports[i];
                if (Airport.ARPC == airportCode) {
                    return Airport;

                }

            }

            return null;
        }
        //Format Time        
        function formatTime(Time) {
            //var ch=Time.toString().split("");

            //if(ch.length==4)
            //return ch[0]+ch[1]+":"+ch[2]+ch[3];
            //else if(ch.length==3)
            //return '0'+ch[0]+":"+ch[1]+ch[2];
            //else if(ch.length==2)
            //return '0'+'0'+":"+ch[0]+ch[1];
            //else if(ch.length==1)
            //return '0'+'0'+":"+'0'+ch[1];
            //else
            //return '0'+'0'+":"+'0'+'0';


            var p = pad(Time.toString(), 4);

            var p1 = p.toString().substring(0, 2);
            var p2 = p.toString().substring(2, 4);
            var temp = parseInt(p1);
            if (temp >= 12 && temp < 24) {
                if (temp > 12) {
                    temp = temp - 12;
                }
                p1 = pad(temp.toString(), 2);
                p2 += " PM";
            }
            else {


                if (temp > 24) {
                    temp = temp - 24;

                }

                if (temp == 0 || temp == 24) {
                    temp = 12;

                }
                p1 = pad(temp.toString(), 2);




                p2 += " AM";
            }


            p = p1 + ":" + p2;
            return p;

        }
        function formatTime1(Time) {
            //var ch=Time.toString().split("");

            //if(ch.length==4)
            //return ch[0]+ch[1]+":"+ch[2]+ch[3];
            //else if(ch.length==3)
            //return '0'+ch[0]+":"+ch[1]+ch[2];
            //else if(ch.length==2)
            //return '0'+'0'+":"+ch[0]+ch[1];
            //else if(ch.length==1)
            //return '0'+'0'+":"+'0'+ch[1];
            //else
            //return '0'+'0'+":"+'0'+'0';


            var p = pad(Time.toString(), 4);

            var p1 = p.toString().substring(0, 2);
            var p2 = p.toString().substring(2, 4);
            var temp = parseInt(p1);
            if (temp >= 12 && temp < 24) {
                p1 = pad(temp.toString(), 2);
            }
            else {


                if (temp > 24) {
                    temp = temp - 24;
                }

                if (temp == 24) {
                    temp = temp - 24;
                }
                p1 = pad(temp.toString(), 2);



            }


            p = p1 + ":" + p2;
            return p;


        }

        //Mask


        function getMask(Data, airportCode) {
            for (var i = 0; i < Data.MaskAirlines.length; i++) {
                var MaskAirline = Data.MaskAirlines[i];
                if (MaskAirline.mask_aircode == airportCode) {
                    return MaskAirline;

                }

            }

            return null;
        }

        function formatDuration(Duration) {
            var ch = Duration.toString().split("");

            if (ch != "") {

                var hoursTag = "hrs";
                var minutesTag = "mns";




                if (ch.length == 4) {

                    if (parseInt(ch[0] + ch[1]) <= 1) {
                        hoursTag = "hr";
                    }
                    if (parseInt(ch[2] + ch[3]) <= 1) {
                        minutesTag = "mn";
                    }
                    return ch[0] + ch[1] + hoursTag + " " + ch[2] + ch[3] + minutesTag;
                }
                else if (ch.length == 3) {
                    if (parseInt(ch[0]) <= 1) {
                        hoursTag = "hr";
                    }
                    if (parseInt(ch[1] + ch[2]) <= 1) {
                        minutesTag = "mn";
                    }
                    return '0' + ch[0] + hoursTag + " " + ch[1] + ch[2] + minutesTag;
                }
                else if (ch.length == 2) {
                    hoursTag = "hr";
                    if (parseInt(ch[0] + ch[1]) <= 1) {
                        minutesTag = "mn";
                    }

                    return '0' + '0' + hoursTag + ch[0] + " " + ch[1] + minutesTag;
                }
                else if (ch.length == 1) {
                    hoursTag = "hr";
                    if (parseInt(ch[0]) <= 1) {
                        minutesTag = "mn";
                    }

                    return '0' + '0' + hoursTag + " " + '0' + ch[1] + minutesTag;
                }
            }
            else {
                return '-';
            }
        }
        //price order ASC or DESC        
        function priceorder(sorttype, objFares) {
            if (sorttype == "ASC") {
                objFares.sort(function (a, b) {
                    return parseFloat(a.Amount) - parseFloat(b.Amount)
                });
            }
            else {
                objFares.sort(function (a, b) {
                    return parseFloat(b.Amount) - parseFloat(a.Amount)
                });
            }
        }
        // Trip Duration sort ASC or DESC

        function tripdurationorder(sortorder, objFares) {

            if (sortorder == "ASC")
                objFares.sort(function (a, b) { return parseInt(a.tripduration) - parseInt(b.tripduration) });
            else
                objFares.sort(function (a, b) { return parseInt(b.tripduration) - parseInt(a.tripduration) });
        }
        function tripdurationorder1(sortorder, objFares) {

            if (sortorder == "ASC")
                objFares.sort(function (a, b) { return parseInt(a.tripduration) - parseInt(b.tripduration) });
            else
                objFares.sort(function (a, b) { return parseInt(b.tripduration) - parseInt(a.tripduration) });
        }
        //Out
        function outtripdurationorder(sortorder, objFares) {

            if (sortorder == "ASC")
                objFares.sort(function (a, b) { return parseInt(a.outtripduration) - parseInt(b.outtripduration) });
            else
                objFares.sort(function (a, b) { return parseInt(b.outtripduration) - parseInt(a.outtripduration) });
        }


        //In
        function intripdurationorder(sortorder, objFares) {

            if (sortorder == "ASC")
                objFares.sort(function (a, b) { return parseInt(a.intripduration) - parseInt(b.intripduration) });
            else
                objFares.sort(function (a, b) { return parseInt(b.intripduration) - parseInt(a.intripduration) });
        }





        function sortbytripduration() {


            onlyFlexible = "All";



            sorttype = "duration";
            if (tripdurationsortorder == "ASC") {
                tripdurationsortorder = "DESC";
            }
            else {
                tripdurationsortorder = "ASC";
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }

        //Show ALL

        function showall() {
            onlyFlexible = "All";
            //        pricesortorder="ASC";
            //        tripdurationsortorder="ASC";
            //        sorttype="price";

            var objAirlines = obj[2];
            for (var i = 0; i < objAirlines.Airlines.length; i++) {
                var Airline = objAirlines.Airlines[i];
                document.getElementById(Airline.AirCode).checked = true;
            }
            var objStops = obj[4];

            var count = 2;
            if (objStops.Stops.length <= 3) {
                count = objStops.Stops.length;
            }

            for (i = 0; i < count; i++) {
                var stop = objStops.Stops[i];
                if (document.getElementById(stop.stops) != null)
                    document.getElementById(stop.stops).checked = true;
            }


            sliderinit();
            //currentpage = 1;
            //pagecount = 0;
            change();
        }

        function selectallairlines() {
            onlyFlexible = "All";
            var objAirlines = obj[2];

            for (var i = 0; i < objAirlines.Airlines.length; i++) {
                var Airline = objAirlines.Airlines[i];

                document.getElementById(Airline.AirCode).checked = true;
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }


        function clearallairlines() {
            var objAirlines = obj[2];
            for (var i = 0; i < objAirlines.Airlines.length; i++) {
                var Airline = objAirlines.Airlines[i];
                document.getElementById(Airline.AirCode).checked = false;
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }

        function selectallOutboundVia() {
            var objOutboundVia = obj[8];
            for (var i = 0; i < objOutboundVia.OutboundVia.length; i++) {
                var OutboundVia = objOutboundVia.OutboundVia[i];
                document.getElementById("Out-" + OutboundVia.AirPortcode).checked = true;
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }
        function selectallInboundVia() {
            var objInboundVia = obj[9];
            for (var i = 0; i < objInboundVia.InboundVia.length; i++) {
                var InboundVia = objInboundVia.InboundVia[i];
                document.getElementById("In-" + InboundVia.AirPortcode).checked = true;
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }

        function clearallOutboundVia() {
            var objOutboundVia = obj[8];
            for (var i = 0; i < objOutboundVia.OutboundVia.length; i++) {
                var OutboundVia = objOutboundVia.OutboundVia[i];
                document.getElementById("Out-" + OutboundVia.AirPortcode).checked = false;
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }
        function clearallInboundVia() {
            var objInboundVia = obj[9];
            for (var i = 0; i < objInboundVia.InboundVia.length; i++) {
                var InboundVia = objInboundVia.InboundVia[i];
                document.getElementById("In-" + InboundVia.AirPortcode).checked = false;
            }
            //currentpage = 1;
            //pagecount = 0;
            change();
        }

        function Airline() {
            onlyFlexible = "All";
            //currentpage = 1;
            //pagecount = 0;
            //// window.scroll(0,200);
            change();
        }
        function OutBoundVia() {
            onlyFlexible = "All";
            //currentpage = 1;
            //pagecount = 0;
            //// window.scroll(0,200);
            change();
        }
        function InBoundVia() {
            onlyFlexible = "All";
            //currentpage = 1;
            //pagecount = 0;
            //// window.scroll(0,200);
            change();
        }
        //Pad Left
        function Stop() {
            onlyFlexible = "All";
            ////window.scroll(0,200);
            //currentpage = 1;
            //pagecount = 0;

            change();
        }

        function DepartTime() {
            onlyFlexible = "All";
            ////window.scroll(0,200);
            //currentpage = 1;
            //pagecount = 0;
            outdepttimeLowerLimit
            outdepttimeUpperLimit
            change();
        }
        function InDepartTime() {
            onlyFlexible = "All";
            ////window.scroll(0,200);
            //currentpage = 1;
            //pagecount = 0;
            indepttimeLowerLimit
            indepttimeUpperLimit
            change();
        }
        function ArriveTime() {
            onlyFlexible = "All";
            ////window.scroll(0,200);
            //currentpage = 1;
            //pagecount = 0;
            outarrtimeLowerLimit
            outarrtimeUpperLimit
            change();
        }
        function InArriveTime() {
            onlyFlexible = "All";
            ////window.scroll(0,200);
            //currentpage = 1;
            //pagecount = 0;
            inarrtimeLowerLimit
            inarrtimeUpperLimit
            change();
        }
        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        }
        var lastScrollTop = 0;
        var lastcount = 0;
        var counter = 0;
        //$(window).scroll(function () {
        //    var sctop = $(window).scrollTop();
        //    var ht = $(document).height() - $(window).height();

        //    var window_top = $(window).scrollTop();
        //    var footer_top = $("#foot").offset().top;
        //    if (sctop < lastScrollTop) {
        //        //if (sctop + 200 < $(document).height() - $(window).height()) {
        //        //    if (lastcount < pagecount) {
        //        //        if (pagecount < pagecount1) {
        //        //            pagecount--;
        //        //            currentpage--;
        //        //            change();
        //        //            var inht = document.getElementById("accordion").innerHTML
        //        //            inht.replace('<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>', '');
        //        //            document.getElementById("accordion").innerHTML = inht;
        //        //            $('#accordion').append('<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>');
        //        //        }
        //        //        else {
        //        //            document.getElementById("loading1").innerHTML = '<div class="resultsLoading"><p>End of Results...</p></div>';
        //        //        }
        //        //    }
        //        //    lastcount = pagecount;
        //        //}
        //    }
        //    else {
        //        if (sctop + 100 > $(document).height() - $(window).height()) {
        //            if (pagecount1 != 0 && pagecount1 > 1) {
        //                if (document.getElementById('isDone').value == "1") {
        //                    if (pagecount < pagecount1) {
        //                        document.getElementById("loading1").innerHTML = '';
        //                        if (pagecount == 0) {
        //                            var d = res1.indexOf('<p>Load more...</p></div></div>');
        //                            var d1 = res1.indexOf('<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>');
        //                            res1 = res1.substring(0, d1);
        //                        }
        //                        pagecount++;
        //                        currentpage++;
        //                        change();

        //                        var inht = document.getElementById("accordion").innerHTML
        //                        inht.replace('<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>', '');
        //                        document.getElementById("accordion").innerHTML = inht;
        //                        $('#accordion').append('<div class="" id="loading1" style=" display:inline-block; width:100%;"><div class="resultsLoading"><p>Load more...</p></div></div>');
        //                        //document.getElementById("pagination" + (pagecount - 1)).innerHTML = '';
        //                        //$("#pagination" + (pagecount - 1)).removeClass("Pagination-wrap large");
        //                    }
        //                    else {
        //                        document.getElementById("loading1").innerHTML = '<div class="resultsLoading"><p>End of Results...</p></div>';
        //                        counter++;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    lastScrollTop = sctop;
        //});

        $(function () {
            cccc = function (elm) {
                var vvv = $(elm).attr('href');
                var href = $(elm).attr('href');
                var a = href;
                var hrefff = $(elm);
                var ddd = hrefff[0].parentElement.children;
                if (ddd.length < 3) {
                    var dhref = 'a[href="' + href + '"]';
                    hrefff = $(dhref);
                    ddd = hrefff[0].parentElement.children;
                    ddd = ddd[1].children[0].children;
                }
                for (var i = 0; i < ddd.length; i++) {
                    if (ddd[i].className == "rs-footer") {
                        var aaaa = ddd[i].children[0].children; {
                            for (var j = 0; j < aaaa.length; j++) {
                                var b = aaaa[j].hash;
                                if (b != vvv)
                                    $(b).removeClass('in')
                            }
                        }
                    }
                    else {
                        var a = ddd[i].hash;
                        if (a != vvv)
                            $(a).removeClass('in')
                    }
                }
                href = 'a[href="' + href + '"]';
                $(href).focus();
            }
        });
        var theForm;
        var searchesmade = [];
        function validate(ctrl) {
            var round = $("#RoundTrip");
            var SearchType = "";
            if (round[0].checked) {
                SearchType = "ROUND TRIP";
            }
            else
                SearchType = "ONE WAY";
            var dept = "";
            var dest = "";
            var deptDate = "";
            var arrDate = "";
            var Travellers = "";

            var MultiCitydept = [];
            var MultiCitydest = [];
            var MultiCitydeptDate = [];

            if (SearchType == "ROUND TRIP") {
                dept = document.forms["flight"]["Departure"];
                dest = document.forms["flight"]["Destination"];
                deptDate = document.forms["flight"]["DepartureDate"];
                arrDate = document.forms["flight"]["ReturnDate"];
                Travellers = document.forms["flight"]["Travellers"];
            }
            else if (SearchType == "ONE WAY") {
                dept = document.forms["flight"]["Departure"];
                dest = document.forms["flight"]["Destination"];
                deptDate = document.forms["flight"]["DepartureDate"];
                arrDate = document.forms["flight"]["ReturnDate"];
                Travellers = document.forms["flight"]["Travellers"];
            }

            $("#CabinClass").val(classs);

            var flag = true;

            var flagDept = true;
            var flagDeptLen = true;
            var flagDest = true;
            var flagDestLen = true;

            var flagDeptDate = true;
            var flagArrDate = true;
            var flagTraveller = true;
            var SearchError = "";


            flagDept = validateIndexInputsLength(dept);


            if (!flagDept) {
                $("#from").append("<span id='Deptform_hint' class='form_hint'>Please Enter The Departure Airport</span>");
                $("#Deptform_hint").css("display", "block");
                flag = false;
            }

            flagDeptLen = PaymentLinkstringlength(dept, 3, 100);
            if (!flagDeptLen && flagDept) {
                $("#from").append("<span id='Deptform_hint' class='form_hint'>Departure Airport is Not valid</span>");
                $("#Deptform_hint").css("display", "block");
                flag = false;
            }
            if (flag) {
                $("#Deptform_hint").remove();
            }
            flag = true;
            flagDest = validateIndexInputsLength(dest);
            if (!flagDest) {
                $("#to").append("<span id='Toform_hint' class='form_hint'>Please Enter The Destination Airport</span>");
                $("#Toform_hint").css("display", "block");
                flag = false;
            }
            flagDestLen = PaymentLinkstringlength(dest, 3, 100);
            if (!flagDestLen && flagDest) {
                $("#to").append("<span id='Toform_hint' class='form_hint'>Destination Airport is Not valid</span>");
                $("#Toform_hint").css("display", "block");
                flag = false;
            }
            if (flag) {
                $("#Toform_hint").remove();
            }

            flagDeptDate = validateIndexInputsLength(deptDate);
            if (!flagDeptDate) {
                $("#depDate").append("<span id='deptDateform_hint' class='form_hint'>Please Enter The Departure Date</span>");
                $("#deptDateform_hint").css("display", "block");
            }
            else {
                $("#deptDateform_hint").remove();
            }

            if (SearchType != "ONE WAY") {
                flagArrDate = validateIndexInputsLength(arrDate);
                if (!flagArrDate) {
                    $("#retDate").append("<span id='arrDateform_hint' class='form_hint'>Please Enter The Return Date</span>");
                    $("#arrDateform_hint").css("display", "block");
                }
                else {
                    $("#arrDateform_hint").remove();
                }
            }
            else {
                $("#deptDateform_hint").remove();
            }
            flagTraveller = validateIndexInputsLength(Travellers);
            if (!flagTraveller) {
                $("#traveler").append("<span id='Travellersform_hint' class='form_hint'>Please select the Number of Passenger and Class</span>");
                $("#Travellersform_hint").css("display", "block");
            }
            else {
                $("#Travellersform_hint").remove();
            }
            if (flagDept && flagDeptLen && flagDest && flagDestLen && flagDeptDate && flagArrDate && flagTraveller) {
                flag = false;
                //Adultss Childss Infantss Classs
                //var trav = $('#traveller-bx').val().split(',');
                var trav = top.GlobPaxCount.split(',');
                $("#Adultss").val(parseInt(trav[0].substring(0, 1)));
                $("#Childss").val(parseInt(trav[1].substring(1, 2)));
                $("#Infantss").val(parseInt(trav[2].substring(1, 2)));
                $("#CabinClass").val(trav[3].split(' ')[1]);
            }

            if (!flagDept || !flagDeptLen || !flagDest || !flagDestLen || !flagDeptDate || !flagArrDate || !flagTraveller) {
                flag = false;
            }
            else {
                $("#txtFlyFromFO_hidden").val(dept.value.split('(')[1].split(')')[0]);
                $("#txtFlyToFO_hidden").val(dest.value.split('(')[1].split(')')[0]);
                if (SearchType != "ONE WAY")
                    $("#Triptype").val("Roundtrip");
                else
                    $("#Triptype").val("Oneway");
                if ($('#directFlights').is(":checked")) {
                    $("#chkDirectFlightsFO").val("NonStop");
                }
                if ($("#Flexibility").val() == 0) {
                    $("#Flexible").val("0");
                }
                else {
                    $("#Flexible").val($("#Flexibility").val());
                }
                var TempScratch = getScratch("Scratch");
                if (TempScratch != "") {
                    searchesmade = eval(TempScratch);
                    if (!searchesmade) {
                        searchesmade = JSON.parse(result);
                    }
                }
                var departure = "";
                var dsf = $("#Triptype").val();
                if (document.getElementById("txtFlyFromFO_hidden").value != "" && document.getElementById("txtFlyToFO_hidden").value != "") {

                    var jtype = "2";
                    if (SearchType == "ONE WAY") {
                        jtype = "1";
                    }
                    var search = {
                        Dept: document.getElementById("txtFlyFromFO_hidden").value,
                        Dest: document.getElementById("txtFlyToFO_hidden").value,
                        DepDate: document.getElementById("DepartureDate").value,
                        Jtype: jtype,
                        ArrDate: document.getElementById("ReturnDate").value,
                        Adults: document.getElementById("Adultss").value,
                        Children: document.getElementById("Childss").value,
                        Infants: document.getElementById("Infantss").value,
                        Class: document.getElementById("CabinClass").value,
                        Airlines: document.getElementById("Airlines").value,
                        Flexibility: document.getElementById("Flexibility").value,
                        depppt: document.getElementById("dept").value,
                        dessst: document.getElementById("dest").value,
                    };
                    var index = CheckIndexof(searchesmade, search);
                    if (index == -1)
                        Add(searchesmade, search);
                    setScratch("Scratch", JSON.stringify(searchesmade), 365);
                }
                flag = true;
                //$("#Errors").css("display", "none");
            }
            if (ctrl.id == "RefreshExpired1" || ctrl.id == "RefreshExpired2") {
                $("#Refresh1").css("display", "none");
                searchagainRefresh();
            }
            return /*false*/flag;
        }

        function flightselect(name, value) {

            theForm = document.forms['formServer'];
            if (!theForm) {
                theForm = document.formServer;
            }
            __doPostBack(name, value);
        }

    </script>



    <script type="text/javascript">

        function ChangeRound() {
            var round = $("#RoundTrip");
            if (round[0].checked) {
                $("#ReturnDate").removeAttr("disabled");
                $("#DepartureDate").caleran({
                    oninit: function (instance) {
                        var current = new Date('<%=depDateValue%>');
                        end = new Date();
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);

                        instance.globals.delayInputUpdate = true;
                        instance.$elem.val("<%=depDateValue%>");
                        caleranStart = instance;
                        if (startSelected == null)
                            startSelected = moment().add(days + 1, "days").startOf("day");
                        current = new Date('<%=arrDateValue%>');
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);
                        if (endSelected == null)
                            endSelected = moment().add(days + 1, "days").startOf("day");
                        instance.$elem.val(startSelected.format(caleranStart.config.format));
                    },
                    ondraw: ondrawEvent1,
                    onfirstselect: function (instance, start) {
                        startSelected = start;
                        instance.globals.startSelected = false;
                        updateInputs();
                        instance.hideDropdown();
                        caleranEnd.showDropdown();
                    },
                    minDate: moment().subtract(1, "days").startOf("day"),
                    maxDate: moment().add(365, "days").endOf("day"),
                    startDate: "<%=depDateValue%>",
                    endDate: "<%=arrDateValue%>",
                    showFooter: false
                });
                // end always selects end date.
                $("#ReturnDate").caleran({
                    oninit: function (instance) {
                        var current = new Date('<%=depDateValue%>');
                        end = new Date();
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);

                        instance.globals.delayInputUpdate = true;
                        instance.$elem.val("<%=arrDateValue%>");
                        caleranEnd = instance;
                        if (startSelected == null)
                            startSelected = moment().add(days + 1, "days").startOf("day");
                        current = new Date('<%=arrDateValue%>');
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);
                        if (endSelected == null)
                            endSelected = moment().add(days + 1, "days").startOf("day");
                        instance.$elem.val(endSelected.format(caleranStart.config.format));
                    },
                    ondraw: ondrawEvent1,
                    onfirstselect: function (instance, start) {
                        endSelected = start;
                        instance.globals.startSelected = false;
                        updateInputs();
                        instance.hideDropdown();
                    },
                    minDate: moment().subtract(1, "days").startOf("day"),
                    maxDate: moment().add(365, "days").endOf("day"),
                    startDate: "<%=depDateValue%>",
                    endDate: "<%=arrDateValue%>",
                    showFooter: false
                });
            }
            else {
                <%--var current = new Date('<%=depDateValue%>');
                end = new Date();
                diff = new Date(current - end);
                days = diff / 1000 / 60 / 60 / 24;
                days = Math.round(days);

                if (startSelected == null)
                    startSelected = moment().add(days + 1, "days").startOf("day");
                current = new Date('<%=arrDateValue%>');
                diff = new Date(current - end);
                days = diff / 1000 / 60 / 60 / 24;
                days = Math.round(days);
                if (endSelected == null)
                    endSelected = moment().add(days + 1, "days").startOf("day");--%>

                $("#ReturnDate").attr("disabled", "disabled");
                $("#DepartureDate").caleran({
                    singleDate: true, autoCloseOnSelect: true,
                    minDate: moment().subtract(1, "days").startOf("day"),
                    maxDate: moment().add(365, "days").endOf("day"),
                    startDate: "<%=depDateValue%>",
                    showFooter: false
                });
            }
        }
        function slidetogle() {
            $(".box").slideToggle();
        }
        function csToggle() {
            $(".csbox").slideToggle();
        }

        //function closeFilter() {
        //    $(".box").slideToggle();
        //}

        $(document).ready(function () {
            $(function () {
                $("[data-toggle='tooltip']").tooltip();
            });
            var target = $("#countAdult");
            var target1 = $("#countChild");
            var target2 = $("#countInfant");

            target[0].innerText = '<%=AdultsCnt%>';
            target1[0].innerText = '<%=ChildsCnt%>';
            target2[0].innerText = '<%=InfantsCnt%>';
            $("#DepartureDate").val('<%=depDateValue%>');
            $("#ReturnDate").val('<%=arrDateValue%>');
            var child = "";
            var infant = "";
            if (<%=ChildsCnt%> > 0)
                child = ', <%=ChildsCnt%> Childs';
            if (<%=InfantsCnt%> > 0)
                infant = ', <%=InfantsCnt%> Infants';
            top.GlobPaxCount = '<%=AdultsCnt%> Adults' + ', <%=ChildsCnt%> Childs' + ', <%=InfantsCnt%> Infants' + ', <%=ClassText%>';

            $("#traveller-bx").val('<%=AdultsCnt%> Adults' + child + infant + ', <%=ClassText%>');

            travelerBXAD = "<%=AdultsCnt%> Adults";
            travelerBXCH = ", <%=ChildsCnt%> Childs";
            travelerBXIN = ", <%=InfantsCnt%> Infants";

           <%-- $("#countAdult").innerHTML ='<%=AdultsCnt%>';
            $("#countChild").innerHTML='<%=ChildsCnt%>';
            $("#countInfant").innerHTML='<%=InfantsCnt%>';
            --%>
            var sdf = '<%=ClassText%>';
            sdf = sdf.split(' ')[0];
            //document.getElementById(sdf).checked = true;
            //$('input[name="ClassType"]').attr('checked', true);
            ////document.getElementsByName("ClassType").checked = 
            document.getElementById(sdf).checked = true;
            //$(sdf).prop("checked", "checked");

            // start always selects start date.
            if (<%=JType%> == 2) {

                var asd = "";

                $("#DepartureDate").caleran({
                    oninit: function (instance) {
                        var current = new Date('<%=depDateValue%>');
                        end = new Date();
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);

                        instance.globals.delayInputUpdate = true;
                        instance.$elem.val("<%=depDateValue%>");
                        caleranStart = instance;
                        if (startSelected == null)
                            startSelected = moment().add(days + 1, "days").startOf("day");
                        current = new Date('<%=arrDateValue%>');
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);
                        if (endSelected == null)
                            endSelected = moment().add(days + 1, "days").startOf("day");
                        instance.$elem.val(startSelected.format(caleranStart.config.format));
                    },
                    ondraw: ondrawEvent1,
                    onfirstselect: function (instance, start) {
                        startSelected = start;
                        instance.globals.startSelected = false;
                        updateInputs();
                        instance.hideDropdown();
                        caleranEnd.showDropdown();
                    },
                    minDate: moment().subtract(1, "days").startOf("day"),
                    maxDate: moment().add(365, "days").endOf("day"),
                    startDate: "<%=depDateValue%>",
                    endDate: "<%=arrDateValue%>",
                    showFooter: false
                });
                // end always selects end date.
                $("#ReturnDate").caleran({
                    oninit: function (instance) {
                        var current = new Date('<%=depDateValue%>');
                        end = new Date();
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);

                        instance.globals.delayInputUpdate = true;
                        instance.$elem.val("<%=arrDateValue%>");
                        caleranEnd = instance;
                        if (startSelected == null)
                            startSelected = moment().add(days + 1, "days").startOf("day");
                        current = new Date('<%=arrDateValue%>');
                        diff = new Date(current - end);
                        days = diff / 1000 / 60 / 60 / 24;
                        days = Math.round(days);
                        if (endSelected == null)
                            endSelected = moment().add(days + 1, "days").startOf("day");
                        instance.$elem.val(endSelected.format(caleranStart.config.format));
                    },
                    ondraw: ondrawEvent1,
                    onfirstselect: function (instance, start) {
                        endSelected = start;
                        instance.globals.startSelected = false;
                        updateInputs();
                        instance.hideDropdown();
                    },
                    minDate: moment().subtract(1, "days").startOf("day"),
                    maxDate: moment().add(365, "days").endOf("day"),
                    startDate: "<%=depDateValue%>",
                    endDate: "<%=arrDateValue%>",
                    showFooter: false
                });
            }
            else {
                $("#DepartureDate").caleran({
                    singleDate: true, autoCloseOnSelect: true,
                    minDate: moment().subtract(1, "days").startOf("day"),
                    maxDate: moment().add(365, "days").endOf("day"),
                    startDate: "<%=depDateValue%>",
                    showFooter: false
                });
            }
        });

    </script>
    <script type="text/javascript">
        adroll_adv_id = "XWYYUWV7ZBFPZK2GETPLYN";
        adroll_pix_id = "WHIZH3FTUNA3XO27DPVBE4";
        (function () {
            var oldonload = window.onload;
            window.onload = function () {
                __adroll_loaded = true;
                var scr = document.createElement("script");
                var host = (("https:" == document.location.protocol) ? "https://s.adroll.com" : "http://a.adroll.com");
                scr.setAttribute('async', 'true');
                scr.type = "text/javascript";
                scr.src = host + "/j/roundtrip.js";
                ((document.getElementsByTagName('head') || [null])[0] ||
                    document.getElementsByTagName('script')[0].parentNode).appendChild(scr);
                if (oldonload) { oldonload() }
            };
        }());
    </script>


    <script src="js/client.min.js"></script>
    <script type="text/javascript" src="js/jivo.js"></script>
    <script>

        var client = new ClientJS();
        var fingerprint = client.getFingerprint();
        console.log(fingerprint);
        var obj = {};
        obj.fingerprint = fingerprint;
        obj.track_id = '<%= track_id%>';

        $.ajax({
            type: "POST",
            url: "FlightResults.aspx/Update_Device_ID",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // alert(msg.d);
            }
        });
        console.log(fingerprint);
        //Notification Message jss

    </script>


    <script type="text/javascript">


        var counter1 = 1;


        $(document.body).on('scrollstop', onScroll); // for 

        $(window).on('scroll', onScroll);

        function onScroll() {

            if (pagecount1 >= counter1) {

                if (document.getElementById('isDone').value == "1") {

                    //if ($(window).scrollTop() == $(document).height() - $(window).height() ) {
                    if ($(window).scrollTop() + window.innerHeight >= document.body.scrollHeight) {

                        counter1++;
                        $('#loadGf').fadeIn('fast');

                        setTimeout(function () {
                            $('#loadGf').fadeOut('fast');
                            currentpage = counter1;
                            change();



                        }, 800);




                    }
                }
            }
            else {
                currentpage = 1;
                res1 = "";
            }

        }
    </script>





</body>
</html>
