<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="WebUserControl" %>

<header id="Main_header">

    <nav id="Main_nav">

        <section id="Main_sec">
            <a href="Default.aspx" class="Header_logo"><i class="fa-solid fa-c">Laptop World</i></a>
        </section>
 
        <section id="Main_lists">
            <ul class="Main_ul">
                
                <li class="Main_list"><a href="Default.aspx" class="Main_A">Home</a></li>
                <li class="Main_list"><a href="ProductUpload.aspx" class="Main_A" id="productUpload" runat="server">Product Upload</a></li>
                <li class="Main_list"><a href="Shop.aspx" class="Main_A">Shop</a>

                    <ul class="Main_Sub_ul">
                        <%--a tag is not button, so onlcick button event will not be fired
                            onserverclick used instead --%>
                        <li class="Main_Sub_list"><a href="#" id="APPLE" onserverclick="BrandFilterClick" class="Main_Sub_A" runat="server">APPLE</a></li>
                        <li class="Main_Sub_list"><a href="#" id="SAMSUNG" onserverclick="BrandFilterClick" class="Main_Sub_A" runat="server">SAMSUNG</a></li>
                        <li class="Main_Sub_list"><a href="#" id="DELL" onserverclick="BrandFilterClick" class="Main_Sub_A" runat="server">DELL</a></li>
                        <li class="Main_Sub_list"><a href="#" id="LENOVO" onserverclick="BrandFilterClick" class="Main_Sub_A" runat="server">LENOVO</a></li>
                        <li class="Main_Sub_list"><a href="#" id="ACER" onserverclick="BrandFilterClick" class="Main_Sub_A" runat="server">ACER</a></li>
                        <li class="Main_Sub_list"><a href="#" id="LG" onserverclick="BrandFilterClick" class="Main_Sub_A" runat="server">LG</a></li>
                    </ul>

                </li>

                <li class="Main_list"><a href="MyCart.aspx" class="Main_A"> 
                    <asp:Label Text="My Cart" runat="server" ID="_cart" /> </a>
                </li>

                <li class="Main_list" id="login" runat="server">
                    <a href="Login.aspx" class="Main_A" runat="server" id="_alink">
                    <asp:Label Text="Login" runat="server" ID="_login" /> </a>

                    <ul class="Main_Sub_ul">
                        <li class="Main_Sub_list"><a href="Logout.aspx" class="Main_Sub_A">
                            <asp:Label Text="Log Out" runat="server" ID="_logout" Visible="false"/></a></li>
                    </ul>
                </li>

            </ul>

        </section>
    </nav>

</header>
