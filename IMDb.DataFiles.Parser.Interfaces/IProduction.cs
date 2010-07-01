using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Parser.Interfaces
{
    public interface IProduction
    {
        string Title { get; set; }
        int Year { get; set; }
    }
}
