using Services.Enum;


namespace Services.Model
{
    public class CourseModel
    {
        public int UserId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public decimal Price { get; set; }
        
    }
}
