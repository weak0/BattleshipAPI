using Microsoft.EntityFrameworkCore;

namespace BattleShipAPI.Entities
{
    public class BattleShipDbContext : DbContext
    {
        public BattleShipDbContext(DbContextOptions<BattleShipDbContext> options) : base(options) 
        {
            
        }

        public BattleShipDbContext()
        {
            
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameBoard> GameBoards { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Cordiantes> Cordiantes { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ship>()
                .HasMany(s => s.Cordinates)
                .WithOne(c => c.Ship)
                .HasForeignKey(c => c.ShipId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


