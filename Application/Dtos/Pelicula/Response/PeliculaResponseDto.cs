namespace POS.Application.Dtos.Pelicula.Response
{
    public class PeliculaResponseDto
    {
        public int Id_pelicula { get; set; }
        public string? Nombre { get; set; }
        public int Duracion { get; set; }
        public int State { get; set; }
        public string? StateCategory { get; set; }
        public DateTime? AuditCreateDate { get; set; }
    }
}
