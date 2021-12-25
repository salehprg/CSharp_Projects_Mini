namespace Backend.ClientModels
{
    public class TicketModel
    {
        public long Id { get; set; }
        public long CreateDate { get; set; }
        public ContactModel User { get; set; }
        public ContactModel Responder { get; set; }
        public short Status { get; set; }
        public string Header { get; set; }

        public string LastMessage { get; set; }
        public long Unread { get; set; }
    }
}