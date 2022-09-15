using ConsultaAPICodeFirst.Context;
using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class UsuarioRepository 
    {
        DataContext ctx;

        public UsuarioRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Usuario entity)
        {
            ctx.Usuario.Remove(entity);

            ctx.SaveChanges();
        }
    }
}
