using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Notifies
    {

        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notifications { get; set; }

        //method valid to string
        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notifications.Add(new Notifies
                {
                    Mensagem = "Campo Obrigatório"
                    , NomePropriedade = nomePropriedade
                });

                return true;
            }

            return false;
        }

         public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {
            if(valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notifications.Add(new Notifies
                {
                    Mensagem = "Campo Obrigatório"
                    , NomePropriedade = nomePropriedade
                });

                return true;
            }

            return false;
        }

    }
}