using Backend.ClientModels;

namespace Backend.Models
{
    public partial class AddressInfo
    {
        public AddressInfoModel ToModel()
        {
            return new AddressInfoModel()
            {
                Id = this.UserId,
                Address = this.Address,
                City = this.City,
                Latitude = this.Latitude.HasValue ? this.Latitude.Value.ToString() : "",
                Longitude = this.Longitude.HasValue ? this.Longitude.Value.ToString() : "",
                Region = this.Region
            };
        }
    }
}