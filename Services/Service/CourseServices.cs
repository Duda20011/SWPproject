using AutoMapper;
using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service
{
    public class CourseServices : ICourseServices
    {
        public Task<ResponseModel<CourseResponse>> CreateCourse(CourseModel course)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Pagination<CourseModel>>> GetAllCourse()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Pagination<CourseModel>>> GetCourseById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Pagination<CourseModel>>> GetCourseByName(string coursename)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<string>> RemoveCourse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<string>> UpdateCourse(int id, CourseModel courseModel)
        {
            throw new NotImplementedException();
        }
    }
}
