using System;
using System.Collections.Generic;
using System.Threading;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Services
{
    // Real Subject
    public class DocumentRepository : IDocumentRepository
    {
        private readonly Dictionary<string, ConfidentialDocument> _database;

        public DocumentRepository()
        {
            Console.WriteLine("[Repository] Initializing connection with database...");
            Thread.Sleep(1000); // Simulating heavy connection (Lazy loading in Proxy will avoid this if not used)

            _database = new Dictionary<string, ConfidentialDocument>
            {
                ["DOC001"] = new ConfidentialDocument(
                    "DOC001",
                    "Financial Report Q4",
                    "Confidential content of the financial report... (10 MB)",
                    3
                ),
                ["DOC002"] = new ConfidentialDocument(
                    "DOC002",
                    "Market Strategy 2025",
                    "Confidential strategic plans... (50 MB)",
                    5
                ),
                ["DOC003"] = new ConfidentialDocument(
                    "DOC003",
                    "Employee Manual",
                    "Policies and procedures... (2 MB)",
                    1
                )
            };
        }

        public ConfidentialDocument GetDocument(string documentId, User user)
        {
            Console.WriteLine($"[Repository] Loading document {documentId} from database...");
            Thread.Sleep(500); // Simulating heavy operation

            if (_database.ContainsKey(documentId))
            {
                var doc = _database[documentId];
                Console.WriteLine($"[Repository] Document loaded: {doc.SizeInBytes / (1024 * 1024)} MB");
                return doc;
            }

            return null;
        }

        public void UpdateDocument(string documentId, string newContent, User user)
        {
            Console.WriteLine($"[Repository] Updating document {documentId} in database...");
            Thread.Sleep(300);

            if (_database.ContainsKey(documentId))
            {
                _database[documentId].Content = newContent;
            }
        }
    }
}
