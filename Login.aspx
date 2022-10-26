<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Login" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
    <section id="">

        <div id="Login_Div">
            <asp:MultiView runat="server" ID="_mWizard">
                <asp:View runat="server" ID="_vLogin">

                    <section class="Login-Input">

                        <section id="Login-header">

                            <h1 id="Login-h1">Sign In</h1>

                        </section>

                        <section id="Login-id">

                            <i class="fa fa-user icon"></i>

                            <asp:TextBox runat="server" ID="_LloginID" placeholder=" Input Your Login ID" CssClass="Login_textbox" />
                           
                        </section>
                        <section id="Login-pass">
                            <i class="fa fa-key" aria-hidden="true"></i>
                            <asp:TextBox runat="server" ID="_Lpassword" placeholder="Input Your Password" CssClass="Login_password" TextMode="Password" />
                           
                        </section>
                        <section id="Login-btns">
                            <asp:Button Text="Login" runat="server" ID="_Llogin" CssClass="_btnLogin" OnClick="_Llogin_Click" />
                            <asp:Label Text="" runat="server" ID="_lbl_failLogin" />

                        </section>
                        <section id="Login-btns2">
                            <asp:Button Text="Sign Up" runat="server" ID="Button3" CssClass="_btnPass" OnClick="_signUp_Click" />

                        </section>
                        <section id="Login-sec">
                            <pre>If you want to sell item through this website
contact zzangzwh@hotmail.com please</pre>
                        </section>
                    </section>
                </asp:View>
                <asp:View runat="server" ID="_vSignUP">
                    <section class="Sign-Input2">

                        <section id="Sign-header">

                            <h1 id="Sign-h1">SIGN UP</h1>

                        </section>

                        <section id="Sign-check">
                            <asp:TextBox runat="server" ID="_SuserID" placeholder="Input Your UserID" CssClass="signUp_" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input User ID" Display="Dynamic"
                                ControlToValidate="_SuserID" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server"
                                ErrorMessage="Only allowed letters and numbers length 5-18"
                                ControlToValidate="_SuserID"
                                ValidationExpression="[A-Za-z\d]{5,18}"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>


                        </section>
                        <section id="Sign-fname">
                            <asp:TextBox runat="server" ID="_SfirstName" placeholder="Input Your First Name" CssClass="signUp_" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                runat="server"
                                ErrorMessage="Only allowed letters"
                                ControlToValidate="_SfirstName"
                                ValidationExpression="[A-Za-z]+"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input User First name" Display="Dynamic"
                                ControlToValidate="_SfirstName" ForeColor="Red"></asp:RequiredFieldValidator>

                        </section>
                        <section id="Sign-lname">
                            <asp:TextBox runat="server" ID="_SlastName" placeholder="Input Your Last Name" CssClass="signUp_" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                runat="server"
                                ErrorMessage="Only allowed letters"
                                ControlToValidate="_SlastName"
                                ValidationExpression="[A-Za-z]+"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input User Last name" Display="Dynamic"
                                ControlToValidate="_SlastName" ForeColor="Red"></asp:RequiredFieldValidator>
                        </section>
                        <section id="Sign-pass">
                            <asp:TextBox runat="server" ID="_Spassword" placeholder="Input Password" CssClass="signUp_" TextMode="Password"  />
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
                        </section>

                        <section id="Sign-passC">
                            <asp:TextBox runat="server" ID="_SconfirmPassword" placeholder="Confim your Password" CssClass="signUp_" TextMode="Password" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Your password doesnt Match" ForeColor="Red" Display="Dynamic" ControlToCompare="_Spassword" ControlToValidate="_SconfirmPassword"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Comfirm Password" Display="Dynamic" ControlToValidate="_SconfirmPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                        </section>
                        <section id="Sign-phone">
                            <asp:TextBox runat="server" ID="_SphoneNum" placeholder="Input your Phone Number" CssClass="signUp_" />
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
                        </section>
                        <section id="Sign-post">
                            <asp:TextBox runat="server" ID="_SpostCode" placeholder="Input your Post-Code" CssClass="signUp_" />
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
                        </section>
                        <section id="Sign-email">
                            <asp:TextBox runat="server" ID="_Semail" placeholder="Input your Email" CssClass="signUp_" />
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
                        </section>
                        <section id="Sign-address">
                            <asp:TextBox runat="server" ID="_Saddress" placeholder="Input your Address" CssClass="signUp_" />
                        </section>

                        <section id="SignUP-btns">
                            <asp:Button Text="Sign Up" runat="server" ID="_SuccessSingIn" CssClass="_btnLogin" OnClick="_SuccessSingIn_Click" />
                        </section>


                    </section>
                </asp:View>
            </asp:MultiView>


        </div>

    </section>
</asp:Content>


