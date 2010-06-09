using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Factories;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Parser.Types
{
    public class Movie : Production, IProductionParser
    {
        public IProduction Parse(string productionDefinition)
        {
            return base.Parse(productionDefinition) as Movie;            
        }
    }
}
