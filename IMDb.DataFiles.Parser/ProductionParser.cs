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
                    Logger.DebugFormat("Creating a new TelevisionShow for {0}", productionDefinition);
                    production = ParseTelevisionShow(match);
                }
                else if (match.Groups[RegexVideoGameGroup].Length > 0)
                {
                    Logger.DebugFormat("Creating a new VideoGame for {0}", productionDefinition);
                    production = ParseVideoGame(match);
                }
                else
                {
                    Logger.DebugFormat("Creating a new Movie for {0}", productionDefinition);
                    production = ParseMovie(match);
                }

                return production;
            }

            Logger.ErrorFormat("Failed to parse the production definition {0}", productionDefinition);
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

            var seriesNumber = regexMatch.Groups[RegexSeriesNumberGroup].Value;
            try
            {
                Logger.DebugFormat("Successfully parsed series number {0}", seriesNumber);
                tvShow.SeriesNumber = int.Parse(seriesNumber);
            }
            catch (FormatException)
            {
                Logger.ErrorFormat("Failed to parse series number {0}", seriesNumber);
                tvShow.SeriesNumber = 0;
            }

            var episodeNumber = regexMatch.Groups[RegexEpisodeNumberGroup].Value;
            try
            {
                Logger.DebugFormat("Successfully parsed episode number {0}", episodeNumber);
                tvShow.EpisodeNumber = int.Parse(episodeNumber);
            }
            catch (FormatException)
            {
                Logger.ErrorFormat("Failed to parse episode number {0}", episodeNumber);
                tvShow.EpisodeNumber = 0;
            }

            var episodeTitle = regexMatch.Groups[RegexEpisodeTitleGroup].Value;
            if (string.IsNullOrEmpty(episodeTitle))
            {
                Logger.ErrorFormat("Failed to parse episode title {0}", regexMatch);
                tvShow.EpisodeTitle = string.Empty;
            }
            else
            {
                Logger.DebugFormat("Successfully parsed episode title {0}", episodeTitle);
                tvShow.EpisodeTitle = episodeTitle;
            }

            return tvShow;
        }

        public string ParseTitle(Match regexMatch)
        {
            var title = regexMatch.Groups[RegexTitleGroup].Value;

            if (string.IsNullOrEmpty(title))
            {
                Logger.ErrorFormat("Failed to parse title {0}", regexMatch);
                return string.Empty;
            }

            Logger.DebugFormat("Successfully parsed title {0}", title);
            return title;
        }

        public int ParseYear(Match regexMatch)
        {
            var year = regexMatch.Groups[RegexYearGroup].Value;

            try
            {
                Logger.DebugFormat("Successfully parsed year {0}", year);
                return int.Parse(year);
            }
            catch (FormatException)
            {
                Logger.ErrorFormat("Failed to parse year {0}", year);
                return 0;
            }
        }
    }
}
