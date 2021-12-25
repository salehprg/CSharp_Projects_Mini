using Backend.ClientModels;

namespace Backend.Models
{
    public partial class AccountInfo
    {
        public AccountInfoModel ToModel(){
            return new AccountInfoModel(){
                Id = this.Id,
                Username = this.Username,
                Email = this.Email,
                MobilePhone = this.MobilePhone,
                HomePhone = this.HomePhone,
                BusinessPhone = this.BusinessPhone,
                CreateDate = this.CreateDate.Ticks,
                LastLogin = this.LastLogin.Ticks,
                Picture = this.Picture,
                FormId = this.FormGuid
            };
        }
    }
}