namespace BotTelegram.Models
{
    public class User
    {
        public int Id {get;set;}
        public bool is_bot {get;set;}
        public string first_name {get;set;}
        public string last_name {get;set;}
        public string sername {get;set;}
        public string language_code {get;set;}
        public bool can_join_groups {get;set;}
        public bool can_read_all_group_messages {get;set;}
        public bool supports_inline_queries {get;set;}
    }
}