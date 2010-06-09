using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Parser.Factories
{
    public interface IProductionParser
    {
        IProduction Parse(string productionDefinition);
    }
}
