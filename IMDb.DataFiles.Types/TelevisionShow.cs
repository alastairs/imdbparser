using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Types
{
    public class TelevisionShow : Production
    {
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

        public override bool Equals(object obj)
        {
            TelevisionShow other = obj as TelevisionShow;
            if (other != null)
            {
                return base.Equals(other) && 
                       this.SeriesNumber == other.SeriesNumber && 
                       this.EpisodeNumber == other.EpisodeNumber && 
                       this.EpisodeTitle == other.EpisodeTitle;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
