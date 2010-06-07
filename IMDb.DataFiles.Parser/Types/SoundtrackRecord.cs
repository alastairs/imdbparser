using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDb.DataFiles.Parser.Types
{
    public class SoundtrackRecord
    {
        public Production MovieTitle { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
