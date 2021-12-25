import {AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {GeoLocationService} from '../services/location/geo-location.service';
import {HttpClient} from '@angular/common/http';
import {Subscription} from 'rxjs';
import {marker} from './marker.image';


declare var ol: any;

@Component({
  selector: 'app-osm-view',
  templateUrl: './osm-view.component.html',
  styleUrls: ['./osm-view.component.scss'],
  providers: [HttpClient, GeoLocationService]
})
export class OsmViewComponent implements OnInit, OnDestroy, AfterViewInit {
  map: any;
  @Input()
  geoReverseService =
    'https://nominatim.openstreetmap.org/reverse?format=json&addressdetails=1&lat={lat}&lon={lon}';
  // 'https://nominatim.openstreetmap.org/reverse?key=iTzWSiYpGxDvhATNtSrqx5gDcnMOkntL&format=json&addressdetails=1&lat={lat}&lon={lon}';

  @Input()
  width = '100%';
  @Input()
  height = '400px';

  @Input()
  latitude = 52.520008;
  @Input()
  longitude = 13.404954;

  @Input()
  latitudePointer = 52.520008;
  @Input()
  longitudePointer = 13.404954;

  @Input()
  showControlsZoom: boolean;
  @Input()
  titleZoomIn = 'بزرگ نمایی';
  @Input()
  titleZoomOut = 'کوچک نمایی';
  @Input()
  showControlsCurrentLocation: boolean;
  @Input()
  titleCurrentLocation = 'مکان شما';

  @Input()
  showDebugInfo: boolean;
  @Input()
  opacity = 1;
  @Input()
  zoom = 14;

  markerImage = marker;

  reverseGeoSub: Subscription = null;
  pointedAddress: string;
  pointedAddressOrg: string;
  position: any;
  dirtyPosition;

  @Output()
  addressChanged = new EventEmitter<any>();

  constructor(private httpClient: HttpClient, private geoLocationService: GeoLocationService) {
  }

  setCenter() {
    const view = this.map.getView();
    view.setCenter(ol.proj.fromLonLat([this.longitude, this.latitude]));
    view.setZoom(8);
  }

  ngAfterViewInit(): void {
    const mousePositionControl = new ol.control.MousePosition({
      coordinateFormat: ol.coordinate.createStringXY(4),
      projection: 'EPSG:4326',
      // comment the following two lines to have the mouse position
      // be placed within the map.
      className: 'custom-mouse-position',
      target: document.getElementById('mouse-position'),
      undefinedHTML: '&nbsp;'
    });
    this.map = new ol.Map({

      target: 'map',
      layers: [
        new ol.layer.Tile({
          source: new ol.source.OSM(),
        }),
        new ol.layer.Vector(
          {}
        )
      ],
      view: new ol.View({
        center: ol.proj.fromLonLat([59.59362030029296, 36.37264499608118]),
        zoom: 8,

      }),

    });

    const cls = this;
    this.map.on('click', function(args) {
      console.log(args.coordinate);
      const lonlat = ol.proj.transform(args.coordinate, 'EPSG:3857', 'EPSG:4326');
      console.log(lonlat);

      const lon = lonlat[0];
      const lat = lonlat[1];
      cls.longitudePointer = lon;
      cls.latitudePointer = lat;
      cls.reverseGeo();
      // alert(`lat: ${lat} long: ${lon}`);
    });

  }

  ngOnInit() {

    // var map = new Map({
    //   interactions: defaultInteractions().extend([
    //     new DragRotateAndZoom()
    //   ]),
    //   layers: [
    //     new TileLayer({
    //       source: new OSM()
    //     })
    //   ],
    //   target: 'map',
    //   view: new View({
    //     center: [0, 0],
    //     zoom: 2
    //   })
    // });
    if (this.showControlsCurrentLocation) {
      this.geoLocationService.getLocation().subscribe((position) => {
        this.position = position;
        if (!this.dirtyPosition) {
          this.dirtyPosition = true;
          this.longitude = this.longitudePointer = this.position.coords.longitude;
          this.latitude = this.latitudePointer = this.position.coords.latitude;
        }
      });
    }
  }

  ngOnDestroy() {
    if (this.reverseGeoSub) {
      this.reverseGeoSub.unsubscribe();
    }
  }

  onSingleClick(event) {
    // const lonlat = proj.transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
    // this.longitudePointer = lonlat[0];
    // this.latitudePointer = lonlat[1];
    this.reverseGeo();
  }

  increaseOpacity() {
    this.opacity += 0.1;
  }

  decreaseOpacity() {
    this.opacity -= 0.1;
  }

  increaseZoom() {
    this.zoom++;
  }

  decreaseZoom() {
    this.zoom--;
  }

  setCurrentLocation(event) {
    // TODO FIX: setting current location does move the pointer but not the map!!!
    if (this.position) {
      this.longitude = this.longitudePointer = this.position.coords.longitude;
      this.latitude = this.latitudePointer = this.position.coords.latitude;
      /**
       * Trigger new address change
       */
      this.reverseGeo();
    }
  }

  reverseGeo() {
    const service = (this.geoReverseService || '')
      .replace(new RegExp('{lon}', 'ig'), `${this.longitudePointer}`)
      .replace(new RegExp('{lat}', 'ig'), `${this.latitudePointer}`);
    this.reverseGeoSub = this.httpClient.get(service).subscribe((data: any) => {
      const val = (data || {});

      this.pointedAddressOrg = val['display_name'];
      const address = [];

      const building = [];
      if (val['address']['building']) {
        building.push(val['address']['building']);
      }
      if (val['address']['mall']) {
        building.push(val['address']['mall']);
      }
      if (val['address']['theatre']) {
        building.push(val['address']['theatre']);
      }

      const zip_city = [];
      if (val['address']['postcode']) {
        zip_city.push(val['address']['postcode']);
      }
      if (val['address']['city']) {
        zip_city.push(val['address']['city']);
      }
      const street_number = [];
      if (val['address']['street']) {
        street_number.push(val['address']['street']);
      }
      if (val['address']['road']) {
        street_number.push(val['address']['road']);
      }
      if (val['address']['footway']) {
        street_number.push(val['address']['footway']);
      }
      if (val['address']['pedestrian']) {
        street_number.push(val['address']['pedestrian']);
      }
      if (val['address']['house_number']) {
        street_number.push(val['address']['house_number']);
      }

      if (building.length) {
        address.push(building.join(' '));
      }
      if (zip_city.length) {
        address.push(zip_city.join(' '));
      }
      if (street_number.length) {
        address.push(street_number.join(' '));
      }

      // this.pointedAddress = address.join(', ');
      let adrs = data.display_name;
      let state = '';
      let county = '';
      for (let i = 0; i < 3; i++) {
        const indx = adrs.lastIndexOf(',');
        const data = adrs.substr(indx).replace(',', '').trim();
        adrs = adrs.substr(0, indx);
        // PostalCode
        if (this.isNormalInteger(data)) {
          i--;
          continue;
        }
        // country
        if (i === 0) {
        }
        // State
        if (i === 1) {
          state = data;
        }
        // County
        if (i === 2) {
          county = data;
        }
      }
      const addrss = {
        region: state,
        city: county,
        address: adrs,
        latitude: this.latitudePointer,
        longitude: this.longitudePointer
      };
      this.addressChanged.emit(addrss);
    });
  }

  isNormalInteger(str) {
    return /^\+?(0|[1-9]\d*)$/.test(str);
  }
}
