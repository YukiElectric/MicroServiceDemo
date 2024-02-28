using AutoMapper;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Common
{
    public class Mapper : Profile
    {
        public Mapper() { 
            CreateMap<Student, StudentModel>().ReverseMap();
        }
    }
}
