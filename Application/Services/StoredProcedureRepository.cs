using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Application.Dtos.Pelicula_SalaCine.Response;
using POS.Application.Interfaces;
using POS.Infraestructure.Persistences.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    internal class StoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly POSCineContext _context;

        public StoredProcedureRepository(POSCineContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PeliculaSalaResponseDto>> BuscarPeliculaPorNombreYSalaAsync(string nombre, int idSalaCine)
        {
            using (var connection = _context.Database.GetDbConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new Exception("Connection is not valid");

                using (var command = new SqlCommand("sp_BuscarPeliculaPorNombreYSala", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@IdSalaCine", idSalaCine);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var result = new List<PeliculaSalaResponseDto>();
                        while (await reader.ReadAsync())
                        {
                            result.Add(new PeliculaSalaResponseDto
                            {
                                Id_pelicula_sala = reader.GetInt32(0),
                                Id_sala_cine = reader.GetInt32(1),
                                Id_pelicula = reader.GetInt32(2),
                                //Nombre = reader.GetString(3),
                                Fecha_publicacion = reader.GetDateTime(4),
                                Fecha_fin = reader.GetDateTime(5)
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public async Task<int> ContarPeliculasPorFechaAsync(DateTime fechaPublicacion)
        {
            using (var connection = _context.Database.GetDbConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new Exception("Connection is not valid");

                using (var command = new SqlCommand("sp_ContarPeliculasPorFecha", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);

                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    await connection.CloseAsync();

                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task RegistrarPeliculaSalaAsync(int idSalaCine, int idPelicula, DateTime fechaPublicacion, DateTime fechaFin)
        {
            using (var connection = _context.Database.GetDbConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new Exception("Connection is not valid");

                using (var command = new SqlCommand("RegistrarPeliculaSala", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdSalaCine", idSalaCine);
                    command.Parameters.AddWithValue("@IdPelicula", idPelicula);
                    command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<string> VerificarDisponibilidadSalaAsync(string nombreSala)
        {
            using (var connection = _context.Database.GetDbConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new Exception("Connection is not valid");

                using (var command = new SqlCommand("sp_VerificarDisponibilidadSala", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreSala", nombreSala);

                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    await connection.CloseAsync();

                    return result?.ToString() ?? "Error al verificar la disponibilidad.";
                }
            }
        }
    }
}
