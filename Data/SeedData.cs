using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProposalSystem.Models;

namespace ProposalSystem.Data
{
    public class SeedData
    {
        public static List<AppUser> GetUsers(IPasswordHasher<AppUser> passwordHasher)
        {
            var users = new List<AppUser>
        {
            new() { Id = "admin1", UserName = "NgZiXing", NormalizedUserName = "NGZIXING", Email = "ngzixing@example.com", NormalizedEmail = "NGZIXING@EXAMPLE.COM", PhoneNumber= "0123456789"},
            new() { Id = "lecturer1", UserName = "AngYiQin", NormalizedUserName = "ANGYIQIN", Email = "angyiqin@example.com", NormalizedEmail = "ANGYIQIN@EXAMPLE.COM", PhoneNumber= "0123456789" },
            new() { Id = "lecturer2", UserName = "LiewYvonne", NormalizedUserName = "LIEWYVONNE", Email = "liewyvonne@example.com", NormalizedEmail = "liewyvonne@EXAMPLE.COM", PhoneNumber= "0123456789" },
            new() { Id = "lecturer3", UserName = "SooWanYing", NormalizedUserName = "SOOWANYING", Email = "soowanying@example.com", NormalizedEmail = "SOOWANYING@EXAMPLE.COM", PhoneNumber= "0123456789" },
            new() { Id = "student1", UserName = "YewRuiXiang", NormalizedUserName = "YEWRUIXIANG", Email = "yewruixiang@example.com", NormalizedEmail = "YEWRUIXIANG@EXAMPLE.COM", PhoneNumber= "0123456789" },
            new() { Id = "student2", UserName = "LooZhiYuan", NormalizedUserName = "LOOZHIYUAN", Email = "loozhiyuan@example.com", NormalizedEmail = "LOOZHIYUAN@EXAMPLE.COM", PhoneNumber= "0123456789" },
            new() { Id = "student3", UserName = "SamChiaYun", NormalizedUserName = "SAMCHIAYUN", Email = "samchiayun@example.com", NormalizedEmail = "SAMCHIAYUN@EXAMPLE.COM", PhoneNumber= "0123456789" },

        };

            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "User@123456789");
            }

            return users;
        }


        public static List<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                    new IdentityRole {Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole {Id = "2", Name = "Lecturer", NormalizedName = "LECTURER" },
                    new IdentityRole {Id = "3", Name = "Student", NormalizedName = "STUDENT" },
            };
        }

        public static List<IdentityUserRole<string>> GetRolesToUsers()
        {
            return new List<IdentityUserRole<string>>{

                new IdentityUserRole<string> { UserId = "admin1", RoleId = "1" },
                new IdentityUserRole<string> { UserId = "lecturer1", RoleId = "2" },
                new IdentityUserRole<string> { UserId = "lecturer2", RoleId = "2" },
                new IdentityUserRole<string> { UserId = "lecturer3", RoleId = "2" },
                new IdentityUserRole<string> { UserId = "student1", RoleId = "3" },
                new IdentityUserRole<string> { UserId = "student2", RoleId = "3" },
                new IdentityUserRole<string> { UserId = "student3", RoleId = "3" }
            };
        }

        public static List<AcademicProgram> GetAcademicPrograms(){

            return new List<AcademicProgram>{

                new AcademicProgram{ Id = "program1", Name = "Software Engineering", Description = "Software Engineering Description" },
                new AcademicProgram{ Id = "program2", Name = "Data Engineering", Description = "Data Engineering Description" }
            };
        }

        public static List<Admin> GetAdmin(){

            return new List<Admin>{

                new Admin{ Id = "admin1", AdminId = "A21EC0213" },
            };
        }

        public static List<Lecturer> GetLecturer(){

            return new List<Lecturer>{

                new Lecturer{ Id = "lecturer1", StaffId = "A21EC1234", Domain = Enum.Domain.Research, AcademicProgramId = "program1" },
                new Lecturer{ Id = "lecturer2", StaffId = "A21EC2234", Domain = Enum.Domain.Research, AcademicProgramId = "program1" },
                new Lecturer{ Id = "lecturer3", StaffId = "A21EC3234", Domain = Enum.Domain.IsDevelopment, AcademicProgramId = "program2" },
            };
        }

        public static List<Student> GetStudent(){

            return new List<Student>{

                new Student{ Id = "student1", MatricId = "A21EC0149", Year = 1, Session = 1, Semester = 1, SupervisorId = "A21EC3234", FirstEvaluatorId = "A21EC2234", SecondEvaluatorId = "A21EC1234", AcademicProgramId = "program1" },
                new Student{ Id = "student2", MatricId = "A21EC0197", Year = 1, Session = 2, Semester = 1, SupervisorId = "A21EC3234", FirstEvaluatorId = "A21EC2234", SecondEvaluatorId = "A21EC1234", AcademicProgramId = "program1"},
                new Student{ Id = "student3", MatricId = "A21EC0127", Year = 1, Session = 3, Semester = 2, SupervisorId = "A21EC1234", AcademicProgramId = "program2" },
            };
        }

        public static List<Committee> GetCommittee(){

            return new List<Committee>{

                new Committee{ Id = "committee1", LecturerId = "A21EC1234", AcademicProgramId = "program1" },
                new Committee{ Id = "committee2", LecturerId = "A21EC2234", AcademicProgramId = "program1" },
                new Committee{ Id = "committee3", LecturerId = "A21EC3234", AcademicProgramId = "program2" },
            };
        }

        public static List<ApplySupervisor> GetApply(){

            return new List<ApplySupervisor>{

                new ApplySupervisor{ Id = "committee1", SupervisorId = "A21EC1234", MatricId = "A21EC0149", ApplyState = Enum.ApplySupervisorStatus.Approved },
                new ApplySupervisor{ Id = "committee2", SupervisorId = "A21EC2234", MatricId = "A21EC0149", ApplyState = Enum.ApplySupervisorStatus.Reject },
                new ApplySupervisor{ Id = "committee3", SupervisorId = "A21EC3234", MatricId = "A21EC0197", ApplyState = Enum.ApplySupervisorStatus.Reject },
                new ApplySupervisor{ Id = "committee4", SupervisorId = "A21EC2234", MatricId = "A21EC0197", ApplyState = Enum.ApplySupervisorStatus.Pending },
            };
        }

        public static List<Proposal> GetProposal(){

            return new List<Proposal>{

                new Proposal{ Id = "proposal1", StudentId = "A21EC0149", Year = 1, Session = 1, Semester = 1, Domain = Enum.Domain.Research, ProposalStatus = Enum.ProposalStatus.Accept, Mark = 89, Title = "Attention Is All You Need", CreatedAt = new DateTime(2024, 7, 15, 8, 23, 50), LinkProposal = "1-emCUoUVFDOTgwWn0YAdWGf7xz_2cjOj", LinkForm = "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy"  },
                new Proposal{ Id = "proposal2", StudentId = "A21EC0149", Year = 1, Session = 1, Semester = 1, Domain = Enum.Domain.Research, ProposalStatus = Enum.ProposalStatus.Condition, Title = "GPT", CreatedAt = new DateTime(2024, 7, 14, 7, 13, 50), LinkProposal = "1EZ8hzuu1dx5TtjLvQyAihEW6YRC3HJpt", LinkForm = "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy" },
                new Proposal{ Id = "proposal3", StudentId = "A21EC0149", Year = 1, Session = 1, Semester = 1, Domain = Enum.Domain.Research, ProposalStatus = Enum.ProposalStatus.Reject, Title = "Bert", CreatedAt = new DateTime(2024, 7, 13, 10, 56, 51), LinkProposal = "1iSMSMqsudSCIsOUwxhS1aqIwAItqPlKc", LinkForm = "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy" },
                new Proposal{ Id = "proposal4", StudentId = "A21EC0149", Year = 1, Session = 1, Semester = 1, Domain = Enum.Domain.Research, ProposalStatus = Enum.ProposalStatus.Pending, Title = "Chain Of Thought", CreatedAt = new DateTime(2024, 7, 1, 8, 00, 32), LinkProposal = "1HFVTio-i4xXXScDc8PBEmRYfbQZKGeiP", LinkForm = "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy" },
                new Proposal{ Id = "proposal5", StudentId = "A21EC0197", Year = 1, Session = 2, Semester = 1, Domain = Enum.Domain.IsDevelopment, ProposalStatus = Enum.ProposalStatus.Pending, Title = "Attention Is All You Need", CreatedAt = new DateTime(2024, 6, 15, 12, 23, 21),  LinkProposal = "1-emCUoUVFDOTgwWn0YAdWGf7xz_2cjOj", LinkForm = "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy" },
                new Proposal{ Id = "proposal6", StudentId = "A21EC0127", Year = 1, Session = 3, Semester = 2, Domain = Enum.Domain.IsDevelopment, ProposalStatus = Enum.ProposalStatus.Reject, Title = "GPT", CreatedAt = new DateTime(2024, 6, 24, 11, 11, 29),  LinkProposal = "1EZ8hzuu1dx5TtjLvQyAihEW6YRC3HJpt", LinkForm = "1o8s4xOvuPbZVN-LhpV-IhwE54-Z85VAy" },
            };
        }

    }
}