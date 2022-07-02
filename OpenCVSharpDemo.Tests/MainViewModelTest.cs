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

            mainViewModel.FilePath = testImageFilePath;

            Assert.NotNull(updated);
            Assert.Equal(updated.PropertyName, nameof(mainViewModel.IsEnabled));
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
            Assert.Equal(updated.PropertyName, nameof(mainViewModel.ErrorMessage));
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
            Assert.Equal(updated.PropertyName, nameof(mainViewModel.ErrorMessage));
        }

        //[Fact]
        //public void BlurCommandTest()
        //{
        //    mainViewModel.
        //    mainViewModel.OpenFileCommand.Execute(null);
        //}

    }
}
