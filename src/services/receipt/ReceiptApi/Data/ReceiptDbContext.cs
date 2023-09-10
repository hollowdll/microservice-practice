using Microsoft.EntityFrameworkCore;
using ReceiptApi.Models;

namespace ReceiptApi.Data;

public class ReceiptDbContext : DbContext
{
    public ReceiptDbContext(DbContextOptions<ReceiptDbContext> options) : base(options) {}

    public DbSet<Receipt> Receipts { get; set; } = null!;
}