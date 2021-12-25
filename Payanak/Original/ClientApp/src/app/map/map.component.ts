import {Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewEncapsulation} from '@angular/core';

import Map from 'ol/Map';
import {fromLonLat, transform} from 'ol/proj';
import Point from 'ol/geom/Point';
import Feature from 'ol/Feature';
import {Vector as VectorLayer} from 'ol/layer';
import {Icon, Style} from 'ol/style';
import VectorSource from 'ol/source/Vector';
import {MapService} from './map.service';
import {MapidService} from './mapid.service';
import {Subscription} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {NominatimRank} from './map-rank.config';

/**
 * Map Component: load and display a map
 * @example
 * <app-map id="map"></app-map>
 */
@Component({
  selector: 'app-map',
  template: '',
  // Include ol style as global
  encapsulation: ViewEncapsulation.None,
  styleUrls: [
    '../../../node_modules/ol/ol.css',
    '../../../node_modules/ol-ext/dist/ol-ext.css'
  ],
  providers: [MapidService]
})

export class MapComponent implements OnInit, OnDestroy {
  @Input()
  geoReverseService =
    'https://nominatim.openstreetmap.org/reverse?format=jsonv2&addressdetails=1&lat={lat}&lon={lon}';
  // 'https://nominatim.openstreetmap.org/reverse?key=iTzWSiYpGxDvhATNtSrqx5gDcnMOkntL&format=json&addressdetails=1&lat={lat}&lon={lon}';

  /** Map id
   */
  @Input() id: string;

  /** Longitude of the map
   */
  @Input() lon: string;

  /** Latitude of the map
   */
  @Input() lat: string;

  /** Zoom of the map
   */
  @Input() zoom: string;

  @Input()
  latitudePointer = 52.520008;
  @Input()
  longitudePointer = 13.404954;
  /**
   * [ol.Map](http://openlayers.org/en/latest/apidoc/ol.Map.html) Openlayer map object
   */
  map: Map;
  clickPoint: Feature;
  @Output()
  addressChanged = new EventEmitter<any>();

  reverseGeoSub: Subscription = null;
  pointedAddressOrg: string;

  constructor(
    private mapService: MapService,
    private mapidService: MapidService,
    private elementRef: ElementRef,
    private httpClient: HttpClient
  ) {
  }

  ngOnDestroy() {
    if (this.reverseGeoSub) {
      this.reverseGeoSub.unsubscribe();
    }
  }

  /**
   * Create map on Init
   */
  ngOnInit() {
    // Register a new id in the Mapid Service
    this.mapidService.setId(this.id);
    // Create a new map
    this.map = this.mapService.getMap(this.id);
    // If id is not defined place the map in the component element itself
    if (!this.id) {
      this.id = 'map';
      this.map.setTarget(this.elementRef.nativeElement);
    }
    // Center on attribute
    this.map.getView().setCenter(fromLonLat([parseFloat(this.lon) || 0, parseFloat(this.lat) || 0]));
    this.map.getView().setZoom(this.zoom);
    const clas = this;
    this.map.on('click', function(args) {
      const lonlat = transform(args.coordinate, 'EPSG:3857', 'EPSG:4326');

      const lon = lonlat[0];
      const lat = lonlat[1];
      clas.longitudePointer = lon;
      clas.latitudePointer = lat;
      clas.clickPoint = new Feature({
        geometry: new Point(args.coordinate)
      });
      clas.clickPoint.setStyle(new Style({
        image: new Icon({
          color: '#8959A8',
          crossOrigin: 'anonymous',
          src: 'assets/img/ico/position.svg',
          scale: .2
        })
      }));
      const vectorSource = new VectorSource({
        features: [clas.clickPoint]
      });
      const vectorLayer = new VectorLayer({
        source: vectorSource
      });
      const vecs = [];
      for (const vec of clas.map.getLayers().array_) {
        if (vec.type === 'VECTOR') {
          vecs.push(vec);
        }
      }
      for (const vec of vecs) {
        clas.map.removeLayer(vec);
      }
      clas.map.addLayer(vectorLayer);
      clas.reverseGeo();
      // alert(`lat: ${lat} long: ${lon}`);
    });
  }

  reverseGeo() {
    const service = (this.geoReverseService || '')
      .replace(new RegExp('{lon}', 'ig'), `${this.longitudePointer}`)
      .replace(new RegExp('{lat}', 'ig'), `${this.latitudePointer}`);

    const headers = new HttpHeaders({'accept-language': 'fa-IR'});
    this.reverseGeoSub = this.httpClient.get(service, {headers}).subscribe((data: any) => {
      let val = (data || {});
      const type = val.type;
      if (val && val.address) {
        val = val.address;
      } else {
        console.error('NO_ADDRESS');
        return;
      }
      let point = '';
      let state = '';
      let county = '';
      for (const rank of NominatimRank) {
        for (const itm of rank) {
          if (val[itm]) {
            if (itm === 'state' || itm === 'region') {
              state = val[itm];
              break;
            }
            if (itm === 'city' || itm === 'county') {
              county = val[itm];
              break;
            }
            point = point + (point !== '' ? ' - ' : '') + val[itm];
            break;
          }
        }
      }
      if (type && val[type]) {
        point = point + (point !== '' ? ' - ' : '') + val[type];
      }
      // console.log(point);
      // this.pointedAddressOrg = val['display_name'];
      // const address = [];
      //
      // const building = [];
      // if (val['address']['building']) {
      //   building.push(val['address']['building']);
      // }
      // if (val['address']['mall']) {
      //   building.push(val['address']['mall']);
      // }
      // if (val['address']['theatre']) {
      //   building.push(val['address']['theatre']);
      // }
      //
      // const zip_city = [];
      // if (val['address']['postcode']) {
      //   zip_city.push(val['address']['postcode']);
      // }
      // if (val['address']['city']) {
      //   zip_city.push(val['address']['city']);
      // }
      // const street_number = [];
      // if (val['address']['street']) {
      //   street_number.push(val['address']['street']);
      // }
      // if (val['address']['road']) {
      //   street_number.push(val['address']['road']);
      // }
      // if (val['address']['footway']) {
      //   street_number.push(val['address']['footway']);
      // }
      // if (val['address']['pedestrian']) {
      //   street_number.push(val['address']['pedestrian']);
      // }
      // if (val['address']['house_number']) {
      //   street_number.push(val['address']['house_number']);
      // }
      //
      // if (building.length) {
      //   address.push(building.join(' '));
      // }
      // if (zip_city.length) {
      //   address.push(zip_city.join(' '));
      // }
      // if (street_number.length) {
      //   address.push(street_number.join(' '));
      // }
      //
      // // this.pointedAddress = address.join(', ');
      // let adrs = data.display_name;
      // let state = '';
      // let county = '';
      // for (let i = 0; i < 3; i++) {
      //   const indx = adrs.lastIndexOf(',');
      //   const data = adrs.substr(indx).replace(',', '').trim();
      //   adrs = adrs.substr(0, indx);
      //   // PostalCode
      //   if (this.isNormalInteger(data)) {
      //     i--;
      //     continue;
      //   }
      //   // country
      //   if (i === 0) {
      //   }
      //   // State
      //   if (i === 1) {
      //     state = data;
      //   }
      //   // County
      //   if (i === 2) {
      //     county = data;
      //   }
      // }
      const addrss = {
        region: state,
        city: county,
        address: point,
        latitude: this.latitudePointer,
        longitude: this.longitudePointer
      };
      console.log(addrss);
      this.addressChanged.emit(addrss);
    });
  }

  isNormalInteger(str) {
    return /^\+?(0|[1-9]\d*)$/.test(str);
  }
}
