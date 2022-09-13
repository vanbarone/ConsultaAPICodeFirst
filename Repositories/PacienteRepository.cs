using ConsultaAPICodeFirst.Context;
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
            return ctx.Paciente.ToList();
        }

        public Paciente FindById(int id)
        {
            return ctx.Paciente.Find(id);
        }

        public Paciente Insert(Paciente entity)
        {
            ctx.Paciente.Add(entity);

            ctx.SaveChanges();

            return entity;
        }

        public void Update(Paciente entity)
        {
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
