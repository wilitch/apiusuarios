using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces.Producers;
using ApiUsuarios.Domain.Interfaces.Repositories;
using ApiUsuarios.Domain.Interfaces.Services;
using ApiUsuarios.Domain.Models;
using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributos
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IMessageQueueProducer? _messageQueueProducer;

        //método construtor utilizado para inicializar o atributo
        public UsuarioDomainService(IUsuarioRepository? usuarioRepository, IMessageQueueProducer? messageQueueProducer)
        {
            _usuarioRepository = usuarioRepository;
            _messageQueueProducer = messageQueueProducer;
        }

        public void CriarUsuario(Usuario usuario)
        {
            //verificar se o email informado já existe no banco de dados
            if (_usuarioRepository.Get(usuario.Email) != null)
                throw new ArgumentException("O email informado já está cadastrado, tente outro.");

            //criptografar a senha do usuário
            usuario.Senha = EncriptarSenha(usuario.Senha);

            //gravar o usuário no banco de dados
            _usuarioRepository.Add(usuario);
        }

        public Usuario Autenticar(string email, string senha)
        {
            //criptografar a senha do usuário
            senha = EncriptarSenha(senha);

            //procurar o usuário através do email e da senha
            var usuario = _usuarioRepository.Get(email, senha);

            //verificar se o usuário foi encontrado
            if (usuario != null)
                return usuario;
            else
                throw new ArgumentException("Acesso negado. Usuário inválido.");
        }

        public Usuario RecuperarSenha(string email)
        {
            //procurar o usuário através do email
            var usuario = _usuarioRepository.Get(email);

            //verificar se o usuário foi encontrado
            if (usuario != null)
            {
                //gerando uma nova senha para o usuário
                var novaSenha = new Faker().Internet.Password(8);

                //criando o conteudo do email de recuperação de senha
                var assunto = "Recuperação de Senha - API Usuários";
                var corpo = @$"
                    <p>Olá {usuario.Nome},</p>
                    <p>Uma nova senha foi gerada com sucesso.</p>
                    <p>Acesse o sistema usando a senha: <strong>{novaSenha}</strong></p>
                    <p>Att,</p>
                    <p>Equipe COTI Informática</p>
                ";

                //dados que serão enviados para a mensageria
                var model = new MessageModel
                {
                    EmailDest = usuario.Email,
                    Assunto = assunto,
                    Conteudo = corpo
                };

                //enviar para a fila da mensageria
                _messageQueueProducer.Add(JsonConvert.SerializeObject(model));

                //atualizando a senha do usuário no banco de dados
                usuario.Senha = EncriptarSenha(novaSenha);
                _usuarioRepository.Update(usuario);

                return usuario;
            }
            else
                throw new ArgumentException("Usuário não encontrado.");
        }

        /// <summary>
        /// Método para criptografar a senha do usuário
        /// </summary>
        private string EncriptarSenha(string senha)
        {
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(hash);
        }
    }
}



