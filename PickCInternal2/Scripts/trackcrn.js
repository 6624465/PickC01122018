var map;
var directionDisplay;
var directionsService;
var stepDisplay;
var markerArray = [];
var position;
var marker = null;
var polyline = null;
var poly2 = null;
var speed = 0.000005,
    wait = 1;
var infowindow = null;
var timerHandle = null;

function createMarker(latlng, label, html) {
    var contentString = '<b>' + label + '</b><br>' + html;
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: label,
        zIndex: Math.round(latlng.lat() * -100000) << 5
    });
    marker.myname = label;
    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });
    return marker;
}

function initialize() {
    debugger;
    infowindow = new google.maps.InfoWindow({
        size: new google.maps.Size(150, 50)
    });
    // Instantiate a directions service.
    directionsService = new google.maps.DirectionsService();

    // Create a map and center it on Manhattan.
    var myOptions = {
        zoom: 13,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("dvMap"), myOptions);

    address = 'Hyderabad';
    geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'address': address
    }, function (results, status) {
        map.setCenter(results[0].geometry.location);
    });

    // Create a renderer for directions and bind it to the map.
    var rendererOptions = {
        map: map
    };
    directionsDisplay = new google.maps.DirectionsRenderer(rendererOptions);

    // Instantiate an info window to hold step text.
    stepDisplay = new google.maps.InfoWindow();

    polyline = new google.maps.Polyline({
        path: [],
        strokeColor: '#FF0000',
        strokeWeight: 3
    });
    poly2 = new google.maps.Polyline({
        path: [],
        strokeColor: '#FF0000',
        strokeWeight: 3
    });
}

var steps = [];

function calcRoute() {
    debugger;
    if (timerHandle) {
        clearTimeout(timerHandle);
    }
    if (marker) {
        marker.setMap(null);
    }
    polyline.setMap(null);
    poly2.setMap(null);
    directionsDisplay.setMap(null);
    polyline = new google.maps.Polyline({
        path: [],
        strokeColor: '#FF0000',
        strokeWeight: 3
    });
    poly2 = new google.maps.Polyline({
        path: [],
        strokeColor: '#FF0000',
        strokeWeight: 3
    });
    // Create a renderer for directions and bind it to the map.
    var rendererOptions = {
        map: map
    };
    directionsDisplay = new google.maps.DirectionsRenderer(rendererOptions);

    /*
    var start = document.getElementById("start").value;
    var end = document.getElementById("end").value;*/
    debugger;
    var start = bkgTrackCrnLat + ',' + bkgTrackCrnLong;
    var end = bkgTrackCrnToLat + ',' + bkgTrackCrnToLong;
    var travelMode = google.maps.DirectionsTravelMode.DRIVING;

    var request = {
        origin: start,
        destination: end,
        travelMode: travelMode
    };

    // Route the directions and pass the response to a
    // function to create markers for each step.
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);

            var bounds = new google.maps.LatLngBounds();
            var route = response.routes[0];
            startLocation = new Object();
            endLocation = new Object();

            // For each route, display summary information.
            var path = response.routes[0].overview_path;
            var legs = response.routes[0].legs;
            for (i = 0; i < legs.length; i++) {
                if (window.CP.shouldStopExecution(3)) { break; }
                if (i === 0) {
                    startLocation.latlng = legs[i].start_location;
                    startLocation.address = legs[i].start_address;
                    //   marker = createMarker(legs[i].start_location, "start", legs[i].start_address, "green");
                }
                endLocation.latlng = legs[i].end_location;
                endLocation.address = legs[i].end_address;
                var steps = legs[i].steps;
                for (j = 0; j < steps.length; j++) {
                    if (window.CP.shouldStopExecution(2)) { break; }
                    var nextSegment = steps[j].path;
                    for (k = 0; k < nextSegment.length; k++) {
                        if (window.CP.shouldStopExecution(1)) { break; }
                        polyline.getPath().push(nextSegment[k]);
                        bounds.extend(nextSegment[k]);
                    }
                    window.CP.exitedLoop(1);

                }
                window.CP.exitedLoop(2);

            }
            window.CP.exitedLoop(3);

            polyline.setMap(map);
            map.fitBounds(bounds);
            map.setZoom(18);
            startAnimation();
        }
    });
}



var step = 50; // 5; // metres
var tick = 100; // milliseconds
var eol;
var k = 0;
var stepnum = 0;
var speed = "";
var lastVertex = 1;

//=============== animation functions ======================
function updatePoly(d) {
    // Spawn a new polyline every 20 vertices, because updating a 100-vertex poly is too slow
    if (poly2.getPath().getLength() > 20) {
        poly2 = new google.maps.Polyline([polyline.getPath().getAt(lastVertex - 1)]);
        //map.addOverlay(poly2)
    }

    if (polyline.GetIndexAtDistance(d) < lastVertex + 2) {
        if (poly2.getPath().getLength() > 1) {
            poly2.getPath().removeAt(poly2.getPath().getLength() - 1);
        }
        poly2.getPath().insertAt(poly2.getPath().getLength(), polyline.GetPointAtDistance(d));
    } else {
        poly2.getPath().insertAt(poly2.getPath().getLength(), endLocation.latlng);
    }
}

function animate(d) {
    debugger;
    if (d > eol) {
        map.panTo(endLocation.latlng);
        marker.setPosition(endLocation.latlng);
        return;
    }
    var p = polyline.GetPointAtDistance(d);
    map.panTo(p);
    var lastPosn = marker.getPosition();
    marker.setPosition(p);
    var heading = google.maps.geometry.spherical.computeHeading(lastPosn, p);
    icon.rotation = heading;
    marker.setIcon(icon);
    updatePoly(d);
    //CurrentLatLngValues();
    //timerHandle = setTimeout("animate(" + (d + step) + ")", tick);
}
/*Vijay*/
//function CurrentLatLngValues() {
//    var start = document.getElementById("start").value;
//    var end = document.getElementById("end").value;
//    var currentLatLng = '17.493887,78.398231';

//    //var d = google.maps.LatLng.prototype.distanceFrom(currentLatLng);
//    var d = polyline.Distance();
//    var p = polyline.GetPointAtDistance(d);
//    var startLatLng = new google.maps.LatLng(start);
//    var currentLatLng = new google.maps.LatLng(currentLatLng);
//    var tempDi = TempDistance(startLatLng, currentLatLng);
//    //var s = tempDi
//    if (i <= 30) {
//        setTimeout(function () {
//            animate(tempDi);
//        }, 2000);
//    }
//}
//var i = 0;
//function TempDistance(startLatLng, newLatLng) {
//    console.log(i);
//    var EarthRadiusMeters = 6378137.0; // meters
//    //var lat1 = startLatLng.lat();
//    //var lon1 = startLatLng.lng();
//    //var lat2 = newLatLng.lat();
//    //var lon2 = newLatLng.lng();
//    var lat1 = 17.500010;
//    var lon1 = 78.401527;
//    var lat2 = 17.493887;
//    var lon2 = 78.398231;
//    //if (i == 1) {
//    //    lat2 = 17.49738;
//    //    lon2 = 78.395648;
//    //}
//    //if (i == 1) {
//    //    lat2 = 17.48987;
//    //    lon2 = 78.3909826;
//    //} \
//    if (i == 2 || i == 1) {
//        lat2 = 17.498144;
//        lon2 = 78.400846;
//    } else if (i == 3) {
//        lat2 = 17.499153;
//        lon2 = 78.398915;
//    } else if (i == 4) {
//        lat2 = 17.497579;
//        lon2 = 78.398255;
//    } else if (i == 5) {
//        lat2 = 17.496734;
//        lon2 = 78.398014;
//    } else if (i == 6) {
//        lat2 = 17.49586;
//        lon2 = 78.397861;
//    } else if (i == 7) {
//        lat2 = 17.495515;
//        lon2 = 78.396724;
//    } else if (i == 8) {
//        lat2 = 17.495727;
//        lon2 = 78.395755;
//    } else if (i == 9) {
//        lat2 = 17.495128;
//        lon2 = 78.395235;
//    } else if (i == 10) {
//        lat2 = 17.493558;
//        lon2 = 78.394594;
//    } else if (i == 11) {
//        lat2 = 17.492509;
//        lon2 = 78.394132;
//    } else if (i == 12) {
//        lat2 = 17.490046;
//        lon2 = 78.393192;
//    } else if (i == 13) {
//        lat2 = 17.488879;
//        lon2 = 78.392712;
//    } else if (i == 14) {
//        lat2 = 17.486483;
//        lon2 = 78.391768;
//    } else if (i == 15) {
//        lat2 = 17.482935;
//        lon2 = 78.390234;
//    } else if (i == 16) {
//        lat2 = 17.476425;
//        lon2 = 78.390823;
//    } else if (i == 17) {
//        lat2 = 17.471846;
//        lon2 = 78.388581;
//    } else if (i == 18) {
//        lat2 = 17.4596;
//        lon2 = 78.386948;
//    } else if (i == 19) {
//        lat2 = 17.454704;
//        lon2 = 78.385529;
//    } else if (i == 20) {
//        lat2 = 17.452829;
//        lon2 = 78.38719;
//    } else if (i == 21) {
//        lat2 = 17.452225;
//        lon2 = 78.386606;
//    } else if (i == 22) {
//        lat2 = 17.450649;
//        lon2 = 78.386231;
//    } else if (i == 23 || i == 24) {
//        lat2 = 17.452829;
//        lon2 = 78.387856;
//    } else if (i == 25) {
//        lat2 = 17.452281;
//        lon2 = 78.387154;
//    } else if (i == 26) {
//        lat2 = 17.451816;
//        lon2 = 78.38602;
//    } else if (i == 27) {
//        lat2 = 17.451179;
//        lon2 = 78.38593;
//    } else if (i == 28) {
//        lat2 = 17.450674;
//        lon2 = 78.385907;
//    } else if (i == 29) {
//        lat2 = 17.450256;
//        lon2 = 78.385879;
//    } else if (i == 30) {
//        lat2 = 17.449963;
//        lon2 = 78.386096;
//    }
//    i++;
//    var dLat = (lat2 - lat1) * Math.PI / 180;
//    var dLon = (lon2 - lon1) * Math.PI / 180;
//    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
//    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
//    var d = EarthRadiusMeters * c;
//    console.log(d);
//    return d;
//}

function CalFromToCurrentDistance(curLat, curLng) {
    var EarthRadiusMeters = 6378137.0;
    var lat1 = bkgTrackCrnLat;
    var lon1 = bkgTrackCrnLong;
    var lat2 = curLat;
    var lon2 = curLng;

    var dLat = (lat2 - lat1) * Math.PI / 180;
    var dLon = (lon2 - lon1) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = EarthRadiusMeters * c;
    console.log(d);
    return d;
}
/*Vijay*/
function startAnimation() {
    eol = polyline.Distance();
    map.setCenter(polyline.getPath().getAt(0));
    marker = new google.maps.Marker({
        position: polyline.getPath().getAt(0),
        map: map,
        icon: icon
    });

    poly2 = new google.maps.Polyline({
        path: [polyline.getPath().getAt(0)],
        strokeColor: "#0000FF",
        strokeWeight: 10
    });
  
     //map.addOverlay(poly2);
    //setTimeout("animate(50)", 2000); // Allow time for the initial map display

    //var isModalOpen = ($("#track-crn-modal").data('bs.modal') || {}).isShown;
    //if (isModalOpen) {
    //    promiseCall();
    //}
    debugger;
    promiseCall();
}

var gD = 0;
function promiseCall() {
    debugger;
    var promise = GetDriverCurrentLatLng(bkgTrackCrnBookingNo);
    $.when(promise).done(function (result) {
        debugger;
        setTimeout(function () {
            debugger;
            promiseCall();
        }, 10000);
        if (!result.booking.IsComplete && !result.booking.IsCancel) {
            var d = CalFromToCurrentDistance(result.driverMonitorInCustomer.CurrentLat, result.driverMonitorInCustomer.CurrentLong);
            if (gD != d) {
                animate(d);
                gD = d;
            }               
            
            //setTimeout(function () {
            //    debugger;
            //    promiseCall();
            //}, 10000);
        }
    });
}
//google.maps.event.addDomListener(window, 'load', initialize);

//=============== ~animation funcitons =====================

var car = "M17.402,0H5.643C2.526,0,0,3.467,0,6.584v34.804c0,3.116,2.526,5.644,5.643,5.644h11.759c3.116,0,5.644-2.527,5.644-5.644 V6.584C23.044,3.467,20.518,0,17.402,0z M22.057,14.188v11.665l-2.729,0.351v-4.806L22.057,14.188z M20.625,10.773 c-1.016,3.9-2.219,8.51-2.219,8.51H4.638l-2.222-8.51C2.417,10.773,11.3,7.755,20.625,10.773z M3.748,21.713v4.492l-2.73-0.349 V14.502L3.748,21.713z M1.018,37.938V27.579l2.73,0.343v8.196L1.018,37.938z M2.575,40.882l2.218-3.336h13.771l2.219,3.336H2.575z M19.328,35.805v-7.872l2.729-0.355v10.048L19.328,35.805z";
var icon = {
    path: car,
    scale: .7,
    strokeColor: 'white',
    strokeWeight: .10,
    fillOpacity: 1,
    fillColor: '#404040',
    offset: '5%',
    // rotation: parseInt(heading[i]),
    anchor: new google.maps.Point(10, 25) // orig 10,50 back of car, 10,0 front of car, 10,25 center of car
};

/*********************************************************************\
*                                                                     *
* epolys.js                                          by Mike Williams *
* updated to API v3                                  by Larry Ross    *
*                                                                     *
* A Google Maps API Extension                                         *
*                                                                     *
* Adds various Methods to google.maps.Polygon and google.maps.Polyline *
*                                                                     *
* .Contains(latlng) returns true is the poly contains the specified   *
*                   GLatLng                                           *
*                                                                     *
* .Area()           returns the approximate area of a poly that is    *
*                   not self-intersecting                             *
*                                                                     *
* .Distance()       returns the length of the poly path               *
*                                                                     *
* .Bounds()         returns a GLatLngBounds that bounds the poly      *
*                                                                     *
* .GetPointAtDistance() returns a GLatLng at the specified distance   *
*                   along the path.                                   *
*                   The distance is specified in metres               *
*                   Reurns null if the path is shorter than that      *
*                                                                     *
* .GetPointsAtDistance() returns an array of GLatLngs at the          *
*                   specified interval along the path.                *
*                   The distance is specified in metres               *
*                                                                     *
* .GetIndexAtDistance() returns the vertex number at the specified    *
*                   distance along the path.                          *
*                   The distance is specified in metres               *
*                   Returns null if the path is shorter than that      *
*                                                                     *
* .Bearing(v1?,v2?) returns the bearing between two vertices          *
*                   if v1 is null, returns bearing from first to last *
*                   if v2 is null, returns bearing from v1 to next    *
*                                                                     *
*                                                                     *
***********************************************************************
*                                                                     *
*   This Javascript is provided by Mike Williams                      *
*   Blackpool Community Church Javascript Team                        *
*   http://www.blackpoolchurch.org/                                   *
*   https://econym.org.uk/gmap/                                        *
*                                                                     *
*   This work is licenced under a Creative Commons Licence            *
*   http://creativecommons.org/licenses/by/2.0/uk/                    *
*                                                                     *
***********************************************************************
*                                                                     *
* Version 1.1       6-Jun-2007                                        *
* Version 1.2       1-Jul-2007 - fix: Bounds was omitting vertex zero *
*                                add: Bearing                         *
* Version 1.3       28-Nov-2008  add: GetPointsAtDistance()           *
* Version 1.4       12-Jan-2009  fix: GetPointsAtDistance()           *
* Version 3.0       11-Aug-2010  update to v3                         *
*                                                                     *
\*********************************************************************/

// === first support methods that don't (yet) exist in v3
google.maps.LatLng.prototype.distanceFrom = function (newLatLng) {
    var EarthRadiusMeters = 6378137.0; // meters
    var lat1 = this.lat();
    var lon1 = this.lng();
    var lat2 = newLatLng.lat();
    var lon2 = newLatLng.lng();
    var dLat = (lat2 - lat1) * Math.PI / 180;
    var dLon = (lon2 - lon1) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = EarthRadiusMeters * c;
    return d;
}

google.maps.LatLng.prototype.latRadians = function () {
    return this.lat() * Math.PI / 180;
}

google.maps.LatLng.prototype.lngRadians = function () {
    return this.lng() * Math.PI / 180;
}

// === A method which returns the length of a path in metres ===
google.maps.Polygon.prototype.Distance = function () {
    var dist = 0;
    for (var i = 1; i < this.getPath().getLength(); i++) {
        if (window.CP.shouldStopExecution(4)) { break; }
        dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
    }
    window.CP.exitedLoop(4);

    return dist;
}

// === A method which returns a GLatLng of a point a given distance along the path ===
// === Returns null if the path is shorter than the specified distance ===
google.maps.Polygon.prototype.GetPointAtDistance = function (metres) {
    // some awkward special cases
    if (metres == 0) return this.getPath().getAt(0);
    if (metres < 0) return null;
    if (this.getPath().getLength() < 2) return null;
    var dist = 0;
    var olddist = 0;
    for (var i = 1;
        (i < this.getPath().getLength() && dist < metres); i++) {
        if (window.CP.shouldStopExecution(5)) { break; }
        olddist = dist;
        dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
    }
    window.CP.exitedLoop(5);

    if (dist < metres) {
        return null;
    }
    var p1 = this.getPath().getAt(i - 2);
    var p2 = this.getPath().getAt(i - 1);
    var m = (metres - olddist) / (dist - olddist);
    return new google.maps.LatLng(p1.lat() + (p2.lat() - p1.lat()) * m, p1.lng() + (p2.lng() - p1.lng()) * m);
}

// === A method which returns an array of GLatLngs of points a given interval along the path ===
google.maps.Polygon.prototype.GetPointsAtDistance = function (metres) {
    var next = metres;
    var points = [];
    // some awkward special cases
    if (metres <= 0) return points;
    var dist = 0;
    var olddist = 0;
    for (var i = 1;
        (i < this.getPath().getLength()); i++) {
        if (window.CP.shouldStopExecution(7)) { break; }
        olddist = dist;
        dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
        while (dist > next) {
            if (window.CP.shouldStopExecution(6)) { break; }
            var p1 = this.getPath().getAt(i - 1);
            var p2 = this.getPath().getAt(i);
            var m = (next - olddist) / (dist - olddist);
            points.push(new google.maps.LatLng(p1.lat() + (p2.lat() - p1.lat()) * m, p1.lng() + (p2.lng() - p1.lng()) * m));
            next += metres;
        }
        window.CP.exitedLoop(6);

    }
    window.CP.exitedLoop(7);

    return points;
}

// === A method which returns the Vertex number at a given distance along the path ===
// === Returns null if the path is shorter than the specified distance ===
google.maps.Polygon.prototype.GetIndexAtDistance = function (metres) {
    // some awkward special cases
    if (metres == 0) return this.getPath().getAt(0);
    if (metres < 0) return null;
    var dist = 0;
    var olddist = 0;
    for (var i = 1;
        (i < this.getPath().getLength() && dist < metres); i++) {
        if (window.CP.shouldStopExecution(8)) { break; }
        olddist = dist;
        dist += this.getPath().getAt(i).distanceFrom(this.getPath().getAt(i - 1));
    }
    window.CP.exitedLoop(8);

    if (dist < metres) {
        return null;
    }
    return i;
}
// === Copy all the above functions to GPolyline ===
google.maps.Polyline.prototype.Distance = google.maps.Polygon.prototype.Distance;
google.maps.Polyline.prototype.GetPointAtDistance = google.maps.Polygon.prototype.GetPointAtDistance;
google.maps.Polyline.prototype.GetPointsAtDistance = google.maps.Polygon.prototype.GetPointsAtDistance;
google.maps.Polyline.prototype.GetIndexAtDistance = google.maps.Polygon.prototype.GetIndexAtDistance;

/*
17.498144,78.400846
17.499153,78.398915
17.497579,78.398255
17.496734,78.398014
17.49586,78.397861
17.495515,78.396724
17.495727,78.395755
17.495128,78.395235
17.493558,78.394594
17.492509,78.394132
17.490046,78.393192
17.488879,78.392712
17.486483,78.391768
17.482935,78.390234
17.476425,78.390823
17.471846,78.388581
17.4596,78.386948
17.454704,78.385529
17.452829,78.38719
17.452225,78.386606
17.450649,78.386231
17.453719,78.387579
17.452829,78.387856
17.452281,78.387154
17.451816,78.38602
17.451179,78.38593
17.450674,78.385907
17.450256,78.385879
17.449963,78.386096
*/