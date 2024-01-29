using Connection.Database.Dapper;

namespace ImageService.IService
{
	//public interface IImageUpload<T1, T2>
	//{
	//	Task<Response> SaveImage(IFormFile file, MImageInfo model);
	//}
	public interface IImageUpload<T> : ISaveUpdateService<T> where T : class { }
}
