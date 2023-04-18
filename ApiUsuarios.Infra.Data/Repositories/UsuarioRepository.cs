using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces.Repositories;
using ApiUsuarios.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Infra.Data.Repositories
{
    /// <summary>
    /// Classe para implementação do repositório de usuários
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Add(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Usuario.Add(usuario);
                dataContext.SaveChanges();
            }
        }

        public void Update(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(usuario).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
        }

        public Usuario? Get(string email)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Usuario
                    .Where(u => u.Email.Equals(email))
                    .FirstOrDefault();
            }
        }

        public Usuario? Get(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Usuario
                    .Where(u => u.Email.Equals(email) && u.Senha.Equals(senha))
                    .FirstOrDefault();
            }
        }
    }
}



