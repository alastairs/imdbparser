using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Parser.Types
{
    public class Movie : Production
    {
        public override string Title
        {
            get;
            set;
        }

        public override int Year
        {
            get;
            set;
        }
    }
}
