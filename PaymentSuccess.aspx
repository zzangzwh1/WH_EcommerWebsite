<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentSuccess.aspx.cs" Inherits="PaymentSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Success</title>
        <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="e_Success">
            <div id="e_Success_img" >
                <img src="image/download.png" />
            </div>
            <div >
                <h1 class="e_Success_head">Transscation Completed Successfully</h1>
                <h2 class="e_Success_head">Thank you for your Billing</h2>

            </div>
            <div id="e_Sucess_btns">
                <asp:Button Text="Go Home" runat="server" ID="e_Success_goHome" OnClick="e_Success_goHome_Click" />
                 <asp:Button Text="Continue Shop" runat="server" ID="e_Success_shop" OnClick="e_Success_shop_Click" />
                       
            </div>
        </div>
    </form>
</body>
</html>
