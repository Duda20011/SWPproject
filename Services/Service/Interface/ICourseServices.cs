using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service
{
    public interface ICourseServices
    {
        Task<Course> CreateCourse(CourseModel courseModel);
        Task<bool> UpdateCourse(CourseModel courseModel, int id);
        Task<bool> DeleteCourse(int id);
        Task<Course> GetCourseById(int id);
        Task<List<Course>> GetCoursesAsync();
    }
}
