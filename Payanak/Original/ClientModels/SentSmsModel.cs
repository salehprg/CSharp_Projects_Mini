using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class SentSMSModel
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public List<string> Numbers { get; set; }
        public List<long> GroupIds { get; set; }
        public List<long> Delivery { get; set; }
        public short Status { get; set; }
        public long SentDate { get; set; }
        public long Count { get; set; }
        public decimal Price { get; set; }
        public string SendNumber { get; set; }
    }
}