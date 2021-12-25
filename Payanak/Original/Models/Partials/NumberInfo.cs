using Backend.ClientModels;

namespace Backend.Models
{
    public partial class NumberInfo
    {
        public NumberModel ToModel()
        {
            return new NumberModel()
            {
                Id = this.Id,
                IsBlocked = this.IsBlocked.HasValue ? this.IsBlocked.Value : false,
                IsShared = this.IsShared.HasValue ? this.IsShared.Value : false,
                Owner = this.Owner.HasValue ? this.Owner.Value : -1,
                Password = "",
                Type = this.Type.HasValue ? this.Type.Value : (short)1,
                Username = this.Username,
                Number = this.Number,
                CreateDate = this.CreateDate.HasValue ? this.CreateDate.Value.Ticks : -1,
                User = null
            };
        }
    }
}