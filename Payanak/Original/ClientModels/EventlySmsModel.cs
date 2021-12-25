namespace Backend.ClientModels
{
    public class EventlySmsModel
    {
        public long Id { get; set; }
        public ScheduleSmsInfoModel Parent {get;set;}
        public long Date{get;set;}
        public long UpdatedDate{get;set;}
        public long Counter{get;set;}
        public ContactModel User{get;set;}

        public int EventId { get; set; }

  }
}