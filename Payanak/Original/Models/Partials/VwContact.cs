using Backend.ClientModels;

namespace Backend.Models
{
    public partial class VwContact
    {
        public ContactModel ToModel()
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
                Username = this.Username,
                FormId = this.IsFormValid.Value == 1 ? this.FormGuid : null,
                Picture = this.Picture,
                Credit = this.Credit.HasValue ? this.Credit.Value : 0
            };
        }
        public CreditModel ToCreditModel()
        {
            return new CreditModel()
            {
                Credit = this.Credit.HasValue ? this.Credit.Value : 0,
                Discount = this.Discount.HasValue ? this.Discount.Value : (short)0
            };
        }
    }
}