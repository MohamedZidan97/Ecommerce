using Microsoft.EntityFrameworkCore;
using Zain.Application.Contracts;
using Zain.Application.Features;

namespace Zain.Persistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // ➕ Add
        public async Task<GeneralResponse<T>> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return GeneralResponse<T>.SuccessResponse(entity, "Entity added successfully (pending save)");
            }
            catch (Exception ex)
            {
                return GeneralResponse<T>.FailResponse($"Error adding entity: {ex.Message}");
            }
        }

        // 🔍 GetAllById
        public async Task<GeneralResponse<T>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return GeneralResponse<T>.FailResponse("Entity not found");

                return GeneralResponse<T>.SuccessResponse(entity, "Entity retrieved successfully");
            }
            catch (Exception ex)
            {
                return GeneralResponse<T>.FailResponse($"Error retrieving entity: {ex.Message}");
            }
        }

        // 📋 GetAll
        public async Task<GeneralResponse<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                if (!entities.Any())
                    return GeneralResponse<IEnumerable<T>>.FailResponse("No entities found");

                return GeneralResponse<IEnumerable<T>>.SuccessResponse(entities, "Entities retrieved successfully");
            }
            catch (Exception ex)
            {
                return GeneralResponse<IEnumerable<T>>.FailResponse($"Error retrieving entities: {ex.Message}");
            }
        }


        // ✏️ Update
        public async Task<GeneralResponse<T>> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                return GeneralResponse<T>.SuccessResponse(entity, "Entity updated successfully (pending save)");
            }
            catch (Exception ex)
            {
                return GeneralResponse<T>.FailResponse($"Error updating entity: {ex.Message}");
            }
        }

        // ❌ Delete
        public async Task<GeneralResponse<T>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return GeneralResponse<T>.FailResponse("Entity not found");

                _dbSet.Remove(entity);
                return GeneralResponse<T>.SuccessResponse(entity, "Entity deleted successfully (pending save)");
            }
            catch (Exception ex)
            {
                return GeneralResponse<T>.FailResponse($"Error deleting entity: {ex.Message}");
            }
        }

    }
}
