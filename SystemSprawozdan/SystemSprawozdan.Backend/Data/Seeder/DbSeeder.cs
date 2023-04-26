using System.Collections;
using Microsoft.AspNetCore.Identity;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Data.Seeder
{
    public class DbSeeder
    {
        private readonly ApiDbContext _dbContext;
        private readonly IPasswordHasher<Admin> _passwordHasher;
        private readonly IPasswordHasher<Student> _passwordHasherStudent;
        private readonly IPasswordHasher<Teacher> _passwordHasherTeacher;

        public DbSeeder(ApiDbContext dbContext, IPasswordHasher<Admin> passwordHasher, IPasswordHasher<Student> passwordHasherStudent, IPasswordHasher<Teacher> passwordHasherTeacher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _passwordHasherStudent = passwordHasherStudent;
            _passwordHasherTeacher = passwordHasherTeacher;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Admin.Any())
                {
                    var admin = GetAdmin();
                    _dbContext.Admin.AddRange(admin);
                }

                if (!_dbContext.Major.Any())
                {
                    _dbContext.Major.AddRange(GetMajors());
                }
                
                if (!_dbContext.Term.Any())
                {
                    _dbContext.Term.AddRange(GetTerms());
                }
                
                if (!_dbContext.Subject.Any())
                {
                    _dbContext.Subject.AddRange(GetSubjects());
                }
                
                if (!_dbContext.SubjectGroup.Any())
                {
                    _dbContext.SubjectGroup.AddRange(GetSubjectGroups());
                }
                
                if (!_dbContext.SubjectSubgroup.Any())
                {
                    _dbContext.SubjectSubgroup.AddRange(GetSubjectSubgroups());
                }
                
                if (!_dbContext.Student.Any())
                {
                    _dbContext.Student.AddRange(GetStudents());
                }

                if (!_dbContext.Teacher.Any())
                {
                    _dbContext.Teacher.AddRange(GetTeachers());
                }

                if (!_dbContext.ReportTopic.Any())
                {
                    _dbContext.ReportTopic.AddRange(GetReportTopics());
                }
                
                if (!_dbContext.StudentReport.Any())
                {
                    _dbContext.StudentReport.AddRange(GetStudentReports());
                }
                
                if (!_dbContext.StudentReportFile.Any())
                {
                    _dbContext.StudentReportFile.AddRange(GetStudentReportFiles());
                }
                
                if (!_dbContext.ReportComment.Any())
                {
                    _dbContext.ReportComment.AddRange(GetReportComments());
                }

                _dbContext.SaveChanges();

            }
        }

        private Admin GetAdmin()
        {
            var admin = new Admin()
            {
                Id = 1,
                Login = "admin"
            };

            admin.Password = _passwordHasher.HashPassword(admin, "admin");
            return admin;
        }

        private List<Major> GetMajors()
        {
            var majors = new List<Major>()
            {
                new Major()
                {
                    Id = 1,
                    Name = "Informatyka",
                    StartedAt = DateOnly.Parse("01-10-2021"),
                    MajorCode = "EF-DI"
                },

                new Major()
                {
                    Id = 2,
                    Name = "Automatyka i robotyka",
                    StartedAt = DateOnly.Parse("01-10-2021"),
                    MajorCode = "EA-DI"
                },

                new Major()
                {
                    Id = 3,
                    Name = "Elektromobilność",
                    StartedAt = DateOnly.Parse("01-10-2020"),
                    MajorCode = "EM-DI"
                }
            };

            return majors;
        }

        private List<Term> GetTerms()
        {
            var terms = new List<Term>()
            {
                new Term()
                {
                    Id = 1,
                    TermNumber = 1,
                    StartedAt = DateOnly.Parse("01-10-2021")
                },

                new Term()
                {
                    Id = 2,
                    TermNumber = 2,
                    StartedAt = DateOnly.Parse("25-03-2022")
                },

                new Term()
                {
                    Id = 3,
                    TermNumber = 3,
                    StartedAt = DateOnly.Parse("01-10-2022")
                },

                new Term()
                {
                    Id = 4,
                    TermNumber = 4,
                    StartedAt = DateOnly.Parse("27-03-2023")
                },

                new Term()
                {
                    Id = 5,
                    TermNumber = 5,
                    StartedAt = DateOnly.Parse("01-10-2023")
                },

                new Term()
                {
                    Id = 6,
                    TermNumber = 1,
                    StartedAt = DateOnly.Parse("1-10-2020")
                },

                new Term()
                {
                    Id = 7,
                    TermNumber = 1,
                    StartedAt = DateOnly.Parse("22-03-2021")
                },

                new Term()
                {
                    Id = 8,
                    TermNumber = 2,
                    StartedAt = DateOnly.Parse("01-10-2022")
                },

                new Term()
                {
                    Id = 9,
                    TermNumber = 3,
                    StartedAt = DateOnly.Parse("24-03-2023")
                },
            };

            return terms;
        }

        private List<Subject> GetSubjects()
        {
            var subjects = new List<Subject>()
            {
                new Subject()
                {
                    Id = 1,
                    Name = "Gnębienie studentów",
                    Description = "Przedmiot powstał tylko i wyłącznie w celu gnębienia studentów. Polega na uprzykrzaniu życia studentom. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus.",
                    MajorId = 1,
                    TermId = 1
                },

                new Subject()
                {
                    Id = 2,
                    Name = "Gnębienie studentów",
                    Description = "Przedmiot powstał tylko i wyłącznie w celu gnębienia studentów. Polega na uprzykrzaniu życia studentom. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus.",
                    MajorId = 2,
                    TermId = 1
                },

                new Subject()
                {
                    Id = 3,
                    Name = "Bazy danych",
                    Description = "Bazy danych to bardzo przydatny przedmiot, tralalala, turururu, blablabla. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus. LUBIE PIEROGI RUSKIE",
                    MajorId = 1,
                    TermId = 4
                },

                new Subject()
                {
                    Id = 4,
                    Name = "Sieci komputerowe 1",
                    Description = "Sieci komputerowe 1 to przedmiot z którego egzamin, to zabójcze wydarzenie. Występują liczne ofiary śmiertelne po przeprowadzeniu takowego egzaminu. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus. ",
                    MajorId = 1,
                    TermId = 4
                },

                new Subject()
                {
                    Id = 5,
                    Name = "Sieci komputerowe 2",
                    Description = "Sieci komputerowe 2 to przedmiot który pokazuje wytrwałość. Nie wiem co ja już piszę XD! Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus. ",
                    MajorId = 1,
                    TermId = 5
                }
            };

            return subjects;
        }

        private List<SubjectGroup> GetSubjectGroups()
        {
            var subjectGroups = new List<SubjectGroup>()
            {
                new SubjectGroup()
                {
                    Id = 1,
                    Name = "L05",
                    GroupType = "Laboratorium",
                    SubjectId = 1,
                },

                new SubjectGroup()
                {
                    Id = 2,
                    Name = "L06",
                    GroupType = "Laboratorium",
                    SubjectId = 1,
                },

                new SubjectGroup()
                {
                    Id = 3,
                    Name = "L05",
                    GroupType = "Projekt",
                    SubjectId = 3,
                },

                new SubjectGroup()
                {
                    Id = 4,
                    Name = "L06",
                    GroupType = "Projekt",
                    SubjectId = 3,
                },

                new SubjectGroup()
                {
                    Id = 5,
                    Name = "L06",
                    GroupType = "Laboratorium",
                    SubjectId = 3,
                },

                new SubjectGroup()
                {
                    Id = 6,
                    Name = "L05",
                    GroupType = "Laboratorium",
                    SubjectId = 3,
                },

                new SubjectGroup()
                {
                    Id = 7,
                    Name = "L05",
                    GroupType = "Projekt",
                    SubjectId = 3,
                },
            };

            return subjectGroups;
        }

        private List<SubjectSubgroup> GetSubjectSubgroups()
        {
            var subjectSubgroups = new List<SubjectSubgroup>()
            {
                new SubjectSubgroup()
                {
                    Id = 1,
                    Name = "Kusal",
                    SubjectGroupId = 1
                },

                new SubjectSubgroup()
                {
                    Id = 2,
                    Name = "Kuszowski-Morawczyński",
                    SubjectGroupId = 1
                },

                new SubjectSubgroup()
                {
                    Id = 3,
                    Name = "Mazur",
                    SubjectGroupId = 1
                },
                new SubjectSubgroup()
                {
                    Id = 4,
                    Name = "Ziętek-Latawiec",
                    SubjectGroupId = 1
                },

                new SubjectSubgroup()
                {
                    Id = 5,
                    Name = "Kowalski",
                    SubjectGroupId = 2
                },

                new SubjectSubgroup()
                {
                    Id = 6,
                    Name = "Kowalski-Nowak",
                    SubjectGroupId = 4
                },

                new SubjectSubgroup()
                {
                    Id = 7,
                    Name = "Kaczmarski-Parówczak",
                    SubjectGroupId = 2
                }
            };

            return subjectSubgroups;
        }

        private List<ReportTopic> GetReportTopics()
        {
            var reportTopics = new List<ReportTopic>()
            {
                new ReportTopic()
                {
                    Id = 1,
                    Name = "Wprowadzenie do Oracle",
                    //Deadline = DateTime.Parse("20-04-2023 08:30:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 1
                },

                new ReportTopic()
                {
                    Id = 2,
                    Name = "Wprowadzenie do Oracle",
                    //Deadline = DateTime.Parse("22-04-2023 08:30:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 2
                },

                new ReportTopic()
                {
                    Id = 3,
                    Name = "Zaawansowane zapytania Oracle",
                    //Deadline = DateTime.Parse("30-04-2023 11:00:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 1
                },

                new ReportTopic()
                {
                    Id = 4,
                    Name = "Zaawansowane zapytania Oracle",
                    //Deadline = DateTime.Parse("29-04-2023 10:00:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 2
                },

                new ReportTopic()
                {
                    Id = 5,
                    Name = "Zajęcia nr 1 z gnębienia studentów",
                    //Deadline = DateTime.Parse("19-05-2023 07:30:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 1
                },

                new ReportTopic()
                {
                    Id = 6,
                    Name = "Zajęcia nr 1 z gnębienia studentów",
                    //Deadline = DateTime.Parse("17-04-2023 10:00:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 2
                },

                new ReportTopic()
                {
                    Id = 7,
                    Name = "Zajęcia nr 2 z gnębienia studentów",
                    //Deadline = DateTime.Parse("26-04-2023 04:00:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 1
                },

                new ReportTopic()
                {
                    Id = 8,
                    Name = "Zajęcia nr 2 z gnębienia studentów",
                    //Deadline = DateTime.Parse("28-04-2023 05:30:00 PM"),
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 2
                }

            };

            return reportTopics;
        }

        private List<StudentReport> GetStudentReports()
        {
            var studentReports = new List<StudentReport>()
            {
                new StudentReport()
                {
                    Id = 1,
                    //SentAt = DateTime.Parse("21-04-2023 05:30:00 PM"),
                    SentAt = DateTime.UtcNow,
                    Note = "Sprawko z przedmiotu \"Bazy danych\" z labów 1 \"Wprowadzanie do Oracle\", nadesłane przez Kusal",
                    ReportTopicId = 1,
                    SubjectSubgroupId = 1
                },

                new StudentReport()
                {
                    Id = 2,
                    //SentAt = DateTime.Parse("23-04-2023 06:00:00 PM"),
                    SentAt = DateTime.UtcNow,
                    Note = "Sprawko z przedmiotu \"Bazy danych\" z labów 1 \"Wprowadzanie do Oracle\", nadesłane przez Kuszowski-Morawczyński",
                    ReportTopicId = 1,
                    SubjectSubgroupId = 2
                },

                new StudentReport()
                {
                    Id = 3,
                    //SentAt = DateTime.Parse("22-04-2023 09:00:00 PM"),
                    SentAt = DateTime.UtcNow,
                    Note = "Sprawko z przedmiotu \"Gnębienie studentów\" z labów 1 \"Gnębienie studentów cz. 1\", nadesłane przez Mazur",
                    ReportTopicId = 5,
                    SubjectSubgroupId = 3
                },

                new StudentReport()
                {
                    Id = 4,
                    //SentAt = DateTime.Parse("26-04-2023 05:30:00 PM"),
                    SentAt = DateTime.UtcNow,
                    Note = "Sprawko z przedmiotu \"Gnębienie studentów\" z labów 1 \"Gnębienie studentów cz. 1\", nadesłane przez Kaczmarski-Parówczak",
                    ReportTopicId = 6,
                    SubjectSubgroupId = 7
                },

                new StudentReport()
                {
                    Id = 5,
                    //SentAt = DateTime.Parse("02-05-2023 05:30:00 PM"),
                    SentAt = DateTime.UtcNow,
                    Note = "Sprawko z przedmiotu \"Gnębienie studentów\" z labów 2 \"Gnębienie studentów cz. 2\", nadesłane przez Kusal",
                    ReportTopicId = 4,
                    SubjectSubgroupId = 6
                }

            };

            return studentReports;
        }

        private List<StudentReportFile> GetStudentReportFiles()
        {
            var studentReports = new List<StudentReportFile>()
            {
                new StudentReportFile()
                {
                    Id = 1,
                    StudentReportId = 1
                },

                new StudentReportFile()
                {
                    Id = 2,
                    StudentReportId = 2
                },

                new StudentReportFile()
                {
                    Id = 3,
                    StudentReportId = 3
                },

                new StudentReportFile()
                {
                    Id = 4,
                    StudentReportId = 4
                },

                new StudentReportFile()
                {
                    Id = 5,
                    StudentReportId = 5
                },
            };

            return studentReports;
        }

        private List<Student> GetStudents()
        {
            var students = new List<Student>();

            var student1 = new Student();
            student1.Id = 169571;
            student1.Name = "Jakub";
            student1.Surname = "Kusal";
            student1.Email = "169571@stud.prz.edu.pl";
            student1.Login = "169571";
            student1.Password = _passwordHasherStudent.HashPassword(student1, "Hasło12345");
            student1.IsDeleted = false;
            students.Add(student1);

            var student2 = new Student();
            student2.Id = 169572;
            student2.Name = "Mikołaj";
            student2.Surname = "Kuszowski";
            student2.Email = "169572@stud.prz.edu.pl";
            student2.Login = "169572";
            student2.Password = _passwordHasherStudent.HashPassword(student2,"Masło12345");
            student2.IsDeleted = false;
            students.Add(student2);

            var student3 = new Student();
            student3.Id = 169587;
            student3.Name = "Paweł";
            student3.Surname = "Morawczyński";
            student3.Email = "169587@stud.prz.edu.pl";
            student3.Login = "169587";
            student3.Password = _passwordHasherStudent.HashPassword(student3, "Hasło321456");
            student3.IsDeleted = false;
            students.Add(student3);

            var student4 = new Student();
            student4.Id = 169576;
            student4.Name = "Grzegorz";
            student4.Surname = "Latawiec";
            student4.Email = "169576@stud.prz.edu.pl";
            student4.Login = "169576";
            student4.Password = _passwordHasherStudent.HashPassword(student4, "Hasło12345678");
            student4.IsDeleted = false;
            students.Add(student4);

            var student5 = new Student();
            student5.Id = 171685;
            student5.Name = "Mateusz";
            student5.Surname = "Ziętek";
            student5.Email = "171685@stud.prz.edu.pl";
            student5.Login = "171685";
            student5.Password = _passwordHasherStudent.HashPassword(student5, "Masło456789");
            student5.IsDeleted = false;
            students.Add(student5);

            var student6 = new Student();
            student6.Id = 169584;
            student6.Name = "Mazur";
            student6.Surname = "Dominika";
            student6.Email = "169584@stud.prz.edu.pl";
            student6.Login = "169584";
            student6.Password = _passwordHasherStudent.HashPassword(student6, "Masło7891234");
            student6.IsDeleted = false;
            students.Add(student6);

            var student7 = new Student();
            student7.Id = 213742;
            student7.Name = "Janusz";
            student7.Surname = "Kaczmarski";
            student7.Email = "213742@stud.prz.edu.pl";
            student7.Login = "213742";
            student7.Password = _passwordHasherStudent.HashPassword(student7, "Hasełko9876");
            student7.IsDeleted = false;
            students.Add(student7);

            var student8 = new Student();
            student8.Id = 693141;
            student8.Name = "Sebastian";
            student8.Surname = "Parówczak";
            student8.Email = "693141@stud.prz.edu.pl";
            student8.Login = "693141";
            student8.Password = _passwordHasherStudent.HashPassword(student8, "qwertyuiop12345678");
            student8.IsDeleted = false;
            students.Add(student8);

            return students;
        }

        private List<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>();

            var teacher1 = new Teacher();
            teacher1.Id = 1;
            teacher1.Name = "Tomasz";
            teacher1.Surname = "Krzeszowski";
            teacher1.Email = "tkrzeszo@prz.edu.pl";
            teacher1.Login = "tkrzeszo";
            teacher1.Password = _passwordHasherTeacher.HashPassword(teacher1, "HasłoMasło98765");
            teacher1.Degree = "Doktor inżynier";
            teacher1.Position = "Profesor uczelni w grupie pracowników badawczo-dydaktycznych";
            teacher1.IsDeleted = false;
            teachers.Add(teacher1);

            var teacher2 = new Teacher();
            teacher2.Id = 2;
            teacher2.Name = "Piotr";
            teacher2.Surname = "Woźniak";
            teacher2.Email = "p.wozniak@prz.edu.pl ";
            teacher2.Login = "pwozniak";
            teacher2.Password = _passwordHasherTeacher.HashPassword(teacher2, "Masło420213769");
            teacher2.Degree = "Magister inżynier";
            teacher2.Position = "Asystent";
            teacher2.IsDeleted = false;
            teachers.Add(teacher2);

            return teachers;
        }


        private List<ReportComment> GetReportComments()
        {
            var reportComments = new List<ReportComment>()
            {
                new ReportComment()
                {
                    Id = 1,
                    Content = "Komentarz wystawiony przez studenta",
                    StudentId = 169571,
                    StudentReportId = 1
                },

                new ReportComment()
                {
                    Id = 2,
                    Content = "Komentarz taktyczny wystawiony przez prowadzącego",
                    TeacherId = 1,
                    StudentReportId = 1
                },

                new ReportComment()
                {
                    Id = 3,
                    Content = "Komentarz NIEtaktyczny wystawiony przez studenta taktycznego",
                    StudentId = 169572,
                    StudentReportId = 2
                },

            };

            return reportComments;
        }
    }
}
