using Connection.Database.Dapper;
using ImageService.Model;

namespace ImageService.IService
{
	public interface IUploadFile
	{
		Task<Response> SaveImage(MUploadFile model, string path);
		Task<Response> IFindByIdService(string Id);
		Task<Response> IDeleteByIdService(string Id, string UserId);
		Task<Response> UnauthorizedAccess(string FromDate, string ToDate, int limit, int offset);
	}
}
