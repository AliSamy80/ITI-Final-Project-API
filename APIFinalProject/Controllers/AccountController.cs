using APIFinalProject.DTO;
using APIFinalProject.Models;
using APIFinalProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace APIFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthManager _authManager;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        //private readonly IMapper _mapper;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            ILogger<AccountController> logger, DataContext context,IAuthManager authManager,IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _authManager = authManager;
            _hostingEnvironment =hostingEnvironment;
            // _mapper = mapper;
        }

        [HttpPost]
        // [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            RegisterDTO registerDTO = new RegisterDTO();
            registerDTO.Address = userDTO.Address;
            registerDTO.BirthDate = userDTO.BirthDate;
            registerDTO.Email = userDTO.Email;
            registerDTO.FullName = userDTO.FullName;
            registerDTO.Password = userDTO.Password;
            registerDTO.PasswordConfirmed = userDTO.Password;
            registerDTO.PhoneNumber = userDTO.PhoneNumber;
            registerDTO.Roles.Add("User");
            _logger.LogInformation($"Registration Attempt for {registerDTO.Email}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ggg");
                return BadRequest(ModelState);

            }
            try
            {
                var user = new User { UserName = registerDTO.Email,
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Name = registerDTO.FullName,
                    Address = registerDTO.Address,
                    BirthDate = registerDTO.BirthDate, 
                };
                user.UserName = registerDTO.Email;
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (!result.Succeeded)
                {
                    return BadRequest("$User Registration attempt to Failed");
                }
                await _userManager.AddToRolesAsync(user, registerDTO.Roles);
                return Accepted(new { ID = user.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(Register)}");
                return Problem($"Somthing Went Wrong in the {nameof(Register)}", statusCode: 404);
            }
        }

        [HttpGet]
        [Route("getUser/{id}")]
        public IActionResult GeUser(string id)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(id);
                UserDTO userDTO = new UserDTO()
                {
                    Address = user.Address,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    BirthDate = user.BirthDate,
                    FullName = user.Name,
                };
                return Ok(userDTO);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            //  returnUrl ??= Url.Content("~/");

            //   ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var user = await _context.Users.Where(u => u.Email == loginDTO.Email).Select(u => u).FirstOrDefaultAsync();
                    return Accepted(new {ID=user.Id });
                }

                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //  //  return RedirectToPage("./Lockout");
                //}
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest();
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest();
        }

        //[HttpPost]
        //[Route("LoginWithToken")]
        //public async Task<IActionResult> LoginWithToken([FromBody] LoginDTO loginDTO)
        //{
        //    _logger.LogInformation($"User Attempt for {loginDTO.Email}");

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try {
        //        if (!await _authManager.ValidateUser(loginDTO))
        //        {
        //            return Unauthorized();
        //        }
        //        return Accepted(new {Token=await _authManager.CreateToken()});
        //    }
        //    catch (Exception ex) {
        //        _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(Login)}");
        //        return Problem($"Somthing Went Wrong in the {nameof(Login)}", statusCode: 500);
        //    }

        //}

        [HttpPost]
        [Route("verify/{id}")]
        public async Task<IActionResult> VerifyAccount([FromRoute] string id,[FromForm] VerifyDTO verifyDTO)
        {
              if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var user =await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
                user.NID = verifyDTO.NID;
                user.NIDPhoto = saveNIDImage(verifyDTO.NIDPhoto);
                user.PersonalPhoto = savePersonalImage(verifyDTO.PersonalPhoto);
                await _context.SaveChangesAsync();
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(Login)}");
                return Problem($"Somthing Went Wrong in the {nameof(Login)}", statusCode: 500);
            }

        }

        [HttpGet]
        [Route("getUsers/{userID}")]
        public async Task<IActionResult> getUsers([FromRoute] string[] userID)
        {
            List<UsersChatDTO> userDTOs = new List<UsersChatDTO>();
            string[] ids = userID[0].Split(',');
            await Console.Out.WriteLineAsync(ids[0]);

            foreach (string id in ids)
            {
                var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (User != null)
                {
                    userDTOs.Add(new UsersChatDTO
                    {
                        Id = User.Id,
                        Name = User.Name,
                        PersonalImage = "http://localhost:5219/PersonalImages/" + User.PersonalPhoto
                    });
                }
            }
            return Ok(userDTOs);
        }

        [HttpGet]
        [Route("getUsername/{id}")]
        public async Task<IActionResult> GetUsernameAndImage([FromRoute] string id) {
            var user = await _context.Users.Where(u=>u.Id==id).Select(u => new { u.UserName,  u.PersonalPhoto }).FirstOrDefaultAsync();
            UsernameAndPersonalPhoto usernameAndPersonalPhoto = new UsernameAndPersonalPhoto();
            if (user != null)
            {
                usernameAndPersonalPhoto.Username = user.UserName;
                usernameAndPersonalPhoto.PersonalPhoto = "http://localhost:5219/PersonalImages/" + user.PersonalPhoto;
                return Ok(usernameAndPersonalPhoto);
            }
            return NotFound();
        }
        string savePersonalImage(IFormFile personal) {
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

       string saveNIDImage(IFormFile nid)
        {
            if (nid != null)
            {
                string _wwwRootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources","NIDImages");
                string NewFileName = Guid.NewGuid().ToString() + nid.FileName;

                var targetFilePath = Path.Combine(_wwwRootPath, NewFileName);
                using (var stream = new FileStream(targetFilePath, FileMode.Create))
                {
                   nid.CopyTo(stream);

                }
                return NewFileName;
            }
            return null;
        }


    }

}
