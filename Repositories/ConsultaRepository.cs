using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class ConsultaRepository: IConsultaRepository
    {
        DataContext ctx;

        public ConsultaRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Consulta entity)
        {
            ctx.Consulta.Remove(entity);

            ctx.SaveChanges();
        }

        public ICollection<Consulta> FindAll()
        {
            return ctx.Consulta
                            .Include(m => m.Medico).ThenInclude(u => u.Usuario)
                            .Include(m => m.Medico).ThenInclude(e => e.Especialidade)
                            .Include(p => p.Paciente).ThenInclude(u => u.Usuario)
                            .ToList();
        }

        public Consulta FindById(int id)
        {
            return ctx.Consulta.Find(id);
        }

        public Consulta Insert(Consulta entity)
        {
            ctx.Consulta.Add(entity);

            ctx.SaveChanges();

            return entity;
        }

        public void Update(Consulta entity)
        {
            ctx.Consulta.Update(entity);

            ctx.SaveChanges();
        }

        public void UpdatePartial(JsonPatchDocument patch, Consulta entity)
        {
            patch.ApplyTo(entity);

            ctx.Entry(entity).State = EntityState.Modified;

            ctx.SaveChanges();
        }
    }
}
