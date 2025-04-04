namespace Examen_Parcial2.Models
{
    public class EventoParticipante
    {
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }

        public bool Confirmado { get; set; }
    }
}
