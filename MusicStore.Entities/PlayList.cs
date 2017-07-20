using MusicStore.Entities.EntityBase;
using System.Collections.Generic;

namespace MusicStore.Entities
{
    public class PlayList:IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Music> Music { get; set; }

    }
}
