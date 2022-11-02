using OpenCvSharp;
using OpenCVSharpDemo.ViewModel;
using System.ComponentModel;

namespace OpenCVSharpDemo.Tests
{
    public class MainViewModelTest
    {
        MainViewModel mainViewModel;
        Mat testMat;
        string testImageFilePath = "./Test Image/Dog.jpg";

        public MainViewModelTest()
        {
            mainViewModel = new MainViewModel();
            testMat = new Mat(3, 3, MatType.CV_8UC1);
        }

        [Fact]
        public void ViewModelCommandsInitalisedTest()
        {
            Assert.NotNull(mainViewModel.ConvertToGrayscaleCommand);
            Assert.NotNull(mainViewModel.DisplayAboutCommand);
            Assert.NotNull(mainViewModel.ResetWorkingImageCommand);
        }

        [Fact]
        public void IsEnabledInitalisedToFalse()
        {
            Assert.False(mainViewModel.IsEnabled);
        }

        [Fact]
        public void ImgIsNullTest()
        {
            Assert.Null(mainViewModel.Img);
        }

        [Fact]
        public void ImgWorkingIsNullTest()
        {
            Assert.Null(mainViewModel.ImgWorking);
        }

        [Fact]
        public void InitialBlurValueTest()
        {
            Assert.Equal(1, mainViewModel.BlurValue);
        }

        [Fact]
        public void InitialSharpenValueTest()
        {
            Assert.Equal(1, mainViewModel.SharpenValue);
        }

        [Fact]
        public void InitialCannyThresholdValueTest()
        {
            Assert.Equal(1, mainViewModel.CannyThresholdValue);
        }

        [Fact]
        public void InitialHarrisThresholdValueTest()
        {
            Assert.Equal(200, mainViewModel.HarrisThresholdValue);
        }

        [Fact]
        public void InitialFilePathTest()
        {
            Assert.Equal("", mainViewModel.FilePath);
        }

        [Fact]
        public void LoadFileEnableTest()
        {
            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            Assert.Equal("", mainViewModel.FilePath);

            mainViewModel.FilePath = testImageFilePath;

            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.IsEnabled), updated.PropertyName);
            Assert.Equal(testImageFilePath, mainViewModel.FilePath);
        }

        [Fact]
        public void LoadFileEmptyPathTest()
        {
            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            mainViewModel.FilePath = "";

            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.ErrorMessage), updated.PropertyName);
        }

        [Fact]
        public void LoadFileIncorrectPathTest()
        {
            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            mainViewModel.FilePath = "./not_a_path";
            
            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.ErrorMessage), updated.PropertyName);
        }

        [Fact]
        public void BlurValueChangedTest()
        {
            // Required to perform Harris corner detection.
            mainViewModel.FilePath = testImageFilePath;

            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            mainViewModel.BlurValue = 2;

            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.BlurValue), updated.PropertyName);
        }

        [Fact]
        public void SharpenValueChangedTest()
        {
            // Required to perform Harris corner detection.
            mainViewModel.FilePath = testImageFilePath;

            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            mainViewModel.SharpenValue = 2;

            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.SharpenValue), updated.PropertyName);
        }

        [Fact]
        public void CannyThresholdValueChangedTest()
        {
            // Required to perform Harris corner detection.
            mainViewModel.FilePath = testImageFilePath;

            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            mainViewModel.CannyThresholdValue = 2;

            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.CannyThresholdValue), updated.PropertyName);
        }

        [Fact]
        public void HarrisThresholValueChangedTest()
        {
            // Required to perform Harris corner detection.
            mainViewModel.FilePath = testImageFilePath;

            PropertyChangedEventArgs updated = null;
            mainViewModel.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            mainViewModel.HarrisThresholdValue = 2;

            Assert.NotNull(updated);
            Assert.Equal(nameof(mainViewModel.HarrisThresholdValue), updated.PropertyName);
        }

    }
}
