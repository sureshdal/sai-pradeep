<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dynamicAirlines.aspx.cs" Inherits="dynamicAirlines" %>

<!DOCTYPE html>


<html>
<head>
    
    <%=strMetaTemp %>

    <%--<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE = edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   
    <title>TravelMerry - US </title> --%>
    <link rel="shortcut icon" href="Design/images/favicon.ico">

    <link rel="stylesheet" type="text/css" href="Design/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/tmStyle.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/icofont.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/rsw-style.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/pagination.css" />

    <script src="js/validate.js" type="text/javascript"></script>
    <!--[if lt IE 9]>         
    <script src = "https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src = "https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <noscript>
        <div class="noscript">
            <p style="margin-left: 10px">JavaScript is not enabled.</p>
        </div>
    </noscript>
   <script type="text/javascript" src="Design/js/jquery.min.js"></script>
    <script type="text/javascript" src="Design/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Design/css/checkboxstyle.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/airlinestyle.css" />
     <link rel="stylesheet" type="text/css" href="Design/css/BookingEngine-tm.css" />
    

    <link rel="stylesheet" type="text/css" href="Design/build/css/caleran.min.css" />



    
    <script type="text/javascript" src="Design/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        if (top.location != self.location)
            top.location = self.location;
        $(document).ready(function () {
           
          $(".masthead").css("background-image", "url(Design/images/airline-images/airline-default-img.jpg)");

        });
    </script>
    

    

    <script src="Design/js/jquery-ui.js" type="text/javascript"></script>
    <script src="Design/js/scratchpad-airlines.js" type="text/javascript"></script>
    <script type="text/javascript" src="Design/js/flightdeals-carousel.js"></script>


    <script type="text/javascript">
        function Validate() {
            var re = /^[A-Za-z ]+$/;
            var ree = /^(0|[0-9]\d*)$/;
            var reee = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var bookingidre = /^[a-zA-Z0-9_-]*$/;

            var txtEmailAddress = document.getElementById("txtEmailAddress");
            var flagCNEmail = ValidateEmail(txtEmailAddress); //required(cardEmail);
            if (flagCNEmail)
                flagCNEmail = reee.test(txtEmailAddress.value);
            if (document.getElementById("txtEmailAddress").value != "") {
                if (!flagCNEmail) {
                    $('#txtEmailAddress').css('border', '2px solid');
                    $('#txtEmailAddress').css('border-color', '#f49b9b');
                    $("#txtEmailAddresshint_hint").remove();
                    $("#txtEmailAddresshint").append('<span id="txtEmailAddresshint_hint" class="tool-info"><span class="tool-head-title">Email ID</span><span class="tool-body-text">Email ID is not Valid</span></span>');
                    $("#txtEmailAddresshint_hint").css("display", "block");
                }
                else {
                    $("#txtEmailAddresshint_hint").remove();
                    $('#txtEmailAddress').css('border', '');
                    $('#txtEmailAddress').css('border-color', '');
                }
            }
            else {
                $('#txtEmailAddress').css('border', '2px solid');
                $('#txtEmailAddress').css('border-color', '#f49b9b');
                $("#txtEmailAddresshint_hint").remove();
                $("#txtEmailAddresshint").append('<span id="txtEmailAddresshint_hint" class="tool-info"><span class="tool-head-title">Email ID</span><span class="tool-body-text">Please enter Email ID</span></span>');
                $("#txtEmailAddresshint_hint").css("display", "block");
            }
            if (flagCNEmail ) {
                return true;
            }
            else {
                return false;
            }
        }

        </script>

    <%--searchengg--%>

     <script type="text/javascript">
        var searchesmade = [];
        function btn_Submit() {
            var SearchType = $('li[role=presentation]');
            for (var i = 0; i < SearchType.length; i++) {
                if (SearchType[i].className == "active") {
                    SearchType = SearchType[i].innerText.replace(/\n/ig, '');
                }
            }
            var dept = "";
            var dest = "";
            var deptDate = "";
            var arrDate = "";
            var Travellers = "";

            var MultiCitydept = [];
            var MultiCitydest = [];
            var MultiCitydeptDate = [];
            SearchType = SearchType.toUpperCase().trim();
            var FlagsearchType = validateIndexInputsLength(SearchType.length);
            if (SearchType.toUpperCase() == "ROUND TRIP") {
                dept = document.forms["Form1"]["Departure"];
                dest = document.forms["Form1"]["Destination"];
                deptDate = document.forms["Form1"]["DepartureDate"];
                arrDate = document.forms["Form1"]["ReturnDate"];
                Travellers = document.forms["Form1"]["Travellers"];
            }
            else if (SearchType.toUpperCase() == "ONE WAY") {
                dept = document.forms["Form1"]["Departure"];
                dest = document.forms["Form1"]["Destination"];
                deptDate = document.forms["Form1"]["DepartureDate"];
                arrDate = document.forms["Form1"]["ReturnDate"];
                Travellers = document.forms["Form1"]["Travellers"];
            }
            else {
                var multicities = $("#MultiCiti > div").length;
                var multicitiesExtra = $("#MultuCityItemExtra > div").length;
                multicities--;
                for (var i = 1; i < multicities; i++) {
                    MultiCitydept.push(document.forms["Form1"]["MultiCityDeparture" + i]);
                    MultiCitydest.push(document.forms["Form1"]["MultiCityDestination" + i]);
                    MultiCitydeptDate.push(document.forms["Form1"]["MultiCityDepartureDate" + i]);
                    //fisrt time to get the travellers
                    if (i == 1) {
                        Travellers = document.forms["Form1"]["MultiCityTravellers1"];
                    }
                }
                for (var i = 3; i <= multicitiesExtra + 2; i++) {
                    MultiCitydept.push(document.forms["Form1"]["MultiCityDeparture" + i]);
                    MultiCitydest.push(document.forms["Form1"]["MultiCityDestination" + i]);
                    MultiCitydeptDate.push(document.forms["Form1"]["MultiCityDepartureDate" + i]);
                }

            }

            if (SearchType.toUpperCase() != "MULTI CITIES") {
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

                if (SearchType.toUpperCase() != "ONE WAY") {
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
                //Adultss Childss Infantss Classs
                var trav = top.GlobPaxCount.split(',');

                $("#Adultss").val(parseInt(trav[0].substring(0, 1)));
                $("#Childss").val(parseInt(trav[1].substring(1, 2)));
                $("#Infantss").val(parseInt(trav[2].substring(1, 2)));
                $("#CabinClass").val(trav[3].split(' ')[1]);
                if (!flagDept || !flagDeptLen || !flagDest || !flagDestLen || !flagDeptDate || !flagArrDate || !flagTraveller) {
                    flag = false;
                }
                else {
                    $("#txtFlyFromFO_hidden").val(dept.value.split('(')[1].split(')')[0]);
                    $("#txtFlyToFO_hidden").val(dest.value.split('(')[1].split(')')[0]);
                    if (SearchType.toUpperCase() != "ONE WAY")
                        $("#Triptype").val("Roundtrip");
                    else
                        $("#Triptype").val("Oneway");
                    var c = $('#directFlights').is(":checked");
                    if ($('#directFlights').is(":checked")) {
                        $("#chkDirectFlightsFO").val("NonStop");
                    }
                    if ($("#Flexibility").val() == 0) {
                        $("#Flexible").val("0");
                    }
                    else {
                        $("#Flexible").val($("#Flexibility").val());
                    }
                    var departure = "";

                    if (document.getElementById("txtFlyFromFO_hidden").value != "" && document.getElementById("txtFlyToFO_hidden").value != "") {

                        var jtype = "2";
                        if (SearchType.toUpperCase() == "ONE WAY") {
                            jtype = "1";
                            $("#ReturnDate").val(document.getElementById("DepartureDate").value);
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
                    }
                    flag = true;
                }
            }
            else if (SearchType.toUpperCase() == "MULTI CITIES") {
                var flag = true;

                var flagDept = true;
                var flagDeptLen = true;
                var flagDest = true;
                var flagDestLen = true;

                var flagDeptDate = true;
                var flagArrDate = true;
                var flagTraveller = true;
                var SearchError = "";
                var multicities = $("#MultuCityItemExtra > div").length;
                for (var i = 0; i < MultiCitydept.length; i++) {
                    flagDept = validateIndexInputsLength(MultiCitydept[i]);
                    if (!flagDept) {
                        $("#multifrom" + parseInt(parseInt(i) + 1)).append("<span class='form_hint'>Please Enter The Departure Airport</span>");
                        $(".form_hint").css("display", "block");
                        flag = false;
                    }
                    flagDeptLen = PaymentLinkstringlength(MultiCitydept[i], 3, 100);
                    if (!flagDeptLen && flagDept) {
                        $("#multifrom" + parseInt(parseInt(i) + 1)).append("<span class='form_hint'>Please Enter The Departure Airport</span>");
                        $(".form_hint").css("display", "block");
                        flag = false;
                    }
                    if (flag) {
                        $(".form_hint").remove();
                    }
                }
                for (var i = 0; i < MultiCitydest.length; i++) {
                    flagDest = validateIndexInputsLength(MultiCitydest[i]);
                    if (!flagDest) {
                        $("#multito" + parseInt(parseInt(i) + 1)).append("<span class='form_hint'>Please Enter The Departure Airport</span>");
                        $(".form_hint").css("display", "block");
                        flag = false;
                    }
                    flagDestLen = PaymentLinkstringlength(MultiCitydest[i], 3, 100);
                    if (!flagDestLen && flagDest) {
                        $("#multito" + parseInt(parseInt(i) + 1)).append("<span class='form_hint'>Please Enter The Departure Airport</span>");
                        $(".form_hint").css("display", "block");
                        flag = false;
                    }
                    if (flag) {
                        $(".form_hint").remove();
                    }
                }

                for (var i = 0; i < MultiCitydeptDate.length; i++) {
                    flagDeptDate = validateIndexInputsLength(MultiCitydeptDate[i]);
                    if (!flagDeptDate) {
                        $("#multidepDate" + parseInt(parseInt(i) + 1)).append("<span class='form_hint'>Please Enter The Departure Airport</span>");
                        $(".form_hint").css("display", "block");
                        flag = false;
                    }
                }
                flagTraveller = validateIndexInputsLength(Travellers);
                if (!flagTraveller) {
                    $("#multitraveler1").append("<span class='form_hint'>Please Enter The Departure Airport</span>");
                    $(".form_hint").css("display", "block");
                    flag = false;
                }
                //Adultss Childss Infantss Classs
                $("#Adultss").val(parseInt(travelerBXAD1.substring(0, 1)));
                $("#Childss").val(parseInt(travelerBXCH1.substring(2, 3)));
                $("#Infantss").val(parseInt(travelerBXIN1.substring(2, 3)));
                if (!flagDept || !flagDeptLen || !flagDest || !flagDestLen || !flagDeptDate || !flagArrDate || !flagTraveller) {
                    flag = false;
                }
                else {
                }
            }
            return /*false*/flag;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var de = $("#Departure").val();
            de = document.getElementById("Departure").value
            document.getElementById("txtFlyFromFO_hidden").value = de

            $.ajax({
                url: "/js/Airports.txt",
                type: "get",
                async: false,
                success: function (data) {
                    top.Globair = JSON.parse(data);
                },
                error: function () {

                }
            });



            $("#Departure").val('<%=city%>'.split('^')[0]);
            $("#txtFlyFromFO_hidden").val('<%=city%>'.split('^')[1]);

            $("#traveler").click(function (event) {
                event.stopImmediatePropagation();
            });
            var travelerBXAD = "1 Adults";
            var travelerBXCH = "";
            var travelerBXIN = "";
            var class1 = ", Economy Class";
            var classs = "Economy";
            $('#traveller-bx').val(travelerBXAD + travelerBXCH + travelerBXIN + class1);

            var class11 = ", Economy Class";
            var classs1 = "Economy";
            var travelerBXAD1 = "1 Adults";
            var travelerBXCH1 = ", 0 Childs";
            var travelerBXIN1 = ", 0 Infants";

            function scrollToTop() {
                verticalOffset = typeof (verticalOffset) != 'undefined' ? verticalOffset : 0;
                element = $('body');
                offset = element.offset();
                offsetTop = offset.top;
                $('html, body').animate({ scrollTop: offsetTop }, 500, 'linear');
            }
            $("#DepartureDate").caleran({
                oninit: function (instance) {
                    instance.globals.delayInputUpdate = true;
                    instance.$elem.val("");
                    caleranStart = instance;
                    if (startSelected == null)
                        startSelected = moment().add(5, "days").startOf("day");
                    if (endSelected == null)
                        endSelected = moment().add(10, "days").startOf("day");
                    instance.$elem.val(startSelected.format(caleranStart.config.format));
                },
                ondraw: ondrawEvent,
                onfirstselect: function (instance, start) {
                    startSelected = start;
                    endSelected = start;
                    instance.globals.startSelected = false;
                    updateInputs();
                    instance.hideDropdown();
                    caleranEnd.showDropdown();
                    $("#deptDateform_hint").remove();
                },
                minDate: moment().subtract(1, "days").startOf("day"),
                maxDate: moment().add(365, "days").endOf("day"),
                showFooter: false
            });
            // end always selects end date.
            $("#ReturnDate").caleran({
                oninit: function (instance) {
                    instance.globals.delayInputUpdate = true;
                    instance.$elem.val("");
                    caleranEnd = instance;
                    if (startSelected == null)
                        startSelected = moment().add(5, "days").startOf("day");
                    if (endSelected == null)
                        endSelected = moment().add(10, "days").startOf("day");
                    instance.$elem.val(endSelected.format(caleranEnd.config.format));
                },
                ondraw: ondrawEvent,
                onfirstselect: function (instance, start) {
                    endSelected = start;
                    instance.globals.startSelected = false;
                    updateInputs();
                    instance.hideDropdown();
                    $("#arrDateform_hint").remove();
                },
                minDate: moment().subtract(1, "days").startOf("day"),
                maxDate: moment().add(365, "days").endOf("day"),
                showFooter: false
            });
            //One way date js
            $("#caleran-Oneway").caleran({
                singleDate: true, autoCloseOnSelect: true,
                minDate: moment().subtract(1, "days").startOf("day"),
                maxDate: moment().add(365, "days").endOf("day"),

                onfirstselect: function (instance, start) {
                },
                showFooter: false
            });

            $.ajax({
                type: "POST",
                //url: "https://www.travelmerry.com/dynamicAirlines.aspx/GetScratch",
                url: "http://localhost:50244/dynamicAirlines.aspx/GetScratch",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != '') {
                        searchesmade = JSON.parse(msg.d).Airlines;
                        top.jsonScratchpad = searchesmade;
                        if (searchesmade != null) {
                            if (searchesmade.length > 0) {
                                imagename = searchesmade[0].Dest;
                                var imageurl = 'http://localhost:58281/images/back/'+imagename+'.jpg';
                                var image = 'url(images/back/'+imagename+'.jpg)';
                                isUrlExists(imageurl, function (status) {
                                    if (status === 200) {
                                        $(".masthead").css("background-image", image);
                                    }
                                    else if (status === 404) {
                                        $(".masthead").css("background-image", "url(Design/images/backgroung-img.jpg)");
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
                                }
                            }
                            else {
                                $(".masthead").css("background-image", "url(Design/images/airline-images/airline-default-img.jpg)");
                            }
                        }
                        else {
                            $(".masthead").css("background-image", "url(Design/images/airline-images/airline-default-img.jpg)");
                        }
                       //top_airlines(searchesmade);
                    }
                    else {
                        display1('');
                        top.jsonScratchpad = "";
                    }
                    if (msg.d != '') {
                        searchesmade1 = JSON.parse(msg.d).Deals;
                        top.jsonScratchpadDeals = searchesmade1;
                        //displayDeals(searchesmade1); display_popularRoutes
                        display_popularRoutes(searchesmade1);
                        top_airlines(searchesmade);
                    }
                    else {
                        display_popularRoutes('');
                        top.jsonScratchpadDeals = "";
                    }
                },
                error: function (msg) {
                    $("#popular_routes").css("display", "none");
                }
            });


            carosel('.flightDealSlider', '.flightDealSlider-inner')
            //}, 0);

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
                                showFooter: false
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
                            showFooter: false
                        });
                    }
                }
            }
        });
    </script>



    <script type="text/javascript">

        function AlphaFilters(val) {


            document.getElementById('txtSearChKeyId').value = "";
            var alph = "A-B-C-D-E-F-G-H-I-J-K-L-M-N-O-P-Q-R-S-T-U-V-W-X-Y-Z";
            var rplcStr = alph.replace(val, "");
            var rplcStr1 = rplcStr.replace("--", "-");
            var alphArr = rplcStr1.split('-');

            var ss = "#" + val;
            var strVal = $("#txtHiddenDivIds").val();
            var resArr = new Array();
            resArr = strVal.split("~");
            if (val == "All") {
                for (i = 0; i < resArr.length - 1; i++) {
                    document.getElementById(resArr[i]).style.display = "";
                }
                //$( ss ).addClass( "active" );
                $(ss).removeClass('nactive').addClass('active');
                for (a = 0; a < alphArr.length; a++) {
                    if (alphArr[a] != "") {
                        var nn = "#" + alphArr[a];
                        //$( nn ).addClass( "nactive" );
                        if ($(nn).hasClass('active')) {
                            $(nn).removeClass('active').addClass('nactive');
                        }

                    }
                }
            }
            else {
                var MatchValide = "false";
                for (j = 0; j < resArr.length - 1; j++) {
                    var txt = resArr[j].toUpperCase();
                    var n = txt.startsWith(val);


                    if (n) {
                        document.getElementById(resArr[j]).style.display = "";
                        //$(ss).css('background-color', 'red');
                        //$( ss ).addClass( "active" ); 
                        $(ss).removeClass('nactive').addClass('active');
                        MatchValide = "true";
                    }
                    else {
                        document.getElementById(resArr[j]).style.display = "none";
                    }
                }
                if (MatchValide == "false") {
                    if ($(ss).hasClass('nactive')) {
                        $(ss).removeClass('nactive').addClass('active');
                    }
                }

                for (a = 0; a < alphArr.length; a++) {
                    if (alphArr[a] != "") {
                        var nn = "#" + alphArr[a];
                        //$( nn ).addClass( "nactive" );
                        if ($(nn).hasClass('active')) {
                            $(nn).removeClass('active').addClass('nactive');
                        }
                    }
                }
                if ($("#All").hasClass('active')) {
                    $("#All").removeClass('active').addClass('nactive');
                }
            }
        }



    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("input").bind("keyup", function (e) {
                var strVal = $("#txtHiddenDivIds").val();
                var resArr = new Array();
                resArr = strVal.split("~");
                //alert(resArr.length);     
                var textvalue = document.getElementById('txtSearChKeyId').value.toString().replace(" ", "_");
                /*************************/
                var alph = "All-A-B-C-D-E-F-G-H-I-J-K-L-M-N-O-P-Q-R-S-T-U-V-W-X-Y-Z";
                var alphArr = alph.split('-');
                var SorceStrChar = textvalue.charAt(0);
                for (a = 0; a < alphArr.length; a++) {
                    if (SorceStrChar.toUpperCase() == "") {
                        if (alphArr[a] == "All") {
                            var nn = "#" + alphArr[a];
                            if ($(nn).hasClass('active')) {
                                //$(nn).removeClass('active').addClass('nactive');
                            }
                            else {
                                $(nn).removeClass('nactive').addClass('active');
                            }

                        }
                        else {
                            var nn = "#" + alphArr[a];
                            if ($(nn).hasClass('active')) {
                                $(nn).removeClass('active').addClass('nactive');
                            }
                            else {
                                //$(nn).removeClass('nactive').addClass('active');
                            }

                        }

                    }
                    else {
                        if (alphArr[a] == SorceStrChar.toUpperCase()) {
                            var nn = "#" + alphArr[a];
                            //$( nn ).addClass( "nactive" );
                            if ($(nn).hasClass('active')) {
                                //$(nn).removeClass('active').addClass('nactive');
                            }
                            else {
                                $(nn).removeClass('nactive').addClass('active');
                            }
                        }
                        else {
                            var nn = "#" + alphArr[a];
                            if ($(nn).hasClass('active')) {
                                $(nn).removeClass('active').addClass('nactive');
                            }
                            //                      else
                            //                      {
                            //                        $(nn).removeClass('nactive').addClass('active');
                            //                      } 

                        }
                    }
                }
                /**********************/

                if (textvalue != '') {
                    for (i = 0; i < resArr.length - 1; i++) {
                        document.getElementById(resArr[i]).style.display = "none";
                    }

                    for (i = 0; i < resArr.length - 1; i++) {
                        var txt = resArr[i].toUpperCase();
                        var n = txt.startsWith(textvalue.toUpperCase());

                        if (n) {
                            document.getElementById(resArr[i]).style.display = "";
                        }
                        else {
                            document.getElementById(resArr[i]).style.display = "none";
                        }

                    }
                }
                else {
                    for (i = 0; i < resArr.length - 1; i++) {
                        document.getElementById(resArr[i]).style.display = "";
                    }
                }

                //alert(str);               
                //alert(value);
            });

            //  $("input").keydown(function(){
            //    $("input").css("background-color","yellow");
            //    alert($("#txtSearChKeyId").val());
            //  });
            //  $("input").keyup(function(){
            //    $("input").css("background-color","pink");
            //  });
        });

    </script>

    <!--[if !IE]><!-->
    <style>
        /* 
	Max width before this PARTICULAR table gets nasty
	This query will take effect for any screen smaller than 760px
	and also iPads specifically.
	*/
        @media only screen and (max-width: 760px), (min-device-width: 768px) and (max-device-width: 1024px) {

            /* Force table to not be like tables anymore */
            table, thead, tbody, th, td, tr {
                display: block;
            }

                /* Hide table headers (but not display: none;, for accessibility) */
                thead tr {
                    position: absolute;
                    top: -9999px;
                    left: -9999px;
                }

            tr {
                border: 1px solid #ccc;
            }

            td {
                /* Behave  like a "row" */
                border: none;
                border-bottom: 1px solid #eee;
                position: relative;
                padding-left: 50%;
            }

                td:before {
                    /* Now like a table header */
                    position: absolute;
                    /* Top/left values mimic padding */
                    top: 6px;
                    left: 6px;
                    width: 45%;
                    padding-right: 10px;
                    white-space: nowrap;
                }

                /*
		Label the data
		*/
                td:nth-of-type(1):before {
                    content: "Airline Logo";
                }

                td:nth-of-type(2):before {
                    content: "Route";
                }

                td:nth-of-type(3):before {
                    content: "Hand Baggage";
                }

                td:nth-of-type(4):before {
                    content: "1st Baggage";
                }

                td:nth-of-type(5):before {
                    content: "2nd baggage";
                }

                td:nth-of-type(6):before {
                    content: "Additional Baggage";
                }

                td:nth-of-type(7):before {
                    content: "Overweight Bags";
                }

                td:nth-of-type(8):before {
                    content: "Oversized Bags";
                }
            /*td:nth-of-type(9):before { content: ""; }
		td:nth-of-type(10):before { content: ""; }*/
        }
    </style>
    <!--<![endif]-->
     <%--<script src="js/google-analytics.js"></script>--%>
    <script src="js/travelmerry_hotjar.js" type="text/javascript"></script>
    
  <%--  <script>
    function imgError(me) {
    // place here the alternative image
    var AlterNativeImg = "Design/images/airline-images/TK.jpeg";

    // to avoid the case that even the alternative fails        
    if (AlterNativeImg != me.src)
        me.src = AlterNativeImg;
     }
    </script>--%>
</head>

<body>
    <!--#include file="Header.aspx" -->


<section  id="masthead"  class="land-bookingForm " style="background-image:url(Design/images/airline-images/<%=airline_headerImage%>.jpg); " >

    <div class="overlay"></div>
	<div class="container">

       <%-- <asp:ContentPlaceHolder ID="Xmassearch" runat="server">     
            </asp:ContentPlaceHolder>--%>
    	<div class="row">
            <div class="col-md-12 text-center">
        	<div class="land-wrap">
            	<div class="land-bk-air-Title"><%--<h1>Book <%=Aircode %> At Lowest Price</h1>--%>
                    <%=AIrline_header_offer %>

            	</div>

                <div id="airlineimage"></div>
     <div class="land-bk-air-logo"><img src="Design/images/airline-logo-lg/<%=airline_headerImage %>.jpg"alt="<%=Aircode %>"   /></div>

            </div>
        </div>


        	<div class="col-md-12">

                 <div class="searchEngine-wrap" id="SearchEng">
                        <form method="post" id="Form1" novalidate runat="server" action="https://www.travelmerry.com/FlightRequest.aspx">
                            <input type="hidden" name="CabinClass" id="CabinClass" value="" />
                            <input type="hidden" name="Adultss" id="Adultss" value="" />
                            <input type="hidden" name="Childss" id="Childss" value="" />
                            <input type="hidden" name="Infantss" id="Infantss" value="" />
                            <input id='dept' name='dept' type='hidden'>
                            <input id='dest' name='dest' type='hidden'>
                            <input id='Triptype' name='Triptype' type='hidden'>
                            <input id='chkDirectFlightsFO' name='chkDirectFlightsFO' type='hidden'>
                            <input id='Flexible' name='Flexible' type='hidden'>
                            <input id='txtFlyFromFO_hidden' name='txtFlyFromFO_hidden' type='hidden'>
                            <input id='txtFlyToFO_hidden' name='txtFlyToFO_hidden' type='hidden'>
                            <div id="one-round-trip" class="one-round-trip">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation" class="active">
                                        <a aria-controls="RoundTrip" role="tab" data-toggle="tab">
                                            <span class="sprte icontrips iconRoundTrip"></span>Round Trip
                                        </a>
                                    </li>
                                    <li role="presentation">
                                        <a aria-controls="One way" role="tab" data-toggle="tab">
                                            <span class="sprte icontrips icnOneway"></span>One Way
                                        </a>
                                    </li>
                                   
                                </ul>
                            </div>
                            <div class="tab-content">
                                <div id="RoundTrip" role="tabpanel" class="tab-pane active">
                                    <div class="from" id="from">
                                        <input type="text" name="Departure" class="dpIcon" required id="Departure" onfocus="this.select();" autocomplete='off' placeholder="Enter city or airport">
                                        <div class="switch-places" id="switchplaces"></div>
                                    </div>

                                    <div class="to" id="to">
                                        <input type="text" name="Destination" class="ArvIcon" required id="Destination" onfocus="this.select();" autocomplete='off' placeholder="Enter city or airport">
                                    </div>

                                    <div class="deprtDate" id="depDate">
                                        <input type="text" name="DepartureDate" class="dpDateIcon" readonly required id="DepartureDate" placeholder="Departure">
                                    </div>
                                    <div class="arrivDate" id="retDate">
                                        <input type="text" name="ReturnDate" class="ArvDateIcon" readonly required id="ReturnDate" placeholder="Return">
                                    </div>

                                    <div class="travellers" id="traveler">
                                        <input type="text" name="Travellers" id="traveller-bx" class="travellerIcon" readonly required placeholder="1 Person, Economy">
                                        <div class="options-popup traveler-pop" id="travelerpop">
                                            <div class="options-popup-header">
                                                <span>Travellers</span>
                                                <span class="options-popup-close pull-right" id="optionspopupclose"><i class="fa fa-times"></i></span>
                                            </div>
                                            <div class="passengers">
                                                <div class="row">
                                                    <div class="buttons">
                                                        <div id='minAdult' class="minus" onclick="ChildDecrease(this)" tabindex="-1"></div>
                                                        <div id='countAdult' class="count" title="Adults">1</div>
                                                        <div id='plAdult' class="plus" onclick="ChildIncrease(this)" tabindex="-1"></div>

                                                    </div>
                                                    <div class="description">
                                                        <div class="name">Adults</div>
                                                        <div class="age">(11+ Years)</div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="buttons">
                                                        <div id='minChild' class="minus" onclick="ChildDecrease(this)" tabindex="-1"></div>
                                                        <div id='countChild' class="count">0</div>
                                                        <div id='plChild' class="plus" onclick="ChildIncrease(this)" tabindex="-1"></div>
                                                    </div>

                                                    <div class="description">
                                                        <div class="name">Children</div>
                                                        <div class="age">(2-11 Years)</div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="buttons">
                                                        <div id='minInfant' class="minus" tabindex="-1" onclick="ChildDecrease(this)" data-metric="options infants-minus"></div>
                                                        <div id='countInfant' class="count">0</div>
                                                        <div id='plInfant' class="plus" tabindex="-1" onclick="ChildIncrease(this)" data-metric="options infants-plus"></div>
                                                    </div>

                                                    <div class="description">
                                                        <div class="name">Infant in lap</div>
                                                        <div class="age">(0-2 Years)</div>
                                                    </div>
                                                </div>
                                                <div class="row" id="error" style="display: none">
                                                </div>
                                            </div>

                                            <div class="trip-class">
                                                <span>Flight Class</span>
                                                <div class="radio" tabindex="-1" data-metric="options business-type">
                                                    <input checked="checked" name="ClassType" onchange="class1change(this)" id="Economy" class="radio-custom" onclick="" style="margin: 0px" type="radio">
                                                    <label class="radio-custom-label" for="Economy">Economy</label>
                                                </div>
                                                <div class="radio" tabindex="-1" data-metric="options business-type">
                                                    <input class="radio-custom" id="Business" onchange="class1change(this)" name="ClassType" style="margin: 0px" type="radio">
                                                    <label class="radio-custom-label" for="Business">Business</label>
                                                </div>
                                                <div class="radio" tabindex="-1" data-metric="options business-type">
                                                    <input class="radio-custom" id="First" onchange="class1change(this)" name="ClassType" style="margin: 0px" type="radio">
                                                    <label class="radio-custom-label" for="First">First</label>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 text-center">
                                                    <button type="button" class="btn doneBtn options-popup-close mb-10" id="optionspopupclose1">Done</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="SearchBtn-wrap">
                                    <span>
                                        <asp:Button runat="server" ID="BtnSearch" CssClass="btn SearchFlightBtn" OnClick="Button1_Click" OnClientClick="return btn_Submit();" Text="Search Flights" /></span>
                                </div>

                                    <div class="advoption">
                                        <div class="left" id="toggle1">
                                            <a href="" class="">
                                                <i class="fa fa-plus-square"></i>Show more options
                                            </a>
                                        </div>
                                        <div class="right">
                                            <input id="directFlights" class="checkbox-custom" name="directFlights" type="checkbox">
                                            <label for="directFlights" class="checkbox-custom-label">Direct flights only</label>
                                        </div>
                                        <div id="AdvancedOptions" class="AdvancedOptions" style="display: none;">
                                            <div class="airlines">
                                                <label>Airlines:</label>
                                                <div class="selectbx">
                                                    <select class="" name="Airlines" id="Airlines" data-msg-required="Please Select the Airline" data-rule-required="true">
                                                        <option value="ALL">Any Airline</option>
                                                        <option value="I8">Aboriginal Air</option>
                                                        <option value="9B">AccesRail</option>
                                                        <option value="ZY">Ada Air</option>
                                                        <option value="JP">Adria Airways</option>
                                                        <option value="DF">Aebal</option>
                                                        <option value="A3">Aegean Air</option>
                                                        <option value="RE">Aer Arann</option>
                                                        <option value="EI">Aer Lingus</option>
                                                        <option value="EE">Aero Airlines</option>
                                                        <option value="E4">Aero Asia</option>
                                                        <option value="EM">Aero Benin</option>
                                                        <option value="JR">Aero California</option>
                                                        <option value="QA">Aero Caribe</option>
                                                        <option value="P4">Aero Lineas Sosa</option>
                                                        <option value="M0">Aero Mongolia</option>
                                                        <option value="DW">Aero-Charter</option>
                                                        <option value="AJ">Aerocontractors</option>
                                                        <option value="SU">Aeroflot</option>
                                                        <option value="2K">Aerogal Aerolina</option>
                                                        <option value="KG">Aerogaviota</option>
                                                        <option value="AR">Aerolineas Argen</option>
                                                        <option value="VW">Aeromar</option>
                                                        <option value="BQ">Aeromar Air</option>
                                                        <option value="AM">Aeromexico</option>
                                                        <option value="5D">Aeromexico</option>
                                                        <option value="OT">Aeropelican</option>
                                                        <option value="VH">Aeropostal</option>
                                                        <option value="P5">AeroRepublica</option>
                                                        <option value="5L">Aerosur</option>
                                                        <option value="VV">Aerosvit</option>
                                                        <option value="6I">AeroSyncro</option>
                                                        <option value="XU">African Express</option>
                                                        <option value="ML">African Transprt</option>
                                                        <option value="8U">Afriqiyah</option>
                                                        <option value="X5">Afrique Airlines</option>
                                                        <option value="ZI">Aigle Azur</option>
                                                        <option value="AH">Air Algerie</option>
                                                        <option value="A6">Air Alps</option>
                                                        <option value="2Y">Air Andaman</option>
                                                        <option value="G9">Air Arabia</option>
                                                        <option value="KC">Air Astana</option>
                                                        <option value="UU">Air Austral</option>
                                                        <option value="W9">Air Bagan</option>
                                                        <option value="BT">Air Baltic</option>
                                                        <option value="AB">Air Berlin</option>
                                                        <option value="YL">Air Bissau</option>
                                                        <option value="BP">Air Botswana</option>
                                                        <option value="2J">Air Burkina</option>
                                                        <option value="TY">Air Caledonie</option>
                                                        <option value="SB">Air Calin</option>
                                                        <option value="AC">Air Canada</option>
                                                        <option value="QK">Air Canada Jazz</option>
                                                        <option value="TX">Air Caraibes</option>
                                                        <option value="NV">Air Central</option>
                                                        <option value="CV">Air Chatham</option>
                                                        <option value="CA">Air China</option>
                                                        <option value="4F">Air City</option>
                                                        <option value="A7">Air Comet</option>
                                                        <option value="DV">Air Company SCAT</option>
                                                        <option value="QC">Air Corridor</option>
                                                        <option value="YN">Air Creebec</option>
                                                        <option value="DN">Air Deccan</option>
                                                        <option value="EN">Air Dolomiti</option>
                                                        <option value="UX">Air Europa</option>
                                                        <option value="PE">Air Europe</option>
                                                        <option value="PC">Air Fiji</option>
                                                        <option value="OF">Air Finland</option>
                                                        <option value="AF">Air France</option>
                                                        <option value="GL">Air Greenland</option>
                                                        <option value="LQ">Air Guinea</option>
                                                        <option value="3S">Air Guyane</option>
                                                        <option value="NY">Air Iceland</option>
                                                        <option value="AI">Air India</option>
                                                        <option value="IX">Air India Exp</option>
                                                        <option value="3H">Air Inuit</option>
                                                        <option value="I9">Air Italy</option>
                                                        <option value="VU">Air Ivoire</option>
                                                        <option value="JM">Air Jamaica</option>
                                                        <option value="NQ">Air Japan</option>
                                                        <option value="JS">Air Koryo</option>
                                                        <option value="L4">Air Liaison</option>
                                                        <option value="DR">Air Link</option>
                                                        <option value="NX">Air Macau</option>
                                                        <option value="MD">Air Madagascar</option>
                                                        <option value="QM">Air Malawi</option>
                                                        <option value="KM">Air Malta</option>
                                                        <option value="6T">Air Mandalay</option>
                                                        <option value="MK">Air Mauritius</option>
                                                        <option value="ZV">Air Midwest</option>
                                                        <option value="9U">Air Moldova</option>
                                                        <option value="SW">Air Namibia</option>
                                                        <option value="XN">Air Nepal</option>
                                                        <option value="NZ">Air New Zealand</option>
                                                        <option value="7A">Air Next</option>
                                                        <option value="EH">Air Nippon Netwk</option>
                                                        <option value="PX">Air Niugini</option>
                                                        <option value="4N">Air North</option>
                                                        <option value="TL">Air North Region</option>
                                                        <option value="YW">Air Nostrum</option>
                                                        <option value="AP">Air One</option>
                                                        <option value="FJ">Air Pacific</option>
                                                        <option value="TP">Air Portugal</option>
                                                        <option value="GZ">Air Rarotonga</option>
                                                        <option value="PJ">Air Saint-Pierre</option>
                                                        <option value="V7">Air Senegal</option>
                                                        <option value="X7">Air Service</option>
                                                        <option value="HM">Air Seychelles</option>
                                                        <option value="4D">Air Sinai</option>
                                                        <option value="GM">Air Slovakia</option>
                                                        <option value="SZ">Air Southwest</option>
                                                        <option value="ZP">Air St Thomas</option>
                                                        <option value="YI">Air Sunshine</option>
                                                        <option value="VT">Air Tahiti</option>
                                                        <option value="TN">Air Tahiti Nui</option>
                                                        <option value="TC">Air Tanzania</option>
                                                        <option value="8T">Air Tindi</option>
                                                        <option value="YT">Air Togo</option>
                                                        <option value="TS">Air Transat</option>
                                                        <option value="JY">Air Turks Caicos</option>
                                                        <option value="U7">Air Uganda</option>
                                                        <option value="DO">Air Vallee</option>
                                                        <option value="NF">Air Vanuatu</option>
                                                        <option value="ZW">Air Wisconsin</option>
                                                        <option value="UM">Air Zimbabwe</option>
                                                        <option value="AK">AirAsia</option>
                                                        <option value="D7">AirAsia X</option>
                                                        <option value="BM">Airbee</option>
                                                        <option value="YQ">Aircompany Polet</option>
                                                        <option value="4C">Aires</option>
                                                        <option value="P2">AirKenya Express</option>
                                                        <option value="A5">Airlinair</option>
                                                        <option value="FL">Airtran Air</option>
                                                        <option value="6L">Aklak Air</option>
                                                        <option value="AS">Alaska Airlines</option>
                                                        <option value="KO">Alaska Central</option>
                                                        <option value="J5">Alaska Seaplane</option>
                                                        <option value="LV">Albanian Air</option>
                                                        <option value="ZR">Alexandria Air</option>
                                                        <option value="D4">Alidaunia</option>
                                                        <option value="AZ">Alitalia</option>
                                                        <option value="XM">Alitalia Express</option>
                                                        <option value="YY">All Airlines</option>
                                                        <option value="NH">All Nippon</option>
                                                        <option value="G4">Allegiant Air</option>
                                                        <option value="CD">Alliance Air</option>
                                                        <option value="QQ">Alliance Airline</option>
                                                        <option value="3A">Alliance Chicago</option>
                                                        <option value="YJ">AMC Airlines</option>
                                                        <option value="AA">American</option>
                                                        <option value="AX">American Connexn</option>
                                                        <option value="MQ">American Eagle</option>
                                                        <option value="2V">Amtrak</option>
                                                        <option value="OY">Andes Lineas</option>
                                                        <option value="G6">Angkor Airways</option>
                                                        <option value="O4">Antrak Air</option>
                                                        <option value="7P">APA Intl Air</option>
                                                        <option value="5F">Arctic Circle</option>
                                                        <option value="FG">Ariana Afghan</option>
                                                        <option value="W3">Arik Air</option>
                                                        <option value="5N">Arkhangelsk Airl</option>
                                                        <option value="IZ">Arkia Israeli</option>
                                                        <option value="U8">Armavia Airline</option>
                                                        <option value="MV">Armenian Intl</option>
                                                        <option value="7S">Artic</option>
                                                        <option value="R7">Aserca Air</option>
                                                        <option value="6K">Asian Spirit</option>
                                                        <option value="OZ">Asiana Airlines</option>
                                                        <option value="ZA">Astair</option>
                                                        <option value="5W">Astraeus</option>
                                                        <option value="RC">Atlantic Faroe</option>
                                                        <option value="EV">Atlantic SE</option>
                                                        <option value="TD">Atlantis Europea</option>
                                                        <option value="8A">Atlas Blue</option>
                                                        <option value="KK">Atlas Jet Intl</option>
                                                        <option value="IP">Atyrau Airways</option>
                                                        <option value="IQ">Augsburg Airways</option>
                                                        <option value="GR">Aurigny Air Svc</option>
                                                        <option value="AU">Austral Lineas</option>
                                                        <option value="OS">Austrian Airline</option>
                                                        <option value="AV">Avianca</option>
                                                        <option value="WR">Aviaprad</option>
                                                        <option value="GU">Aviateca</option>
                                                        <option value="U3">Avies LTD</option>
                                                        <option value="M4">Aviompex</option>
                                                        <option value="9V">Avior Airlines</option>
                                                        <option value="2Q">Avitrans Nordi</option>
                                                        <option value="J2">Azerbaijan Airl</option>
                                                        <option value="JA">B and H Airlines</option>
                                                        <option value="CJ">BA City Flyer</option>
                                                        <option value="UP">Bahamasair</option>
                                                        <option value="2B">Bahrain Air</option>
                                                        <option value="PG">Bangkok Airways</option>
                                                        <option value="V9">Bashkir Airlines</option>
                                                        <option value="JV">Bearskin Airline</option>
                                                        <option value="4T">Belair</option>
                                                        <option value="B2">Belavia</option>
                                                        <option value="LZ">Belle Air</option>
                                                        <option value="B3">Bellview</option>
                                                        <option value="O3">Bellview Airline</option>
                                                        <option value="CH">Bemidji</option>
                                                        <option value="A8">Benin Golf Air</option>
                                                        <option value="8E">Bering Air</option>
                                                        <option value="J8">Berjaya Air</option>
                                                        <option value="5Q">Best Air</option>
                                                        <option value="BH">BH Airlines</option>
                                                        <option value="BG">Biman Bangladesh</option>
                                                        <option value="NT">Binter Canarias</option>
                                                        <option value="4V">Birdy Airlines</option>
                                                        <option value="5Z">Bismillah Air</option>
                                                        <option value="BV">Blue Panorama</option>
                                                        <option value="QW">Blue Wings</option>
                                                        <option value="KF">Blue1</option>
                                                        <option value="BD">Bmi British</option>
                                                        <option value="WW">Bmibaby</option>
                                                        <option value="Z2">Bravo Air Congo</option>
                                                        <option value="FQ">Brindabella Air</option>
                                                        <option value="DB">Brit Air</option>
                                                        <option value="BA">British Airways</option>
                                                        <option value="TH">British Citiexpr</option>
                                                        <option value="BS">British Intl</option>
                                                        <option value="SN">Brussels Airline</option>
                                                        <option value="FB">Bulgaria Air</option>
                                                        <option value="UZ">Buraq Air</option>
                                                        <option value="XD">Bus</option>
                                                        <option value="MO">Calm Air Intl</option>
                                                        <option value="R9">Camai Air</option>
                                                        <option value="9O">Cameroon Air</option>
                                                        <option value="5T">Canadian North</option>
                                                        <option value="C6">Canjet</option>
                                                        <option value="9K">Cape Air</option>
                                                        <option value="6C">Cape Smythe Air</option>
                                                        <option value="3Q">Carib Aviation</option>
                                                        <option value="BW">Caribbean Air</option>
                                                        <option value="V3">Carpatair</option>
                                                        <option value="C4">Carriacou</option>
                                                        <option value="XP">Casino Express</option>
                                                        <option value="RV">Caspian Airlines</option>
                                                        <option value="CX">Cathay Pacific</option>
                                                        <option value="KX">Cayman Airways</option>
                                                        <option value="XK">CCM Airlines</option>
                                                        <option value="5J">Cebu Pacific Air</option>
                                                        <option value="C2">CEIBA Intercon</option>
                                                        <option value="9M">Central Mtn Air</option>
                                                        <option value="C0">Centralwings</option>
                                                        <option value="J7">Centre-Avia</option>
                                                        <option value="OP">Chalk's Airlines</option>
                                                        <option value="5U">Challenge Aero</option>
                                                        <option value="XI">Charter</option>
                                                        <option value="RP">Chautauqua</option>
                                                        <option value="CI">China Airlines</option>
                                                        <option value="MU">China Eastern</option>
                                                        <option value="G5">China Express</option>
                                                        <option value="CZ">China Southern</option>
                                                        <option value="PN">China West Air</option>
                                                        <option value="OQ">Chongqing Air</option>
                                                        <option value="QI">Cimber Sterling</option>
                                                        <option value="C9">Cirrus Airlines</option>
                                                        <option value="CF">City Airline</option>
                                                        <option value="WX">City Jet</option>
                                                        <option value="XG">Clickair</option>
                                                        <option value="6P">Clubair Sixgo</option>
                                                        <option value="9L">Colgan Air</option>
                                                        <option value="OH">Comair Inc.</option>
                                                        <option value="C5">CommutAir</option>
                                                        <option value="KR">Comores Aviation</option>
                                                        <option value="I5">Compagnie Mali</option>
                                                        <option value="CP">Compass Airlines</option>
                                                        <option value="DE">Condor</option>
                                                        <option value="C3">Contact Air</option>
                                                        <option value="CO">Continental</option>
                                                        <option value="CM">Copa Airlines</option>
                                                        <option value="3C">Corp Express</option>
                                                        <option value="SS">Corsair</option>
                                                        <option value="DQ">Costal Air</option>
                                                        <option value="OU">Croatia Airlines</option>
                                                        <option value="QE">Crossair Europe</option>
                                                        <option value="CU">Cubana Airlines</option>
                                                        <option value="CY">Cyprus Airways</option>
                                                        <option value="OK">Czech Airlines</option>
                                                        <option value="D3">Daallo Airlines</option>
                                                        <option value="N2">Dagestan Air</option>
                                                        <option value="DX">Danish Air</option>
                                                        <option value="D5">Dauair</option>
                                                        <option value="JD">Deer Air</option>
                                                        <option value="DL">Delta</option>
                                                        <option value="3D">Denim Air</option>
                                                        <option value="2A">Deutsche Bahn</option>
                                                        <option value="ES">DHL Intl</option>
                                                        <option value="D8">Djibouti Airl</option>
                                                        <option value="Z6">Dnieproavia</option>
                                                        <option value="7D">Donbassaero</option>
                                                        <option value="R6">DOT LT</option>
                                                        <option value="KA">Dragonair</option>
                                                        <option value="Y3">Driessen Svcs</option>
                                                        <option value="KB">Druk Air</option>
                                                        <option value="9H">Dutch Antilles</option>
                                                        <option value="H7">Eagle Uganda</option>
                                                        <option value="3E">East Asia</option>
                                                        <option value="T3">Eastern Airways</option>
                                                        <option value="U2">Easyjet Airline</option>
                                                        <option value="EU">Ecuatoriana</option>
                                                        <option value="WK">Edelweiss Air</option>
                                                        <option value="MS">Egyptair</option>
                                                        <option value="LY">El Al Israel</option>
                                                        <option value="A0">Elysair</option>
                                                        <option value="EK">Emirates Air</option>
                                                        <option value="7H">Era Aviation</option>
                                                        <option value="YE">Eram Airlines</option>
                                                        <option value="B8">Eritrean Airline</option>
                                                        <option value="K9">Esen Air</option>
                                                        <option value="OV">Estonian Air</option>
                                                        <option value="ET">Ethiopian Air</option>
                                                        <option value="EY">Etihad Airways</option>
                                                        <option value="5B">Euro- Asia Air</option>
                                                        <option value="UI">Eurocypria Air</option>
                                                        <option value="GJ">Eurofly</option>
                                                        <option value="K2">Eurolot S.A.</option>
                                                        <option value="5O">Europe Airpost</option>
                                                        <option value="QY">European Air</option>
                                                        <option value="EA">European Express</option>
                                                        <option value="9F">Eurostar</option>
                                                        <option value="EW">Eurowings</option>
                                                        <option value="BR">EVA Airways</option>
                                                        <option value="3Z">Everts Air</option>
                                                        <option value="JN">Excel Airways</option>
                                                        <option value="OW">Executive Air</option>
                                                        <option value="XE">Express Jet</option>
                                                        <option value="IH">Falcon Air</option>
                                                        <option value="EF">Far Eastern Air</option>
                                                        <option value="AY">Finnair</option>
                                                        <option value="FC">Finncomm Air</option>
                                                        <option value="FY">Firefly</option>
                                                        <option value="7F">First Air</option>
                                                        <option value="DP">First Choice Air</option>
                                                        <option value="5H">Five Fourty</option>
                                                        <option value="RF">Florida West</option>
                                                        <option value="E7">Fly European</option>
                                                        <option value="F3">Fly Excellent</option>
                                                        <option value="F7">Flybaboo</option>
                                                        <option value="BE">Flybe</option>
                                                        <option value="7Y">Flying Carpet</option>
                                                        <option value="LF">FlyNordic</option>
                                                        <option value="Q5">Forty Mile Air</option>
                                                        <option value="FP">Freedom Air Guam</option>
                                                        <option value="F8">Freedom Airlines</option>
                                                        <option value="F9">Frontier</option>
                                                        <option value="2F">Frontier Flying</option>
                                                        <option value="FH">Futura Intl Air</option>
                                                        <option value="GY">Gabon Airlines</option>
                                                        <option value="GP">Gadair</option>
                                                        <option value="GA">Garuda</option>
                                                        <option value="4G">Gazpromavia</option>
                                                        <option value="GT">GB Airways</option>
                                                        <option value="A9">Georgian Airways</option>
                                                        <option value="QB">Georgian Natl</option>
                                                        <option value="4U">Germanwings</option>
                                                        <option value="G0">Ghana Intl</option>
                                                        <option value="Z5">GMG Airlines</option>
                                                        <option value="G3">Gol Trans</option>
                                                        <option value="DC">Golden Air</option>
                                                        <option value="CN">Grand China Air</option>
                                                        <option value="GS">Grand China Exp</option>
                                                        <option value="GV">Grant Aviation</option>
                                                        <option value="ZK">Great Lakes</option>
                                                        <option value="IJ">GREAT WALL</option>
                                                        <option value="3R">Gromov Air</option>
                                                        <option value="GF">Gulf Air</option>
                                                        <option value="6G">Gulfstream</option>
                                                        <option value="3M">Gulfstream Intl</option>
                                                        <option value="H6">Hageland</option>
                                                        <option value="HR">Hahn Air</option>
                                                        <option value="HU">Hainan Airlines</option>
                                                        <option value="7Z">Halcyon Air</option>
                                                        <option value="4R">Hamburg Intl</option>
                                                        <option value="HQ">Harmony Airways</option>
                                                        <option value="HA">Hawaiian</option>
                                                        <option value="HN">Heavylift Cargo</option>
                                                        <option value="H4">Heli Securite</option>
                                                        <option value="YO">Heli-Air Monaco</option>
                                                        <option value="JB">Helijet Intl</option>
                                                        <option value="L5">Helikopter Svc</option>
                                                        <option value="ZU">Helios Airways</option>
                                                        <option value="2L">Helvetic Airways</option>
                                                        <option value="EO">Hewa Bora</option>
                                                        <option value="UD">Hex Air</option>
                                                        <option value="8H">Highland Airways</option>
                                                        <option value="HD">Hokkaido Intl</option>
                                                        <option value="HB">Homer Air</option>
                                                        <option value="HX">Hong Kong Air</option>
                                                        <option value="UO">Hong Kong Expres</option>
                                                        <option value="QX">Horizon Air</option>
                                                        <option value="IB">Iberia</option>
                                                        <option value="FW">IBEX Airlines</option>
                                                        <option value="X8">Icaro</option>
                                                        <option value="FI">Icelandair</option>
                                                        <option value="V8">Iliamna Air Taxi</option>
                                                        <option value="IK">Imair</option>
                                                        <option value="DH">Independence Air</option>
                                                        <option value="IC">Indian Airlines</option>
                                                        <option value="6E">Indigo Air</option>
                                                        <option value="QZ">Indonesia Air</option>
                                                        <option value="7N">Inland Aviation</option>
                                                        <option value="7I">Insel Air</option>
                                                        <option value="D6">Interair</option>
                                                        <option value="IO">Intercontinental</option>
                                                        <option value="4O">Interjet</option>
                                                        <option value="ID">Interlink Air</option>
                                                        <option value="3L">Intersky</option>
                                                        <option value="IR">Iran Air</option>
                                                        <option value="EP">Iran Asseman</option>
                                                        <option value="IA">Iraqi Airways</option>
                                                        <option value="2O">Island Air</option>
                                                        <option value="WP">Island Air Hawai</option>
                                                        <option value="IS">Island Air Nantk</option>
                                                        <option value="HH">Islandsflug</option>
                                                        <option value="IF">Islas Airways</option>
                                                        <option value="WC">Islena Airlines</option>
                                                        <option value="6H">Israir</option>
                                                        <option value="FS">Itali Airlines</option>
                                                        <option value="GI">Itek Air</option>
                                                        <option value="I3">Ivoirienne Trans</option>
                                                        <option value="4I">IzAir</option>
                                                        <option value="JC">JAL Express</option>
                                                        <option value="JO">JALways</option>
                                                        <option value="JL">Japan Airlines</option>
                                                        <option value="3X">Japan Commuter</option>
                                                        <option value="NU">Japan Trans Air</option>
                                                        <option value="JU">JAT Airways</option>
                                                        <option value="VJ">Jatayu Airlines</option>
                                                        <option value="J9">Jazeera Airways</option>
                                                        <option value="O2">Jet Air</option>
                                                        <option value="9W">Jet Airways</option>
                                                        <option value="PP">Jet Aviation</option>
                                                        <option value="S2">Jet Lite</option>
                                                        <option value="LS">Jet2.com</option>
                                                        <option value="B6">JetBlue</option>
                                                        <option value="J0">Jetlink Express</option>
                                                        <option value="JQ">Jetstar Airways</option>
                                                        <option value="3K">Jetstar Asia Air</option>
                                                        <option value="BL">Jetstar Pacific</option>
                                                        <option value="GX">JetX</option>
                                                        <option value="LJ">Jin Air</option>
                                                        <option value="3B">Job Air Central</option>
                                                        <option value="6J">Jubba Airways</option>
                                                        <option value="HO">Juneyao Airlines</option>
                                                        <option value="XC">K D Air</option>
                                                        <option value="RQ">Kam Air</option>
                                                        <option value="KT">Katmai Air</option>
                                                        <option value="KV">Kavminvodyavia</option>
                                                        <option value="KD">KD Avia</option>
                                                        <option value="FK">Keewatin Air</option>
                                                        <option value="M5">Kenmore Air</option>
                                                        <option value="4K">Kenn Borek Air</option>
                                                        <option value="KQ">Kenya Airways</option>
                                                        <option value="BZ">Keystone Air</option>
                                                        <option value="YK">Kibris Turkish</option>
                                                        <option value="IT">Kingfisher Air</option>
                                                        <option value="Y9">Kish Airlines</option>
                                                        <option value="KP">Kiwi Intl</option>
                                                        <option value="KL">KLM</option>
                                                        <option value="WA">KLM Cityhopper</option>
                                                        <option value="KE">Korean Air</option>
                                                        <option value="7B">Krasnoyarsk</option>
                                                        <option value="K4">Kronflyg</option>
                                                        <option value="MN">Kulula air</option>
                                                        <option value="KU">Kuwait Airways</option>
                                                        <option value="QH">Kyrgystan</option>
                                                        <option value="JF">L A B Flying</option>
                                                        <option value="WJ">Labrador</option>
                                                        <option value="LR">Lacsa</option>
                                                        <option value="N7">Lagun Air</option>
                                                        <option value="TM">LAM</option>
                                                        <option value="LA">Lan Airlines</option>
                                                        <option value="4M">Lan Argentina</option>
                                                        <option value="UC">Lan Chile Cargo</option>
                                                        <option value="XL">Lan Ecuador</option>
                                                        <option value="LU">Lan Express</option>
                                                        <option value="LP">Lan Peru</option>
                                                        <option value="QV">Lao Aviation</option>
                                                        <option value="J6">Larry's Flying</option>
                                                        <option value="QL">Laser Airlines</option>
                                                        <option value="6Y">Latcharter Air</option>
                                                        <option value="QJ">Latpass Airlines</option>
                                                        <option value="NG">Lauda Air</option>
                                                        <option value="HE">LGW</option>
                                                        <option value="LI">Liat Ltd</option>
                                                        <option value="LN">Libyan Airlines</option>
                                                        <option value="JT">Lion Air</option>
                                                        <option value="LM">Livingston</option>
                                                        <option value="LB">Lloyd Aereo Boli</option>
                                                        <option value="LO">LOT Polish</option>
                                                        <option value="XO">LTE Intl Airways</option>
                                                        <option value="LH">Lufthansa</option>
                                                        <option value="CL">Lufthansa Expr</option>
                                                        <option value="LE">Lugansk Airlines</option>
                                                        <option value="LG">Luxair</option>
                                                        <option value="5V">Lviv Airlines</option>
                                                        <option value="I2">Magenta Air</option>
                                                        <option value="W5">Mahan Air</option>
                                                        <option value="M2">Mahfooz</option>
                                                        <option value="MH">Malaysia</option>
                                                        <option value="MA">Malev Hungarian</option>
                                                        <option value="TF">Malmo Aviation</option>
                                                        <option value="R5">Malta Air</option>
                                                        <option value="JE">Mango</option>
                                                        <option value="NM">Manx2</option>
                                                        <option value="M7">Marsland Aviat</option>
                                                        <option value="MP">Martinair</option>
                                                        <option value="FZ">Master Airways</option>
                                                        <option value="YD">Mauritania Air</option>
                                                        <option value="MM">Medellin Aeronau</option>
                                                        <option value="IG">Meridiana</option>
                                                        <option value="YV">Mesa Airlines</option>
                                                        <option value="XJ">Mesaba Aviation</option>
                                                        <option value="MX">Mexicana</option>
                                                        <option value="CS">Micronesia</option>
                                                        <option value="ME">Middle East</option>
                                                        <option value="YX">Midwest Airlines</option>
                                                        <option value="MJ">Mihin Lanka</option>
                                                        <option value="MW">Mokulele Airline</option>
                                                        <option value="2M">Moldavian</option>
                                                        <option value="OM">Mongolian</option>
                                                        <option value="UJ">Montair Avaition</option>
                                                        <option value="YM">Montenegro</option>
                                                        <option value="M9">Motor Sich</option>
                                                        <option value="XV">MR Lines</option>
                                                        <option value="VZ">My TravelLite</option>
                                                        <option value="8M">Myanmar Airways</option>
                                                        <option value="C8">NAC Air</option>
                                                        <option value="T2">Nakina Air</option>
                                                        <option value="UE">Nasair</option>
                                                        <option value="NC">National Jet</option>
                                                        <option value="CE">Nationwide Air</option>
                                                        <option value="4J">Nationwide Air</option>
                                                        <option value="5C">Nature Air</option>
                                                        <option value="ZN">Naysa</option>
                                                        <option value="NO">Neos</option>
                                                        <option value="9X">New Axis Airways</option>
                                                        <option value="EJ">New England</option>
                                                        <option value="N9">Niger Air</option>
                                                        <option value="HG">Niki Luftfahrt</option>
                                                        <option value="DD">Nok Air</option>
                                                        <option value="JH">Nordeste-Linhas</option>
                                                        <option value="6N">Nordic Regional</option>
                                                        <option value="8N">Nordkalottflyg</option>
                                                        <option value="M3">North Flying</option>
                                                        <option value="NS">Northeastern Air</option>
                                                        <option value="NW">Northwest</option>
                                                        <option value="2G">Northwest Seapln</option>
                                                        <option value="J3">Northwestern Air</option>
                                                        <option value="HW">North-Wright</option>
                                                        <option value="9E">Nortwest Airlink</option>
                                                        <option value="DY">Norwegian Air</option>
                                                        <option value="BJ">Nouvelair Tunisi</option>
                                                        <option value="N6">Nuevo Continente</option>
                                                        <option value="O8">Oasis</option>
                                                        <option value="VC">Ocean Airlines</option>
                                                        <option value="5K">Odessa Airlines</option>
                                                        <option value="OA">Olympic Airlines</option>
                                                        <option value="WY">Oman Aviation</option>
                                                        <option value="OC">Omni</option>
                                                        <option value="N3">Omskavia</option>
                                                        <option value="EC">Openskies</option>
                                                        <option value="R2">Orenair</option>
                                                        <option value="OX">Orient Thai</option>
                                                        <option value="QO">Origin Pacific</option>
                                                        <option value="OL">Ostfriesisc Air</option>
                                                        <option value="XH">Other Travel</option>
                                                        <option value="ON">Our Airline</option>
                                                        <option value="OJ">Overland Airways</option>
                                                        <option value="O7">OzJet</option>
                                                        <option value="P8">P.L.A.S.</option>
                                                        <option value="Y5">Pace Airlines</option>
                                                        <option value="3F">Pacific Airways</option>
                                                        <option value="8P">Pacific Coastal</option>
                                                        <option value="9J">Pacific Island</option>
                                                        <option value="LW">Pacific Wings</option>
                                                        <option value="PK">Pakistan Intl</option>
                                                        <option value="9P">Palau Airlines</option>
                                                        <option value="PF">Palestinian Airl</option>
                                                        <option value="NR">Pamir Air</option>
                                                        <option value="PQ">PANAF</option>
                                                        <option value="7E">Panagra</option>
                                                        <option value="HI">Papillon</option>
                                                        <option value="CG">Papua New Guinea</option>
                                                        <option value="I7">Paramount Air</option>
                                                        <option value="P6">Pascan Aviation</option>
                                                        <option value="9Q">PB Air Company</option>
                                                        <option value="3O">Peau Vava U</option>
                                                        <option value="H9">Pegasus Airlines</option>
                                                        <option value="7V">Pelican Air</option>
                                                        <option value="KS">PenAir</option>
                                                        <option value="4B">Perimeter</option>
                                                        <option value="P9">Perm Airlines</option>
                                                        <option value="PR">Philippine</option>
                                                        <option value="PU">Pluna</option>
                                                        <option value="Z3">PM Air Inc</option>
                                                        <option value="U4">PMT Air</option>
                                                        <option value="PO">Polar Air Cargo</option>
                                                        <option value="PH">Polynesian</option>
                                                        <option value="PD">Porter Airlines</option>
                                                        <option value="PW">Precisionair</option>
                                                        <option value="FE">Primaris Airline</option>
                                                        <option value="8W">Private Wings</option>
                                                        <option value="5S">Profesionales</option>
                                                        <option value="P0">Proflight</option>
                                                        <option value="PB">Provincial Airl</option>
                                                        <option value="RI">PT. Mandella</option>
                                                        <option value="EB">Pullmantur Air</option>
                                                        <option value="QF">Qantas Airways</option>
                                                        <option value="QR">Qatar Airways</option>
                                                        <option value="QG">Qualiflyer Group</option>
                                                        <option value="Q0">Quebecair Expres</option>
                                                        <option value="XA">Railroad</option>
                                                        <option value="RT">RAK Airways</option>
                                                        <option value="RW">RAS Fluggesellsc</option>
                                                        <option value="7R">Red Sea Air</option>
                                                        <option value="RM">Regional Air</option>
                                                        <option value="FN">Regional Air MA</option>
                                                        <option value="YS">Regional CAE</option>
                                                        <option value="ZL">Regional Express</option>
                                                        <option value="QT">Regional Pacific</option>
                                                        <option value="SL">Rio Sul</option>
                                                        <option value="RH">Robin Hood</option>
                                                        <option value="RR">Royal Air Force</option>
                                                        <option value="AT">Royal Air Maroc</option>
                                                        <option value="V5">Royal Aruban</option>
                                                        <option value="4A">Royal Bengal Air</option>
                                                        <option value="BI">Royal Brunei</option>
                                                        <option value="RJ">Royal Jordanian</option>
                                                        <option value="RA">Royal Nepal</option>
                                                        <option value="RY">Royal Wings</option>
                                                        <option value="FV">Russian Airlines</option>
                                                        <option value="WB">Rwandair Express</option>
                                                        <option value="FR">RyanAir</option>
                                                        <option value="YB">S African Expres</option>
                                                        <option value="4Q">Safi Airways</option>
                                                        <option value="PV">Saint Barth</option>
                                                        <option value="HZ">Sakhalinskie</option>
                                                        <option value="S6">Salmon Air</option>
                                                        <option value="ZS">Sama</option>
                                                        <option value="JI">San Juan Aviatio</option>
                                                        <option value="S3">Santa Barbara</option>
                                                        <option value="EX">Santo Domingo</option>
                                                        <option value="SK">SAS</option>
                                                        <option value="BU">SAS Norway</option>
                                                        <option value="SP">SATA Air Acores</option>
                                                        <option value="S4">SATA Intl</option>
                                                        <option value="9N">SATENA</option>
                                                        <option value="SV">Saudi Arabian</option>
                                                        <option value="YR">Scenic Airlines</option>
                                                        <option value="CB">ScotAirways</option>
                                                        <option value="BB">Seaborne</option>
                                                        <option value="8D">Servant Air</option>
                                                        <option value="UG">Sevenair</option>
                                                        <option value="D2">Severstal</option>
                                                        <option value="NL">Shaheen Air Intl</option>
                                                        <option value="SC">Shandong</option>
                                                        <option value="FM">Shanghai</option>
                                                        <option value="ZH">Shenzhen Airline</option>
                                                        <option value="S5">Shuttle America</option>
                                                        <option value="S7">Siberian Air</option>
                                                        <option value="3U">Sichuan Airlines</option>
                                                        <option value="FT">Siem Reap</option>
                                                        <option value="MI">SilkAir</option>
                                                        <option value="SQ">Singapore Airl</option>
                                                        <option value="JW">Skippers</option>
                                                        <option value="H2">Sky Airline</option>
                                                        <option value="XW">Sky Express</option>
                                                        <option value="NE">SkyEurope</option>
                                                        <option value="5P">SkyEurope Air</option>
                                                        <option value="SI">Skynet Airlines</option>
                                                        <option value="5G">Skyservice</option>
                                                        <option value="XT">Skystar Airways</option>
                                                        <option value="AL">Skyway Airlines</option>
                                                        <option value="JZ">Skyways</option>
                                                        <option value="OO">Skywest (Utah)</option>
                                                        <option value="XR">Skywest Air (AU)</option>
                                                        <option value="S0">Slok Air Intl</option>
                                                        <option value="6Q">Slovak Air</option>
                                                        <option value="QS">Smartwings</option>
                                                        <option value="2E">Smokey Bay Air</option>
                                                        <option value="2C">SNCF</option>
                                                        <option value="IE">Solomon</option>
                                                        <option value="SA">South African</option>
                                                        <option value="XZ">South AfricanEx</option>
                                                        <option value="YG">South Airlines</option>
                                                        <option value="DG">South East Asian</option>
                                                        <option value="PL">Southern Air Cht</option>
                                                        <option value="A4">Southern Winds</option>
                                                        <option value="WN">Southwest</option>
                                                        <option value="JK">Spanair</option>
                                                        <option value="SG">Spice Jet</option>
                                                        <option value="NK">Spirit Airlines</option>
                                                        <option value="UL">SriLankan</option>
                                                        <option value="SJ">Sriwijaya Air</option>
                                                        <option value="2I">Star Up</option>
                                                        <option value="NB">Sterling</option>
                                                        <option value="DM">Sterling Blue</option>
                                                        <option value="8F">STP Airways</option>
                                                        <option value="SD">Sudan Airways</option>
                                                        <option value="EZ">Sun Air Scandina</option>
                                                        <option value="SY">Sun Country</option>
                                                        <option value="2U">Sun d'OR Airline</option>
                                                        <option value="XQ">SunExpress</option>
                                                        <option value="CQ">Sunshine Express</option>
                                                        <option value="WG">Sunwing</option>
                                                        <option value="PY">Surinam Airways</option>
                                                        <option value="HS">Svenska Direktfl</option>
                                                        <option value="Q4">Swazi Express</option>
                                                        <option value="SM">Swedline</option>
                                                        <option value="LX">SWISS</option>
                                                        <option value="RB">Syrian Arab Air</option>
                                                        <option value="DT">TAAG Angola</option>
                                                        <option value="TA">Taca Intl</option>
                                                        <option value="VR">TACV Cabo Verde</option>
                                                        <option value="7J">Tajikair</option>
                                                        <option value="PZ">TAM del Mercosur</option>
                                                        <option value="JJ">TAM Linhas Aerea</option>
                                                        <option value="TQ">Tandem Aero</option>
                                                        <option value="K3">Taquan Air Svc</option>
                                                        <option value="RO">Tarom-Romanian</option>
                                                        <option value="SF">Tassili Air</option>
                                                        <option value="U9">Tatarstan</option>
                                                        <option value="L6">Tbilaviamsheni</option>
                                                        <option value="RU">TCI Skyking</option>
                                                        <option value="L9">Teamline Air Luf</option>
                                                        <option value="FD">Thai AirAsia</option>
                                                        <option value="TG">Thai Intl</option>
                                                        <option value="MT">Thomas Cook Air</option>
                                                        <option value="BY">Thomsonfly</option>
                                                        <option value="7Q">Tibesti AirLibya</option>
                                                        <option value="TR">Tiger Airways</option>
                                                        <option value="TT">Tiger Airways</option>
                                                        <option value="9D">Toumai Air Tchad</option>
                                                        <option value="WI">Tradewinds</option>
                                                        <option value="T8">Tran African Air</option>
                                                        <option value="N4">Trans Air Benin</option>
                                                        <option value="Q8">Trans Air Congo</option>
                                                        <option value="T9">Trans Meridian</option>
                                                        <option value="7T">Trans North</option>
                                                        <option value="UN">Transaero</option>
                                                        <option value="GE">TransAsia</option>
                                                        <option value="HV">Transavia</option>
                                                        <option value="9T">Travelspan</option>
                                                        <option value="OR">Tui Airlines</option>
                                                        <option value="TB">Tui Airlines</option>
                                                        <option value="X3">Tuifly</option>
                                                        <option value="R4">Tulpar Avia</option>
                                                        <option value="TU">Tunis Air</option>
                                                        <option value="TK">Turkish Airlines</option>
                                                        <option value="T5">Turkmenistan</option>
                                                        <option value="T7">Twin Jet</option>
                                                        <option value="VO">Tyrolean</option>
                                                        <option value="UH">U.S. Helicopter</option>
                                                        <option value="PS">Ukraine Intl</option>
                                                        <option value="UF">Ukranian Mediter</option>
                                                        <option value="B7">Uni Airways</option>
                                                        <option value="UA">United</option>
                                                        <option value="8X">United (Passive)</option>
                                                        <option value="4H">United Airways</option>
                                                        <option value="UW">Universal</option>
                                                        <option value="3Y">UNIWAYS</option>
                                                        <option value="U6">Ural Airlines</option>
                                                        <option value="US">US Airways</option>
                                                        <option value="U5">USA 3000</option>
                                                        <option value="UT">UTAir</option>
                                                        <option value="HY">Uzbekistan</option>
                                                        <option value="VF">Valuair LTD</option>
                                                        <option value="X4">Vanair</option>
                                                        <option value="VA">V-Australia</option>
                                                        <option value="2R">Via Rail</option>
                                                        <option value="VI">Vieques Air Lnk</option>
                                                        <option value="VN">Vietnam Air</option>
                                                        <option value="4P">Viking Airlines</option>
                                                        <option value="NN">VIM Airlines</option>
                                                        <option value="BF">Vincent Aviation</option>
                                                        <option value="VQ">Vintage</option>
                                                        <option value="V6">VIP S.A</option>
                                                        <option value="VX">Virgin America</option>
                                                        <option value="VS">Virgin Atlantic</option>
                                                        <option value="DJ">Virgin Blue</option>
                                                        <option value="VK">Virgin Nigeria</option>
                                                        <option value="ZG">Viva Macau</option>
                                                        <option value="XF">Vladivostok Air</option>
                                                        <option value="VG">VLM Airlines</option>
                                                        <option value="VE">Volare</option>
                                                        <option value="Y4">Volaris</option>
                                                        <option value="VY">Vueling Airlines</option>
                                                        <option value="4W">Warbelows Air</option>
                                                        <option value="WT">Wasaya Airways</option>
                                                        <option value="KW">Wataniya Airline</option>
                                                        <option value="2W">Welcome Air</option>
                                                        <option value="YH">West Caribbean</option>
                                                        <option value="8O">West Coast Air</option>
                                                        <option value="WS">WestJet</option>
                                                        <option value="WF">Wideroes Flyvese</option>
                                                        <option value="9C">Wimbi Dira Air</option>
                                                        <option value="IV">Wind Jet</option>
                                                        <option value="7W">Wind Rose</option>
                                                        <option value="WM">Windward Island</option>
                                                        <option value="IW">Wings Air</option>
                                                        <option value="K5">Wings of Alaska</option>
                                                        <option value="8Z">Wizz Air</option>
                                                        <option value="W6">Wizz Air</option>
                                                        <option value="WU">Wizz Air Ukraine</option>
                                                        <option value="WO">World Airways</option>
                                                        <option value="8V">Wright Air Svc</option>
                                                        <option value="2X">Xexox</option>
                                                        <option value="MF">Xiamen Airlines</option>
                                                        <option value="SE">XL Airways</option>
                                                        <option value="Y8">Yakutia</option>
                                                        <option value="YC">Yamal Airlines</option>
                                                        <option value="HK">Yangon Airways</option>
                                                        <option value="Y0">Yellow Air Taxi</option>
                                                        <option value="IY">Yemenia Yemen</option>
                                                        <option value="4Y">Yute Air Alaska</option>
                                                        <option value="ZJ">Zambezi Airlines</option>
                                                        <option value="K8">Zambia Skyways</option>
                                                        <option value="Q3">Zambian Airways</option>
                                                        <option value="B4">Zan Air</option>
                                                        <option value="Z4">Zoom Airlines</option>
                                                        <option value="ZX">Zoom Airlines UK</option>

                                                    </select>
                                                </div>
                                            </div>
                                            <div class="flexibility">
                                                <label>Flexibility:</label>
                                                <div class="selectbx">
                                                    <select class="" name="Flexibility" id="Flexibility" data-msg-required="Please Select the Flexibility" data-rule-required="true">
                                                        <option value="0">Exact Dates</option>
                                                        <option value="1">+/- 1 day</option>
                                                        <option value="2">+/- 2 days</option>
                                                        <option value="3">+/- 3 days</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                </div>

                                <div role="tabpanel" class="tab-pane MultiCiti" id="MultiCiti">

                                    <div class="MultiCiti-list-item" id="MultuCityItem1">
                                        <div class="from" id="multifrom1">
                                            <input type="text" name="MultiCityDeparture1" onfocus="autocomplete1(this)" class="dpIcon" required id="MultiCityDeparture1" placeholder="Enter city or airport">
                                        </div>

                                        <div class="to" id="multito1">
                                            <input type="text" name="MultiCityDestination1" onfocus="autocomplete1(this)" class="ArvIcon" required id="MultiCityDestination1" placeholder="Enter city or airport">
                                        </div>

                                        <div class="deprtDate" id="multidepDate1">
                                            <input type="text" name="MultiCityDepartureDate1" readonly class="dpDateIcon" required id="MultiCityDepartureDate1" placeholder="Departure">
                                        </div>

                                    </div>

                                    <div class="MultiCiti-list-item" id="MultuCityItem2">
                                        <div class="from" id="multifrom2">
                                            <input type="text" name="MultiCityDeparture2" onfocus="autocomplete1(this)" class="dpIcon" required id="MultiCityDeparture2" placeholder="Enter city or airport">
                                        </div>

                                        <div class="to" id="multito2">
                                            <input type="text" name="MultiCityDestination2" onfocus="autocomplete1(this)" class="ArvIcon" required id="MultiCityDestination2" placeholder="Enter city or airport">
                                        </div>

                                        <div class="deprtDate" id="multidepDate2">
                                            <input type="text" name="MultiCityDepartureDate2" readonly class="dpDateIcon" required id="MultiCityDepartureDate2" placeholder="Departure">
                                        </div>
                                        <div class="addRemovebtns">
                                            <button type="button" class="btn addButton" id="AddSegment2" onclick="AddSegment(this)"><i class="fa fa-plus-square"></i>Add Segment</button>
                                            <button type="button" class="btn removeButton" id="RemoveSegment2" onclick="RemoveSegment(this)" style="display: none;"><i class="fa fa-trash-o"></i>Remove</button>
                                        </div>
                                    </div>
                                    <div class="MultiCiti-list-item" name="MultuCityItemExtra" id="MultuCityItemExtra">
                                    </div>
                                    <div class="travellers" id="multitraveler1">
                                        <input type="text" name="MultiCityTravellers1" readonly class="" required id="MultiCityTravellers1" placeholder="1 Person, Economy">
                                        <div class="options-popup traveler-pop" id="MultiCitytravelerpop">
                                            <div class="options-popup-header">
                                                <span>Travellers</span>
                                                <span class="options-popup-close" id="Multicityoptionspopupclose"><i class="fa fa-times"></i></span>
                                            </div>
                                            <div class="passengers">
                                                <div class="row">
                                                    <div class="buttons">
                                                        <div id='MultiCityminAdult' class="minus" tabindex="-1" onclick="ChildDecrease1(this)" data-init="minus"></div>
                                                        <div id='MultiCitycountAdult' class="count" title="Adults" data-init="count">1</div>
                                                        <div id='MultiCityplAdult' class="plus" onclick="ChildIncrease1(this)" tabindex="-1" data-init="plus"></div>
                                                    </div>
                                                    <div class="description">
                                                        <div class="name">Adults</div>
                                                        <div class="age">(11+ Years)</div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="buttons">
                                                        <div id='MultiCityminChild' class="minus" tabindex="-1" onclick="ChildDecrease1(this)" data-init="minus"></div>
                                                        <div id='MultiCitycountChild' class="count" data-init="count">0</div>
                                                        <div id='MultiCityplChild' class="plus" onclick="ChildIncrease1(this)" tabindex="-1" data-init="plus"></div>
                                                    </div>

                                                    <div class="description">
                                                        <div class="name">Children</div>
                                                        <div class="age">(2-11 Years)</div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="buttons">
                                                        <div id='MultiCityminInfant' class="minus" tabindex="-1" onclick="ChildDecrease1(this)" data-init="minus" data-metric="options infants-minus"></div>
                                                        <div id='MultiCitycountInfant' class="count" data-init="count">0</div>
                                                        <div id='MultiCityplInfant' class="plus" onclick="ChildIncrease1(this)" tabindex="-1" data-init="plus" data-metric="options infants-plus"></div>
                                                    </div>

                                                    <div class="description">
                                                        <div class="name">Infant in lap</div>
                                                        <div class="age">(0-2 Years)</div>
                                                    </div>
                                                </div>
                                                <div class="row" id="error1" style="display: none">
                                                </div>
                                            </div>

                                            <div class="trip-class">
                                                <span>Flight Class</span>
                                                <div class="radio" tabindex="-1" data-metric="options business-type">
                                                    <input checked="checked" name="MultiCityClassType" onchange="class1change1(this)" id="MultiCityEconomy" class="radio-custom" style="margin: 0px" type="radio">
                                                    <label class="radio-custom-label" for="MultiCityEconomy">Economy</label>
                                                </div>
                                                <div class="radio" tabindex="-1" data-metric="options business-type">
                                                    <input class="radio-custom" id="MultiCityBusiness" onchange="class1change1(this)" name="MultiCityClassType" style="margin: 0px" type="radio">
                                                    <label class="radio-custom-label" for="MultiCityBusiness">Business</label>
                                                </div>
                                                <div class="radio" tabindex="-1" data-metric="options business-type">
                                                    <input class="radio-custom" id="MultiCityFirst" onchange="class1change1(this)" name="MultiCityClassType" style="margin: 0px" type="radio">
                                                    <label class="radio-custom-label" for="MultiCityFirst">First</label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="pm-error warning noteImp" id="Errors" style="display: none">
                                </div>
                                <%--<div class="SearchBtn-wrap">
                                    <span>
                                        <asp:Button runat="server" ID="BtnSearch" CssClass="btn SearchFlightBtn" OnClick="Button1_Click" OnClientClick="return btn_Submit();" Text="Search Flights" /></span>
                                </div>--%>
                            </div>
                    </form>
                    </div>

            	
            </div>
        </div>
    </div>
</section>




         
<section class="subscribe-bg">
<div class="container">
	<div class="row">
    	<div class="col-md-12">
            <form>
        	<div class="subscribe-wrap">

            	<div class="subscribe-icon"><img src="Design/images/signup-icon.png" alt="Subscribe Icon"></div>

               
                <div class="subscribe-text"><h4>Get the latest special and great offers from TravelMerry</h4></div>
                <div class="subscribe-btn">
                	<div class="form-group">
                    	<input type="email" name="email" id="newsletteremail" class="form-control" placeholder="Enter your email" >
                        <span class="" id="news-message"></span>
                    </div>
                    <div class="sub-button"><button type="button" class="btn btn-block" id="newsSignUP">Subscribe</button></div>
                </div>
             


            </div>
                </form>
        </div>

    </div>
</div>
</section>

    
    <section class="land-airline-wrap">
	<div class="container">
    	<div class="row">
        	<div class="col-md-6 col-sm-6 col-xs-12">

 <div class="comment land-contentText more">
            <%=Airline_Description %>

</div>


        	</div>
            <div class="col-md-6 col-sm-6 col-xs-12" id="popular_routes"> </div>
        
        </div>	
    </div>
</section>

<div class="tab-style" role="tabpanel">
<div class="container">
    	<div class="row">
        <div class="col-md-12">
        <h2 class="text-center"><% =Airline_header %> <img src="Design/images/traveller-icon.png" alt="Trally icon"></h2>
	<div class="nav-tabs-full">
    	<ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#flightClass" aria-controls="home" role="tab" data-toggle="tab"><i class="fa fa-plane"></i> Flight Classes</a></li>
                    <li role="presentation"><a href="#fleets" aria-controls="profile" role="tab" data-toggle="tab"><i class="fa fa-cube"></i> Fleets</a></li>
            <li role="presentation"><a href="#baggage" aria-controls="profile" role="tab" data-toggle="tab"><i class="fa fa-cube"></i>Baggage Policy</a></li>

                    <%--<li><a href="https://www.travelmerry.com/ViewBaggageInfo.aspx" target="_blank"><i class="fa fa-suitcase"></i>Baggage Policy</a></li>--%>
                	<li role="presentation"><a href="#otherPolicies" aria-controls="settings" role="tab" data-toggle="tab"><i class="fa fa-comment"></i>Other</a></li>
                </ul>    	
    </div>
	<div class="tab-content-full">
    	<div class="tab-content">
            <%=Airline_information %>

                </div>
    </div>
    </div>
    </div>
</div>
</div>
<section class="land-popular-wrap">
	<div class="container">
    	<div class="row">
        <div class="col-md-12">
        	<h2>Popular Airlines <img src="Design/images/traveller-icon.png" alt="Trally icon"></h2>
        </div>
            <div id="top_Airlines"></div>
        	
        </div>
    </div>
</section>


<!--#include file=~/footer.aspx-->




<!--

 <div class="alert alert-warning alert-dismissible" data-auto-dismiss role="alert">
 <div class="container">
  <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
  <p>We use cookies to offer an improved online experience and offer you content and services adopted to your interests. By using TravelMerry you are giving your consent to  our cookie policy. <a href="privacy_policy.html"> Privacy Policy</a></p>
  </div>
</div>
-->



<script type="text/javascript" src="Design/build/js/moment.min.js"></script>
<script type="text/javascript" src="Design/build/js/caleran.min.js"></script>

<script type="text/javascript" src="Design/js/indscript.js"></script>
 
    


<script src="js/client.min.js"></script>
    <script>

        var client = new ClientJS();
        var fingerprint = client.getFingerprint();
        console.log(fingerprint);
        var obj = {};
        obj.fingerprint = fingerprint;


        $.ajax({
            type: "POST",
            url: "dynamicAirlines.aspx/Update_Device_ID",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // alert(msg.d);
            }
        });

        console.log(fingerprint);
    </script>



<script>
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();
});
</script>
<!-----==== Show/Hide btn js--->
<script type="text/javascript">
$(document).ready(function() {
	var showChar = 1250;
	var ellipsestext = "...";
	//var moretext = "Read Less..";
	//var lesstext = "Read More...";
        
    var moretext = "Read More...";
	var lesstext = "Read Less..";
	$('.more').each(function() {
		var content = $(this).html();

		if(content.length > showChar) {

			var c = content.substr(0, showChar);
			var h = content.substr(showChar, content.length + showChar);

			var html = c + '<span class="moreelipses">'+ ellipsestext +'</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">'+ moretext +'</a></span>';

            //var html = h + '</span>&nbsp;&nbsp;<a href="" class="morelink">'+moretext+'</a></span>';

			$(this).html(html);
		}

	});

	$(".morelink").click(function(){
		if($(this).hasClass("less")) {
			$(this).removeClass("less");
			$(this).html(moretext);
		} else {
			$(this).addClass("less");
			$(this).html(lesstext);
		}
		$(this).parent().prev().toggle();
		$(this).prev().toggle();
		return false;
	});
});


</script>

   
</body>


</html>
