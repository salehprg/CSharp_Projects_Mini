using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class InDepthJson
    {
        public static string Serialize(object obj)
        {
            var propertis = obj.GetType().GetProperties();
            if(propertis == null)
            return System.Text.Json.JsonSerializer.Serialize(obj);
            var str = "{";
            foreach (var item in propertis)
            {
                str += Serialize(item.GetConstantValue());
            }
            str+="}";
            return null;
        }
    }
}