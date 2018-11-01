using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SneakyCat
{
    public class Extract : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Encrypt class.
        /// </summary>
        public Extract()
          : base("Hiss", "Hiss",
              "Extract data from an embedded image",
              "SneakyCat", "Basic")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Run", "R", "Run to embed", GH_ParamAccess.item);
            pManager.AddTextParameter("PathToSource", "I", "Path to image containing embedded data", GH_ParamAccess.item);
            pManager.AddTextParameter("Password", "P", "Password to use for decryption", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("EmbeddedData", "E", "Embedded data", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool on = false;
            if (!DA.GetData("Run", ref on)) { return; }
            if (!on) { return; }

            string sourcePath = "";
            string pwd = null;

            if (!DA.GetData("PathToSource", ref sourcePath)) { DA.SetData("EmbeddedData", "No source"); return; }
            if (!DA.GetData("Password", ref pwd)) { pwd = null; }

            byte[] b = Steganographer.Decode(sourcePath, pwd);
            string s = System.Text.Encoding.UTF8.GetString(b);
            DA.SetData("EmbeddedData", s);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources._out;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8aa8b3bf-dad0-468d-a11f-d62ee98019b7"); }
        }
    }
}