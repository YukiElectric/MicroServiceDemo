using AutoMapper;
using StudentAPI.Application.Commands;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Common
{
    public class Mapper : Profile
    {
        public Mapper() { 
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Student, CreateStudentCommand>().ReverseMap();
        }
    }
}
