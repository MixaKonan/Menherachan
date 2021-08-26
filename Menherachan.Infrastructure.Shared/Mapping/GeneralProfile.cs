using System;
using AutoMapper;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Infrastructure.Shared.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(view => view.Files, post => post.MapFrom(p => p.File))
                .ForMember(view => view.Admin, post => post.MapFrom(p => p.Admin))
                .ForMember(view => view.Author, post => post.MapFrom(p => p.AnonName))
                .ForMember(view => view.PostedAt, post => post.MapFrom(p => DateTimeOffset.FromUnixTimeSeconds(p.CreatedAt).DateTime));

            CreateMap<Admin, AdminViewModel>()
                .ForMember(view => view.Nickname, admin => admin.MapFrom(a => a.Login))
                .ForMember(view => view.ColorCode, admin => admin.MapFrom(a => a.NicknameColorCode));

            CreateMap<File, FileViewModel>();
        }
    }
}