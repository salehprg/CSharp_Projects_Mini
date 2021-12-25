/// <reference types="@types/googlemaps" />
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import * as data from './MashhadMap.json'


interface PositionData {
  Positions: string;
  MCI:string;
  Irancell:string;
}

@Component({
  selector: 'app-MapPostalCode',
  templateUrl: './MapPostalCode.component.html',
  styleUrls: ['./MapPostalCode.component.css']
})
export class MapPostalCodeComponent implements OnInit {

  constructor() { }

  @ViewChild('mapContainer', { static: false })
  gmap: ElementRef;
  map: google.maps.Map;
  lat = 36.29;
  lng = 59.59;
  coordinates = new google.maps.LatLng(this.lat, this.lng);

  mapOptions: google.maps.MapOptions = {
    center: this.coordinates,
    zoom: 13,
    mapTypeId: google.maps.MapTypeId.ROADMAP,
    disableDefaultUI : true
  };

  ngAfterViewInit(): void {
    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.
    this.GooglemapInitializer();
  }

  ngOnInit() {
  }

  GooglemapInitializer() {

    this.map = new google.maps.Map(this.gmap.nativeElement, this.mapOptions);

    var PosData = data.d.Positions.split("}");
    var __selected_overlays = new Array();

    for (var x = 0; x < PosData.length; x++) {

      var myTrip = new Array();

      PosData[x] = PosData[x].replace('{', '');

      var SplitedData = PosData[x].split(":");

      var PostalCode = SplitedData[0];

      var longlatdata = SplitedData[2].split(";");


      for (var i = 0; i < longlatdata.length; i++) {
        if (longlatdata[i] == null) continue;

        var temp = longlatdata[i].split(',');
        temp[0] = temp[0].trim().replace('"' , '');
        temp[1] = temp[1].trim().replace('"' , '');
        myTrip[i] = new google.maps.LatLng(Number(temp[0]), Number(temp[1]));
      }

      var flightPath = new google.maps.Polygon({
        paths: myTrip,
        strokeColor: "#9F69EA",
        strokeOpacity: 1,
        strokeWeight: 1,
        fillColor: "#9F69EA",
        fillOpacity: 0
      });

      flightPath.set("PostalCode" , PostalCode)

      var infowindow = new google.maps.InfoWindow();

      var j = 0;

      flightPath.addListener('click', function (e) {
        var marker = new google.maps.Marker({
          position: e.latLng,
          map : this.getMap(),
          icon: 'images/flag.png',
        });

        for (var t in __selected_overlays) {
          __selected_overlays[t].setOptions({ fillColor: "#9F69EA", fillOpacity: 0 });
        }

        this.setOptions({fillColor : "#C0A5FF" , fillOpacity: 0.5})
        infowindow.setContent("<div>" + this.get("PostalCode") + "</div>");

        infowindow.open(this.getMap(), marker)
        __selected_overlays[j] = this;
        j++
      });



      flightPath.setMap(this.map);


    }
  }
}
