<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" Title="MainPage" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">

    <section id="defaulatMain">
     
        <section id="e_home_video" >
         
        </section>
            <section id="e_home_sec">
                <h1 id="e_hope_shopText">
                    WELCOME  
                </h1>
             <asp:Button Text="G O S H O P" runat="server" CssClass="e_home_btn" ID="GoSHOP" OnClick="GoSHOP_Click"  />

            </section>
      
        <section>
            <video src="image/Project.mp4" autoplay muted loop />
        </section>
        
        <section id="e_Home_">
            <section id="e_Home_about">
                <h1 class="e_home-head">About US</h1>
                <pre class="e_home_head_p">Laptop World sells multiple brands of laptop </pre>
            </section>
            <section id="e_Home_fix">
                        <h1 class="e_home-head">How to be Vendor</h1>
                <pre class="e_home_head_p">You have to contact to zzangzwh@hotmail.com. <br />we will contact you and set an appointment thanks!
                </pre>
            </section>
        </section>
    </section>
   
</asp:Content>
