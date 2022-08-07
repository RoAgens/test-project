using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public interface IRVEXE
    {
        string TitleValue { get; set; }
        string LabelContent { get; set; }
        string TextBoxValue { get; set; }
        string ButtonContent { get; set; }
        Action ToDo();
    }
}
