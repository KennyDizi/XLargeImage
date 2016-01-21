using System;
using Xamarin.Forms;

namespace XLargeImage.SourceCode.Controls
{
    public class XLargeImage : Image
    {
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create<XLargeImage, string>(p => p.ImageSource, string.Empty, BindingMode.Default, null,
                OnImageSourceChanged);

        private static void OnImageSourceChanged(BindableObject bindable, string oldvalue, string newvalue)
        {
            if (Device.OS == TargetPlatform.Android) return;
            var image = (XLargeImage)bindable;

            var baseImage = (Image)bindable;
            baseImage.Source = image.ImageSource;
        }

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }        
    }
}
