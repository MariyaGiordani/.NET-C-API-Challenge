using APIChallenge.Models;
using APIChallenge.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace APIChallenge.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User Find(long id);
        void Remove(long id);
        void Update(User user);
        User Login(string userName, string password);
        User FindByUser(User user);
        long FindByUserLong(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionProvider _context;
        public UserRepository(DbConnectionProvider ctx)
        {
            _context = ctx;
        }
        public void Add(User user)
        {
            user.Password = user.HashStringPassword(user.Password);
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User Find(long id)
        {
            return _context.User.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
        }

        public void Remove(long id)
        {
            var entity = _context.User.First(p => p.Id == id);
            _context.User.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            return _context.User
                  .Where(p => p.Email == email && p.Password == password)
                  .FirstOrDefault();
        }

        public User FindByUser(User user)
        {
            IQueryable<User> result = _context.User.AsQueryable();
            return result.Where(u => u.Email == user.Email).FirstOrDefault();
        }

        public long FindByUserLong(User user)
        {
            IQueryable<User> result = _context.User.AsQueryable();
            return result.Where(u => u.Email == user.Email).Select(u => u.Id).FirstOrDefault();
        }
    }
}
