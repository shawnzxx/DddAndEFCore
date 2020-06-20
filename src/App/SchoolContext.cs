using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public sealed class SchoolContext : DbContext
    {
        private static readonly Type[] EnumerationTypes = { typeof(Course), typeof(Suffix) };

        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public SchoolContext(string connectionString, bool useConsoleLogger)
        {
            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();

            if (_useConsoleLogger) {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("Student").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("StudentID").ValueGeneratedOnAdd();
                x.Property(p => p.Email)
                .HasConversion(
                    p => p.Value, 
                    p => Email.Create(p).Value); //ValueObject convert to and from DB
                    
                //Student own the Name property
                x.OwnsOne(p => p.Name, p =>
                {
                    //for navigation object inside the value object 
                    //we need declare shadow property first map it to the correct name
                    p.Property<Guid>("NameSuffixID").HasColumnName("NameSuffixID");
                    //Name property consist three fileds
                    //How to map three fileds to the db columns
                    p.Property(pp => pp.First).HasColumnName("FirstName");
                    p.Property(pp => pp.Last).HasColumnName("LastName");
                    //Then map navigation proerty to the shadow property
                    p.HasOne(pp => pp.Suffix).WithMany().HasForeignKey("NameSuffixID").IsRequired(false);
                });
                x.HasOne(p => p.FavoriteCourse).WithMany();
                x.HasMany(p => p.Enrollments).WithOne(p => p.Student)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Suffix>(x =>
            {
                x.ToTable("Suffix").HasKey(p => p.Id);
                x.Property(p => p.Id).HasColumnName("SuffixID").ValueGeneratedOnAdd();
                x.Property(p => p.Name);
            });
            modelBuilder.Entity<Course>(x =>
            {
                x.ToTable("Course").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("CourseID").ValueGeneratedOnAdd();
                x.Property(p => p.Name);
            });
            modelBuilder.Entity<Enrollment>(x =>
            {
                x.ToTable("Enrollment").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("EnrollmentID").ValueGeneratedOnAdd();
                x.HasOne(p => p.Student).WithMany(p => p.Enrollments);
                x.HasOne(p => p.Course).WithMany();
                x.Property(p => p.Grade);
            });
        }

        //This method is to block enum class been set to EntityState.Updated 
        //and every time modify root Student entity will trigger Course and Suffix table update
        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> enumerationEntries = ChangeTracker.Entries()
                .Where(x => EnumerationTypes.Contains(x.Entity.GetType()));

            foreach (EntityEntry enumerationEntry in enumerationEntries)
            {
                enumerationEntry.State = EntityState.Unchanged;
            }

            return base.SaveChanges();
        }
    }
}
