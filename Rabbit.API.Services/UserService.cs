using AutoMapper;
using Rabbit.API.Infra;
using Rabbit.API.Infra.Repositorys;
using Rabbit.API.Models;
using Rabbit.API.Models.DataTransferObjects;
using Rabbit.API.Services.Exceptions;
using Rabbit.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rabbit.API.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRabbitMensagemService _rabbitMensagemService;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IRabbitMensagemService rabbitMensagemService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _rabbitMensagemService = rabbitMensagemService;
            _userRepository = userRepository;
        }




        public async Task<List<UserResponse>> GetAll()
        {
            var users = _userRepository.GetAll();
            var result = await Task.Run(() => _mapper.Map<List<UserResponse>>(users));
            return result;
        }

        private bool ValidateEmail(UserRequest request)
        {
            var regularExpression = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(regularExpression);
            var validatorEmail = regex.Match(request.Email);
            if (validatorEmail.Success)
            {
                return true;
            }

            return false;
        }

        private bool ExistUser(string email)
        {
            var exist = _userRepository.GetAll().Any(x => x.Email.ToLower().Equals(email.ToLower()));
            return exist;
        }
        public async Task InsertUser(UserRequest request)
        {

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Password))
                throw new NotFoundException("Os campos: email, nome e senha não podem estar vazios.");

            var validate = ValidateEmail(request);
            var exist = ExistUser(request.Email);

            if (!validate)
                throw new NotFoundException("O email está inválido, utilize um formato válido.");
            if (exist)
                throw new NotFoundException("O email já pertence a um outro usuário.");

            User novoUser = new User
            {
                Nome = request.Nome,
                Email = request.Email,
                Password = request.Password
            };

            await _userRepository.Insert(novoUser);
            SendEmail(novoUser);


        }

        public void SendEmail(User user)
        {
            RabbitMensagem mensagem = new RabbitMensagem()
            {
                Id = user.Id,
                UserName = user.Nome,
                Titulo = $"Bem vindo ao nosso aplicativo {user.Nome}!",
                Corpo = "Agradecemos pelo seu cadastro em nossa plataforma, fique a vontade para disponibilizar de nossos serviços!",
                EmailTo = user.Email,
            };

            _rabbitMensagemService.SendMensagem(mensagem);

        }

        public async Task Update(int id, UserRequest request)
        {
            var userUpdate = _userRepository.GetById(id);
            if (userUpdate == null)
                throw new NotFoundException($"Não existe usuário com o id: {id}, cadastrado.");

            userUpdate.Email = request.Email;
            userUpdate.Password = request.Password;
            userUpdate.Nome = request.Nome;

            await _userRepository.Update(userUpdate);

        }

        public async Task Delete(int id)
        {
            var userRemove = _userRepository.GetById(id);
            if (userRemove == null)
                throw new NotFoundException($"Não existe usuário com o id: {id}, cadastrado.");


            await _userRepository.Delete(userRemove);

        }

    }
}
