using System.Collections.Generic;

namespace Backend.ClientModels
{
 public class QueryParamModel
    {
        public string Filter { get; set; }
        public string SortOrder { get; set; }
        public string SortField { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}