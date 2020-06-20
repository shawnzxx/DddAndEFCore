using System;
using System.Linq;

namespace App
{
    public class Course : Entity
    {
        public static readonly Course Calculus = new Course(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213671"), "Calculus");
        public static readonly Course Chemistry = new Course(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213672"), "Chemistry");
        public static readonly Course Literature = new Course(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213673"), "Literature");
        public static readonly Course Trigonometry = new Course(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213674"), "Trigonometry");
        public static readonly Course Microeconomics = new Course(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213675"), "Microeconomics");

        public static readonly Course[] AllCourses = { Calculus, Chemistry, Literature, Trigonometry, Microeconomics};

        public string Name { get; }

        protected Course()
        {
        }

        private Course(Guid id, string name)
            : base(id)
        {
            Name = name;
        }

        public static Course FromId(Guid id)
        {
            return AllCourses.SingleOrDefault(x => x.Id == id);
        }
    }
}