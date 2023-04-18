using ApiUsuarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface para repositório de dados de usuário
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Inserir um usuário no banco de dados
        /// </summary>
        void Add(Usuario usuario);

        /// <summary>
        /// Atualizar um usuário no banco de dados
        /// </summary>
        void Update(Usuario usuario);

        /// <summary>
        /// Consultar 1 usuário através do email
        /// </summary>
        Usuario? Get(string email);

        /// <summary>
        /// Consultar 1 usuário através do email e da senha
        /// </summary>
        Usuario? Get(string email, string senha);
    }
}



