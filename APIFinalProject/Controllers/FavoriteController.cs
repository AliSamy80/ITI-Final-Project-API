//Favorite Controller




using APIFinalProject.AhmedFavoritesDTO;
using APIFinalProject.AhmedUnitBuildingDTO;
using APIFinalProject.DTO;
using APIFinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {

        private readonly DataContext _context;
        public FavoriteController(DataContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("getAllFavoriteUnitsByUserID")]
        public IActionResult GetAllFavoriteUnitsByUserID(string userID, int pageNumber = 1, int pageSize = 10)
        {
            if (userID != null)
            {
                var totalCount = _context.Favorites.Count();
                List<UnitBuildingCardDTO> unitBuildingCardDTOs = _context.Favorites.Include(x => x.UnitBuilding)
                   .Where(x => x.User.Id == userID).Select(unit=>new UnitBuildingCardDTO{
                       ID = unit.UnitBuilding.ID,
                       Area = unit.UnitBuilding.Area,
                       CapacityBathRoom = unit.UnitBuilding.CapacityBathRoom,
                       CapacityRoom = unit.UnitBuilding.CapacityRoom,
                       City = unit.UnitBuilding.City,
                       CoverImageString = "http://localhost:5219/UnitImages/" + unit.UnitBuilding.CoverImgage,
                       Governamnet = unit.UnitBuilding.Governamnet,
                       Location = unit.UnitBuilding.Location,
                       Name = unit.UnitBuilding.Name,
                       Price = unit.UnitBuilding.Price,
                       UnitType = unit.UnitBuilding.UnitType
                   }).Skip((pageNumber - 1) * pageSize)
        .Take(pageSize).ToList();

                var response = new
                {
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = unitBuildingCardDTOs
                };

                return Ok(response);
            }

            return BadRequest();



        }
        [HttpPost("{userID}")]
        public IActionResult AddUnitToFavoriteByUserID(string userID, [FromBody] int unitID)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == userID);
                UnitBuilding unit = _context.UnitBuildings.FirstOrDefault(u => u.ID == unitID);

                if (user != null && unit != null)
                {
                    Favorite favorite = new ()
                    {
                        User = user,
                        UnitBuilding = unit
                    };
                    _context.Favorites.Add(favorite);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{userID}/{unitID}")]
        public IActionResult DeleteUnitFromFavoriteByUserID(string userID,int unitID)
        {
            if (userID != null && unitID != null)
            {
                Favorite favorite = _context.Favorites.Include(x => x.User).Include(x => x.UnitBuilding).FirstOrDefault(f => f.User.Id == userID && f.UnitBuilding.ID == unitID);

                if (favorite != null)
                {
                    _context.Favorites.Remove(favorite);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getFavorites/{userID}")]
        public async Task<IActionResult> GetFavorites([FromRoute]string userID)
        {
            List<FavoritesDTO> favoritesDTOs = new List<FavoritesDTO>();
            if (userID != null)
            {
               List<int> list=await _context.Favorites.Where(f => f.User.Id == userID).Select(u => u.UnitBuilding.ID).ToListAsync();
                foreach (int id in list) {
                    FavoritesDTO favoritesDTO = new() {
                    UnitID=id
                    };
                    favoritesDTOs.Add(favoritesDTO);
                }
            }
            return Ok(favoritesDTOs);
        }
    
    }
}



//favorite class 






