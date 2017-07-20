using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.WEB.Models
{
    [Bind(Exclude = "AlbumCover, Image")]
    public class AlbumViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Album Title")]
        [StringLength(100)]
        public string AlbumTitle { get; set; }

        [Required]
        public int ArtistID { get; set; }

        [Display(Name = "Album Cover")]
        public string AlbumCover { get; set; }

        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Display(Name = "Tracks")]
        public IEnumerable<MusicViewModel> MusicList { get; set; }
    }
}