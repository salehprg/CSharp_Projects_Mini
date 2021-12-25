namespace Backend.ClientModels
{
    public class PanelModel
    {
        public long Id { get; set; }
        public string Serial { get; set; }
        public long CreateDate { get; set; }
        public string HashId { get; set; }
        public string Number { get; set; }
        public GroupModel Group { get; set; }
        public ContactModel User { get; set; }
        public string Version { get; set; }
        public long LastActivity { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
        public short Status { get; set; }
        public TemplateModel Template { get; set; }
        public NumberModel SendNumber { get; set; }

    }
}