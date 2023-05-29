namespace SystemSprawozdan.Shared.Dto;

public class StudentReportFileGetDto
{
    public int Id { get; set; }
    public int? StudentReportId { get; set; }
    public string ContentType { get; set; }
    public string OriginalFileName { get; set; }
    public string RandomizedFileName { get; set; }
    
}