using POS.Application.Dtos.Pelicula_SalaCine.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface IStoredProcedureRepository
    {
        Task<int> ContarPeliculasPorFechaAsync(DateTime fechaPublicacion);
        Task<IEnumerable<PeliculaSalaResponseDto>> BuscarPeliculaPorNombreYSalaAsync(string nombre, int idSalaCine);
        Task<string> VerificarDisponibilidadSalaAsync(string nombreSala);
        Task RegistrarPeliculaSalaAsync(int idSalaCine, int idPelicula, DateTime fechaPublicacion, DateTime fechaFin);
    }
}
