using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IMDb.DataFiles.Parser.Factories
{
    public interface IImdbDataFileParser<T>
    {
        IEnumerable<T> Parse(Stream dataFileStream);
    }
}
