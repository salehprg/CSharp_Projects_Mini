using Backend.ClientModels;

namespace Backend.Models
{
    public partial class AdditionalInfo
    {
        public AdditionalInfoModel ToModel()
        {
            return new AdditionalInfoModel()
            {
                Id = this.UserId,
                Instagram = this.InstagramLink,
                SpecialDay = this.SpecialDate.HasValue ? this.SpecialDate.Value.Ticks : -1,
                Telegram = this.TelegramLink
            };
        }
    }
}