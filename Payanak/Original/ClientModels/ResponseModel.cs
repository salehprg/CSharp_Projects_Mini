using System.Collections.Generic;

namespace Backend.ClientModels{
    public class ResponseModel
    {
        public List<ResponseStatusModel> Status {get;set;}
        public int TotalCount {get;set;}
        public object Result{get;set;}
    }
}