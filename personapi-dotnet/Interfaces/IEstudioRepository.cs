using System.Collections.Generic;
using System.Threading.Tasks;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
 public interface IEstudioRepository
 {
 Task<IEnumerable<Estudio>> GetAllAsync();
 Task<Estudio?> GetByIdAsync(int idProf, int? ccPer = null);
 Task<Estudio> AddAsync(Estudio estudio);
 Task UpdateAsync(Estudio estudio);
 Task DeleteAsync(int idProf, int? ccPer = null);
 Task<bool> ExistsAsync(int idProf, int? ccPer = null);
 }
}
