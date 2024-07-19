using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProposalSystem.Dtos.Student;
using ProposalSystem.Models;

namespace ProposalSystem.utils.mapper
{
    public class StudentMapper : Profile
    {
        public StudentMapper(){

            CreateMap<CreateStudent, Student>()
                .ForMember( 
                    dest => dest.AppUser,
                    opt => opt.MapFrom( src => new  AppUser(){ PhoneNumber = src.PhoneNumber, UserName = src.UserName, Email = src.Email } ) );

            CreateMap<Student, CreateStudentResult>()
                .ForMember(

                    dest => dest.Email,
                    opt => opt.MapFrom( src => src.AppUser.Email )
                )
                .ForMember(

                    dest => dest.UserName,
                    opt => opt.MapFrom( src => src.AppUser.UserName )
                )
                .ForMember(

                    dest => dest.PhoneNumber,
                    opt => opt.MapFrom( src => src.AppUser.PhoneNumber )
                );

            CreateMap<Student, GetStudentResult>();
            
            CreateMap<UpdateStudent, AppUser>();
            

        }
    }
}