using System.ComponentModel;
using System.Linq;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLargeImage.Droid.SourceCode.Controls;

[assembly: ExportRenderer(typeof(XLargeImage.SourceCode.Controls.XLargeImage), typeof(XLargeImageRenderer))]
namespace XLargeImage.Droid.SourceCode.Controls
{
    public class XLargeImageRenderer : ImageRenderer
    {
        private bool _isDecoded;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var largeImage = (XLargeImage.SourceCode.Controls.XLargeImage)Element;

            if ((!(Element.Width > 0) || !(Element.Height > 0) || _isDecoded) &&
                (e.PropertyName != "ImageSource" || largeImage.ImageSource == null)) return;
            var options = new BitmapFactory.Options {InJustDecodeBounds = true};

            //Get the resource id for the image
            var field = typeof(Resource.Drawable).GetField(largeImage.ImageSource.Split('.').First());
            if(field == null) return;
            var value = (int)field.GetRawConstantValue();

            BitmapFactory.DecodeResource(Context.Resources, value, options);

            //The with and height of the elements (XLargeImage) will be used to decode the image
            var width = (int)Element.Width;
            var height = (int)Element.Height;
            options.InSampleSize = CalculateInSampleSize(options, width, height);

            options.InJustDecodeBounds = false;
            var bitmap = BitmapFactory.DecodeResource(Context.Resources, value, options);

            //Set the bitmap to the native control
            Control.SetImageBitmap(bitmap);

            _isDecoded = true;
        }
        public int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            var inSampleSize = 1D;

            if (!(height > reqHeight) && !(width > reqWidth)) return (int) inSampleSize;
            var halfHeight = (int)(height / 2);
            var halfWidth = (int)(width / 2);

            // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
            while ((halfHeight / inSampleSize > reqHeight) && (halfWidth / inSampleSize > reqWidth))
            {
                inSampleSize *= 2;
            }

            return (int)inSampleSize;
        }
    }
}