using ApiUsuarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface para definição dos métodos de serviço do domínio
    /// </summary>
    public interface IUsuarioDomainService
    {
        /// <summary>
        /// Método para cadastro do usuário do sistema
        /// </summary>
        void CriarUsuario(Usuario usuario);

        /// <summary>
        /// Método para obter e autenticar um usuário
        /// </summary>
        Usuario Autenticar(string email, string senha);

        /// <summary>
        /// Método para recuperar e gerar uma nova senha para o usuário
        /// </summary>
        Usuario RecuperarSenha(string email);
    }
}



