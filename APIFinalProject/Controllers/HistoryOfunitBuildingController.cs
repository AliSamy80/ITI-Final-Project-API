//using APIFinalProject.Ali_DTO;
//using APIFinalProject.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace APIFinalProject.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HistoryOfunitBuildingController : ControllerBase
//    {

//        private readonly DataContext context;

//        public HistoryOfunitBuildingController(DataContext context)
//        {
//            this.context = context;
//        }


//        [HttpGet("user/{userID}")]
//        public async Task<IActionResult> GetAllUnitsByUserID(string userID)
//        {
//            return Ok();
//            //try
//            //{
//            //    var user = context.Users
//            //        .Include(u => u.UnitBuildings)
//            //        .FirstOrDefault(u => u.Id == userID);

//            //    if (user == null)
//            //    {
//            //        return NotFound();
//            //    }

//            //    var unitDTOs = user.UnitBuildings.Select(u => new unitBluildingsWithUserDTO
//            //    {
//            //        UnitID = u.ID,
//            //        UnitName = u.Name,
//            //        CoverImageUnit = u.coverImgage,
//            //        Price = u.Price,
//            //        UserName = user.Name
//            //    }).ToList();

//            //    return Ok(unitDTOs);
//            //}
//            //catch (Exception ex)
//            //{
//            //    // Handle exception and return appropriate response
//            //    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the units.");
//            //}
//        }

//    }
//}
