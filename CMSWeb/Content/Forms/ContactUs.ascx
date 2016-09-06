<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.ascx.cs" Inherits="CMSWeb.Content.Forms.ContactUs" %>



<link rel="stylesheet" type="text/css" href="../../Assets/CSS/ContactUs.css">

<div class="contactUsForm">
    <%--FORM--%>
    <asp:Panel ID="Panel1" runat="server" Visible="true">
        <!--[if lte IE 9]>
        <div class="css-browser-wrapper ie">
        <![endif]-->
        <span style="display: none">
            <asp:Label ID="errLabel" runat="server" Visible="false" ForeColor="red" Font-Bold="true" class="alert alert-warning" Style="display: block;" />
        </span>
        <div class="css-form-group col-md-6 col-xs-12" aria-required="true">
            <span class="css-label">* First Name</span>
            <input type="text" ID="FirstName" runat="server" aria-required="true" data-error="First Name field is required." placeholder="First Name"/>
        </div>
        <div class="css-form-group col-md-6 col-xs-12" aria-required="true">
            <span class="css-label">* Last Name</span>
            <input type="text" ID="LastName" runat="server" placeholder="Last Name" aria-required="true" data-error="Last Name field is required."/>
        </div>
        <div class="css-form-group col-md-6 col-xs-12">
            <span class="css-label">Address Line 1</span>
            <input type="text" ID="Address1" runat="server" placeholder="Address Line 1"/>
        </div>
        <div class="css-form-group col-md-6 col-xs-12">
            <span class="css-label">Address Line 2</span>
            <input type="text" ID="Address2" runat="server" placeholder="Address Line 2"/>
        </div>
        <div class="css-form-group col-md-6 col-xs-12">
            <span class="css-label">City</span>
            <input type="text" ID="City" runat="server" placeholder="City"/>
        </div>
        <div class="css-form-group col-md-6 col-xs-12">
            <span class="css-label">State</span>
                                <select ID="State" runat="server" class="form-control" Width="100%">
                                    <option Value="" Selected>Select State</option>
                                    <option Value="AL">Alabama</option>
                                    <option Value="AK">Alaska</option>
                                    <option Value="AZ">Arizona</option>
                                    <option Value="AR">Arkansas</option>
                                    <option Value="CA">California</option>
                                    <option Value="CO">Colorado</option>
                                    <option Value="CT">Connecticut</option>
                                    <option Value="DE">Delaware</option>
                                    <option Value="District of Columbia">DC</option>
                                    <option Value="FL">Florida</option>
                                    <option Value="GA">Georgia</option>
                                    <option Value="HI">Hawaii</option>
                                    <option Value="ID">Idaho</option>
                                    <option Value="IL">Illinois</option>
                                    <option Value="IN">Indiana</option>
                                    <option Value="IA">Iowa</option>
                                    <option Value="KS">Kansas</option>
                                    <option Value="KY">Kentucky</option>
                                    <option Value="LA">Louisiana</option>
                                    <option Value="ME">Maine</option>
                                    <option Value="MD">Maryland</option>
                                    <option Value="MA">Massachusetts</option>
                                    <option Value="MI">Michigan</option>
                                    <option Value="MN">Minnesota</option>
                                    <option Value="MS">Mississippi</option>
                                    <option Value="MO">Missouri</option>
                                    <option Value="MT">Montana</option>
                                    <option Value="NE">Nebraska</option>
                                    <option Value="NV">Nevada</option>
                                    <option Value="NH">New Hampshire</option>
                                    <option Value="NJ">New Jersey</option>
                                    <option Value="NM">New Mexico</option>
                                    <option Value="NY">New York</option>
                                    <option Value="NC">North Carolina</option>
                                    <option Value="ND">North Dakota</option>
                                    <option Value="OH">Ohio</option>
                                    <option Value="OK">Oklahoma</option>
                                    <option Value="OR">Oregon</option>
                                    <option Value="PA">Pennsylvania</option>
                                    <option Value="RI">Rhode Island</option>
                                    <option Value="SC">South Carolina</option>
                                    <option Value="SD">South Dakota</option>
                                    <option Value="TN">Tennessee</option>
                                    <option Value="TX">Texas</option>
                                    <option Value="UT">Utah</option>
                                    <option Value="VT">Vermont</option>
                                    <option Value="VA">Virginia</option>
                                    <option Value="WA">Washington</option>
                                    <option Value="WV">West Virginia</option>
                                    <option Value="WI">Wisconsin</option>
                                    <option Value="WY">Wyoming</option>
                                </select>
        </div>
        <div class="css-form-group col-md-4 col-xs-12" style="clear: left">
            <span class="css-label">Zip Code</span>
            <input type="text" ID="Zip" runat="server" rel="zip" aria-required="true" data-error="Zip Code field is required" placeholder="Zip Code"/>
        </div>
        <div class="css-form-group col-md-4 col-xs-12" aria-required="true">
            <span class="css-label">* Phone Number</span>
            <input type="text" ID="Phone" runat="server" aria-required="true" data-error="Phone Number field is required." placeholder="Phone Number" rel="phone"/>
        </div>
        <div class="css-form-group col-md-4 col-xs-12">
            <span class="css-label">Fax Number</span>
            <input type="text" ID="Fax" runat="server" placeholder="Fax Number" rel="phone"/>
        </div>
        <div class="form-group col-md-12" aria-required="true">
            <span class="css-label">* E-mail Address</span>
            <input type="text" ID="Email" runat="server" placeholder="E-Mail Address" aria-required="true" data-error="Email Field is required and must be a valid email address" class="valid-email"/>
        </div>
        <div class="css-form-group css-clearfix col-md-12">
            <span class="css-label">Notes</span>
            <input type="text" ID="Notes" runat="server" TextMode="MultiLine" Rows="4" placeholder="Notes" />
        </div>
        <div class="css-form-group col-md-5 col-xs-12">
            <span class="css-label">* Anti-Robot Validation Code</span>
            <input type="Image" ID="captchaimg1" runat="server" class="img-responsive img-rounded" />
        </div>
        <div class="form-group col-md-7 col-xs-12" aria-required="true">
            <span class="css-label">&nbsp;</span>
            <input type="text" ID="CodeNumberTextBox" aria-required="true" data-error="Anti-Robot code is required." runat="server" Text="" Rows="3" placeholder="Enter the code shown on left" class="form-control" />
        </div>
        <div class="error"></div>
        <div class="css-form-group col-md-12">
            <input type="submit" runat="server" OnClick="Submit_Click" ID="Submit" class="css-button css-submit-button ladda-button lock-submit" data-color="red" data-style="contract-overlay" Style="z-index: 10;" />                
        </div>
        <!--[if lte IE 9]>
        </div>
        <![endif]-->
    </asp:Panel>
    <asp:Label ID="Message" runat="server"></asp:Label>
    <asp:Panel ID="Panel2" runat="server" Visible="false">
        <div class="sweet-overlay" style="display: block; opacity: 1"></div>
        <div class="sweet-alert sweet-alert showSweetAlert visible" style="display: block">
            <div class="icon success animate">
                <span class="line tip animateSuccessTip"></span>
                <span class="line long animateSuccessLong"></span>
                <div class="placeholder"></div>
                <div class="fix"></div>
            </div>
            <h2>Good Job!</h2>
            <p>Your contact information has been sent!</p>
            <button class="confirm" type="button" onclick="window.location.href = '<%= link %>';">Continue</button>
        </div>
    </asp:Panel>
</div>

<script type="text/javascript" src="../../Assets/JS/ContactUs.js"></script>
