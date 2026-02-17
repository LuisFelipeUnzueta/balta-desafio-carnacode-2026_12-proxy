using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;
using DesignPatternChallenge.Services;

namespace DesignPatternChallenge.Proxies
{
    // Lazy Proxy: Delay the create of real object until it is really needed
    public class LazyDocumentRepositoryProxy : IDocumentRepository
    {
        private DocumentRepository _realRepository;

        public ConfidentialDocument GetDocument(string documentId, User user)
        {
            return GetRepository().GetDocument(documentId, user);
        }

        public void UpdateDocument(string documentId, string newContent, User user)
        {
            GetRepository().UpdateDocument(documentId, newContent, user);
        }

        private IDocumentRepository GetRepository()
        {
            if (_realRepository == null)
            {
                Console.WriteLine("[Lazy Proxy] Creating instance of real repository for the first time...");
                _realRepository = new DocumentRepository();
            }
            return _realRepository;
        }
    }
}
