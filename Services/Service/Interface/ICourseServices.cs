using Services.Commons;
using Services.Model;

namespace Services.Service
{
    public interface ICourseServices
    {
        Task<ResponseModel<string>> CreateCourse(CourseModel courseModel);
    }
}
