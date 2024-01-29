using Connection.Database.Dapper;
using ImageService.Helper;
using ImageService.IService;
using ImageService.Model;

namespace ImageService.Service
{
	public class ImageUploadService : BasePoint, IImageUpload<ImageUploadRequest>
	{
		private readonly Response _response;
		private readonly ResponseResult _responseResult;
		private readonly ImageHelper _imageSave;
		private const string ImageFolder = @"\Upload\";
		public ImageUploadService()
		{
			_response = new Response();
			_responseResult = new ResponseResult();
			_imageSave = new ImageHelper();
		}
		//public async Task<Response> SaveImage(IFormFile file, MImageInfo model)
		//{
		//	try
		//	{
		//		if (model.ImageId > 0)
		//		{
		//			//// Update Process
		//			if (!string.IsNullOrEmpty(model.ImagePath) && model.ImageName != null)
		//			{
		//				if (!_imageSave.RemoveImage(model.ImagePath, ImageFolder))
		//				{
		//					_response.message = "Image Remove Fail From Local File";
		//				}
		//			}
		//			model.ImagePath = null;
		//		}
		//		if (string.IsNullOrEmpty(model.ImagePath) && model.ImageName != null)
		//		{
		//			//model.ImagePath = _imageSave.GetImageUrl(ImageFolder, model.ImageFile.Base64, model.ImageId.ToString());
		//		}
		//		_response.data = await DatabaseHub.QueryAsync<MImageInfo, object>(storedProcedureName: @"dbo.[HomeBanner_InsertUpdate]",
		//			model: model, dbName: DatabaseName.imgService);
		//	}
		//	catch (System.Exception ex)
		//	{
		//		_imageSave.RemoveImage(model.ImagePath, ImageFolder);
		//		_response.error = ex.Message;
		//	}
		//	finally
		//	{
		//		_responseResult.GetLoadResult(_response);
		//	}
		//	return _response;
		//}

		public async Task<Response> SaveUpdate(ImageUploadRequest model)
		{

			try
			{
				if (model.model.ImageId > 0)
				{
					//// Update Process
					if (!string.IsNullOrEmpty(model.model.ImagePath) && model.model.ImageName != null)
					{
						if (!_imageSave.RemoveImage(model.model.ImagePath, ImageFolder))
						{
							_response.message = "Image Remove Fail From Local File";
						}
					}
					model.model.ImagePath = null;
				}
				if (string.IsNullOrEmpty(model.model.ImagePath) && model.model.ImageName != null)
				{
					//model.ImagePath = _imageSave.GetImageUrl(ImageFolder, model.ImageFile.Base64, model.ImageId.ToString());
				}
				_response.data = await DatabaseHub.QueryAsync<MImageInfo, object>(storedProcedureName: @"dbo.[HomeBanner_InsertUpdate]",
					model: model.model, dbName: DatabaseName.imgService);
			}
			catch (System.Exception ex)
			{
				_imageSave.RemoveImage(model.model.ImagePath, ImageFolder);
				_response.error = ex.Message;
			}
			finally
			{
				_responseResult.GetLoadResult(_response);
			}
			return _response;
		}

		//public async Task<Response> GetAll()
		//{
		//	try
		//	{
		//		_response.data = await DatabaseHub.QueryAsync<object>(
		//			 storedProcedureName: @"dbo.[HomeBanner_Get_All]", dbName: DatabaseName.imgService);
		//	}
		//	catch (System.Exception ex)
		//	{
		//		_response.error = ex.Message;
		//	}
		//	finally
		//	{
		//		_responseResult.GetLoadResult(_response);
		//	}
		//	return _response;
		//}

		//public async Task<Response> SaveUpdate(MImageInfo model)
		//{
		//	try
		//	{
		//		if (model.ImageId > 0)
		//		{
		//			//// Update Process
		//			if (!string.IsNullOrEmpty(model.ImagePath) && model.ImageFile != null)
		//			{
		//				if (!_imageSave.RemoveImage(model.ImagePath, ImageFolder))
		//				{
		//					_response.message = "Image Remove Fail From Local File";
		//				}
		//			}
		//			model.ImagePath = null;
		//		}
		//		if (string.IsNullOrEmpty(model.ImagePath) && model.ImageFile != null)
		//		{
		//			model.ImagePath = _imageSave.GetImageUrl(ImageFolder, model.ImageFile.Base64, model.ImageId.ToString());
		//		}
		//		_response.data = await DatabaseHub.QueryAsync<MImageInfo, object>(storedProcedureName: @"dbo.[HomeBanner_InsertUpdate]",
		//			model: model, dbName: DatabaseName.imgService);
		//	}
		//	catch (System.Exception ex)
		//	{
		//		_imageSave.RemoveImage(model.ImagePath, ImageFolder);
		//		_response.error = ex.Message;
		//	}
		//	finally
		//	{
		//		_responseResult.GetLoadResult(_response);
		//	}
		//	return _response;
		//}

		//public async Task<Response> DeleteByModel(MImageInfo model)
		//{
		//	try
		//	{
		//		_imageSave.RemoveImage(model.ImagePath, ImageFolder);
		//		DynamicParameters _parameter = new DynamicParameters();
		//		_parameter.Add("@ImageId", model.ImageId, DbType.Int32);
		//		_parameter.Add("@UserId", model.UserId, DbType.String);
		//		_response.data = await DatabaseHub.QueryAsync<object>(
		//			storedProcedureName: @"dbo.[HomeBanner_Delete]", parameters: _parameter, dbName: DatabaseName.imgService);
		//	}
		//	catch (Exception ex)
		//	{
		//		_response.error = ex.Message;
		//	}
		//	finally
		//	{
		//		_responseResult.GetLoadResult(_response);
		//	}
		//	return _response;
		//}




	}
}
