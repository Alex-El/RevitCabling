using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling
{
    internal static class Host
    {
        public static Settings Settings { get; private set; } = null;

        public static void Initialize(UIControlledApplication application) 
        {  
            Settings = new Settings(application);
        }
    }
}
