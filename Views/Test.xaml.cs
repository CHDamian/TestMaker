using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TestMaker.Views
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {
        public Test()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                StartFadeInAnimation();
            }
        }

        private void StartFadeInAnimation()
        {
            var storyboard = (Storyboard)FindResource("FadeInStoryboard");
            storyboard.Begin(MainGrid);
        }
    }
}