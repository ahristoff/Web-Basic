
namespace _4_Many_to_Many_Relation
{
    using System.Collections.Generic;

    public class Course
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public List<StudentsCourse> StudentsCourses { get; set; } = new List<StudentsCourse>();
    }
}
