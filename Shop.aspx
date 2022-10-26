<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Shop" Title="Shop" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">

  <section>
          <asp:Button Text="Hide" runat="server" ID="_eShopHide"  OnClick="_eShopHide_Click" CssClass="e_shop_imgBtn" />
  </section>
    <asp:HiddenField ID="hfColumnRepeat" runat="server" Value="5" />
    <section id="e_Shop">

        <section id="e_Shop_SideNav" runat="server">

            <asp:Label Text="Brand" runat="server" CssClass="e_Shop_property" ID="lable_brand" />
            <asp:CheckBoxList ID="CB_BRAND" runat="server" OnSelectedIndexChanged="SortOptions_SelectedIndexChanged" CssClass="e_shop_checkboxLbl" AutoPostBack="true">
                <asp:ListItem Text="APPLE" runat="server" />
                <asp:ListItem Text="DELL" runat="server" />
                <asp:ListItem Text="ACER" runat="server" />
                <asp:ListItem Text="LENOVO" runat="server" />
                <asp:ListItem Text="SAMSUNG" runat="server" />
                <asp:ListItem Text="LG" runat="server" />
            </asp:CheckBoxList>

            <asp:Label Text="Color" runat="server" CssClass="e_Shop_property" ID="lable_color" />
            <asp:CheckBoxList ID="CB_COLOR" runat="server" OnSelectedIndexChanged="SortOptions_SelectedIndexChanged" CssClass="e_shop_checkboxLbl" AutoPostBack="true">
                <asp:ListItem Text="BLUE" runat="server" />
                <asp:ListItem Text="WHITE" runat="server" />
                <asp:ListItem Text="BLACK" runat="server" />
                <asp:ListItem Text="GREY" runat="server" />
                <asp:ListItem Text="RED" runat="server" />
            </asp:CheckBoxList>

            <asp:Label Text="Screen Size" runat="server" CssClass="e_Shop_property" ID="label_ss" />
            <asp:CheckBoxList ID="CB_SCREENSIZE" runat="server" OnSelectedIndexChanged="SortOptions_SelectedIndexChanged" CssClass="e_shop_checkboxLbl" AutoPostBack="true">

                <asp:ListItem Text="Under 14 Inch" Value="14" runat="server" />
                <asp:ListItem Text="14 ~ 15 Inch" Value="14,15" runat="server" />
                <asp:ListItem Text="15 ~ 16 Inch" Value="15,16" runat="server" />
                <asp:ListItem Text="16 ~ 17 Inch" Value="16,17" runat="server" />
                <asp:ListItem Text="Over 17 Inch" Value="17" runat="server" />


            </asp:CheckBoxList>
            <asp:Label Text="Capacity" runat="server" CssClass="e_Shop_property" ID="label_capacity" />
            <asp:CheckBoxList ID="CB_CAPACITY" runat="server" OnSelectedIndexChanged="SortOptions_SelectedIndexChanged" CssClass="e_shop_checkboxLbl" AutoPostBack="true">

                <asp:ListItem Text="128 GB" Value="128" runat="server" />
                <asp:ListItem Text="256 GB" Value="256" runat="server" />
                <asp:ListItem Text="512 GB" Value="512" runat="server" />

            </asp:CheckBoxList>
            <asp:Label Text="Price" runat="server" CssClass="e_Shop_property" ID="label_price" />
            <asp:CheckBoxList ID="CB_PRICE" runat="server" OnSelectedIndexChanged="SortOptions_SelectedIndexChanged" CssClass="e_shop_checkboxLbl" AutoPostBack="true">

                <asp:ListItem Text="Under $1000 " Value="1000" runat="server" />
                <asp:ListItem Text="$1000 ~ $1500" Value="1000, 1500" runat="server" />
                <asp:ListItem Text="$1500 ~ $2000" Value="1500,2000" runat="server" />
                <asp:ListItem Text="Over $2000" Value="2000" runat="server" />

            </asp:CheckBoxList>
        </section>

        <div id="e_Shop_Main">
            <asp:Repeater ID="DataList2" runat="server">
                <HeaderTemplate>
                    <div class="row">
                </HeaderTemplate>
                <ItemTemplate>

                    <!--      <div class="e_Shop_div_product">-->

                    <div class="col-sm-12 col-md-6 col-lg-4 ">
                        <div id="e_Shop_productName_div">

                        <asp:Label Text='<%#Eval("ProductName")%>' runat="server" CssClass="e_Shop_productName"  />
                        </div>
                   
                        <asp:ImageButton ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("ProductImage")) %>' runat="server" CssClass="e_Shop_image"
                            OnClick="ProductImgClick" CommandArgument='<%#Eval("productID") %>' />

                        <div class="e_Shop_div_price_img">
                            <asp:Label Text='<%# string.Concat("$", Eval("price", "{0:0.00}")) %>' runat="server" CssClass="e_Shop_productPrice" />
                             
                        </div>

                    </div>

                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>


    </section>

</asp:Content>
