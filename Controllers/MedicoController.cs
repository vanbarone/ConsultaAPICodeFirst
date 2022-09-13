using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConsultaAPICodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        IMedicoRepository repo;

        public MedicoController(IMedicoRepository _repository)
        {
            repo = _repository;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            try
            {
                var retorno = repo.FindAll();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Inserir(Medico entity)
        {
            try
            {
                var retorno = repo.Insert(entity);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico entity)
        {
            try
            {
                //verifica se o Id passado é o mesmo id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

                repo.Update(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        [HttpPatch("{id}")]
        public IActionResult AlterarPatch(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch == null)
                    return BadRequest(new { message = "Objeto não passado" });

                //verifica se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

                //Efetua alteração parcial
                repo.UpdatePartial(patch, obj);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //verificar se existe no BD
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Message = "Médico não cadastrado" });

                //Efetua alteração
                repo.Delete(obj);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }
    }
}
