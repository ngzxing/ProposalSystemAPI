using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProposalSystem.Dtos.Account;
using ProposalSystem.Dtos.Lecturer;
using ProposalSystem.Models;

namespace ProposalSystem.utils.mapper
{
    public class LecturerMapper : Profile
    {
        public LecturerMapper(){

            CreateMap<CreateLecturer, Lecturer>()
                .ForMember( 
                    dest => dest.AppUser,
                    opt => opt.MapFrom( src => new  AppUser(){ PhoneNumber = src.PhoneNumber, UserName = src.UserName, Email = src.Email } ) );


            CreateMap<Lecturer, CreateLecturerResult>()
                .ForMember(

                    dest => dest.UserName,
                    opt => opt.MapFrom( src => src.AppUser.UserName )
                )
                .ForMember(

                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom( src => src.AppUser.PhoneNumber )
                )
                .ForMember(

                    dest => dest.Email,
                    opt => opt.MapFrom( src => src.AppUser.Email )
            );

            CreateMap<Lecturer, GetLecturerResult>()
                .ForMember(

                    dest => dest.UserName,
                    opt => opt.MapFrom( src => src.AppUser.UserName )
                )
                .ForMember(

                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom( src => src.AppUser.PhoneNumber )
                )
                .ForMember(

                    dest => dest.Email,
                    opt => opt.MapFrom( src => src.AppUser.Email )
                );

            CreateMap<AppUserDto, AppUser>();

            CreateMap<UpdateLecturer, Lecturer>()
                .ForMember(

                    dest => dest.AppUser,
                    opt => opt.MapFrom( src => new  AppUserDto(){ PhoneNumber = src.PhoneNumber, UserName = src.UserName, Email = src.Email }  )
                );

            
        }
    }
}