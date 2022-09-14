using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Exceptions;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class PacienteRepository: IPacienteRepository
    {
        DataContext ctx;

        public PacienteRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Paciente entity)
        {
            ctx.Paciente.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Paciente> FindAll()
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            return ctx.Paciente
                        .Include(u => u.Usuario)
                        .Include(c => c.Consultas).ThenInclude(m => m.Medico).ThenInclude(u => u.Usuario)
                        .ToList();
        }

        public Paciente FindById(int id)
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            var obj = ctx.Paciente
                            .Include(u => u.Usuario)
                            .Include(c => c.Consultas).ThenInclude(m => m.Medico).ThenInclude(u => u.Usuario)
                            .FirstOrDefault(p => p.Id == id);

            return obj;
        }

        public Paciente Insert(Paciente entity)
        {
            //Verifica se o tipo de usuário existe
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            if (repoTipo.FindById(entity.Usuario.IdTipoUsuario) == null)
            {
                throw new ConstraintException("Tipo de Usuário não cadastrado");
            }


            //Salva no BD
            ctx.Paciente.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Paciente entity)
        {
            //Verifica se o tipo de usuário existe
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            if (repoTipo.FindById(entity.Usuario.IdTipoUsuario) == null)
            {
                throw new ConstraintException("Tipo de Usuário não cadastrado");
            }


            //Salva no BD
            ctx.Paciente.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Paciente entity)
        {
            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
