using Backend.ClientModels;

namespace Backend.Models
{
    public partial class Group
    {
        public GroupModel ToModel()
        {
            return new GroupModel()
            {
                Id = this.Id,
                Descriptions = this.Descriptions,
                Parent = null,
                Status = this.Status.HasValue ? this.Status.Value : (short)1,
                Title = this.Title,
                Picture = this.Picture,
                Name = this.Name
            };
        }
    }
}