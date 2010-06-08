using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDb.DataFiles.Parser.Types
{
    public abstract class Production
    {
        public string Title { get; set; }
        public int Year { get; set; }

        protected const string RegexTitleGroup = "title";
        protected const string RegexYearGroup = "year";
        protected const string RegexEpisodeTitleGroup = "episodeTitle";
        protected const string RegexVideoGameGroup = "videoGame";

        private static readonly Regex MovieTitleLineRegex = new Regex(
            @"^\# ""?(?<title>.*)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>.*) \(\#(?<series>\d+)\.(?<episode>\d+)\)\})| \((?<videoGame>VG)\))?$");
        
        public static Production Parse(string production)
        {
            if (MovieTitleLineRegex.IsMatch(production))
            {
                var match = MovieTitleLineRegex.Match(production);
                if (match.Groups[RegexEpisodeTitleGroup].Length > 0)
                {
                    return TelevisionShow.Parse(production);
                }

                if (match.Groups[RegexVideoGameGroup].Length > 0)
                {
                    return VideoGame.Parse(production);
                }
            }

            return Movie.Parse(production);
        }
    }
}
