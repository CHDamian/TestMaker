using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TestMaker.Models
{
    public class Question : INotifyPropertyChanged
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        private List<Answer> _answers;
        public List<Answer> Answers
        {
            get { return _answers; }
            set
            {
                if (_answers != value)
                {
                    _answers = value;
                    OnPropertyChanged(nameof(Answers));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
