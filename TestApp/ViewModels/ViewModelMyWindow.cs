using TestApp.Models;
using System;
using TestApp.Views;
using System.Windows.Data;

namespace TestApp.ViewModels
{
    public class ViewModelMyWindow : NotifyProperty
    {
        IRVEXE _difClass;
        MainWindow _mainWindow;

        public ViewModelMyWindow()
        {
            _mainWindow = new MainWindow();
            _mainWindow.DataContext = this;
        }

        private void ClW()
        {
            _mainWindow.Close();
        }

        public ViewModelMyWindow(IRVEXE difClass) : this()
        {
            _difClass = difClass;
            _mainWindow.Title = _difClass.TitleValue;
            _mainWindow.myButton.Content = _difClass.ButtonContent;
            ButtonAction = _difClass.ToDo();
            if (_difClass is Revit)
            {
                BindingOperations.ClearAllBindings(_mainWindow.myLabel);
                _mainWindow.myLabel.Content = _difClass.LabelContent;
            }
            _mainWindow.ShowDialog();
        }

        public Action MakeBinding { get; set; }

        public Action ButtonAction { get; set; }

        private string _TextBoxText { get; set; }

        private string _LabelText { get; set; }

        /// <summary>
        /// заполнение техстбокса
        /// </summary>
        public string TextBoxText
        {
            get
            {
                if (_TextBoxText == null)
                {
                    _TextBoxText = _difClass.TextBoxValue;
                }

                return _TextBoxText;
            }

            set
            {
                _TextBoxText = value;
                _difClass.TextBoxValue = _TextBoxText;
                 OnPropertyChanged();
            }
        }

        private RelayCommand _Btn;
        /// <summary>
        /// Команда по нажатию на Button 
        /// </summary>
        public RelayCommand Btn
        {
            get
            {
                return _Btn ??
                    (_Btn = new RelayCommand(obj => { ClW(); ButtonAction(); }));
            }
        }
    }
}
