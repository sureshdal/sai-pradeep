<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassengerDetails.aspx.cs" Inherits="PassengerDetails" %>

<!DOCTYPE html>
<html lang="en">
<head>
    	<!--
	<meta charset="utf-8">
    	<meta http-equiv="X-UA-Compatible" content="IE = edge">
    	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    	<title>TravelMerry - US</title>Results
	-->

<%=Dymanic_Metadata_Db %>

    <link rel="shortcut icon" href="Design/images/favicon.ico">


    <link rel="stylesheet" type="text/css" href="Design/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/tmStyle.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/resultstyle.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/giftvoucherStyle.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/psDetails.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/checkboxstyle.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/font-awesome-animation.min.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/icofont.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/ccard-style.css" />
    <%--<link rel="stylesheet" type="text/css" href="Design/css/jp-card-style.css" />--%>
    <link rel="stylesheet" type="text/css" href="Design/css/tooltip-curved.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/tmpm_link.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/rsw-style.css" />

    <link rel="stylesheet" type="text/css" href="Design/css/popup-paylater.css" />
    <link rel="stylesheet" type="text/css" href="Design/css/jivo_over.css" />

    <!--[if lt IE 9]>
    <script src = "https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src = "https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <noscript>
        <div class="noscript">
            <p style="margin-left: 10px">JavaScript is not enabled.</p>
        </div>
    </noscript>

    <style>
        .error1 {
            border: 2px solid;
            border-color: #f49b9b;
        }
    </style>

    <script src="js/validate.js" type="text/javascript"></script>

    <script type="text/javascript">
        if (top.location != self.location)
            top.location = self.location;

    </script>
    <script type="text/javascript">


        function SessionExpireAlert(timeout) {
            var seconds = timeout / 1000;
            var asdf = (1200000 - 200000) * 1000;
            var count = 0;

            setInterval(function () {
                seconds--;
                document.getElementById("tripsummery").style.display = 'block';
                document.getElementById("divPriceDetails1").style.display = 'block';
                if (count == 0) {
                    document.getElementById("visitors").innerHTML = 1 + Math.floor(Math.random() * 20) + ' Visitors Viewing this Itinerary';
                }
                count++;
            }, 1000);
            setTimeout(function () {
                //$("#Refresh").css("display", "block");visitors
                document.getElementById("Refresh").style.display = "block";
                //document.getElementById("Refresh").dis ="display", "block");
            }, 1200000 - 120000);
            setTimeout(function () {
                window.location = "PassengerDetails.aspx";
            }, timeout);
        };
        function ResetSession() {
            //Redirect to refresh Session.
            window.location = window.location.href.split('#')[0];
        }
    </script>
    <script type="text/javascript">

        function val(ctrl) {
            //if (ctrl.value != "") {
            $('#' + ctrl.id).css('border', '');
            $('#' + ctrl.id).css('border-color', '');
            var re = /^[A-Za-z ]+$/;
            var ree1 = /^(0|[0-9]\d*)$/;
            var reee = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var addre = /^[a-zA-Z0-9~_,./: -#-]*$/;
            var totre = /^([A-Za-z ]){1,30}/;

            var ree = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
            var avoidspecial = /^[a-zA-Z0-9~_,: -]*$/
            if (ctrl.id == "Email") {
                if (ree.test(ctrl.value)) {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#Emailhint_hint").remove();
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Emailhint_hint").remove();
                    $("#Emailhint").append('<span id="Emailhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Email ID is not Valid</span></span>');
                    $("#Emailhint_hint").css("display", "block");
                }
            }

            else if (ctrl.id == "Email2") {
                if (ree.test(ctrl.value)) {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#Emailhint_hint1").remove();
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Emailhint_hint1").remove();
                    $("#Emailhint1").append('<span id="Emailhint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Email ID is not Valid</span></span>');
                    $("#Emailhint_hint1").css("display", "block");
                }
            }

            else if (ctrl.id == "Mobile") {
                var Mobile = document.forms["form1"]["Mobile"];
                if (ctrl.value != "") {
                    if (Mobile.value.length <= 10 || !ree1.test(ctrl.value)) {
                        if (ree1.test(ctrl.value)) {
                            if (Mobile.value.length < 10) {
                                $('#' + ctrl.id).css('border', '1px solid');
                                $('#' + ctrl.id).css('border-color', '#f49b9b');
                                $("#Mobilehint_hint").remove();
                                $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                                $("#Mobilehint_hint").css("display", "block");
                            }
                            else {
                                $('#' + ctrl.id).css('border', '');
                                $('#' + ctrl.id).css('border-color', '');
                                $("#Mobilehint_hint").remove();
                            }
                        }
                        else {
                            $('#' + ctrl.id).css('border', '1px solid');
                            $('#' + ctrl.id).css('border-color', '#f49b9b');
                            $("#Mobilehint_hint").remove();
                            $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Only Numeric Values</span></span>');
                            $("#Mobilehint_hint").css("display", "block");
                        }
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                        $("#Mobilehint_hint").remove();
                        $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                        $("#Mobilehint_hint").css("display", "block");
                    }
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Mobilehint_hint").remove();
                    $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                    $("#Mobilehint_hint").css("display", "block");
                }
            }

            else if (ctrl.id == "Mobile1") {
                var Mobile1 = document.forms["form1"]["Mobile1"];
                if (ctrl.value != "") {
                    if (Mobile1.value.length <= 10 || !ree1.test(ctrl.value)) {
                        if (ree1.test(ctrl.value)) {
                            if (Mobile1.value.length < 10) {
                                $('#' + ctrl.id).css('border', '1px solid');
                                $('#' + ctrl.id).css('border-color', '#f49b9b');
                                $("#Mobilehint_hint1").remove();
                                $("#Mobilehint1").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                                $("#Mobilehint_hint1").css("display", "block");
                            }
                            else {
                                $('#' + ctrl.id).css('border', '');
                                $('#' + ctrl.id).css('border-color', '');
                                $("#Mobilehint_hint1").remove();
                            }
                        }
                        else {
                            $('#' + ctrl.id).css('border', '1px solid');
                            $('#' + ctrl.id).css('border-color', '#f49b9b');
                            $("#Mobilehint_hint1").remove();
                            $("#Mobilehint1").append('<span id="Mobilehint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Only Numeric Values</span></span>');
                            $("#Mobilehint_hint1").css("display", "block");
                        }
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                        $("#Mobilehint_hint1").remove();
                        $("#Mobilehint1").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                        $("#Mobilehint_hint1").css("display", "block");
                    }
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Mobilehint_hint1").remove();
                    $("#Mobilehint1").append('<span id="Mobilehint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                    $("#Mobilehint_hint1").css("display", "block");
                }
            }

            else if (ctrl.id == "Address") {
                if (ctrl.value != "") {
                    if (addre.test(ctrl.value)) {
                        $('#' + ctrl.id).css('border', '');
                        $('#' + ctrl.id).css('border-color', '');
                        $("#Addresshint_hint").remove();
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                        $("#Addresshint_hint").remove();
                        $("#Addresshint").append('<span id="Addresshint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Address is Not Valid</span></span>');
                        $("#Addresshint_hint").css("display", "block");
                    }
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Addresshint_hint").remove();
                    $("#Addresshint").append('<span id="Addresshint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Address</span></span>');
                    $("#Addresshint_hint").css("display", "block");
                }
            }
            else if (ctrl.id == "cardNumber") {
                var cardId = document.forms["form1"]["cardNumber"];
                var flagCNO = allnumeric(cardId, 16, 12);
                if (!flagCNO) {
                    $('#cardNumber').css('border', '1px solid');
                    $('#cardNumber').css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please enter only numerical numbers for <strong>card Number</strong> ex 0,1,2...9</li>";
                    $("#cardNumberhint_hint").remove();
                    paymentcount++;
                    $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Number is not Valid</span></span>');
                    $("#cardNumberhint_hint").css("display", "block");
                }
                else {
                    if (!cardvalidation(cardId)) {
                        $('#cardNumber').css('border', '1px solid');
                        $('#cardNumber').css('border-color', '#f49b9b');
                        PaymentErrors += "<li> " + paymentcount + ". please enter only numerical numbers for <strong>card Number</strong> ex 0,1,2...9</li>";
                        paymentcount++;
                        flagCNO = false;
                        $("#cardNumberhint_hint").remove();
                        $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Number is not Valid</span></span>');
                        $("#cardNumberhint_hint").css("display", "block");
                    }
                    else {
                        $('#cardNumber').css('border', '');
                        $('#cardNumber').css('border-color', '');
                        $("#cardNumberhint_hint").remove();
                    }
                }

                var d = "";
                if (top.globCType == 'visa')
                    d = "VSA";
                else if (top.globCType == 'mastercard')
                    d = "MSC";
                else if (top.globCType == 'amex')
                    d = "AMEX";
                else
                    d = "";
                var flagCType = validate_dropDown("cardtype"); //ValidateCardType(cardType);
                $("#cardtype").val(d);
                if (flagCNO) {
                    if (top.globCType == "unknown" || top.globCType == undefined) {
                        $('#' + cardId.id).css('border', '1px solid');
                        $('#' + cardId.id).css('border-color', '#f49b9b');
                        PaymentErrors += "<li> " + paymentcount + ". Invalid <strong>Card Type</strong></li>";
                        paymentcount++;
                        $("#cardNumberhint_hint").remove();
                        $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Type is not Valid</span></span>');
                        $("#cardNumberhint_hint").css("display", "block");
                    }
                    else if (d == "") {
                        $('#' + cardId.id).css('border', '1px solid');
                        $('#' + cardId.id).css('border-color', '#f49b9b');
                        PaymentErrors += "<li> " + paymentcount + ". Invalid <strong>Card Type</strong></li>";
                        paymentcount++;
                        $("#cardNumberhint_hint").remove();
                        $("#cardNumberhint").append("<span id='cardNumberhint_hint' class='tooltip-content'>Card Type is not Valid</span>");
                        $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Type is not Valid</span></span>');
                        $("#cardNumberhint_hint").css("display", "block");
                    }
                    else {
                        $('#' + cardId.id).css('border', '');
                        $('#' + cardId.id).css('border-color', '');
                        $("#cardNumberhint_hint").remove();
                    }
                }
            }
            else if (ctrl.id == "ExpMonth") {
                var flagCMon = validate_dropDown("ExpMonth"); //validateMonth(monthCard);
                if (!flagCMon) {
                    $('#ExpMonth').css('border', '1px solid');
                    $('#ExpMonth').css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Month</strong></li>";
                    paymentcount++;
                    $("#ExpiryMonthhint_hint").remove();
                    $("#ExpiryMonthhint").append('<span id="ExpiryMonthhint_hint" class="tool-info"><span class="tool-head-title"> </span><span class="tool-body-text">Please Select Expiry Month</span></span>');
                    $("#ExpiryMonthhint_hint").css("display", "block");
                }
                else {
                    //cardNamehint cardNumberhint ExpiryMonthhint ExpiryYearhint cvvhint
                    $('#ExpMonth').css('border', '');
                    $('#ExpMonth').css('border-color', '');
                    $("#expiry").val($('#ExpMonth').val() + $('#ExpYear').val());
                    $("#ExpiryMonthhint_hint").remove();
                }
                var mn = $('#ExpMonth').val();
                var yr = $('#ExpYear').val();

                var current = new Date();
                var current_year = current.getFullYear();
                var current_Month = current.getMonth() + 1;
                var current_Day = current.getDate();

                if (current_year == parseInt(yr)) {
                    if (parseInt(mn) < current_Month) {
                        $('#ExpMonth').css('border', '1px solid');
                        $('#ExpMonth').css('border-color', '#f49b9b');
                        PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Month</strong></li>";
                        paymentcount++;
                        $("#ExpiryMonthhint_hint").remove();
                        $("#ExpiryMonthhint").append('<span id="ExpiryMonthhint_hint" class="tool-info"><span class="tool-head-title"> </span><span class="tool-body-text">Please Select Card Expiry Month</span></span>');
                        $("#ExpiryMonthhint_hint").css("display", "block");
                    }
                    else {
                        $('#ExpMonth').css('border', '');
                        $('#ExpMonth').css('border-color', '');
                        $("#ExpiryMonthhint_hint").remove();
                    }
                }
            }
            else if (ctrl.id == "ExpYear") {
                var flagCYear = validate_dropDown("ExpYear"); //validateYear(yearCard);
                if (!flagCYear) {
                    $('#ExpYear').css('border', '1px solid');
                    $('#ExpYear').css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Year</strong></li>";
                    paymentcount++;
                    $("#ExpiryYearhint_hint").remove();
                    $("#ExpiryYearhint").append('<span id="ExpiryYearhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Expiry Year</span></span>');
                    $("#ExpiryYearhint_hint").css("display", "block");
                }
                else {
                    $('#ExpYear').css('border', '');
                    $('#ExpYear').css('border-color', '');
                    $("#expiry").val($('#ExpMonth').val() + $('#ExpYear').val());
                    $("#ExpiryYearhint_hint").remove();
                }

                var mn = $('#ExpMonth').val();
                var yr = $('#ExpYear').val();

                var current = new Date();
                var current_year = current.getFullYear();
                var current_Month = current.getMonth() + 1;
                var current_Day = current.getDate();

                if (current_year == parseInt(yr)) {
                    if (parseInt(mn) < current_Month) {
                        $('#ExpMonth').css('border', '1px solid');
                        $('#ExpMonth').css('border-color', '#f49b9b');
                        PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Month</strong></li>";
                        paymentcount++;
                        $("#ExpiryMonthhint_hint").remove();
                        $("#ExpiryMonthhint").append('<span id="ExpiryMonthhint_hint" class="tool-info"><span class="tool-head-title"> </span><span class="tool-body-text">Please Select Card Expiry Month</span></span>');
                        $("#ExpiryMonthhint_hint").css("display", "block");
                    }
                    else {
                        $('#ExpMonth').css('border', '');
                        $('#ExpMonth').css('border-color', '');
                        $("#ExpiryMonthhint_hint").remove();
                    }
                }
            }
            else if (ctrl.id == "cvc") {
                var cardCvv = document.forms["form1"]["cvc"];
                var flagCvv = allnumericPhone(cardCvv, 4, 3);
                if (!flagCvv) {
                    $('#cvc').css('border', '1px solid');
                    $('#cvc').css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please enter only numerical numbers for <strong>CVV</strong> ex 0,1,2...9</li>";
                    paymentcount++;
                    $("#cvvhint_hint").remove();
                    $("#cvvhint").append('<span id="cvvhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">CVV is not Valid</span></span>');
                    $("#cvvhint_hint").css("display", "block");
                }
                else {
                    $('#cvc').css('border', '');
                    $('#cvc').css('border-color', '');
                    $("#cvvhint_hint").remove();
                }
            }
            else if (ctrl.id == "CardHolderName") {
                var flagCNa = required(document.getElementById("CardHolderName"));
                if (!flagCNa) {
                    $('#CardHolderName').css('border', '1px solid');
                    $('#CardHolderName').css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please enter valid <strong></strong></li>";
                    paymentcount++;
                    $("#cardNamehint_hint").remove();
                    $("#cardNamehint").append('<span id="cardNamehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Card Holder Name</span></span>');
                    $("#cardNamehint_hint").css("display", "block");
                }
                else {
                    if (re.test(ctrl.value)/*.match('^[a-zA-Z ]')*/) {
                        $('#CardHolderName').css('border', '');
                        $('#CardHolderName').css('border-color', '');
                        $("#cardNamehint_hint").remove();
                    }
                    else {
                        $('#CardHolderName').css('border', '1px solid');
                        $('#CardHolderName').css('border-color', '#f49b9b');
                        PaymentErrors += "<li> " + paymentcount + ". please enter valid <strong>Card Holder Name</strong></li>";
                        paymentcount++;
                        flagCNa = false;
                        $("#cardNamehint_hint").remove();
                        $("#cardNamehint").append('<span id="cardNamehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Holder Name is Not Valid</span></span>');
                        $("#cardNamehint_hint").css("display", "block");
                    }
                }
            }
            else if (ctrl.id == "ZipCode") {
                if (ctrl.value != "") {
                    if (avoidspecial.test(ctrl.value)) {
                        $('#' + ctrl.id).css('border', '');
                        $('#' + ctrl.id).css('border-color', '');
                        $("#Postcodehint_hint").remove();
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                        $("#Postcodehint_hint").remove();
                        $("#Postcodehint").append('<span id="Postcodehint_hint" class="tool-info"><span class="tool-head-title">span><span class="tool-body-text">Postal Code is Not Valid</span></span>');
                        $("#Postcodehint_hint").css("display", "block");
                    }
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Postcodehint_hint").remove();
                    $("#Postcodehint").append('<span id="Postcodehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Postal Code</span></span>');
                    $("#Postcodehint_hint").css("display", "block");
                }
            }
            else if (ctrl.id == "State") {
                if (ctrl.value != "") {
                    if (re.test(ctrl.value)) {
                        $('#' + ctrl.id).css('border', '');
                        $('#' + ctrl.id).css('border-color', '');
                        $("#Statehint_hint").remove();
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                        $("#Statehint_hint").remove();
                        $("#Statehint").append('<span id="Statehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">State is not Valid</span></span>');
                        $("#Statehint_hint").css("display", "block");
                    }
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Statehint_hint").remove();
                    $("#Statehint").append('<span id="Statehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter State</span></span>');
                    $("#Statehint_hint").css("display", "block");
                }
            }
            else if (ctrl.id == "City") {
                if (ctrl.value != "") {
                    if (re.test(ctrl.value)) {
                        $('#' + ctrl.id).css('border', '');
                        $('#' + ctrl.id).css('border-color', '');
                        $("#Cityhint_hint").remove();
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                        $("#Cityhint_hint").remove();
                        $("#Cityhint").append('<span id="Cityhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">City is Not Valid</span></span>');
                        $("#Cityhint_hint").css("display", "block");
                    }
                }
                else {
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                    $("#Cityhint_hint").remove();
                    $("#Cityhint").append('<span id="Cityhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter City</span></span>');
                    $("#Cityhint_hint").css("display", "block");
                }
            }
            else if (ctrl.id == "country") {
                flagcountry = validate_dropDown("country");
                if (!flagcountry) {
                    $("#Countryhint_hint").remove();
                    $("#Countryhint").append('<span id="Countryhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Country</span></span>');
                    $("#Countryhint_hint").css("display", "block");
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                }
                else {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#Countryhint_hint").remove();
                }
            }
            else if (ctrl.id.includes("title")) {
                var ct = ctrl.id.split('title');
                var tite = $("#ptitle" + ct[1]).val();
                flagtitle = validate_dropDown("ptitle" + ct[1]);
                if (!flagtitle) {
                    $("#ptitlehint_hint" + ct[1]).remove();
                    $("#ptitlehint" + ct[1]).append('<span id="ptitlehint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Title</span></span>');
                    $("#ptitlehint_hint" + ct[1]).css("display", "block");
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                }
                else {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#ptitlehint_hint" + ct[1]).remove();
                }
            }
            else if (ctrl.id.includes("day")) {
                var ct = ctrl.id.split('day');
                var tite = $("#dday" + ct[1]).val();
                flagtitle = validate_dropDown("dday" + ct[1]);
                if (!flagtitle) {
                    $("#ddayhint_hint" + ct[1]).remove();
                    $("#ddayhint" + ct[1]).append('<span id="ddayhint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Day</span></span>');
                    $("#ddayhint_hint" + ct[1]).css("display", "block");
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                }
                else {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#ddayhint_hint" + ct[1]).remove();
                }
            }
            else if (ctrl.id.includes("dmonth")) {
                var ct = ctrl.id.split('month');
                var tite = $("#dmonth" + ct[1]).val();
                flagtitle = validate_dropDown("dmonth" + ct[1]);
                if (!flagtitle) {
                    $("#dmonthhint_hint" + ct[1]).remove();
                    $("#dmonthhint" + ct[1]).append('<span id="dmonthhint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Month</span></span>');
                    $("#dmonthhint_hint" + ct[1]).css("display", "block");
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                }
                else {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#dmonthhint_hint" + ct[1]).remove();
                }
            }
            else if (ctrl.id.includes("dyear")) {
                var ct = ctrl.id.split('year');
                var tite = $("#dyear" + ct[1]).val();
                flagtitle = validate_dropDown("dyear" + ct[1]);
                if (!flagtitle) {
                    $("#dyearhint_hint" + ct[1]).remove();
                    $("#dyearhint" + ct[1]).append('<span id="dyearhint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Year</span></span>');
                    $("#dyearhint_hint" + ct[1]).css("display", "block");
                    $('#' + ctrl.id).css('border', '1px solid');
                    $('#' + ctrl.id).css('border-color', '#f49b9b');
                }
                else {
                    $('#' + ctrl.id).css('border', '');
                    $('#' + ctrl.id).css('border-color', '');
                    $("#dyearhint_hint" + ct[1]).remove();
                }
            }
            else {
                if (ctrl.nodeName == "SELECT") {
                    var flagCYear = validate_dropDown(ctrl.id); //validateYear(yearCard);
                    if (!flagCYear) {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                    }
                    else {
                        $('#' + ctrl.id).css('border', '');
                        $('#' + ctrl.id).css('border-color', '');
                    }
                }
                else if (ctrl.id.includes("lname") || ctrl.id.includes("fname") || ctrl.id.includes("mname")) {
                    var ct = ctrl.id.split('name');

                    var fmname = $("#mname" + ct[1]).val();
                    if (fmname != "") {
                        flagmiddleName = required(document.getElementById("mname" + ct[1]));
                        if (flagmiddleName) {
                            if (re.test(fmname)/*.match('^[a-zA-Z]{3,16}$')*/) {
                                $("#mname" + ct[1]).css('border', '');
                                $("#mname" + ct[1]).css('border-color', '');
                                $("#mnamehint_hint" + ct[1]).remove();
                            }
                            else {
                                $("#mname" + ct[1]).css('border', '1px solid');
                                $("#mname" + ct[1]).css('border-color', '#f49b9b');
                                flagmiddleName = false;
                                $("#mnamehint_hint" + ct[1]).remove();
                                $("#mnamehint" + ct[1]).append('<span id="mnamehint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title">Middle Name</span><span class="tool-body-text">Middle Name is Not Valid</span></span>');
                                $("#mnamehint_hint" + ct[1]).css("display", "block");
                            }
                        }
                        else {
                            $("#mname" + ct[1]).css('border', '');
                            $("#mname" + ct[1]).css('border-color', '');
                            flagmiddleName = true;
                            $("#mnamehint_hint" + ct[1]).remove();
                        }
                    }
                    else {
                        $("#mname" + ct[1]).css('border', '');
                        $("#mname" + ct[1]).css('border-color', '');
                        flagmiddleName = true;
                        $("#mnamehint_hint" + ct[1]).remove();
                    }
                    var flname = $("#lname" + ct[1]).val();
                    flaglastName = required(document.getElementById("lname" + ct[1]));
                    if (!flaglastName) {
                        $("#lname" + ct[1]).css('border', '1px solid');
                        $("#lname" + ct[1]).css('border-color', '#f49b9b');
                        $("#lnamehint_hint" + ct[1]).remove();
                        $("#lnamehint" + ct[1]).append('<span id="lnamehint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Last Name</span></span>');
                        $("#lnamehint_hint" + ct[1]).css("display", "block");
                    }
                    else {
                        if (re.test(flname)/*.match('^[a-zA-Z]{3,16}$')*/) {
                            $("#lname" + ct[1]).css('border', '');
                            $("#lname" + ct[1]).css('border-color', '');
                            $("#lnamehint_hint" + ct[1]).remove();
                        }
                        else {
                            $("#lname" + ct[1]).css('border', '1px solid');
                            $("#lname" + ct[1]).css('border-color', '#f49b9b');
                            flaglastName = false;
                            $("#lnamehint_hint" + ct[1]).remove();
                            $("#lnamehint" + ct[1]).append('<span id="lnamehint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Last Name not Valid</span></span>');
                            $("#lnamehint_hint" + ct[1]).css("display", "block");
                        }
                    }

                    var ffname = $("#fname" + ct[1]).val();
                    flagfirstName = required(document.getElementById("fname" + ct[1]));
                    if (!flagfirstName) {
                        $("#fname" + ct[1]).css('border', '1px solid');
                        $("#fname" + ct[1]).css('border-color', '#f49b9b');
                        $("#fnamehint_hint" + ct[1]).remove();
                        $("#fnamehint" + ct[1]).append('<span id="fnamehint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter First Name</span></span>');
                        $("#fnamehint_hint" + ct[1]).css("display", "block");
                    }
                    else {
                        if (re.test(ffname)/*.match('^[a-zA-Z]{3,16}$')*/) {
                            $('#' + ffname.id).css('border', '');
                            $('#' + ffname.id).css('border-color', '');
                            $("#fnamehint_hint" + ct[1]).remove();
                        }
                        else {
                            $("#fname" + ct[1]).css('border', '1px solid');
                            $("#fname" + ct[1]).css('border-color', '#f49b9b');
                            flagfirstName = false;
                            $("#fnamehint_hint" + ct[1]).remove();
                            $("#fnamehint" + ct[1]).append('<span id="fnamehint_hint' + ct[1] + '" class="tool-info"><span class="tool-head-title">span><span class="tool-body-text">First Name not Valid</span></span>');
                            $("#fnamehint_hint" + ct[1]).css("display", "block");
                        }
                    }
                }
                else {
                    if (re.test(ctrl.value)) {
                        $('#' + ctrl.id).css('border', '');
                        $('#' + ctrl.id).css('border-color', '');
                    }
                    else {
                        $('#' + ctrl.id).css('border', '1px solid');
                        $('#' + ctrl.id).css('border-color', '#f49b9b');
                    }
                }
            }
        }

        function validate(ctrl, type, ct) {
            var age = 0;
            var day = document.getElementById("dday" + ctrl).value;
            var month = document.getElementById("dmonth" + ctrl).value;
            var year = document.getElementById("dyear" + ctrl).value;
            var errormsg = "";


            if (day != "" && month != "" && year != "") {
                var pax_year = parseInt(year);
                var pax_month = parseInt(month);
                var pax_day = parseInt(day);
                var current = new Date('<%=arrivedate%>');
                var current_year = current.getFullYear();
                var current_Month = current.getMonth() + 1;
                var current_Day = current.getDate();
                if (type == "ADT") {
                    var objPromoCode = {};
                    objPromoCode.arivedate = '<%=arrivedate%>';
                    objPromoCode.dob = document.getElementById("dyear" + ctrl).value + "/" + document.getElementById("dmonth" + ctrl).value + "/" + document.getElementById("dday" + ctrl).value;
                    $.ajax({
                        type: "POST",
                        url: "PassengerDetails.aspx/AdultCheck",
                        data: JSON.stringify(objPromoCode),
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "false") {
                                errormsg = "Adult age should be greater than 11 years";
                            }
                        }
                    });
                }
                else if (type == "CHD") {
                    var objPromoCode = {};
                    objPromoCode.arivedate = '<%=arrivedate%>';
                    objPromoCode.dob = document.getElementById("dyear" + ctrl).value + "/" + document.getElementById("dmonth" + ctrl).value + "/" + document.getElementById("dday" + ctrl).value;
                    $.ajax({
                        type: "POST",
                        url: "PassengerDetails.aspx/ChildCheck",
                        data: JSON.stringify(objPromoCode),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (msg) {
                            // alert(msg.d);
                            if (msg.d == "false") {
                                errormsg = "Child age should be between 2 years to 11 years";
                            }
                        }
                    });
                }
                else {
                    var objPromoCode = {};
                    objPromoCode.arivedate = '<%=arrivedate%>';
                    objPromoCode.dob = document.getElementById("dyear" + ctrl).value + "/" + document.getElementById("dmonth" + ctrl).value + "/" + document.getElementById("dday" + ctrl).value;
                    $.ajax({
                        type: "POST",
                        url: "PassengerDetails.aspx/InfantCheck",
                        data: JSON.stringify(objPromoCode),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (msg) {
                            // alert(msg.d);
                            if (msg.d == "false") {
                                errormsg = "Infant age should be less than 2 years";
                            }
                        }
                    });
                }
            }
            else {
                if (ct.id.includes("dday")) {
                    if (day == "") {
                        $("#ddayhint_hint" + ctrl).remove();
                        $("#ddayhint" + ctrl).append('<span id="ddayhint_hint' + ctrl + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Day</span></span>');
                        $("#ddayhint_hint" + ctrl).css("display", "block");
                        $("#dday" + ctrl).css('border', '1px solid');
                        $("#dday" + ctrl).css('border-color', '#f49b9b');
                    }
                    else {
                        $("#dday" + ctrl).css('border', '');
                        $("#dday" + ctrl).css('border-color', '');
                        $("#ddayhint_hint" + ctrl).remove();
                    }
                }
                else if (ct.id.includes("dmonth")) {
                    if (month == "") {
                        $("#dmonthhint_hint" + ctrl).remove();
                        $("#dmonthhint" + ctrl).append('<span id="dmonthhint_hint' + ctrl + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Month</span></span>');
                        $("#dmonthhint_hint" + ctrl).css("display", "block");
                        $("#dmonth" + ctrl).css('border', '1px solid');
                        $("#dmonth" + ctrl).css('border-color', '#f49b9b');
                    }
                    else {
                        $("#dmonth" + ctrl).css('border', '');
                        $("#dmonth" + ctrl).css('border-color', '');
                        $("#dmonthhint_hint" + ctrl).remove();
                    }
                }
                else if (ct.id.includes("dyear")) {
                    if (year == "") {
                        $("#dyearhint_hint" + ctrl).remove();
                        $("#dyearhint" + ctrl).append('<span id="dyearhint_hint' + ctrl + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Year</span></span>');
                        $("#dyearhint_hint" + ctrl).css("display", "block");
                        $("#dyear" + ctrl).css('border', '1px solid');
                        $("#dyear" + ctrl).css('border-color', '#f49b9b');
                    }
                    else {
                        $("#dyear" + ctrl).css('border', '');
                        $("#dyear" + ctrl).css('border-color', '');
                        $("#dyearhint_hint" + ctrl).remove();
                    }
                }
                return false;
            }
            if (errormsg != "") {
                if (ct.id.includes("dday")) {

                    $("#ddayhint_hint" + ctrl).remove();
                    $("#ddayhint" + ctrl).append('<span id="ddayhint_hint' + ctrl + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">' + errormsg + '</span></span>');
                    $("#ddayhint_hint" + ctrl).css("display", "block");
                    $("#dday" + ctrl).css('border', '1px solid');
                    $("#dday" + ctrl).css('border-color', '#f49b9b');

                    $("#dyear" + ctrl).css('border', '');
                    $("#dyear" + ctrl).css('border-color', '');
                    $("#dyearhint_hint" + ctrl).remove();

                    $("#dmonth" + ctrl).css('border', '');
                    $("#dmonth" + ctrl).css('border-color', '');
                    $("#dmonthhint_hint" + ctrl).remove();
                }
                else if (ct.id.includes("dmonth")) {

                    $("#dmonthhint_hint" + ctrl).remove();
                    $("#dmonthhint" + ctrl).append('<span id="dmonthhint_hint' + ctrl + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">' + errormsg + '</span></span>');
                    $("#dmonthhint_hint" + ctrl).css("display", "block");
                    $("#dmonth" + ctrl).css('border', '1px solid');
                    $("#dmonth" + ctrl).css('border-color', '#f49b9b');

                    $("#dyear" + ctrl).css('border', '');
                    $("#dyear" + ctrl).css('border-color', '');
                    $("#dyearhint_hint" + ctrl).remove();

                    $("#dday" + ctrl).css('border', '');
                    $("#dday" + ctrl).css('border-color', '');
                    $("#ddayhint_hint" + ctrl).remove();
                }
                else if (ct.id.includes("dyear")) {

                    $("#dyearhint_hint" + ctrl).remove();
                    $("#dyearhint" + ctrl).append('<span id="dyearhint_hint' + ctrl + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">' + errormsg + '</span></span>');
                    $("#dyearhint_hint" + ctrl).css("display", "block");
                    $("#dyear" + ctrl).css('border', '1px solid');
                    $("#dyear" + ctrl).css('border-color', '#f49b9b');

                    $("#dday" + ctrl).css('border', '');
                    $("#dday" + ctrl).css('border-color', '');
                    $("#ddayhint_hint" + ctrl).remove();

                    $("#dmonth" + ctrl).css('border', '');
                    $("#dmonth" + ctrl).css('border-color', '');
                    $("#dmonthhint_hint" + ctrl).remove();
                }
                return false;
            }
            else {
                $("#dyear" + ctrl).css('border', '');
                $("#dyear" + ctrl).css('border-color', '');
                $("#dyearhint_hint" + ctrl).remove();

                $("#dday" + ctrl).css('border', '');
                $("#dday" + ctrl).css('border-color', '');
                $("#ddayhint_hint" + ctrl).remove();

                $("#dmonth" + ctrl).css('border', '');
                $("#dmonth" + ctrl).css('border-color', '');
                $("#dmonthhint_hint" + ctrl).remove();

                return true;
            }
        }

        var theForm;

        function cardvalidation(ctrl) {

            var cardtype = "";
            var card_nummber = ctrl.value.replace(/\s/g, '');/*document.getElementById(ctrl + '').value*/;
            var card_visa = /^(?:4[0-9]{12}(?:[0-9]{3})?)$/;
            var card_master = /^(?:5[1-5][0-9]{14}(?:[0-9]{3})?)$/;
            var card_amex = /^3[47][0-9]{13}$/;
            if (card_nummber.match(card_visa)) {

                cardtype = "VSA";
                $("#cardtype").val(cardtype);
                return validate_luhn_card(card_nummber);
            }
            else if (card_nummber.match(card_master)) {

                cardtype = "MSC";
                $("#cardtype").val(cardtype);
                return validate_luhn_card(card_nummber);
            }
            else if (card_nummber.match(card_amex)) {
                cardtype = "AMEX";
                $("#cardtype").val(cardtype);
                return validate_luhn_card(card_nummber);
            }
            else {
                return false;
            }
            return false;
        }

        function validate_luhn_card(card_nummber) {

            var card_array = card_nummber.toString().split('')

            var sum = 0;

            var position = 1;
            for (var i = card_array.length - 2; i >= 0; i--) {
                if (position % 2 != 0) {
                    var temp = parseInt(card_array[i]) * 2;

                    if (temp > 9)
                        temp -= 9;
                    sum += parseInt(temp);
                }
                else {
                    sum += parseInt(card_array[i]);
                }
                position++;
            }




            var last_digit = (sum * 9) % 10;


            if (parseInt(card_array[card_array.length - 1]) == last_digit) {
                return true;
            }


            return false;

        }
        function RemoveError() {
            var flagAcceptsLive = $('#chkAcceptsLive').is(':checked');
            if (!flagAcceptsLive) {
                $("#chkAcceptsLiveError").show();
            }
            else {
                $("#chkAcceptsLiveError").hide();
            }
        }
        function ContinuePayment() {
            var flagAcceptsLive = $('#chkAcceptsLive').is(':checked');
            if (!flagAcceptsLive) {
                flagAcceptsLive = false;
                $("#chkAcceptsLiveError").show();
            }
            else {
                $("#chkAcceptsLiveError").hide();
                flagAcceptsLive = true;
                theForm = document.forms['Form1'];
                if (!theForm) {
                    theForm = document.formServer;
                }
                __doPostBack("AcceptsLiveFare", "AcceptsLiveFare");
            }
            return flagAcceptsLive;
            ////TermsErrors += "</ul>";
            //$("#divBookingDesc").show();
            //$("#divBookingError").hide();
            //$("#divBookingId").show();
            //$("#divPassengerInfo").show();
            //$("#divItineraryInfo").show();
            //$("#divBaggageInfo").show();
            //$("#divPaymentDetails1").show();
            //$("#divPaymentDetails2").show();
        }
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }

        function btn_Submit() {
            var re = /^[A-Za-z ]+$/;
            var ree = /^(0|[0-9]\d*)$/;
            var reee = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var totre = /^([A-Za-z ]){1,30}/;
            var ree1 = /^(0|[0-9]\d*)$/;
            var flagPass = true;
            var totpax = $("#_paxcount").val();
            var totpad = $("#_adtcount").val();
            var totpch = $("#_chdcount").val();
            var totpin = $("#_infcount").val();
            theForm = document.forms['form1'];
            var flagtitle = true;
            var flagfirstName = true;
            var flagmiddleName = true;
            var flaglastName = true;
            var flagmiddleName = true;
            var flagday = true;
            var flagmonth = true;
            var flagyear = true;

            var PaymentErrors = "<ul>";
            var paymentcount = 1;
            var BillingErrors = "<ul>";
            var Billingcount = 1;
            var TermsErrors = "<ul>";
            var Termscount = 1;
            if (page == "paynow") {
                var flagnewsChannel = $('#newsChannel').is(':checked');
                if (flagnewsChannel) {
                    var flagnewsemail = true;

                    if (document.getElementById("Email").value == "") {
                        flagnewsemail = false;
                    }
                    var e = document.forms["form1"]["Email"]
                    if (!ValidateEmail(e)) {
                        flagnewsemail = false;
                    }

                    if (flagnewsemail) {
                        var ur = "";
                        var lov = window.location.href.split('#')[0];
                        if (lov.split('?')[1].split('&')[1].split('=')[1] != null)
                            ur = "email=" + document.getElementById("Email").value + "&searchid=" + lov.split('?')[1].split('&')[1].split('=')[1];
                        else
                            ur = "email=" + document.getElementById("Email").value;

                        var request = $.ajax({
                            method: "POST",
                            url: "/newsletter.aspx?" + ur,
                            data: {}
                        });
                    }
                }
                else {
                }
                var flagTerms = $('#chkterms').is(':checked');
                if (!flagTerms) {
                    flagTerms = false;
                    TermsErrors += "<li> " + Termscount + ". Please accept our <strong>Terms and Conditions</strong></li>";
                    Termscount++;
                    $("#chkTerms").css("border", "1px solid #e71717");
                    $('#chkTerms').focus();
                }
                else {
                    $("#TermsErr").text("");
                    $("#chkTerms").css("border", "1px solid #b3bbbf");
                }

                var flagTravelDoc = $('#chkTravelDoc').is(':checked');
                if (!flagTravelDoc) {
                    flagTravelDoc = false;
                    TermsErrors += "<li> " + Termscount + ". Please accept our <strong>Travel Documents Conditions</strong></li>";
                    $("#chkTravelDoc").css("border", "1px solid #e71717");
                    $('#chkTravelDoc').focus();
                }
                else {
                    $("#TermsErr").text("");
                    $("#chkTravelDoc").css("border", "1px solid #b3bbbf");
                }
                TermsErrors += "</ul>";
            }
// paylater terms 

            if (page == "paylater") {
                var flagnewsChannel1 = $('#newsChannel1').is(':checked');
                if (flagnewsChannel1) {
                    var flagnewsemail1 = true;

                    if (document.getElementById("Email2").value == "") {
                        flagnewsemail1 = false;
                    }
                    var e = document.forms["form1"]["Email2"]
                    if (!ValidateEmail(e)) {
                        flagnewsemail1 = false;
                    }

                    if (flagnewsemail1) {
                        var ur = "";
                        var lov = window.location.href.split('#')[0];
                        if (lov.split('?')[1].split('&')[1].split('=')[1] != null)
                            ur = "email=" + document.getElementById("Email2").value + "&searchid=" + lov.split('?')[1].split('&')[1].split('=')[1];
                        else
                            ur = "email=" + document.getElementById("Email2").value;

                        var request = $.ajax({
                            method: "POST",
                            url: "/newsletter.aspx?" + ur,
                            data: {}
                        });
                    }
                }
                else {
                }
                var flagTerms1 = $('#chkterms').is(':checked');
                if (!flagTerms1) {
                    flagTerms1 = false;
                    TermsErrors += "<li> " + Termscount + ". Please accept our <strong>Terms and Conditions</strong></li>";
                    Termscount++;
                    $("#chkTerms").css("border", "1px solid #e71717");
                    $('#chkTerms').focus();
                }
                else {
                    $("#TermsErr").text("");
                    $("#chkTerms").css("border", "1px solid #b3bbbf");
                }

                var flagTravelDoc = $('#chkTravelDoc').is(':checked');
                if (!flagTravelDoc) {
                    flagTravelDoc = false;
                    TermsErrors += "<li> " + Termscount + ". Please accept our <strong>Travel Documents Conditions</strong></li>";
                    $("#chkTravelDoc").css("border", "1px solid #e71717");
                    $('#chkTravelDoc').focus();
                }
                else {
                    $("#TermsErr").text("");
                    $("#chkTravelDoc").css("border", "1px solid #b3bbbf");
                }
                TermsErrors += "</ul>";
            }


            if (page == "paynow") {
                var cardEmail = document.forms["form1"]["Email"];
                var flagCNEmail = ValidateEmail(cardEmail); //required(cardEmail);

                if (flagCNEmail)
                    flagCNEmail = reee.test(cardEmail.value);
                if (!flagCNEmail) {
                    $('#Email').css('border', '1px solid');
                    $('#Email').css('border-color', '#f49b9b');
                    BillingErrors += "<li> " + Billingcount + ". please enter valid <strong>Email Id</strong></li>";
                    Billingcount++;
                    $('#Email').focus();
                    $("#Emailhint_hint").remove();
                    $("#Emailhint").append('<span id="Emailhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Email ID</span></span>');
                    $("#Emailhint_hint").css("display", "block");
                }
                else {
                    $('#Email').css('border', '');
                    $('#Email').css('border-color', '');
                    $("#Emailhint_hint").remove();
                }
            }

// paylater email 
            if (page == "paylater") {
                var cardEmail1 = document.forms["form1"]["Email2"];
                var flagCNEmail1 = ValidateEmail(cardEmail1); //required(cardEmail);

                if (flagCNEmail1)
                    flagCNEmail1 = reee.test(cardEmail1.value);
                if (!flagCNEmail1) {
                    $('#Email2').css('border', '1px solid');
                    $('#Email2').css('border-color', '#f49b9b');
                    BillingErrors += "<li> " + Billingcount + ". please enter valid <strong>Email Id</strong></li>";
                    Billingcount++;
                    $('#Email2').focus();
                    $("#Emailhint_hint1").remove();
                    $("#Emailhint1").append('<span id="Emailhint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Email ID</span></span>');
                    $("#Emailhint_hint1").css("display", "block");
                }
                else {
                    $('#Email2').css('border', '');
                    $('#Email2').css('border-color', '');
                    $("#Emailhint_hint1").remove();
                }

            }


            if (page == "paynow") {
                var flagCNPh = false;
                var Mobile = document.forms["form1"]["Mobile"];
                if (Mobile.value != "") {
                    if (Mobile.value.length <= 10 || !ree1.test(Mobile.value)) {
                        if (ree1.test(Mobile.value)) {
                            if (Mobile.value.length < 10) {
                                $('#Mobile').css('border', '1px solid');
                                $('#Mobile').css('border-color', '#f49b9b');
                                $("#Mobilehint_hint").remove();
                                $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                                $("#Mobilehint_hint").css("display", "block");
                                $('#Mobile').focus();
                                flagCNPh = false;
                            }
                            else {
                                $('#Mobile').css('border', '');
                                $('#Mobile').css('border-color', '');
                                $("#Mobilehint_hint").remove();
                                flagCNPh = true;
                            }
                        }
                        else {
                            $('#Mobile').css('border', '1px solid');
                            $('#Mobile').css('border-color', '#f49b9b');
                            $("#Mobilehint_hint").remove();
                            $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Only Numeric Values</span></span>');
                            $("#Mobilehint_hint").css("display", "block");
                            $('#Mobile').focus();
                            flagCNPh = false;
                        }
                    }
                    else {
                        $('#Mobile').css('border', '1px solid');
                        $('#Mobile').css('border-color', '#f49b9b');
                        $("#Mobilehint_hint").remove();
                        $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                        $("#Mobilehint_hint").css("display", "block");
                        $('#Mobile').focus();
                        flagCNPh = false;
                    }
                }
                else {
                    $('#Mobile').css('border', '1px solid');
                    $('#Mobile').css('border-color', '#f49b9b');
                    $("#Mobilehint_hint").remove();
                    $("#Mobilehint").append('<span id="Mobilehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                    $("#Mobilehint_hint").css("display", "block");
                    $('#Mobile').focus();
                    flagCNPh = false;
                }
            }

// paylater mobile number 

            if (page == "paylater") {
                var flagCNPh1 = false;
                var Mobile1 = document.forms["form1"]["Mobile1"];
                if (Mobile1.value != "") {
                    if (Mobile1.value.length <= 10 || !ree1.test(Mobile1.value)) {
                        if (ree1.test(Mobile1.value)) {
                            if (Mobile1.value.length < 10) {
                                $('#Mobile1').css('border', '1px solid');
                                $('#Mobile1').css('border-color', '#f49b9b');
                                $("#Mobilehint_hint1").remove();
                                $("#Mobilehint1").append('<span id="Mobilehint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                                $("#Mobilehint_hint1").css("display", "block");
                                $('#Mobile1').focus();
                                flagCNPh1 = false;
                            }
                            else {
                                $('#Mobile1').css('border', '');
                                $('#Mobile1').css('border-color', '');
                                $("#Mobilehint_hint1").remove();
                                flagCNPh1 = true;
                            }
                        }
                        else {
                            $('#Mobile1').css('border', '1px solid');
                            $('#Mobile1').css('border-color', '#f49b9b');
                            $("#Mobilehint_hint1").remove();
                            $("#Mobilehint1").append('<span id="Mobilehint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Only Numeric Values</span></span>');
                            $("#Mobilehint_hint1").css("display", "block");
                            $('#Mobile1').focus();
                            flagCNPh1 = false;
                        }
                    }
                    else {
                        $('#Mobile1').css('border', '1px solid');
                        $('#Mobile1').css('border-color', '#f49b9b');
                        $("#Mobilehint_hint1").remove();
                        $("#Mobilehint1").append('<span id="Mobilehint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                        $("#Mobilehint_hint1").css("display", "block");
                        $('#Mobile1').focus();
                        flagCNPh1 = false;
                    }
                }
                else {
                    $('#Mobile1').css('border', '1px solid');
                    $('#Mobile1').css('border-color', '#f49b9b');
                    $("#Mobilehint_hint1").remove();
                    $("#Mobilehint1").append('<span id="Mobilehint_hint1" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter 10 Digit Mobile Number</span></span>');
                    $("#Mobilehint_hint1").css("display", "block");
                    $('#Mobile1').focus();
                    flagCNPh1 = false;
                }

            }

           

            var flagCountry = validate_dropDown("country"); //validateCountryId(CountryIdCard);
            if (!flagCountry) {
                flagCountry = false;
                $('#country').css('border', '1px solid');
                $('#country').css('border-color', '#f49b9b');
                BillingErrors += "<li> " + Termscount + ". Please Select <strong>Billing Country</strong></li>";
                $('#country').focus();
                $("#Countryhint_hint").remove();
                $("#Countryhint").append('<span id="Countryhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Billing Country</span></span>');
                $("#Countryhint_hint").css("display", "block");
            }
            else {
                $('#country').css('border', '');
                $('#country').css('border-color', '');
                $("#Countryhint_hint").remove();
            }

            var cardZipCode = document.forms["form1"]["ZipCode"];
            var flagCNZip = required(cardZipCode);
            if (!flagCNZip) {
                $('#ZipCode').css('border', '1px solid');
                $('#ZipCode').css('border-color', '#f49b9b');
                BillingErrors += "<li> " + Billingcount + ". please enter valid <strong>PostalCode</strong></li>";
                Billingcount++;
                $('#ZipCode').focus();
                $("#Postcodehint_hint").remove();
                $("#Postcodehint").append('<span id="Postcodehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Postal Code</span></span>');
                $("#Postcodehint_hint").css("display", "block");
            }
            else {
                $('#ZipCode').css('border', '');
                $('#ZipCode').css('border-color', '');
                $("#Postcodehint_hint").remove();
            }

            var cardState = document.forms["form1"]["State"];
            var flagState = required(cardState);

            if (flagState)
                flagState = re.test(cardState.value);

            if (!flagState) {
                $('#State').css('border', '1px solid');
                $('#State').css('border-color', '#f49b9b');
                BillingErrors += "<li> " + Billingcount + ". please enter valid <strong>State</strong></li>";
                Billingcount++;
                $('#State').focus();
                $("#Statehint_hint").remove();
                $("#Statehint").append('<span id="Statehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter State</span></span>');
                $("#Statehint_hint").css("display", "block");
            }
            else {
                $('#State').css('border', '');
                $('#State').css('border-color', '');
                $("#Statehint_hint").remove();
            }


            var cardCity = document.forms["form1"]["City"];
            var flagCNCity = required(cardCity);
            if (flagCAdr)
                flagCAdr = re.test(cardCity.value);
            if (!flagCNCity) {
                $('#City').css('border', '1px solid');
                $('#City').css('border-color', '#f49b9b');
                BillingErrors += "<li> " + Billingcount + ". please enter valid <strong>City</strong></li>";
                Billingcount++;
                $('#City').focus();
                $("#Cityhint_hint").remove();
                $("#Cityhint").append('<span id="Cityhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter City</span></span>');
                $("#Cityhint_hint").css("display", "block");
            }
            else {
                $('#City').css('border', '');
                $('#City').css('border-color', '');
                $("#Cityhint_hint").remove();
            }

            var addre = /^[a-zA-Z0-9~_,./: -#-]*$/;
            var cardAddr = document.forms["form1"]["Address"];
            var flagCAdr = required(cardAddr);
            if (flagCAdr)
                flagCAdr = addre.test(cardAddr.value);
            if (!flagCAdr) {
                $('#Address').css('border', '1px solid');
                $('#Address').css('border-color', '#f49b9b');
                BillingErrors += "<li> " + Billingcount + ". please enter valid <strong>address</strong></li>";
                Billingcount++;
                $('#Address').focus();
                $("#Addresshint_hint").remove();
                $("#Addresshint").append('<span id="Addresshint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Address</span></span>');
                $("#Addresshint_hint").css("display", "block");
            }
            else {
                $('#Address').css('border', '');
                $('#Address').css('border-color', '');
                $("#Addresshint_hint").remove();
            }
            //cardNamehint cardNumberhint ExpiryMonthhint ExpiryYearhint cvvhint

            var cardCvv = document.forms["form1"]["cvc"];
            var flagCvv = allnumericPhone(cardCvv, 4, 3);
            if (!flagCvv) {
                $('#cvc').css('border', '1px solid');
                $('#cvc').css('border-color', '#f49b9b');
                PaymentErrors += "<li> " + paymentcount + ". please enter only numerical numbers for <strong>CVV</strong> ex 0,1,2...9</li>";
                paymentcount++;
                $('#cvc').focus();
                $("#cvvhint_hint").remove();
                $("#cvvhint").append('<span id="cvvhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Card CVV</span></span>');
                $("#cvvhint_hint").css("display", "block");
            }
            else {
                $('#cvc').css('border', '');
                $('#cvc').css('border-color', '');
                $("#cvvhint_hint").remove();
            }
            BillingErrors += "</ul>";


            var flagCMon = validate_dropDown("ExpMonth"); //validateMonth(monthCard);
            if (!flagCMon) {
                $('#ExpMonth').css('border', '1px solid');
                $('#ExpMonth').css('border-color', '#f49b9b');
                PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Month</strong></li>";
                paymentcount++;
                $('#ExpMonth').focus();
                $("#ExpiryMonthhint_hint").remove();
                $("#ExpiryMonthhint").append('<span id="ExpiryMonthhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Card Expiry Month</span></span>');
                $("#ExpiryMonthhint_hint").css("display", "block");
            }
            else {
                $('#ExpMonth').css('border', '');
                $('#ExpMonth').css('border-color', '');
                $("#ExpiryMonthhint_hint").remove();
            }

            var flagCYear = validate_dropDown("ExpYear"); //validateYear(yearCard);
            if (!flagCYear) {
                $('#ExpYear').css('border', '1px solid');
                $('#ExpYear').css('border-color', '#f49b9b');
                PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Year</strong></li>";
                paymentcount++;
                $('#ExpMonth').focus();
                $("#ExpiryYearhint_hint").remove();
                $("#ExpiryYearhint").append('<span id="ExpiryYearhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Card Expiry Year</span></span>');
                $("#ExpiryYearhint_hint").css("display", "block");
            }
            else {
                $('#ExpYear').css('border', '');
                $('#ExpYear').css('border-color', '');
                $("#ExpiryYearhint_hint").remove();
            }

            var mn = $('#ExpMonth').val();
            var yr = $('#ExpYear').val();

            var current = new Date();
            var current_year = current.getFullYear();
            var current_Month = current.getMonth() + 1;
            var current_Day = current.getDate();

            if (current_year == parseInt(yr)) {
                if (parseInt(mn) < current_Month) {
                    $('#ExpMonth').css('border', '1px solid');
                    $('#ExpMonth').css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please Select the <strong>Expiry Month</strong></li>";
                    paymentcount++;
                    $('#ExpMonth').focus();
                    $("#ExpiryMonthhint_hint").remove();
                    $("#ExpiryMonthhint").append('<span id="ExpiryMonthhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Expiry Month Not Valid</span></span>');
                    $("#ExpiryMonthhint_hint").css("display", "block");
                }
                else {
                    $('#ExpMonth').css('border', '');
                    $('#ExpMonth').css('border-color', '');
                    $("#ExpiryMonthhint_hint").remove();
                }
            }
            PaymentErrors += "</ul>"


            var cardId = document.forms["form1"]["cardNumber"];
            var flagCNO = allnumeric(cardId, 16, 12);
            if (!flagCNO) {
                $('#' + cardId.id).css('border', '1px solid');
                $('#' + cardId.id).css('border-color', '#f49b9b');
                PaymentErrors += "<li> " + paymentcount + ". please enter only numerical numbers for <strong>card Number</strong> ex 0,1,2...9</li>";
                paymentcount++;
                $('#' + cardId.id).focus();
                $("#cardNumberhint_hint").remove();
                $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Number is not Valid</span></span>');
                $("#cardNumberhint_hint").css("display", "block");
            }
            else {
                if (!cardvalidation(cardId)) {
                    $('#' + cardId.id).css('border', '1px solid');
                    $('#' + cardId.id).css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please enter only numerical numbers for <strong>card Number</strong> ex 0,1,2...9</li>";
                    paymentcount++;
                    flagCNO = false;
                    $('#' + cardId.id).focus();
                    $("#cardNumberhint_hint").remove();
                    $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Number is not Valid</span></span>');
                    $("#cardNumberhint_hint").css("display", "block");
                }
                else {
                    $('#' + cardId.id).css('border', '');
                    $('#' + cardId.id).css('border-color', '');
                    $("#cardNumberhint_hint").remove();
                }
            }




            var d = "";
            if (top.globCType == 'visa')
                d = "VSA";
            else if (top.globCType == 'mastercard')
                d = "MSC";
            else if (top.globCType == 'amex')
                d = "AMEX";
            else
                d = "";
            var flagCType = validate_dropDown("cardtype"); //ValidateCardType(cardType);
            $("#cardtype").val(d);
            if (flagCNO) {
                if (top.globCType == "unknown" || top.globCType == undefined) {
                    $('#' + cardId.id).css('border', '1px solid');
                    $('#' + cardId.id).css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". Invalid <strong>Card Type</strong></li>";
                    paymentcount++;
                    $('#' + cardId.id).focus();
                    $("#cardNumberhint_hint").remove();
                    $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Type is not Valid</span></span>');
                    $("#cardNumberhint_hint").css("display", "block");
                }
                else if (d == "") {
                    $('#' + cardId.id).css('border', '1px solid');
                    $('#' + cardId.id).css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". Invalid <strong>Card Type</strong></li>";
                    paymentcount++;
                    $('#' + cardId.id).focus();
                    $("#cardNumberhint_hint").remove();
                    $("#cardNumberhint").append("<span id='cardNumberhint_hint' class='tooltip-content'>Card Type is not Valid</span>");
                    $("#cardNumberhint").append('<span id="cardNumberhint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Type is not Valid</span></span>');
                    $("#cardNumberhint_hint").css("display", "block");
                }
                else {
                    $('#' + cardId.id).css('border', '');
                    $('#' + cardId.id).css('border-color', '');
                    $("#cardNumberhint_hint").remove();
                }
            }

            var cardName = document.forms["form1"]["CardHolderName"];
            var flagCNa = required(cardName);
            if (!flagCNa) {
                $('#' + cardName.id).css('border', '1px solid');
                $('#' + cardName.id).css('border-color', '#f49b9b');
                PaymentErrors += "<li> " + paymentcount + ". please enter valid <strong>Card Holder Name</strong></li>";
                paymentcount++;
                $('#' + cardName.id).focus();
                $("#cardNamehint_hint").remove();
                $("#cardNamehint").append('<span id="cardNamehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Card Holder Name</span></span>');
                $("#cardNamehint_hint").css("display", "block");
            }
            else {
                if (re.test(cardName.value)/*.match('^[a-zA-Z ]')*/) {
                    $('#' + cardName.id).css('border', '');
                    $('#' + cardName.id).css('border-color', '');
                    $("#cardNamehint_hint").remove();
                }
                else {
                    $('#' + cardName.id).css('border', '1px solid');
                    $('#' + cardName.id).css('border-color', '#f49b9b');
                    PaymentErrors += "<li> " + paymentcount + ". please enter valid <strong>Card Holder Name</strong></li>";
                    paymentcount++;
                    flagCNa = false;
                    $('#' + cardName.id).focus();
                    $("#cardNamehint_hint").remove();
                    $("#cardNamehint").append('<span id="cardNamehint_hint" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Card Holder Name is Not Valid</span></span>');
                    $("#cardNamehint_hint").css("display", "block");
                }
            }


            var PaxErrors = "<ul>";

            var flagadult = false;
            var flagchild = false;
            var flaginfant = false;
            //ADT CHD
            //totpad totpch totpin 
            if (parseInt(totpad) > 0) {
                for (var i = totpad; i >= 1; i--) {
                    var dyyear = document.forms["form1"]["dyear" + i];
                    flagyear = validate_dropDown("dyear" + i);
                    if (!flagyear) {
                        PaxErrors += "<li> " + i + ". Please Select Year</li>";
                        //dyearhint
                        $('#' + dyyear.id).css('border', '1px solid');
                        $('#' + dyyear.id).css('border-color', '#f49b9b');
                        $('#' + dyyear.id).focus();
                        $("#dyearhint_hint" + i).remove();
                        $("#dyearhint" + i).append('<span id="dyearhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Year</span></span>');
                        $("#dyearhint_hint" + i).css("display", "block");
                    }
                    else {
                        $('#' + dyyear.id).css('border', '');
                        $('#' + dyyear.id).css('border-color', '');
                        $("#dyearhint_hint" + i).remove();
                    }

                    var dmmonth = document.forms["form1"]["dmonth" + i];
                    flagmonth = validate_dropDown("dmonth" + i);
                    if (!flagmonth) {
                        PaxErrors += "<li> " + i + ". Please Select Month</li>";
                        //dmonthhint
                        $("#dmonthhint_hint" + i).remove();
                        $("#dmonthhint" + i).append('<span id="dmonthhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Month</span></span>');
                        $("#dmonthhint_hint" + i).css("display", "block");
                        $('#' + dmmonth.id).css('border', '1px solid');
                        $('#' + dmmonth.id).css('border-color', '#f49b9b');
                        $('#' + dmmonth.id).focus();
                    }
                    else {
                        $('#' + dmmonth.id).css('border', '');
                        $('#' + dmmonth.id).css('border-color', '');
                        $("#dmonthhint_hint" + i).remove();
                    }

                    var ddday = document.forms["form1"]["dday" + i];
                    flagday = validate_dropDown("dday" + i);
                    if (!flagday) {
                        PaxErrors += "<li> " + i + ". Please Select Day</li>";
                        $("#ddayhint_hint" + i).remove();
                        $("#ddayhint" + i).append('<span id="ddayhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Day</span></span>');
                        $("#ddayhint_hint" + i).css("display", "block");
                        //ddayhint
                        $('#' + ddday.id).css('border', '1px solid');
                        $('#' + ddday.id).css('border-color', '#f49b9b');
                        $('#' + ddday.id).focus();
                    }
                    else {
                        $('#' + ddday.id).css('border', '');
                        $('#' + ddday.id).css('border-color', '');
                        $("#ddayhint_hint" + i).remove();
                    }
                    if (flagday && flagmonth && flagyear) {
                        flagadult = validate(i, "ADT", "1");
                        if (!flagadult)
                            $('#dday' + i).focus();
                    }
                }
            }
            else {
                flagadult = true;
            }
            if (parseInt(totpch) > 0) {
                for (var i = (parseInt(totpch) + parseInt(totpad)); i >= (parseInt(1) + parseInt(totpad)); i--) {
                    var dyyear = document.forms["form1"]["dyear" + i];
                    flagyear = validate_dropDown("dyear" + i);
                    if (!flagyear) {
                        PaxErrors += "<li> " + i + ". Please Select Year</li>";
                        //dyearhint
                        $('#' + dyyear.id).css('border', '1px solid');
                        $('#' + dyyear.id).css('border-color', '#f49b9b');
                        $('#' + dyyear.id).focus();
                        $("#dyearhint_hint" + i).remove();
                        $("#dyearhint" + i).append('<span id="dyearhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Year</span></span>');
                        $("#dyearhint_hint" + i).css("display", "block");
                    }
                    else {
                        $('#' + dyyear.id).css('border', '');
                        $('#' + dyyear.id).css('border-color', '');
                        $("#dyearhint_hint" + i).remove();
                    }

                    var dmmonth = document.forms["form1"]["dmonth" + i];
                    flagmonth = validate_dropDown("dmonth" + i);
                    if (!flagmonth) {
                        PaxErrors += "<li> " + i + ". Please Select Month</li>";
                        //dmonthhint
                        $("#dmonthhint_hint" + i).remove();
                        $("#dmonthhint" + i).append('<span id="dmonthhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Month</span></span>');
                        $("#dmonthhint_hint" + i).css("display", "block");
                        $('#' + dmmonth.id).css('border', '1px solid');
                        $('#' + dmmonth.id).css('border-color', '#f49b9b');
                        $('#' + dmmonth.id).focus();
                    }
                    else {
                        $('#' + dmmonth.id).css('border', '');
                        $('#' + dmmonth.id).css('border-color', '');
                        $("#dmonthhint_hint" + i).remove();
                    }

                    var ddday = document.forms["form1"]["dday" + i];
                    flagday = validate_dropDown("dday" + i);
                    if (!flagday) {
                        PaxErrors += "<li> " + i + ". Please Select Day</li>";
                        $("#ddayhint_hint" + i).remove();
                        $("#ddayhint" + i).append('<span id="ddayhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Day</span></span>');
                        $("#ddayhint_hint" + i).css("display", "block");
                        //ddayhint
                        $('#' + ddday.id).css('border', '1px solid');
                        $('#' + ddday.id).css('border-color', '#f49b9b');
                        $('#' + ddday.id).focus();
                    }
                    else {
                        $('#' + ddday.id).css('border', '');
                        $('#' + ddday.id).css('border-color', '');
                        $("#ddayhint_hint" + i).remove();
                    }
                    if (flagday && flagmonth && flagyear) {
                        flagchild = validate(i, "CHD", "1");
                        if (!flagchild)
                            $('#dday' + i).focus();
                    }
                }
            }
            else {
                flagchild = true;
            }
            if (parseInt(totpin) > 0) {
                for (var i = (parseInt(totpch) + parseInt(totpad) + parseInt(totpin)); i >= (parseInt(totpch) + parseInt(totpad) + parseInt(1)); i--) {
                    var dyyear = document.forms["form1"]["dyear" + i];
                    flagyear = validate_dropDown("dyear" + i);
                    if (!flagyear) {
                        PaxErrors += "<li> " + i + ". Please Select Year</li>";
                        //dyearhint
                        $('#' + dyyear.id).css('border', '1px solid');
                        $('#' + dyyear.id).css('border-color', '#f49b9b');
                        $('#' + dyyear.id).focus();
                        $("#dyearhint_hint" + i).remove();
                        $("#dyearhint" + i).append('<span id="dyearhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Year</span></span>');
                        $("#dyearhint_hint" + i).css("display", "block");
                    }
                    else {
                        $('#' + dyyear.id).css('border', '');
                        $('#' + dyyear.id).css('border-color', '');
                        $("#dyearhint_hint" + i).remove();
                    }

                    var dmmonth = document.forms["form1"]["dmonth" + i];
                    flagmonth = validate_dropDown("dmonth" + i);
                    if (!flagmonth) {
                        PaxErrors += "<li> " + i + ". Please Select Month</li>";
                        //dmonthhint
                        $("#dmonthhint_hint" + i).remove();
                        $("#dmonthhint" + i).append('<span id="dmonthhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Month</span></span>');
                        $("#dmonthhint_hint" + i).css("display", "block");
                        $('#' + dmmonth.id).css('border', '1px solid');
                        $('#' + dmmonth.id).css('border-color', '#f49b9b');
                        $('#' + dmmonth.id).focus();
                    }
                    else {
                        $('#' + dmmonth.id).css('border', '');
                        $('#' + dmmonth.id).css('border-color', '');
                        $("#dmonthhint_hint" + i).remove();
                    }

                    var ddday = document.forms["form1"]["dday" + i];
                    flagday = validate_dropDown("dday" + i);
                    if (!flagday) {
                        PaxErrors += "<li> " + i + ". Please Select Day</li>";
                        $("#ddayhint_hint" + i).remove();
                        $("#ddayhint" + i).append('<span id="ddayhint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Birth Day</span></span>');
                        $("#ddayhint_hint" + i).css("display", "block");
                        //ddayhint
                        $('#' + ddday.id).css('border', '1px solid');
                        $('#' + ddday.id).css('border-color', '#f49b9b');
                        $('#' + ddday.id).focus();
                    }
                    else {
                        $('#' + ddday.id).css('border', '');
                        $('#' + ddday.id).css('border-color', '');
                        $("#ddayhint_hint" + i).remove();
                    }
                    if (flagday && flagmonth && flagyear) {
                        flaginfant = validate(i, "INF", "1");
                        if (!flaginfant)
                            $('#dday' + i).focus();
                    }
                }
            }
            else {
                flaginfant = true;
            }
            for (var i = totpax; i >= 1; i--) {
                //var dyyear = document.forms["form1"]["dyear" + i];
                //flagyear = validate_dropDown("dyear" + i);
                //if (!flagyear) {
                //    PaxErrors += "<li> " + i + ". Please Select Year</li>";
                //    //dyearhint
                //    $('#' + dyyear.id).css('border', '1px solid');
                //    $('#' + dyyear.id).css('border-color', '#f49b9b');
                //    $('#' + dyyear.id).focus();
                //    $("#dyearhint_hint" + i).remove();
                //    $("#dyearhint" + i).append('<span id="dyearhint_hint' + i + '" class="tool-info"><span class="tool-head-title">Birth Year</span><span class="tool-body-text">Please Select Birth Year</span></span>');
                //    $("#dyearhint_hint" + i).css("display", "block");
                //}
                //else {
                //    $('#' + dyyear.id).css('border', '');
                //    $('#' + dyyear.id).css('border-color', '');
                //    $("#dyearhint_hint" + i).remove();
                //}

                //var dmmonth = document.forms["form1"]["dmonth" + i];
                //flagmonth = validate_dropDown("dmonth" + i);
                //if (!flagmonth) {
                //    PaxErrors += "<li> " + i + ". Please Select Month</li>";
                //    //dmonthhint
                //    $("#dmonthhint_hint" + i).remove();
                //    $("#dmonthhint" + i).append('<span id="dmonthhint_hint' + i + '" class="tool-info"><span class="tool-head-title">Birth Month</span><span class="tool-body-text">Please Select Birth Month</span></span>');
                //    $("#dmonthhint_hint" + i).css("display", "block");
                //    $('#' + dmmonth.id).css('border', '1px solid');
                //    $('#' + dmmonth.id).css('border-color', '#f49b9b');
                //    $('#' + dmmonth.id).focus();
                //}
                //else {
                //    $('#' + dmmonth.id).css('border', '');
                //    $('#' + dmmonth.id).css('border-color', '');
                //    $("#dmonthhint_hint" + i).remove();
                //}

                //var ddday = document.forms["form1"]["dday" + i];
                //flagday = validate_dropDown("dday" + i);
                //if (!flagday) {
                //    PaxErrors += "<li> " + i + ". Please Select Day</li>";
                //    $("#ddayhint_hint" + i).remove();
                //    $("#ddayhint" + i).append('<span id="ddayhint_hint' + i + '" class="tool-info"><span class="tool-head-title">Birth Day</span><span class="tool-body-text">Please Select Birth Day</span></span>');
                //    $("#ddayhint_hint" + i).css("display", "block");
                //    //ddayhint
                //    $('#' + ddday.id).css('border', '1px solid');
                //    $('#' + ddday.id).css('border-color', '#f49b9b');
                //    $('#' + ddday.id).focus();
                //}
                //else {
                //    $('#' + ddday.id).css('border', '');
                //    $('#' + ddday.id).css('border-color', '');
                //    $("#ddayhint_hint" + i).remove();
                //}

                var fmname = document.forms["form1"]["mname" + i];
                flagmiddleName = required(fmname);
                if (flagmiddleName) {
                    if (re.test(fmname.value)/*.match('^[a-zA-Z]{3,16}$')*/) {
                        $('#' + fmname.id).css('border', '');
                        $('#' + fmname.id).css('border-color', '');
                        $("#mnamehint_hint" + i).remove();
                    }
                    else {
                        PaxErrors += "<li> " + i + ". Please Enter Valid Name</li>";
                        $('#' + fmname.id).css('border', '1px solid');
                        $('#' + fmname.id).css('border-color', '#f49b9b');
                        flagmiddleName = false;
                        $("#mnamehint_hint" + i).remove();
                        $('#' + fmname.id).focus();
                        $("#mnamehint" + i).append('<span id="mnamehint_hint' + i + '" class="tool-info"><span class="tool-head-title">Middle Name</span><span class="tool-body-text">Middle Name is Not Valid</span></span>');
                        $("#mnamehint_hint" + i).css("display", "block");
                    }
                }
                else {
                    $('#' + fmname.id).css('border', '');
                    $('#' + fmname.id).css('border-color', '');
                    flagmiddleName = true;
                    $("#mnamehint_hint" + i).remove();
                }
                var flname = document.forms["form1"]["lname" + i];
                flaglastName = required(flname);
                if (!flaglastName) {
                    PaxErrors += "<li> " + i + ". Please Enter Last Name</li>";
                    $('#' + flname.id).css('border', '1px solid');
                    $('#' + flname.id).css('border-color', '#f49b9b');
                    $('#' + flname.id).focus();
                    $("#lnamehint_hint" + i).remove();
                    $("#lnamehint" + i).append('<span id="lnamehint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter Last Name</span></span>');
                    $("#lnamehint_hint" + i).css("display", "block");
                }
                else {
                    if (re.test(flname.value)/*.match('^[a-zA-Z]{3,16}$')*/) {
                        $('#' + flname.id).css('border', '');
                        $('#' + flname.id).css('border-color', '');
                        $("#lnamehint_hint" + i).remove();
                    }
                    else {
                        PaxErrors += "<li> " + i + ". Please Enter Valid Name</li>";
                        $('#' + flname.id).css('border', '1px solid');
                        $('#' + flname.id).css('border-color', '#f49b9b');
                        flaglastName = false;
                        $('#' + flname.id).focus();
                        $("#lnamehint_hint" + i).remove();
                        $("#lnamehint" + i).append('<span id="lnamehint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Last Name is Not Valid</span></span>');
                        $("#lnamehint_hint" + i).css("display", "block");
                    }
                }

                var ffname = document.forms["form1"]["fname" + i];
                flagfirstName = required(ffname);
                if (!flagfirstName) {
                    $("#fnamehint_hint" + i).remove();
                    $("#fnamehint" + i).append('<span id="fnamehint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Enter First Name</span></span>');
                    $("#fnamehint_hint" + i).css("display", "block");
                    PaxErrors += "<li> " + i + ". Please Enter First Name</li>";
                    $('#' + ffname.id).css('border', '1px solid');
                    $('#' + ffname.id).css('border-color', '#f49b9b');
                    $('#' + ffname.id).focus();
                }
                else {
                    if (re.test(ffname.value)/*.match('^[a-zA-Z]{3,16}$')*/) {
                        $('#' + ffname.id).css('border', '');
                        $('#' + ffname.id).css('border-color', '');
                        $("#fnamehint_hint" + i).remove();
                    }
                    else {
                        $('#' + ffname.id).css('border', '1px solid');
                        $('#' + ffname.id).css('border-color', '#f49b9b');
                        PaxErrors += "<li> " + i + ". Please Enter Valid Name</li>";
                        flagfirstName = false;
                        $("#fnamehint_hint" + i).remove();
                        $('#' + ffname.id).focus();
                        $("#fnamehint" + i).append('<span id="fnamehint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">First Name is Not Valid</span></span>');
                        $("#fnamehint_hint" + i).css("display", "block");
                    }
                }
                
                var tite = document.forms["form1"]["ptitle" + i];
                flagtitle = validate_dropDown("ptitle" + i);
                if (!flagtitle) {
                    $("#ptitlehint_hint" + i).remove();
                    $("#ptitlehint" + i).append('<span id="ptitlehint_hint' + i + '" class="tool-info"><span class="tool-head-title"></span><span class="tool-body-text">Please Select Title</span></span>');
                    $("#ptitlehint_hint" + i).css("display", "block");

                    PaxErrors += "<li> " + i + ". Please Select Title</li>";
                    $('#' + tite.id).css('border', '1px solid');
                    $('#' + tite.id).css('border-color', '#f49b9b');
                    $('#' + tite.id).focus();
                }
                else {
                    $('#' + tite.id).css('border', '');
                    $('#' + tite.id).css('border-color', '');
                    $("#ptitlehint_hint" + i).remove();
                }
            }
            PaxErrors += "</ul>"

            if (!theForm) {
                theForm = document.form1;
            }

            if (flagCNO && flagCNa && flagCvv && flagCAdr && flagCNCity && flagCNZip && flagState && flagCNPh && flagCNEmail && flagCType && flagCMon && flagCYear && flagCountry && flagTerms && flagfirstName && flaglastName && flagchild && flaginfant && flagtitle && flagadult && flagday && flagmonth && flagyear)
            {
                flagPass = true;
            }
            else if (page == 'paylater')
              {
                if (flagCNPh1 && flagCNEmail1 && flagTerms1 && flagfirstName && flaglastName && flagchild && flaginfant && flagtitle && flagadult && flagday && flagmonth && flagyear) {
                    flagPass = true;
                }
                else {
                    flagPass = false;
                }
               // flagPass = true;
            }

             else {
                    flagPass = false;
                }


            var Errors = "";

            if (PaymentErrors != "" && PaymentErrors != "<ul></ul>") {
                Errors += "<p><span class='blue'>Payment Related Errors</span></p>" + PaymentErrors;
            }
            if (BillingErrors != "" && BillingErrors != "<ul></ul>") {
                Errors += "<p><span class='blue'>Billing Address Related Errors</span></p>" + BillingErrors;
            }
            if (TermsErrors != "" && TermsErrors != "<ul></ul>") {
                Errors += "<p><span class='blue'>Terms Related Errors</span></p>" + TermsErrors;
            }

            if (Errors != "") {

            }
            else {
            }

            if (TermsErrors != "" && TermsErrors != "<ul></ul>") {
                $("#TermsErrors").html(TermsErrors);
                $("#TermsErrors").show();
                //window.location.hash = '#TermsErrors';
            }
            else {
                $("#TermsErrors").hide();
            }
            if (BillingErrors != "" && BillingErrors != "<ul></ul>") {
                //window.location.hash = '#billingError';
            }
            else {
            }
            if (PaymentErrors != "" && PaymentErrors != "<ul></ul>") {
                //window.location.hash = '#paymentDetails';

            }
            else {
            }
            if (PaxErrors != "" && PaxErrors != "<ul></ul>") {
               // window.location.hash = '#divPassengerDetails';
            }
            else {
            }
            if (flagPass) {
                __doPostBack("booknow", "booknow");
            }

            return flagPass;
        }

        function Cancelpolicy(ctrl) {
            //alert(ctrl.id);
            var totamount = $("#totalAmount1").text();
            //var vamount = 0;
            if ($("#VoucherApply").val() != "") {
                vamount = parseFloat($("#VoucherApply").val());
            }

            var radioValue = $("input[name='radio-group']:checked").val();
            var radioValue1 = $("input[name='radio-group1']:checked").val();

            if (radioValue1 == 'Yes') {
                Iamount = parseFloat(<%=InsuAm%>);
            }
            else {
                Iamount = '0';
            }

            if (radioValue == 'Yes') {
                cancel = 15 * parseFloat(<%=TotalPax%>);
                var objcancel = {};
                objcancel.cancel = 'yes';
                objcancel.am = cancel;
                objcancel.ssid = '<%=ssid%>';
                $.ajax({
                    type: "POST",
                    url: "PassengerDetails.aspx/UpdateCancellation",
                    data: JSON.stringify(objcancel),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert(msg.d);
                    }
                });


		document.getElementById("dPD").setAttribute("style", "display:");

		document.getElementById("dPD1").setAttribute("style", "display:");


            }
            else {
                cancel = 0;
                var objcancel = {};
                objcancel.cancel = 'no';
                objcancel.am = cancel;
                objcancel.ssid = '<%=ssid%>';
                $.ajax({
                    type: "POST",
                    url: "PassengerDetails.aspx/UpdateCancellation",
                    data: JSON.stringify(objcancel),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert(msg.d);
                    }
                });

document.getElementById("dPD").setAttribute("style", "display:none");

		document.getElementById("dPD1").setAttribute("style", "display:none");
            }
            if (ctrl.id.indexOf("Yes") != -1) {
                $("#CancellationFee").empty();
                $("#CancellationFee").append(cancel + ".00 <small>USD</small>");
                $("#CancellationFee1").empty();
                $("#CancellationFee1").append(cancel + ".00 <small>USD</small>");
                //$("#Insurance").empty();
                //$("#Insurance").append(Iamount + " <small>USD</small>");
                //$("#Insurance1").empty();
                //$("#Insurance1").append(Iamount + " <small>USD</small>");
                $("#totalAmount1").empty();
                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + "<small> USD</small> ");
                $("#totalAmount").empty();
                $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD</h4><p>incl. all taxes and fees</p>");
                $("#totalAmount2").empty();
                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> </h3>");
                $("#totalAmount3").empty();
                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount4").empty();
                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount5").empty();
                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));
                $("#ExtendedCancellation").val(cancel);

            }
            else {
                <%--if ('<%=cancel%>' == 'yes') {
                    cancel = -15 * parseFloat(<%=TotalPax%>);
                }
                else {
                    cancel = 0;
                }--%>
                $("#CancellationFee").empty();
                $("#CancellationFee").append(" 0.00 <small>USD</small>");
                $("#CancellationFee1").empty();
                $("#CancellationFee1").append(" 0.00 <small>USD</small>");
                //$("#Insurance").empty();
                //$("#Insurance").append(Iamount + " <small>USD</small>");
                //$("#Insurance1").empty();
                //$("#Insurance1").append(Iamount + " <small>USD</small>");
                $("#totalAmount1").empty();
                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + "<small> USD</small> ");
                $("#totalAmount").empty();
                $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD</h4><p>incl. all taxes and fees</p>");
                $("#totalAmount2").empty();
                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> </h3>");
                $("#totalAmount3").empty();
                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount4").empty();
                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount5").empty();
                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));
                $("#ExtendedCancellation").val(parseFloat("0"));
            }
        }

        function AddInsurance(ctrl) {
            //alert(ctrl.id);
            var totamount = $("#totalAmount1").text();
            //var vamount = 0;
            if ($("#VoucherApply").val() != "") {
                vamount = parseFloat($("#VoucherApply").val());
            }

            var radioValue = $("input[name='radio-group']:checked").val();
            var radioValue1 = $("input[name='radio-group1']:checked").val();

            if (radioValue1 == 'Yes') {
                Iamount = parseFloat(<%=InsuAm%>);
            }
            else {
                Iamount = '0';
            }

            if (radioValue == 'Yes') {
                cancel = 15 * parseFloat(<%=TotalPax%>);

            }
            else {
                cancel = 0;
            }

            if (radioValue1 == 'Yes') {
                var objcancel = {};
                objcancel.cancel = 'yes';
                objcancel.am = Iamount;
                objcancel.ssid = '<%=ssid%>';
                $.ajax({
                    type: "POST",
                    url: "PassengerDetails.aspx/UpdateInsurance",
                    data: JSON.stringify(objcancel),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert(msg.d);
                    }
                });
            }
            else {

                var objcancel = {};
                objcancel.cancel = 'no';
                objcancel.am = Iamount;
                objcancel.ssid = '<%=ssid%>';
                $.ajax({
                    type: "POST",
                    url: "PassengerDetails.aspx/UpdateInsurance",
                    data: JSON.stringify(objcancel),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert(msg.d);
                    }
                });
            }
            if (ctrl.id.indexOf("Yes") != -1) {
                $("#CancellationFee").empty();
                $("#CancellationFee").append(cancel + ".00 <small>USD</small>");
                $("#CancellationFee1").empty();
                $("#CancellationFee1").append(cancel + ".00 <small>USD</small>");
                //$("#Insurance").empty();
                //$("#Insurance").append(Iamount + " <small>USD</small>");
                //$("#Insurance1").empty();
                //$("#Insurance1").append(Iamount + " <small>USD</small>");
                $("#totalAmount1").empty();
                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + "<small> USD</small> ");
                $("#totalAmount").empty();
                $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD</h4><p>incl. all taxes and fees</p>");
                $("#totalAmount2").empty();
                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> </h3>");
                $("#totalAmount3").empty();
                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount4").empty();
                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount5").empty();
                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));
                $("#CASInsurance").val(cancel);

            }
            else {
                <%--if ('<%=cancel%>' == 'yes') {
                    cancel = -15 * parseFloat(<%=TotalPax%>);
                }
                else {
                    cancel = 0;
                }--%>
                $("#CancellationFee").empty();
                $("#CancellationFee").append(cancel + ".00 <small>USD</small>");
                $("#CancellationFee1").empty();
                $("#CancellationFee1").append(cancel + ".00 <small>USD</small>");
                //$("#Insurance").empty();
                //$("#Insurance").append(" 0.00 <small>USD</small>");
                //$("#Insurance1").empty();
                //$("#Insurance1").append(" 0.00 <small>USD</small>");
                $("#totalAmount1").empty();
                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + "<small> USD</small> ");
                $("#totalAmount").empty();
                $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD</h4><p>incl. all taxes and fees</p>");
                $("#totalAmount2").empty();
                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> </h3>");
                $("#totalAmount3").empty();
                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount4").empty();
                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAmount5").empty();
                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));
                $("#CASInsurance").val(parseFloat("0"));
            }
        }

        function validatePassengers() {
            var err = "";
            var errDD = "";
            var errMM = "";
            var errYY = "";

            var paxCnt = $("#paxTtCount_hidden").val();

            for (var pCnt = 1; pCnt <= paxCnt; pCnt++) {

                var paxTitle = document.forms["Form1"]["ddlTitle" + pCnt];
                var flagTitle = validate_dropDown("ddlTitle" + pCnt);
                if (!flagTitle) {
                    err += "please enter valid Title-"
                }
                else {
                }

                var fname = document.forms["Form1"]["txtFName" + pCnt];
                var flagFna = requiredPax(fname);
                if (!flagFna) {
                    err += "please enter valid first name-";
                    $("#spanErrFNA" + pCnt).text("please enter valid first name");
                    $("#spanErrFNA" + pCnt).show();
                }
                else {
                    $("#spanErrFNA" + pCnt).text(" ");
                    $("#spanErrFNA" + pCnt).show();
                }

                var lname = document.forms["Form1"]["txtLName" + pCnt];
                var flagLna = requiredPax(lname);
                if (!flagLna) {
                    err += "please enter valid Last name";
                    $("#spanErrLNA" + pCnt).text("please enter valid last name");
                    $("#spanErrLNA" + pCnt).show();
                }
                else {
                    $("#spanErrLNA" + pCnt).text(" ");
                    $("#spanErrLNA" + pCnt).hide();
                }

                var paxDD = document.forms["Form1"]["ddlDOBDay" + pCnt];
                var flagDD = validate_dropDown("ddlDOBDay" + pCnt);
                if (!flagDD) {
                    err += "Please select valid Day of pax no." + pCnt + "-";
                    //errDD += "Please select valid Day of pax no." + pCnt + "-";
                }

                var paxMM = document.forms["Form1"]["ddlDOBMonth" + pCnt];
                var flagMM = validate_dropDown("ddlDOBMonth" + pCnt);
                if (!flagMM) {
                    err += "Please select valid Month of pax no." + pCnt + "-";
                    //errMM += "Please select valid Month of pax no." + pCnt + "-";
                }

                var paxYY = document.forms["Form1"]["ddlDOBYear" + pCnt];
                var flagYY = validate_dropDown("ddlDOBYear" + pCnt);
                if (!flagYY) {
                    err += "Please select valid Year of pax no." + pCnt + "-";
                    //errYY += "Please select valid Year of pax no." + pCnt + "-";
                }
                //spnPaxDD                                         
            }

            //        if (errDD != "") {
            //            $("#spanPaxErrDD").text(errDD);
            //            $("#spanPaxErrDD").show();
            //            $("#divErrDD").show();
            //        }
            //        else {
            //            $("#spanPaxErrDD").text(" ");
            //            $("#spanPaxErrDD").hide();
            //            $("#divErrDD").hide(); 
            //        }
            //        if (errMM != "") {
            //            $("#spanPaxErrMM").text(errMM);
            //            $("#spanPaxErrMM").show();
            //            $("#divErrMM").show();
            //        }
            //        else {
            //            $("#spanPaxErrMM").text(" ");
            //            $("#spanPaxErrMM").hide();
            //            $("#divErrMM").hide(); 
            //        }
            //        if (errYY != "") {
            //            $("#spanPaxErrYY").text(errYY);
            //            $("#spanPaxErrYY").show();
            //            $("#divErrYY").show(); 
            //        }
            //        else {
            //            $("#spanPaxErrYY").text(" ");
            //            $("#spanPaxErrYY").hide();
            //            $("#divErrYY").hide(); 
            //        }
            //        if (errDD != "" || errMM != "" || errYY != "") {
            //            $("#divErrDob").show();
            //            err = "dob error";
            //        }
            //        else {
            //            $("#divErrDob").hide();
            //        }

            //alert(err)

            return err;

        }

        function assignPassengerValues() {
            var assignFlag = true;
            var strPaxTitles = "";
            var strPaxFNames = "";
            var strPaxMNames = "";
            var strPaxLNames = "";
            var strPaxDobs = "";
            var paxCnt = $("#paxTtCount_hidden").val();
            for (var pCnt = 1; pCnt <= paxCnt; pCnt++) {

                if ($("#txtPaxTitle" + pCnt).val() == "")
                    assignFlag = false;
                else
                    strPaxTitles += $("#txtPaxTitle" + pCnt).val() + "-";


                if ($("#txtFName" + pCnt).val() == "")
                    assignFlag = false;
                else
                    strPaxFNames += $("#txtFName" + pCnt).val() + "-";

                strPaxMNames += $("#txtMName" + pCnt).val() + "-";

                if ($("#txtLName" + pCnt).val() == "")
                    assignFlag = false;
                else
                    strPaxLNames += $("#txtLName" + pCnt).val() + "-";

                if ($("#txtPaxDD" + pCnt).val() == "" || $("#txtPaxMM" + pCnt).val() == "" || $("#txtPaxYY" + pCnt).val() == "")
                    assignFlag = false;
                else
                    strPaxDobs += $("#txtPaxDD" + pCnt).val() + "/" + $("#txtPaxMM" + pCnt).val() + "/" + $("#txtPaxYY" + pCnt).val() + "-";
            }

            if (assignFlag) {
                $("#paxTitles_hidden").val(strPaxTitles);
                $("#paxFName_hidden").val(strPaxFNames);
                $("#paxMName_hidden").val(strPaxMNames);
                $("#paxLName_hidden").val(strPaxLNames);
                $("#paxDobs_hidden").val(strPaxDobs);
            }
            //alert($("#paxTitles_hidden").val() + "  " + $("#paxFName_hidden").val() + "  " + $("#paxMName_hidden").val() + "  " + $("#paxLName_hidden").val() + $("#paxDobs_hidden").val());

            return assignFlag;
        }

        function validatePassengersDob(id, idd) {
            var err = "";
            var errDD = "";
            var errMM = "";
            var errYY = "";

            if (id.includes("txtPaxDD")) {
                var paxDD = document.forms["Form1"]["txtPaxDD" + idd];
                var flagDD = validateDD(paxDD);
                if (!flagDD) {
                    err += "Please select valid Day of pax no." + idd + "-";
                }
            }
            else if (id.includes("txtPaxMM")) {
                var paxMM = document.forms["Form1"]["txtPaxMM" + idd];
                var flagMM = validateMM(paxMM);
                if (!flagMM) {
                    err += "Please select valid Month of pax no." + idd + "-";
                }
            }
            else if (id.includes("txtPaxYY")) {
                var paxYY = document.forms["Form1"]["txtPaxYY" + idd];
                var flagYY = validateYY(paxYY);
                if (!flagYY) {
                    err += "Please select valid Year of pax no." + idd + "-";
                }
            }
            //alert(idd);

            if (id.includes("txtPaxDD")) {
                if ($("#spanPaxErrDD").text() == "") {
                    $("#spanPaxErrDD").text(err);
                    errDD = $("#spanPaxErrDD").text();
                    $("#spanPaxErrDD").show();
                    $("#divErrDD").show();

                }
                else {
                    var strErr = $("#spanPaxErrDD").text();

                    if (err != "") {
                        if (strErr.includes(err)) {
                            $("#spanPaxErrDD").text(strErr);
                            errDD = $("#spanPaxErrDD").text();
                        }
                        else {
                            $("#spanPaxErrDD").text($("#spanPaxErrDD").text() + err);
                            errDD = $("#spanPaxErrDD").text();
                        }
                    }
                    else {
                        //alert();
                        var srtSS = $("#spanPaxErrDD").text().replace("Please select valid Day of pax no." + idd + "-", "");
                        //alert(srtSS);
                        $("#spanPaxErrDD").text(srtSS);
                        errDD = $("#spanPaxErrDD").text();
                    }
                }

            }
            else if (id.includes("txtPaxMM")) {
                if ($("#spanPaxErrMM").text() == "") {
                    $("#spanPaxErrMM").text(err);
                    errMM = $("#spanPaxErrMM").text();
                    $("#spanPaxErrMM").show();
                    $("#divErrMM").show();

                }
                else {
                    var strErr = $("#spanPaxErrMM").text();

                    if (err != "") {
                        if (strErr.includes(err)) {
                            $("#spanPaxErrMM").text(strErr);
                            errMM = $("#spanPaxErrMM").text();
                        }
                        else {
                            $("#spanPaxErrMM").text($("#spanPaxErrMM").text() + err);
                            errMM = $("#spanPaxErrMM").text();
                        }
                    }
                    else {
                        //alert();
                        var srtSS = $("#spanPaxErrMM").text().replace("Please select valid Month of pax no." + idd + "-", "");
                        //alert(srtSS);
                        $("#spanPaxErrMM").text(srtSS);
                        errMM = $("#spanPaxErrMM").text();
                    }

                }

            }
            else if (id.includes("txtPaxYY")) {
                if ($("#spanPaxErrYY").text() == "") {
                    $("#spanPaxErrYY").text(err);
                    errYY = $("#spanPaxErrYY").text();
                    $("#spanPaxErrYY").show();
                    $("#divErrYY").show();

                }
                else {
                    var strErr = $("#spanPaxErrYY").text();

                    if (err != "") {
                        if (strErr.includes(err)) {
                            $("#spanPaxErrYY").text(strErr);
                            errYY = $("#spanPaxErrYY").text();
                        }
                        else {
                            $("#spanPaxErrYY").text($("#spanPaxErrYY").text() + err);
                            errYY = $("#spanPaxErrYY").text();
                        }
                    }
                    else {
                        //alert();
                        var srtSS = $("#spanPaxErrYY").text().replace("Please select valid Year of pax no." + idd + "-", "");
                        //alert(srtSS);
                        $("#spanPaxErrYY").text(srtSS);
                        errYY = $("#spanPaxErrYY").text();
                    }

                }

            }

            if ($("#spanPaxErrDD").text() != "") {
                if (errDD != "") {
                    $("#spanPaxErrDD").text(errDD);
                    $("#spanPaxErrDD").show();
                    $("#divErrDD").show();
                }
            }
            else {
                $("#spanPaxErrDD").hide();
                $("#divErrDD").hide();
            }
            if ($("#spanPaxErrMM").text() != "") {
                if (errMM != "") {
                    $("#spanPaxErrMM").text(errMM);
                    $("#spanPaxErrMM").show();
                    $("#divErrMM").show();
                }
            }
            else {
                $("#spanPaxErrMM").hide();
                $("#divErrMM").hide();
            }
            if ($("#spanPaxErrYY").text() != "") {
                if (errYY != "") {
                    $("#spanPaxErrYY").text(errYY);
                    $("#spanPaxErrYY").show();
                    $("#divErrYY").show();
                }
            }
            else {
                $("#spanPaxErrYY").hide();
                $("#divErrYY").hide();
            }


            if ($("#spanPaxErrDD").text() != "" || $("#spanPaxErrMM").text() != "" || $("#spanPaxErrYY").text() != "") {
                $("#divErrDob").show();
                err = "dob error";
            }
            else {
                $("#divErrDob").hide();
            }



            //return err;

        }
    </script>



<script type="text/javascript">
        function GetChatMessage() {
            PageMethods.GetMessage('<%=sid%>','<%=TtAmount%>','<%=isProfitMaxi_Country_or_City%>', OnSuccess);
        }
        function OnSuccess(response, userContext, methodName) {

	if(response=="NA")
	{
	clearInterval(myVar);
	message='';

	}
	else
	{
            message=response;
	}
	
        }
</script>

  

 <!--
 <script src="js/google-analytics.js"></script>
-->
    

  <!-- Google Tag Manager -->
    <script>        (function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-KGS238J');</script>
    <!-- End Google Tag Manager -->


</head>
<body>

<!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-KGS238J"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->


    <!-- #include file ="Header.aspx" -->

    <form id="form1" name="form1" runat="server" class="form-horizontal">


 <asp:ScriptManager ID="ScriptManager1" runat="server"  EnablePageMethods="true">
</asp:ScriptManager>




        <input type="hidden" name="ioBB" id="ioBB">
        <input type="hidden" name="fpBB" id="fpBB">

        <input type="hidden" name="_paxcount" id="_paxcount" runat="server" />
        <input type="hidden" name="_adtcount" id="_adtcount" runat="server" />
        <input type="hidden" name="_chdcount" id="_chdcount" runat="server" />
        <input type="hidden" name="_infcount" id="_infcount" runat="server" />

        <input type="hidden" name="DeptDate" id="DeptDate" runat="server" />
        <input type="hidden" name="ArrDate1" id="ArrDate1" runat="server" />
        <input type="hidden" name="DeptCountry" id="DeptCountry" runat="server" />
        <input type="hidden" name="DestCountry" id="DestCountry" runat="server" />
        <input type="hidden" name="JType" id="JType" runat="server" />

        <input type="hidden" name="IpCountry" id="IpCountry" runat="server" />
        <input type="hidden" name="IpCity" id="IpCity" runat="server" />
        <input type="hidden" name="PaxEmailID" id="PaxEmailID" runat="server" />

        <input type="hidden" name="afid1" id="afid1" runat="server" />
        <input type="hidden" name="searchType1" id="searchType1" runat="server" />

        <input type="hidden" name="VoucherCode" id="VoucherCode" runat="server" />
        <input type="hidden" name="VoucherApply" id="VoucherApply" runat="server" />
        <input type="hidden" name="totalAm" id="totalAm" runat="server" />
        <input type="hidden" name="ExtendedCancellation" id="ExtendedCancellation" runat="server" />
        <input type="hidden" name="CASInsurance" id="CASInsurance" runat="server" />
        <input type="hidden" name="conf" id="conf" runat="server" />
        <input type="hidden" name="inid" id="inid" runat="server" />
        <input type="hidden" name="outid" id="outid" runat="server" />
        <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" runat="server" />
        <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" runat="server" />
        <input type="hidden" name="paymentMode" id="paymentMode" runat="server" />
        <asp:Panel ID="pnlPayment" runat="server">
            <section class="pm-wraper"  id="BackImgcity" runat="server">
                <div class="container backbg-trans">
                    <div class="row">
                        <div class="col-md-12" id="backresults" runat="server">
                            <div class="backtoRslt">
                                <div class="backtoRslt-Inner">
                                    <div class="backtoResult"><span class="tcktleft">Only 3 tickets left</span> at this price!</div>
                                    <div class="ticketsLeft"><a href="FlightResults.aspx"><i class="fa fa-angle-left" aria-hidden="true"></i> More Results</a></div>
                                </div>
                                <div class="reviewProcess">
                                    <ul>
                                        <li class="reviewProcessList"><i class="fa fa-search"></i>Review</li>
                                        <li><i class="fa fa-user"></i>Travellers</li>
                                        <li><i class="fa fa-ticket"></i>Payment</li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-9 col-sm-9 col-xs-12">
                            <div id="diverrmsg" class="pm-container" runat="server" style="display: none">
                            </div>


                            <div class="pm-container">

                                <div class="pm-head-title" id="divFDHeader1" runat="server">
                                </div>

                                <div class="tripdetails-info" id="divFDHeader2" runat="server">
                                </div>

                                 

                                <div class="pm-content" id="divFlightDetails" runat="server">
                                </div>
                            </div>

                            <div class="pay-tab-btn">
                     	<%--<ul id="pay-menu" role="tablist">--%>

                             <%=paynowORlater %>

                             <%--<li><a data-page="paynow" class="active">Pay Now</a></li>
    						<li><a data-page="paylater">Pay Later</a></li>--%>
                    	<%--</ul>--%>
               		</div>


                            <div id="paylater_description"  class="pm-container" runat="server"></div>
                            
                        <%--	<div class="pay-option-title" >
                            	Lock Fare Benefits
                            </div>
                            
                            <div class="pay-later-tbl">
                            	<div class="pay-later-cel-1">
                                	<img src="Design/images/lockfareIcon.png" alt="Lock fare icon">
                                	<h4>Locked Price</h4>
									<p>Lock @ $0 helps you lock the<br> fares by paying nothing.
									Absolutely nothing.</p>
                                </div>
                                <div class="pay-later-cel-2">
                                	<img src="Design/images/pricehighIcon.png" alt="Price hike icon">
                                	<h4>Price Hike Protection</h4>
									<p>Fares can increase in next 24 hours. <br>You pay at the locked price.</p>
                                </div>
                            </div>--%>

                            
                             <div class="pm-container" id="divPassengerDetails" runat="server">
                            </div>


                           
                    <div class="pay-option-wrap" id="pages">

                    	<div id="paynow" data-page="paynow" class="pay-option-row page">

                            
                           
                            <div class="passpotWrap">
                                        <div class="passpotImg">
                                            <img src="Design/images/passport-icon.png" alt="passport-icon" class="img-responsive faa-horizontal animated" />
                                        </div>
                                        <div class="passpottext">
                                            <h3>Check Passenger Details again</h3>
                                            <p>Make sure that all above passenger names are exactly same as in their passports, otherwise airlines deny the boarding and you might need to buy a new ticket or pay additional charges to make changes.</p>
                                        </div>
                                    </div>

                            <div class='cancelPolicy-bg'>
                                <h3>Lower Price Finder and 24 Hours Cancellation Policy <small class="white">(Optional)</small></h3>
                               
                            	<p><i class="fa fa-check"></i> Hassle free cancellation for a very minimum fee. Our 24 hours Cancellation Policy allows you to cancel your flight for any reason within 24 hours of your booking.</p>
                            
								<p><i class="fa fa-check"></i> Our Lower Price Finder will automatically check for additional savings on your exact itinerary for up to 24 hours after you book.</p>
                            
                                <div class='cancelbg-inner'>

                                    <div id='yesExtend' class='form-group'>
                                        <input id='CancellPolicy-Yes' class='radio-custom' onchange='Cancelpolicy(this)' name='radio-group' value='Yes' type='radio' checked>
                                        <label for='CancellPolicy-Yes' class='radio-custom-label'>Yes, I'd like to take advantage of the Lower Price Finder and 24 Hours Cancellation Policy benefits!  <strong>Add 15 USD</strong></label>
                                    </div>

                                    <div id='donotExtend' class='form-group'>
                                        <input id='CancellPolicy-No' value='No' class='radio-custom' onchange='Cancelpolicy(this)' name='radio-group' type='radio' checked>
                                        <label for='CancellPolicy-No' class='radio-custom-label'>No thanks, I decline the Lower Price Finder and 24 Hours cancellation plan. Changes/Cancellation starts at <strong>USD150</strong> per ticket</label>
                                    </div>

                                </div>
                            </div> 
                           
                             <div class="pm-container" id="paymentDetails">
                                <div class="pm-head-title">
                                    <div class="pm-content">
                                        <div class="pm-payment-head">
                                            <img src='Design/images/payment-icon.png' alt='Passenger icon' class='title-icons'>
                                            Payment Details
                                            <p>Your credit card information is protected by a Secure SSL Encrypted Transaction </p>
                                        </div>

                                        <div class="pm-visaimg">
                                            <img src="Design/images/visaMasterAmExp-Cards.png" alt="Visa Master And Amex">
                                        </div>
                                    </div>
                                </div>
                                <div class="pm-content">
                                    <div class="pm-payment">
                                        <div class="pm-error warning" id="paymentErrors" runat="server" style="display: none">
                                        </div>
                                        <div class="form-container" id="paymentCard">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div class="paymentCard-icons" id="paymentHeader" runat="server">
                                                    </div>
                                                    <div class="card-wrapper"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <form class="form-horizontal">
                                                        <div class="ccInput tool-main-wrap">
                                                            <label for="NameonCard">NAME ON CARD <span class="starMark">*</span></label>
                                                            <div class="inputbx tooltip tooltip-west" id="cardNamehint">
                                                                <input type="text" class="" onblur="val(this)" data-placement="bottom" autocomplete="off" data-msg-required="Please enter the card holder name" data-rule-required="true" name="CardHolderName" id="CardHolderName" placeholder="Card Holder's Name">
                                                                <%--<input value="" type="text" id="" name="CardHolderName" placeholder="Enter name on card" required>--%>
                                                            </div>
                                                        </div>

                                                        <div class="ccInput tool-main-wrap">
                                                            <label for="CARDNUMBER">CARD NUMBER <span class="starMark">*</span></label>
                                                            <div class="inputbx tooltip tooltip-west" id="cardNumberhint">
                                                                <input type="text" class="creditcardnumber" onblur="val(this)" data-placement="bottom" autocomplete="off" name="cardNumber" id="cardNumber" placeholder="Debit/Credit Card Number" data-msg-required="Please enter credit card number." data-rule-required="true" data-msg-number="please enter only numerics." data-rule-number="true" />
                                                                <%--<input value="" type="text" id="input-field" name="number" placeholder="Enter card number" required>--%>
                                                            </div>
                                                        </div>
                                                        <div class="paymentCard tool-main-wrap">
                                                            <label>EXPIRY MM/ YY: <span class="starMark">*</span></label>
                                                            <%--<input class="" type="text" name="expiry" id="expiry" placeholder="MM / YY" required="">--%>
                                                            <div class="card-expMonth selectbx tooltip" id="ExpiryMonthhint">
                                                                <select class="" onblur="val(this)" data-placement="bottom" data-msg-required="Please select the month of expiry date on your credit card." data-rule-required="true" name="ExpMonth" id="ExpMonth">
                                                                    <option value="">Month</option>
                                                                    <option value="01">Jan</option>
                                                                    <option value="02">Feb</option>
                                                                    <option value="03">Mar</option>
                                                                    <option value="04">Apr</option>
                                                                    <option value="05">May</option>
                                                                    <option value="06">Jun</option>
                                                                    <option value="07">Jul</option>
                                                                    <option value="08">Aug</option>
                                                                    <option value="09">Sep</option>
                                                                    <option value="10">Oct</option>
                                                                    <option value="11">Nov</option>
                                                                    <option value="12">Dec</option>
                                                                </select>
                                                            </div>
                                                            <div class="card-expYear selectbx tooltip" id="ExpiryYearhint">
                                                                <select class="" onblur="val(this)" data-placement="bottom" data-msg-required="Please select the year of expiry date on your credit card." data-rule-required="true" name="ExpYear" id="ExpYear">
                                                                    <option value="">Year</option>
                                                                    <%               
                                                                        for (int i = 0; i <= 20; i++)
                                                                        {
                                                                            Response.Write("<option value=\"" + (DateTime.Now.Year + i) + "\">" + (DateTime.Now.Year + i) + "</option>");
                                                                        }
                                                                    %>
                                                                </select>
                                                                <input type="hidden" id="expiry" name="expiry" runat="server" />
                                                                <input type="hidden" id="cardtype" name="cardtype" runat="server" />
                                                            </div>
                                                        </div>

                                                        <div class="cvvNum tool-main-wrap">
                                                            <label for="CVVNo">CVV <span class="starMark">*</span></label>
                                                            <div class="inputbx tooltip" id="cvvhint">
                                                                <input autocomplete="off" onblur="val(this)" data-placement="bottom" class='' name="cvc" id="cvc" data-msg-required="please enter the cvv number of your credit card" data-rule-required="true" data-msg-number="Please enter only numerics." data-rule-number="true" data-msg-minlength="cvv should be minimum of three digits" data-rule-minlength="3" data-msg-maxlength="cvv should not be more than three digits" data-rule-maxlength="3" placeholder='ex. 000' size='4' type='text'>
                                                            </div>
                                                        </div>
                                                    </form>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="pm-container">
                                <div class="pm-head-title">
                                    <div class="pm-content">
                                        <img src="Design/images/address-icon.png" alt="Address icon" class="title-icons">
                                        Billing & Contact Information
                                        <p>Please enter the billing information and phone number on file with your credit card provider. If this information does not match the account information on file with your card provider, your booking could be delayed or cancelled.</p>
                                    </div>
                                </div>

                                <div class="pm-content">
                                    <div class="pm-error warning" id="billingError" runat="server" style="display: none">
                                    </div>
                                    <div class="pm-billing-ads">



   				     <div class="md-input tool-main-wrap">
                                       <div class="inputbx tooltip" id="locationdiv">
                                         <label for="location">Enter your address</label>
                                            <input id="location" placeholder="Enter your address" onFocus="geolocate()" type="text" class="form-control">
                                     </div>
                                     </div>



                                        <div class="md-input tool-main-wrap">
                                            <label for="Address">Address <span class="starMark">*</span></label>
                                            <div class="inputbx tooltip" id="Addresshint">
                                                <input type="text" class="" onblur="val(this)" data-placement="bottom" runat="server" name="Address" id="Address" data-msg-required="please enter the billing address of your credit card" data-rule-required="true" placeholder="Address">
                                                <%--<input value="" type="text" id="Address" name="Address" placeholder="Enter your Address">--%>
                                            </div>
                                        </div>
                                        <div class="md-input tool-main-wrap">
                                            <label for="City">City  <span class="starMark">*</span></label>
                                            <div class="inputbx tooltip" id="Cityhint">
                                                <input type="text" class="" onblur="val(this)" data-placement="bottom" name="City" id="City" data-msg-required="please enter the billing city of your credit card" data-rule-required="true" placeholder="City" runat="server">
                                                <%--<input value="" type="text" id="City" name="City" placeholder="Enter your City">--%>
                                            </div>
                                        </div>

                                        <div class="md-input tool-main-wrap">
                                            <label for="State">State<span class="starMark">*</span></label>
                                            <div class="inputbx tooltip" id="Statehint">
                                                <input type="text" class="" onblur="val(this)" data-placement="bottom" name="State" id="State" data-msg-required="please enter the billing city of your credit card" data-rule-required="true" placeholder="State" runat="server">
                                                <%--<input value="" type="text" id="State" name="State" placeholder="Enter your State">--%>
                                            </div>
                                        </div>
                                        <div class="sm-input tool-main-wrap">
                                            <label for="Postcode">Postcode <span class="starMark">*</span></label>
                                            <div class="inputbx tooltip" id="Postcodehint">
                                                <input type="text" class="" onblur="val(this)" data-placement="bottom" data-msg-required="please enter the billing city zipcode of your credit card" data-rule-required="true" enableviewstate="true" name="ZipCode" id="ZipCode" placeholder="Zip Code" runat="server">
                                            </div>
                                        </div>
                                        <div class="md-select tool-main-wrap">
                                            <label for="Country">Country <span class="starMark">*</span></label>
                                            <div class="tooltip tooltip-west" id="Countryhint" runat="server">
                                                <select class="" onblur="val(this)" data-placement="bottom" data-msg-required="Please select the country of the billing address." data-rule-required="true" name="country" id="country" runat="server">
                                                    <option value="">Select country Name</option>
                                                    <option value="AF">Afghanistan</option>
                                                    <option value="AX">Aland Islands</option>
                                                    <option value="AL">Albania</option>
                                                    <option value="DZ">Algeria</option>
                                                    <option value="AS">American Samoa</option>
                                                    <option value="AD">Andorra</option>
                                                    <option value="AO">Angola</option>
                                                    <option value="AI">Anguilla</option>
                                                    <option value="AQ">Antarctica</option>
                                                    <option value="AG">Antigua and Barbuda</option>
                                                    <option value="AR">Argentina</option>
                                                    <option value="AM">Armenia</option>
                                                    <option value="AW">Aruba</option>
                                                    <option value="AU">Australia</option>
                                                    <option value="AT">Austria</option>
                                                    <option value="AZ">Azerbaijan</option>
                                                    <option value="BS">Bahamas</option>
                                                    <option value="BH">Bahrain</option>
                                                    <option value="BD">Bangladesh</option>
                                                    <option value="BB">Barbados</option>
                                                    <option value="BY">Belarus</option>
                                                    <option value="BE">Belgium</option>
                                                    <option value="BZ">Belize</option>
                                                    <option value="BJ">Benin</option>
                                                    <option value="BM">Bermuda</option>
                                                    <option value="BT">Bhutan</option>
                                                    <option value="BO">Bolivia</option>
                                                    <option value="BA">Bosnia and Herzegovina</option>
                                                    <option value="BW">Botswana</option>
                                                    <option value="BV">Bouvet Island</option>
                                                    <option value="BR">Brazil</option>
                                                    <option value="IO">British Indian Ocean Territory</option>
                                                    <option value="BN">Brunei Darussalam</option>
                                                    <option value="BG">Bulgaria</option>
                                                    <option value="BF">Burkina Faso</option>
                                                    <option value="BI">Burundi</option>
                                                    <option value="KH">Cambodia</option>
                                                    <option value="CM">Cameroon</option>
                                                    <option value="CA">Canada</option>
                                                    <option value="CV">Cape Verde</option>
                                                    <option value="KY">Cayman Islands</option>
                                                    <option value="CF">Central African Republic</option>
                                                    <option value="TD">Chad</option>
                                                    <option value="CL">Chile</option>
                                                    <option value="CN">China</option>
                                                    <option value="CX">Christmas Island</option>
                                                    <option value="CC">Cocos (Keeling) Islands</option>
                                                    <option value="CO">Colombia</option>
                                                    <option value="KM">Comoros</option>
                                                    <option value="CG">Congo</option>
                                                    <option value="CD">Congo The Democratic Republic of the</option>
                                                    <option value="CK">Cook Islands</option>
                                                    <option value="CR">Costa Rica</option>
                                                    <option value="CI">Côte d'Ivoire</option>
                                                    <option value="HR">Croatia</option>
                                                    <option value="CU">Cuba</option>
                                                    <option value="CY">Cyprus</option>
                                                    <option value="CZ">Czech Republic</option>
                                                    <option value="DK">Denmark</option>
                                                    <option value="DJ">Djibouti</option>
                                                    <option value="DM">Dominica</option>
                                                    <option value="DO">Dominican Republic</option>
                                                    <option value="EC">Ecuador</option>
                                                    <option value="EG">Egypt</option>
                                                    <option value="SV">El Salvador</option>
                                                    <option value="GQ">Equatorial Guinea</option>
                                                    <option value="ER">Eritrea</option>
                                                    <option value="EE">Estonia</option>
                                                    <option value="ET">Ethiopia</option>
                                                    <option value="FK">Falkland Islands (Malvinas)</option>
                                                    <option value="FO">Faroe Islands</option>
                                                    <option value="FJ">Fiji</option>
                                                    <option value="FI">Finland</option>
                                                    <option value="FR">France</option>
                                                    <option value="GF">French Guiana</option>
                                                    <option value="PF">French Polynesia</option>
                                                    <option value="TF">French Southern Territories</option>
                                                    <option value="GA">Gabon</option>
                                                    <option value="GM">Gambia</option>
                                                    <option value="GE">Georgia</option>
                                                    <option value="DE">Germany</option>
                                                    <option value="GH">Ghana</option>
                                                    <option value="GI">Gibraltar</option>
                                                    <option value="GR">Greece</option>
                                                    <option value="GL">Greenland</option>
                                                    <option value="GD">Grenada</option>
                                                    <option value="GP">Guadeloupe</option>
                                                    <option value="GU">Guam</option>
                                                    <option value="GT">Guatemala</option>
                                                    <option value="GG">Guernsey</option>
                                                    <option value="GN">Guinea</option>
                                                    <option value="GW">Guinea-Bissau</option>
                                                    <option value="GY">Guyana</option>
                                                    <option value="HT">Haiti</option>
                                                    <option value="HM">Heard Island and McDonald Islands</option>
                                                    <option value="VA">Holy See (Vatican City State)</option>
                                                    <option value="HN">Honduras</option>
                                                    <option value="HK">Hong Kong</option>
                                                    <option value="HU">Hungary</option>
                                                    <option value="IS">Iceland</option>
                                                    <option value="IN">India</option>
                                                    <option value="ID">Indonesia</option>
                                                    <option value="IR">Iran Islamic Republic of</option>
                                                    <option value="IQ">Iraq</option>
                                                    <option value="IE">Ireland</option>
                                                    <option value="IM">Isle of Man</option>
                                                    <option value="IL">Israel</option>
                                                    <option value="IT">Italy</option>
                                                    <option value="JM">Jamaica</option>
                                                    <option value="JP">Japan</option>
                                                    <option value="JE">Jersey</option>
                                                    <option value="JO">Jordan</option>
                                                    <option value="KZ">Kazakhstan</option>
                                                    <option value="KE">Kenya</option>
                                                    <option value="KI">Kiribati</option>
                                                    <option value="KP">Korea Democratic People's Republic of</option>
                                                    <option value="KR">Korea Republic of</option>
                                                    <option value="KW">Kuwait</option>
                                                    <option value="KG">Kyrgyzstan</option>
                                                    <option value="LA">Lao People's Democratic Republic</option>
                                                    <option value="LV">Latvia</option>
                                                    <option value="LB">Lebanon</option>
                                                    <option value="LS">Lesotho</option>
                                                    <option value="LR">Liberia</option>
                                                    <option value="LY">Libyan Arab Jamahiriya</option>
                                                    <option value="LI">Liechtenstein</option>
                                                    <option value="LT">Lithuania</option>
                                                    <option value="LU">Luxembourg</option>
                                                    <option value="MO">Macao</option>
                                                    <option value="MK">Macedonia The Former Yugoslav Republic of</option>
                                                    <option value="MG">Madagascar</option>
                                                    <option value="MW">Malawi</option>
                                                    <option value="MY">Malaysia</option>
                                                    <option value="MV">Maldives</option>
                                                    <option value="ML">Mali</option>
                                                    <option value="MT">Malta</option>
                                                    <option value="MH">Marshall Islands</option>
                                                    <option value="MQ">Martinique</option>
                                                    <option value="MR">Mauritania</option>
                                                    <option value="MU">Mauritius</option>
                                                    <option value="YT">Mayotte</option>
                                                    <option value="MX">Mexico</option>
                                                    <option value="FM">Microneia Federated States of</option>
                                                    <option value="MD">Moldova</option>
                                                    <option value="MC">Monaco</option>
                                                    <option value="MN">Mongolia</option>
                                                    <option value="ME">Montenegro</option>
                                                    <option value="MS">Montserrat</option>
                                                    <option value="MA">Morocco</option>
                                                    <option value="MZ">Mozambique</option>
                                                    <option value="MM">Myanmar</option>
                                                    <option value="NA">Namibia</option>
                                                    <option value="NR">Nauru</option>
                                                    <option value="NP">Nepal</option>
                                                    <option value="NL">Netherlands</option>
                                                    <option value="AN">Netherlands Antilles</option>
                                                    <option value="NC">New Caledonia</option>
                                                    <option value="NZ">New Zealand</option>
                                                    <option value="NI">Nicaragua</option>
                                                    <option value="NE">Niger</option>
                                                    <option value="NG">Nigeria</option>
                                                    <option value="NU">Niue</option>
                                                    <option value="NF">Norfolk Island</option>
                                                    <option value="MP">Northern Mariana Islands</option>
                                                    <option value="NO">Norway</option>
                                                    <option value="OM">Oman</option>
                                                    <option value="PK">Pakistan</option>
                                                    <option value="PW">Palau</option>
                                                    <option value="PS">Palestinian Territory Occupied</option>
                                                    <option value="PA">Panama</option>
                                                    <option value="PG">Papua New Guinea</option>
                                                    <option value="PY">Paraguay</option>
                                                    <option value="PE">Peru</option>
                                                    <option value="PH">Philippines</option>
                                                    <option value="PN">Pitcairn</option>
                                                    <option value="PL">Poland</option>
                                                    <option value="PT">Portugal</option>
                                                    <option value="PR">Puerto Rico</option>
                                                    <option value="QA">Qatar</option>
                                                    <option value="RE">Réunion</option>
                                                    <option value="RO">Romania</option>
                                                    <option value="RU">Russian Federation</option>
                                                    <option value="RW">Rwanda</option>
                                                    <option value="BL">Saint Barthélemy</option>
                                                    <option value="SH">Saint Helena</option>
                                                    <option value="KN">Saint Kitts and Nevis</option>
                                                    <option value="LC">Saint Lucia</option>
                                                    <option value="MF">Saint Martin</option>
                                                    <option value="PM">Saint Pierre and Miquelon</option>
                                                    <option value="VC">Saint Vincent and the Grenadines</option>
                                                    <option value="WS">Samoa</option>
                                                    <option value="SM">San Marino</option>
                                                    <option value="ST">Sao Tome and Principe</option>
                                                    <option value="SA">Saudi Arabia</option>
                                                    <option value="SN">Senegal</option>
                                                    <option value="RS">Serbia</option>
                                                    <option value="SC">Seychelles</option>
                                                    <option value="SL">Sierra Leone</option>
                                                    <option value="SG">Singapore</option>
                                                    <option value="SK">Slovakia</option>
                                                    <option value="SI">Slovenia</option>
                                                    <option value="SB">Solomon Islands</option>
                                                    <option value="SO">Somalia</option>
                                                    <option value="ZA">South Africa</option>
                                                    <option value="GS">South Georgia and the South Sandwich Islands</option>
                                                    <option value="ES">Spain</option>
                                                    <option value="LK">Sri Lanka</option>
                                                    <option value="SD">Sudan</option>
                                                    <option value="SR">Suriname</option>
                                                    <option value="SJ">Svalbard and Jan Mayen</option>
                                                    <option value="SZ">Swaziland</option>
                                                    <option value="SE">Sweden</option>
                                                    <option value="CH">Switzerland</option>
                                                    <option value="SY">Syrian Arab Republic</option>
                                                    <option value="TW">Taiwan Province of China</option>
                                                    <option value="TJ">Tajikistan</option>
                                                    <option value="TZ">Tanzania United Republic of</option>
                                                    <option value="TH">Thailand</option>
                                                    <option value="TL">Timor-Leste</option>
                                                    <option value="TG">Togo</option>
                                                    <option value="TK">Tokelau</option>
                                                    <option value="TO">Tonga</option>
                                                    <option value="TT">Trinidad and Tobago</option>
                                                    <option value="TN">Tunisia</option>
                                                    <option value="TR">Turkey</option>
                                                    <option value="TM">Turkmenistan</option>
                                                    <option value="TC">Turks and Caicos Islands</option>
                                                    <option value="TV">Tuvalu</option>
                                                    <option value="UG">Uganda</option>
                                                    <option value="UA">Ukraine</option>
                                                    <option value="AE">United Arab Emirates</option>
                                                    <option value="GB">United Kingdom</option>
                                                    <option value="US">United States</option>
                                                    <option value="UM">United States Minor Outlying Islands</option>
                                                    <option value="UY">Uruguay</option>
                                                    <option value="UZ">Uzbekistan</option>
                                                    <option value="VU">Vanuatu</option>
                                                    <option value="VE">Venezuela</option>
                                                    <option value="VN">Viet Nam</option>
                                                    <option value="VG">Virgin Islands British</option>
                                                    <option value="VI">Virgin Islands U.S.</option>
                                                    <option value="WF">Wallis and Futuna</option>
                                                    <option value="EH">Western Sahara</option>
                                                    <option value="YE">Yemen</option>
                                                    <option value="ZM">Zambia</option>
                                                    <option value="ZW">Zimbabwe</option>

                                                </select>
                                            </div>
                                        </div>




                                       <%-- <div id="mobileandEmail" runat="server"></div>--%>

                                        <div class="mt-15 ph-email-brd">
                                            <div class="md-input tool-main-wrap">
                                                <label for="Mobile">Mobile <span class="starMark">*</span></label>
                                                <div class="inputbx tooltip" id="Mobilehint">
                                                    <input type="text" class="" onblur="val(this)" data-placement="bottom" name="Mobile" data-msg-required="please enter your mobile number" data-rule-required="true" data-msg-number="Please enter only numerics." data-rule-number="true" id="Mobile" placeholder="Enter Mobile Number" runat="server">
                                                </div>
                                            </div>

                                            <div class="md-input tool-main-wrap">
                                                <label for="E-mail">E-mail <span class="starMark">*</span></label>
                                                <div class="inputbx tooltip" id="Emailhint">
                                                    <input type="text" class="" onblur="val(this)" data-placement="bottom" data-msg-required="please enter your Email address" data-rule-required="true"
                                                        data-msg-email="Please enter valid email address." data-rule-email="true" name="Email" id="Email" placeholder="Enter E-mail" runat="server" />
                                                </div>
                                            </div>
                                            <p class="checkBx">
                                                <input id="newsChannel" class="checkbox-custom" name="Accepted" type="checkbox" checked="checked">
                                                <label for="newsChannel" class="checkbox-custom-label">Subscribe to our news channels</label>
                                            </p>
                                        </div>



                                           <%-- <p class="checkBx">
                                                <input id="newsChannel" class="checkbox-custom" name="Accepted" type="checkbox" checked="checked">
                                                <label for="newsChannel" class="checkbox-custom-label">Subscribe to our news channels</label>
                                            </p>--%>



                                        </div>
                                    
                                </div>
                            </div>
                       	</div> 





                        <div id="paylater"  data-page="paylater" class="pay-option-row page hide">
                        	<div class="pm-container">


                        	<%--<div class="pay-option-title">
                            	Lock Fare Benefits
                            </div>
                            
                            <div class="pay-later-tbl">
                            	<div class="pay-later-cel-1">
                                	<img src="Design/images/lockfareIcon.png" alt="Lock fare icon">
                                	<h4>Locked Price</h4>
									<p>Lock @ $0 helps you lock the<br> fares by paying nothing.
									Absolutely nothing.</p>
                                </div>
                                <div class="pay-later-cel-2">
                                	<img src="Design/images/pricehighIcon.png" alt="Price hike icon">
                                	<h4>Price Hike Protection</h4>
									<p>Fares can increase in next 24 hours. <br>You pay at the locked price.</p>
                                </div>
                            </div>--%>
                            
                          
                            <div class="pm-billing-ads mt-15 ph-email-brd">
                                            
                              
                                        <div class="">
                                           
                                            <div class="md-input tool-main-wrap">
                                                <label for="Mobile1">Mobile <span class="starMark">*</span></label>
                                                <div class="inputbx tooltip" id="Mobilehint1">
                                                    <input type="text" class="" onblur="val(this)" data-placement="bottom" name="Mobile1" data-msg-required="please enter your mobile number" data-rule-required="true" data-msg-number="Please enter only numerics." data-rule-number="true" id="Mobile1" placeholder="Enter Mobile Number" runat="server">
                                                </div>
                                            </div>

                                            <div class="md-input tool-main-wrap">
                                                <label for="E-mail1">E-mail <span class="starMark">*</span></label>
                                                <div class="inputbx tooltip" id="Emailhint1">
                                                    <input type="text" class="" onblur="val(this)" data-placement="bottom" data-msg-required="please enter your Email address" data-rule-required="true"
                                                        data-msg-email="Please enter valid email address." data-rule-email="true" name="Email2" id="Email2" placeholder="Enter E-mail" runat="server" />
                                                </div>
                                            </div>


                                            <p class="checkBx">
                                                <input id="newsChannel1" class="checkbox-custom" name="Accepted" type="checkbox" checked="checked">
                                                <label for="newsChannel1" class="checkbox-custom-label">Subscribe to our news channels</label>
                                            </p>
                                        </div>
                                </div>


                           	</div>
                        </div>

                         <div class="pm-container" id="divPriceDetails" runat="server"></div>

                    </div>

                          
                               <div class="pm-container">
                                
                                <div class="pm-footer-title">
                                    <div class="pm-content" id="ReviewtripTerm" runat="server">


                                       <%-- <h5><strong>Review the trip terms:</strong></h5>
                                        <ul>
					    
                                            <li>1. Name changes are not permitted. Tickets are non-refundable and non-transferable.</li>
                                            <li>2. Where permitted, changes to your itinerary costs you minimum of USD150 per ticket. Airlines fare/fees are additional cost for you.</li>
                                            <li>3. Your credit/debit card may be billed in multiple charges totaling the final total price. Billing statement may disply Airlines name or Travelmerry or Agent Fee or our suplier name.</li>




                                        </ul>--%>

                                    </div>


                                    <div class="pm-content">
                                        <h5><strong>Terms and Conditions:</strong></h5>
                                        <div class="scrollbar" id="sidebar">
                                            <div class="scroll-overflow">
                                                <ul>
                                                    <li>Following is the User agreement between you, the User (referred to herein as "User"or you) and
                                                        TravelMerry ("we, "TravelMerry.com," or "TravelMerry").
                        Please read these terms and conditions (Terms and Conditions) before using the TravelMerry.com site (Site) or making any Bookings.
                        You consent to the Terms and Conditions when you use the site, without qualification. If you disagree with any part of the Terms and Conditions, you may not use the site in any way or make a Booking.
                        All correspondence regarding customer service,yourBooking, these Terms and Conditions, or any other questions should be sent to <%=ContactDetails.Footer_Address_Updated %> or email to <a href="mailto:<%=ContactDetails.Email_Support %>"><%=ContactDetails.Email_Support %></a>
                                                    </li>

                                                    <h4>Your Contract</h4>
                                                    <li>A "Booking"is defined herein as any order for products or services you make on TravelMerry.com (the"Site").
                        Acceptance will be made by TravelMerry when we have received full payment from you and sent you a confirmation email (from either TravelMerry or the relevant Travel Supplier). All travel products and services featured on the Site are subject to availability.
                        Our prices are subject to change in real time. No claim relating to the price of a Booking will be considered once the contract is concluded.
                                                    </li>

                                                    <li>TravelMerry is solely acting as the agent for the third party Travel Suppliers such as airlines, hotels, motels, resorts,
                        and insurance companies ("Travel Suppliers" or "Suppliers"), made available through our Site. When you make a Bookingwith TravelMerry, and your preferred travel product or service is available,
                        your contract will be between the Travel Supplier and you. TravelMerry is not a party to the contractual relationship formed by you and the Travel Supplier, and is not responsible for the actions of thoseTravel Suppliers.
                                                    </li>

                                                    <h4>Travel Supplier’s Terms and Conditions: </h4>
                                                    <li><strong>All Bookings are subject to the Terms and Conditions of the Travel Suppliers providing the services offered on the Site.</strong> By placing an order with TravelMerry, you agree to abide by all the Terms and Conditions and relevant policies of the applicable Suppliers (including airline’s security policies, fare revision policies, flight schedule change policies, cancellation policies, refund policies, baggage allowance policies, pet policies, privacy policies and payment policies) without reservation, and to be bound by the limitations therein. Before you make any Booking on our Site, it is your responsibility to check and accept the policies of the all Travel Suppliers included in your Booking. User acknowledges that Travel Suppliers can cancel your ticketed or Booked itinerary in case they discover a violation of any of their relevant policies. If the Supplier’s Terms and Conditions are ever in conflict with the Terms and Conditions of TravelMerry, TravelMerry will control all issues relating to the liabilities and responsibilities of TravelMerry.</li>

                                                    <h4>Site Usage </h4>
                                                    <li>TravelMerry.comBookings are available for purchase by residents of the United States (excluding Hawaii) while in the United States, its territories, possessions, and protectorates, who have all the requisite power and authority to enter into and perform the obligations under these Terms and Conditions. Travelers must be over the age of 18 to make a Booking through TravelMerry.com. You agree that you are legally 18yrs of age and have legal authority to enter an agreement with us and to use our Site as per our Site’s Terms and Conditions. </li>
                                                    <li>Please read these Terms and Conditions carefully, ask us any questions, and consult an attorney for any clarifications before you agree to be bound by them. User acknowledges that they have taken note of these Terms and Conditions before making a Booking and have accepted the same by clicking on the "I have accepted the <a href="terms_conditions.html" target="_blank">Terms and Conditions</a> of the booking" check box on the bottom of the Booking submission page. When User is making a Booking for someone besides themselves, clicking on the "I have accepted the Terms and Conditions of the booking" check box verifies User has informed all other Travelers in their group of these Terms and Conditions, our privacy policy, the relevant Travel Suppliers Terms and Conditions and policies, and accepts them on their behalf, warranting that the other Travelers in their group, after consideration and with an opportunity to consult legal counsel, also agree to be bound by these Terms and Conditions to the same extent as User. Without this acceptance, the processing of aBooking is not technically possible. Therefore, by placing an order with TravelMerry, you, and all Travelers in your group (if applicable), agree to abide by these Terms and Conditions without reservation and to be bound by the terms and limitations herein.</li>

                                                    <li>User warrants that all information provided by you during Bookingis accurate and true. User agrees to be financially responsible for the Bookings theymake with TravelMerry, even if those Bookings are for third parties.</li>

                                                    <h4>Modification of Our Terms and Conditions </h4>
                                                    <li>Our Terms and Conditions may be amended or modified by us at any time, without notice, on the understanding that such changes will not apply to Bookings made prior to the amendment or modification. It is therefore essential that you consult and accept our Terms and Conditions at the time of making a Booking, particularly in order to determine which provisions are in operation at that time in case they have changed since the last time you placed an order with TravelMerry or reviewed our Terms and Conditions.</li>

                                                    <h4>Payment </h4>
                                                    <li>TravelMerry accepts payments through Master, Visa and American Express Card which can be either Debit/Credit Card. Your Booking is confirmed only when we receive full payment for the Booking and when we issue the ticket(s) for your Booking. </li>
                                                    <li>If we could not process your payment and take payment from your payment card account, we will send you an email with in 24 hours of your Booking. This could be because of insufficient funds in your payment card account, incorrect details provided by you on our site, any fraud alerts received from our fraud prevention system or if your bank declined the transaction. You can also contact your bank or card issuing company to authorize your transaction or provide us a different payment card acceptable by TravelMerry. We do not take responsibility for any damages caused by the non-acceptance of your payment card.</li>
                                                    <li>Fares may increase while we process your Booking. If there is an increase in the booked airfare, airline does not confirm your trip and decline your payment during the transaction. In such case, <strong>you can either cancel the Booking without any extra cost or purchase the ticket(s) at the increased fare.</strong></li>
                                                    <li>When a Booking is submitted, you hereby authorize that Travelmerry and/or its authorized Travel Suppliers may charge the payment card provided to us for the total amount of your Booking.</li>

                                                    <li>If we notice any fraudulent transaction from the provided credit or debit card, we will inform such activity to Credit Card Verification Company, bank, or airline, and if required we will also inform the appropriate legal authorities. We process all credit and debit card transactions securely and transmit the data using SSL.</li>
                                                    <li>TravelMerry does not accept P.O.Box as billing address and <strong>your billing address must be valid and verifiable.</strong> </li>
                                                    <li>We may request you to send us the <strong>credit card authorization</strong> form (completed and signed) along with additional documents to verify your identity and process the payment. This step must be completed by you with in <strong>24 hours</strong> of your Booking; otherwise TravelMerry has the right to cancel your Booking.</li>
                                                    <li>You are authorizing TravelMerry to perform pre-authorization verification by charging less than one US dollar on your payment card and get the confirmation of the same charge from you. This charge is temporary and will be refunded immediately.</li>
                                                    <li>You are agreeing to show the original payment card used to make this purchase to airport authorities or airlines staff during the check in or before your departure. It is your responsibility to check with airlines, if it is required to show the original payment card during the check in or not. TravelMerry is not liable if airlines refused to board you on any flight for not showing the original payment card used to purchase your tickets on our Site.</li>

                                                    <h4>Booking Service Fee</h4>
                                                    <li>When you do an online Booking on our site, TravelMerry includes a Booking service fee in the total price of the ticket. This fee is in between <strong>$0 to $25 per person</strong> for both domestic and international travel. </li>
                                                    <li>You can also make Bookings by contacting our call center agents and the service for such Bookings will be anywhere between <strong>$0 to $100 per person</strong> for both domestic and international travel.</li>
                                                    <li>TravelMerry has authority to change this fee without any notice. <strong>If the total reflected price on the site is NOT acceptable by you then you have the right to cancel the Booking and not to make any purchase.</strong></li>
                                                    <li>TravelMerry does not make any representations or promises in regards to the Booking Service Fees we charge. This fee is for Site usage and our Booking services. This Booking service fee charged for online Bookings or call center Bookings is <strong>not refundable.</strong></li>

                                                    <h4>Exchange Fee for Modifications and Cancellations </h4>
                                                    <li>If any itinerary permits the changes (cancellation or modification), TravelMerry reserve the right to charge an exchange fee of <strong>$100 per traveler</strong> in addition to the penalties of the Travel Suppliers and any difference in the fare/rate. It is your responsibility to contact our customer representative agents and find out the total cost for any changes. </li>

                                                    <h4>California and Illinois Residents only </h4>
                                                    <li>Upon cancellation of the transportation or travel services, where the Traveler is not at fault and has not canceled in violation of any terms and conditions previously clearly and conspicuously disclosed and agreed to by the Traveler, all sums paid to the seller of travel for services not provided will be promptly paid to the Traveler, unless the Traveler advises the seller of travel in writing, after cancellation. In California, this provision does not apply where the seller of travel has remitted the payment to another registered wholesale seller of travel or a carrier, without obtaining a refund, and where the wholesaler or provider defaults in providing the agreed-upon transportation or service. In this situation, the seller of travel must provide the Traveler with a written statement accompanied by bank records establishing the disbursement of the payment, and if disbursed to a wholesale seller of travel, proof of current registration of that wholesaler.</li>


                                                    <h4>Booking Reconfirmation </h4>
                                                    <li>It is your responsibility to review and reconfirm the Itinerary details (including each passenger’s legal name(first, middle and last name) as given in the passport, date of birth, title, travel dates, departure city, destination city, flight number, scheduled departure and arrival time) for the itinerary that you have booked on our site <a href="http://travelmerry.com/" target="_blank">www.travelmerry.com</a> or through our customer support agent. Please contact us with in 4hrs from your Booking time for any changes required on your Booking. After this time window, TravelMerry considers all your details are accurate and acceptable by you and without any liability on our part.</li>

                                                    <h4>Errors in Booking and ticketing: </h4>
                                                    <li>Our staff is committed to avoid any human errors during Booking and tickets process. It is very unlikely, but if any of our agents unknowingly makes a mistake during Booking or ticketing of your flight/Accommodations it is your responsibility to contact us with in 24 hours of receiving the confirmed itinerary or e-ticket, so that we can do our best to correct the issue as soon as we can. After 24 hours, we cannot correct any errors on your itinerary or e-ticket. If your Booking is canceled by mistake, we are committed to reimbursing a maximum of our Booking Service Fee for your Booking in addition to a $50 voucher which you can use within 12 months for your next Booking with TravelMerry.</li>

                                                    <h4>AIR TRANSPORT </h4>
                                                    <h4>Ticket delivery:</h4>
                                                    <li>Your purchase will be completed when the payment has been taken from your payment card and when we issue the e-ticket for your Booking. TravelMerry delivers the e-tickets to the email address that you provide to us during the Booking process. It is your responsibility to provide us a valid and personal email address for which you have access to it. </li>
                                                    <li>As of June 1st, 2008, the International Air Transport Association (IATA) imposed rules with regard to the issuing of air travel tickets. As of that date, travel agencies and airlines have an obligation to only issue travel tickets via electronic means (i.e. electronic ticket or "e-ticket"). Due to technical constraints to do with airline’s restrictions in relation to certain requirements (infants under the age of 2, inter-airline agreements, groups, etc.), it may be impossible to issue an electronic ticket. Therefore, though a flight may be shown as available, it might prove impossible for us to honor your reservation.This situation, which is outside our control, will not result in liability on our part.</li>
                                                    <li>We will deliver paper tickets in certain cases such as (but not limited to) required by law, required by airline, e-tickets system is down, technical issues to deliver an email from our end. TravelMerry will not take any responsibility in the delay of delivering your paper ticket. It is your responsibility to provide us with accurate mailing address to deliver these tickets. </li>

                                                    <h4>Best Price Guarantee Policy </h4>
                                                    <li>With in the 4hours of your Booking with us, if you can find a lower price for the same itinerary on reputed US based website and call our customer support agent so that they may verify that price, we will either: cancel your itinerary; provide you full refund; or refund you the fare difference. We need all the documentation from you to prove that all the elements of the lower offer, including, without limitation, airline, class, ticket type, fare, cancellation policy, departure and arrival dates and locations must match with itinerary which you booked on our Site. The lower rate fare must be available for Booking when you call our support center. If the carrier is not displayed on lower fare displayed web site during the fare search then it is not qualified under this policy. This policy applies when lower fare on other US based reputed website(in English language) is advertised and available for public to book.A lower price offered solely through another sellers call center will not be considered for this offer/ subject to our Best Price Guarantee Policy.</li>

                                                    <h4>24 Hours Cancellation Policy </h4>
                                                    <li>Once you confirm the booking, shortly you will receive a booking confirmation email from us. If you purchased ‘24 Hours Cancellation Policy’, you will be able to cancel your booking with in 24hours of the booking confirmation email. With this, you do not need to provide any reason or additional documentation when you contact us via email or by calling our support team to cancel your booking. After 24hours from booking confirmation or if you do not purchase this policy, additional cancellation or ticket change fee will apply. The fee details are provided under “Exchanges, Cancellation and Refund Policies” section of our terms and conditions.</li>

                                                    <h4>Exchange, Cancellation and Refund Policies </h4>
                                                    <li>TravelMerry offers non-refundable airfare only. In some instances, if you call our customer service agent prior to 72hrs of your departure time, some airlines may grant cancellation or refund on certain ticket types. If the relevant airline allows, TravelMerry can process your request to cancel your ticketed itineraries. Instead of refunds some airlines offer credit for future travel for the same passenger on their airline. Be aware there could be an expiration date for this travel credit and check with airlines directly regarding the terms of any credit offered. In this case, Airlines may also charge you a penalty and the fare difference for any changes (if allowed). These charges are in addition to TravelMerry’s $100 per passenger exchange fee.</li>
                                                    <li>TravelMerry reserves the right to determine the amount of refund value in case cancellation and modifications are permitted by airlines. Our decision is final on the refund value. Refunded amount will be funded back to the payment card which was used to complete the Booking. It will take 30-45 days to process refunds for all services. In certain cases, TravelMerry may transfer the refunded value to passenger’s bank account. This is subjected to security verifications.</li>
                                                    <li>There will be no refund on unutilized services. All cancellation and change fees are subjected to apply as stated in this policy document. </li>

                                                    <h4>Alteration Policy </h4>
                                                    <li><strong>All confirmed flight bookings are non-changeable</strong>.In some instances, TravelMerry can process change requests (Ex: change in travel dates, departure city, destination city, etc.) on confirmed tickets as long as these requests are acceptable by relevant airline’s and subjected to their policies. Not all change requests are allowed by airlines. If changes are permitted,User will be charge the airline’s penalty fee in addition to the fare difference and our <strong>$100 per passenger as change fee</strong>. Contact our customer service agents to find out the total cost for your request and the timeframe to complete the changes. </li>
                                                    <li>TravelMerry does not guarantee any changes will be accepted and does not accept any liability, if airlines do not accept your change request or it is not processed within the timeframe you have anticipated or require.</li>

                                                    <h4>No-Show or Unused </h4>
                                                    <li>Your ticket will be considered as a no-show ticket,if you neither turn up to catch a confirmed schedule flight before its scheduled departure time nor cancel the flight by calling the airline or our customer support center with in the allowable time window to cancel your trip. These no show tickets do not hold any value and not refundable.</li>
                                                    <li>TravelMerry and/or Airlines do not process any refunds if you partially use a ticket. All segments must be travelled by you. </li>

                                                    <h4>Flight Schedule Change or Cancellations by Airlines </h4>
                                                    <li>Depending on various reasons, airlines may change a flight schedule or to cancel it. TravelMerry has no control over these decisions made by airlines and is not liable for flight cancellation or flight schedule change (Date, time or both). Before your departure or during your trip, it is your responsibility to check with airlines regarding flight cancellation or flight schedule change relevant to your trip. TravelMerry makes all efforts to inform you about schedule changes or cancellations but we do not take any liability if you do not receive any alert from us.</li>
                                                    <li>Not all the tickets (Itinerary types and flight types) are refundable against flight cancellation within 4hrs prior to the scheduled departure. In such cases, TravelMerry does not accept any request from you to refund your ticket and is not liable to give you refund. You must review the relevant airline’s refund policy and contact the relevant airline for further help. </li>

                                                    <h4>Multiple Airline Itineraries </h4>
                                                    <li>Depending on the number of airlines included on your Itinerary, separate fare rules, terms & conditions and policies(Baggage allowance policy, baggage fee, refund policy etc) will apply by each airline. For any of your change, cancellation and refund requests may handle separately by each airline and different rules and penalties/fees apply by those airlines. If any of flight schedule changes or cancelled by airlines or by you then your return or connecting airline is not obligated to issue a refund or exchange or change your itinerary. It is your responsibility to comply with each airline’s terms &conditions and various other policies.</li>
                                                    <li>It is recommended to have a physical copy of all your e-ticket confirmations from all the airlines included in your confirmed itinerary. While entering into airport or during check in, you are responsible to show your all itinerary, tickets or both. </li>

                                                    <h4>Fare Changes </h4>
                                                    <li>All airline fares very dynamic and fares are subject to change in real time. TravelMerry does not take liability on airline fare changes and your Booking is not confirmed with the fare you have chosen until we issue the ticket for your Booking.</li>
                                                    <li>If there is an increase in the booked airfare, airline does not confirm your trip and may decline your payment during the transaction. In such case, you have right to cancel the Booking without any extra cost or purchase the ticket(s) at the increased fare. </li>

                                                    <h4>Baggage Policy and Fee </h4>
                                                    <li>Every airline has its own baggage policy which defines their baggage allowance for checked and cabin bags. Baggage fee is determined based on the number, weight and measurement of each checked bag. Baggage fee is directly payable to airlines. It is your responsibility to check the baggage allowance policy of each airline included on your confirmed itinerary. If multiple airlines are included on your trip and may have different baggage policies and fees that you need to check with relevant airlines.</li>
                                                    <li>Also it is your responsibility to check the baggage policy of each airline for prohibited items and acceptable items in your checked and carryon bags. You will be responsible for paying to the airline any additional charges for checked or overweight baggage, including, but not limited to, golf bags and oversized luggage. If you exceed the weight limit set by your airline, and excess weight is permitted, you must pay a supplement directly to the airline at the airport. </li>
                                                    <li>TravelMerry assumes no liability for any loss or damage to baggage or personal effects, whether in transit or during your trip. Your airline is liable to you for the baggage you entrust to it only up to the compensation contemplated in the international conventions and relevant statutes. In the event of damage, late forwarding, theft or loss of luggage, you should contact the your airline and declare the damage, absence or loss of your personal effects before leaving the airport, and then submit a declaration, attaching the originals of the following documents: the travel ticket, the baggage check-in slip, and the declaration. Some airlines may require additional info, it is your responsibility to follow the appropriate procedures. If traveling with valuable items, it is recommended that you take out an insurance policy covering the value of your items. </li>



                                                    <h4>Over Booking by Airlines </h4>
                                                    <li>It is unlikely but airline flights may be overbooked, in such case there is a possibility that seats may not be available for you for which you have a confirmed reservation. In this case, airlines try to accommodate the passengers by asking the passengers to volunteer to give up their reserved seat for compensation. If there are not enough volunteers, you may be denied boarding at the departure airport. Persons denied boarding involuntarily may be entitled to receive compensation from their airline. You can check with check-in counters and boarding locations for the airlines policies for the payment of compensation and boarding priorities. TravelMerry cannot be held liable for denied boarding.</li>

                                                    <h4>Frequent flyer reward policy </h4>
                                                    <li>Most of the airlines offer rewards program for frequent flyer and every airline have different policy for frequent flyer rewards program and these policies are out of our terms and conditions. It is your responsibility to check directly with the airline (or the airline which you have booked a trip) regarding their frequent flyer rewards program policy. We have no control on their policies.</li>

                                                    <h4>Optional Services offered by Airline </h4>
                                                    <li>Every airline may offer some optional services and they may charge extra for these services. The total fare provided by TravelMerry does not include these extra charges imposed by airlines for any optional service. It is your responsibility to check with airlines directly on their terms of service for these optional services and the charges for these services.</li>
                                                    <li>Your special requests like seat, meals, frequent flyer, wheel chair, bassinet and other requests are requests only and these are subjected to availability. Airlines reserve the right to make changes to allocated seats and also check with airlines directly on all your special requests. TravelMerry does not guarantee and does not accept any liability for any special requests you made through our Site. </li>

                                                    <h4>Unaccompanied Minors </h4>
                                                    <li>TravelMerry does not issue tickets directly to unaccompanied minors (age 18 or under). It is your responsibility to check with airlines directly regarding their policies and regulations for children travelling without adult supervision.</li>

                                                    <h4>Visa and Entry Requirements </h4>
                                                    <li>It is User’s responsibility to verify that they (including infants)have all the necessary visas, passport, and vaccinations prior to travel. A full and valid passport is required for all persons traveling to any of the destinations outside the U.S. that we feature. You must obtain and have possession of a valid passport, all visas, permits and certificates, and vaccination certificates required for your entire Trip. </li>
                                                    <li>Most international travel requires a passport valid until at least six (6) months beyond the scheduled end of your Itinerary.  Non-U.S. citizens should contact the appropriate consular office for any requirements pertaining to their Tour.  Further information on entry requirements can be obtained from the State Department, by phone (202) 647-5335 or access online at <a href="http://travel.state.gov/travel" target="_blank">http://travel.state.gov/travel</a> or directly from the destination country's website.</li>
                                                    <li>Some countries require you to be in possession of a return ticket or exit ticket and have sufficient funds, etc. Similarly, certain countries require that the Traveler produce evidence of insurance/repatriation coverage before it will issue a visa. </li>
                                                    <li>You must carefully observe all applicable formalities and ensure that the surnames, middle names and forenames used for all passengers when making a booking and appearing in your travel documents (booking forms, travel tickets, vouchers, etc.), correspond exactly with those appearing on your passport, visas, etc. </li>
                                                    <li>Immunization requirements vary from country to country and even region to region.  Up-to date information should be obtained from your local health department and consulate.  You assume complete and full responsibility for, and hereby release TravelMerry from, any duty of checking and verifying vaccination or other entry requirements of each destination, as well as all safety and security conditions of such destinations during the length of the proposed travel or extensions expected or unexpected.  For State Department information about conditions abroad that may affect travel safety and security, go to <a href="http://travel.state.gov/travel/travel_1744.html" target="_blank">http://travel.state.gov/travel/travel_1744.html</a>, or contact them by phone at (202) 647-5335. For foreign health requirements and dangers, contact the U.S. Centers for Disease Control (CDC) at (404) 332-4559, use their fax information service at (404) 332-4565, or go to <a href="http://wwwnc.cdc.gov/travel/" target="_blank">http://wwwnc.cdc.gov/travel/</a>.</li>
                                                    <li>It is your responsibility to ensure that you hold the correct, valid documents for the countries you are visiting and have obtained the necessary vaccinations, clearance to travel, and hold the necessary confirmations for medications required as we cannot be held liable for any illness, delays, compensation, claims and costs resulting from your failure to meet these requirements. </li>
                                                    <li>WE CANNOT ACCEPT RESPONSIBILITY IF YOU ARE REFUSED PASSAGE ON ANY AIRLINE, TRANSPORT OR ENTRY INTO ANY COUNTRY DUE TO THE FAILURE ON YOUR PART TO CARRY OR OBTAIN THE CORRECT DOCUMENTATION. IF FAILURE TO DO SO RESULTS IN FINES, SURCHARGES, CLAIMS, FINANCIAL DEMANDS OR OTHER FINANCIAL PENALTIES BEING IMPOSED ON US, YOU WILL BE RESPONSIBLE FOR INDEMNIFYING AND REIMBURSING US ACCORDINGLY. </li>

                                                    <h4>Travel Risks </h4>
                                                    <li>Although most travel to destinations made available through the Site is completed without incident, travel to certain areas may involve greater risk than others. You assume sole responsibility for your own safety at any destination traveled to. TravelMerry does not guarantee your safety at any time, and assumes no responsibility for gathering and/or disseminating information for you relating to risks associated with your destinations. BY OFFERING OR FACILITATING TRAVEL TO CERTAIN DESTINATIONS, WE DO NOT REPRESENT OR WARRANT THAT TRAVEL TO SUCH POINTS IS ADVISABLE OR WITHOUT RISK, AND WE SHALL NOT BE LIABLE FOR DAMAGES OR LOSSES THAT MAY RESULT FROM TRAVEL TO SUCH DESTINATIONS. </li>

                                                    <h4>Discount Fares </h4>
                                                    <li>According to the US Airline Deregulation Act, Open Sky Agreement, and other acts/codifications, it is legal to offer discounted airfares. Because of our Travel Supplier’s huge inventory and our large volume, we are allowed to provide you many published air fares for less than the carriers published fares. The discount value may be changed based on carrier, availability, fare type, seasonality, destinations and referral source. These discounts may continuously vary/change.</li>

                                                    <h4>Hazardous Materials </h4>
                                                    <li>Federal law strictly prohibits carrying any hazardous material in your checked baggage, cabin bags, carrying on bags or on person. Any violation can result in 5yrs of imprisonment and penalty of$250,000 or more (49U.S.C 5124). Hazardous materials include explosives, flammable liquids and solids, compressed gases, poisons, oxidizers, corrosives and radioactive materials, including paints, fireworks, tear gases, lighter fluid, radio-pharmaceuticals and oxygen bottles. Check this link for detail information
                        <a href="http://www.faa.gov/about/office_org/headquarters_offices/ash/ash_programs/hazmat/passenger_info/" target="_blank">http://www.faa.gov/about/office_org/headquarters_offices/ash/ash_programs/hazmat/passenger_info/</a>
                                                    </li>

                                                    <h4>APIS </h4>
                                                    <li>All airlines are required to collect Advance Passenger Information (API) from passengers before travel to or from the USA and certain other countries. You agree to supply this information to TravelMerry and consent to TravelMerry passing this information to the airlines who may then disclose it to foreign authorities as necessary. If you do not supply Advance Passenger Information at the time of reservation, you may be refused to entry to these countries. Many airlines require complete passport information before issuance of tickets. It is very important that this information is accurate so that you do not have any delay when you pass through immigration on arrival in these countries. The information you will be asked to provide will depend on the country you are visiting, and can include passport information, city and country of residence and destination address for all travelers on your Booking. If you are traveling to a country that requires API, TravelMerry will ask for the information after completing your reservation. It is Users responsibility to provide us the requested information on time. If User is not able to provide the requested information, you will be charged the airline’s cancellation charge in addition to our cancellation fee described herein.</li>

                                                    <h4>Non-Use of Flight Segments </h4>
                                                    <li>You agree not to purchase a ticket or tickets containing flight segments that you will not be using, such as a "point-beyond", "hidden-city", or "back-to-back tickets". You further agree not to purchase a round-trip ticket that you plan to use only for one-way travel. You acknowledge that the airlines generally prohibit all such tickets, and therefore we do not guarantee that the airline will honor your ticket or tickets. You agree to indemnify TravelMerry against any airline claims for the difference between the full fare of your actual itinerary and the value of the ticket or tickets that you purchased. </li>

                                                    <h4>Babies and Infants </h4>
                                                    <li>Babies (up to 2 years of age) do not occupy a seat; the price of the ticket is generally 10% of the official rate. On certain flights children (from 2 to 11 years of age) may be granted a reduction, except on charter flights. Always check the relevant airlines for their policies before booking.</li>

                                                    <h4>Pregnancy </h4>
                                                    <li>Different airlines have their own restrictions on when pregnant woman may fly on their plane, which can range from prohibiting flying anywhere from 7 to 30 days by the due date. It is your responsibility to check the restrictions of your particular airline. If you are denied boarding, TravelMerry will not be responsible for any resulting cancellation fees and charges.</li>

                                                    <h4>LIABILITY LIMITATIONS, DISCLAIMERS, AND INDEMNIFICATION </h4>
                                                    <li>WE DO NOT WARRANT OR REPRESENT THAT THE CONTENT OF THIS WEBSITE IS ACCURATE, UP-TO-DATE OR COMPLETE, NOR THAT IT DOES NOT INFRINGE THE RIGHTS OF OTHERS. WE ARE PROVIDING THIS WEBSITE AND ITS CONTENTS ON AN "AS IS" BASIS. WE MAKE NO REPRESENTATIONS OR WARRANTIES OF ANY KIND WITH RESPECT TO THE WEBSITE, ITS CONTENTS OR ANY OF THE PRODUCTS OR SERVICES SUPPLIED THROUGH THIS WEBSITE. TO THE MAXIMUM EXTENT PERMITTED BY LAW, WE DISCLAIM ALL IMPLIED REPRESENTATIONS AND WARRANTIES INCLUDING, WITHOUT LIMITATION, IMPLIED WARRANTIES THAT THE PRODUCTS AND SERVICES OFFERED AND SUPPLIED THROUGH THIS WEBSITE WILL BE OF MERCHANTABLE QUALITY, FIT FOR ANY PURPOSE OR WILL COMPLY WITH ANY DESCRIPTIONS ON THIS WEBSITE OR SAMPLES.</li>
                                                    <li>We do not represent or warrant that this website, the server that makes it available or any of our products or services supplied through this website will be free of errors, viruses or defects. Your access and use of this website is subject to factors beyond our control. We do not warrant that this website or the products and services offered via this website will meet your requirements or that the service will be uninterrupted or timely. We will use our best endeavors to make this website secure and have implemented technology for this purpose. However, because of the nature of the internet, we do not warrant that this Website will be secure.</li>
                                                    <li>TO THE EXTENT PERMITTED BY LAW, YOU RELEASE US FROM ALL LIABILITY, COST, DAMAGES, CLAIMS AND EXPENSES (INCLUDING DIRECT, INDIRECT, SPECIAL AND CONSEQUENTIAL LOSS OR DAMAGE WHETHER IN NEGLIGENCE OR OTHERWISE) ARISING OUT OF THE SUPPLY OR FAILURE TO SUPPLY OR USE OR NON-USE OF THE THIRD PARTY PRODUCTS OR SERVICES. </li>
                                                    <li>To the maximum extent permitted by law, neither we nor any of our officers, employees, shareholders or other representatives will be liable in damages or otherwise in connection with your use of or inability to access this website or the purchase and use of any products and services supplied via this website or any breach of any warranties that may be implied by law. This limitation of liability applies to all damages of any kind, including compensatory, direct, indirect or consequential damages, loss of data, income or profit, loss of or damage to property, personal injury and claims of third parties. In the event that our website fails to operate or causes you loss or damage, your sole remedy is the refund any money that you paid to us to use this website.</li>
                                                    <li>IN NO EVENT SHALL TRAVELMERRY BE LIABLE FOR ANY CONSEQUENTIAL, INDIRECT, EXEMPLARY, SPECIAL, INCIDENTAL OR PUNITIVE DAMAGES OF ANY KIND, INCLUDING WITHOUT LIMITATION, DAMAGES FOR ANY LOSS OF OPPORTUNITY OR OTHER PECUNIARY LOSS, EVEN IF TRAVELMERRY HAS BEEN ADVISED OF THE POSSIBILITY OR PROBABILITY OF SUCH DAMAGES OR LOSSES, WHETHER SUCH LIABILITY IS BASED UPON CONTRACT, TORT, NEGLIGENCE OR OTHER LEGAL THEORY. IN NO EVENT SHALL TRAVELMERRY’ TOTAL AGGREGATE LIABILITY TO THE TRAVELER FOR CLAIMS ARISING UNDER THIS AGREEMENT EXCEED THE TOTAL AMOUNTS PAID BY THE TRAVELER TO TRAVELMERRY IN FEES UNDER THIS AGREEMENT.</li>
                                                    <li>TRAVELMERRY IS ACTING AS A MERE AGENT FOR ALL SUPPLIERS OF SERVICES AND CONVEYANCES ADVERTISED AND/OR SOLD BY US. ANY AND ALL SUPPLIERS OF SERVICES AND CONVEYANCES ADVERTISED AND/OR SOLD BY TRAVELMERRY ARE THIRD PARTY VENDORS AND TRAVELMERRY RETAINS NO OWNERSHIP INTEREST, MANAGEMENT, OR CONTROL OF THOSE THIRD PARTY VENDORS. TO THE FULLEST EXTENT PERMITTED BY LAW, TRAVELMERRY DOES NOT ASSUME LIABILITY FOR ANY INJURY, DAMAGE, DEATH, LOSS, ACCIDENT OR DELAY DUE TO AN ACT OR OMISSION OF ANY THIRD PARTIES (INCLUDING THIRD PARTY VENDORS), GOVERNMENTAL AUTHORITY, OR ACTS ATTRIBUTABLE TO YOU YOURSELF, INCLUDING, WITHOUT LIMITATION, NEGLIGENT OR RECKLESS ACTS, EVEN IF TRAVELMERRY HAS BEEN ADVISED THAT SUCH DAMAGES WERE POSSIBLE OR PROBABLE. </li>

                                                    <h4>Indemnification and Release </h4>
                                                    <li>Except as otherwise set forth herein, User hereby releases TravelMerry from any and all liability, loss, expense, damages, or claims arising out of or resulting from User’s Booking or their use of the Site, whether caused by the negligent or reckless conduct of User, a Supplier, a third party, or otherwise.</li>
                                                    <li>User hereby also agrees to indemnify, defend and hold harmless TravelMerry our affiliates, officers, employees and agents against all losses, costs, damages, claims and expenses (including reasonable attorneys' fees) arising from: </li>
                                                    <li>Any breach of these Terms and Conditions by you;</li>
                                                    <li>Any act or omission by you or an officer, employee or agent of you;</li>
                                                    <li>Any claim, action, demand or proceeding by a third party against us or our officers, employees or agents caused or contributed to by you or an employee or agent of you. </li>

                                                    <h4>DISPUTES </h4>
                                                    <h4>Binding Arbitration, Governing Law, Jurisdiction, Venue, etc:</h4>
                                                    <li>These Terms and Conditions and the relationship between You and TravelMerry will be governed by the laws of the State of Georgia without regard to its conflict of law provisions. </li>
                                                    <li>You and TravelMerry shall attempt in good faith to resolve any dispute concerning, relating, or referring to a Booking, our Privacy Policy, Credit Card charges, TravelMerry’ website, any literature or materials concerning TravelMerry, and these Terms and Conditions or the breach, termination, enforcement, interpretation or validity thereof, (hereinafter a "Dispute") through preliminary negotiations. The parties shall use their best efforts to settle the dispute, claim, question, or disagreement. To this effect, they shall consult and negotiate with each other in good faith and, recognizing their mutual interests, attempt to reach a just and equitable solution satisfactory to both parties. If they do not reach such solution within a period of 60 days, then, upon noticeby either party to the other, all disputes, claims, questions, or differences shall be finally settled by arbitration in Atlanta, GA, administered by the American Arbitration Association in accordance with the provisions of its Commercial Arbitration Rules, and judgment on the award rendered by the arbitrator(s) may be entered in any court having jurisdiction thereof.</li>
                                                    <li>In the event a party fails to proceed with arbitration the other party is entitled of costs of suit including a reasonable attorney’s fee for having to compel arbitration. Nothing herein will be construed to prevent any party’s use of injunction, and/or any other prejudgment or provisional action or remedy. Any such action or remedy shall act as a waiver of the moving party’s right to compel arbitration of any dispute.</li>
                                                    You and TravelMerry agree to submit to the personal jurisdiction of the federal and state courts located in Fulton County, GA with respect to any legal proceedings that may arise in connection with, or relate to, our Binding Arbitration clause and/or a Dispute. The Client and TravelMerry agree the exclusive venue for any and all legal proceedings that may arise in connection with, or relate to, our Binding Arbitration clause and/or a Dispute, shall be the federal and state courts located in Washington, DC and to irrevocably submit to the jurisdiction of any such court in any such action, suit or proceeding and hereby agrees not to assert, by way of motion, as a defense or otherwise, in any such action, suit or proceeding, any claim that (i) he, she or it is not subject personally to the jurisdiction of such court, (ii) the venue is improper, or (iii) this agreement or the subject matter hereof may not be enforced in or by such court. YOU RECOGNIZE, BY AGREEING TO THESE TERMS AND CONDITIONS, YOU AND TRAVELMERRY ARE EACH WAIVING THE RIGHT TO A TRIAL BY JURY OR TO PARTICIPATE IN A CLASS ACTION WITH RESPECT TO THE CLAIMS COVERED BY THIS MANDATORY BINDING ARBITRATION PROVISION.</li>

                    <h4>Attorney’s Fees, Costs, and Expenses of Suit:  </h4>
                                                    <li>If any act of law or equity, including an action for declaratory relief or any Arbitration Proceeding, is brought to enforce, interpret or construe the provisions of these Terms and Conditions, our Privacy Policy, TravelMerry’ website or any literature or materials concerning TravelMerry, the prevailing party shall be entitled to recover actual reasonable attorney’s fees, costs, and expenses.</li>

                                                    <h4>MISCELLANEOUS  </h4>
                                                    <h4>Insurance: </h4>
                                                    <li>TravelMerry strongly recommends that all Users purchase some form of Travel, Cancellation, Lost Baggage and/or Medical Emergency Insurance for all travel. User acknowledges that it is User’s responsibility to understand the limitations of their insurance coverage and purchase additional insurance as needed. It is the User’s sole responsibility to research, evaluate and purchase appropriate coverage. User agrees that TravelMerry is not responsible for any uninsured losses.</li>

                                                    <h4>Notices </h4>
                                                    <li>Any notices required or permitted hereunder shall be given: </li>
                                                    <li>a. If to TravelMerry, via certified mail, return receipt requested, to the address listed above. Notice may also be sent via email to: <a href="mailto:<%=ContactDetails.Email_Support%>" target="_blank"><%=ContactDetails.Email_Support%></a></li>
                                                    <li>b. If to User, at the email or physical address provided by User during the booking process.</li>
                                                    <li>c. Such notice shall be deemed given: upon personal delivery; if sent by electronic mail, upon confirmation of receipt; or if sent by certified or registered mail, postage prepaid, three (3) days after the date of mailing. </li>

                                                    <h4>Privacy Policy </h4>
                                                    <li>The terms of the TravelMerry privacy policy are incorporated into these Terms and Conditions. You agree to the use of personal information by TravelMerry and its affiliates or third party suppliers in accordance with the terms of and for the purposes set forth in the TravelMerry privacy policy.</li>

                                                    <h4>Confidentiality </h4>
                                                    <li>By accepting our Terms, you acknowledge that any liability arising form you sharing the details of your Booking with any third party is yours alone. TravelMerry will not be liable if any third party misuses Booking information shared by you. You are considered liable for loses occurred in such cases.</li>

                                                    <h4>Force Majeure </h4>
                                                    <li>TravelMerry shall not be responsible for failure to perform any of its obligations under this Agreement during any period in which such performance is prevented or delayed due to Force Majeure. "Force Majeure" refers to any event beyond TravelMerry’ reasonable control, including but not limited to severe weather, fire, flood, mudslides, earthquakes, war, labor disputes, strikes, epidemics, World Health Organization’s advisories and/or alerts, Center for Disease Control’s advisories and/or alerts, U.S. State Department’s advisories and/or alerts, any order of any local, provincial or federal government authority, interruption of power Services, terrorism or any other causes beyond the control of TravelMerry or deemed by TravelMerry to constitute a danger to the safety and well-being of Users.</li>

                                                    <h4>Data Security </h4>
                                                    <li>To prevent payment card fraud and to make sure the payment card is being used with your consent, we perform security checks based on the data you provided to us during the Booking process on our Site. By accepting these Terms and Conditions, you are here by authorizing TravelMerry to carry out such security checks. You understand and acknowledge that the personal information provided to us can be disclosed to registered credit agencies which may keep a record of such information partially or entire information. This check is to verify your identity and to avoid any fraud. This security will not perform a credit check and your credit rating will not be affected with this security check. We process your data securely as per data protection act.</li>

                                                    <h4>User Obligations </h4>
                                                    <li>You agree to be bound by the following obligations, including without limitation:</li>
                                                    <li>You accept financial responsibility for all transactions made under your name or account.</li>
                                                    <li>You must be 18 years of age or over and have legal capacity.</li>
                                                    <li>You warrant that all information you provide about yourself or members of your household shall be true and accurate. </li>
                                                    <li>You will not use, or caused to be used, the Site for speculative, false or fraudulent Bookings. The Site and any content may not be modified, copied, transmitted, distributed, sold, displayed, licensed or reproduced in any way by you, except that one copy of the information contained within the Site may be made for personal, non-commercial use. </li>

                                                    <h4>Denial of Access </h4>
                                                    <li>TravelMerry reserves the right to deny access to the Site at any time without notice.</li>

                                                    <h4>Links to Third Party Web Sites </h4>
                                                    <li>The Site may contain hyperlinks to external web sites owned and operated by third parties. TravelMerry has no control over or association with such third party sites and no responsibility in relation to the accuracy, completeness and quality of the information contained within them. Any and all contents on these external web sites do not reflect products, services or information provided by TravelMerry. You should direct any concerns regarding any external link to the site administrator or webmaster of such site.</li>

                                                    <h4>Information on the Site </h4>
                                                    <li>TravelMerry offers all of the general information on the site for purposes of guidance only. Please note that TravelMerry may at any time change any aspect of the site or its content, including the availability of any suppliers, features, information, database or content. You need to check with the relevant Travel Supplier, destination, embassy, or tourist office to confirm the guidance is up to date. In particular, with respect to passports, visas, and vaccination requirements, TravelMerry does not guarantee that the information is always up to date and it is your responsibility to ensure that you understand and comply with all relevant passport, visa, and vaccination requirements. TravelMerry does not guarantee that information on the site (including without limitation prices, descriptions or dates) is free from errors or omissions but we will use all reasonable endeavors to correct any errors or omissions as soon as practicable once they have been brought to our attention.</li>

                                                    <h4>Feedback and Complaints relating to your Booking </h4>
                                                    <li>Please contact our customer services team, if you have any other enquiries or complaints relating to yourBooking prior to departure. If you have a complaint relating to a Travel Supplier, please ensure that you officially log your complaint with the relevant Travel Supplier prior to your return. If you have any other enquiries or complaints relating to your Booking, please contact our customer services team on your return and no later than 30 days after it giving your Booking reference and all other relevant information. If we or any of our Travel Supplier's requires further information you must supply that promptly in writing. This simple procedure must be followed as we and our Travel Supplier's need to be able to investigate the problem and, where possible, rectify it quickly.</li>

                                                    <h4>Seller of Travel Information </h4>
                                                    <li>California: CST#2115700-50. REGISTRATION AS A SELLER OF TRAVEL DOES NOT CONSTITUTE APPROVAL BY THE STATE OF CALIFORNIA. CALIFORNIA LAW REQUIRES CERTAIN SELLERS OF TRAVEL TO HAVE A TRUST ACCOUNT OR BOND. Vyoma Travels Inc HAS A BOND ISSUED BY HARTFORD FIRE INSURNACE COMPANY IN THE AMOUNT OF $10,000. Vyoma Travels LLC IS NOT A PARTICIPANT IN THE TRAVEL CONSUMER RESTITUTION FUND (TCRF). For any question related to refunds, Please contact our Customer Service at  California: CST#2115700-50. REGISTRATION AS A SELLER OF TRAVEL DOES NOT CONSTITUTE APPROVAL BY THE STATE OF CALIFORNIA. CALIFORNIA LAW REQUIRES CERTAIN SELLERS OF TRAVEL TO HAVE A TRUST ACCOUNT OR BOND. Vyoma Travels LLC HAS A BOND ISSUED BY HARTFORD FIRE INSURNACE COMPANY IN THE AMOUNT OF $10,000. Vyoma Travels LLC IS NOT A PARTICIPANT IN THE TRAVEL CONSUMER RESTITUTION FUND (TCRF). For any question related to refunds, Please contact our Customer Service at <a href="tel:<%=ContactDetails.Phone_support %> "><%=ContactDetails.Phone_support %> </a></li>
                                                    <li>Iowa:SOT# 1160<br>
                                                        FL: SOT# ST39170<br>
                                                        WA: SOT# 603373092 </li>

                                                    <h4>Assignment </h4>
                                                    <li>User may not assign his rights or obligations hereunder without the prior written consent of TravelMerry. </li>

                                                    <h4>Severability and Survivability </h4>
                                                    <li>If any provision, or portion of a provision, in these Terms and Conditions shall be unlawful, void, or for any reason unenforceable, then that provision shall be deemed severable and shall not affect the validity and enforceability of any remaining provisions. User and TravelMerryagree to substitute for such provision a valid provision which most closely approximates the intent and economic effect of such severed provision. </li>
                                                    <li>Notwithstanding any other provisions of this these Terms and Conditions, or any general legal principles to the contrary, any provision of these Terms and Conditions that imposes or contemplates continuing obligations on a party will survive the expiration or termination of these Terms and Conditions. </li>

                                                    <h4>Entire Agreement, Waiver, Etc </h4>
                                                    <li>These Terms and Conditions constitute the entire understanding and agreement of the parties with respect to the subject matter covered by them, and supersede all prior and contemporaneous understandings and agreements, whether written or oral, with respect to such subject matter. No terms contained on any proposal, purchase order, acknowledgment or other document will be effective with respect to affecting the terms hereof. No delay or failure by either party to exercise or enforce at any time any right or provision hereof will be considered a waiver thereof of such party's rights thereafter to exercise or enforce each and every right and provision hereof. No single waiver will constitute a continuing or subsequent waiver. TravelMerrydoes not guarantee it will take action against all breaches of these Terms and Conditions. No waiver, modification or amendment of any provision hereof will be effective unless it is in a writing signed by both the parties.</li>

                                                    <li>**Terms and Conditions have been updated on 06/12/2016.</li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div class="pm-error warning" id="TermsErrors" runat="server" style="display: none">
                                        </div>
                                        <p class="mb-10">
                                            <input name="chkTravelDoc" id="chkTravelDoc" data-rule-required="true" data-msg-required="Please confirm that you have all the travel document with you to travel" class="checkbox-custom" type="checkbox">
                                            <label for="chkTravelDoc" class="checkbox-custom-label">I/We have all the travel required documents, visa and transit visa to travel.</label>
                                        </p>

                                        <p>
                                            <input name="chkterms" id="chkterms" class="checkbox-custom" type="checkbox" data-rule-required="true" data-msg-required="Please accept our terms and conditions" onclick="_tmtrack()" />
                                            <%--<input id="Accepted" class="checkbox-custom" name="Accepted" type="checkbox">--%>
                                           <label for="chkterms" class="checkbox-custom-label">By clicking the button, you agree to our <a href="Termsandconditions.aspx" target="_blank" class="blue">Terms and Conditions</a> including refund and cancellation policy of this booking.</label>
                                        </p>
                                    </div>
                                </div>

                                <div class="pm-footer" id="tripsummery" runat="server">
                                    <div class="pm-error warning" id="Errors" runat="server" style="display: none">
                                    </div>
                                </div>



     
<div class="pm-content">
    <div class="protect-row-bg">
	<p class="mt-10"><i class="fa fa-lock"></i> We use secure transmission and encrypted storage to protect your personal information.</p>
        <p class=" font12 ml-25"> This payment will be processed in the U.S. This does not apply when the travel provider (airline/hotel) processes your payment.</p>
   </div>
</div>







                                <div class="pm-footer">
                                    <div class="pm-content">
                                        <p>
                                            <strong>Note:</strong> The next screen could be <a class="" data-toggle="modal" data-target="#3DSecure">
                                                <i class="fa fa-lock"></i> 3D Secure</a> page from Veryfied by VISA or Master card secure. 
                                        </p>
                                        <div class="modal fade" id="3DSecure" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header modal-header-success">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                        <h3 class="text-white"><i class="fa fa-lock"></i>3D Secure</h3>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="threeD-table">
                                                            <div class="threeD-logo-cell">
                                                                <img src="Design/images/vbv_small_logo.gif" alt="Verified by Visa">
                                                            </div>
                                                            <div class="threeD-text-cell">
                                                                <strong>Verified by Visa (VBV)</strong> is a new service from VISA that lets you shop securely online with your existing Visa Card. Usable only on Verified by Visa sites, this service through a simple checkout process, confirms your identity when you make purchases.
                                                            </div>
                                                        </div>

                                                        <div class="threeD-table">
                                                            <div class="threeD-logo-cell">
                                                                <img src="Design/images/securecode_small_logo.gif" alt="MasterCard SecureCode">
                                                            </div>
                                                            <div class="threeD-text-cell">
                                                                <strong>MasterCard® SecureCode™</strong> is an easy to use, secured online payment service that lets you shop securely online with your MasterCard® Card. This service through a simple checkout process, confirms your identity when you make purchases on the Internet. 
                                                            </div>
                                                        </div>

                                                        <div class="threeD-table">
                                                            <div class="threeD-logo-cell">
                                                                <img src="Design/images/amex_logo.jpg" alt="American Express Cards">
                                                            </div>
                                                            <div class="threeD-text-cell">
                                                                <strong>American Express Cards</strong> With American Express Card just shop online as you normally would and then enter your correct card information and billing address. We would validate the billing address that you provide with our records and authenticate your transaction. It’s that simple and helps in safeguarding your online transaction.
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            

                        </div>

                        <div class="col-md-3 col-sm-3 col-xs-12">
                            <div id="stick" class="aside stickem">


                                <div class="faresummery-wrap" id="divPriceDetails1" runat="server">
                                </div>
                                <div class="modal fade" id="CheapestFare" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                    <div class="modal-dialog">
                                        <div class="modal-content modal-lw">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                                <h4 class="modal-title" id="myModalLabel">Save $200 <span class="refCode">Reference Code: TM200</span></h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                                        <div class="">
                                                            <div class="cp-deparHeading">Depart:</div>
                                                            <div class="cp-Seg-A">
                                                                <div class="cpCityName">Hyderabad</div>
                                                                <div class="cpCityCode">HYD </div>
                                                                <div class="cpTime">22:55 </div>
                                                                <div class="cpDate">Sep 26,2017</div>
                                                            </div>

                                                            <div class="cp-Seg-B">
                                                                <div class="cpCityName">New York </div>
                                                                <div class="cpCityCode">JFK </div>
                                                                <div class="cpTime">07:25 </div>
                                                                <div class="cpDate">Oct 26,2017</div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                                        <div class="retunMrg">
                                                            <div class="cp-deparHeading">Return:</div>
                                                            <div class="cp-Seg-A">
                                                                <div class="cpCityName">New York</div>
                                                                <div class="cpCityCode">JFK </div>
                                                                <div class="cpTime">07:25 </div>
                                                                <div class="cpDate">Oct 26,2017</div>
                                                            </div>

                                                            <div class="cp-Seg-B">
                                                                <div class="cpCityName">Hyderabad</div>
                                                                <div class="cpCityCode">HYD </div>
                                                                <div class="cpTime">22:55 </div>
                                                                <div class="cpDate">Oct 26,2017</div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="modal-footer">
                                                <div class="PhIcon">
                                                    <img src="Design/images/phoneIcon.png" alt="Phone Icon">
                                                </div>
                                                <div class="contactNum">
                                                    <p class="callText">Call for Our Best Deals!</p>
                                                    <p class="CallNum"><a href="tel:<%=ContactDetails.Phone_support %> ">"><%=ContactDetails.Phone_support %>"></a></p>
                                                </div>

                                                <div class="contactimg">
                                                    <img src="Design/images/contactusImg.png" alt="">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="securePay-wrap">
                                <div class="securePay-head">
                                    Secure Payment
                                </div>
                                <div class="securePay-body">
                                    <img src="Design/images/visaMasterAmExp-Cards.png" alt="Secure Payment">
                                    <img src="Design/images/secure-payment-bg.png" alt="secure images">
                                </div>
                                <div class="secured-logo-body">
                                    <div class="astaImg">
                                        <img src="Design/images/ASTA-logo.png" alt="ASTA Logo" title="ASTA">
                                    </div>
                                    <div class="arcImg">
                                        <img src="Design/images/ARC-logo.png" alt="ARC" title="ARC">
                                    </div>
                                    <div class="iatanImg">
                                        <img src="Design/images/iatan_small_logo.gif" alt="IATA Logo" title="IATA">
                                    </div>
                                    <div class="nortonImg">
                                        <script type="text/javascript" src="https://seal.websecurity.norton.com/getseal?host_name=www.travelmerry.com&amp;size=XS&amp;use_flash=NO&amp;use_transparent=NO&amp;lang=en"></script>
                                    </div>
                                    <div class="trustwaveImg">
                                        <script type="text/javascript" src="https://sealserver.trustwave.com/seal.js?code=5941599fa4cb444499bbcb97a21fae82"></script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </asp:Panel>
        <asp:Panel ID="pnl3DSecure" runat="server" Visible="false">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <iframe id="IFPayment" width="100%" height="630px" frameborder="0" name="I1" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </asp:Panel>




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
                            <h3 class="red">Timed Out</h3>

                            <p><strong>Still interested in flying from San Francisco to Hyderabad?  </strong></p>
                            <p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>

                            <a href="Index.aspx" class="btn button newSearchbtn mr-15">New Search </a>
                            <button type="submit" class="btn button refreshBtn" onclick="ResetSession()" id="RefreshExpired">Refresh  </button>
                            <h5 class="red">Your search results have expired!</h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>




        <%-- <div id="overlay-giftbg">--%>
        <div class="giftWraper-main">
            <div class="modal fade in success-popup " id="poppromo1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="giftVocher-wrap">
                                <div class="giftVocher-content">
                                    <button type="button" class="close" data-dismiss="modal" onclick="Closeprom()" aria-hidden="true">×</button>
                                    <div class="logo">
                                        <img src="Design/images/logo.png" alt="TravelMerry">
                                    </div>
                                    <h1>Gift Voucherr</h1>
                                    <p>Exclusively discount voucher for you</p>
                                    <div id="clossepop" runat="server">
                                        <%--<div class="discountPrice">
                                            <h3>$5 <span>OFF</span></h3>
                                        </div>
                                        <div class="giftVocher-bg" >
                                            <h5>Use Promocode: <span>TM0005</span> </h5>
                                        </div>--%>
                                    </div>
                                    <div class="giftVocher-btn-wrap">
                                        <span>
                                            <button name="No Thanks" value="No Thanks" id="" onclick="Closeprom()" class="btn nothxButton" type="button">No Thanks! </button>
                                        </span>
                                        <span>
                                            <button name="Accept" value="Accept" onclick="addpromo()" id="" class="btn acceptButton" type="button">Accept</button>
                                        </span>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</div>--%>
        <div class="modal fade in success-popup" id="poppromo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" runat="server">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <button type="button" class="close" aria-hidden="true" onclick="Closeprom()">×</button>
                        <h4 class="modal-title" id="myModalLabel">Session Expired </h4>
                    </div>
                    <div class="modal-body text-center">
                        <div class="refreshWrap">
                            <div class="timer"><i class="fa fa-3x icofont icofont-clock-time faa-tada animated"></i></div>
                            <h3 class="red">Timed Out</h3>

                            <p><strong>Still interested in flying from San Francisco to Hyderabad?  </strong></p>
                            <p>We noticed you have been inactive for a while. Please refresh your search results to review the latest air fares.</p>

                            <button type="button" class="btn button newSearchbtn mr-15" onclick="Closeprom()">No Thanks! </button>
                            <button type="button" class="btn button refreshBtn" id="popprom" onclick="addpromo()">Refresh  </button>
                            <h5 class="red">Your search results have expired!</h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <div id="paylater_timer_popup" runat="server"></div>
       

 <script>

        setTimeout(function () {
    $('#paylater_popup_timer').modal();
}, <%=timer_payletr%>);</script>


    </form>



    <!-- #include file ="footer.aspx" -->


    <script type="text/javascript" src="Design/js/jquery.min.js"></script>

    <script type="text/javascript" src="Design/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="Design/js/jquery.stickem.js"></script>
    <script>
        //$(document).keydown(function (event) {
        //    if (event.keyCode == 123) {
        //        return false;
        //    }
        //    else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
        //        return false;
        //    }
        //});

        //$(document).on("contextmenu", function (e) {
        //    e.preventDefault();
        //});
        $(document).ready(function () {
            $('#stick').stickem();
        });
        $("#Voucher_Id").keydown(function () {
            $("#Voucher_Id1").val($(this).val());
        });
        $("#Voucher_Id1").keydown(function () {
            $("#Voucher_Id").val($(this).val());
        });
    </script>


    <script type="text/javascript">
        $(function () {
            var moveLeft = 0;
            var moveDown = 0;

            $('#popover-Icon').hover(function (e) {
                $('#popover-msg').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('#popover-msg').hide();
            });

            //$('#popover-Icon').mousemove(function(e) {
            // $("#popover-msg").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            //});

            $('#popover-Incl').hover(function (e) {
                $('#popover-Incl-msg').show();
                //.css('top', e.pageY + moveDown)
                //.css('left', e.pageX + moveLeft)
                //.appendTo('body');
            }, function () {
                $('#popover-Incl-msg').hide();
            });
        });
    </script>

    <!--Payment card animation js -->
    <script type="text/javascript" src="Design/js/card.js"></script>
    <script type="text/javascript" src="Design/js/jquery.card.js"></script>
    <script type="text/javascript" src="Design/js/index.js"></script>

    <!-----==== Scroll to top js--->
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("[data-toggle='tooltip']").tooltip();
            });
            <%--if ('<%=ipcity%>' != '') {
                $("#City").val('<%=ipcity%>');
            }--%>
            <%--if ('<%=ipstate%>' != '') {
                $("#State").val('<%=ipstate%>');
            }--%>
            if ('<%=ipCountry%>' != '') {
                $("#country").val('<%=ipCountry%>');
            }
            totamount = <%=TtAmountCalc%>;
            if ('<%=cancel%>' == 'yes') {
                document.getElementById("CancellPolicy-Yes").checked = true;
                cancel = 15 * parseFloat(<%=TotalPax%>);
            }
            else {
                document.getElementById("CancellPolicy-No").checked = true;
                cancel = 0;
            }
            if ('<%=Promo%>' == 'yes') {
                vamount = '<%=PromoAm%>';
            }
            else {
                vamount = 0;
            }

            if ('<%=InsuAm%>' != '0') {
                Iamount = '<%=InsuAm%>';
            }
            else {
                Iamount = 0;
            }

            $(function () {

                $(document).on('scroll', function () {

                    if ($(window).scrollTop() > 100) {
                        $('.scroll-top-wrapper').addClass('show');
                    } else {
                        $('.scroll-top-wrapper').removeClass('show');
                    }
                });

                $('.scroll-top-wrapper').on('click', scrollToTop);
            });

            function scrollToTop() {
                verticalOffset = typeof (verticalOffset) != 'undefined' ? verticalOffset : 0;
                element = $('body');
                offset = element.offset();
                offsetTop = offset.top;
                $('html, body').animate({ scrollTop: offsetTop }, 500, 'linear');
            }

        });

    </script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $(function () {
        //        $("[data-toggle='tooltip']").tooltip();
        //    });
        //    $(".selectdropdown").change(function () {
        //        var index = $(this).val();
        //        if (index == "") {
        //            $(this).data("title", "Please select the option") // Clear the title - there is no error associated anymore
        //                .addClass("error")
        //                .tooltip();
        //        }
        //        else {
        //            $(this).data("title", "") // Clear the title - there is no error associated anymore
        //                .removeClass("error")
        //                .tooltip("destroy");
        //        }
        //    });
        //});
    </script>




    <script type="text/javascript" src="https://mpsnare.iesnare.com/snare.js"></script>
    <script lang="javascript" src="/content/static_wdp.js"></script>
    <script lang="javascript" src="/iojs/4.1.1/dyn_wdp.js"></script>


    <script src="js/client.min.js"></script>
    <script>
        var mouseX = 0;
        var mouseY = 0;
        var popupCounter = 0;

        document.addEventListener("mousemove", function (e) {
            mouseX = e.clientX;
            mouseY = e.clientY;
            //document.getElementById("coordinates").innerHTML = "<br />X: " + e.clientX + "px<br />Y: " + e.clientY + "px";
        });

        $(document).mouseleave(function () {
            if (mouseY < 100) {
                var csfd = document.getElementById("clossepop").innerHTML;//$("#clossepop").InnerHtml;
                if (<%=discount%> == 0) {
                    if ('<%=closeuppopup%>' == 'true') {
                        if (csfd != "") {
                            if (popupCounter < 1) {
                                //alert("Please don't close the tab!");
                                if ($("#Voucher_Id").val() == "")
                                    document.getElementById("poppromo1").style.display = "block";
                            }
                            popupCounter++;
                        }
                    }
                }
            }
        });
        function Closeprom() {
            document.getElementById("poppromo1").style.display = "none";
            var objPromoCode = {};
            objPromoCode.id = $("#Voucher_Id1").val();
            objPromoCode.am = vamount;
            objPromoCode.status = 'no';
            objPromoCode.ssid = '<%=ssid%>';
            objPromoCode.sid = '<%=sid%>';
            $.ajax({
                type: "POST",
                url: "PassengerDetails.aspx/UpdatePromoCodeClosePop",
                data: JSON.stringify(objPromoCode),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                }
            });
        }







        function addpromo() {
            var flVoucher_Id = document.forms["form1"]["Voucher_Id1"];
            $("#Voucher_Id").val('<%=promocode%>');
            $("#Voucher_Id1").val('<%=promocode%>');
            var flagVoucher_Id = true;
            flagVoucher_Id = required(flVoucher_Id);
            $("#Voucher_Ihint1_hint").remove();
            var totamount = $("#totalAmount1").text();

            $("#Voucher_Ihint1_hint").remove();
            document.getElementById("Voucher_Ihint1").innerHTML = "You got USD 5 Discount";
            document.getElementById("Voucher_Ihint").innerHTML = "You got USD 5 Discount";
            vamount = parseFloat(5);
            $("#promocode").empty();
            $("#promocode").append("- " + 5 + ".00 <small>USD</small>");
            $("#promocode1").empty();
            $("#promocode1").append("- " + 5 + ".00 <small>USD</small> ");
            $("#totalAmount1").empty();
            $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel) + " <small>USD</small> ");
            $("#totalAmount").empty();
            $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel) + " USD</h4><p>incl. all taxes and fees</p>");
            $("#totalAmount2").empty();
            $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel) + " <small>USD</small> </h3>");
            $("#totalAmount3").empty();
            $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel) + " <small>USD</small> ");
            $("#totalAmount4").empty();
            $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel) + " <small>USD</small> ");
           $("#totalAmount5").empty();
           $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel) + " <small>USD</small> ");
            $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel));
            $("#VoucherApply").val(vamount);
            $("#VoucherCode").val($("#Voucher_Id1").val());

            var objPromoCode = {};
            objPromoCode.id = $("#Voucher_Id1").val();
            objPromoCode.am = vamount;
            objPromoCode.status = 'yes';
            objPromoCode.ssid = '<%=ssid%>';
            objPromoCode.sid = '<%=sid%>';
            $.ajax({
                type: "POST",
                url: "PassengerDetails.aspx/UpdatePromoCodeClosePop",
                data: JSON.stringify(objPromoCode),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                }
            });
            document.getElementById("poppromo1").style.display = "none";
        }
        var client = new ClientJS();
        var fingerprint = client.getFingerprint();
        console.log(fingerprint);
        var obj = {};
        obj.fingerprint = fingerprint;
        obj.track_id = '<%= track_id%>';
        obj.ssid = '<%= ssid%>';

        $.ajax({
            type: "POST",
            url: "PassengerDetails.aspx/Update_Device_ID",
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
        var io_bbout_element_id = 'ioBB';
        var io_install_stm = false;
        var io_exclude_stm = 12;
        var io_install_flash = false;
        var io_enable_rip = true;
        var fp_bbout_element_id = 'fpBB';
    </script>

    <script type="text/javascript" src="https://mpsnare.iesnare.com/snare.js"></script>
    <script lang="javascript" src="/content/static_wdp.js"></script>
    <script lang="javascript" src="/iojs/4.1.1/dyn_wdp.js"></script>
    <script type="text/javascript" src="js/jivo_v1.js"></script>


  	<script type="text/javascript" language="javascript">

        var message='<%=ChatMessage%>';

	 (function () { var widget_id = 'YYZfPAHgG2'; var d = document; var w = window; function l() { var s = document.createElement('script'); s.type = 'text/javascript'; s.async = true; s.src = '//code.jivosite.com/script/widget/' + widget_id; var ss = document.getElementsByTagName('script')[0]; ss.parentNode.insertBefore(s, ss); } if (d.readyState == 'complete') { l(); } else { if (w.attachEvent) { w.attachEvent('onload', l); } else { w.addEventListener('load', l, false); } } })();

	</script>






    <script type="text/javascript" src="js/html2canvas.js"></script>
    <script type="text/javascript">
        function _tmtrack() {
            html2canvas(document.body, { logging: false }).then(function (canvas) {
                var imagedata = canvas.toDataURL('image/png');
                var imgdata = imagedata.replace(/^data:image\/(png|jpg);base64,/, "");
                //ajax call to save image inside folder
                $.ajax({
                    url: '_tmtrack.aspx',
                    data: {
                        imgdata: imgdata,
                        foldername:'<%=sid%>',
                        filename:'<%=ssid%>',
                        page: 'details'

                    },
                    type: 'post',
                    success: function (response) {
                        //console.log(response);
                        //	   $('#image_id img').attr('src', response);
                    }
                });
            });
        }
    </script>






    <script type="text/javascript">
        adroll_adv_id = "XWYYUWV7ZBFPZK2GETPLYN";
        adroll_pix_id = "WHIZH3FTUNA3XO27DPVBE4";

        adroll_custom_data = { "product_id": "<%=product_Id%>", "product_action": "AddToCart" };

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
        (function () {
            window.onload = function () {
               
            };
        }());


        function ValidateVoucher() {
            var flVoucher_Id = document.forms["form1"]["Voucher_Id1"];
            $("#Voucher_Id").val($("#Voucher_Id1").val());
            var flagVoucher_Id = true;
            flagVoucher_Id = required(flVoucher_Id);
            $("#Voucher_Ihint1_hint").remove();
            var totamount = $("#totalAmount1").text();

            if (!flagVoucher_Id) {
                document.getElementById("Voucher_Ihint1").innerHTML = "Please Enter Promo Code";
                document.getElementById("Voucher_Ihint").innerHTML = "Please Enter Promo Code";

                //$("#Voucher_Ihint1").append("<span id='Voucher_Ihint1_hint' class='form_hint'>Please Enter Promo Code</span>");
                //$("#Voucher_Ihint1_hint").css("display", "block");CancellationFee1
                $("#promocode").empty();
                $("#promocode").append("- 0.00 <small>USD</small>");
                $("#promocode1").empty();
                $("#promocode1").append("- 0.00 <small>USD</small>");
                //var vamount = 0;
                //if ($("#VoucherApply").val() != "") {
                //    vamount = parseFloat($("#VoucherApply").val());
                //}
                vamount = 0;
                var radioValue = $("input[name='radio-group']:checked").val();
                cancel = 0;
                if (radioValue == 'Yes') {
                    cancel = 15 * parseFloat(<%=TotalPax%>);
                }
                else {
                    cancel = 0;
                }
                var radioValue1 = $("input[name='radio-group1']:checked").val();
                Iamount = 0;
                if (radioValue1 == 'Yes') {
                    Iamount = parseFloat(<%=InsuAm%>);
                }
                else {
                    Iamount = 0;
                }

                $("#VoucherApply").val(0);
                $("#VoucherCode").val($("#Voucher_Id1").val());
                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));

                $("#totalAmount1").empty();
                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small> USD</small>");
                $("#totalAmount").empty();
                $("#totalAmount").append("<h4>" + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD </h4><p>incl. all taxes and fees</p>");
                $("#totalAmount2").empty();
                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small></h3>");
                $("#totalAmount3").empty();
                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                $("#totalAmount4").empty();
                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                $("#totalAmount5").empty();
                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                $("#VoucherCode").val($("#Voucher_Id1").val());

                var objPromoCode = {};
                objPromoCode.id = $("#Voucher_Id1").val();
                objPromoCode.am = vamount;
                objPromoCode.status = 'no';
                objPromoCode.ssid = '<%=ssid%>';
                $.ajax({
                    type: "POST",
                    url: "PassengerDetails.aspx/UpdatePromoCode",
                    data: JSON.stringify(objPromoCode),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert(msg.d);
                    }
                });
            }
            else {
                var promo1 = $("#promocode").text();
                if ($("#VoucherCode").val() != $("#Voucher_Id1").val()) {
                    $("#Voucher_Ihint1_hint").remove();
                    $.ajax({
                        url: "AjaxCompleteAirport.asmx/Vouchers",
                        method: "post",
                        data: JSON.stringify({
                            'term': flVoucher_Id.value,
                            'searchType': searchType1.value,
                            'afid': afid1.value,
                            'validFrom': DeptDate.value,
                            'validTo': ArrDate1.value,
                            'minimumBasket': '<%=TtAmountCalc%>',
                            'trackid': fingerprint,
                            'DeptDate': DeptDate.value,
                            'ArrDate': ArrDate1.value,
                            'JType': JType.value,
                            'DeptCountry': '<%=deptCountry%>',
                            'DestCountry': '<%=destCountry%>',
			    'Dept': '<%=deptCode%>',
                            'Dest': '<%=destCode%>',
                            'IpCountry': $("#country").val(),
                            'IpCity': '<%=ipcity%>',
                            'PaxEmailID': $("#Email").val(),
			    'Airline': '<%=Airline_Code %>'
                        }),
                        //Dept Dest DeptDate ArrDate JType Status DeptCountry DestCountry IpCountry IpCity Vouchers PaxEmailID
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var radioValue = $("input[name='radio-group']:checked").val();
                            var cancel = 0;
                            if (radioValue == 'Yes') {
                                cancel = 15 * parseFloat(<%=TotalPax%>);
                            }
                            else {
                                cancel = 0;
                            }

                            var radioValue1 = $("input[name='radio-group1']:checked").val();
                            Iamount = 0;
                            if (radioValue1 == 'Yes') {
                                Iamount = parseFloat(<%=InsuAm%>);
                            }
                            else {
                                Iamount = 0;
                            }
                            if (msg.d.indexOf("Voucher is valid for ") != -1) {
                                document.getElementById("Voucher_Ihint1").innerHTML = msg.d.split("^")[0];
                                document.getElementById("Voucher_Ihint").innerHTML = msg.d.split("^")[0];
                                //$("#Voucher_Ihint1").append("<span id='Voucher_Ihint1_hint' class='form_hint'>" + msg.d + "</span>");
                                //$("#Voucher_Ihint1_hint").css("display", "block");
                                //var vamount = 0;
                                vamount = parseFloat(msg.d.split("^")[1]);
                                $("#promocode").empty();
                                $("#promocode").append("- " + msg.d.split("^")[1] + ".00 <small>USD</small>");
                                $("#promocode1").empty();
                                $("#promocode1").append("- " + msg.d.split("^")[1] + ".00 <small>USD</small> ");
                                $("#totalAmount1").empty();
                                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                                $("#totalAmount").empty();
                                $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD</h4><p>incl. all taxes and fees</p>");
                                $("#totalAmount2").empty();
                                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> </h3>");
                                $("#totalAmount3").empty();
                                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                                $("#totalAmount4").empty();
                                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                                $("#totalAmount5").empty();
                                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                            <%--totalAm.Value = parseFloat(parseFloat(<%=TtAmountCalc%>) - parseFloat(msg.d.split("USD")[1]));--%>
                                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));
                                $("#VoucherApply").val(vamount);
                                $("#VoucherCode").val($("#Voucher_Id1").val());

                                var objPromoCode = {};
                                objPromoCode.id = $("#Voucher_Id1").val();
                                objPromoCode.am = vamount;
                                objPromoCode.status = 'yes';
                                objPromoCode.ssid = '<%=ssid%>';
                                $.ajax({
                                    type: "POST",
                                    url: "PassengerDetails.aspx/UpdatePromoCode",
                                    data: JSON.stringify(objPromoCode),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        // alert(msg.d);
                                    }
                                });
                            }
                            else {
                                $("#Voucher_Id1").val("");
                                $("#Voucher_Id").val("");
                                document.getElementById("Voucher_Ihint1").innerHTML = msg.d.split("^")[0];
                                document.getElementById("Voucher_Ihint").innerHTML = msg.d.split("^")[0];
                                //$("#Voucher_Ihint1").append("<span id='Voucher_Ihint1_hint' class='form_hint'>" + msg.d + "</span>");
                                //$("#Voucher_Ihint1_hint").css("display", "block");
                                var promo = $("#promocode").text();
                                $("#promocode").empty();
                                $("#promocode").append("- 0.00 <small>USD</small>");
                                $("#promocode1").empty();
                                $("#promocode1").append("- 0.00 <small>USD</small>");


                                vamount = 0;
                                $("#VoucherApply").val(0);
                                $("#VoucherCode").val($("#Voucher_Id1").val());
                                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2));

                                $("#totalAmount1").empty();
                                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small> USD</small>");
                                $("#totalAmount").empty();
                                $("#totalAmount").append("<h4>" + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " USD </h4><p>incl. all taxes and fees</p>");
                                $("#totalAmount2").empty();
                                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small></h3>");
                                $("#totalAmount3").empty();
                                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                                $("#totalAmount4").empty();
                                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                                $("#totalAmount5").empty();
                                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                                //totalAm.Value = "<%=TtAmountCalc%>";
                                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2));
                                $("#VoucherCode").val($("#Voucher_Id1").val());

                                var objPromoCode = {};
                                objPromoCode.id = $("#Voucher_Id1").val();
                                objPromoCode.am = vamount;
                                objPromoCode.status = 'no';
                                objPromoCode.ssid = '<%=ssid%>';
                                $.ajax({
                                    type: "POST",
                                    url: "PassengerDetails.aspx/UpdatePromoCode",
                                    data: JSON.stringify(objPromoCode),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        // alert(msg.d);
                                    }
                                });
                            }
                        },
                        error: function (err) {
                            $("#Voucher_Id1").val("");
                            $("#Voucher_Id").val("");
                            //alert(err);
                        }
                    });
                }
                else {
                    document.getElementById("Voucher_Ihint").innerHTML = "Promo Code Already Applied";
                    document.getElementById("Voucher_Ihint1").innerHTML = "Promo Code Already Applied";
                }
            }
        }
        function ValidateVoucher1() {
            var flVoucher_Id = document.forms["form1"]["Voucher_Id"];
            $("#Voucher_Id1").val($("#Voucher_Id").val());
            var totamount = $("#totalAmount1").text();
            var flagVoucher_Id = true;
            flagVoucher_Id = required(flVoucher_Id);
            $("#Voucher_Ihint_hint").remove();
            if (!flagVoucher_Id) {
                document.getElementById("Voucher_Ihint1").innerHTML = "Please Enter Promo Code";
                document.getElementById("Voucher_Ihint").innerHTML = "Please Enter Promo Code";

                //$("#Voucher_Ihint1").append("<span id='Voucher_Ihint1_hint' class='form_hint'>Please Enter Promo Code</span>");
                //$("#Voucher_Ihint1_hint").css("display", "block");CancellationFee1
                $("#promocode").empty();
                $("#promocode").append("- 0.00 <small>USD</small>");
                $("#promocode1").empty();
                $("#promocode1").append("- 0.00 <small>USD</small>");
                vamount = 0;
                var radioValue = $("input[name='radio-group']:checked").val();
                var cancel = 0;
                if (radioValue == 'Yes') {
                    cancel = 15 * parseFloat(<%=TotalPax%>);
                }
                else {
                    cancel = 0;
                }
                var radioValue1 = $("input[name='radio-group1']:checked").val();
                Iamount = 0;
                if (radioValue1 == 'Yes') {
                    Iamount = parseFloat(<%=InsuAm%>);
                }
                else {
                    Iamount = 0;
                }
                $("#VoucherApply").val(0);
                $("#VoucherCode").val($("#Voucher_Id1").val());
                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount + Iamount).toFixed(2));

                $("#totalAmount1").empty();
                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small> USD</small>");
                $("#totalAmount").empty();
                $("#totalAmount").append("<h4>" + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD </h4><p>incl. all taxes and fees</p>");
                $("#totalAmount2").empty();
                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small></h3>");
                $("#totalAmount3").empty();
                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                $("#totalAmount4").empty();
                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                $("#totalAmount5").empty();
                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                $("#VoucherCode").val($("#Voucher_Id1").val());

                var objPromoCode = {};
                objPromoCode.id = $("#Voucher_Id1").val();
                objPromoCode.am = vamount;
                objPromoCode.status = 'no';
                objPromoCode.ssid = '<%=ssid%>';
                $.ajax({
                    type: "POST",
                    url: "PassengerDetails.aspx/UpdatePromoCode",
                    data: JSON.stringify(objPromoCode),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert(msg.d);
                    }
                });
            }
            else {
                var promo1 = $("#promocode").text();
                if ($("#VoucherCode").val() != $("#Voucher_Id1").val()) {
                    $("#Voucher_Ihint_hint").remove();
                    $.ajax({
                        url: "AjaxCompleteAirport.asmx/Vouchers",
                        method: "post",
                        data: JSON.stringify({
                            'term': flVoucher_Id.value,
                            'searchType': searchType1.value,
                            'afid': afid1.value,
                            'validFrom': DeptDate.value,
                            'validTo': ArrDate1.value,
                            'minimumBasket': '<%=TtAmountCalc%>',
                            'trackid': fingerprint,
                            'DeptDate': DeptDate.value,
                            'ArrDate': ArrDate1.value,
                            'JType': JType.value,
                            'DeptCountry': '<%=deptCountry%>',
                            'DestCountry': '<%=destCountry%>',
 			    'Dept': '<%=deptCode%>',
                            'Dest': '<%=destCode%>',
                            'IpCountry': $("#country").val(),
                            'IpCity': '<%=ipcity%>',
                            'PaxEmailID': $("#Email").val(),
			    'Airline': '<%=Airline_Code %>'
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var radioValue = $("input[name='radio-group']:checked").val();
                            var cancel = 0;
                            if (radioValue == 'Yes') {
                                cancel = 15 * parseFloat(<%=TotalPax%>);
                            }
                            else {
                                cancel = 0;
                            }
                            var radioValue1 = $("input[name='radio-group1']:checked").val();
                            Iamount = 0;
                            if (radioValue1 == 'Yes') {
                                Iamount = parseFloat(<%=InsuAm%>);
                            }
                            else {
                                Iamount = 0;
                            }
                            if (msg.d.indexOf("Voucher is valid for ") != -1) {
                                document.getElementById("Voucher_Ihint1").innerHTML = msg.d.split("^")[0];
                                document.getElementById("Voucher_Ihint").innerHTML = msg.d.split("^")[0];
                                //$("#Voucher_Ihint1").append("<span id='Voucher_Ihint1_hint' class='form_hint'>" + msg.d + "</span>");
                                //$("#Voucher_Ihint1_hint").css("display", "block");
                                vamount = parseFloat(msg.d.split("^")[1]);
                                $("#promocode").empty();
                                $("#promocode").append("- " + msg.d.split("^")[1] + ".00 <small>USD</small>");
                                $("#promocode1").empty();
                                $("#promocode1").append("- " + msg.d.split("^")[1] + ".00 <small>USD</small> ");
                                $("#totalAmount1").empty();
                                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                                $("#totalAmount").empty();
                                $("#totalAmount").append("<h4> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " USD</h4><p>incl. all taxes and fees</p>");
                                $("#totalAmount2").empty();
                                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> </h3>");
                                $("#totalAmount3").empty();
                                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                                $("#totalAmount4").empty();
                                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                                $("#totalAmount5").empty();
                                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2) + " <small>USD</small> ");
                            <%--totalAm.Value = parseFloat(parseFloat(<%=TtAmountCalc%>) - parseFloat(msg.d.split("USD")[1]));--%>
                                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) - vamount + cancel + Iamount).toFixed(2));
                                $("#VoucherApply").val(vamount);
                                $("#VoucherCode").val($("#Voucher_Id1").val());

                                var objPromoCode = {};
                                objPromoCode.id = $("#Voucher_Id1").val();
                                objPromoCode.am = vamount;
                                objPromoCode.status = 'yes';
                                objPromoCode.ssid = '<%=ssid%>';
                                $.ajax({
                                    type: "POST",
                                    url: "PassengerDetails.aspx/UpdatePromoCode",
                                    data: JSON.stringify(objPromoCode),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        // alert(msg.d);
                                    }
                                });
                            }
                            else {
                                $("#Voucher_Id1").val("");
                                $("#Voucher_Id").val("");
                                document.getElementById("Voucher_Ihint1").innerHTML = msg.d.split("^")[0];
                                document.getElementById("Voucher_Ihint").innerHTML = msg.d.split("^")[0];
                                //$("#Voucher_Ihint1").append("<span id='Voucher_Ihint1_hint' class='form_hint'>" + msg.d + "</span>");
                                //$("#Voucher_Ihint1_hint").css("display", "block");
                                var promo = $("#promocode").text();
                                $("#promocode").empty();
                                $("#promocode").append("- 0.00 <small>USD</small>");
                                $("#promocode1").empty();
                                $("#promocode1").append("- 0.00 <small>USD</small>");


                                vamount = 0;
                                $("#VoucherApply").val(0);
                                $("#VoucherCode").val($("#Voucher_Id1").val());
                                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2));

                                $("#totalAmount1").empty();
                                $("#totalAmount1").append(parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small> USD</small>");
                                $("#totalAmount").empty();
                                $("#totalAmount").append("<h4>" + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " USD </h4><p>incl. all taxes and fees</p>");
                                $("#totalAmount2").empty();
                                $("#totalAmount2").append("<p>Total Price:</p><h3> " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small></h3>");
                                $("#totalAmount3").empty();
                                $("#totalAmount3").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                                $("#totalAmount4").empty();
                                $("#totalAmount4").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                                $("#totalAmount5").empty();
                                $("#totalAmount5").append(" " + parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2) + " <small>USD</small>");
                                //totalAm.Value = "<%=TtAmountCalc%>";
                                $("#totalAm").val(parseFloat(parseFloat(<%=TtAmountCalc%>) + vamount + cancel + Iamount).toFixed(2));
                                $("#VoucherCode").val($("#Voucher_Id1").val());

                                var objPromoCode = {};
                                objPromoCode.id = $("#Voucher_Id1").val();
                                objPromoCode.am = vamount;
                                objPromoCode.status = 'no';
                                objPromoCode.ssid = '<%=ssid%>';
                                $.ajax({
                                    type: "POST",
                                    url: "PassengerDetails.aspx/UpdatePromoCode",
                                    data: JSON.stringify(objPromoCode),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        // alert(msg.d);
                                    }
                                });
                            }
                        },
                        error: function (err) {
                            $("#Voucher_Id1").val("");
                            $("#Voucher_Id").val("");
                            //alert(err);
                        }
                    });
                }
                else {
                    document.getElementById("Voucher_Ihint").innerHTML = "Promo Code Already Applied";
                    document.getElementById("Voucher_Ihint1").innerHTML = "Promo Code Already Applied";
                }
            }
        }

    </script>
        <!-- Mini Itinerary Show/hide js -->
<script type="text/javascript">
    $("div.show-more a").on("click", function () {
        var $this = $(this);
        var $content = $this.parent().prev("div.rcs-wrap-list");
        var linkText = $this.text().toUpperCase();
        if (linkText === "SHOW MORE") {
            linkText = "Show less";
            $content.removeClass("hideContent").addClass("showContent");
        } else {
            linkText = "Show more";
            $content.removeClass("showContent").addClass("hideContent");
            $('.rcs-wrap-list').animate({ scrollTop: 0 }, 595);
        };
        $this.text(linkText);
    });
</script>





    <script type="text/javascript">
 //Pay now and Pay Later JS
 // in normal paynow active if you select the paylater paylater active and  paymentMode(Hiddenvalue)=paylater in cs code



    var page = 'paynow';
     $("#paymentMode").val(page);
$(document).ready(function() {
    $("#pay-menu li a").on('click', function(e) {
        e.preventDefault()
		 $("#pay-menu li a").removeClass('active');
		 $(this).addClass('active');
        page = $(this).data('page');
        if (page == "paynow") {
            //document.getElementById("btnbooknow").innerHTML = "Pay Now";
            var btnbooknow = document.getElementById('btnbooknow');
            var btnbooknow1 = document.getElementById('btnbooknow1');
            btnbooknow.innerHTML = "Pay Now <i class='fa fa-plane planeIcon'></i>";
            btnbooknow1.innerHTML="Pay Now <i class='fa fa-plane planeIcon'></i>";


            ReviewtripTerm.innerHTML = "<h5><strong>Review the trip terms:</strong></h5><ul><li>1. Name changes are not permitted. Tickets are non-refundable and non-transferable.</li><li>2. Where permitted, changes to your itinerary costs you minimum of USD150 per ticket. Airlines fare/fees are additional cost for you.</li> <li>3. Your credit/debit card may be billed in multiple charges totaling the final total price. Billing statement may disply Airlines name or Travelmerry or Agent Fee or our suplier name.</li></ul> ";

            // chktext.InnerHtml= "By clicking “Pay Now” button, you agree to our <a href='Termsandconditions.aspx' target='_blank' class='blue'>Terms and Conditions</a> including refund and cancellation policy of this booking.";


            //btnbooknow1.innerHTML = "Pay Now <i class='fa fa-plane planeIcon'></i>";


            paylater_description.innerHTML = "";





        }
        else {
            //document.getElementById("btnbooknow").innerHTML = "BookNow PayLater";

            var btnbooknow = document.getElementById('btnbooknow');
            var btnbooknow1 = document.getElementById('btnbooknow1');
            btnbooknow.innerHTML = "Book Now & Pay Later <i class='fa fa-plane planeIcon'></i>";

           
            btnbooknow1.innerHTML = "Book Now & Pay Later <i class='fa fa-plane planeIcon'></i>";
            ReviewtripTerm.innerHTML = "";
            // chktext.InnerHtml= "By clicking “Pay Later” button, you agree to our <a href='Termsandconditions.aspx' target='_blank' class='blue'>Terms and Conditions</a> including refund and cancellation policy of this booking.";
            paylater_description.innerHTML ="<div class='pay-option-title' class='pm-container'>Lock Fare Benefits </div>" +
                            
                            "<div class='pay-later-tbl'>" +
                            	"<div class='pay-later-cel-1'>" +
                                	"<img src='Design/images/lockfareIcon.png' alt='Lock fare icon'>" +
                                	"<h4>Locked Price</h4>" +
					"<p>Lock @ $0 helps you lock the<br> fares by paying nothing." +
									"Absolutely nothing.</p>" +
                                "</div>" +
                                "<div class='pay-later-cel-2'>" +
                                	"<img src='Design/images/pricehighIcon.png' alt='Price hike icon'>" +
                                	"<h4>Price Hike Protection</h4>" +
					"<p>Fares can increase in next 24 hours. <br>You pay at the locked price.</p>" +
                                "</div>" +
                           " </div>" ;


        }

        // assign page val to paymentMode hidden field 
        $("#paymentMode").val(page);
        
        $("#pages .page:not('.hide')").stop().fadeOut('fast', function() {
            $(this).addClass('hide');
			
            $('#pages .page[data-page="'+page+'"]').fadeIn('slow').removeClass('hide');
        });
    });
});
</script>




<script>


       var placeSearch, autocomplete;
       var componentForm = {
           street_number: 'short_name',
           route: 'long_name',
           locality: 'long_name',
           administrative_area_level_1: 'short_name',
           country: 'long_name',
           postal_code: 'short_name'
       };


       var componentForm1 = {
           street_number: 'Address',          
           locality: 'City',
           administrative_area_level_1: 'State',
           country: 'country',
           postal_code: 'ZipCode'
       };


       function initAutocomplete() {
           // Create the autocomplete object, restricting the search to geographical
           // location types.
           autocomplete = new google.maps.places.Autocomplete(
           /** @type {!HTMLInputElement} */(document.getElementById('location')),
            { types: ['geocode'] });

           // When the user selects an address from the dropdown, populate the address
           // fields in the form.
           autocomplete.addListener('place_changed', fillInAddress);
       }

       function fillInAddress() {
           // Get the place details from the autocomplete object.
           var place = autocomplete.getPlace();

           for (var component in componentForm) {


               if (componentForm1[component]) {
                   document.getElementById(componentForm1[component]).value = '';
                   document.getElementById(componentForm1[component]).disabled = false;
               }
           }

           // Get each component of the address from the place details
           // and fill the corresponding field on the form.


           var country_name = "";


           for (var i = 0; i < place.address_components.length; i++) {

               var addressType = place.address_components[i].types[0];
               if (componentForm[addressType]) {
                   var val = place.address_components[i][componentForm[addressType]];
                   if (addressType != "route") {
                       if (componentForm1[addressType]) {
                           if (addressType != "country") {
                               document.getElementById(componentForm1[addressType]).value = val;
                           }
                           else {
                               country_name = val;
                           }
                       }
                   }
                   else {
                       if (document.getElementById("Address").value != "")
                           document.getElementById("Address").value += "," + val;
                       else
                           document.getElementById("Address").value = val;
                   }
               }

           }


           var sel = document.getElementById("country");

           for (var i = 0, j = sel.options.length; i < j; ++i) {
               if (sel.options[i].innerHTML == country_name) {
                   sel.selectedIndex = i;
                   break;
               }
           }
       }
       

       // Bias the autocomplete object to the user's geographical location,
       // as supplied by the browser's 'navigator.geolocation' object.
       function geolocate() {
           if (navigator.geolocation) {
               navigator.geolocation.getCurrentPosition(function (position) {
                   var geolocation = {
                       lat: position.coords.latitude,
                       lng: position.coords.longitude
                   };
                   var circle = new google.maps.Circle({
                       center: geolocation,
                       radius: position.coords.accuracy
                   });
                   autocomplete.setBounds(circle.getBounds());
               });
           }
       }
    </script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCMWrBxUXxydNKbWRXM2es4rJVF_T0Vrps&libraries=places&callback=initAutocomplete"
        async defer></script>


     <script>
       function hj_fliters() {

           let dest_hj = '<%=destCode%>';
           let deptDate_hj = '<%=deptDate_Hj%>';

           hj('tagRecording', [dest_hj,deptDate_hj]);

          <%-- hj('tagRecording', ['Cabin Class', 'Trip Type', 'Outbound Travel Date', 'Inbound Travel Date', 'Origin', 'Destination', 'User Currency', 'Final Price', 'Carrier Name', '<%=JType%>']);--%>
           window.hj = window.hj || function () { (hj.q = hj.q || []).push(arguments) };
       }
      
    </script>
    <script src="js/travelmerry_hotjar.js" type="text/javascript"></script>







</body>

</html>

