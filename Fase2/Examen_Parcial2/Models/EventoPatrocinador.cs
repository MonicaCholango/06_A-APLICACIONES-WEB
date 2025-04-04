namespace Examen_Parcial2.Models
{
    public class EventoPatrocinador
    {
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public int PatrocinadorId { get; set; }
        public Patrocinador Patrocinador { get; set; }

        public decimal MontoPatrocinio { get; set; }
    }
}
