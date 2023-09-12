using Rabbit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.API.Infra.Repositorys
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        Task Insert(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}
