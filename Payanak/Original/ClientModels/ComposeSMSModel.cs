using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class ComposeSMSModel
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public List<string> Numbers { get; set; }
        public List<long> GroupIds { get; set; }
        public long? SendNumber { get; set; }
        public long? TemplateId { get; set; }
    }
}