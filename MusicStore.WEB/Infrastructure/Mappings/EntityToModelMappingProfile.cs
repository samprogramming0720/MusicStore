using AutoMapper;
using MusicStore.Entities;
using MusicStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WEB.Infrastructure.Mappings
{
    public class EntityToModelMappingProfile:Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<Music, MusicViewModel>()
                .ForMember(vm => vm.AlbumID, map => map.MapFrom(m => m.Album.ID))
                .ForMember(vm => vm.AlbumTitle, map => map.MapFrom(m => m.Album.AlbumTitle))
                .ForMember(vm => vm.AlbumCover, map => map.MapFrom(m => m.Album.AlbumCover))
                .ForMember(vm => vm.ArtistID, map => map.MapFrom(m => m.Artist.ID))
                .ForMember(vm => vm.ArtistName, map => map.MapFrom(m => m.Artist.Name));

            CreateMap<Album, AlbumViewModel>()
                .ForMember(vm => vm.ArtistID, map => map.MapFrom(m => m.Artist.ID))
                .ForMember(vm => vm.ArtistName, map => map.MapFrom(m => m.Artist.Name))
                .ForMember(vm => vm.AlbumCover,
                map => map.MapFrom(m => string.IsNullOrEmpty(m.AlbumCover) == true ?
                 "unknown.png" : m.AlbumCover));

            CreateMap<Artist, ArtistViewModel>()
                .ForMember(vm => vm.AlbumList, map => map.MapFrom(m => m.Albums))
                .ForMember(vm => vm.MusicList, map => map.MapFrom(m => m.Music))
                .ForMember(vm => vm.Image,
                map => map.MapFrom(m => string.IsNullOrEmpty(m.Image) == true ?
                "unknown.png" : m.Image));
        }
    }
}