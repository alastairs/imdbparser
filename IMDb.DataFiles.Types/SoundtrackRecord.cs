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

        public override bool Equals(object obj)
        {
            var soundtrackRecord = obj as SoundtrackRecord;
            if (soundtrackRecord == null)
            {
                return false;
            }

            bool songsAreEqual = true;
            int i = 0;
            while (songsAreEqual)
            {
                songsAreEqual = this.Songs.ElementAt(i) == soundtrackRecord.Songs.ElementAt(i);
                i++;
            }

            return (this.Production == soundtrackRecord.Production) && songsAreEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
