using System;
using AutoMapper;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Common;
using Menherachan.Domain.Entities.ViewModels.Thread;

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

            CreateMap<Token, RefreshToken>()
                .ForMember(rt => rt.Token, t => t.MapFrom(token => token.TokenString))
                .ForMember(rt => rt.CreatedAt, t => t.MapFrom(token => token.CreatedAt))
                .ForMember(rt => rt.ExpiresAt, t => t.MapFrom(token => token.ExpiresAt))
                .ReverseMap();

            CreateMap<Thread, ThreadViewModel>()
                .ForMember(tvm => tvm.Posts, t => t.MapFrom(thread => thread.Post))
                .ForMember(tvm => tvm.ThreadId, t => t.MapFrom(thread => thread.ThreadId));
        }
    }
}