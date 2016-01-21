using System;
using System.Windows.Input;
using XLargeImage.SourceCode.Common.Mvvm;

namespace XLargeImage.SourceCode.Views
{
    public class TestPageViewModel : BindableBase
    {
        private string _largeImageSource;

        public string LargeImageSource
        {
            get { return _largeImageSource ?? (_largeImageSource = string.Empty); }
            set { Set(() => LargeImageSource, ref _largeImageSource, value); }
        }

        private DelegateCommand _changeLargeImageSourceCommand;

        public ICommand ChangeLargeImageSourceCommand
            =>
                _changeLargeImageSourceCommand
                    ?? (_changeLargeImageSourceCommand = new DelegateCommand(ChangeLargeImageSourceCommandAction));

        private void ChangeLargeImageSourceCommandAction()
        {
            LargeImageSource = "WP_20160101_22_25_36_Pro_LI.jpg";
        }
    }
}
