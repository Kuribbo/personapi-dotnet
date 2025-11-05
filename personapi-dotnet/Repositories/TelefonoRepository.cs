using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
 public class TelefonoRepository : ITelefonoRepository
 {
 private readonly PersonaDbContext _context;

 public TelefonoRepository(PersonaDbContext context)
 {
 _context = context;
 }

 public async Task<IEnumerable<Telefono>> GetAllAsync()
 {
 return await _context.Telefonos.Include(t => t.DuenoNavigation).AsNoTracking().ToListAsync();
 }

 public async Task<Telefono?> GetByIdAsync(string num)
 {
 return await _context.Telefonos.FindAsync(num);
 }

 public async Task<Telefono> AddAsync(Telefono telefono)
 {
 _context.Telefonos.Add(telefono);
 await _context.SaveChangesAsync();
 return telefono;
 }

 public async Task UpdateAsync(Telefono telefono)
 {
 _context.Entry(telefono).State = EntityState.Modified;
 await _context.SaveChangesAsync();
 }

 public async Task DeleteAsync(string num)
 {
 var existing = await _context.Telefonos.FindAsync(num);
 if (existing == null) return;
 _context.Telefonos.Remove(existing);
 await _context.SaveChangesAsync();
 }

 public async Task<bool> ExistsAsync(string num)
 {
 return await _context.Telefonos.AnyAsync(t => t.Num == num);
 }
 }
}
