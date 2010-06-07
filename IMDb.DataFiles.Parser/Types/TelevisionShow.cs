using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Parser.Types
{
    public class TelevisionShow : Production
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

        public string EpisodeTitle
        {
            get;
            set;
        }

        public int SeriesNumber
        {
            get;
            set;
        }

        public int EpisodeNumber
        {
            get;
            set;
        }
    }
}
