using ConsultaAPICodeFirst.Context;
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
        }

        public ICollection<Medico> FindAll()
        {
            return ctx.Medico.ToList();
        }

        public Medico FindById(int id)
        {
            return ctx.Medico.Find(id);
        }

        public Medico Insert(Medico entity)
        {
            ctx.Medico.Add(entity);

            ctx.SaveChanges();

            return entity;
        }

        public void Update(Medico entity)
        {
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
