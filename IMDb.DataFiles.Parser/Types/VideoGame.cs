using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMDb.DataFiles.Parser.Types
{
    public class VideoGame : Production
    {
        public override string Title
        {
            get;
            set;
        }

        public override int Year
        {
            get;
            set;
        }

        public static VideoGame Parse(Match production)
        {
            int year = int.Parse(production.Groups[RegexYearGroup].Value);

            return new VideoGame
            {
                Title = production.Groups[RegexTitleGroup].Value,
                Year = year
            };
        }
    }
}
