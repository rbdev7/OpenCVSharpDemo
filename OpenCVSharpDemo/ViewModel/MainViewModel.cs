using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenCVSharpDemo.View;

namespace OpenCVSharpDemo.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        //[ObservableProperty]
        string _fileName = "";
        Mat _img;

        public ImageSource Img 
        { 
            get 
            {
                if (_img is not null && _img.Width > 0 && _img.Height > 0)
                    return ConvertBitmapToImageSource(_img.ToBitmap());
                return null;
            } 
        }

        [ICommand]
        void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            bool? result = dialog.ShowDialog();
            if (result != null && result == true)
            {
                _fileName = dialog.FileName;
                //_img = new Mat(_fileName);
                _img = Cv2.ImRead(_fileName);
                OnPropertyChanged("Img");
            }
        }

        [ICommand]
        void DisplayAbout()
        {
            About about = new About();
            about.Show();
        }

        private ImageSource ConvertBitmapToImageSource(Bitmap imToConvert)
        {
            Bitmap bmp = new Bitmap(imToConvert);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            ImageSource sc = (ImageSource)image;

            return sc;
        }
    }
}
