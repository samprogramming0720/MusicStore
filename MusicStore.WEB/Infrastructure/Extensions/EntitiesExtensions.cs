using MusicStore.Entities;
using MusicStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WEB.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateMusic(this Music music, MusicViewModel musicVM)
        {
            music.Title = musicVM.Title;
            music.Genre = musicVM.Genre;
            music.MusicVideoURI = musicVM.MusicVideoURI;
            music.AlbumID = musicVM.AlbumID;
            music.ArtistID = musicVM.ArtistID;
        }

        public static void UpdateAlbum(this Album album, AlbumViewModel albumVM)
        {
            album.AlbumTitle = albumVM.AlbumTitle;
            album.ArtistID = albumVM.ArtistID;
        }

        public static void UpdateArtist(this Artist artist, ArtistViewModel artistVM)
        {
            artist.Name = artistVM.Name;
            artist.Description = artistVM.Description;
        }
    }
}