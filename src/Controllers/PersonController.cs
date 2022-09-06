using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Persistence;

namespace src.Controllers;

[ApiController]
[Route("[Controller]")]
public class PersonController : ControllerBase
{
    private DatabaseContext _context { get; set; }

    public PersonController(DatabaseContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public ActionResult<List<Person>> Get()
    {
        var result = _context.persons.Include(c => c.contracts).ToList();

        if(!result.Any())
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpPost()]
    public ActionResult<Person> Post(Person person)
    {
        _context.persons.Add(person);
        _context.SaveChanges();

        return Created("Criado", person);
    }

    [HttpPut("{id}")]
    public ActionResult<Object> Update(
        [FromRoute]int id, 
        [FromBody]Person person
    )
    {
        try
        {
            _context.persons.Update(person);
            _context.SaveChanges();    
        }
        catch (System.Exception)
        {
            
            return BadRequest(new {
                message = "Houve erro ao enviar a solicitação do "+ id + " atualizados",
                status = HttpStatusCode.OK
            });
            
        }
        

        return Ok(new {
            message = "dados do id " + id + " atualizados",
            status = HttpStatusCode.OK
        });
    }

    [HttpDelete("{id}")]
    public ActionResult<Object> Delete([FromRoute]int id)
    {
        var result = _context.persons.SingleOrDefault(p => p.id == id);

        if(result == null)
        {
            return BadRequest(new {
                message = "Conteúdo inexistente, solicitação invalida",
                status = HttpStatusCode.BadRequest
            });
        }

        _context.persons.Remove(result);
        _context.SaveChanges();

        return Ok(new {
                message = "Deletado pessoa com Id: " + id,
                status = HttpStatusCode.OK
            });
    }
    
}