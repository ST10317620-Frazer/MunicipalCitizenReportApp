using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MunicipalCitizenReportApp.ViewModels
{
    public class ReportIssueViewModel
    {
         [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty; 
        public IFormFile? Attachment { get; set; } 

        public string? EncouragementMessage { get; set; }
    }
}