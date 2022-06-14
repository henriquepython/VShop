using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext context;

    public CategoryRepository(AppDbContext context)
    {
       this.context = context;
    }
    public async Task<IEnumerable<Category>> GetAll()
    {
        return await context.Categories.ToListAsync();
    }
    
    public async Task<IEnumerable<Category>> GetCatagoriesProducts()
    {
        return await context.Categories.Include(c => c.Products).ToListAsync();
    }

    public async Task<Category> GetById(int id)
    {
        return await  context.Categories.Where(c=> c.CategoryId == id).FirstOrDefaultAsync();
    }

    public async Task<Category> Create(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        context.Entry(category).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Delete(int id)
    {
        var category = await GetById(id);
        context.Categories.Remove(category);
        context.SaveChanges();
        return category;
    }

}
