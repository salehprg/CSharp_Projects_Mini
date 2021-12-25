namespace Backend.ClientModels
{
    public class PanelVersionModel
    {
        public long Id { get; set; }
        public string Nickname { get; set; }
        public decimal MinVersion { get; set; }
        public decimal MaxVersion { get; set; }
        public string Path { get; set; }
        public long CreateDate { get; set; }
    }
}