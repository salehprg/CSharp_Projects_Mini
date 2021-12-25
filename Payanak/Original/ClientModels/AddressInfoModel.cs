namespace Backend.ClientModels
{
 public class AddressInfoModel
    {
        public long Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address {get;set;}
    }
}