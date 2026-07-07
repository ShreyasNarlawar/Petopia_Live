using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaWebApi.Models;

namespace PetopiaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetProfileController : ControllerBase
    {
        private readonly PetopiaDbContext context;

        public PetProfileController(PetopiaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PetProfile>>> GetPetProfile()
        {
            var data = await context.PetProfiles.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetProfile>> GetPetProfileById(int id)
        {
            var pet = await context.PetProfiles.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return pet;
        }

        [HttpPost]
        public async Task<ActionResult<PetProfile>> CreatePetProfile(PetDto petDto)
        {
            // Validate foreign key for category
            if (!await context.PetCategories.AnyAsync(c => c.CategoryId == petDto.CategoryId))
            {
                return BadRequest("Invalid CategoryId.");
            }

            var pet = new PetProfile
            {
                Name = petDto.Name,
                Age = petDto.Age,
                Size = petDto.Size,
                Gender = petDto.Gender,
                Weight = petDto.Weight,
                Vaccinated = petDto.Vaccinated,
                Spayed = petDto.Spayed,
                Health = petDto.Health,
                Needs = petDto.Needs,
                HouseTrained = petDto.HouseTrained,
                GoodWithKids = petDto.GoodWithKids,
                GoodWithPets = petDto.GoodWithPets,
                Personality = petDto.Personality,
                MonthlyExpenses = petDto.MonthlyExpenses,
                IsRegisteredWithGovt = petDto.IsRegisteredWithGovt,
                CategoryId = petDto.CategoryId, // Ensure you set the CategoryId
                BreedId=petDto.BreedId,
                UserId=petDto.UserId
            };

            await context.PetProfiles.AddAsync(pet);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPetProfileById), new { id = pet.PetId }, pet);
        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PetProfile>> UpdatePetProfile(int id, PetDto petDto)
        {
            if (id != petDto.PetId) 
            {
                return BadRequest();
            }

            var pet = await context.PetProfiles.FindAsync(id); 
            if (pet == null)
            {
                return NotFound();
            }

            // Update pet properties
            pet.Name = petDto.Name;
            pet.Age= petDto.Age;
            pet.Size = petDto.Size;
            pet.Gender = petDto.Gender;
            pet.Weight = petDto.Weight;
            pet.Vaccinated = petDto.Vaccinated;
            pet.Spayed = petDto.Spayed;
            pet.Health = petDto.Health;
            pet.Needs = petDto.Needs;
            pet.HouseTrained = petDto.HouseTrained;
            pet.GoodWithKids = petDto.GoodWithKids;
            pet.GoodWithPets = petDto.GoodWithPets;
            pet.Personality = petDto.Personality;
            pet.MonthlyExpenses = petDto.MonthlyExpenses;
            pet.IsRegisteredWithGovt = petDto.IsRegisteredWithGovt;
            pet.CategoryId = petDto.CategoryId;// Ensure you set the CategoryId
            pet.BreedId = petDto.BreedId;
            pet.UserId = petDto.UserId;

            context.Entry(pet).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(pet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PetProfile>> DeletePetProfile(int id)
        {
            var pet = await context.PetProfiles.FindAsync(id); 
            if (pet == null)
            {
                return NotFound();
            }
            context.PetProfiles.Remove(pet); 
            await context.SaveChangesAsync();
            return Ok();
        }

        public class PetDto
        {
            public int PetId { get; set; }

            public string? Name { get; set; }

           public int CategoryId { get; set; }

            public int BreedId { get; set; }

            public int? Age { get; set; }

            public string? Size { get; set; }

            public string? Gender { get; set; }

            public decimal? Weight { get; set; }

            public bool? Vaccinated { get; set; }

            public bool? Spayed { get; set; }

            public string? Health { get; set; }

            public string? Needs { get; set; }

            public bool? HouseTrained { get; set; }

            public bool? GoodWithKids { get; set; }

            public bool? GoodWithPets { get; set; }

            public string? Personality { get; set; }

            public decimal? MonthlyExpenses { get; set; }

            public string? IsRegisteredWithGovt { get; set; }

            public int UserId { get; set; }
        }
    }
}