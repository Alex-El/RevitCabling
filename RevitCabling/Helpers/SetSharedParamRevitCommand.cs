using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitCabling.Services;
using System;
using System.Linq;


namespace RevitCabling.Helpers
{
    static class SetSharedParamRevitCommand
    {
        public static Result Execute(UIApplication app)
        {
            DefinitionFile dfile = app.Application.OpenSharedParameterFile();

            if (dfile == null )
            {
                TaskDialog.Show("Error", "Shared parameter file not found");
                return Result.Failed;
            }

            if ( SetNewParameterToInstanceCableTray(app, dfile))
            {
                //TODO write to log: "Setup shares params"
                //TaskDialog.Show("Error", "OK");
            }
            else
            {
                //TODO write to log: "Shared params already exists"
                //TaskDialog.Show("Error", "Not OK");
            }

            return Result.Succeeded;
        }

         static bool SetNewParameterToInstanceCableTray(UIApplication app, DefinitionFile myDefinitionFile)
         {
            // create a new group in the shared parameters file
            DefinitionGroups shareParamGroups = myDefinitionFile.Groups;

            DefinitionGroup cablesGroup = shareParamGroups.Where(g => g.Name.Equals(Properties.Resources.CableParameterGroupName)).FirstOrDefault();
            Definition cables_difinition;

            if (cablesGroup == null )
            {
                cablesGroup = shareParamGroups.Create(Properties.Resources.CableParameterGroupName);
                //DefinitionGroup myGroup = myGroups.Create(Properties.Resources.CableParameterGroupName);
                // create an instance definition in definition group MyParameters
                ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions(Properties.Resources.CableParameterName, ParameterType.Text);
                // Don't let the user modify the value, only the API
                option.UserModifiable = false;
                // Set tooltip
                //option.Description = "Wall product date";
                cables_difinition = cablesGroup.Definitions.Create(option);
            }
            else
            {
                cables_difinition = cablesGroup.Definitions.FirstOrDefault();
            }

            // Get the BingdingMap of current document.
            BindingMap bindingMap = app.ActiveUIDocument.Document.ParameterBindings;

            if (bindingMap.Contains(cables_difinition))
            {
                return false;
            }

            // create a category set and insert category of cable tray to it
            CategorySet myCategories = app.Application.Create.NewCategorySet();

            // use BuiltInCategory to get category of cable tray
            Category myCategory = app.ActiveUIDocument.Document.Settings.Categories.get_Item(BuiltInCategory.OST_CableTray);
            myCategories.Insert(myCategory);

            //Create an instance of InstanceBinding
            InstanceBinding instanceBinding = app.Application.Create.NewInstanceBinding(myCategories);

            // Bind the definitions to the document
            bool instanceBindOK = false;

            using (Transaction tr = new Transaction(app.ActiveUIDocument.Document, "Binding shared param"))
            {
                try
                {
                    tr.Start();

                    instanceBindOK = bindingMap.Insert(cables_difinition, instanceBinding, BuiltInParameterGroup.PG_TEXT);

                    tr.Commit();
                }
                catch(Exception ex)
                {
                    tr.RollBack();
                    throw ex;
                }

            }

            return instanceBindOK;
         }
    }
}
