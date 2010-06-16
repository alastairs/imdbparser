using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Types
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
            @"^\# ""?(?<title>.*?)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>.*) \(\#(?<series>\d+)\.(?<episode>\d+)\)\})| \((?<videoGame>VG)\))?$",
            RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Multiline);
        
        public static IProduction Parse(string productionDefinition)
        {
            if (MovieTitleLineRegex.IsMatch(productionDefinition))
            {
                IProduction production;
                var match = MovieTitleLineRegex.Match(productionDefinition);
                if (match.Groups[RegexEpisodeTitleGroup].Length > 0)
                {
                    production = new TelevisionShow();
                }
                else if (match.Groups[RegexVideoGameGroup].Length > 0)
                {
                    production = new VideoGame();
                }
                else
                {
                    production = new Movie();
                }

                production.Load(productionDefinition);

                return production;
            }

            return null;
        }

        public virtual void Load(string productionDefinition)
        {
            var match = MovieTitleLineRegex.Match(productionDefinition);
            int year = int.Parse(match.Groups[RegexYearGroup].Value);

            this.Title = match.Groups[RegexTitleGroup].Value;
            this.Year = year;
        }

        public override bool Equals(object obj)
        {
            Production other = obj as Production;
            if (other != null)
            {
                return (this.Title == other.Title) && (this.Year == other.Year);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
