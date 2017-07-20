using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.WEB.Models
{
    public class PlayListViewModel
    {
        [Display(Name="Playlist Name")]
        [StringLength(100)]
        public string Name { get; set; }

        public List<MusicViewModel> ListOfMusic { get; set; }

        [Display(Name = "Count")]
        public int MusicCount { get; set; }
    }
}