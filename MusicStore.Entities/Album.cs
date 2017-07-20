using MusicStore.Entities.EntityBase;
using System;
using System.Collections.Generic;

namespace MusicStore.Entities
{
    public class Album:IEntityBase
    {
        public int ID { get; set; }
        public string AlbumTitle { get; set; }
        public string AlbumCover { get; set; }
        
        public virtual ICollection<Music> Music { get; set; }

        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
