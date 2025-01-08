namespace POS.Application.Dtos.Pelicula_SalaCine.Response
{
    public class PeliculaSalaResponseDto
    {
        public int Id_pelicula_sala { get; set; }
        public int Id_sala_cine { get; set; }
        public int Id_pelicula { get; set;}
        public DateTime Fecha_publicacion { get; set; }
        public DateTime Fecha_fin { get; set; }
        public int State { get; set; }
        public string? StateCategory { get; set; }
        public DateTime AuditCreateDate { get; set; }
    }
}
