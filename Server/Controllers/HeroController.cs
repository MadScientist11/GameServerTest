using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Services;
using SharedLibrary;
using SharedLibrary.Requests;

namespace Server.Controllers;


[ApiController]
[Route("[controller]")]
public class HeroController : ControllerBase
{
    private IHeroService _heroService;
    private GameDbContext _dbContext;

    public HeroController(IHeroService heroService, GameDbContext dbContext)
    {
        _dbContext = dbContext;
        _heroService = heroService;
    }
    [HttpGet("{id}")]
    public Hero Get([FromRoute] int id)
    {
        var player = new Hero()
        {
            Id = id,
        };
        _heroService.DoSomething();
        return player;
    }
    
    [HttpPost]
    public Hero Post(CreateHeroRequest request)
    {
        var userId = int.Parse(User.FindFirst("id").Value);
        var user = _dbContext.Users
            .Include(u=>u.Heroes).First(u => u.Id == userId);
        
        
        var hero = new Hero()
        {
            Name = request.Name,
            User = user
        };

        _dbContext.Add(hero);
        _dbContext.SaveChanges();
        
        Console.WriteLine("Has been added");
        return hero;
    }
}