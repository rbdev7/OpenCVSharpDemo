using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCVSharpDemo.ViewModel;
//using OpenCVSharpDemo.View;
using CommunityToolkit.Mvvm;

namespace OpenCVSharpDemo.Tests
{
    public class AboutUnitTest
    {
        
        AboutViewModel aboutViewModel;
        //About about;
        public AboutUnitTest()
        { 
            aboutViewModel = new AboutViewModel();
            //aboutViewModel.CloseAction = new Action(Window.Close);
            //about = new About();
        }

        [Fact]
        public void PopulateAboutInfoTest()
        {
            Assert.True(aboutViewModel.AppInfo.Length > 0);
            Assert.Contains("Version", aboutViewModel.AppInfo);
            Assert.Contains("Description", aboutViewModel.AppInfo);
            Assert.Contains("Author", aboutViewModel.AppInfo);
        }

        //[Fact]
        //public void CloseWindowTest()
        //{
        //    Assert.True(aboutViewModel.OkCommand.CanExecute(null));
        //    aboutViewModel.OkCommand.Execute(null);
        //    Assert.False(aboutViewModel.OkCommand.CanExecute(null));
        //}
    }
}
