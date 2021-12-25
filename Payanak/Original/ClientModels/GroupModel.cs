namespace Backend.ClientModels
{
    public class GroupModel
    {
        public string Title { get; set; }
        public long Id { get; set; }
        public GroupModel Parent { get; set; }
        public string Descriptions { get; set; }
        public short Status { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
    }
}