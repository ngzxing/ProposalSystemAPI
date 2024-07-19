using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProposalSystem.Models;

namespace ProposalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {   
        // private readonly UserManager<AppUser> _userManager;
        public ApplicationDbContext( DbContextOptions dbContextOptions)
        : base(dbContextOptions){
            
            // _userManager = userManager;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Lecturer>().HasIndex( l => l.StaffId ).IsUnique();
            builder.Entity<Student>().HasIndex( l => l.MatricId ).IsUnique();

            builder.Entity<ApplySupervisor>().Property( a => a.ApplyState );

            builder.Entity<Committee>()
                .HasIndex(c => new { c.AcademicProgramId , c.LecturerId })
                .IsUnique();

            builder.Entity<Proposal>()
                    .Property( p => p.Id )
                    .ValueGeneratedOnAdd();

            builder.Entity<Committee>()
                    .Property( c => c.Id )
                    .ValueGeneratedOnAdd();

            builder.Entity<Comment>()
                    .Property( c => c.Id )
                    .ValueGeneratedOnAdd();

            builder.Entity<AcademicProgram>()
                    .Property( c => c.Id )
                    .ValueGeneratedOnAdd();

            builder.Entity<ApplySupervisor>()
                    .Property( a => a.Id )
                    .ValueGeneratedOnAdd();


            builder.Entity<ApplySupervisor>()
                .HasOne( a => a.Supervisor )
                .WithMany( l => l.Applications )
                .HasForeignKey( a => a.SupervisorId )
                .HasPrincipalKey( l => l.StaffId )
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ApplySupervisor>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications )
                .HasForeignKey( a => a.MatricId )
                .HasPrincipalKey( s => s.MatricId )
                .OnDelete(DeleteBehavior.ClientCascade);
            
            builder.Entity<Comment>()
                .HasOne( c => c.Evaluator )
                .WithMany(l => l.Comments)
                .HasForeignKey( c => c.EvaluatorId )
                .HasPrincipalKey( l => l.StaffId )
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Committee>()
                .HasOne( c => c.Lecturer )
                .WithMany(l => l.Committee)
                .HasForeignKey(  c => c.LecturerId )
                .HasPrincipalKey( l => l.StaffId )
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Proposal>()
                .HasOne( c => c.Student )
                .WithMany( s => s.Proposals )
                .HasForeignKey( c => c.StudentId )
                .HasPrincipalKey( s => s.MatricId )
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.Entity<Student>()
                .HasOne( s => s.Supervisor )
                .WithMany( l => l.SupervisedStudents )
                .HasForeignKey( s => s.SupervisorId )
                .HasPrincipalKey( l => l.StaffId )
                .OnDelete(DeleteBehavior.ClientSetNull);;

            builder.Entity<Student>()
                .HasOne( s => s.FirstEvaluator )
                .WithMany( l => l.FirstEvaluatedStudents )
                .HasForeignKey( s => s.FirstEvaluatorId )
                .HasPrincipalKey( l => l.StaffId )
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Student>()
                .HasOne( s => s.SecondEvaluator )
                .WithMany( l => l.SecondEvaluatedStudents )
                .HasForeignKey( s => s.SecondEvaluatorId )
                .HasPrincipalKey( l => l.StaffId )
                .OnDelete(DeleteBehavior.ClientSetNull);


            var passwordHasher = new PasswordHasher<AppUser>();

            builder.Entity<AppUser>().HasData(SeedData.GetUsers(passwordHasher));
            builder.Entity<IdentityRole>().HasData(SeedData.GetRoles());
            builder.Entity<IdentityUserRole<string>>().HasData(SeedData.GetRolesToUsers());
            builder.Entity<AcademicProgram>().HasData(SeedData.GetAcademicPrograms());
            builder.Entity<Admin>().HasData(SeedData.GetAdmin());
            builder.Entity<Lecturer>().HasData(SeedData.GetLecturer());
            builder.Entity<Student>().HasData(SeedData.GetStudent());
            builder.Entity<Committee>().HasData(SeedData.GetCommittee());
            builder.Entity<ApplySupervisor>().HasData(SeedData.GetApply());
            builder.Entity<Proposal>().HasData(SeedData.GetProposal());
                

        }
        public DbSet<AcademicProgram> AcademicPrograms {get; set;}

        public DbSet<ApplySupervisor> ApplySupervisors {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Committee> Committees {get; set;}
        public DbSet<Lecturer> Lecturers {get; set;}
        public DbSet<Proposal> Proposals {get; set;}
        public DbSet<Student> Students {get; set;}
        public DbSet<Admin> Admin {get; set;}
    }
}