namespace ImageService.Model
{
	public class MUploadFile
	{
		public IFormFile files { get; set; }
		public bool IsPublic { get; set; }
		public int UserId { get; set; }
	}
}
