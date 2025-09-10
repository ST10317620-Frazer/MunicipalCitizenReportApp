using System;

namespace MunicipalCitizenReportApp.Services
{
    public interface IEncouragementMessageService
    {
        string GetRandomMessage();
    }

    public class EncouragementMessageService : IEncouragementMessageService
    {
        private readonly string[] _messages = new[]
        {
            "Great job reporting issues to improve our community!",
            "Your input helps us serve you better!",
            "Thank you for being an active citizen!"
        };

        public string GetRandomMessage()
        {
            Random rand = new Random();
            return _messages[rand.Next(_messages.Length)];
        }
    }
}