﻿using CommunityToolkit.Mvvm.ComponentModel;
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
using OpenCVSharpDemo.Helper;

namespace OpenCVSharpDemo.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        //[ObservableProperty]
        string _filePath = "";
        Mat _img;
        Mat _imgWorking;

        [ObservableProperty]
        Boolean _isEnabled = false;

        [ObservableProperty]
        string _errorMessage = "";

        //[ObservableProperty]
        //[AlsoNotifyCanExecuteFor(nameof(BlurCommand))]
        int _blurValue = 1;
        int _sharpenValue = 1;
        int _cannyThresholdValue = 1;
        int _harrisThresholdValue = 200;

        public string FilePath
        {
            get => _filePath;
            set
            {
                try
                {
                    _filePath = value;
                    if (_filePath.EndsWith(".png") || _filePath.EndsWith(".bmp") || _filePath.EndsWith(".jpg") || _filePath.EndsWith(".jpeg") || _filePath.EndsWith(".gif"))
                    {
                        LoadFile();
                    }
                    else if (_filePath.Length > 0)
                    {
                        throw new FileFormatException();
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                catch (Exception e)
                {
                    _errorMessage = e.Message;
                    OnPropertyChanged("ErrorMessage");
                }
                
            }
        }

        public int BlurValue
        {
            get => _blurValue;
            set
            {
                _blurValue = value;
                Blur();
                OnPropertyChanged(nameof(BlurValue));
            }
        }

        public int SharpenValue
        {
            get => _sharpenValue;
            set
            {
                _sharpenValue = value;
                Sharpen();
                OnPropertyChanged(nameof(SharpenValue));
            }
        }

        public int CannyThresholdValue
        {
            get => _cannyThresholdValue;
            set
            {
                _cannyThresholdValue = value;
                ApplyCannyEdgeDetection();
                OnPropertyChanged(nameof(CannyThresholdValue));
            }
        }

        public int HarrisThresholdValue
        {
            get => _harrisThresholdValue;
            set
            {
                _harrisThresholdValue = value;
                ApplyHarrisCornerDetecton();
                OnPropertyChanged(nameof(HarrisThresholdValue));
            }
        }

        public ImageSource Img 
        { 
            get 
            {
                if (_img is not null && _img.Width > 0 && _img.Height > 0)
                    return ViewModelHelper.ConvertBitmapToImageSource(_img.ToBitmap());
                    //return ConvertBitmapToImageSource(_img.ToBitmap());
                return null;
            } 
        }

        public ImageSource ImgWorking
        {
            get
            {
                if (_imgWorking is not null && _imgWorking.Width > 0 && _imgWorking.Height > 0)
                    return ViewModelHelper.ConvertBitmapToImageSource(_imgWorking.ToBitmap());
                //return ConvertBitmapToImageSource(_imgWorking.ToBitmap());
                return null;
            }
        }

        void LoadFile()
        {
            _img = Cv2.ImRead(_filePath);
            _imgWorking = Cv2.ImRead(_filePath);
            OnPropertyChanged("Img");
            OnPropertyChanged("ImgWorking");
            _isEnabled = true;
            OnPropertyChanged("IsEnabled");
        }

        //[ICommand]
        //void OpenFile()
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    dialog.DefaultExt = ".png";
        //    //dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

        //    bool? result = dialog.ShowDialog();
        //    if (result != null && result == true)
        //    {
        //        _fileName = dialog.FileName;
        //        //_img = new Mat(_fileName);
        //        _img = Cv2.ImRead(_fileName);
        //        _imgWorking = Cv2.ImRead(_fileName);
        //        OnPropertyChanged("Img");
        //        OnPropertyChanged("ImgWorking");
        //        _isEnabled = true;
        //        OnPropertyChanged("IsEnabled");
        //    }
        //}

        [ICommand]
        void DisplayAbout()
        {
            About about = new About();
            about.Show();
        }

        [ICommand]
        void ConvertToGrayscale()
        {
            Cv2.CvtColor(_img, _imgWorking, ColorConversionCodes.BGR2GRAY);
            OnPropertyChanged("ImgWorking");
        }

        [ICommand]
        void ResetWorkingImage()
        {
            _imgWorking = _img;
            OnPropertyChanged("ImgWorking");
        }

        void ApplyCannyEdgeDetection()
        {
            Cv2.CvtColor(_img, _imgWorking, ColorConversionCodes.BGR2GRAY);
            Cv2.Canny(_imgWorking, _imgWorking, _cannyThresholdValue, _cannyThresholdValue * 3);
            OnPropertyChanged("ImgWorking");
        }

        void Blur()
        {
            // Blur value needs to be odd.
            if (_blurValue % 2 > 0)
            {
                Cv2.GaussianBlur(_img, _imgWorking, new OpenCvSharp.Size(_blurValue, _blurValue), 0);
                OnPropertyChanged("ImgWorking");
            }
        }

        void Sharpen()
        {
            // Sharpen value needs to be odd.
            if (_sharpenValue % 2 > 0)
            {
                Cv2.GaussianBlur(_img, _imgWorking, new OpenCvSharp.Size(_sharpenValue, _sharpenValue), 0);
                Cv2.AddWeighted(_img, 1.5, _imgWorking, -0.5, 0, _imgWorking);
                OnPropertyChanged("ImgWorking");
            }
        }

        void ApplyHarrisCornerDetecton()
        {
            int blockSize = 2;
            int apatureSize = 1;
            double k = 0.04d;

            Cv2.CvtColor(_img, _imgWorking, ColorConversionCodes.BGR2GRAY);
            Cv2.CornerHarris(_imgWorking, _imgWorking, blockSize, apatureSize, k);

            Mat norm = new Mat(); 
            Mat normScaled = new Mat();

            Cv2.Normalize(_imgWorking, norm, 0, 255, NormTypes.MinMax);
            Cv2.ConvertScaleAbs(norm, normScaled);

            // Apply circles at detected point
            for(int i = 0; i < norm.Rows; i++)
            {
                for(int j = 0; j < norm.Cols; j++)
                {
                    if(norm.At<float>(i,j) > _harrisThresholdValue)
                    {
                        Cv2.Circle(normScaled, new OpenCvSharp.Point(j, i),5,new OpenCvSharp.Scalar(0),2);
                    }
                }
            }

            _imgWorking = normScaled;
            OnPropertyChanged("ImgWorking");
        }

        //private ImageSource ConvertBitmapToImageSource(Bitmap imToConvert)
        //{
        //    Bitmap bmp = new Bitmap(imToConvert);
        //    MemoryStream ms = new MemoryStream();
        //    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

        //    BitmapImage image = new BitmapImage();
        //    image.BeginInit();
        //    ms.Seek(0, SeekOrigin.Begin);
        //    image.StreamSource = ms;
        //    image.EndInit();

        //    ImageSource sc = (ImageSource)image;

        //    return sc;
        //}
    }
}
