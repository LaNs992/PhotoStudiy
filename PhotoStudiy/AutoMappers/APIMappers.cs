using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using PhotoStudiy.API.Enum;
using PhotoStudiy.API.Models.CreateRequest;
using PhotoStudiy.API.Models.Request;
using PhotoStudiy.API.Models.Response;
using PhotoStudiy.Services.Contracts.Enums;
using PhotoStudiy.Services.Contracts.ModelReqest;
using PhotoStudiy.Services.Contracts.Models;
using System.IO;

namespace PhotoStudiy.API.AutoMappers
{
    public class APIMappers:Profile
    {
        public APIMappers()
        {
            CreateMap<PostModel, PostResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<CreatePhotographRequest, PhotographModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreatePhotoSetRequest, PhotoSetModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateProductRequest, ProductModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateClientRequest, ClientModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateRecvisitRequest, RecvisitModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateUslugiRequest, UslugiModel>(MemberList.Destination)
               .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<PhotographRequest, PhotographModel>(MemberList.Destination);
            CreateMap<ProductRequest, ProductModel>(MemberList.Destination);
            CreateMap<PhotoSetRequest, PhotoSetModel>(MemberList.Destination);
            CreateMap<ClientRequest, ClientModel>(MemberList.Destination);
            CreateMap<RecvisitRequest, RecvisitModel>(MemberList.Destination);
            CreateMap<UslugiRequest, UslugiModel>(MemberList.Destination);
            CreateMap<DogovorRequest, DogovorModel>(MemberList.Destination)
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Photograph, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Photoset, opt => opt.Ignore())
            .ForMember(x => x.Recvisit, opt => opt.Ignore())
            .ForMember(x => x.Uslugi, opt => opt.Ignore());

            CreateMap<DogovorRequest, DogovorRequestModel>(MemberList.Destination);
            CreateMap<CreateDogovorRequest, DogovorRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<PhotographModel, PhotographResponse>(MemberList.Destination);
            CreateMap<ProductModel, ProductResponse>(MemberList.Destination);
            CreateMap<PhotoSetModel, PhotoSetResponse>(MemberList.Destination);
            CreateMap<RecvisitModel, RecvisitResponse>(MemberList.Destination);
            CreateMap<UslugiModel, UslugiResponse>(MemberList.Destination);
            CreateMap<DogovorModel, DogovorResponse>(MemberList.Destination);
            CreateMap<ClientModel, ClientResponse>(MemberList.Destination);
               
        }
    }
}
