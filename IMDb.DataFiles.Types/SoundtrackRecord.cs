using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMDb.DataFiles.Parser.Interfaces;

namespace IMDb.DataFiles.Types
{
    public class SoundtrackRecord
    {
        public IProduction Production { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
