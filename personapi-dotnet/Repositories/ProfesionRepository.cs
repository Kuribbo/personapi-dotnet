using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
 public class ProfesionRepository : IProfesionRepository
 {
 private readonly PersonaDbContext _context;
 public ProfesionRepository(PersonaDbContext context)
 {
 _context = context;
 }

 public async Task<IEnumerable<Profesion>> GetAllAsync()
 {
 return await _context.Profesions.AsNoTracking().ToListAsync();
 }

 public async Task<Profesion?> GetByIdAsync(int id)
 {
 return await _context.Profesions.FindAsync(id);
 }

 public async Task<Profesion> AddAsync(Profesion profesion)
 {
 _context.Profesions.Add(profesion);
 await _context.SaveChangesAsync();
 return profesion;
 }

 public async Task UpdateAsync(Profesion profesion)
 {
 _context.Attach(profesion);
 _context.Entry(profesion).State = EntityState.Modified;
 await _context.SaveChangesAsync();
 }

 public async Task DeleteAsync(int id)
 {
 // Prevent deleting a profesion that has related estudios to avoid FK conflict
 var hasEstudios = await _context.Estudios.AnyAsync(e => e.IdProf == id);
 if (hasEstudios)
 {
 throw new InvalidOperationException("No se puede eliminar la profesión porque tiene estudios asociados. Elimina primero los estudios o reasigna su profesión.");
 }

 var existing = await _context.Profesions.FindAsync(id);
 if (existing == null) return;
 _context.Profesions.Remove(existing);
 await _context.SaveChangesAsync();
 }

 public async Task<bool> ExistsAsync(int id)
 {
 return await _context.Profesions.AnyAsync(p => p.Id == id);
 }
 }
}
