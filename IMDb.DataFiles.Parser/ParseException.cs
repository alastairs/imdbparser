using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Types
{
    public class ParseException : Exception
    {
        public string ParseFailure { get; private set; }

        public ParseException(string parseFailure) {
            ParseFailure = parseFailure;
        }
    }
}
