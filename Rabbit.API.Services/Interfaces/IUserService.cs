using Rabbit.API.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task InsertUser(UserRequest request);
        Task Update(int id, UserRequest request);
        Task Delete(int id);
    }
}
