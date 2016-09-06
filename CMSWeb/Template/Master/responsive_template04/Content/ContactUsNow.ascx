<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUsNow.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Content.ContactUsNow" %>
 


<%@ Register TagPrefix="uc" TagName="ContactUs" Src="../../../../Content/Forms/ContactUs.ascx" %>

</div>
</div>

<script>
    var map, mapOptions, google;
    function initialize() {
        var siteTitle = "";
        geocoder = new google.maps.Geocoder();
        var mapOptions = {
            zoom: 11,
            scrollwheel: false,
            center: new google.maps.LatLng(-34.397, 150.644),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
        var address = document.getElementById("address").value;
        siteTitle = document.getElementById("inf_title").value.length > 0 ? document.getElementById("inf_title").value : siteTitle;
        geocoder.geocode({ 'address': address.replace("<br />", "") }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                centerTo = results[0].geometry.location;
                map.setCenter(results[0].geometry.location);
                var contentString = '<div id="contentAddress" style="font-size: 12pt">' +
              '<div id="siteNotice">' +
              '</div>' +
              '<strong>' + siteTitle + '</strong><br>' +
              '<div id="bodyContentAddress">' + address +
              '</div>' +
              '</div>';
                var styles = [
                  {
                      featureType: "all",
                      stylers: [
                          { hue: "#444" },
                        { saturation: 50 }
                      ]
                  }, {
                      featureType: "road.arterial",
                      elementType: "geometry",
                      stylers: [
                        { hue: "#ddd" },
                        { saturation: 10 }
                      ]
                  }, {
                      featureType: "poi.business",
                      elementType: "labels",
                      stylers: [
                        { visibility: "off" }
                      ]
                  }
                ];
                map.setOptions({ styles: styles });
                var infowindow = new google.maps.InfoWindow({
                    content: contentString
                });
                var marker = new google.maps.Marker({
                    position: results[0].geometry.location,
                    map: map,
                    title: siteTitle
                });
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
                infowindow.open(map, marker);
            } else {
                console.error("Geocode was not successful for the following reason: " + status);
            }
        });
        google.maps.event.addDomListener(window, "resize", function () {
            var center = map.getCenter();
            google.maps.event.trigger(map, "resize");
            map.setCenter(center);
        });
    }

    function loadScript() {
        var script = document.createElement("script");
        script.type = "text/javascript";
        script.src = "//maps.googleapis.com/maps/api/js?sensor=true&callback=initialize";
        document.body.appendChild(script);
    }

    $(document).on('ready', function () {
        loadScript();
    });
</script>

<% if (los)
   { %>
<input type="hidden" id="inf_title" value='<%= CMSWeb.Models.Consumable.Consultants.Data().FullName %>'>
<% }
   else
   { %>
<input type="hidden" id="inf_title" value="<%= CMSWeb.Models.Consumable.Companys.Data().CompanyName %>">
<% } %>
<input type="hidden" id="address" value="<%= FullAddress() %>">
<div class="masthead">
    <div class="container">
        <h1 class="sub-text">Contact <%= IterateConjuction() %></h1>
    </div>
</div>
<div id="map-canvas" style="border-bottom-width: 2px; border-bottom-style: solid; border-bottom-color: rgb(242, 242, 242); width: 100%; height: 300px; margin: 0px; display: block; position: relative; overflow: hidden; top: 0px; background-color: #f2f2f2">
</div>
<div class="container">
    <div class="row">
        <div class="col-md-6 col-xs-12">
            <%=  CMSWeb.Models.Tools.ContentBuilders.Body(30) %>
            <% if (los)
               { %>
            <%= CMSWeb.Models.Consumable.Consultants.Data().Content %>
            <% } %>
            <div class="addSpacex2 clearfix"></div>
        </div>
        <div class="col-md-6 col-xs-12 col-sidebar">
            <h3>CONTACT <%= IterateConjuction().ToUpper() %> NOW</h3>
            <uc:ContactUs ID="ContactUsApp" runat="server" />
        </div>
    </div>
</div>
<div class="addSpace clearfix"></div>
<div>
    <div>


