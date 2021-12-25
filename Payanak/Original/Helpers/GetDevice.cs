using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using DeviceDetectorNET;

namespace Backend.Helpers
{
    public class DeviceDetection
    {
        public static DeviceInfo GetDevice(string userAgent)
        {
            var dd = new DeviceDetector(userAgent);
            dd.Parse();
            var clientInfo = dd.GetClient();
            if (!clientInfo.Success)
                return null;
            var osInfo = dd.GetOs();
            var device = dd.GetDeviceName();
            var brand = dd.GetBrand();
            var model = dd.GetModel();
            return new DeviceInfo
            {
                Browser = clientInfo.Match.Name + "_$_" + clientInfo.Match.Version,
                Os = osInfo.Success ? (osInfo.Match.Name + "_$_" + osInfo.Match.Platform + "_$_" + osInfo.Match.Version) : "",
                Platform = string.IsNullOrEmpty(brand) ?
                (string.IsNullOrEmpty(model) ? (device) : (model + "_$_" + device)) :
                (brand + "_$_" + (string.IsNullOrEmpty(model) ? (device) : (model + "_$_" + device)))
            };
        }
    }
}