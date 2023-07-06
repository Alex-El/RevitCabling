using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Models
{
    internal class XYZc : XYZ
    {
        public bool IsOnTray { get; set; } = false;
        public XYZc(XYZ p) : base(p.X, p.Y, p.Z) { }
    }
}
