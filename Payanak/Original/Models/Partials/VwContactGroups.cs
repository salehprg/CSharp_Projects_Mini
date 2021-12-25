using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwContactGroups
    {
        public ContactModel ToContactModel()
        {
            return new ContactModel()
            {
                Id = this.Id.Value,
                Birthday = this.Birthday.HasValue ? this.Birthday.Value.Ticks : -1,
                FName = this.FirstName,
                LName = this.LastName,
                Gender = this.Gender.HasValue ? this.Gender.Value : (short)0,
                MobilePhone = this.MobilePhone,
                Address = this.Address,
                Instagram = this.InstagramLink,
                Latitude = this.Latitude?.ToString(),
                Longitude = this.Longitude?.ToString(),
                NationalCode = "",
                SpecialDay = this.SpecialDate.HasValue ? this.SpecialDate.Value.Ticks : -1,
                Telegram = this.TelegramLink,
                NickName = this.NickName,
                FormId = this.FormGuid
            };
        }
    }
}