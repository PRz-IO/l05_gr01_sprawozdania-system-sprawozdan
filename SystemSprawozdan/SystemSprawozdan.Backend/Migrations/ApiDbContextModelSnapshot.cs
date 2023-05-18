﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SystemSprawozdan.Backend.Data;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("StudentSubjectSubgroup", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("integer");

                    b.Property<int>("SubjectSubgroupId")
                        .HasColumnType("integer");

                    b.HasKey("StudentsId", "SubjectSubgroupId");

                    b.HasIndex("SubjectSubgroupId");

                    b.ToTable("StudentSubjectSubgroup");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Major", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("MajorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("StartedAt")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Major");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.ReportComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentReportId")
                        .HasColumnType("integer");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentReportId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ReportComment");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.ReportTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SubjectGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubjectGroupId");

                    b.ToTable("ReportTopic");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Mark")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("ReportTopicId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SubjectSubgroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("ToCheck")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ReportTopicId");

                    b.HasIndex("SubjectSubgroupId");

                    b.ToTable("StudentReport");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReportFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("StoredFileName")
                        .HasColumnType("text");

                    b.Property<int?>("StudentReportId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudentReportId");

                    b.ToTable("StudentReportFile");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MajorId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TermId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MajorId");

                    b.HasIndex("TermId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("GroupType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SubjectGroup");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectSubgroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsIndividual")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("SubjectGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubjectGroupId");

                    b.ToTable("SubjectSubgroup");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("StartedAt")
                        .HasColumnType("date");

                    b.Property<int>("TermNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Term");
                });

            modelBuilder.Entity("StudentSubjectSubgroup", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectSubgroup", null)
                        .WithMany()
                        .HasForeignKey("SubjectSubgroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.ReportComment", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Student", "Student")
                        .WithMany("ReportComments")
                        .HasForeignKey("StudentId");

                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReport", "StudentReport")
                        .WithMany("ReportComments")
                        .HasForeignKey("StudentReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Teacher", "Teacher")
                        .WithMany("ReportComments")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Student");

                    b.Navigation("StudentReport");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.ReportTopic", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectGroup", "SubjectGroup")
                        .WithMany("reportTopics")
                        .HasForeignKey("SubjectGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubjectGroup");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReport", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.ReportTopic", "ReportTopic")
                        .WithMany("StudentReports")
                        .HasForeignKey("ReportTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectSubgroup", "SubjectSubgroup")
                        .WithMany("StudentReports")
                        .HasForeignKey("SubjectSubgroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReportTopic");

                    b.Navigation("SubjectSubgroup");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReportFile", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReport", "StudentReport")
                        .WithMany("studentReportFiles")
                        .HasForeignKey("StudentReportId");

                    b.Navigation("StudentReport");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Subject", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Major", "Major")
                        .WithMany("Subjects")
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Term", "Term")
                        .WithMany("Subjects")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("Term");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectGroup", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Subject", "Subject")
                        .WithMany("SubjectGroups")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.Teacher", "Teacher")
                        .WithMany("SubjectGroups")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectSubgroup", b =>
                {
                    b.HasOne("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectGroup", "SubjectGroup")
                        .WithMany("subjectSubgroups")
                        .HasForeignKey("SubjectGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubjectGroup");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Major", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.ReportTopic", b =>
                {
                    b.Navigation("StudentReports");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Student", b =>
                {
                    b.Navigation("ReportComments");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.StudentReport", b =>
                {
                    b.Navigation("ReportComments");

                    b.Navigation("studentReportFiles");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Subject", b =>
                {
                    b.Navigation("SubjectGroups");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectGroup", b =>
                {
                    b.Navigation("reportTopics");

                    b.Navigation("subjectSubgroups");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.SubjectSubgroup", b =>
                {
                    b.Navigation("StudentReports");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Teacher", b =>
                {
                    b.Navigation("ReportComments");

                    b.Navigation("SubjectGroups");
                });

            modelBuilder.Entity("SystemSprawozdan.Backend.Data.Models.DbModels.Term", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
