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
            @"^\# ""?(?<title>.*?)""? \((?<year>\d{4})\)( (\{(?<episodeTitle>[^\(].*[^\)])?\s*(?:\(\#(?<series>\d+)\.(?<episode>\d+)\))?\})| \((?<videoGame>VG)\))?$",
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
                if (match.Groups[RegexEpisodeTitleGroup].Length > 0 || match.Groups[RegexSeriesNumberGroup].Length > 0)
                {
                    Logger.DebugFormat("Parsing TelevisionShow {0}", productionDefinition);
                    production = ParseTelevisionShow(match);
                }
                else if (match.Groups[RegexVideoGameGroup].Length > 0)
                {
                    Logger.DebugFormat("Parsing VideoGame {0}", productionDefinition);
                    production = ParseVideoGame(match);
                }
                else
                {
                    Logger.DebugFormat("Parsing Movie {0}", productionDefinition);
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

            tvShow.SeriesNumber = ParseSeriesNumber(regexMatch);
            tvShow.EpisodeNumber = ParseEpisodeNumber(regexMatch);
            tvShow.EpisodeTitle = ParseEpisodeTitle(regexMatch);

            return tvShow;
        }

        private string ParseEpisodeTitle(Match regexMatch)
        {
            var episodeTitle = regexMatch.Groups[RegexEpisodeTitleGroup].Value;
            if (!string.IsNullOrEmpty(episodeTitle))
            {
                Logger.DebugFormat("Successfully parsed episode title {0}", episodeTitle);
                return episodeTitle.Trim();
            }

            Logger.DebugFormat("No episode title data to parse in production definition {0}", regexMatch);
            return string.Empty;
        }

        private int ParseEpisodeNumber(Match regexMatch)
        {
            var episodeNumber = regexMatch.Groups[RegexEpisodeNumberGroup].Value;
            if (!string.IsNullOrEmpty(episodeNumber))
            {
                try
                {
                    int parsedEpisodeNumber = int.Parse(episodeNumber);
                    Logger.DebugFormat("Successfully parsed episode number {0}", episodeNumber);
                    return parsedEpisodeNumber;
                }
                catch (FormatException)
                {
                    Logger.ErrorFormat("Failed to parse episode number {0}", episodeNumber);
                    return 0;
                }
            }
            
            Logger.Debug("No episode number to parse.");
            return 0;
        }

        private int ParseSeriesNumber(Match regexMatch)
        {
            var seriesNumber = regexMatch.Groups[RegexSeriesNumberGroup].Value;
            if (!string.IsNullOrEmpty(seriesNumber))
            {
                try
                {
                    int parsedSeriesNumber = int.Parse(seriesNumber);
                    Logger.DebugFormat("Successfully parsed series number {0}", seriesNumber);
                    return parsedSeriesNumber;
                }
                catch (FormatException)
                {
                    Logger.ErrorFormat("Failed to parse series number {0}", seriesNumber);
                    return 0;
                }
            }
            
            Logger.Debug("No series number to parse");
            return 0;
        }

        public string ParseTitle(Match regexMatch)
        {
            var title = regexMatch.Groups[RegexTitleGroup].Value.Trim();

            if (!string.IsNullOrEmpty(title))
            {
                Logger.DebugFormat("Successfully parsed title {0}", title);
                return title;
            }

            Logger.DebugFormat("No title data to parse in production definition {0}", regexMatch);
            return string.Empty; 
        }

        public int ParseYear(Match regexMatch)
        {
            var year = regexMatch.Groups[RegexYearGroup].Value;

            if (!string.IsNullOrEmpty(year))
            {
                try
                {
                    int parsedYear = int.Parse(year);
                    Logger.DebugFormat("Successfully parsed year {0}", year);
                    return parsedYear;
                }
                catch (FormatException)
                {
                    Logger.ErrorFormat("Failed to parse year {0}", year);
                    return 0;
                }
            }

            Logger.Debug("No year data to parse.");
            return 0;
        }
    }
}
