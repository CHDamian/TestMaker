using System.Collections.ObjectModel;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;

namespace TestMaker.ViewModels
{
    public class EditorTestQuestionsViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Test _test;

        public EditorTestQuestionsViewModel(MainViewModel mainViewModel, Test test)
        {
            _mainViewModel = mainViewModel;
            _test = test;

            GoBackCommand = new RelayCommand(param => GoBack());
            SaveTestCommand = new RelayCommand(param => SaveTest());
            AddQuestionCommand = new RelayCommand(param => AddQuestion());

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
        }

        // Komenda do powrotu
        public ICommand GoBackCommand { get; }

        // Komenda do zapisywania testu
        public ICommand SaveTestCommand { get; }

        // Komenda do dodawania pytania
        public ICommand AddQuestionCommand { get; }

        // Lista pytań testu
        public ObservableCollection<QuestionForm> Questions { get; set; }

        // Metoda do powrotu
        private void GoBack()
        {
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }

        // Metoda do zapisywania testu
        private void SaveTest()
        {
            // Implementacja zapisywania testu
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
    }
}
