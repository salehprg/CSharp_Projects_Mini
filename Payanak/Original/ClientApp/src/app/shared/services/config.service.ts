import {Injectable} from '@angular/core';
import {TemplateConfig} from '../template-config/config.interface';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  public templateConf: TemplateConfig;
  options = {
    direction: 'ltr',
    bgColor: 'black',
    transparentColor: '',
    bgImage: 'assets/img/sidebar-bg/01.jpg',
    bgImageDisplay: true,
    compactMenu: false,
    sidebarSize: 'sidebar-md',
    layout: 'dark'
  };
  constructor() {
    const theme = localStorage.getItem('theme');
    const themeDetail = localStorage.getItem('themeDetail');
    if (theme) {
      this.templateConf = JSON.parse(theme);
      if (themeDetail) {
        let td = JSON.parse(themeDetail);
        td = {...this.options, ...td};
        this.templateConf.layout.sidebar.size = td.sidebarSize;
        this.templateConf.layout.dir = td.direction;
        this.templateConf.layout.variant = td.layout;
        this.templateConf.layout.sidebar.collapsed = td.compactMenu;
        this.templateConf.layout.sidebar.backgroundColor = td.transparentColor;
        this.templateConf.layout.sidebar.backgroundImage = td.bgImageDisplay;
        this.templateConf.layout.sidebar.backgroundImageURL = td.bgImage;
      }
    } else {
      this.setConfigValue();
      localStorage.setItem('theme', JSON.stringify(this.templateConf));
    }
  }

  setConfigValue() {
    this.templateConf = {
      layout: {
        variant: 'Dark', // options:  Dark, Light & Transparent
        dir: 'rtl', // Options: ltr, rtl
        customizer: {
          hidden: true // options: true, false
        },
        sidebar: {
          collapsed: false, // options: true, false
          size: 'sidebar-md', // Options: 'sidebar-lg', 'sidebar-md', 'sidebar-sm'
          backgroundColor: 'black',
          // Options: 'black', 'pomegranate', 'king-yna', 'ibiza-sunset', 'flickr', 'purple-bliss', 'man-of-steel', 'purple-love'

          /* If you want transparent layout add any of the following mentioned classes
          to backgroundColor of sidebar option :
            bg-glass-1, bg-glass-2, bg-glass-3, bg-glass-4, bg-hibiscus, bg-purple-pizzaz,
             bg-blue-lagoon, bg-electric-viloet, bg-protage, bg-tundora
          */
          backgroundImage: true, // Options: true, false | Set true to show background image
          backgroundImageURL: 'assets/img/sidebar-bg/01.jpg'
        }
      }
    };
  }
}
