using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Proxies
{
    public class SecurityProxy : IDocumentRepository
    {
        private readonly IDocumentRepository _innerRepository;

        public SecurityProxy(IDocumentRepository innerRepository)
        {
            _innerRepository = innerRepository;
        }

        public ConfidentialDocument GetDocument(string documentId, User user)
        {
            // The SecurityProxy needs to first get the document to check the SecurityLevel, 
            // or it can delegate to the inner repository and filter the result.
            // To be efficient, it could ask for only the metadata, but for the challenge 
            // we will load and check.

            var document = _innerRepository.GetDocument(documentId, user);

            if (document != null && user.ClearanceLevel < document.SecurityLevel)
            {
                Console.WriteLine($"[Security Proxy] ❌ ACCESS DENIED: User '{user.Username}' (Level {user.ClearanceLevel}) tried to access document '{document.Title}' (Req {document.SecurityLevel})");
                return null;
            }

            return document;
        }

        public void UpdateDocument(string documentId, string newContent, User user)
        {
            // Check permission before updating
            var document = _innerRepository.GetDocument(documentId, user);

            if (document != null && user.ClearanceLevel < document.SecurityLevel)
            {
                Console.WriteLine($"[Security Proxy] ❌ UPDATE REJECTED: User '{user.Username}' does not have sufficient level.");
                return;
            }

            _innerRepository.UpdateDocument(documentId, newContent, user);
        }
    }
}
