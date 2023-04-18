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
    /// Mapeamento de models para entities
    /// </summary>
    public class ModelToEntityMap : Profile
    {
        //construtor -> ctor + 2x[tab]
        public ModelToEntityMap()
        {
            CreateMap<CriarContaModel, Usuario>()
                .AfterMap((src, dest) =>
                {
                    dest.IdUsuario = Guid.NewGuid();
                    dest.DataHoraCriacao = DateTime.Now;
                });
        }
    }
}
