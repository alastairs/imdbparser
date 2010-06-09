using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;
using IMDb.DataFiles.Parser.Factories;

namespace IMDb.DataFiles.Parser.Types
{
    public class Production : IProduction
    {
        public string Title { get; set; }
        public int Year { get; set; }

        protected const string RegexTitleGroup = "title";
        protected const string RegexYearGroup = "year";
        protected const string RegexEpisodeTitleGroup = "episodeTitle";
        protected const string RegexVideoGameGroup = "videoGame";

        protected static readonly Regex MovieTitleLineRegex = new Regex(
            @"^\# ""?(?<title>.*)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>.*) \(\#(?<series>\d+)\.(?<episode>\d+)\)\})| \((?<videoGame>VG)\))?$");
        
        public static IProduction ParseProduction(string production)
        {
            if (MovieTitleLineRegex.IsMatch(production))
            {
                IProductionParser parser;
                var match = MovieTitleLineRegex.Match(production);
                if (match.Groups[RegexEpisodeTitleGroup].Length > 0)
                {
                    parser = new TelevisionShow();
                }
                else if (match.Groups[RegexVideoGameGroup].Length > 0)
                {
                    parser = new VideoGame();
                }
                else
                {
                    parser = new Movie();
                }

                return parser.Parse(production);
            }

            return null;
        }

        public virtual IProduction Parse(string productionDefinition)
        {
            var match = MovieTitleLineRegex.Match(productionDefinition);
            int year = int.Parse(match.Groups[RegexYearGroup].Value);

            return new Production
            {
                Title = match.Groups[RegexTitleGroup].Value,
                Year = year
            };
        }
    }
}
