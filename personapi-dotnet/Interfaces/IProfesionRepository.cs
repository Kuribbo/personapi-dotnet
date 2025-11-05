using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
 public interface IProfesionRepository
 {
 Task<IEnumerable<Profesion>> GetAllAsync();
 Task<Profesion?> GetByIdAsync(int id);
 Task<Profesion> AddAsync(Profesion profesion);
 Task UpdateAsync(Profesion profesion);
 Task DeleteAsync(int id);
 Task<bool> ExistsAsync(int id);
 }
}
