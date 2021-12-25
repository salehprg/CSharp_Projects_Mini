namespace Backend.ClientModels
{
 public class AccountInfoModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password {get;set;}
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string BusinessPhone { get; set; }
        public long CreateDate { get; set; }
        public long LastLogin { get; set; }
        public string Picture { get; set; } 
        public string FormId{get;set;}
    }
}