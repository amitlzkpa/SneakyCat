using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SneakyCat
{
    public class Embed : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Embed class.
        /// </summary>
        public Embed()
          : base("Purr", "Purr",
              "Embed data into an image",
              "SneakyCat", "Basic")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Run", "R", "Run to embed", GH_ParamAccess.item);
            pManager.AddTextParameter("DataToEmbed", "D", "Data to embed", GH_ParamAccess.item);
            pManager.AddTextParameter("PathToTargetFolder", "Tf", "Folder where embedded image will be saved", GH_ParamAccess.item);
            pManager.AddTextParameter("PathToSource", "I", "Path to image data will be embedded into", GH_ParamAccess.item);
            pManager.AddTextParameter("Password", "P", "Password to use for decryption", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Message", "Message", "Message", GH_ParamAccess.item);
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


            string dataToEmbed = "";
            string targetPath = "";
            string sourcePath = "";
            string pwd = null;

            if (!DA.GetData("DataToEmbed", ref dataToEmbed)) { DA.SetData("Message", "No data"); return; }
            if (!DA.GetData("PathToTargetFolder", ref targetPath)) { DA.SetData("Message", "No target"); return; }
            if (!DA.GetData("PathToSource", ref sourcePath)) { DA.SetData("Message", "No source"); return; }
            if (!DA.GetData("Password", ref pwd)) { pwd = null; }

            Steganographer.Encode(dataToEmbed, sourcePath, targetPath, pwd);
            DA.SetData("Message", "The cats have been loaded");
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
                return Properties.Resources._in;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("bdc4706b-c1de-4b37-a494-fbcd7347a8d7"); }
        }
    }
}