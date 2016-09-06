<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApplyNow.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Content.ApplyNow" %>



<%@ Register TagPrefix="uc" TagName="Full" Src="../../../../Content/Forms/FullApplyNow.ascx" %>
<%@ Register TagPrefix="uc" TagName="Short" Src="../../../../Content/Forms/ShortApplyNow.ascx" %>

<div class="page">
    <div class="contain2">
        <div class="wrapper">
            <span class="clearfix"></span>
            <% if (Request.QueryString["type"] == "full")
               { %>
            <uc:Full ID="FullApp" runat="server" />
            <% }
               else if (Request.QueryString["type"] == "short")
               { %>
            <uc:Short ID="ShortApp" runat="server" />
            <% }
               else
               { %>
            <div class="container-inside">
                <br />
                <h2>
                    <span style="color: #798382;">Get a Quote Today!</span>
                </h2>
                <p>Our online mortgage inquiry form is an easy and fast way to request more information.</p>
                <div class="addSpace">&nbsp;</div>
                <div class="row">
                    <div class="col-md-4 col-sm-4">
                        <div class="col-md-12 col-sm-12 col-colored">
                            <h3>Quick Inquiry</h3>
                            <p class="text-primary">Complete our quick application online.</p>
                            <p class="text-muted">
                                <small>Take 30 seconds</small>
                            </p>
                            <div class="addSpace">&nbsp;</div>
                            <a class="btnct" href="<%= link %>&amp;content=<%= (String.IsNullOrEmpty(Request.QueryString["content"]) ? "ApplyNow" : Request.QueryString["content"]) %>&amp;type=short">Apply Now</a>
                            <span class="clearfix">&nbsp;</span>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <div class="col-md-12 col-sm-12 col-colored">
                            <h3>Full Application</h3>
                            <p class="text-primary">Complete loan application online (1003)</p>
                            <p class="text-muted">
                                <small>Take 10-15 minutes</small>
                            </p>
                            <div class="addSpace">&nbsp;</div>
                            <a class="btnct" href="<%= link %>&amp;content=<%= (String.IsNullOrEmpty(Request.QueryString["content"]) ? "ApplyNow" : Request.QueryString["content"]) %>&amp;type=full">Apply Now</a>
                            <span class="clearfix">&nbsp;</span>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <div class="col-md-12 col-sm-12 col-colored">
                            <h3>Download 1003</h3>
                            <p class="text-primary">Download&nbsp;pdf format application file</p>
                            <p class="text-muted">
                                <small>&nbsp;</small>
                            </p>
                            <div class="addSpace">&nbsp;</div>
                            <a class="btnct" href="docs/1003new.pdf">Download</a>
                            <span class="clearfix">&nbsp;</span>
                        </div>
                    </div>
                    <span class="clearfix">&nbsp;</span>
                </div>
            </div>

            <style id="noBoostrapStyles">
                .container-inside {
                    padding: 10px;
                    text-align: left;
                    margin: 0 auto;
                    background-color: #fff;
                    max-width: 1100px;
                }

                .no-bootstrap .clearfix {
                    display: block;
                    clear: both;
                }

                .no-bootstrap .col-md-4 {
                    float: left;
                    width: 30%;
                    padding: 10px 0px;
                }

                .no-bootstrap .col-md-12 {
                    padding: 10px;
                }

                @media screen and (max-width: 480px) {
                    .no-bootstrap .col-md-4 {
                        float: none;
                        width: 100%;
                        display: block;
                    }
                }
            </style>
            <script>
                $(document).on("ready",
                    function () {
                        0 < $('body link[href*="bootstrap"]').not('[href*="bootstrap-responsive"]').not('[href*="font"]').length ? $("body").addClass("has-bootstrap") : $("body").addClass("no-bootstrap")
                    });
            </script>

            <% } %>
        </div>
    </div>
</div>
</div>