using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Controllers;

[ApiController]
[Route("[Controller]")]
public class PersonController : ControllerBase
{

    protected List<Person> persons = new List<Person>();

    [HttpGet]
    public List<Person> Hello()
    {
        //Person pessoa = new Person("Thiago", "123456", 35);
        //Contract newContract = new Contract("123", 5.66);
        //pessoa.contracts.Add(newContract);
        return persons;
    }

    [HttpPost()]
    public List<Person> Post(Person person)
    {
        persons.Add(person);
        return persons;
    }

    [HttpPut("{id}")]
    public string Update([FromRoute]int id, [FromBody]Person person)
    {
        return "dados do id " + id + " atualizados";
    }

    [HttpDelete("{id}")]
    public string Delete([FromRoute]int id)
    {
        return "dados do id " + id + " deletados";
    }

    /* private static readonly string[] Nomes = new[]
    {
        "THIAGO", "RIPARDO", "DE", "LIMA"
    };

    [HttpGet(Name="GetPerson")]
    public IEnumerable<Person> Get()
    {
        return Enumerable.Range(0,Nomes.Length).Select(index => new Person
        {
            Nome = Nomes[index]
        })
        .ToArray();
    } */
    
}