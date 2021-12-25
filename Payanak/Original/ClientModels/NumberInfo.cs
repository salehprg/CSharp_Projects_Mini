namespace Backend.ClientModels
{
    public class NumberModel
    {
        public string Username { get; set; }
        public long Id { get; set; }
        public bool IsShared { get; set; }
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public short Type { get; set; }
        public long? Owner { get; set; }
        public string Number{get;set;}
        public long CreateDate{get;set;}
        public ContactModel User{get;set;}

    }
}