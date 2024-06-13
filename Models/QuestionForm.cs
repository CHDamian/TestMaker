using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TestMaker.Models
{
    public class QuestionForm : INotifyPropertyChanged
    {
        private string _header;
        private string _content;
        private ObservableCollection<Answer> _answers;

        public string Header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(nameof(Header));
                }
            }
        }

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

        public ObservableCollection<Answer> Answers
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

        public QuestionForm()
        {
            Answers = new ObservableCollection<Answer>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
