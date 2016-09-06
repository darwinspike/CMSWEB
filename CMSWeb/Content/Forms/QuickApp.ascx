<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickApp.ascx.cs" Inherits="CMSWeb.Content.Forms.QuickApp" %>


<link rel="stylesheet" type="text/css" href="../../Assets/CSS/QuickApp.css">

<!--[if lte IE 9]>
    <div class="css-browser-wrapper ie">
    <![endif]-->
    <div class="quickAppForm--content col-sm-12">
        <div class="quickAppForm--progressbar">
              <div class="quickAppForm--progressbar-progress"></div>
          </div>

          <h4>Quick Contact</h4>
        

        <div id="quickapplicationform" class="quickAppForm">
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
                            <Select runat="server" aria-required="true" data-error="Loan Purpose Field Is Required" ID="loanPurpose" width="100%">
                                <option Value="">Select A Loan Purpose</option>
                                <option Value="Refinance">Refinance</option>
                                <option Value="Purchase">Purchase</option>
                            </Select>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="loanPurpose" ValidationGroup="rform_hp">This field is required!</asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="css-form-group" aria-required="true">
                            <span class="css-label">* Credit Profile</span>
                            <select runat="server" aria-required="true" data-error="Credit Profile Field Is required" ID="creditProfile" width="100%">
                                <option Value="">Select A Credit Profile</option>
                                <option Value="Excellent (740+)">Excellent (740+)</option>
                                <option Value="Very Good (700-739)">Very Good (700-739)</option>
                                <option Value="Good (650-699)">Good (650-699)</option>
                                <option Value="Fair (580-649)">Fair (580-649)</option>
                                <option Value="Other">Other</option>
                            </select>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="creditProfile" ValidationGroup="rform_hp">This field is required!</asp:RequiredFieldValidator>--%>

                            <div class="error">
                            </div>

                             <div class="css-form-group css-form-actions">
                                 <a href="javascript:;" class="css-button next-step">Continue</a>
                            </div>

                        </div>

                    </div>

                    <div class="quickApp--step">

                        <div class="css-form-group" aria-required="true">
                            <span class="css-label">* First Name</span>
                            <input type="text" runat="server" aria-required="true" data-error="First Name Field is Required." ID="firstName" placeholder="First Name" />
                        </div>
                        <div class="css-form-group" aria-required="true">
                            <span class="css-label">* Last Name</span>
                            <input type="text" runat="server" aria-required="true" data-error="Last Name Field Is Required." ID="lastName" placeholder="Last Name" />
                        </div>
                        <div class="css-form-group" aria-required="true">
                            <span class="css-label">* Email</span>
                            <input type="text" runat="server" aria-required="true" data-error="Email Field Is Required." ID="email" CssClass="valid-email" placeholder="Email" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Phone Number</span>
                            <input type="text" runat="server" rel="phone" ID="phone" placeholder="Phone Number" />
                        </div>

                        <div class="error">
                        </div>

                         <div class="css-form-group css-form-actions">
                              <a href="javascript:;" class="css-button prev-step with-icon"><i class="inline-icon __back-arrow"></i> Back</a>

                            <%--<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />--%>
                            <input type="submit" runat="server" OnClick="Button1_Click" ID="Button1" class="css-button css-submit-button ladda-button lock-submit" data-color="red" data-style="contract-overlay" Style="z-index: 10;" />
<%--                                <span class="css-overlay"></span>
                                <span class="ladda-label">Submit</span>
                                <span class="ladda-spinner"></span>
                            </asp:LinkButton>--%>
                        </div>

                    </div>
            


           
           
                </div>
            </asp:PlaceHolder>

            <asp:PlaceHolder runat="server" ID="thankYou" Visible="false">
                <script>$('.quickAppForm--content h4').remove();</script>
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


<script type="text/javascript" src="../../Assets/JS/QuickApp.js"></script>
