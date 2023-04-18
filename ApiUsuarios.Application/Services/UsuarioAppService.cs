using ApiUsuarios.Application.Interfaces;
using ApiUsuarios.Application.Models;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        //atributos
        private readonly IUsuarioDomainService? _usuarioDomainService;
        private readonly IMapper? _mapper;

        //construtor para injeção de dependência
        public UsuarioAppService(IUsuarioDomainService? usuarioDomainService, IMapper? mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
        }

        public UsuarioModel CriarUsuario(CriarContaModel model)
        {
            var usuario = _mapper.Map<Usuario>(model);
            _usuarioDomainService.CriarUsuario(usuario);

            return _mapper.Map<UsuarioModel>(usuario);
        }

        public UsuarioModel Autenticar(LoginModel model)
        {
            var usuario = _usuarioDomainService.Autenticar(model.Email, model.Senha);
            return _mapper.Map<UsuarioModel>(usuario);
        }

        public UsuarioModel RecuperarSenha(RecuperarSenhaModel model)
        {
            var usuario = _usuarioDomainService.RecuperarSenha(model.Email);
            return _mapper.Map<UsuarioModel>(usuario);
        }
    }
}



