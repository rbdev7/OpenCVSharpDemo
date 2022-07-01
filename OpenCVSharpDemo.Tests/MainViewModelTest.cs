using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCVSharpDemo.ViewModel;
using CommunityToolkit.Mvvm;
using OpenCvSharp;

namespace OpenCVSharpDemo.Tests
{
    public class MainViewModelTest
    {
        MainViewModel mainViewModel;
        Mat testMat;

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

        //[Fact]
        //public void BlurCommandTest()
        //{
        //    mainViewModel.OpenFileCommand.Execute(null);
        //}

    }
}
