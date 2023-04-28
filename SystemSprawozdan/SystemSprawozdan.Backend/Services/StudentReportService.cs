﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportService
    {
        void SendStudentReport(SendStudentReportDto sendStudentReportDto);
        void EditStudentReport(int studentReportId, EditStudentReportDto editStudentReportDto);
    }

    public class StudentReportService : IStudentReportService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public StudentReportService(ApiDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public void SendStudentReport(SendStudentReportDto sendStudentReportDto)
        {
            var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            

            var subjectGroup = _dbContext
                .SubjectGroup.FirstOrDefault(subjectGroup =>
                    subjectGroup.reportTopics.Any(reportTopic =>
                        reportTopic.Id == sendStudentReportDto.ReportTopicId
                    )
                );

            var subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subjectSubgroup =>
                 subjectSubgroup.SubjectGroup.Id == subjectGroup.Id &&
                 subjectSubgroup.Students.Any(student => student.Id == loginUserId)
            );
            
            string? noteToSend;
            if (sendStudentReportDto.Note != null)
            {
                noteToSend = DateTime.Now.ToString() + ":\n" + sendStudentReportDto.Note;
            }
            else
            {
                noteToSend = null;
            }

            var newStudentReport = new StudentReport()
            {

                SentAt = DateTime.UtcNow,
                Note = noteToSend,
                ReportTopicId = sendStudentReportDto.ReportTopicId,
                SubjectSubgroupId = subjectSubgroup.Id
                
            };
            _dbContext.StudentReport.Add(newStudentReport);
            _dbContext.SaveChanges();


            var formFile = sendStudentReportDto.File;
            if (formFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                formFile.CopyToAsync(memoryStream);
            
                var studentReportFile = new StudentReportFile()
                {
                    StudentReportId = newStudentReport.Id,
                    File = memoryStream.ToArray()
                };
            
                _dbContext.StudentReportFile.Add(studentReportFile);
                _dbContext.SaveChanges();
            }
        }



        public void EditStudentReport(int studentReportId, EditStudentReportDto editStudentReportDto)
        {
            var reportToEdit = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);
            string previousComment = reportToEdit.Note;
            string commentToInsert;
            var currentDateTime = DateTime.Now.ToString();
            
            if(editStudentReportDto.ReportCommentFromStudent != null)
            {
                if (previousComment != null)
                {
                    commentToInsert = previousComment + "\n\n" + currentDateTime + ":\n" + editStudentReportDto.ReportCommentFromStudent;
                    reportToEdit.Note = commentToInsert;

                }
                else
                {
                    commentToInsert = currentDateTime + ":\n" + editStudentReportDto.ReportCommentFromStudent;
                    reportToEdit.Note = commentToInsert;
                }
            }


            var formFile = editStudentReportDto.OptionalFile;
            if (formFile != null )
            {
                if (formFile.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    formFile.CopyToAsync(memoryStream);

                    var studentReportFile = new StudentReportFile()
                    {
                        StudentReportId = reportToEdit.Id,
                        File = memoryStream.ToArray()
                    };

                    _dbContext.StudentReportFile.Add(studentReportFile);
                }
            }
            

            _dbContext.SaveChanges();
        }


    }

}

            

