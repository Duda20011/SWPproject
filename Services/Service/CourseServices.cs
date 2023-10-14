using AutoMapper;
using Services.Commons;
using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                // Assuming _unitOfWork and courseRepo are properly initialized and available.

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
    }
}
