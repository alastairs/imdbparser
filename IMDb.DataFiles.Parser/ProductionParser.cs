using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMDb.DataFiles.Parser.Interfaces;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Types;
using log4net;

namespace IMDb.DataFiles.Parser
{
    public class ProductionParser
    {
        private const string RegexTitleGroup = "title";
        private const string RegexYearGroup = "year";
        private const string RegexEpisodeTitleGroup = "episodeTitle";
        private const string RegexVideoGameGroup = "videoGame";
        
        private const string RegexSeriesNumberGroup = "series";
        private const string RegexEpisodeNumberGroup = "episode";

        private static readonly Regex ProductionTitleLineRegex = new Regex(
            @"^\# ""?(?<title>.*?)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>.*) \(\#(?<series>\d+)\.(?<episode>\d+)\)\})| \((?<videoGame>VG)\))?$",
            RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Multiline);

        protected ILog Logger { get; private set; }

        public ProductionParser(ILog logger)
        {
            Logger = logger;
        }
        
        public IProduction Parse(string productionDefinition)
        {
            if (ProductionTitleLineRegex.IsMatch(productionDefinition))
            {
                IProduction production;
                var match = ProductionTitleLineRegex.Match(productionDefinition);
                if (match.Groups[RegexEpisodeTitleGroup].Length > 0)
                {
                    production = ParseTelevisionShow(match);
                }
                else if (match.Groups[RegexVideoGameGroup].Length > 0)
                {
                    production = ParseVideoGame(match);
                }
                else
                {
                    production = ParseMovie(match);
                }

                return production;
            }

            return null;
        }

        private IProduction ParseMovie(Match regexMatch)
        {
            var movie = new Movie();
            movie.Title = ParseTitle(regexMatch);
            movie.Year = ParseYear(regexMatch);
            return movie;
        }

        private IProduction ParseVideoGame(Match regexMatch)
        {
            var videoGame = new VideoGame();
            videoGame.Title = ParseTitle(regexMatch);
            videoGame.Year = ParseYear(regexMatch);
            return videoGame;
        }

        private IProduction ParseTelevisionShow(Match regexMatch)
        {
            var tvShow = new TelevisionShow();

            tvShow.Title = ParseTitle(regexMatch);
            tvShow.Year = ParseYear(regexMatch);

            int seriesNumber = int.Parse(regexMatch.Groups[RegexSeriesNumberGroup].Value);
            int episodeNumber = int.Parse(regexMatch.Groups[RegexEpisodeNumberGroup].Value);

            tvShow.EpisodeTitle = regexMatch.Groups[RegexEpisodeTitleGroup].Value;
            tvShow.SeriesNumber = seriesNumber;
            tvShow.EpisodeNumber = episodeNumber;

            return tvShow;
        }

        public string ParseTitle(Match regexMatch)
        {
            return regexMatch.Groups[RegexTitleGroup].Value;
        }

        public int ParseYear(Match regexMatch)
        {
            return int.Parse(regexMatch.Groups[RegexYearGroup].Value);
        }
    }
}
