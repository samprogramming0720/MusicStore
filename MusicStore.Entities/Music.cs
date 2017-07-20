using MusicStore.Entities.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Entities
{
    public class Music:IEntityBase
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string MusicVideoURI { get; set; }
        public string Genre { get; set; }

        //Nav Prop
        public int AlbumID { get; set; }
        public virtual Album Album { get; set; }

        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }
        
        public virtual ICollection<PlayList> PlayLists { get; set; }
    }
}
