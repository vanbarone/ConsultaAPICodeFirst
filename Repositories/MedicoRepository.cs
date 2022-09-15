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
    public class MedicoRepository: IMedicoRepository
    {
        DataContext ctx;

        public MedicoRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Medico entity)
        {
            ctx.Medico.Remove(entity);

            ctx.SaveChanges();

            //Apaga tb o usuario
            UsuarioRepository repo = new UsuarioRepository(ctx);
            repo.Delete(entity.Usuario);
        }

        public ICollection<Medico> FindAll()
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            return ctx.Medico
                        .Include(e => e.Especialidade)
                        .Include(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                        .Include(c => c.Consultas).ThenInclude(p => p.Paciente).ThenInclude(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                        .ToList();
        }

        public Medico FindById(int id)
        {
            //include e theninclude são utilizados para carregar os respectivos objetos
            var obj = ctx.Medico
                            .Include(e => e.Especialidade)
                            .Include(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .Include(c => c.Consultas).ThenInclude(p => p.Paciente).ThenInclude(u => u.Usuario).ThenInclude(t => t.TipoUsuario)
                            .FirstOrDefault(m => m.Id == id);

            return obj;
        }

        public Medico Insert(Medico entity)
        {
            //Verifica se a especialidade existe
            IEspecialidadeRepository repo = new EspecialidadeRepository(ctx);

            if (repo.FindById(entity.IdEspecialidade) == null)
            {
                throw new ConstraintException("Especialidade não cadastrada");
            }

            //Verifica se o tipo de usuário existe
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            if (repoTipo.FindById(entity.Usuario.IdTipoUsuario) == null)
            {
                throw new ConstraintException("Tipo de Usuário não cadastrado");
            }


            //Salva no BD
            ctx.Medico.Add(entity);

            ctx.SaveChanges();

            entity = FindById(entity.Id);

            return entity;
        }

        public void Update(Medico entity)
        {
            //Verifica se a especialidade existe
            IEspecialidadeRepository repo = new EspecialidadeRepository(ctx);

            if (repo.FindById(entity.IdEspecialidade) == null)
            {
                throw new ConstraintException("Especialidade não cadastrada");
            }

            //Verifica se o tipo de usuário existe
            ITipoUsuarioRepository repoTipo = new TipoUsuarioRepository(ctx);

            if (repoTipo.FindById(entity.Usuario.IdTipoUsuario) == null)
            {
                throw new ConstraintException("Tipo de Usuário não cadastrado");
            }


            //Salva no BD
            ctx.Medico.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Medico entity)
        {
            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
