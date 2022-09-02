namespace src.Models;

public class Contract
{
    public Contract()
    {
        this.DataCriacao = DateTime.Now;
        this.Valor = 0;
        this.TokenId = "0000";
    }

    public Contract(string tokeid, double value)
    {
        this.TokenId = tokeid;
        this.Valor = value;
        this.DataCriacao = DateTime.Now;
    }
    
    public DateTime DataCriacao { get; set; }
    public string TokenId { get; set; }
    public double Valor { get; set; }
    
}
