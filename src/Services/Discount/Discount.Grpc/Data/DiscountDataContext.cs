using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountDataContext(DbContextOptions<DiscountDataContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Coupon> Coupones { get; set; } = default!;
}
