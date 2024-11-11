using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using olimpia.Models;

namespace olimpia.Controllers
{
    [Route("player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayer)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = createPlayer.Name,
                Age = createPlayer.Age,
                Weight = createPlayer.Weight,
                Height = createPlayer.Height,
                CreatedTime = DateTime.Now,
            };
            if (player != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public ActionResult<Player> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Players.ToList());
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetById(Guid id)
        {
            using (var context = new OlimpiaContext()) {
                var player = context.Players.FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Player> Put(Guid Id,UpdatePlayerDto updatePlayerDto)
        {
            using (var context = new OlimpiaContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(player => player.Id == Id);
                if (existingPlayer != null) 
                {
                    existingPlayer.Name = updatePlayerDto.Name;
                    existingPlayer.Age = updatePlayerDto.Age;
                    existingPlayer.Height = updatePlayerDto.Height;
                    existingPlayer.Weight = updatePlayerDto.Weight;

                    context.Players.Update(existingPlayer);
                    context.SaveChanges();

                    return Ok(existingPlayer);

                }
                return NotFound();
            }

        }
    }
}
