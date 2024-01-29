using ImageService.IService;
using ImageService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{

    [Route("api/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly ICreateUser<MCreateUser> _repository;
        private IConfiguration _config;
        public CreateUserController(ICreateUser<MCreateUser> repository, IConfiguration configuration)
        {
            _config = configuration;
            _repository = repository;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateUser([FromBody] MCreateUser model)
        {
            IActionResult response = Unauthorized();
            if (model == null)
            {
                return BadRequest();
            }
            var result = await _repository.SaveUpdate(model);
            if (result != null)
            {
                response = Ok(result);
            }
            return response;
        }
    }
}
