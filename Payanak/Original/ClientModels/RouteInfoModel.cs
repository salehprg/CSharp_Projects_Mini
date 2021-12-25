using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class RouteInfo
    {
        public string path { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string Class { get; set; }
        public string badge { get; set; }
        public string badgeClass { get; set; }
        public bool isExternalLink { get; set; }
        public List<RouteInfo> submenu { get; set; }

        public RouteInfo(){
            submenu = new List<RouteInfo>();
        }
    }
}