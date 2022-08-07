using System;
using System.IO;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;
using TestApp.Views;
using TestApp.Services;
using TestApp.ViewModels;
using System.Windows.Data;
using TestApp.Models;

namespace TestApp.IExternalCommands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class RevitTestApp : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            RevitManager.CommandData = commandData;
            UIDocument _uidoc = commandData.Application.ActiveUIDocument;
            Document _doc = RevitManager.Document;

            try
            {
                ViewModelMyWindow viewmodel = new ViewModelMyWindow(new Revit(_doc));
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
