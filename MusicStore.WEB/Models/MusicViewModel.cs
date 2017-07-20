using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.WEB.Models
{
    [Bind(Exclude = "AlbumCover")]
    public class MusicViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public int ArtistID { get; set; }

        public string ArtistName { get; set; }

        [Required]
        public int AlbumID { get; set; }

        public string AlbumTitle { get; set; }

        public string AlbumCover { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(200)]
        [MusicVideoURI]
        [Display(Name = "Music Video")]
        public string MusicVideoURI { get; set; }

    }

    public class MusicVideoURIAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;
            if (!string.IsNullOrEmpty(strValue) && strValue.ToLower().StartsWith("http://www.youtube.com/watch?"))
            {
                return true;
            }
            return false;
        }
    }
}