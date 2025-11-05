using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
 public class PersonaRepository : IPersonaRepository
 {
 private readonly PersonaDbContext _context;

 public PersonaRepository(PersonaDbContext context)
 {
 _context = context;
 }

 public async Task<IEnumerable<Persona>> GetAllAsync()
 {
 return await _context.Personas.AsNoTracking().ToListAsync();
 }

 public async Task<Persona?> GetByIdAsync(int id)
 {
 return await _context.Personas.FindAsync(id);
 }

 public async Task<Persona> AddAsync(Persona persona)
 {
 _context.Personas.Add(persona);
 await _context.SaveChangesAsync();
 return persona;
 }

 public async Task UpdateAsync(Persona persona)
 {
 _context.Entry(persona).State = EntityState.Modified;
 await _context.SaveChangesAsync();
 }

 public async Task DeleteAsync(int id)
 {
 var existing = await _context.Personas.FindAsync(id);
 if (existing == null) return;
 _context.Personas.Remove(existing);
 await _context.SaveChangesAsync();
 }

 public async Task<bool> ExistsAsync(int id)
 {
 return await _context.Personas.AnyAsync(p => p.Cc == id);
 }
 } 
}
