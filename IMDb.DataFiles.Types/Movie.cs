using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Types
{
    public class Movie : Production
    {
        public override void Load(string productionDefinition)
        {
            base.Load(productionDefinition);
        }
    }
}
