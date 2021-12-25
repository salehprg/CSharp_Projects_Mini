namespace Backend.ClientModels{
    public class BusinessCardModel
    {
        public long Id{get;set;}
        public string Key{get;set;}
        public long CreateDate {get;set;}
        public bool IsBlocked{get;set;}
        public short Status{get;set;}
        public ContactModel User{get;set;}
        public GroupModel Group{get;set;}
        public TemplateModel Template{get;set;}
        public NumberModel Number{get;set;}
    }
}