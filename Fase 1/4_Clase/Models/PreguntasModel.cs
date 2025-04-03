namespace _4_Clase.Models
{
    public class PreguntasModel
    {
        public int Id { get; set; }
        public string Enunciado { get; set; }
        public int TipoPreguntaModelId { get; set; }
        public TipoPreguntaModel TipoPreguntaModel { get; set; }    
        public DateOnly CreateAdd { get; set; }
        public DateOnly UpdateAdd { get; set; } 
    }
}
