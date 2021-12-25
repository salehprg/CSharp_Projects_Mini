using System.Collections.Generic;

namespace BotTelegram.Models
{
    public class UpdateModel 
    {
        public bool ok {get; set;}
        public List<UpdateMessages> result {get; set;}
        
    }
}