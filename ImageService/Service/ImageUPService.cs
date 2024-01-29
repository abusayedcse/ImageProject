using Connection.Database.Dapper;
using Dapper;
using ImageService.IService;
using ImageService.Model;
using System.Data;

namespace ImageService.Service
{
	public class ImageUPService : BasePoint, IUploadFile
	{
		private readonly Response _response;
		private readonly ResponseResult _responseResult;
		public ImageUPService()
		{
			_response = new Response();
			_responseResult = new ResponseResult();
		}

		public async Task<Response> IDeleteByIdService(string Id, string UserId)
		{
			try
			{
				DynamicParameters _parameter = new DynamicParameters();
				_parameter.Add("@Id", Id, DbType.Int32, direction: ParameterDirection.Input);
				_parameter.Add("@UserId", UserId, DbType.Int32, direction: ParameterDirection.Input);
				_response.data = await DatabaseHub.QueryAsync<object>(storedProcedureName: @"dbo.[usp_DeleteImageById]", parameters: _parameter, dbName: DatabaseName.imgService);
			}
			catch (Exception ex)
			{
				_response.error = ex.Message;
			}
			finally
			{
				if (_response.data != null)
				{
					_response.message = "Not Administrator.";
				}
				else
				{
					_response.message = "Delete Image Successful.";
				}
				_responseResult.GetLoadResult(_response);
			}
			return _response;
		}

		public async Task<Response> IFindByIdService(string Id)
		{
			try
			{
				DynamicParameters _parameter = new DynamicParameters();
				_parameter.Add("@Id", Id, DbType.Int32, direction: ParameterDirection.Input);
				_response.data = await DatabaseHub.QueryAsync<object>(storedProcedureName: @"dbo.[usp_GetImageById]", parameters: _parameter, dbName: DatabaseName.imgService);
			}
			catch (Exception ex)
			{
				_response.error = ex.Message;
			}
			finally
			{
				if (_response.data != null)
				{
					_response.message = "Administrator user.";
				}
				else
				{
					_response.message = "Image Access Not Allow.";
				}
				_responseResult.GetLoadResult(_response);
			}
			return _response;
		}

		public async Task<Response> SaveImage(MUploadFile model, string path)
		{
			string fileExtension = System.IO.Path.GetExtension(model.files.FileName);
			try
			{
				DynamicParameters _parameter = new DynamicParameters();
				_parameter.Add("@UserId", model.UserId, DbType.Int32, direction: ParameterDirection.Input);
				_parameter.Add("@fileType", fileExtension.Trim(), DbType.String, direction: ParameterDirection.Input);
				_parameter.Add("@ImgPath", path, DbType.String, direction: ParameterDirection.Input);
				_parameter.Add("@IsPublic", model.IsPublic, DbType.Boolean, direction: ParameterDirection.Input);
				_response.data = await DatabaseHub.QueryAsync<object>(storedProcedureName: @"dbo.[usp_tblImageInfoUpload]", parameters: _parameter, dbName: DatabaseName.imgService);
			}
			catch (Exception ex)
			{
				_response.error = ex.Message;
			}
			finally
			{
				_response.message = "Image Upload Successful.";
				_responseResult.GetLoadResult(_response);
			}
			return _response;

		}

		public async Task<Response> UnauthorizedAccess(string FromDate, string ToDate, int limit, int offset)
		{
			try
			{
				DynamicParameters _parameter = new DynamicParameters();
				_parameter.Add("@FromDate", FromDate.Trim(), DbType.String, direction: ParameterDirection.Input);
				_parameter.Add("@ToDate", ToDate.Trim(), DbType.String, direction: ParameterDirection.Input);
				_parameter.Add("@limit", limit, DbType.Int32, direction: ParameterDirection.Input);
				_parameter.Add("@offset", offset, DbType.Int32, direction: ParameterDirection.Input);
				_response.data = await DatabaseHub.QueryAsync<object>(storedProcedureName: @"dbo.[usp_UnauthorizedAccessHistory]", parameters: _parameter, dbName: DatabaseName.imgService);
			}
			catch (Exception ex)
			{
				_response.error = ex.Message;
			}
			finally
			{
				_response.message = "Unauthorized Access User Loaded.";
				_responseResult.GetLoadResult(_response);
			}
			return _response;
		}
	}
}
