using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class CalendarEvents
    {
        public int Id { get; set; }
        public int DateDay { get; set; }
        public int MonthId { get; set; }
        public string DayName { get; set; }
        public string MonthName { get; set; }
        public string Description { get; set; }
        public string IsClose { get; set; }

    }
}
