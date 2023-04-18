using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiUsuarios.Application.Models;
using ApiUsuarios.Domain.Entities;
using AutoMapper;

namespace ApiUsuarios.Application.Mappings
{
    /// <summary>
    /// Mapeamento de entities para models
    /// </summary>
    public class EntityToModelMap : Profile
    {
        //construtor -> ctor + 2x[tab]
        public EntityToModelMap()
        {
            CreateMap<Usuario, UsuarioModel>();
        }
    }
}
