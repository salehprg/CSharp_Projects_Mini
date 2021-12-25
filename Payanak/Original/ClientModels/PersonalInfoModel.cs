namespace Backend.ClientModels
{
 public class PersonalInfoModel
    {
        public long Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string NickName { get; set; }
        public long Birthday { get; set; }
        public short Gender { get; set; }
        public string NationalCode { get; set; }
    }
}