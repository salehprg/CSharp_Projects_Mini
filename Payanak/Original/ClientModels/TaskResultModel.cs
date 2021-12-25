using System;
using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class TaskResultModel
    {
        public List<ResponseStatusModel> Status { get; set; }
        public int Percent { get; set; }

        public string Header { get; set; }

        public long UserId { get; set; }

        public DateTime CreateDate { get; set; }
        public TaskResultModel()
        {
            Header = "";
            Status = new List<ResponseStatusModel>();
            Percent = 0;
            this.CreateDate = DateTime.UtcNow;
        }
    }
}