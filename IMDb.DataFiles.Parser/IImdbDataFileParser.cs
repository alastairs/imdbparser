using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Parser.Factories
{
    public interface IImdbDataFileParser<T>
    {
        IEnumerable<T> Parse(string dataFileLocation);
    }
}
