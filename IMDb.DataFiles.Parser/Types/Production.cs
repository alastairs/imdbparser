using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDb.DataFiles.Parser.Types
{
    public abstract class Production
    {
        public abstract string Title { get; set; }
        public abstract int Year { get; set; }

        protected const string RegexTitleGroup = "title";
        protected const string RegexYearGroup = "year";
        protected const string RegexEpisodeTitleGroup = "episodeTitle";
        protected const string RegexVideoGameGroup = "videoGame";

        public static Production Parse(Match production)
        {
            if (production.Groups[RegexEpisodeTitleGroup].Length > 0)
            {
                return TelevisionShow.Parse(production);                
            }

            if (production.Groups[RegexVideoGameGroup].Length > 0)
            {
                return VideoGame.Parse(production);
            }

            return Movie.Parse(production);
        }
    }
}
