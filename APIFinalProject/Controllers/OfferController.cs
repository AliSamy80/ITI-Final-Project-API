using APIFinalProject.DTO;
using APIFinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly DataContext _context;

        public OfferController(DataContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<IActionResult> CreateOffer(OfferDTO offerDTO)
        {
            var BuyerOffer = await _context.Users.FindAsync(offerDTO.BuyerID);
            var OwnerOffer = await _context.Users.FindAsync(offerDTO.OwnerID);
            if(BuyerOffer!=null && OwnerOffer != null)
            {
                var offer = new Offer
                 {
                    Message = offerDTO.Message,
                    Price = offerDTO.Price,
                    UnitBuilding = await _context.UnitBuildings.FindAsync(offerDTO.UnitBuildingID),
                    BuyerOffer = BuyerOffer,
                    OwnerOffer = OwnerOffer
                };
             _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return Ok(offer.ID);
            }
            return BadRequest();
        }





        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetOffersByOwnerId(string ownerId)
        {
            List<OfferDTO> offerDTOs = new List<OfferDTO>();
            offerDTOs = await _context.Offers.Where(o => o.OwnerOffer.Id == ownerId).Select(o => new
             OfferDTO
            {
                BuyerID = o.BuyerOffer.Id,
                BuyerName = o.BuyerOffer.Name,
                ID = o.ID,
                Message = o.Message,
                OwnerID = o.OwnerOffer.Id,
                OwnerName = o.OwnerOffer.Name,
                Price = o.Price,
                UnitBuildingID = o.UnitBuilding.ID
            }
         ).ToListAsync();

            return Ok(offerDTOs);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOffer(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("getOffer/{offerID}")]
        public async Task<IActionResult> GetOffer(int offerID) {
            OfferDTO offerDTO =await _context.Offers.Where(o=>o.ID==offerID).Select(o=>new OfferDTO
            {
                ID=o.ID,
                BuyerID=o.BuyerOffer.Id,
                BuyerName=o.BuyerOffer.Name,
                Message=o.Message,
                OwnerID=o.OwnerOffer.Id,
                Price=o.Price,
                OwnerName=o.OwnerOffer.Name,
                UnitBuildingID=o.UnitBuilding.ID
            }).FirstOrDefaultAsync();
            return Ok(offerDTO);
        }

    }

}

