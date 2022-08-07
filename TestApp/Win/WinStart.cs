using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using TestApp.Models;
using TestApp.ViewModels;
using TestApp.Views;

namespace TestApp.Win
{
    public class WinStart
    {
        [STAThread]
        static void Main()
        {
            ViewModelMyWindow viewmodel = new ViewModelMyWindow(new WinEXE());
        }
    }
}
