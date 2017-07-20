using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WEB.Infrastructure.Mappings
{
    internal class AutoMapperConfig
    {
        internal static void Configure()
        {
            Mapper.Initialize(con =>
            {
                con.AddProfile<EntityToModelMappingProfile>();
            });
        }
    }
}