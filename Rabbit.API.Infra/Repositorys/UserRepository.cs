using Rabbit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.API.Infra.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public User GetById(int id)
        {
            return _context.Usuarios.Where(u => u.Id.Equals(id)).FirstOrDefault();
        }

        public async Task Insert(User user)
        {
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();  
        }

        public async Task Update(User user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
