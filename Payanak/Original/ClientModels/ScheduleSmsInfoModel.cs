namespace Backend.ClientModels
{
    public class ScheduleSmsInfoModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Code { get; set; }
        public int AddedYear { get; set; }
        public int AddedMonth { get; set; }
        public int AddedDay { get; set; }
        public ContactModel Owner { get; set; }
        public TemplateModel Template { get; set; }
        public NumberModel Number { get; set; }
        public short Status { get; set; }
    }
}