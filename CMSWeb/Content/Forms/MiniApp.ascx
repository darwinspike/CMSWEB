<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MiniApp.ascx.cs" Inherits="CMSWeb.Content.Forms.MiniApp" %>

<link rel="stylesheet" type="text/css" href="../../Assets/CSS/MiniApp.css">

<div class="pull-right miniAppForm-wrapper">
    <!--[if lte IE 9]>
    <div class="css-browser-wrapper ie">
    <![endif]-->
    <div class="miniAppForm--content">
        <div class="miniAppForm--progressbar">
              <div class="miniAppForm--progressbar-progress"></div>
          </div>

          <h4>Mini Application</h4>
        

            <div id="miniapplicationform" class="miniAppForm">
                <asp:PlaceHolder runat="server" ID="formData">
                    <div class="js-actions-ie">
                        Your browser is outdated, please consider to update it with a newer version or try another browser. Recommendations: 
                        <ul>
                            <li>Google Chrome</li>
                            <li>Mozilla Firefox</li>
                            <li>Internet Explorer 9+</li>
                            <li>Apple Safari</li>
                        </ul>
                    </div>

                    <div class="quickApp--steps">

                        <div class="quickApp--step js-active">
                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">* Loan Purpose</span>
                                <select runat="server" ID="loanPurpose" aria-required="true" data-error="Loan Purpose Field Is required." width="100%">
                                    <option Value="">Select A Loan Purpose</option>
                                    <option Value="Refinance">Refinance</option>
                                    <option Value="Purchase">Purchase</option>
                                </select>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="loanPurpose" ValidationGroup="rform_mini">This field is required!</asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">* How much would you like to borrow?</span>


                                <select runat="server" ID="howMuch" aria-required="true" data-error="How much to borrow field is required." width="100%">
                                    <option Value="">Select A Value</option>
                                    <option Value="100000">$100,000.00</option>
                                    <option Value="150000">$150,000.00</option>
                                    <option Value="200000">$200,000.00</option>
                                    <option Value="250000">$250,000.00</option>
                                    <option Value="300000">$300,000.00</option>
                                    <option Value="350000">$350,000.00</option>
                                    <option Value="400000">$400,000.00</option>
                                    <option Value="450000">$450,000.00</option>
                                    <option Value="500000">$500,000.00</option>
                                    <option Value="550000">$550,000.00</option>
                                    <option Value="600000">$600,000.00</option>
                                    <option Value="650000">$650,000.00</option>
                                    <option Value="700000">$700,000.00</option>
                                    <option Value="750000">$750,000.00</option>
                                    <option Value="800000">$800,000.00</option>
                                    <option Value="850000">$850,000.00</option>
                                    <option Value="900000">$900,000.00</option>
                                    <option Value="950000">$950,000.00</option>
                                    <option Value="1000000">$1,000,000.00</option>
                                    <option Value="1050000">$1,050,000.00</option>
                                    <option Value="1100000">$1,100,000.00</option>
                                    <option Value="1150000">$1,150,000.00</option>
                                    <option Value="1200000">$1,200,000.00</option>
                                    <option Value="1250000">$1,250,000.00</option>
                                    <option Value="1300000">$1,300,000.00</option>
                                    <option Value="1350000">$1,350,000.00</option>
                                    <option Value="1400000">$1,400,000.00</option>
                                    <option Value="1450000">$1,450,000.00</option>
                                    <option Value="1500000">$1,500,000.00</option>
                                    <option Value="1550000">$1,550,000.00</option>
                                    <option Value="1600000">$1,600,000.00</option>
                                    <option Value="1650000">$1,650,000.00</option>
                                    <option Value="1700000">$1,700,000.00</option>
                                    <option Value="1750000">$1,750,000.00</option>
                                    <option Value="1800000">$1,800,000.00</option>
                                    <option Value="1850000">$1,850,000.00</option>
                                    <option Value="1900000">$1,900,000.00</option>
                                    <option Value="1950000">$1,950,000.00</option>
                                    <option Value="2000000">$2,000,000.00</option>
                                </select>

                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="howMuch" ValidationGroup="rform_mini">This field is required!</asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">* Credit Profile</span>

                                <select runat="server" ID="creditProfile" aria-required="true" data-error="Credit Profile Field is required." width="100%">
                                    <option Value="">Select A Credit Profile</option>
                                    <option Value="Excellent (740+)">Excellent (740+)</option>
                                    <option Value="Very Good (700-739)">Very Good (700-739)</option>
                                    <option Value="Good (650-699)">Good (650-699)</option>
                                    <option Value="Fair (580-649)">Fair (580-649)</option>
                                    <option Value="Other">Other</option>
                                </select>

                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="creditProfile" ValidationGroup="rform_mini">This field is required!</asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="error">
                            </div>

                            <div class="css-form-group css-form-actions">
                                <a href="javascript:;" class="css-button next-step">Continue</a>
                            </div>

                        </div>
                        <div class="quickApp--step">

                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">* First Name</span>

                                <input type="text"  runat="server" ID="firstName" aria-required="true" data-error="First Name field is required." placeholder="First Name"/>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ControlToValidate="firstName" ValidationGroup="rform_mini">This field is required!</asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">* Last Name</span>

                                <input type="text" runat="server" ID="lastName" aria-required="true" data-error="Last Name field is required." placeholder="Last Name"/>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="lastName" ValidationGroup="rform_mini">This field is required!</asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">* Email</span>

                                <input type="text"  runat="server" ID="email" aria-required="true" data-error="Email field is required. Also, make sure is a valid email address." CssClass="valid-email" placeholder="Email"/>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="email" ValidationGroup="rform_mini">This field is required!</asp:RequiredFieldValidator>--%>
                            </div>


                            <div class="error">
                            </div>

                             <div class="css-form-group css-form-actions">
                                  <a href="javascript:;" class="css-button prev-step with-icon"><i class="inline-icon __back-arrow"></i> Back</a>

                               <a href="javascript:;" class="css-button next-step">Continue</a>
                            </div>

                        </div>
                        <div class="quickApp--step">

                            <div class="css-form-group">
                                <span class="css-label">Phone Number</span>

                                <input type="text" runat="server" rel="phone" ID="phone" placeholder="Phone Number"/>
                            </div>
                            <div class="css-form-group" aria-required="true">
                                <span class="css-label">Zip Code</span>

                                <input type="text" runat="server" ID="zip" rel="zip" aria-required="true" data-error="Zip Code field is required." placeholder="Zip Code"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">How did you hear about us?</span>

                                <select  ID="ddlHowdidyouhearaboutus" runat="server" width="100%">
                                    <option Selected>How did you hear about us?</option>
                                    <option Value="Email">Email</option>
                                    <option Value="Facebook">Facebook</option>
                                    <option Value="Google">Google</option>
                                    <option Value="Yahoo">Yahoo</option>
                                    <option Value="Newspaper">Newspaper</option>
                                    <option Value="TV">TV</option>
                                    <option Value="Radio">Radio</option>
                                    <option Value="Yellow Pages">Yellow Pages</option>
                                    <option Value="Realtor">Realtor</option>
                                    <option Value="Mailer">Mailer</option>
                                    <option Value="Other">Other</option>
                                </select>



                            </div>
              

                            <div class="error">
                            </div>

                             <div class="css-form-group css-form-actions">
                                  <a href="javascript:;" class="css-button prev-step with-icon"><i class="inline-icon __back-arrow"></i> Back</a>

                                <%--<asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click1" />--%>
                                <input type="submit"  runat="server" OnClick="Submit_Click1" ID="Submit" class="css-button css-submit-button ladda-button lock-submit" data-color="red" data-style="contract-overlay" Style="z-index: 10;"/>

<%--                                    <span class="css-overlay"></span>
                                    <span class="ladda-label">Submit</span>
                                    <span class="ladda-spinner"></span>--%>
                                <%--</asp:LinkButton>--%>
                            </div>
                        </div>
            
                    </div>
                </asp:PlaceHolder>
            </div>

            <div>
                <asp:PlaceHolder runat="server" ID="thankYou" Visible="false">
                    <script>$('.miniAppForm--content h4').remove();
                        $(window).on('load', function () { $('html,body').animate({ scrollTop: $('.sweet-alert').offset().top - 40 }, 0); });</script>
                    <div class="sweet-overlay" style="display: block; opacity: 1"></div>
                    <!-- SweetAlert box -->
                    <div class="sweet-alert sweet-alert showSweetAlert visible" style="display: block">
                        <div class="icon success animate">
                            <span class="line tip animateSuccessTip"></span>
                            <span class="line long animateSuccessLong"></span>
                            <div class="placeholder"></div>
                            <div class="fix"></div>
                        </div>
                        <h2>Thank you
                            <asp:Label ID="Message" runat="server"></asp:Label>!</h2>
                        <p>A representative will contact you soon.</p>
                        <button class="confirm" type="button" onclick="$('.sweet-overlay').remove();$(this).remove();">Continue</button>
                    </div>
                </asp:PlaceHolder>
            </div>    

    </div>
        <!--[if lte IE 9]>
        </div>
        <![endif]-->
  </div>



<script type="text/javascript" src="../Assets/JS/MiniApp.js"></script>
