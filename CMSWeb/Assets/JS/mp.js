var infowindow, mp, InfoWindow, marker = [], markers = [],

  path = path || "",
  url = typeof window.BranchesURL != "undefined" && BranchesURL || '../../../WebServices/GetBranch.ashx',
  getLos_url = typeof window.LOSURL != "undefined" && LOSURL || '../../../WebServices/GetLoan.ashx',
  // getLos_url = getLos_url || 'getAllLOsWeichert.ashx', 
  SiteID = SiteID || 0,
   searchby, state, query, timestamp, date, listp = $('.mp-list'),
  delay = 50, nextAddress = 0, timers = [], temp = [], qOk = false, waitingTo = false, bounds = [],
  latlng = {}, defaultLatLng = [37.0625, -95.677068],
  defaultBranch = defaultBranch || false,
  defaultAddress = defaultAddress || false,
  focusFound = false,
  sortOrder = sortOrder || -1,
  multiBranch = multiBranch || false,

  //is in development
  development = development || false,
  debug = debug || false,

  //statuses
  statuses = {
      Window: false,
      Document: false,
      Map: false,
      Initialized: false,
      Init: false
  },

  list = $('.mp-list ul'), map, d = [], wLoad = false, mapLoad = false, uniqueAddresses = [];


var branchItemTemplate = '<li data-id="{{id}}" class="{{customClass}}"><h3>{{name}}</h3><p>{{address}}</p><p>\
    <button type="button" class="set-address" data-name="{{name}}" data-address="{{address}}" data-id="{{id}}">\
    <i class="icon-location"></i> <span>directions</span></button>\
    <button type="button" class="find-officers button-default" data-name="{{name}}" data-address="{{address}}" data-id="{{id}}">\
    find officers</button>\
    </p></li>';

var loItemTemplate = '<div class="col-lo-item LO"><div class="lo-panel lo-panel-default">'
                    + '<div class="panel-body"><div class="media">'
                    + '<a class="pull-left" href="Content/WebServices/getImage.ashx?ID={{photo}}&type=2">'
                    + '<img class="media-object" src="GetConsultantPhoto.aspx?id={{photo}}"'
                    + ' style="width:64px;min-height:85px;max-height: 85px" /></a>'
                    + '<div class="media-body"><h4 class="media-heading">{{name}}</h4>'
                    + '{{title}}<br>'
                    + '{{phone}}<br>'
                    + 'NMLS {{nmls}}'
                    + '</div></div></div><div class="panel-footer"><div class="btn-group">'
                    + '{{website}}'
                    + '{{email}}'
                    + '</div></div></div></div>';


function initialize() {

    latlng = new google.maps.LatLng(defaultLatLng[0], defaultLatLng[1]);

    geocoder = new google.maps.Geocoder();
    var mapOptions = {
        zoom: 5,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: false,
        mapTypeControl: true,
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
            position: google.maps.ControlPosition.BOTTOM_CENTER
        },
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.LARGE,
            position: google.maps.ControlPosition.RIGHT_CENTER
        },
        scaleControl: true,
        streetViewControl: true,
        streetViewControlOptions: {
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        }
    }
    map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
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




    //   var contentString = '<div id="contentinsidemap">'+
    // '<div id="siteNotice">'+
    // '</div>'+
    // '<strong id="firstHeading" class="firstHeading"></strong>'+
    // '<div id="bodyContent">'
    // '<div style="clear:both;"></div></div>'+
    // '</div>';

    // infowindow = new google.maps.InfoWindow({
    //     content: contentString
    // });

    // marker.push( new google.maps.Marker() );

    mp.fitMap();

    setProgress(80);


    $('body').addClass('loadedScrips');

    bounds = new google.maps.LatLngBounds();
    infowindow = new google.maps.InfoWindow();


    statuses.Initialized = true;


    // google.maps.event.addListenerOnce(map, 'idle', function(){
    // do something only the first time the map is loaded
    mp.init();

    if (debug) {
        console.log("loaded full");
    }
    // });


}

function isStorage() {
    return typeof localStorage == "object";
}

function setProgress(n) {
    n = isNaN(n) && n.indexOf('%') ? parseInt(n) + "%" : n + "%";
    $('.loading-bar .bar-bg').css({ width: n });
    $('.load-status').text(parseInt(n) + "%");
}


function map_recenter(latlng, offsetx, offsety) {
    var point1 = map.getProjection().fromLatLngToPoint(
        (latlng instanceof google.maps.LatLng) ? latlng : map.getCenter()
    );
    var point2 = new google.maps.Point(
        ((typeof (offsetx) == 'number' ? offsetx : 0) / Math.pow(2, map.getZoom())) || 0,
        ((typeof (offsety) == 'number' ? offsety : 0) / Math.pow(2, map.getZoom())) || 0
    );

    var newPosition = map.getProjection().fromPointToLatLng(new google.maps.Point(
        point1.x - point2.x,
        point1.y + point2.y
    ));

    // map.setCenter(newPosition);

    map.panTo(newPosition);
}

function loadScript() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "//maps.googleapis.com/maps/api/js?v=3&signed_in=true&sensor=true&callback=initialize";
    document.body.appendChild(script);

    setProgress(25);
}

mp = {
    init: function () {

        // SiteID = $('#SiteID').val();
        searchby = $('#searchby').val();
        state = $('#state').val();
        query = $('#query').val();
        timestamp = new Date().getTime();
        date = new Date(timestamp);

        if (!statuses.Init) {
            statuses.Init = true;
        }

        setTimeout(function () {
            if (!statuses.Window && statuses.Document && statuses.Init) {
                statuses.Window = true;
                wLoad = true;
                qOk = true;
                theNext();
            }
        }, 1000);

        mp.search({ SiteID: SiteID, searchby: searchby, state: state, query: query, timestamp: timestamp, date: date });
    },
    setMarkers: function (map) {
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(null);
        }
        // markers.slice(markers.length, -1);
        markers.length = 0;
        markers.slice(0, markers.length);
        d.length = 0;
        d.slice(0, d.length);

        nextAddress = 0;

        bounds = new google.maps.LatLngBounds();
    },
    fitMap: function () {
        var props = {};
        props.w = $('.mp-container').innerWidth();

        if ($('.mp-container').hasClass('fullscreen')) {
            $('.mp-map').css({ width: (props.w - 294) + 'px' });
        }
        else {
            $('.mp-map').css({ width: '100%' });
        }

        var height = $('.mp-container').outerHeight() - ($('.mp-options').outerHeight() + $('.mp-list-head').outerHeight() + 30);
        $('.mp-list ul').css({ maxHeight: height });

        google.maps.event.trigger(map, 'resize')
    },
    clearMarkers: function () {
        mp.setMarkers(null);
    },
    search: function (params) {
        if (debug) {
            console.log(params, params.query, params.query.length);
        }

        if (markers.length > 0) {
            mp.clearMarkers();
        }

        //var filterparam = $('.mp-container').hasClass('showlist') ? true : params.query.length > 0;
        var filterparam = true;

        params.query = params.query.toLowerCase() == "search..." || params.query.toLowerCase() == "search"
                          ? "" : params.query;


        if (filterparam) {


            var tpl = branchItemTemplate;

            var url_ = path + url + '?SiteID=' + params.SiteID + '&' + "NAME=" + params.query
                        + '&STATE=' + params.state
                        + (sortOrder > -1 ? "&sortOrder=" + sortOrder : "");
            if (mp.request != undefined) mp.request.abort();
            mp.request = $.ajax({
                url: url_, success: function (xhr) {

                    if (debug) {
                        console.log(xhr);
                    }

                    var items = '';
                    var firstItemId = 0;

                    list.empty();


                    if (defaultAddress != false) {
                        if (typeof defaultAddress == "object" && defaultAddress != false) {
                            d[defaultAddress.id] = {};
                            d[defaultAddress.id].addressline = defaultAddress.address;
                            d[defaultAddress.id].address = defaultAddress.address;
                            d[defaultAddress.id].city = defaultAddress.address;
                            d[defaultAddress.id].title = defaultAddress.title;
                            d[defaultAddress.id].id = defaultAddress.id;
                            d.push(d[defaultAddress.id]);

                        }
                    }

                    if (xhr[0].name != 'error') {
                        var _total = xhr.length > 1 ? (xhr.length - 1) : 0;
                        for (i = _total; i >= 0; i--) {
                            if (xhr[i].Address != '') {
                                var _addr = xhr[i].Address + (xhr[i].City != "" ? ', ' + xhr[i].City : "") + (xhr[i].State != "" ? ', ' + xhr[i].State : "") + (xhr[i].Zip != "" ? ' ' + xhr[i].Zip : "");
                                var _cssClass = '';

                                d[xhr[i].BranchID] = {};
                                d[xhr[i].BranchID].address = _addr;
                                d[xhr[i].BranchID].title = xhr[i].Name;
                                d[xhr[i].BranchID].id = xhr[i].BranchID;

                                d.push(d[xhr[i].BranchID]);

                                if (i == xhr.length) {
                                    _cssClass = 'active';
                                    firstItemId = xhr[i].BranchID;
                                }

                                if (i == 0) {


                                    $.each(d, function (i, el) {
                                        if ($.inArray(el, uniqueAddresses) === -1) uniqueAddresses.push(el);
                                    });

                                    wLoad && qOk && theNext();
                                    if (!qOk || !wLoad) { waitingTo = true; }
                                }


                                items += tpl.replace(/{{name}}/g, xhr[i].Name).replace(/{{address}}/g, _addr).replace(/{{id}}/g, xhr[i].BranchID).replace(/{{customClass}}/g, _cssClass);
                            }
                        }


                    } else {
                        items = '<li><p>No results</p></li>';
                    }

                    list.html(items);
                    listp.show();
                    // mp.openInfoWindow(firstItemId);
                }
            });

            wLoad && qOk && theNext();
            if (!qOk || !wLoad) { waitingTo = true; }

            //q
        } else {
            var items = '<li><p>Please write something to start searching.</p></li>';
            list.html(items);
            listp.show();
            $('#query').focus();
        }
    },
    addInfoWindow: function (_id, next, loading) {

        loading = loading != undefined ? loading : false;

        // console.log('adding info window', _id, d[_id]);

        var address = d[_id].address,
            isStored = false,
            _info = {};
        if (isStorage) {
            var stored_markers = localStorage.getItem('markers');
            if (stored_markers != null) {
                stored_markers = JSON.parse(stored_markers);

                if (stored_markers[_id] != undefined) {

                    var f = -1;
                    for (i = 0; i < stored_markers.length; i++) {
                        if (stored_markers[i].id == d[_id].id) {
                            f = i;
                        }
                    }

                    if (f > -1) {
                        isStored = true;
                        _info = stored_markers[f];
                    }

                }
            }
        }



        if (isStored) {

            delay = 10;

            // if this was loaded before 

            /*
      
                var contentString = '<div id="contentinsidemap">'+
                '<div id="siteNotice">'+
                '</div>'+
                '<strong id="firstHeading" class="firstHeading">' + d[_id].title + '</strong><br />'+
                '<div id="bodyContent"> ' + d[_id].address +
                '<div class="clearfix"></div></div>'+
                '</div>';
      
                d[_id].contentString = contentString;
      
                infowindow.close();
      
                infowindow = new google.maps.InfoWindow({
                    content: contentString
                });
      
                var _location = new google.maps.LatLng( _info.geo[0].geometry.location.lat(), _info.geo[0].geometry.location.lng() );
      
                var marker_ = new google.maps.Marker({
                    position: _location,
                    address: d[_id].address,
                    BranchID: d[_id].id,
                    map: map,
                    title: d[_id].title          
                });
      
                
                marker[_id] = marker_;
                if(jQuery.inArray(marker_, marker)<0) marker.push(marker_);
      
                d[_id].marker = marker[d[_id].id];
      
      
                // google.maps.event.addListenerOnce(map, 'idle', function(){
                // do something only the first time the map is loaded
                  
      
                  // google.maps.event.addListener(marker[d[_id].id], 'click', function() {
                  //   infowindow.open(map,d[_id].marker);
                  //   console.log(d[_id].address);
                  //   list.find('li[data-id="' + d[_id].id + '"]').addClass('active').siblings().removeClass('active');
                  //   if ($('.mp-container').hasClass('fullscreen') ){
                  //      $('.mp-toolbar').scrollTop(list.find('li[data-id="' + d[_id].id + '"]').position().top - 10);
                  //   }else{
                  //     $('.mp-toolbar ul').scrollTop(list.find('li[data-id="' + d[_id].id + '"]').position().top - 10);
                  //   }
                  // });
      
      
                // });
      
      
                var $el = list.find('li[data-id="' + d[_id].id + '"] .set-address');
      
                $el.data('marker', d[_id].marker);
      
                // $('body').on('click', $el, function(){
                //   infowindow.open(map, d[_id].marker);
                // });
      
                // bounds.extend(marker_.position);
                
      
      
                
                console.log(d[_id]);
      
                var _mark = d[_id].marker;
      
                */


            var loc = _info.location,
                lat = loc.lat,
                lng = loc.lng;



            createMarker([d[_id].address, d[_id].title, d[_id].id], lat, lng);

            // if( !loading )
            // {
            //   setTimeout(function(){ map_recenter(_location, 150, 0); }, delay)

            //   infowindow.open(map,d[_id].marker);
            //   list.find('li[data-id="' + d[_id].id + '"]').addClass('active').siblings().removeClass('active');
            // }

            // if( nextAddress == _id || nextAddress == ( _id - 1 ) && loading )
            // {
            //   setTimeout(function(){ map_recenter(_location, 150, 0); }, delay)

            //   infowindow.open(map, d[_id].marker);
            //   list.find('li[data-id="' + d[_id].id + '"]').addClass('active').siblings().removeClass('active');
            // }


            next != undefined && next();






        }

        else {




            geocoder.geocode({ 'address': d[_id].address }, function (results, status) {

                // console.log( results, status );


                if (status == google.maps.GeocoderStatus.OK) {
                    // If map is OK

                    // map.panTo(results[0].geometry.location);


                    var _location = results[0].geometry.location,
                        lat = _location.lat(),
                        lng = _location.lng();

                    createMarker([d[_id].address, d[_id].title, d[_id].id], lat, lng);

                    /*
        
                    var contentString = '<div id="contentinsidemap">'+
                  '<div id="siteNotice">'+
                  '</div>'+
                  '<strong id="firstHeading" class="firstHeading">' + d[_id].title + '</strong><br />'+
                  '<div id="bodyContent"> ' + d[_id].address +
                  '<div class="clearfix"></div></div>'+
                  '</div>';
        
                  d[_id].contentString = contentString;
        
                  infowindow.close();
        
                  infowindow = new google.maps.InfoWindow({
                      content: contentString
                  });
        
                  var marker_ = new google.maps.Marker({
                      position: results[0].geometry.location,
                      address: d[_id].address,
                      BranchID: d[_id].id,
                      map: map,
                      title: d[_id].title          
                  });
                  
                  marker[_id] = marker_;
                  if(jQuery.inArray(marker_, marker)<0) marker.push(marker_);
        
                  d[_id].marker = marker[d[_id].id];
        
                  console.log("add listener");
                  google.maps.event.addListener(marker_, 'click', function() {
                    console.log("adding listener");
                    infowindow.open(map, marker_);
                    console.log(d[_id].address);
                    list.find('li[data-id="' + d[_id].id + '"]').addClass('active').siblings().removeClass('active');
                    if ($('.mp-container').hasClass('fullscreen') ){
                       $('.mp-toolbar').scrollTop(list.find('li[data-id="' + d[_id].id + '"]').position().top - 10);
                    }else{
                      $('.mp-toolbar ul').scrollTop(list.find('li[data-id="' + d[_id].id + '"]').position().top - 10);
                    }
                  });
        
                  var $el = list.find('li[data-id="' + d[_id].id + '"] .set-address');
                  $('body').on('click', $el, function(){
                    infowindow.open(map, marker_);
                  });
        
        
        
                  // bounds.extend(marker_.position);
                  
        
        
                  
                  console.log(d[_id]);
        
                  var _mark = d[_id].marker;
        
        
        
        
        
                  */

                    if (isStorage) {
                        var stored_markers = localStorage.getItem('markers');
                        if (stored_markers == null) {
                            var tmp = [];

                            var newItem = d[_id];
                            newItem.geo = results;
                            newItem.location = { lat: lat, lng: lng };

                            tmp.push(newItem);
                            tmp = JSON.stringify(tmp);
                            localStorage.setItem('markers', tmp);

                        } else {

                            var newItem = d[_id];
                            newItem.geo = results;
                            newItem.location = { lat: lat, lng: lng };

                            stored_markers = JSON.parse(stored_markers);

                            if ($.inArray(stored_markers, newItem) < 0) {
                                stored_markers.push(newItem);
                            }

                            stored_markers = JSON.stringify(stored_markers);

                            localStorage.setItem('markers', stored_markers);

                        }

                    }


                    // if( !loading )
                    // {
                    //   map_recenter(_location, 150, 0);

                    //   // infowindow.open(map,_mark);
                    //   list.find('li[data-id="' + d[_id].id + '"]').addClass('active').siblings().removeClass('active');
                    // }

                    // if( nextAddress == _id || nextAddress == ( _id - 1 ) && loading )
                    // {
                    //   map_recenter(_location, 150, 0);

                    //   // infowindow.open(map, _mark);
                    //   list.find('li[data-id="' + d[_id].id + '"]').addClass('active').siblings().removeClass('active');
                    // }



                }

                    //if map load fails
                else {
                    if (status == google.maps.GeocoderStatus.OVER_QUERY_LIMIT) {
                        nextAddress--;
                        delay++;
                    } else {
                        // var reason="Code "+status;
                        // var msg = 'address="' + search + '" error=' +reason+ '(delay='+delay+'ms)<br>';
                        // document.getElementById("messages").innerHTML += msg;
                        if (debug) {
                            console.log(msg);
                        }

                    }
                }

                next != undefined && next();



            });




            //if not stored
        }





    },
    openInfoWindow: function (_id) {
        mp.addInfoWindow(_id);
        return d[_id];
    },
    progress: function (progress) {
        $('.progressseek').css({ width: progress + '%' });
        return progress + '%';
    }
}




// mp.addInfoWindow(xhr[i].BranchID);




function theNext() {
    // if (nextAddress < d.length) {

    if (wLoad) {

        // if (nextAddress < 20) {
        if (nextAddress < d.length && d.length > 0) {
            if (debug) {
                console.log('loadNext', d[nextAddress]);
            }
            setTimeout('mp.addInfoWindow("' + nextAddress + '",theNext, true)', delay);
            nextAddress++;


            $('body').removeClass('ready');

            var total = (nextAddress / (d.length / 100));
            setProgress(total);

        } else {
            // We're done. Show map bounds
            setProgress(100);

            if (debug) {
                console.log('loadComplete');
            }

            setTimeout(function () {
                mapLoad = true;

                statuses.Map = true;

                wLoad && $('body').addClass('ready').removeClass('loading');

                if ((defaultBranch == false || defaultAddress == false) && focusFound == false) {
                    if (d.length > 0) {
                        map.fitBounds(bounds);
                        map_recenter(bounds.getCenter(), 150, 0);
                    }
                    mp.fitMap();
                }

            }, 300);

        }


    }

    else {
        setTimeout('theNext()', delay);
    }

}




// ======= Function to create a marker
function createMarker(add, lat, lng) {
    // var contentString = add;

    var contentString = '<div class="contentinsidemap">' +
       '<div class="siteNotice">' +
       '</div>' +
       '<strong class="firstHeading" class="firstHeading"><i class="icon-location"></i> ' + add[1] + '</strong>' +
       '<div class="bodyContent"> ' + add[0] +
       '<div class="clearfix"></div>' +
       '<button type="button" class="map-button button-default find-officers inside-map" \
          data-id="' + add[2] + '" data-name="' + add[1] + '">find officers</button>' +
       '<div class="clearfix"></div></div>' +
       '</div>';


    var latlng = new google.maps.LatLng(lat, lng),
       marker = new google.maps.Marker({
           position: latlng,
           map: map,
           flat: true,
           title: add[1]
       });



    markers.push(marker);

    if (defaultBranch != false && defaultBranch == add[2]) {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
        map_recenter(latlng, 150, 0);

        focusFound = true;
    }

    if (defaultAddress != false && defaultAddress == add[2]) {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
        map_recenter(latlng, 150, 0);

        focusFound = true;
    }




    google.maps.event.clearListeners(marker, 'click');
    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
        map_recenter(latlng, 150, 0);
    });


    $('body').find('.set-addres').off('click');
    $('body').on('click', '.set-address[data-id="' + add[2] + '"]', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
        map_recenter(latlng, 150, 0);
    });


    $('.set-address[data-id="' + add[2] + '"]').attr('data-position', lat + ',' + lng);


    if (!bounds.contains(marker.position)) {
        bounds.extend(marker.position);
    }


}





$(document).ready(function () {

    loadScript();


    statuses.Document = true;



    if (development) {
        loItemTemplate = '<div class="col-lo-item LO"><div class="lo-panel lo-panel-default">'
                          + '<div class="panel-body"><div class="media">'
                          + '<a class="pull-left" href="GetConsultantPhoto.aspx?id={{photo}}">'
                          + '<img class="media-object" src="doe.jpg?id={{photo}}"'
                          + ' style="width:64px;min-height:85px;max-height: 85px" /></a>'
                          + '<div class="media-body"><h4 class="media-heading">{{name}}</h4>'
                          + '{{title}}<br>'
                          + '{{phone}}<br>'
                          + 'NMLS {{nmls}}'
                          + '</div></div></div><div class="panel-footer"><div class="btn-group">'
                          + '{{website}}'
                          + '{{email}}'
                          + '</div></div></div></div>';
    }




    $(".mp-container select").chosen({ width: "100%", disable_search: true });

    $('body').on('click', '.btnfullscreen', function () {
        $('.mp-container').toggleClass('fullscreen');
        $('body').toggleClass('bodyfullscreen');
        mp.fitMap();
    }).on('keyup', '#query', function (e) {
        if (e.keyCode == 13) {
            mp.init();
        }
    }).on('click', '#querygo', function () {
        mp.init();
    }).on('click', '.set-address', function (e) {
        // var _address = $(this).attr('data-address');
        // var _title = $(this).attr('data-name');
        // var _id = $(this).attr('data-id');
        // d[_id] = {
        //   id: _id,
        //   title: _title,
        //   address: _address
        // }
        // mp.openInfoWindow(_id); 

        // var m = $(this).data('marker');


        // infowindow.open(map, m);

    }).on('change', '#searchfilter', function () {

        if ($('.mp-container').hasClass('showlist')) {
            $('#searchby').prop('disabled', false).trigger("chosen:updated");
            $('#query').focus();
        } else {
            $('#searchby').prop('disabled', true).trigger("chosen:updated");
        }

        $('.mp-container').toggleClass('showlist');
        $('#query').val('');
        mp.init();
    })



    //close loan officers search
    .on('click', '.close-find-officers', function () {

        $(".loanSearch").hide();
        if ($("body").hasClass("fullscreen")) {
            $(".mp-container").css({ height: "100%" });
        }
        else {
            $(".mp-container").css({ height: "100%" });
        }

    })



    //when click on search for officers
    .on('click', '.find-officers', function () {


        var _id = $(this).attr('data-id'),
            name = $(this).attr('data-name');


        $('.loanSearch').show();

        $('.back-btn span b').text(name);

        if (window.innerWidth < 768) {
            $('.mp-container').css({ height: 100 + '%' });
        } else {

            if ($("body").hasClass("fullscreen")) {
                $(".mp-container").css({ height: "100%" });
            }
            else {
                $(".mp-container").css({ height: "100%" });
            }

        }
        mp.progress(10);
        $('.mp-lo-results').css({ opacity: 0.6 });


        // var SiteID = $('#SiteID').val();
        var LOS_URL = path + getLos_url + '?BranchID=' + _id + (multiBranch ? '&BranchALL=true' : "")
                  + '&SiteID=' + SiteID
                  + (sortOrder > -1 ? "&sortOrder=" + sortOrder : "");

        if (development) {
            LOS_URL = 'los.json';
        }

        $.ajax({
            url: LOS_URL,
            contentType: 'application/json',
            beforeSend: function () {
                mp.progress(0);
            },
            success: function (xhr) {
                // xhr = $.parseJSON(xhr);
                var htmllist = "";
                var list = [];

                if (xhr != null && xhr[0] != undefined && xhr[0] != null && xhr[0].name != "error") {
                    for (i = 0; i < xhr.length; i++) {
                        if (xhr[i] != null) {
                            list.push([i]);
                            var lotpl = loItemTemplate.replace(/{{name}}/g, xhr[i].fullName)
                                                    .replace(/{{nmls}}/g, xhr[i].nmls)
                                                    .replace(/{{photo}}/g, xhr[i].id)
                                                    .replace(/{{phone}}/g, xhr[i].phone)
                                                    .replace(/{{address}}/g, xhr[i].address)
                                                    .replace(/{{title}}/g, xhr[i].position);



                            if (xhr[i].website != "" && xhr[i].website != null && xhr[i].website != "null" && xhr[i].website.length > 0) {
                                xhr[i].website = xhr[i].website.indexOf('//') > -1 ? xhr[i].website : '//' + xhr[i].website;
                                lotpl = lotpl.replace(/{{website}}/g, '<a href="' + xhr[i].website + '" '
                                        + 'class="map-button button-default" target="_blank">Visit website</a>');
                            } else {
                                lotpl = lotpl.replace(/{{website}}/g, '');
                            }

                            if (xhr[i].email != "" && xhr[i].email != "null" && xhr[i].email != null && xhr[i].email.length > 0) {
                                lotpl = lotpl.replace(/{{email}}/g, '<a href="mailto:' + xhr[i].email + '" class="map-button button-default">Contact me</a>');
                            } else {
                                lotpl = lotpl.replace(/{{email}}/g, '');
                            }

                            if ((xhr[i].email == "" || xhr[i].email == "null" || xhr[i].email == null || xhr[i].email.length == 0)
                                && (xhr[i].website == "" || xhr[i].website == null || xhr[i].website == "null" || xhr[i].website.length == 0)) {
                                if (xhr[i].phone.length > 0 && xhr[i].phone != null && xhr[i].phone != '' && xhr[i].phone != "null") {

                                } else {

                                }
                            }

                            htmllist += lotpl;
                            //inside for


                        }
                        //if != null   
                    }
                } else {
                    htmllist = "<h3 class=\"text-muted\">No loan officers found.</h3>";
                }

                if (list.length == 0) {
                    htmllist = "<h3 class=\"text-muted\">No loan officers found.</h3>";
                }



                mp.progress(100);
                $('.mp-lo-results').empty();
                $('.mp-lo-results').html(htmllist).css({ opacity: 1 });

            }
        }).error(function () {
            var htmllist = "<h3 class=\"text-muted\">No loan officers found.</h3><span style=\"display:none\">source code error</span>";


            mp.progress(100);
            $('.mp-lo-results').empty();
            $('.mp-lo-results').html(htmllist).css({ opacity: 1 });
        });


    })


    //when state changes
    .on('change', '#state', function () {
        // if($('.mp-container').hasClass('showlist')){
        mp.init();
        // }
    })



    //when focus on search textbox
    .on('focus', '.mp-container input[type="text"]', function () {
        var text = $(this).val(),
            fake = "Search...",
            placeholder = $(this).attr('placeholder') != null || $(this).attr('placeholder') != undefined
                ? $(this).attr('placeholder') : "";

        if (text == fake || text == placeholder) {
            $(this).val('');
        }

    })



    .on('blur', '.mp-container input[type="text"]', function () {
        var text = $(this).val(),
            fake = "Search...",
            placeholder = $(this).attr('placeholder') != null || $(this).attr('placeholder') != undefined
                ? $(this).attr('placeholder') : "";

        if (text == fake || text == placeholder || text == "" || text == " ") {
            $(this).val(placeholder);
        }

    });

})
.ajaxStart(function () { $('body').addClass('loading'); })
.ajaxComplete(function () { wLoad && mapLoad && $('body').removeClass('loading'); });

window.onload = function () {
    wLoad = true;
    qOk = true;

    statuses.Window = true;

    $('body').removeClass('loading');

    if (waitingTo) {
        waitingTo = false;
        theNext();
    }

    wLoad && mapLoad && !waitingTo && $('body').addClass('ready');
}






//console pollyfill

window.console || {
    memory: {},
    assert: function () { },
    clear: function () { },
    count: function () { },
    debug: function () { },
    dir: function () { },
    dirxml: function () { },
    error: function () { },
    exception: function () { },
    group: function () { },
    groupCollapsed: function () { },
    groupEnd: function () { },
    info: function () { },
    log: function () { },
    markTimeline: function () { },
    profile: function () { },
    profiles: function () { },
    profileEnd: function () { },
    show: function () { },
    table: function () { },
    time: function () { },
    timeEnd: function () { },
    timeline: function () { },
    timelineEnd: function () { },
    timeStamp: function () { },
    trace: function () { },
    warn: function () { }
};