using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;

namespace RevitCabling.Helpers
{
    internal class ElectricalFixtureFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category != null)
            {
                if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_ElectricalFixtures)
                    return true;
            }

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
