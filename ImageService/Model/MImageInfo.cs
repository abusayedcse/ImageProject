namespace ImageService.Model
{
	public class MImageInfo
	{
		public int ImageId { get; set; }
		public string ImageName { get; set; }
		public string ImgPath { get; set; }
		public int UserId { get; set; }
		public bool IsPublic { get; set; }
	}

}
