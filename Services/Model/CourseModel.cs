using Services.Entity;
using Services.Enum;


namespace Services.Model
{
    public class CourseModel
    {
        public string Title { get; set; }
        public string CourseDescription { get; set; }
        public decimal Price { get; set; }
        public string imageUrl { get; set; }
        public bool isPublish { get; set; }
    }
    public class CourseResponse
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}
