using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using PhotoStudiy.Context.Contracts.Enums;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Services.Contracts.Enums;
using PhotoStudiy.Services.Contracts.ModelReqest;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.AutoMappers
{
    internal class ServiceMapper:Profile
    {
        public ServiceMapper()
        {
            CreateMap<Post, PostModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Client, ClientModel>(MemberList.Destination).ReverseMap();
            CreateMap<Photogragh, PhotographModel>(MemberList.Destination).ReverseMap();
            CreateMap<PhotoSet, PhotoSetModel>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<Recvisit, RecvisitModel>(MemberList.Destination).ReverseMap();
            CreateMap<Uslugi, UslugiModel>(MemberList.Destination).ReverseMap();
            CreateMap<Dogovor, DogovorModel>(MemberList.Destination)
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Photograph, opt => opt.Ignore())
                .ForMember(x => x.Photoset, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Recvisit, opt => opt.Ignore())
                .ForMember(x => x.Uslugi, opt => opt.Ignore()).ReverseMap();

            CreateMap<DogovorRequestModel, Dogovor>(MemberList.Destination)
                 .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Photogragh, opt => opt.Ignore())
                .ForMember(x => x.PhotoSet, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Recvisit, opt => opt.Ignore())
                .ForMember(x => x.Uslugi, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());
        }
    }
}
