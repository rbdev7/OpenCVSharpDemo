using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;

namespace OpenCVSharpDemo.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        //[ObservableProperty]
        string _fileName = "";

        [ICommand]
        void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            bool? result = dialog.ShowDialog();
            if (result != null && result == true)
            {
                _fileName = dialog.FileName;
            }
        }
    }
}
