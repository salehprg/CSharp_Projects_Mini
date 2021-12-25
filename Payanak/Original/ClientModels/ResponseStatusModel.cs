using System.Collections.Generic;

namespace Backend.ClientModels
{
    public class ResponseStatusModel
    {
        public int status
        {
            get { return _status; }
            set
            {
                _status = value;
                _message = ResponseStatus.GetResponseText(value);
            }
        }
        public string Message => _message;
        public ResponseStatusModel() { }
        public ResponseStatusModel(int stat)
        {
            status = stat;
        }
        private int _status;
        private string _message;
    }
}