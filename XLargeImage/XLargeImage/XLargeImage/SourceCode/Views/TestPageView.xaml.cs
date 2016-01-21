using Xamarin.Forms;

namespace XLargeImage.SourceCode.Views
{
    public partial class TestPageView : ContentPage
    {
        public TestPageView()
        {
            InitializeComponent();
            BindingContext = new TestPageViewModel();
        }
    }
}
