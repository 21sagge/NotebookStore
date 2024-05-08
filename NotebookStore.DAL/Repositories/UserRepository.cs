// namespace NotebookStore.DAL;

// using Microsoft.EntityFrameworkCore;
// using NotebookStore.Entities;
// using NotebookStoreContext;

// public class UserRepository : IRepository<User>
// {
//   private readonly NotebookStoreContext _context;

//   public UserRepository(NotebookStoreContext context)
//   {
//     _context = context;
//   }

//   public async Task Create(User entity)
//   {
//     await _context.Users.AddAsync(entity);
//     await _context.SaveChangesAsync();
//   }
//   public async Task<IEnumerable<User>> Read()
//   {
//     return await _context.Users.ToListAsync();
//   }
//   public async Task<User?> Find(int? id)
//   {
//     return await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
//   }
//   public async Task Update(User entity)
//   {
//     _context.Users.Update(entity);
//     await _context.SaveChangesAsync();
//   }

//   public async Task Delete(int id)
//   {
//     var user = await _context.Users.FindAsync(id);
//     if (user == null) return;
//     _context.Users.Remove(user);
//     await _context.SaveChangesAsync();
//   }
//   public void Dispose()
//   {
//     _context.Dispose();
//   }
// }
