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

        public async Task<ResponseModel<string>> CreateCourse(CourseModel courseModel)
        {

            var courseObj = _mapper.Map<Course>(courseModel);
            await _unitOfWork.courseRepo.CreateAsync(courseObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync();

            if (isSuccess > 0)
            {
                return new ResponseModel<string> { Data = "Success" };
            }
            else
            {
                return new ResponseModel<string> { Errors = "Fail!" };
            }
        }

        public async Task<ResponseModel<Pagination<CourseModel>>> GetAllCourse(int pageIndex = 1, int pageSize = 10)
        {
            var courseObj = await _unitOfWork.courseRepo.GetAllCourseDetail(pageIndex, pageSize);
            var result = _mapper.Map<Pagination<CourseModel>>(courseObj);
            if (courseObj.Items.Count() < 1)
            {
                return new ResponseModel<Pagination<CourseModel>> { Errors = "Not found" };
            }
            return new ResponseModel<Pagination<CourseModel>> { Data = result };
        }

        public async Task<ResponseModel<Pagination<CourseModel>>> GetCourseById(int id, int pageIndex = 1, int pageSize = 10)
        {
            var courseObj = await _unitOfWork.courseRepo.GetCourseDetailById(id, pageIndex, pageSize);
            var result = _mapper.Map<Pagination<CourseModel>>(courseObj);
            if (courseObj.Items.Count() < 1)
            {
                return new ResponseModel<Pagination<CourseModel>> { Errors = "Not found" };
            }
            return new ResponseModel<Pagination<CourseModel>> { Data = result };

        }

        public async Task<ResponseModel<Pagination<CourseModel>>> GetCourseByName(string coursename, int pageIndex = 1, int pageSize = 10)
        {
            var courseObj = await _unitOfWork.courseRepo.GetCourseDetailByName(coursename, pageIndex, pageSize);
            var result = _mapper.Map<Pagination<CourseModel>>(courseObj);

            if (courseObj.Items.Count() < 1)
            {
                return new ResponseModel<Pagination<CourseModel>> { Errors = "Not found" };
            }

            return new ResponseModel<Pagination<CourseModel>> { Data = result };
        }

        

        public async Task<ResponseModel<string>> RemoveCourse(int id)
        {
            var courseObj = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (courseObj is not null)
            {
                _unitOfWork.courseRepo.DeleteAsync(courseObj);
                var isSuccess = await _unitOfWork.SaveChangeAsync();
                if (isSuccess > 0)
                {
                    return new ResponseModel<string> { Data = "Success" };
                }
            }
            return new ResponseModel<string> { Errors = "Fail!" };
        }

        public async Task<ResponseModel<string>> UpdateCourse(int id, CourseModel courseModel)
        {
            var courseObj = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (courseObj is not null)
            {
                _mapper.Map(courseModel, courseObj);
                _unitOfWork.courseRepo.UpdateAsync(courseObj);
                var isSuccess = await _unitOfWork.SaveChangeAsync();
                if (isSuccess > 0)
                {
                    return new ResponseModel<string> { Data = "Success" };
                }
            }
            return new ResponseModel<string> { Errors = "Fail!" };
        }
    }
}
