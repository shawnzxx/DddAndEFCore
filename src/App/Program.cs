using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            //string result1 = Execute(x => x.DisenrollStudent(1, 2));
            //string result2 = Execute(x => x.CheckStudentFavoriteCourse(1, 2));
            //string result3 = Execute(x => x.EnrollmentStudent(1, 2, Grade.A));

            string result4 = Execute(x => x.RegisterStudent("Car", "carl@gmail.com", 2, Grade.B));
        }

        private static string Execute(Func<StudentController, string> func)
        {
            string connectionString = GetConnectionString();
            bool useConsoleLogger = true; // use IHostingEnviroment.IsDevelopment for ASP.net project

            using (var context = new SchoolContext(connectionString, useConsoleLogger))
            {
                var controller = new StudentController(context);
                return func(controller);
            }
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration["ConnectionString"];
        }
    }
}
