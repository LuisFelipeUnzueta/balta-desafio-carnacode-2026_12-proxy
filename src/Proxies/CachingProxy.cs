using System;
using System.Collections.Generic;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;

namespace DesignPatternChallenge.Proxies
{
    public class CachingProxy : IDocumentRepository
    {
        private readonly IDocumentRepository _innerRepository;
        private readonly Dictionary<string, ConfidentialDocument> _cache = new Dictionary<string, ConfidentialDocument>();

        public CachingProxy(IDocumentRepository innerRepository)
        {
            _innerRepository = innerRepository;
        }

        public ConfidentialDocument GetDocument(string documentId, User user)
        {
            if (_cache.ContainsKey(documentId))
            {
                Console.WriteLine($"[Caching Proxy] Document {documentId} retrieved from CACHE.");
                return _cache[documentId];
            }

            var document = _innerRepository.GetDocument(documentId, user);

            if (document != null)
            {
                _cache[documentId] = document;
                Console.WriteLine($"[Caching Proxy] Document {documentId} STORED in cache.");
            }

            return document;
        }

        public void UpdateDocument(string documentId, string newContent, User user)
        {
            _innerRepository.UpdateDocument(documentId, newContent, user);

            // Invalidate cache after update
            if (_cache.ContainsKey(documentId))
            {
                _cache.Remove(documentId);
                Console.WriteLine($"[Caching Proxy] Cache INVALIDATED for document {documentId} after update.");
            }
        }
    }
}
