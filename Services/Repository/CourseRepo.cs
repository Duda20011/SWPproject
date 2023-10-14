using Services.Entity;
using Services.Repository.Interface;
using Services.Service;
using Services.Service.Interface;

namespace Services.Repository
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        public CourseRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices) : base(context, currentTime, claimsServices)
        {
        }
    }
}
