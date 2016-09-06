<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Careers.ascx.cs" Inherits="CMSWeb.Content.Forms.Careers" %>

<link rel="stylesheet" type="text/css" href="../../Assets/CSS/Careers.css">

<!--[if lte IE 9]>
<div class="css-browser-wrapper ie">
<![endif]-->

<div class="careersFormWrapper">
    <div class="form-container careersFormContainer">
        <h2>Apply for a Job</h2>
        <% if (Request.Files.AllKeys.Length > 0 && Request.Files[0] != null)
           {
               Response.Write(@"<div class='error-alert show'><div class='custom-error'>Your request has some errors, 
               for this reason the files you attached before were lost, If you attached some files you 
               have to re-attatch them. Thanks</div></div>");
           } %>
        <div class="careersFormComponents">
            <div class="js-actions-ie">
                Your browser is outdated, please consider to update it with a newer version or try another browser. Recommendations: 
                <ul>
                    <li>Google Chrome</li>
                    <li>Mozilla Firefox</li>
                    <li>Internet Explorer 9+</li>
                    <li>Apple Safari</li>
                </ul>
            </div>
        </div>
        <div class="__careers--apply-form careersForm" id="apply-form">
            <div class="css-content-wrapper">
                <div class="careersApplicationForm">
                    <asp:Panel ID="formData" runat="server">
                        <div class="css-wrapper">
                            <div class="css-form-group">
                                <span class="css-label">Career Position</span>
                                <select ID="CareerPositions" runat="server" class="form-control" Width="100%">
                                    <option Value="" Selected>Select Position</option>
                                    <option Value="Management">Management</option>
                                    <option Value="Mortgage Loan Officer">Mortgage Loan Officer</option>
                                    <option Value="Loan Processor">Loan Processor</option>
                                    <option Value="Underwriter">Underwriter</option>
                                </select>

                            </div>
                            
                            <div class="css-form-group">
                                <span class="css-label" style="font-weight: bold">* First Name</span>                                 
                                <input type="text" runat="server" aria-required="true" data-error="First Name field is required." ID="FirstName" placeholder="First Name" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label" style="font-weight: bold">* Last Name</span>
                                <input type="text" runat="server" aria-required="true" data-error="Last Name field is required." ID="LastName" placeholder="Last Name" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">Address 1</span>
                                <input type="text" runat="server" ID="Address1" placeholder="Address 1" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">Address 2</span>
                                <input type="text" runat="server" ID="Address2" placeholder="Address 2" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">City</span>
                                <input type="text" runat="server" ID="City" placeholder="City" class="form-control"/>
                            </div>
                            <div class="css-form-group">
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
                            <div class="css-form-group">
                                <span class="css-label">Zip</span>
                                <input type="text" runat="server" rel="zip" ID="Zip" placeholder="Zip" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label" style="font-weight: bold">* Phone</span>
                                <input type="text" runat="server" rel="phone" ID="Phone" aria-required="true" data-error="Phone Number Field is Required." placeholder="Phone" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">Fax</span>
                                <input type="text" runat="server" ID="Fax" placeholder="Fax" class="form-control"/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label" style="font-weight: bold">* E-Mail</span>
                                <input type="text" runat="server" aria-required="true" ID="Email" placeholder="E-Mail" Class="form-control valid-email" data-error="Email Field is Required. Make Sure Is a Valid Email Address."/>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">Years of Experience</span>
                                <select ID="YearsOfExperience1" runat="server" class="form-control" Width="100%">
                                    <option  Value="" Selected>How many years of industry experience do you have?</option>
                                    <option  Value="0-1 years">0-1 years</option>
                                    <option  Value="2-3 years">2-3 years</option>
                                    <option  Value="4-5 years">4-5 years</option>
                                    <option  Value="5+ years">5+ years</option>
                                </select>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">Licence</span>
                                <select ID="Licensed1" runat="server" class="form-control" Width="100%">
                                    <option Value="" Selected >Are you currently licensed?</option>
                                    <option Value="Yes">Yes</option>
                                    <option Value="No">No</option>
                                    <option Value="In progress">In progress</option>
                                </select>
                            </div>
                            <div class="css-form-group">
                                <span class="css-label">Notes</span>
                                <input type="text" ID="Notes" runat="server" TextMode="MultiLine" Style="height: 90px; width: 98%;" class="form-control" />
                            </div>
                            <div class="css-form-group col-md-5 col-xs-12" aria-required="true">
                                <span class="css-label">* Anti-Robot Validation Code</span>
                                <img ID="captchaimg1" runat="server" class="img-responsive img-rounded" />                           
                            </div>
                            <div class="css-form-group col-md-7 col-xs-12">
                                <span class="css-label">&nbsp;</span>
                                <input type="text" ID="CodeNumberTextBox" aria-required="true" data-error="Anti-Robot Code Is Required." runat="server" Text="" Rows="3" placeholder="Enter the code shown on left" class="form-control" />
                            </div>
                            <div class="css-form-group css-clearfix checbox-hasFiles">
                                <label class="css-label">Check it: &nbsp; 
                                <input type="checkbox" ID="IsAttachments" Class="HasAttachments" runat="server" /> &nbsp; if you like to attach file(s) to the form.</label>
                            </div>
                            <div class="file-uploads">
                                <div class="file-uploads-wrapper" id="uploadFileDiv">
                                    <div class="file-uploads-content">
                                        <h4>Attachments</h4>
                                        <div class="files-upload">
                                            <div class="choose-files"></div>
                                            <div class="form-group add-file-wrapper">
                                                <p>Maximun file size per file is 3MB. Maximun number of files you can attach is 10. Allowed file formats are: jpg, jpeg, gif, png, bmp, pdf, doc, docx.</p>
                                                <button type="button" class="css-button show-if-has-files add-file" id="btnAddFileInner">Choose File</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">

                                <input type="submit" runat="server" OnClick="SubmitBtn_Click" ID="Submit1" class="css-button css-submit-button ladda-button lock-submit" data-color="red" data-style="contract-overlay" Style="z-index: 10;" Value="Submit"/>
<%--                                <asp:LinkButton runat="server" OnClick="SubmitBtn_Click" ID="Submit1" class="css-button css-submit-button ladda-button lock-submit" data-color="red" data-style="contract-overlay" Style="z-index: 10;">
                                    <span class="css-overlay"></span>
                                    <span class="ladda-label">Submit</span>
                                    <span class="ladda-spinner"></span>
                                </asp:LinkButton>--%>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <asp:Panel ID="Panel2" runat="server" Visible="false">
                    <div class="sweet-overlay" style="display: block; opacity: 1"></div>
                    <div class="sweet-alert sweet-alert showSweetAlert visible" style="display: block">
                        <div class="icon success animate">
                            <span class="line tip animateSuccessTip"></span>
                            <span class="line long animateSuccessLong"></span>
                            <div class="placeholder"></div>
                            <div class="fix"></div>
                        </div>
                        <h2>Good Job <asp:Label ID="Message" runat="server"></asp:Label>!</h2>
                        <p>Your information has been sent!</p>
                        <button class="confirm" type="button" onclick="window.location.href = '<%= CMSWeb.Models.Handler.URL.GetLink() %>';">Continue</button>
                    </div>
                </asp:Panel>
            </div>
            <div class="b-error"></div>
            <div class="error erros-console">
                <div class="custom-error">
                    <asp:Label ID="errLabel" runat="server" Visible="false" />
                </div>
            </div>
        </div>
    </div>
</div>

<!--[if lte IE 9]>
</div>
<![endif]-->

<script type="text/html-template" id="mytemplate">
    <div class="file-row no-file">
        <a class="remove-file pull-right" href="javascript:;">&times;</a>
        <div class="choose--file">
            <button class="css-button" type="button">
                <span>Choose a file to upload</span>
                <input type="file" name="archivo[]" runat="server" />
            </button>
        </div>
        <div class="file-name file-info"></div>
    </div>
</script>

<script type="text/javascript" src="../../Assets/JS/Careers.js"></script>
