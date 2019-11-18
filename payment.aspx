<%@ Page Language="C#" AutoEventWireup="true" CodeFile="payment.aspx.cs" Inherits="payment" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    
    <title>TravelMerry - US</title><link rel="shortcut icon" href="images/favicon.ico" >

    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/main.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/animate.css" />
    <link rel="stylesheet" href="css/scrolling-nav.css">
    <link rel="stylesheet" href="css/component.css" />
    
    <link rel="stylesheet" href="css/timer.css" />
     
	<!--HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    
    <!--[if lt IE 9]>
    <script src="js/html5shiv.min.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
    
    <!--[if IE 7]>
  	<link rel="stylesheet" href="path/to/font-awesome/css/font-awesome-ie7.min.css">
	<![endif]-->
    
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
   
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>

</head>

<body>
  <form id="form1" runat="server">
  <asp:Panel ID="pnlPayment" runat="server">
        
            <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
        </asp:Panel>
<section id="terms" class="terms-section">
<asp:Panel ID="pnl3DSecure" runat="server" Visible="false">

<div class="container">

<div class="row"> 
	<div class="col-md-12 text-center" style="margin-top:2%;"> 
		<p>Booking is being made and Payment is processing - Please wait</p> 
		<p>Do not close this window.</p> 
		 
	</div>
</div>


<div class="row"> 
	<div class="col-md-12 text-center" style="margin-top:2%;"> 
		<div class="result-bg"> 
        	<div class="result-content"> 
              <iframe ID="IFPayment" frameborder="0" width="100%" height="700px" name="I1" runat="server" >
              </iframe>
           </div>
     	</div>      
	</div>
</div>
</div>
</asp:Panel>
</section>

 </form>



<!-- Placed at the end of the document so the pages load faster -->
</body>
</html>
