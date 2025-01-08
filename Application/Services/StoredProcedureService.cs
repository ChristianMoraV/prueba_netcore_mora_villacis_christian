using POS.Application.Dtos.Pelicula_SalaCine.Response;
using POS.Application.Interfaces;

namespace POS.Application.Services
{
    public class StoredProcedureService : IStoredProcedureService
    {
        private readonly IStoredProcedureRepository _repository;

        public StoredProcedureService(IStoredProcedureRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PeliculaSalaResponseDto>> BuscarPeliculaPorNombreYSalaAsync(string nombre, int idSalaCine)
        {
            return await _repository.BuscarPeliculaPorNombreYSalaAsync(nombre, idSalaCine);
        }

        public async Task<int> ContarPeliculasPorFechaAsync(DateTime fechaPublicacion)
        {
            return await _repository.ContarPeliculasPorFechaAsync(fechaPublicacion);
        }

        public async Task RegistrarPeliculaSalaAsync(int idSalaCine, int idPelicula, DateTime fechaPublicacion, DateTime fechaFin)
        {
            await _repository.RegistrarPeliculaSalaAsync(idSalaCine, idPelicula, fechaPublicacion, fechaFin);
        }

        public async Task<string> VerificarDisponibilidadSalaAsync(string nombreSala)
        {
            return await _repository.VerificarDisponibilidadSalaAsync(nombreSala);
        }
    }
}
