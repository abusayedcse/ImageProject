using Connection.Database.Dapper;
using ImageService.IService;
using ImageService.Model;

namespace ImageService.Service
{
    public class CreateUserService : BasePoint, ICreateUser<MCreateUser>
    {
        private readonly Response _response;
        private readonly ResponseResult _responseResult;
       // private readonly ImageService _imageSave;
        private const string ImageFolder = @"\UserImage\";
        public CreateUserService()
        {
            _response = new Response();
            _responseResult = new ResponseResult();
           // _imageSave = new ImageService();
        }
        public async Task<Response> SaveUpdate(MCreateUser model)
        {
            try
            {
                #region "For Image"
                // UserModel saveModel = new UserModel();
                //if (model.imageURL  !=null)
                //{
                //    //// Update Process
                //    if (!string.IsNullOrEmpty(model.ImageName) && model.UserID > 0)
                //    {
                //        if (!_imageSave.RemoveImage(model.ImageName, ImageFolder))
                //        {
                //            _response.message = "Image Remove Fail From Local File";
                //        }
                //    }
                //    model.imageURL = null;
                //}
                //if (string.IsNullOrEmpty(model.imageURL))
                //{
                //  string uniqueId =  Guid.NewGuid().ToString();
                //    saveModel.imageURL = _imageSave.GetImageUrl(ImageFolder, model.ImageName, uniqueId);
                //    saveModel.ImageName = model.ImageName;
                //}
                //saveModel.FullName = model.FullName;
                //saveModel.UserName = model.UserName;
                //saveModel.Password = model.Password;
                //saveModel.phone = model.phone;
                //saveModel.AddedBy = model.AddedBy;
                #endregion

                _response.data = await DatabaseHub.QueryAsync<MCreateUser, object>(storedProcedureName: @"dbo.[usp_tblUser_InsertUpdate]",
                    model: model, dbName: DatabaseName.imgService);
            }
            catch (System.Exception ex)
            {
                // _imageSave.RemoveImage(saveModel.ImageName, ImageFolder);
                _response.error = ex.Message;
            }
            finally
            {
                _responseResult.GetLoadResult(_response);
            }
            return _response;
        }
    }
}
