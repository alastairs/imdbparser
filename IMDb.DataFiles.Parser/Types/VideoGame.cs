using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Factories;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Parser.Types
{
    public class VideoGame : Production, IProductionParser
    {
        public IProduction Parse(string videoGameDefinition)
        {
            return base.Parse(videoGameDefinition) as VideoGame;
        }
    }
}
