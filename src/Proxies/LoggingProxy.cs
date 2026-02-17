using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Proxies
{
    public class LoggingProxy : IDocumentRepository
    {
        private readonly IDocumentRepository _innerRepository;

        public LoggingProxy(IDocumentRepository innerRepository)
        {
            _innerRepository = innerRepository;
        }

        public ConfidentialDocument GetDocument(string documentId, User user)
        {
            Log($"User {user.Username} requested access to document {documentId}");
            return _innerRepository.GetDocument(documentId, user);
        }

        public void UpdateDocument(string documentId, string newContent, User user)
        {
            Log($"User {user.Username} requested update of document {documentId}");
            _innerRepository.UpdateDocument(documentId, newContent, user);
        }

        private void Log(string message)
        {
            Console.WriteLine($"[Audit Log] [{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
