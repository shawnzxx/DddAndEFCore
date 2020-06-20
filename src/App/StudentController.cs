﻿using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace App
{
    public sealed class StudentController
    {
        private readonly SchoolContext _context;
        private readonly StudentRepository _repository;

        public StudentController(SchoolContext context)
        {
            _context = context;
            _repository = new StudentRepository(context);
        }

        public string CheckStudentFavoriteCourse(long studentId, Guid courseId)
        {
            Student student = _repository.GetById(studentId);
            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Course not found";

            return student.FavoriteCourse == course ? "Yes" : "No";
        }

        public string EnrollmentStudent(long studentId, Guid courseId, Grade grade)
        {
            Student student = _repository.GetById(studentId);

            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Course not found";

            string result = student.EnrollIn(course, grade);

            _context.SaveChanges();

            return result;
        }

        public string DisenrollStudent(long studentId, Guid courseId)
        {
            Student student = _repository.GetById(studentId);
            if (student == null)
                return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null)
                return "Course not found";

            student.Disenroll(course);

            _context.SaveChanges();

            return "OK";
        }

        public string RegisterStudent(
            string firstName, string lastName, Guid nameSuffixId, string email, 
            Guid favoriteCourseId, Grade favoriteCourseGrade)
        {
            Course favoriteCourse = _context.Courses.Find(favoriteCourseId); //retrieve from db attached object

            //Course favoriteCourse = Course.FromId(favoriteCourseId); //create from local, detached object 
            if (favoriteCourse == null)
                return "Course not found";

            Suffix suffix = Suffix.FromId(nameSuffixId);
            if (suffix == null)
                return "Suffix not found";

            Result<Email> emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            Result<Name> nameResult = Name.Create(firstName, lastName, suffix);
            if (nameResult.IsFailure)
                return nameResult.Error;

            var student = new Student(
                Guid.NewGuid(),
                nameResult.Value, 
                emailResult.Value,
                favoriteCourse,
                favoriteCourseGrade);
            _repository.Save(student);

            _context.SaveChanges();

            return "OK";
        }

        public string EditPersonalInfo(
            long studentId, string firstName, Guid nameSuffixId, string lastName, string email, Guid favoriteCourseId)
        {
            Student student = _repository.GetById(studentId);
            if (student == null)
                return "Student not found";

            Course favoriteCourse = Course.FromId(favoriteCourseId);
            if (favoriteCourse == null)
                return "Course not found";

            Suffix suffix = Suffix.FromId(nameSuffixId);
            if (suffix == null)

                return "Suffix not found";
            Result<Email> emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            Result<Name> nameResult = Name.Create(firstName, lastName, suffix);
            if (nameResult.IsFailure)
                return nameResult.Error;

            student.EditPersonalInfo(nameResult.Value, emailResult.Value, favoriteCourse);

            _context.SaveChanges();

            return "OK";
        }
    }
}