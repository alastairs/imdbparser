using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDb.DataFiles.Parser.Types
{
    public class TelevisionShow : Production
    {
        private const string RegexSeriesNumberGroup = "series";
        private const string RegexEpisodeNumberGroup = "episode";
        
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

        public static Production Parse(Match production)
        {
            int year = int.Parse(production.Groups[RegexYearGroup].Value);
            int episodeNumber = int.Parse(production.Groups[RegexEpisodeNumberGroup].Value);

            return new TelevisionShow
            {
                Title = production.Groups[RegexTitleGroup].Value,
                Year = year,
                EpisodeTitle = production.Groups[RegexEpisodeTitleGroup].Value,
                EpisodeNumber = episodeNumber
            };
        }
    }
}
