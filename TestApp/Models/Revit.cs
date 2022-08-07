using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;

namespace TestApp.Models
{
    public class Revit : IRVEXE
    {
        Document _doc;

        public Revit(Document doc)
        {
            _doc = doc;
            LabelContent = $"Find {FindAllMeshInView} triangles";
            TextBoxValue = $"{DocName}";
        }

        public string TitleValue { get; set; } = "RevitApp by RoAgens";
        public string LabelContent { get; set; }
        public string TextBoxValue { get; set; }
        public string ButtonContent { get; set; } = "Сохранить Revit как...";

        /// <summary>
        /// Сохранение Revit с новым именем
        /// </summary>
        private void SaveAs()
        {
            try
            {
                _doc.SaveAs(Path.Combine(Path.GetDirectoryName(_doc.PathName), $"{TextBoxValue}.rvt"));
            }
            catch
            {

            }
        }

        Action IRVEXE.ToDo()
        {
            return new Action(SaveAs);
        }

        private string DocName => Path.GetFileNameWithoutExtension(_doc.PathName);

        /// <summary>
        /// Поиск всех треугольников на виде
        /// </summary>
        /// <returns></returns>
        private string FindAllMeshInView
        {
            get
            {
                View view = _doc.ActiveView;

                #region опции графики
                Options geomOptions = new Options();
                geomOptions.View = view;
                geomOptions.IncludeNonVisibleObjects = true;
                geomOptions.ComputeReferences = true;
                #endregion

                // кол-во треугольников
                int trCount = 0;

                var ttt = GetAllElements();

                foreach (Element el in GetAllElements())
                {
                    DirectShape selobjectDirectShape = el as DirectShape;

                    GeometryElement wallGeom = el.get_Geometry(geomOptions);

                    foreach (GeometryObject ge in wallGeom)
                    {
                        Mesh mesh = ge as Mesh;
                        trCount += mesh.NumTriangles;
                    }
                }

                return trCount.ToString();
            }
        }

        /// <summary>
        /// Получение все элементов с Mesh
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Element> GetAllElements()
        {
            return new FilteredElementCollector(_doc).WhereElementIsNotElementType().OfType<DirectShape>();
        }
    }
}
