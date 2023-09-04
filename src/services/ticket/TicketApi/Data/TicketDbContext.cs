using Microsoft.EntityFrameworkCore;
using TicketApi.Models;

namespace TicketApi.Data;

public class TicketDbContext : DbContext
{
    public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) {}

    public DbSet<Ticket> Tickets { get; set; } = null!;
}