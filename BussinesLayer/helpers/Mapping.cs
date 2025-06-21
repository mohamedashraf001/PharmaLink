using AutoMapper;
using DataLayer.Entities;
using PharmaLink.DTOs;
using PharmaLink.DTOs.Cart;
using PharmaLink.DTOs.Pharmacy;
using PharmaLink.DTOs.Post;
using PharmaLink.DTOs.Request;

namespace PharmaLink.Profiles
{
    public class PharmacyProfile : Profile
    {
        public PharmacyProfile()
        {
            CreateMap<Pharmacy, PharmacyReadDto>();
            CreateMap<PharmacyCreateDto, Pharmacy>();


            CreateMap<PostCreateDto, Post>();
            CreateMap<Post, PostResponseDto>();

            CreateMap<Pharmacy, PharmacyResponseDto>();
            CreateMap<Post, PostResponseDto>();



            CreateMap<Post, PostResponseDto>()
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate.ToString("yyyy-MM-dd")));

            CreateMap<CartItemCreateDto, CartItem>();
            CreateMap<CartItem, CartItemResponseDto>();
            CreateMap<CartItem, CartItemResponseDto>();

            CreateMap<Request, RequestResponseDto>()
             .ForMember(dest => dest.Pharmacy, opt => opt.MapFrom(src => src.RequestingPharmacy));


            CreateMap<RequestCreateDto, Request>();

        }
    }
}
