using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiUsuarios.Application.Models;

namespace ApiUsuarios.Application.Interfaces
{
    /// <summary>
    /// Interface para métodos da camada de aplicação
    /// </summary>
    public interface IUsuarioAppService
    {
        /// <summary>
        /// Método para executar a criação da conta do usuário
        /// </summary>
        UsuarioModel CriarUsuario(CriarContaModel model);

        /// <summary>
        /// Método para executar a autenticação do usuário
        /// </summary>
        UsuarioModel Autenticar(LoginModel model);

        /// <summary>
        /// Método para recuperar a senha do usuário
        /// </summary>
        UsuarioModel RecuperarSenha(RecuperarSenhaModel model);

    }
}
