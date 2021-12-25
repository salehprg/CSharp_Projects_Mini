using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class DashboardModel
    {
        public long GroupCount { get; set; }
        public long ContactCount { get; set; }
        public long PanelCount { get; set; }
        public decimal Credit { get; set; }
        public List<long> Contacts10Day { get; set; }
        public List<long> Count10Day { get; set; }
        public List<long> CountCalc10Day { get; set; }


    }
}