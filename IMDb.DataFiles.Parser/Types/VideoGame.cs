using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Parser.Types
{
    public class VideoGame : Production
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
