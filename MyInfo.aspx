<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyInfo.aspx.cs" Inherits="MyInfo" Title="MyInfo" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
    <section id="e_Shop_MyInfo_Main">
        <section id="e_Shop_MyInfo_head">
            <asp:DataList runat="server" ID="dl_userInfo">
                <ItemTemplate>
                    <div id="e_Shop_MyInfo_Main_dl">

                        <div id="e_Shop_MyInfo_Main_userID">
                            <asp:Label Text="User ID :" runat="server" CssClass="e_shop_lblText" />
                            <asp:TextBox runat="server" Text='<%#Eval("username") %>' ID="TextBox1" Enabled="false" BackColor="#e7e9ebe3" CssClass="e_Shop_Myinfo_textbox" />
                        </div>
                        <div id="e_Shop_MyInfo_Main_name">
                            <asp:Label Text="Full Name :" runat="server" CssClass="e_shop_lblText" />
                            <asp:TextBox runat="server" Text='<%#string.Concat(Eval("fName")+" " + Eval("lName")) %>' ID="TextBox2" Enabled="false" BackColor="#e7e9ebe3" CssClass="e_Shop_Myinfo_textbox" />
                            <asp:Label Text="New Passord:" runat="server" CssClass="e_shop_lblText" />
                        </div>
                        <div id="e_Shop_MyInfo_Main_pass">

                            <asp:TextBox runat="server" Text='<%#Eval("password") %>' ID="_Spassword" Enabled="false" BackColor="#e7e9ebe3" CssClass="e_Shop_Myinfo_textbox" />

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                runat="server"
                                ErrorMessage="At least one upper,lower,number,special character and minimum 8 in length"
                                ControlToValidate="_Spassword"
                                ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input Password" Display="Dynamic"
                                ControlToValidate="_Spassword" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div id="e_Shop_MyInfo_Main_Cpass">
                            <asp:Label Text="Confirm New Passord:" ID="_Cpassword" runat="server" CssClass="e_shop_lblText" />
                            <asp:TextBox runat="server" Text="" ID="_SconfirmPassword" CssClass="e_Shop_Myinfo_textbox" Enabled="false" BackColor="#e7e9ebe3" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Your password doesnt Match" ForeColor="Red" Display="Dynamic" ControlToCompare="_Spassword" ControlToValidate="_SconfirmPassword"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Comfirm Password" Display="Dynamic" ControlToValidate="_SconfirmPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:Label Text="New Address" runat="server" CssClass="e_shop_lblText" />
                        </div>
                        <div id="e_Shop_MyInfo_Main_addr">

                            <asp:TextBox runat="server" Text='<%#Eval("address") %>' ID="_SAddr" CssClass="e_Shop_Myinfo_textbox" Enabled="false" BackColor="#e7e9ebe3" />
                            <asp:Label Text="New Phoe Number:" runat="server" CssClass="e_shop_lblText" />

                        </div>
                        <div id="e_Shop_MyInfo_Main_phone">
                            <asp:TextBox runat="server" Text='<%#Eval("phone") %>' ID="_SphoneNum" CssClass="e_Shop_Myinfo_textbox" Enabled="false" BackColor="#e7e9ebe3" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input Your Phone Number" Display="Dynamic"
                                ControlToValidate="_SphoneNum" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                runat="server"
                                ErrorMessage="only numbers allowed"
                                ControlToValidate="_SphoneNum"
                                ValidationExpression="[0-9]+"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <asp:Label Text="New Post Code: " runat="server" CssClass="e_shop_lblText" />
                        </div>
                        <div id="e_Shop_MyInfo_Main_post">

                            <asp:TextBox runat="server" Text='<%#Eval("postCode") %>' ID="_SpostCode" CssClass="e_Shop_Myinfo_textbox" Enabled="false" BackColor="#e7e9ebe3" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                runat="server"
                                ErrorMessage="Invalid Postal code!"
                                ControlToValidate="_SpostCode"
                                ValidationExpression="^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input Your PostalCode" Display="Dynamic"
                                ControlToValidate="_SpostCode" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:Label Text="New Email Address: " runat="server" CssClass="e_shop_lblText" />
                        </div>
                        <div id="e_Shop_MyInfo_Main_email">
                            <asp:TextBox runat="server" Text='<%#Eval("email") %>' ID="_Semail" CssClass="e_Shop_Myinfo_textbox" Enabled="false" BackColor="#e7e9ebe3" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input Your Mail Address" Display="Dynamic"
                                ControlToValidate="_Semail" ForeColor="Red"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                runat="server"
                                ErrorMessage="Invalid Email Address"
                                ControlToValidate="_Semail"
                                ValidationExpression="^[\w\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div id="e_Shop_MyInfo_btnPasss">
                            <asp:Button Text="Edit" ID="UI_btn_pass" runat="server" CssClass="_btnPassword" OnClick="UI_btn_pass_Click" />
                        </div>
                        <div id="e_Shop_MyInfo_btn_addr">
                            <asp:Button Text="Edit" runat="server" CssClass="_btnPassword" ID="UI_btn_addr" OnClick="UI_btn_addr_Click" />
                        </div>
                        <div id="e_Shop_MyInfo_btn_phone">
                            <asp:Button Text="Edit" runat="server" CssClass="_btnPassword" ID="UI_btn_phone" OnClick="UI_btn_phone_Click" />
                        </div>
                        <div id="e_Shop_MyInfo_btn_post">
                            <asp:Button Text="Edit" runat="server" CssClass="_btnPassword" ID="UI_btn_post" OnClick="UI_btn_post_Click" />
                        </div>
                        <div id="e_Shop_MyInfo_btn_email">
                            <asp:Button Text="Edit" runat="server" CssClass="_btnPassword" ID="UI_btn_email" OnClick="UI_btn_email_Click" />
                        </div>

                        <div id="e_Shop_MyInfo_Main_btn">
                            <asp:Button Text="Update Your Information" runat="server" ID="_update" CssClass="_btnLogin" OnClick="_update_Click1" />
                        </div>

                    </div>
                </ItemTemplate>
            </asp:DataList>
        </section>

    </section>
</asp:Content>


