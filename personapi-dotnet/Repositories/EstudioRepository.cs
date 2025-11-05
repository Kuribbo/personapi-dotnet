using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
 public class EstudioRepository : IEstudioRepository
 {
 private readonly PersonaDbContext _context;

 public EstudioRepository(PersonaDbContext context)
 {
 _context = context;
 }

 public async Task<IEnumerable<Estudio>> GetAllAsync()
 {
 return await _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation).AsNoTracking().ToListAsync();
 }

 public async Task<Estudio?> GetByIdAsync(int idProf, int? ccPer = null)
 {
 if (ccPer.HasValue)
 {
 return await _context.Estudios
 .Include(e => e.CcPerNavigation)
 .Include(e => e.IdProfNavigation)
 .AsNoTracking()
 .FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer.Value);
 }

 return await _context.Estudios
 .Include(e => e.CcPerNavigation)
 .Include(e => e.IdProfNavigation)
 .AsNoTracking()
 .FirstOrDefaultAsync(e => e.IdProf == idProf);
 }

 public async Task<Estudio> AddAsync(Estudio estudio)
 {
 _context.Estudios.Add(estudio);
 await _context.SaveChangesAsync();
 return estudio;
 }

 public async Task UpdateAsync(Estudio estudio)
 {
 // Attach then mark modified to handle disconnected updates
 _context.Attach(estudio);
 _context.Entry(estudio).State = EntityState.Modified;
 await _context.SaveChangesAsync();
 }

 public async Task DeleteAsync(int idProf, int? ccPer = null)
 {
 Estudio? existing;
 if (ccPer.HasValue)
 {
 existing = await _context.Estudios.FirstOrDefaultAsync(e => e.IdProf == idProf && e.CcPer == ccPer.Value);
 }
 else
 {
 existing = await _context.Estudios.FirstOrDefaultAsync(e => e.IdProf == idProf);
 }
 if (existing == null) return;
 _context.Estudios.Remove(existing);
 await _context.SaveChangesAsync();
 }

 public async Task<bool> ExistsAsync(int idProf, int? ccPer = null)
 {
 if (ccPer.HasValue)
 return await _context.Estudios.AnyAsync(e => e.IdProf == idProf && e.CcPer == ccPer.Value);
 return await _context.Estudios.AnyAsync(e => e.IdProf == idProf);
 }
 }
}
