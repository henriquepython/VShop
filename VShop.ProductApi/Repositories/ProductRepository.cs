using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Context;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext context;

    public ProductRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Product>> GetAll()
    {
        return await context.Products.Include(c=> c.Category).ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        return await context.Products.Include(c => c.Category).Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product> Create(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> Delete(int id)
    {
        var product = await GetById(id);
        context.Products.Remove(product);
        context.SaveChanges();
        return product;
    }

}
