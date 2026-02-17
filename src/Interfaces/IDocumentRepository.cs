using System.Collections.Generic;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Interfaces
{
    public interface IDocumentRepository
    {
        ConfidentialDocument GetDocument(string documentId, User user);
        void UpdateDocument(string documentId, string newContent, User user);
    }
}
