using MunicipalCitizenReportApp.Models;
using MunicipalCitizenReportApp.Services;
using MunicipalCitizenReportApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MunicipalCitizenReportApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEncouragementMessageService _messageService;
        private readonly List<IssueReport> _issueReports;

        public HomeController(IEncouragementMessageService messageService)
        {
            _messageService = messageService;
            _issueReports = new List<IssueReport>();
        }

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            return View();
        }

        public IActionResult ReportIssue()
        {
            var model = new ReportIssueViewModel
            {
                EncouragementMessage = _messageService.GetRandomMessage()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportIssue(ReportIssueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.EncouragementMessage = _messageService.GetRandomMessage();
                return View(model);
            }

            string? attachmentPath = null;
            if (model.Attachment != null && model.Attachment.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsDir);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Attachment.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Attachment.CopyToAsync(stream);
                }
                attachmentPath = $"/uploads/{fileName}";
            }

            var issue = new IssueReport
            {
                Id = _issueReports.Count + 1,
                Title = model.Title, // Set Title from view model
                Location = model.Location,
                Category = model.Category,
                Description = model.Description,
                AttachmentPath = attachmentPath,
                ReportDate = DateTime.Now
            };
            _issueReports.Add(issue);

            TempData["SuccessMessage"] = "Issue reported successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}