using MusicStore.Entities.EntityBase;
using System.Collections.Generic;

namespace MusicStore.Entities
{
    public class Artist:IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Music> Music { get; set; }
    }
}
