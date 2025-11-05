using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
 public interface IPersonaRepository
 {
 Task<IEnumerable<Persona>> GetAllAsync();
 Task<Persona?> GetByIdAsync(int id);
 Task<Persona> AddAsync(Persona persona);
 Task UpdateAsync(Persona persona);
 Task DeleteAsync(int id);
 Task<bool> ExistsAsync(int id);
 }
}   
