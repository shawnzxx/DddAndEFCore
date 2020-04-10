﻿using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App
{
    public class StudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Student GetById(long studentId)
        {
            Student student = _context.Students.Find(studentId);

            if (student == null)
                return null;

            #region Handle initial load backing filed
            //way one: eager loading
            //good performance, one database roundtrip, but missing Identity map pattern
            //because Find on Linq extension method, so not load from cahche

            //_context.Students
            //    .Include(x => x.Enrollments)
            //    .SingleOrDefault(x => x.Id == studentId);

            //way two: explicity load backing field collection
            //Two database roundtrips
            _context.Entry(student).Collection(x => x.Enrollments).Load();
            #endregion

            return student;
        }
    }
}