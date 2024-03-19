using AutoMapper;
using StudentQueryAPI.Data;
using StudentQueryAPI.Models;

namespace StudentQueryAPI.Common
{
    public class Mapper : Profile
    {
        public Mapper() {
            CreateMap<Student, StudentModel>().ReverseMap();
        }
    }
}
