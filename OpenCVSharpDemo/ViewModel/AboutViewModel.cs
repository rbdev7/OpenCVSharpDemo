using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenCVSharpDemo.View;

namespace OpenCVSharpDemo.ViewModel
{
    public partial class AboutViewModel : ObservableObject
    {
        [ObservableProperty]
        string _appInfo = "";

        // Action to allow the ViewModel to close a window.
        public Action CloseAction { get; set; }

        public AboutViewModel()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Name: {Assembly.GetExecutingAssembly().GetName().Name}\n");
            sb.Append($"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
            
            _appInfo = sb.ToString();
        }

        [ICommand]
        void Ok()
        {
            CloseAction();
        }
    }
}
