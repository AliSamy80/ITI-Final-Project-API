using APIFinalProject.DTO;
using APIFinalProject.Models;
using APIFinalProject.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IHostingEnvironment _host;

        private readonly IUnitOfWork _repository;
        private readonly DataContext _context;
        public UnitController(IUnitOfWork repository, DataContext context, IHostingEnvironment host)
        {
            _repository = repository;
            _host = host;
            _context = context;
        }

        [HttpGet]
        [Route("getUnits")]
        public IActionResult GetAllUnits()

        {

            List<UnitBuildingCardDTO> unitBuildingCardDTOs = new List<UnitBuildingCardDTO>();
            unitBuildingCardDTOs = _context.UnitBuildings.Where(u => u.Date == null).Select(u => new UnitBuildingCardDTO
            {
                ID = u.ID,
                Name = u.Name,
                Price = u.Price,
                CapacityBathRoom = u.CapacityBathRoom,
                CapacityRoom = u.CapacityRoom,
                Area = u.Area,
                City = u.City,
                Governamnet = u.Governamnet,
                CoverImageString = u.CoverImgage.IsNullOrEmpty() ? null : "http://localhost:5219/UnitImages/" + u.CoverImgage,
                Location = u.Location,
                UnitType = u.UnitType
            }).ToList();


            return Ok(unitBuildingCardDTOs);
        }

        [HttpGet]
        [Route("getUnitsPagination")]
        public IActionResult GetAllUnitsPagination(int pageNumber = 1, int pageSize = 10)

        {
            var totalCount = _context.UnitBuildings.Count();
            List<UnitBuildingCardDTO> unitBuildingCardDTOs = new List<UnitBuildingCardDTO>();
            unitBuildingCardDTOs = _context.UnitBuildings.Where(u => u.Date == null).Select(u => new UnitBuildingCardDTO
            {
                ID = u.ID,
                Name = u.Name,
                Price = u.Price,
                CapacityBathRoom = u.CapacityBathRoom,
                CapacityRoom = u.CapacityRoom,
                Area = u.Area,
                City = u.City,
                Governamnet = u.Governamnet,
                CoverImageString = u.CoverImgage.IsNullOrEmpty() ? null : "http://localhost:5219/UnitImages/" + u.CoverImgage,
                Location = u.Location,
                UnitType = u.UnitType
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


        [HttpPost]
        [Route("addUnit/{id}")]
        public IActionResult AddUnit([FromRoute] string id, [FromForm] UnitBuildingDTO UnitDTO)
        {

            if (ModelState.IsValid)
            {
                string coverImage = SaveCoverImage(UnitDTO.CoverImage);
                string unitImages = SaveListImages(UnitDTO.UnitImagesFile, coverImage);
                UnitBuilding unitBuilding = new()
                {
                    Name = UnitDTO.Name,
                    Description = UnitDTO.Description,
                    Duration = 5,
                    Address = UnitDTO.Address,
                    City = UnitDTO.City,
                    Governamnet = UnitDTO.Governamnet,
                    Location = UnitDTO.Location,
                    Area = UnitDTO.Area,
                    FloorNumber = UnitDTO.FloorNumber,
                    CapacityBathRoom = UnitDTO.CapacityBathRoom,
                    Price = UnitDTO.Price,
                    MaxPrice = UnitDTO.MaxPrice,
                    MinPrice = UnitDTO.MinPrice,
                    CapacityRoom = UnitDTO.CapacityRoom,
                    CategoryId = UnitDTO.CategoryId,
                    CoverImgage = coverImage,
                    PriceType = UnitDTO.PriceType,
                    UnitType = 0,
                    UnitImagesPath = unitImages,
                    OwnerId = id,
                    UnitConcreteImagesPath = SaveImagesOutWWWRoot(UnitDTO.UnitConcreteImagesFile)
                };
                _repository.unitBuildings.Add(unitBuilding);
                return Ok(unitBuilding);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("setDuration/{id}")]
        public async Task<IActionResult> SetDuration([FromRoute] string id, [FromBody] UpdateDuration updateDuration)
        {
            var unitBuilding = await _context.UnitBuildings.Where(u => u.ID == updateDuration.ID && u.OwnerId == id).FirstOrDefaultAsync();
            if (unitBuilding != null) {
                unitBuilding.Duration = updateDuration.Duration;
                unitBuilding.UnitType = updateDuration.UnitType;
                await _context.SaveChangesAsync();
                return Ok(unitBuilding);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("verifyUnit/{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> VerifyUnit([FromRoute] int id)
        {
            var unitBuilding = await _context.UnitBuildings.Where(u => u.ID == id).FirstOrDefaultAsync();
            if (unitBuilding != null)
            {
                unitBuilding.Date = DateTime.Now;
                await _context.SaveChangesAsync();
                return Ok(unitBuilding);
            }
            return Unauthorized();
        }


        [HttpPut]
        [Route("UpdateUnit/{id}")]
        public async Task<IActionResult> UpdateUnit([FromRoute] string id, [FromBody] UnitBuildingUpdateDTO updateUnit)
        {
            var unit = await _context.UnitBuildings.Where(u => u.ID == updateUnit.ID && u.OwnerId == id).FirstOrDefaultAsync();

            if (unit == null) return NotFound();
            unit.Name = updateUnit.Name;
            unit.Description = updateUnit.Description;
            unit.Address = updateUnit.Address;
            unit.MaxPrice = updateUnit.MaxPrice;
            unit.MinPrice = updateUnit.MinPrice;
            unit.Price = updateUnit.Price;
            unit.CapacityBathRoom = updateUnit.CapacityBathRoom;
            unit.CapacityRoom = updateUnit.CapacityRoom;
            if (ModelState.IsValid)
            {
                _repository.unitBuildings.Update(unit);
                return Ok(unit);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("getUnit/{id}")]
        public async Task<IActionResult> GetUnit(int id)
        {
            UnitBuilding unitBuilding = await _repository.unitBuildings.GetOne(id);
            if (unitBuilding == null)
                return NotFound();
            else
            {
                UnitBuildingDetailsDTO returnbuild = new() {
                    OwnerID = unitBuilding.OwnerId,
                    Name = unitBuilding.Name,
                    Address = unitBuilding.Address,
                    Description = unitBuilding.Description,
                    Area = unitBuilding.Area,
                    CapacityBathRoom = unitBuilding.CapacityBathRoom,
                    CapacityRoom = unitBuilding.CapacityRoom,
                    City = unitBuilding.City,
                    CoverImageString = unitBuilding.CoverImgage,
                    FloorNumber = unitBuilding.FloorNumber,
                    Governamnet = unitBuilding.Governamnet,
                    Location = unitBuilding.Location,
                    MaxPrice = unitBuilding.MaxPrice,
                    MinPrice = unitBuilding.MinPrice,
                    Price = unitBuilding.Price,
                    PriceType = unitBuilding.PriceType,
                    UnitType = unitBuilding.UnitType,
                    UnitImagesString = unitBuilding.UnitImagesPath
                };

                return Ok(returnbuild);

            }
        }

        [HttpDelete]
        [Route("deleteUnit/{id}/{unitID}")]
        public async Task<IActionResult> DeleteUnit(string id, int unitID)
        {
            UnitBuilding unitBuilding = await _context.UnitBuildings.Where(u => u.OwnerId == id && u.ID == unitID).FirstOrDefaultAsync();
            if (unitBuilding != null)
            {
                _repository.unitBuildings.Delete(unitID);
                return Ok(unitBuilding);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("getCities")]
        public async Task<IActionResult> GetCities(int pageNumber = 1, int pageSize = 10)
        {
            int totalCount = _context.UnitBuildings.GroupBy(e => e.Governamnet)
           .Select(g => new CityCountDTO
           {
               CityName = g.Key,
               Count = g.Count(),
               CityImage = "http://localhost:5219/CityImages/" + g.Key + ".jpg"
           }).OrderByDescending(c => c.Count).Count();

            List<CityCountDTO> cityCountDTOs = _context.UnitBuildings
           .GroupBy(e => e.Governamnet)
           .Select(g => new CityCountDTO
           {
               CityName = g.Key,
               Count = g.Count(),
               CityImage = "http://localhost:5219/CityImages/" + g.Key + ".jpg"
           }).OrderByDescending(c => c.Count)
    .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize).ToList();
            var response = new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = cityCountDTOs
            };

            return Ok(response);

        }

        [HttpGet]
        [Route("search/{area}/{category}/{priceType}/{government}")]
        public async Task<IActionResult> GetAllbyselectorAll(int area, int category, PriceType priceType, string government)
        {

            var Menus = await _repository.unitBuildings.SelectGroup(m => m.Governamnet == government);
            var Menus1 = Menus.Where(m => m.CategoryId == category).Where(m => m.Area >= area).OrderBy(m => m.Date).ToList();
            //var Menus2 = Menus.Where(m => m.Area == area).ToList();
            //var Menus3 = Menus.Where(m => m.PriceType == priceType).ToList();

            //var both = Menus4.Select(m => m.Area >= area).Distinct().OrderBy(m => m.Date).ToList();

            ////var both = Menus1.Concat(Menus2).Concat(Menus3).Distinct().ToList();
            var finalstoredunit = new List<UnitBuildingCardDTO>();

            finalstoredunit = Menus1.Select(u => new UnitBuildingCardDTO
            {
                ID = u.ID,
                Name = u.Name,
                Price = u.Price,
                CapacityBathRoom = u.CapacityBathRoom,
                CapacityRoom = u.CapacityRoom,
                Area = u.Area,
                City = u.City,
                Governamnet = u.Governamnet,
                CoverImageString = u.CoverImgage.IsNullOrEmpty() ? null : "http://localhost:5219/UnitImages/" + u.CoverImgage,
                Location = u.Location,
                UnitType = u.UnitType
            }).ToList();

            //foreach (var unit in both)
            //{
            //    if (unit.Date != null)
            //    {
            //        finalstoredunit.Add(unit);
            //    }
            //}
            return Ok(finalstoredunit);

        }

        [HttpGet("{government}")]
        public async Task<IActionResult> GetAllbyCity(string government)
        {


            var Menu = await _repository.unitBuildings.SelectGroup(m => m.Governamnet == government);
            var finalstoredunit = new List<UnitBuildingCardDTO>();


            finalstoredunit = Menu.Select(u => new UnitBuildingCardDTO
            {
                ID = u.ID,
                Name = u.Name,
                Price = u.Price,
                CapacityBathRoom = u.CapacityBathRoom,
                CapacityRoom = u.CapacityRoom,
                Area = u.Area,
                City = u.City,
                Governamnet = u.Governamnet,
                CoverImageString = u.CoverImgage.IsNullOrEmpty() ? null : "http://localhost:5219/UnitImages/" + u.CoverImgage,
                Location = u.Location,
                UnitType = u.UnitType
            }).ToList();

            return Ok(finalstoredunit);

        }

        [HttpGet]
        [Route("suggestions/{unitID}/{area}/{price}/{gover}")]
        public async Task<IActionResult> Suggestions(int unitID,double area,double price,string gover) {

            var list1 =await _context.UnitBuildings.Where(u => u.Area >= area && u.ID != unitID).ToListAsync();
            var list2 =await _context.UnitBuildings.Where(u => u.Price <= price && u.ID != unitID).ToListAsync();
            var list3 =await _context.UnitBuildings.Where(u => u.Governamnet == gover && u.ID != unitID).ToListAsync();
            var both = list1.Concat(list2).Concat(list3).Distinct().ToList();

            Random random = new Random();
            int numberOfItemsToSelect = 3; // Change this value to the desired number of random items

            List<UnitBuildingCardDTO> randomList = new ();

            while (randomList.Count < numberOfItemsToSelect && both.Count > 0)
            {
                int randomIndex = random.Next(0, both.Count);
                UnitBuilding randomItem = both[randomIndex];

                randomList.Add(new UnitBuildingCardDTO
                {
                    Area=randomItem.Area,
                    CapacityBathRoom=randomItem.CapacityBathRoom,
                    CapacityRoom=randomItem.CapacityRoom,
                    City= randomItem.City,
                    CoverImageString= "http://localhost:5219/UnitImages/" + randomItem.CoverImgage,
                    Governamnet=randomItem.Governamnet,
                    ID=randomItem.ID,
                    Location=randomItem.Location,
                    Name=randomItem.Name,
                    Price=randomItem.Price,
                    UnitType=randomItem.UnitType

                });
                both.RemoveAt(randomIndex);
            }

            return Ok(randomList);
        }
        


        [NonAction]
        public string SaveCoverImage(IFormFile coverImage)
        {
            if (coverImage != null)
            {
                string _wwwRootPath = Path.Combine(_host.WebRootPath, "UnitImages");

                string NewFileName = Guid.NewGuid().ToString() + coverImage.FileName;
                var targetFilePath = Path.Combine(_wwwRootPath, NewFileName);
                using (var stream = new FileStream(targetFilePath, FileMode.Create))
                {
                    coverImage.CopyTo(stream);

                }
                return NewFileName;
            }
            return null;
        }
        [NonAction]
        public string SaveListImages(List<IFormFile> images, string coverImage)
        {
            if (images != null && images.Count != 0)
            {
                string _wwwRootPath = Path.Combine(_host.WebRootPath, "UnitImages");
                string NewFilesName = coverImage;
                for (int i = 1; i < images.Count; i++)
                {
                    string NewFileName = Guid.NewGuid().ToString() + images[i].FileName;
                    var targetFilePath = Path.Combine(_wwwRootPath, NewFileName);
                    using (var stream = new FileStream(targetFilePath, FileMode.Create))
                    {
                        images[i].CopyTo(stream);

                    }
                    NewFilesName +=  ","+NewFileName ;
                }

                return NewFilesName;
            }
            return null;
        }
        [NonAction]
        public string SaveImagesOutWWWRoot(List<IFormFile> images)
        {
            if (images != null && images.Count != 0)
            {
                string _wwwRootPath = Path.Combine(_host.ContentRootPath, "Resources", "ConcreteImages");
                string NewFilesName = string.Empty;
                foreach (IFormFile image in images)
                {
                    string NewFileName = Guid.NewGuid().ToString() + image.FileName;
                    var targetFilePath = Path.Combine(_wwwRootPath, NewFileName);
                    using (var stream = new FileStream(targetFilePath, FileMode.Create))
                    {
                        image.CopyTo(stream);

                    }
                    NewFilesName += NewFileName + ",";
                }

                return NewFilesName;
            }
            return null;
        }

    }

}