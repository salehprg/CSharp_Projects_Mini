namespace Backend.ClientModels
{
    public class PermissionModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public short Level { get; set; }
        public long? Parent { get; set; }
        public bool Checked { get; set; }
    }
}