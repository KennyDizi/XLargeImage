using Xamarin.Forms;
using XLargeImage.SourceCode.Views;

namespace XLargeImage
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new TestPageView();
        }
    }
}
