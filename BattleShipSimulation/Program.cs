using BattleShipAPI.Configuration;
using BattleShipAPI.Entities;
using BattleShipSimulation;
using Microsoft.EntityFrameworkCore;

using (var dbContext = new BattleShipDbContext(new DbContextOptionsBuilder<BattleShipDbContext>()
    .UseSqlServer("Server = MACIEK\\SQLEXPRESS; Database = BattleShipDb; Trusted_Connection = True; TrustServerCertificate = true;")
    .Options)){

    var config = new GameConfig();
    Simulation testSimulation = new (dbContext, "test1", "test2", config);
    testSimulation.StartSimulation();
    Console.ReadLine();
}
