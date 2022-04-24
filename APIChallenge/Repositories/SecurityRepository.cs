using APIChallenge.Models;
using APIChallenge.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIChallenge.Repositories
{
    public interface ISecurityRepository
    {
        void Add(Security security);
        IEnumerable<Security> GetAll();
        byte[] Find(long id);
        void Remove(long id);
        void Update(Security security);
    }

    public class SecurityRepository : ISecurityRepository
    {
        private readonly DbConnectionProvider _context;
        public SecurityRepository(DbConnectionProvider ctx)
        {
            _context = ctx;
        }

        public void Add(Security security)
        {
            _context.Security.Add(security);
            _context.SaveChanges();
        }

        public byte[] Find(long id)
        {
            return _context.Security.Where(p => p.Id == id)
                .Select(s => s.SaltPassword)
                .FirstOrDefault();
        }

        public IEnumerable<Security> GetAll()
        {
            return _context.Security.ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.Security.First(p => p.Id == id);
            _context.Security.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Security security)
        {
            _context.Security.Update(security);
            _context.SaveChanges();
        }
    }
}
