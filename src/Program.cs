using System;
using DesignPatternChallenge.Interfaces;
using DesignPatternChallenge.Models;
using DesignPatternChallenge.Proxies;

namespace DesignPatternChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Confidential Documents System with Proxy Pattern ===\n");

            // Configuration of the Proxy Chain (Cross-cutting Concerns)
            // Order: Logging -> Security -> Caching -> Lazy Loading -> Real Subject

            IDocumentRepository repository = new LazyDocumentRepositoryProxy();
            repository = new CachingProxy(repository);
            repository = new SecurityProxy(repository);
            repository = new LoggingProxy(repository);

            var manager = new User("joao.silva", 5);
            var employee = new User("maria.santos", 2);

            Console.WriteLine("--- Scenario 1: Manager accessing high-level document (DOC002 - Level 5) ---");
            // Note: The real Repository will be created here (Lazy Loading)
            var doc1 = repository.GetDocument("DOC002", manager);
            if (doc1 != null) Console.WriteLine($"✅ Document viewed: {doc1.Title}");

            Console.WriteLine("\n--- Scenario 2: Employee trying to access the same document (DOC002) ---");
            var doc2 = repository.GetDocument("DOC002", employee);
            if (doc2 == null) Console.WriteLine("❌ Visualization not allowed.");

            Console.WriteLine("\n--- Scenario 3: Manager accessing again (Should use Caching Proxy) ---");
            // Note: Should not call the real repository
            var doc3 = repository.GetDocument("DOC002", manager);
            if (doc3 != null) Console.WriteLine($"✅ Document viewed via cache: {doc3.Title}");

            Console.WriteLine("\n--- Scenario 4: Employee accessing allowed document (DOC003 - Level 1) ---");
            var doc4 = repository.GetDocument("DOC003", employee);
            if (doc4 != null) Console.WriteLine($"✅ Document viewed: {doc4.Title}");

            Console.WriteLine("\n--- Scenario 5: Document update and cache invalidation ---");
            repository.UpdateDocument("DOC002", "New revised strategic content.", manager);

            Console.WriteLine("\n--- Scenario 6: Access after update (Should reload from database to cache) ---");
            var doc5 = repository.GetDocument("DOC002", manager);
            if (doc5 != null) Console.WriteLine($"✅ Updated document viewed: {doc5.Content}");

            Console.WriteLine("\n=== CHALLENGE CONCLUSION ===");
            Console.WriteLine("✔ Responsabilidades separadas (SRP)");
            Console.WriteLine("✔ Controle de acesso transparente via SecurityProxy");
            Console.WriteLine("✔ Auditoria centralizada via LoggingProxy");
            Console.WriteLine("✔ Performance otimizada via CachingProxy");
            Console.WriteLine("✔ Inicialização otimizada via LazyDocumentRepositoryProxy");
        }
    }
}
