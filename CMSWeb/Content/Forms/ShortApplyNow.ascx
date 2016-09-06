<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShortApplyNow.ascx.cs" Inherits="CMSWeb.Content.Forms.ShortApplyNow" %>



<link rel="stylesheet" type="text/css" href="../../Assets/CSS/ShortApplyNow.css">

<!--[if lte IE 9]>
<div class="css-browser-wrapper ie">
<![endif]-->

<div class="shortApplicationForm">
    <asp:PlaceHolder runat="server" ID="formData">
        <div class="css-wrapper">
            <div class="js-actions-ie">
                Your browser is outdated, please consider to update it with a newer version or try another browser. Recommendations: 
                <ul>
                    <li>Google Chrome</li>
                    <li>Mozilla Firefox</li>
                    <li>Internet Explorer 9+</li>
                    <li>Apple Safari</li>
                </ul>
            </div>

            <div class="mav-plug b" id="tabs">
                <%--STEP 1--%>
                <div class="mav-content css-form-step active" id="part1b">
                    <div class="mav-wrap">
                        <h2>Step 1: Personal Information</h2>
                        <div class="css-form-group" aria-required="true">
                            <span class="css-label" style="font-weight: bold">* First Name</span>
                            <input type="text" runat="server" ID="firstName" placeholder="* First Name" class="form-control" data-error="Please Fill First Name Field" aria-required="true" />
                        </div>
                        <div class="css-form-group" aria-required="true">
                            <span class="css-label" style="font-weight: bold">* Last Name</span>
                            <input type="text" runat="server" ID="lastName" placeholder="* Last Name" class="form-control" data-error="Please Fill Last Name Field" aria-required="true" />
                        </div>
                        <div class="css-form-group" aria-required="true">
                            <span class="css-label" style="font-weight: bold">* Email</span>
                            <input type="text" runat="server" ID="email" placeholder="* Email" CssClass="valid-email" data-error="Invalid email Address" class="form-control" aria-required="true" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Home Phone</span>
                            <input type="text" runat="server" ID="phone" placeholder="Home Phone" rel="phone" class="form-control" />
                        </div>
                        <span class="error"></span>
                    </div>
                    <div class="mav-controller js-actions">
                        <a href="javascript:;"><span class="css-button" data-direction="next" data-validate-part="#part1b">Next</span></a>
                    </div>
                </div>

                <%--STEP 2--%>
                <div class="mav-content css-form-step" id="part2b">
                    <div class="mav-wrap">
                        <h2>Step 2: Loan Information</h2>
                        <div class="css-form-group">
                            <span class="css-label">Loan Purpose</span>
                            <select ID="edtAPP_LOAN_PURPOSE" runat="server" class="LoanProgram" Style="width: 100%">
                                <option Value="" Selected>Select Loan Purpose</option>
                                <option Value="Purchase">Purchase</option>
                                <option Value="Refinance"></option>
                                <option Value="Construction">Construction</option>
                                <option Value="Construction - Perm">Construction - Perm</option>
                                <option Value="Other">Other</option>
                            </select>
                        </div>
                        <div id="refinance_purpose_options" class="custom_hide css-form-group" style="">
                            <span class="css-label">Refinance Loan Purpose</span>
                            <select ID="edtAPP_REF_LOAN_PURPOSE" runat="server" class="LoanProgram form-control" Style="width: 100%">
                                <option Value="Cash-Out">Cash-Out</option>
                                <option Value="No Cash-Out" Selected>No Cash-Out</option>
                            </select>
                        </div>
                        <div class="css-form-group custom_hide">
                            <span class="css-label">Other Loan Purpose</span>
                            <input type="text" ID="LoanPurposeOther" runat="server" placeholder="Other Loan Purpose" class="form-control" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Property Value $</span>
                            <input type="text" runat="server" ID="PropertyEstimatedValue" rel="dollars" placeholder="Property Value $" class="form-control" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Credit:</span>
                            <div class="css-block">
                               
                                <input type="radio" ID="excellent" runat="server" Text="Excellent" GroupName="Credit" Checked="True" />
                            </div>
                            <div class="css-block">
                                <input type="radio" ID="good" runat="server" Text="Good" GroupName="Credit" />
                            </div>
                            <div class="css-block">
                                <input type="radio" ID="fair" runat="server" Text="Fair" GroupName="Credit" />
                            </div>
                            <div class="css-block">
                                <input type="radio" ID="NotSure" runat="server" Text="Not sure" GroupName="Credit" />
                            </div>
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Loan Program</span>
                            <select ID="ddlLoanProgram" runat="server" class="LoanProgram form-control" Style="width: 100%">
                                <option Value="" Selected>Select Loan Program</option>
                                <option Value="Unsure">Unsure</option>
                                <option Value="Conforming 30 year fixed">Conforming 30 year fixed</option>
                                <option Value="Conforming 30 year fixed biweekly">Conforming 30 year fixed biweekly</option>
                                <option Value="Conforming 20 year fixed">Conforming 20 year fixed</option>
                                <option Value="Conforming 15 year fixed">Conforming 15 year fixed</option>
                                <option Value="Conforming 15 year fixed biweekly">Conforming 15 year fixed biweekly</option>
                                <option Value="Conforming 10/1 ARM">Conforming 10/1 ARM</option>
                                <option Value="Conforming 30/7 or 7/1 ARM">Conforming 30/7 or 7/1 ARM</option>
                                <option Value="Conforming 30/5 or 5/1 ARM">Conforming 30/5 or 5/1 ARM</option>
                                <option Value="Conforming 3/1 ARM">Conforming 3/1 ARM</option>
                                <option Value="Conforming Adjustable">Conforming Adjustable</option>
                                <option Value="Conforming 40 year fixed">Conforming 40 year fixed</option>
                                <option Value="Conforming 10 year fixed">Conforming 10 year fixed</option>
                                <option Value="Conforming 25 year fixed">Conforming 25 year fixed</option>
                                <option Value="Jumbo 30 year fixed">Jumbo 30 year fixed</option>
                                <option Value="Jumbo 30 year fixed biweekly">Jumbo 30 year fixed biweekly</option>
                                <option Value="Jumbo 20 year fixed">Jumbo 20 year fixed</option>
                                <option Value="Jumbo 15 year fixed">Jumbo 15 year fixed</option>
                                <option Value="Jumbo 15 year fixed biweekly">Jumbo 15 year fixed biweekly</option>
                                <option Value="Jumbo 10/1 ARM">Jumbo 10/1 ARM</option>
                                <option Value="Jumbo 7/1 ARM">Jumbo 7/1 ARM</option>
                                <option Value="Jumbo 5/1 ARM">Jumbo 5/1 ARM</option>
                                <option Value="Jumbo 3/1 ARM">Jumbo 3/1 ARM</option>
                                <option Value="Jumbo Adjustable">Jumbo Adjustable</option>
                                <option Value="Jumbo 40 year fixed">Jumbo 40 year fixed</option>
                                <option Value="Jumbo 10 year fixed">Jumbo 10 year fixed</option>
                                <option Value="Jumbo 25 year fixed">Jumbo 25 year fixed</option>
                                <option Value="Conforming 1 month COFI ARM">Conforming 1 month COFI ARM</option>
                                <option Value="Conforming 3 month COFI ARM">Conforming 3 month COFI ARM</option>
                                <option Value="Conforming 6 mo CD ARM">Conforming 6 mo CD ARM</option>
                                <option Value="Conforming 1 year tbill ARM">Conforming 1 year tbill ARM</option>
                                <option Value="Jumbo 1 month COFI ARM">Jumbo 1 month COFI ARM</option>
                                <option Value="Jumbo 3 month COFI ARM">Jumbo 3 month COFI ARM</option>
                                <option Value="Jumbo 1 year tbill ARM">Jumbo 1 year tbill ARM</option>
                            </select>
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Loan Amount</span>
                            <input type="text" runat="server" rel="dollars" ID="LoanAmount" placeholder="Loan Amount You Are Applying For:* $" class="form-control" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Down Payment</span>
                            <input type="text" runat="server" rel="digits" ID="LoanDownPayment" placeholder="Or % Down Payment" class="form-control" />
                        </div>
                    </div>
                    <div class="mav-controller js-actions">
                        <a href="javascript:;"><span class="css-button" data-direction="back">Back</span></a>
                        <a href="javascript:;"><span class="css-button" data-direction="next" data-validate-part="#part2b">Next</span></a>
                    </div>
                </div>

                <%--STEP 3--%>
                <div class="mav-content css-form-step" id="part3b">
                    <div class="mav-wrap">
                        <h2>Step 3: Property Information</h2>
                        <div class="css-form-group">
                            <span class="css-label">Street Address</span>
                            <input type="text" runat="server" ID="StreetAddress" placeholder="Street Address" class="form-control" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">City</span>
                            <input type="text" runat="server" ID="city" placeholder="City" class="form-control" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">State</span>
                            <select ID="ddlstate" runat="server" Style="width: 100%" class="LoanProgram form-control">
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
                        <div class="css-form-group">
                            <span class="css-label">Zip</span>
                            <input type="text" runat="server" rel="zip" ID="zip" placeholder="Zip" class="form-control" />
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Property Type</span>
                            <select ID="ddlPropertyType" Style="width: 100%" runat="server" class="LoanProgram form-control">
                                <option Value="" Selected>Select Property Type</option>
                                <option Value="Single family">Single family</option>
                                <option Value="Condominium">Condominium</option>
                                <option Value="Townhouse">Townhouse</option>
                                <option Value="Cooperative">Cooperative</option>
                                <option Value="Duplex">Duplex</option>
                                <option Value="Triplex">Triplex</option>
                                <option Value="Fourplex">Fourplex</option>
                                <option Value="Planned Unit Development">Planned Unit Development</option>
                                <option Value="Office">Office</option>
                                <option Value="Warehouse">Warehouse</option>
                                <option Value="Apartment Bldg.">Apartment Bldg.</option>
                                <option Value="Industrial">Industrial</option>
                                <option Value="Retail">Retail</option>
                                <option Value="Restaurant">Restaurant</option>
                                <option Value="Other">Other</option>
                            </select>
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">Property Will Be</span>
                            <Select ID="ddlPropertyWillBe" Style="width: 100%" runat="server" class="LoanProgram form-control">
                                <option Value="" Selected>Select Property Use</option>
                                <option Value="Primary Residence">Primary Residence</option>
                                <option Value="Secondary Residence">Secondary Residence</option>
                                <option Value="Investment (rental)">Investment (rental)</option>
                            </Select>
                        </div>
                        <div class="css-form-group">
                            <span class="css-label">How did you hear about us?</span>
                            <select ID="ddlHowdidyouhearaboutus" runat="server" Style="width: 100%" class="LoanProgram form-control">
                                <option Value="" Selected>How did you hear about us?</option>
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
                        <div class="css-form-group">
                            <asp:Panel ID="pnlConsultant" runat="server">
                                <span class="css-label">Are You Working With A Mortgage Advisor?</span>
                                <input type="hidden" ID="hdfConsultant" Value="true" runat="server" />
                                <select ID="drpMortgageSpecialist1" Style="width: 100%" runat="server" Class="LoanType form-control" data-required="true" DataSourceID="SqlDataSource1" DataTextField="FullName" DataValueField="Consultant_ID" OnDataBound="DropDownList1_DataBound">
                                </select>
                            </asp:Panel>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:propertyinfoConnectionString %>" OnSelecting="SqlDataSource1_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="SITE_ID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="mav-controller js-actions">
                        <a href="javascript:;"><span class="css-button" data-direction="back">Back</span></a>
                        <input type="submit" runat="server" OnClick="submit_Click" ID="LinkButton78" class="css-button css-submit-button ladda-button lock-submit" data-color="red" data-style="contract-overlay" Style="z-index: 10;">
                    </div>
                </div>

                <br style="clear: both">
            </div>

            <br style="clear: both">

            <div class="css-clear clearfix">
            </div>
        </div>
    </asp:PlaceHolder>

    <%--THANK YOU--%>
    <asp:PlaceHolder runat="server" ID="thankYou" Visible="false">
        <script>
            $('.paginator').remove(); $('.sweet-alert').closest('.container').find('div').first().css({ minHeight: '120px' });
            $(".shortApplicationForm").addClass("thankyou-page");
        </script>

        <div class="sweet-overlay" style="display: block; opacity: 1"></div>

        <!-- SweetAlert box -->
        <div class="sweet-alert sweet-alert showSweetAlert visible" style="display: block">
            <div class="icon success animate">
                <span class="line tip animateSuccessTip"></span>
                <span class="line long animateSuccessLong"></span>
                <div class="placeholder"></div>
                <div class="fix"></div>
            </div>
            <h2>Good Job
                <asp:Label ID="Message" runat="server"></asp:Label>!</h2>

            <% if (los || drpMortgageSpecialist1.SelectedIndex > 1)
               { %>
            <p>Your information has been sent to:</p>

            <div class="css-lo-card">

                <% if (los)
                   { %>
                <div class="css-lo-picture">
                    <img src="<%= CMSWeb.Models.Consumable.Consultants.Data().Image %>">
                </div>
                <div class="css-lo-name"><%= CMSWeb.Models.Consumable.Consultants.Data().FullName %></div>
                <div class="css-lo-title"><%= CMSWeb.Models.Consumable.Consultants.Data().Title %></div>
                <% }
                   else if (drpMortgageSpecialist1.SelectedIndex > 1)
                   { %>
                <div class="css-lo-name">
                    <asp:Label ID="ConsultantName" runat="server"></asp:Label>
                </div>
                <% } %>
            </div>
            <% }
               else
               { %>
            <p>Your information has been sent!</p>
            <% } %>

            <button class="css-confirm" type="button" onclick="window.location.href = '<%= link %>';">Continue</button>
        </div>
    </asp:PlaceHolder>
</div>

<div class="addSpace">
</div>

<!--[if lte IE 8]>
</div>
<![endif]-->

<script type="text/javascript" src="../../Assets/JS/ShortApplyNow.js"></script>
