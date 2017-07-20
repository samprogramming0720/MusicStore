using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.WEB.Models
{
    [Bind(Exclude = "Image")]
    public class ArtistViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Artist Name")]
        public string Name { get; set; }
        
        [StringLength(2000)]
        [Display(Name = "Artist Description")]
        public string Description { get; set; }

        [Display(Name = "Artist Image")]
        public string Image { get; set; }

        [Display(Name = "List of Music")]
        public IEnumerable<MusicViewModel> MusicList { get; set; }

        [Display(Name = "List of Albums")]
        public IEnumerable<AlbumViewModel> AlbumList { get; set; }
    }
}