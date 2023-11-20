using Services.Commons;
using Services.Model;

namespace Services.Service
{
    public interface ICourseServices
    {
        Task<ResponseModel<CourseResponse>> CreateCourse(CourseModel course);
        Task<ResponseModel<Pagination<CourseModel>>> GetAllCourse();
        Task<ResponseModel<Pagination<CourseModel>>> GetCourseById(int id);
        Task<ResponseModel<Pagination<CourseModel>>> GetCourseByName(string coursename);
        Task<ResponseModel<string>> UpdateCourse(int id, CourseModel courseModel);
        Task<ResponseModel<string>> RemoveCourse(int id);
    }
}
