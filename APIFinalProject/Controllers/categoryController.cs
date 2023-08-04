using APIFinalProject.Ali_DTO;
using APIFinalProject.DTO;
using APIFinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryController : ControllerBase
    {
        private readonly DataContext context;
        public categoryController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {

            try
            {

                List<CategoryDTO> categoryDTOs = context.Categories.Select(c=>new CategoryDTO
                {
                    ID=c.ID,
                    Name=c.Name,
                  
                }).ToList();

                return Ok(categoryDTOs);
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the categories.");
            }
        }


        [HttpGet("{id}", Name = "GetOneCat")]
        public IActionResult GetOneCategory(int id)
        {
            CategoryWithBuildinDTO catDTO = new CategoryWithBuildinDTO();
            Category cat = context.Categories.Include(c => c.UnitBuildings).FirstOrDefault(d => d.ID == id);

            catDTO.ID = cat.ID;
            catDTO.Name = cat.Name;

            foreach (var item in cat.UnitBuildings)
            {
                catDTO.UnitBuildingCardDTOs.Add(new UnitBuildingCardDTO
                {
                    ID = item.ID,
                    Name = item.Name,
                    Area = item.Area,
                    City = item.City,
                    CapacityBathRoom = item.CapacityBathRoom,
                    CapacityRoom = item.CapacityRoom,
                    CoverImageString = item.CoverImgage.IsNullOrEmpty() ? null : "http://localhost:5219/UnitImages/" + item.CoverImgage,
                    Governamnet = item.Governamnet,
                    Location = item.Location,
                    Price = item.Price,
                    UnitType = item.UnitType
                });
            }
            return Ok(catDTO);

        }


        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] categoryWithUnitBuilding AddedCategoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category category = new Category
                    {
                        Name = AddedCategoryDTO.CategoryName,
                        coverImage = "http://localhost:5219/CatImage/" + AddedCategoryDTO.CategoryName + ".jpeg"
                    
,
                    };

                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetOneCategory), new { id = category.ID }, category);

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] categoryWithUnitBuilding categoryWithUnitBuilding)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category category = context.Categories.FirstOrDefault(c => c.ID == id);

                    if (category != null)
                    {
                        category.Name = categoryWithUnitBuilding.CategoryName;
                        category.coverImage = "http://localhost:5219/CatImages/" + categoryWithUnitBuilding.CategoryName + ".jpeg";

                         await context.SaveChangesAsync();

                        List<string> unitBuildingNames = category.UnitBuildings
                            .Select(ub => ub.Name)
                            .ToList();

                        // Update the UnitBuildingNames property in the CategoryDTO
                        categoryWithUnitBuilding.UnitBuildingsNames = unitBuildingNames;

                        return Ok(category);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }

        //[HttpGet]
        //[Route("GetCategories")]
        //public IActionResult GetCategories() {
        //    return Ok(context.Categories.ToList());
        //}


    }
}
