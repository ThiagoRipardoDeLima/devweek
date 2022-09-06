namespace src.Models;

public class Person
{

    public Person()
    {
        this.Nome = "template";
        this.Idade = 0;
        this.contracts = new List<Contract>();
    }

    public Person(string Nome, string Cpf, int Idade)
    {
        this.Nome = Nome;
        this.Cpf = Cpf;
        this.Idade = Idade;
        this.contracts = new List<Contract>();
    }

    public int id { get; set; }
    public string Nome { get; set; }    
    public int Idade { get; set; }
    public string? Cpf { get; set; }
    public bool Ativo { get; set; }
    public List<Contract> contracts {get; set;}
}
