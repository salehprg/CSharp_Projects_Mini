using Backend.ClientModels;

namespace Backend.Models
{
    public partial class PersonalInfo
    {
        public PersonalInfoModel ToModel()
        {
            return new PersonalInfoModel()
            {
                Id = this.UserId,
                Birthday = this.Birthday.HasValue ? this.Birthday.Value.Ticks : -1,
                FName = this.FirstName,
                LName = this.LastName,
                Gender = this.Gender.HasValue ? this.Gender.Value : (short)0,
                NationalCode = this.NationalCode,
                NickName = this.NickName
            };
        }
    }
}