
using DotnetCoding.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
    [ApiController]
    public class InstallController : ControllerBase
    {
        private readonly DbContextClass _dbContext;

        public InstallController(DbContextClass dbContext){
_dbContext = dbContext;
        }

        [HttpPost("[action]")]
        public void MigrateChanges()
        {
            try
            {
                _dbContext.Database.EnsureCreated();
               // _dbContext.Database.Migrate();
            }
            catch
            {
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (_dbContext.Status.Any())
                    return BadRequest("Seed Data already created");                    

                DataSeeder.CreateSeedData(_dbContext);
            }

            return Ok();
        }
    }