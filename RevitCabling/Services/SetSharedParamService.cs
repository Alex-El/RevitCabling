using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCabling.Services
{
    internal class SetSharedParamService : ExternalEventCommand
    {
        public SetSharedParamService(string name) : base(name) { }

        public override void Execute()
        {
            Autodesk.Revit.DB.Document doc = UIApplication.ActiveUIDocument.Document;

        }
    }
}
