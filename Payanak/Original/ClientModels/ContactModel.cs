namespace Backend.ClientModels
{
    public class ContactModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string NickName { get; set; }
        public long Birthday { get; set; }
        public short Gender { get; set; }
        public string NationalCode { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public long SpecialDay { get; set; }
        public string MobilePhone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string FormId { get; set; }
        public string Picture { get; set; }
        public decimal Credit { get; set; }
    }
}