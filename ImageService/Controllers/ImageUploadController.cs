using Connection.Database.Dapper;
using ImageService.IService;
using ImageService.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageService.Controllers
{
	[Route("api/[controller]/[action]")]
	[EnableCors("AllowSpecificOrigin")]
	[ApiController]
	public class ImageUploadController : ControllerBase
	{
		private readonly IUploadFile _repository;
		public static IWebHostEnvironment _webHostEnvironment { get; set; }
		private IConfiguration _config;
		public ImageUploadController(IWebHostEnvironment webHostEnvironment, IUploadFile repository, IConfiguration configuration)
		{
			_webHostEnvironment = webHostEnvironment;
			_config = configuration;
			_repository = repository;
		}

		[HttpPost]
		public async Task<IActionResult> FileUpload([FromForm] MUploadFile fileUpload)
		{
			try
			{
				if (fileUpload.files.Length > 0)
				{
					string Filepath = "Imageuploads";
					string path = _webHostEnvironment.WebRootPath + "\\" + Filepath + "\\";

					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					if (fileUpload.files == null || fileUpload.files == null || fileUpload.files.Length == 0)
					{
						return NotFound("No File Found!");
						//return "no File";
					}
					string fileExtension = System.IO.Path.GetExtension(fileUpload.files.FileName);
					if (fileExtension == ".jpg" || fileExtension == ".png")
					{

						var result = await _repository.SaveImage(fileUpload, Filepath);
						Response _obj = new Response();
						string jsonData = JsonConvert.SerializeObject(result.data);

						List<MImageInfo> imageList = JsonConvert.DeserializeObject<List<MImageInfo>>(jsonData);

						bool exists = System.IO.Directory.Exists(path);
						if (!exists)
						{
							Directory.CreateDirectory(path);
						}
						if (imageList != null && imageList.Count > 0)
						{
							using (FileStream fileStream = System.IO.File.Create(path + imageList[0].ImageName))
							{
								fileUpload.files.CopyTo(fileStream);
								fileStream.Flush();
								return Ok(result.data);
								//return "OK";
							}
						}
						else
						{
							return NotFound("ave Error!");
							//return "Save Error!";
						}
					}
					else
					{
						return NotFound("No File Found!");
						//return "Image Format Different!";
					}

				}
				else
				{
					return NotFound("Failed");
				}
			}
			catch (Exception ex)
			{
				//return ex.Message;
				return NotFound(ex.Message);
			}
		}

		[HttpGet("{Id}")]//[FromRoute]
		public async Task<IActionResult> FindImageById(string Id)
		{
			string path = _webHostEnvironment.WebRootPath + "\\Imageuploads\\";

			var result = await _repository.IFindByIdService(Id);
			if (result == null)
			{
				return NotFound("User Not Authoize!");

			}
			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> IDeleteByIdService(string Id, string UserId)
		{
			string path = _webHostEnvironment.WebRootPath + "\\Imageuploads\\";

			var result = await _repository.IDeleteByIdService(Id, UserId);
			if (result == null)
			{
				return NotFound("User Not Authoize!");

			}
			else
			{
				Response _obj = new Response();
				string jsonData = JsonConvert.SerializeObject(result.data);
				List<MImageInfo> imageList = JsonConvert.DeserializeObject<List<MImageInfo>>(jsonData);
				if (imageList != null && imageList.Count > 0)
				{
					System.IO.File.Delete(path + "\\" + imageList[0].ImageName);
				}
			}
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> UnauthorizedAccess(string FromDate, string ToDate, int limit, int offset)
		{

			var result = await _repository.UnauthorizedAccess(FromDate, ToDate, limit, offset);
			if (result == null)
			{
				return NotFound("User Not Authoize!");

			}
			return Ok(result);
		}
	}

}
