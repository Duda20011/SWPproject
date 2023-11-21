using AutoMapper;
using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service
{
    public class CourseServices : ICourseServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Course> CreateCourse(CourseModel courseModel)
        {
            var course = new Course()
            {
                Id = courseModel.Id,
                title = courseModel.Title,
                CourseDescription = courseModel.CourseDescription,
                CreationDate = DateTime.Now,
                Price = courseModel.Price,
                imageUrl = courseModel.imageUrl,
                IsPulished = courseModel.isPublish,
                IsDeleted = false,
                CategoryId = courseModel.CategoryId
            };
            //course.CategoryId = await _unitOfWork.courseRepo.AutoIncreamentId();
            await _unitOfWork.courseRepo.CreateAsync(course);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return course;
            }
            return null;
        }
        public async Task<bool> UpdateCourse(CourseModel courseModel, string id)
        {
            var course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            course.Id = courseModel.Id;
            course.title = courseModel.Title;
            course.CourseDescription = courseModel.CourseDescription;
            course.Price = courseModel.Price;
            course.imageUrl = courseModel.imageUrl;
            course.IsPulished = courseModel.isPublish;
            _unitOfWork.courseRepo.UpdateAsync(course);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCourse(string id)
        {
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            course.DeletionDate = DateTime.Now;
            course.IsDeleted = true;
            _unitOfWork.courseRepo.DeleteAsync(course);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<Course> GetCourseById(string id)
        {
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            return course;
        }
        public async Task<List<Course>> GetCoursesAsync()
        {
            List<Course> courses = (await _unitOfWork.courseRepo.GetAllAsync()).ToList();
            List<Course> result = new List<Course>();
            foreach (var item in courses)
            {
                if (item.IsDeleted == false)
                {
                    result.Add(item);
                }
            }
            return result;
        }

    }
}
