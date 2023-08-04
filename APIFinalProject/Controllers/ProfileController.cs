using APIFinalProject.DTO;
using APIFinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public ProfileController(DataContext context, IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet("{id}")]
        public IActionResult GeProfile(string id)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                VerifyAccountDTO verifyAccountDTO = new VerifyAccountDTO()
                {
                    FullName = user.Name,
                    Address = user.Address,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    BirthDate = user.BirthDate,
                    CreditNumber = user.CreditNumber,
                    NID = user.NID,
                    NIDPhoto = user.NIDPhoto,
                    //PersonalPhoto = user.PersonalPhoto,
                    PersonalPhoto = "http://localhost:5219/PersonalImages/" + user.PersonalPhoto

            };
                return Ok(verifyAccountDTO);
            }
            return BadRequest();
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] UpdateProfileDTO userDto)
        {
            var User = context.Users.Find(id);

            if (User == null)
            {
                return NotFound();
            }

            // Update the user details
            User.Name = userDto.fullName;
           // User.UserName = userDto.fullName;
            User.PhoneNumber = userDto.phoneNumber;
            User.BirthDate = userDto.birthDate;
            User.Address = userDto.address;
            //User.PersonalPhoto = userDto.personalPhoto;

            User.NID = userDto.nid;
            User.NIDPhoto = userDto.nidPhoto;

            context.SaveChanges();
            return Ok(User);
        }


        [HttpPut("img/{id}")]
        public IActionResult UpdateUserPicture(string id, [FromForm] IFormFile pictureURL)
        {
            var User = context.Users.Find(id);

            if (User == null)
            {
                return NotFound();
            }

            // Update the user details

            if (pictureURL != null)
            {
                // Save the image and update the user's profile picture
                string personalPhoto = savePersonalImage(pictureURL);
                User.PersonalPhoto = personalPhoto;
            }
            context.SaveChanges();
            return Ok(User);
        }

        string savePersonalImage(IFormFile personal)
        {
            if (personal != null)
            {
                string _wwwRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "PersonalImages");
                string NewFileName = Guid.NewGuid().ToString() + personal.FileName;

                var targetFilePath = Path.Combine(_wwwRootPath, NewFileName);
                using (var stream = new FileStream(targetFilePath, FileMode.Create))
                {
                    personal.CopyTo(stream);

                }
                return NewFileName;
            }
            return null;
        }
    }
}
