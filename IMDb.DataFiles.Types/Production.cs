using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Types
{
    public class Production : IProduction
    {
        public string Title { get; set; }
        public int Year { get; set; }

        public override bool Equals(object obj)
        {
            Production other = obj as Production;
            if (other != null)
            {
                return (this.Title == other.Title) && (this.Year == other.Year);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Title, this.Year);
        }
    }
}
