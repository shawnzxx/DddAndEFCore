using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Student : Entity
    {
        //1: EF core no need have a private setter, getter is enough(remove private set)
        //it will use backing field by default, but we may need to change some of property value
        //so keep private setter class method can change property value
        
        //public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        //Use navigation property instead of FavoriteCourseId
        public virtual Course FavoriteCourse { get; private set; }
        //Use BackingField for collection relationship
        //Use IReadOnlyList it more specific than IEnumerable provide more funcionality
        private readonly List<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

        //2: If all above property is primitive type then no need the private constructor
        //If we defined a non basic type(Like FavoriteCourse), we need private cnstructor otherwise EF core will throw error
        //Because ef core got a limitation, it don't know how to map db columns to the none basic type
        protected Student() { }


        //3: If have custom constructor, then outside can not use parameterless constructor anymore
        //4: The constructor inputs name need to match with property name
        //5: Inside the custom constructor we no need to provide all fields(like here we don't have Id field)
        //How to define what inputs we need? It based on Student class constrain
        //Like in here we need all three name, email and favoriteCourseId to creat a valid Student object
        public Student(string name, string email, Course favoriteCourse)
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }

        public string EnrollIn(Course course, Grade grade)
        {
            if (_enrollments.Any(x => x.Course == course))
                return $"Already enrolled in course '{course.Name}'";

            var enrollment = new Enrollment(course, this, grade);
            _enrollments.Add(enrollment);

            return "OK";
        }


        public void Disenroll(Course course)
        {
            Enrollment enrollment = _enrollments.FirstOrDefault(x => x.Course == course);

            if (enrollment == null)
                return;

            _enrollments.Remove(enrollment);
        }
    }
}
