using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;
using IMDb.DataFiles.Parser.Factories;

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

        public override void Load(string productionDefinition)
        {
            base.Load(productionDefinition);

            var match = MovieTitleLineRegex.Match(productionDefinition);

            int seriesNumber = int.Parse(match.Groups[RegexSeriesNumberGroup].Value);
            int episodeNumber = int.Parse(match.Groups[RegexEpisodeNumberGroup].Value);

            this.EpisodeTitle = match.Groups[RegexEpisodeTitleGroup].Value;
            this.SeriesNumber = seriesNumber;
            this.EpisodeNumber = episodeNumber;
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
