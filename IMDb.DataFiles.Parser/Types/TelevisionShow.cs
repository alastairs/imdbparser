using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;
using IMDb.DataFiles.Parser.Factories;

namespace IMDb.DataFiles.Parser.Types
{
    public class TelevisionShow : Production, IProductionParser
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

        public IProduction Parse(string productionDefinition)
        {
            var production = base.Parse(productionDefinition) as TelevisionShow;
            var match = MovieTitleLineRegex.Match(productionDefinition);
            
            int seriesNumber = int.Parse(match.Groups[RegexSeriesNumberGroup].Value);
            int episodeNumber = int.Parse(match.Groups[RegexEpisodeNumberGroup].Value);

            production.EpisodeTitle = match.Groups[RegexEpisodeTitleGroup].Value;
            production.SeriesNumber = seriesNumber;
            production.EpisodeNumber = episodeNumber;

            return production;
        }
    }
}
