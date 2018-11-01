using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace SneakyCat
{
    public class SneakyCatInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "SneakyCat";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Snugging data into your renders.";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("560fbcb8-4ad8-44a6-8ff8-b7d069fd1d3c");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Amit Nambiar, Joseph Wagner, Leland Curtis, Vina Soliman, Zach S";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
