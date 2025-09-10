using System;

namespace MunicipalCitizenReportApp.Models
{
    public class IssueReport
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Location { get; set; }
        public required string Category { get; set; }
        public required string Description { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime ReportDate { get; set; }
    }
}