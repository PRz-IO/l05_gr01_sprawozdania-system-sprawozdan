using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Enums;

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
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Subject.Any())
                {
                    _dbContext.Subject.AddRange(GetSubjects());
                    _dbContext.SaveChanges();
                }
                
                if (!_dbContext.Teacher.Any())
                {
                    _dbContext.Teacher.AddRange(GetTeachers());
                    _dbContext.SaveChanges();
                }
                
                if (!_dbContext.SubjectGroup.Any())
                {
                    _dbContext.SubjectGroup.AddRange(GetSubjectGroups());
                    _dbContext.SaveChanges();
                }
                
                if (!_dbContext.ReportTopic.Any())
                {
                    _dbContext.ReportTopic.AddRange(GetReportTopics());
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.SubjectSubgroup.Any())
                {
                    _dbContext.SubjectSubgroup.AddRange(GetSubjectSubgroups());
                    _dbContext.SaveChanges();
                }
                
                if (!_dbContext.StudentReport.Any())
                {
                    _dbContext.StudentReport.AddRange(GetStudentReports());
                    _dbContext.SaveChanges();
                }
                
                if (!_dbContext.ReportComment.Any())
                {
                    _dbContext.ReportComment.AddRange(GetReportComments());
                    _dbContext.SaveChanges();
                }
            }
        }


        private Admin GetAdmin()
        {
            var admin = new Admin()
            {
                //Id = 1,
                Login = "admin"
            };

            admin.Password = _passwordHasher.HashPassword(admin, "admin123");
            return admin;
        }

        private List<Major> GetMajors()
        {
            var majors = new List<Major>()
            {
                new Major()
                {
                    //Id = 1,
                    Name = "Informatyka",
                    StartedAt = DateOnly.Parse("01-10-2021"),
                    MajorCode = "2EF-DI"
                },

                new Major()
                {
                    //Id = 2,
                    Name = "Automatyka i robotyka",
                    StartedAt = DateOnly.Parse("01-10-2021"),
                    MajorCode = "2EA-DI"
                },

                new Major()
                {
                    //Id = 3,
                    Name = "Elektromobilność",
                    StartedAt = DateOnly.Parse("01-10-2020"),
                    MajorCode = "3EM-DI"
                }
            };

            return majors;
        }

     

        private List<Subject> GetSubjects()
        {
            var subjects = new List<Subject>()
            {
                new Subject()
                {
                    //Id = 1,
                    Name = "Gnębienie studentów 1",
                    Description = "Przedmiot powstał tylko i wyłącznie w celu gnębienia studentów. Polega na uprzykrzaniu życia studentom. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus.",
                    MajorId = 1,
                    Term = 1
                },

                new Subject()
                {
                    //Id = 2,
                    Name = "Gnębienie studentów 2",
                    Description = "Przedmiot powstał tylko i wyłącznie w celu gnębienia studentów. Polega na uprzykrzaniu życia studentom. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus.",
                    MajorId = 2,
                    Term = 2
                },

                new Subject()
                {
                    //Id = 3,
                    Name = "Bazy danych",
                    Description = "Bazy danych to bardzo przydatny przedmiot, tralalala, turururu, blablabla. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus. LUBIE PIEROGI RUSKIE",
                    MajorId = 1,
                    Term = 3
                },

                new Subject()
                {
                    //Id = 4,
                    Name = "Sieci komputerowe 1",
                    Description = "Sieci komputerowe 1 to przedmiot z którego egzamin, to zabójcze wydarzenie. Występują liczne ofiary śmiertelne po przeprowadzeniu takowego egzaminu. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus. ",
                    MajorId = 1,
                    Term = 4
                },

                new Subject()
                {
                    //Id = 5,
                    Name = "Sieci komputerowe 2",
                    Description = "Sieci komputerowe 2 to przedmiot który pokazuje wytrwałość. Nie wiem co ja już piszę XD! Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam eros lorem, pulvinar eu elit porta, vestibulum ultricies risus. Vestibulum aliquam accumsan sapien, a aliquet tellus consectetur vitae. In blandit velit a nunc tempus pellentesque. Maecenas aliquet consectetur dui sed sodales. Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus. ",
                    MajorId = 1,
                    Term = 5
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
                    //Id = 1,
                    Name = "L05",
                    GroupType = "Laboratorium",
                    SubjectId = 3, // Bazy danych
                    TeacherId = 1
                },

                new SubjectGroup()
                {
                    //Id = 2,
                    Name = "L06",
                    GroupType = "Laboratorium",
                    SubjectId = 3, // Bazy danych
                    TeacherId = 1
                },

                new SubjectGroup()
                {
                    //Id = 3,
                    Name = "L05",
                    GroupType = "Projekt",
                    SubjectId = 4, // Sieci komputerowe 1
                    TeacherId = 2
                },

                new SubjectGroup()
                { 
                    //Id = 4,
                    Name = "L06",
                    GroupType = "Projekt",
                    SubjectId = 4, // Sieci komputerowe 1
                    TeacherId = 2
                },

                new SubjectGroup()
                {
                    //Id = 5,
                    Name = "L06",
                    GroupType = "Laboratorium",
                    SubjectId = 1, // Gnębienie studentów 1
                    TeacherId = 3
                },

                new SubjectGroup()
                {
                    //Id = 6,
                    Name = "L05",
                    GroupType = "Laboratorium",
                    SubjectId = 1, // Gnębienie studentów 1
                    TeacherId = 1
                },

                new SubjectGroup()
                {
                    //Id = 7,
                    Name = "L05",
                    GroupType = "Projekt",
                    SubjectId = 5, // Sieci komputerowe 2 
                    TeacherId = 2
                },
            };

            return subjectGroups;
        }

        private List<SubjectSubgroup> GetSubjectSubgroups()
        {
            var kusal = new Student()
            {
                Id = 169571,
                Name = "Jakub",
                Surname = "Kusal",
                Email = "169571@stud.prz.edu.pl",
                Login = "169571"
            };
            kusal.Password = _passwordHasherStudent.HashPassword(kusal, "Hasło123");

            var kuszowski = new Student()
            {
                Id = 169572,
                Name = "Mikołaj",
                Surname = "Kuszowski",
                Email = "169572@stud.prz.edu.pl",
                Login = "169572"
            };
            kuszowski.Password = _passwordHasherStudent.HashPassword(kuszowski, "Hasło123");

            var morawczynski = new Student()
            {
                Id = 169587,
                Name = "Paweł",
                Surname = "Morawczyński",
                Email = "169587@stud.prz.edu.pl",
                Login = "169587"
            };
            morawczynski.Password = _passwordHasherStudent.HashPassword(morawczynski, "Hasło123");

            var latawiec = new Student()
            { 
                Id = 169576,
                Name = "Grzegorz",
                Surname = "Latawiec",
                Email = "169576@stud.prz.edu.pl",
                Login = "169576"
            };
            latawiec.Password = _passwordHasherStudent.HashPassword(latawiec, "Hasło123");

            var zietek = new Student()
            {
                Id = 171685,
                Name = "Mateusz",
                Surname = "Ziętek",
                Email = "171685@stud.prz.edu.pl",
                Login = "171685"
            };
            zietek.Password = _passwordHasherStudent.HashPassword(zietek, "Hasło123");

            var mazur = new Student()
            {
                Id = 169584,
                Name = "Mazur",
                Surname = "Dominika",
                Email = "169584@stud.prz.edu.pl",
                Login = "169584"
            };
            mazur.Password = _passwordHasherStudent.HashPassword(mazur, "Hasło123");

            var kaczmarski = new Student()
            {
                Id = 213742,
                Name = "Janusz",
                Surname = "Kaczmarski",
                Email = "213742@stud.prz.edu.pl",
                Login = "213742"
            };
            kaczmarski.Password = _passwordHasherStudent.HashPassword(kaczmarski, "Hasło123");

            var parowczak = new Student()
            {
                Id = 693141,
                Name = "Sebastian",
                Surname = "Parówczak",
                Email = "693141@stud.prz.edu.pl",
                Login = "693141"
            };
            parowczak.Password = _passwordHasherStudent.HashPassword(parowczak, "Hasło123");

            var kowalski = new Student()
            {
                Id = 420691,
                Name = "Bożydar",
                Surname = "Kowalski",
                Email = "420691@stud.prz.edu.pl",
                Login = "420691"
            };
            kowalski.Password = _passwordHasherStudent.HashPassword(kowalski, "Hasło123");

            var nowak = new Student()
            {
                Id = 666666,
                Name = "Romuald",
                Surname = "Nowak",
                Email = "666666@stud.prz.edu.pl",
                Login = "666666"
            };
            nowak.Password = _passwordHasherStudent.HashPassword(nowak, "Hasło123");


            var subjectSubgroups = new List<SubjectSubgroup>()
            {
                new SubjectSubgroup()
                {
                    //Id = 1,
                    Name = "Kusal",
                    SubjectGroupId = 1,
                    IsIndividual = true,
                    Students = new List<Student>()
                    {
                        kusal
                    }
                    
                },

                new SubjectSubgroup()
                {
                    //Id = 2,
                    Name = "Kuszowski-Morawczyński",
                    SubjectGroupId = 1,
                    IsIndividual = false,
                    Students = new List<Student>()
                    {
                        kuszowski,
                        morawczynski
                    }
                },

                new SubjectSubgroup()
                {
                    //Id = 3,
                    Name = "Mazur",
                    SubjectGroupId = 1,
                    IsIndividual = true,
                    Students = new List<Student>()
                    {
                        mazur
                    }
                },
                new SubjectSubgroup()
                {
                    //Id = 4,
                    Name = "Ziętek-Latawiec",
                    SubjectGroupId = 1,
                    IsIndividual = false,
                    Students = new List<Student>()
                    {
                        zietek,
                        latawiec
                    }
                },

                new SubjectSubgroup()
                {
                    //Id = 5,
                    Name = "Kowalski",
                    SubjectGroupId = 2,
                    IsIndividual = true,
                    Students = new List<Student>()
                    {
                        kowalski
                    }
                },

                new SubjectSubgroup()
                {
                    //Id = 6,
                    Name = "Kowalski-Nowak",
                    SubjectGroupId = 4,
                    IsIndividual = false,
                    Students = new List<Student>()
                    {
                        kowalski,
                        nowak
                    }
                },

                new SubjectSubgroup()
                {
                    //Id = 7,
                    Name = "Kaczmarski-Parówczak",
                    SubjectGroupId = 2,
                    IsIndividual = false,
                    Students = new List<Student>()
                    {
                        kaczmarski,
                        parowczak
                    }
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
                    //Id = 1,
                    Name = "Wprowadzenie do Oracle",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 1
                },

                new ReportTopic()
                {
                    //Id = 2,
                    Name = "Funkcje i procedury w Oracle DataBase",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 2
                },

                new ReportTopic()
                {
                    //Id = 3,
                    Name = "Funkcje i procedury w Oracle DataBase",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 1
                },

                new ReportTopic()
                {
                    //Id = 4,
                    Name = "Adresacja IP",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 3
                },

                new ReportTopic()
                {
                    //Id = 5,
                    Name = "Adresacja IP",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 4
                },

                new ReportTopic()
                {
                    //Id = 6,
                    Name = "Zajęcia nr 1 z gnębienia studentów",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 5
                },

                new ReportTopic()
                {
                    //Id = 7,
                    Name = "Zajęcia nr 1 z gnębienia studentów",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 6
                },

                new ReportTopic()
                {
                    //Id = 8,
                    Name = "Sieciowe fiku-miku (przedmiot sieci komputerowe 2)",
                    Deadline = DateTime.UtcNow,
                    SubjectGroupId = 7
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
                    //Id = 1,
                    SentAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    StudentNote = "Sprawko z przedmiotu \"Bazy danych\" z labów 1 \"Wprowadzanie do Oracle\", nadesłane przez Kusal",
                    TeacherNote = "Widać wielkie starania w pańskiej pracy, jednakże jest ona nie na temat. Brak zaliczenia i możliwości poprawy. Pozdrawiam",
                    ReportTopicId = 1,
                    SubjectSubgroupId = 1,
                    Mark = MarkEnum.Failed
                },

                new StudentReport()
                {
                    //Id = 2,
                    SentAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    StudentNote = "Sprawko z przedmiotu \"Bazy danych\" z labów 1 \"Wprowadzanie do Oracle\", nadesłane przez Kuszowski-Morawczyński",
                    ReportTopicId = 1,
                    SubjectSubgroupId = 2,
                    Mark = MarkEnum.Passed
                },

                new StudentReport()
                {
                    //Id = 3,
                    SentAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    StudentNote = "Sprawko z przedmiotu \"Gnębienie studentów\" z labów 1 \"Gnębienie studentów cz. 1\", nadesłane przez Mazur",
                    ReportTopicId = 5,
                    SubjectSubgroupId = 3,
                    Mark = MarkEnum.Three
                },

                new StudentReport()
                {
                    //Id = 4,
                    SentAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    StudentNote = "Sprawko z przedmiotu \"Gnębienie studentów\" z labów 1 \"Gnębienie studentów cz. 1\", nadesłane przez Kaczmarski-Parówczak",
                    ReportTopicId = 6,
                    SubjectSubgroupId = 7
                },

                new StudentReport()
                {
                    //Id = 5,
                    SentAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    StudentNote = "Sprawko z przedmiotu \"Gnębienie studentów\" z labów 2 \"Gnębienie studentów cz. 2\", nadesłane przez Kusal",
                    ReportTopicId = 4,
                    SubjectSubgroupId = 6
                }
            };
            
            return studentReports;
        }
        

        private List<Student> GetStudents()
        {
            var students = new List<Student>();

            var turkot = new Student();
            turkot.Id = 777777;
            turkot.Name = "Genowefa";
            turkot.Surname = "Turkot";
            turkot.Email = "777777@stud.prz.edu.pl";
            turkot.Login = "777777";
            turkot.Password = _passwordHasherStudent.HashPassword(turkot, "Hasło123");
            turkot.IsDeleted = false;
            students.Add(turkot);
            return students;
        }

        private List<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>();

            var teacher1 = new Teacher();
            //teacher1.Id = 1;
            teacher1.Name = "Tomasz";
            teacher1.Surname = "Krzeszowski";
            teacher1.Email = "tkrzeszo@prz.edu.pl";
            teacher1.Login = "tkrzeszo";
            teacher1.Password = _passwordHasherTeacher.HashPassword(teacher1, "Hasło123");
            teacher1.Degree = "Doktor inżynier";
            teacher1.Position = "Profesor uczelni w grupie pracowników badawczo-dydaktycznych";
            teachers.Add(teacher1);


            var teacher2 = new Teacher();
            //teacher2.Id = 2;
            teacher2.Name = "Piotr";
            teacher2.Surname = "Woźniak";
            teacher2.Email = "p.wozniak@prz.edu.pl";
            teacher2.Login = "pwozniak";
            teacher2.Password = _passwordHasherTeacher.HashPassword(teacher2, "Hasło123");
            teacher2.Degree = "Magister inżynier";
            teacher2.Position = "Asystent";
            teachers.Add(teacher2);

            var teacher3 = new Teacher();
            //teacher1.Id = 3;
            teacher3.Name = "Aneta";
            teacher3.Surname = "Rogoz";
            teacher3.Email = "a.rogoz@prz.edu.pl";
            teacher3.Login = "arogoz";
            teacher3.Password = _passwordHasherTeacher.HashPassword(teacher1, "Hasło123");
            teacher3.Degree = "Magister";
            teacher3.Position = "Asystent w grupie pracowników badawczo-dydaktycznych";
            teachers.Add(teacher3);

            return teachers;
        }


        private List<ReportComment> GetReportComments()
        {
            var reportComments = new List<ReportComment>()
            {
                new ReportComment()
                {
                    //Id = 1,
                    Content = "Komentarz wystawiony przez studenta",
                    StudentId = 169571,
                    StudentReportId = 1
                },

                new ReportComment()
                {
                    //Id = 2,
                    Content = "Komentarz taktyczny wystawiony przez prowadzącego",
                    TeacherId = 1,
                    StudentReportId = 1
                },

                new ReportComment()
                {
                    //Id = 3,
                    Content = "Komentarz NIEtaktyczny wystawiony przez studenta taktycznego",
                    StudentId = 169572,
                    StudentReportId = 2
                },

            };

            return reportComments;
        }
    }
}