using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class UserModel
    {
        public AccountInfoModel AccountInfo { get; set; }
        public AdditionalInfoModel AdditionalInfo { get; set; }
        public PersonalInfoModel PersonalInfo { get; set; }
        public AddressInfoModel AddressInfo { get; set; }
        public List<long> Roles { get; set; } = new List<long>();
    }
}