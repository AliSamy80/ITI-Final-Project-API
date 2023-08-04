using APIFinalProject.DTO;
using APIFinalProject.Models;
using APIFinalProject.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPriceController : ControllerBase
    {

        private readonly IUnitOfWork _reposiratory;


        public MenuPriceController(IUnitOfWork reposiratory)
        {
            _reposiratory = reposiratory;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPrices()
        {

            var Menus = await _reposiratory.menuPrices.GetAll();
            return Ok(Menus);


        }

        [HttpPost]
        public async Task<IActionResult> add([FromBody] MenuPriceDTO MDto)
        {

            MenuPrice m = new MenuPrice
            {
                NumberOfDays = MDto.NumberOfDays,
                Price = MDto.Price,
                unittype = MDto.unitType,

            };


            if (ModelState.IsValid)
            {
                _reposiratory.menuPrices.Add(m);
                return Ok(m);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Updatecat([FromHeader] int id, [FromBody] MenuPriceDTO updateMenu)
        {
            var men = await _reposiratory.menuPrices.GetOne(id);

            if (men == null) return NotFound();
            men.NumberOfDays = updateMenu.NumberOfDays;
            men.Price = updateMenu.Price;
            men.unittype = updateMenu.unitType;


            if (ModelState.IsValid)
            {
                _reposiratory.menuPrices.Update(men);
                return Ok(men);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var men = await _reposiratory.menuPrices.GetOne(id);

            if (men == null) return NotFound();
            else
            {
                if (ModelState.IsValid)
                {
                    _reposiratory.menuPrices.Delete(id);
                    return Ok(men);
                }

                return BadRequest();
            }

        }



    }
}
