using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using TestMaker.Services;

namespace TestMaker.ViewModels
{
    public class EditorTestQuestionsViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Test _test;
        private readonly ObservableCollection<Test> _allTests;

        private int _questionsToAnswer;

        public EditorTestQuestionsViewModel(MainViewModel mainViewModel, Test test)
        {
            _mainViewModel = mainViewModel;
            _test = test;
            _allTests = new ObservableCollection<Test>(JsonFileService.LoadTests());

            GoBackCommand = new RelayCommand(param => GoBack());
            SaveTestCommand = new RelayCommand(param => SaveTest());
            AddQuestionCommand = new RelayCommand(param => AddQuestion());
            RemoveQuestionCommand = new RelayCommand(param => RemoveQuestion((QuestionForm)param));

            // Wypełnij pytania i odpowiedzi testu
            Questions = new ObservableCollection<QuestionForm>();
            for (int i = 0; i < _test.Questions.Count; i++)
            {
                var question = _test.Questions[i];
                var questionForm = new QuestionForm
                {
                    Header = $"Pytanie {i + 1}",
                    Content = question.Content,
                    Answers = new ObservableCollection<Answer>(question.Answers)
                };
                Questions.Add(questionForm);
            }

            // Inicjalizacja QuestionsToAnswer z wartością zapisaną w teście
            QuestionsToAnswer = _test.QuestionsToAnswer;
        }

        // Komenda do powrotu
        public ICommand GoBackCommand { get; }

        // Komenda do zapisywania testu
        public ICommand SaveTestCommand { get; }

        // Komenda do dodawania pytania
        public ICommand AddQuestionCommand { get; }

        // Komenda do usuwania pytania
        public ICommand RemoveQuestionCommand { get; }

        // Lista pytań testu
        public ObservableCollection<QuestionForm> Questions { get; set; }

        // Właściwość do przechowywania liczby pytań do odpowiedzi
        public int QuestionsToAnswer
        {
            get { return _questionsToAnswer; }
            set
            {
                if (_questionsToAnswer != value)
                {
                    _questionsToAnswer = value;
                    OnPropertyChanged(nameof(QuestionsToAnswer));
                }
            }
        }

        // Metoda do powrotu
        private void GoBack()
        {
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }

        // Metoda do zapisywania testu
        private void SaveTest()
        {
            _test.QuestionsToAnswer = QuestionsToAnswer;

            var existingTest = _allTests.FirstOrDefault(t => t.Name == _test.Name && t.Category == _test.Category);

            if (existingTest != null)
            {
                existingTest.Questions.Clear();
                existingTest.QuestionsToAnswer = QuestionsToAnswer;

                foreach (var questionForm in Questions)
                {
                    var question = new Question
                    {
                        Content = questionForm.Content,
                        Answers = questionForm.Answers.ToList()
                    };
                    existingTest.Questions.Add(question);
                }
            }
            else
            {
                // Jeśli test nie istnieje, dodajemy nowy
                _allTests.Add(_test);
            }

            JsonFileService.SaveTests(_allTests.ToList());

            // Po zapisie przejdź do widoku EditorTestListViewModel
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }

        private void AddQuestion()
        {
            // Tworzenie nowego pytania
            QuestionForm newQuestionForm = new QuestionForm
            {
                Header = $"Pytanie {Questions.Count + 1}",
                Content = "Treść pytania"
            };

            // Dodawanie czterech pustych odpowiedzi
            for (int i = 0; i < 4; i++)
            {
                newQuestionForm.Answers.Add(new Answer
                {
                    Value = "",
                    IsCorrect = false
                });
            }

            // Dodawanie pytania do listy pytań
            Questions.Add(newQuestionForm);

            // Odświeżenie widoku
            OnPropertyChanged(nameof(Questions));
        }

        private void RemoveQuestion(QuestionForm question)
        {
            if (Questions.Contains(question))
            {
                Questions.Remove(question);

                // Odświeżenie widoku
                OnPropertyChanged(nameof(Questions));
            }
        }
    }
}
