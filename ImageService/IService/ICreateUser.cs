using Connection.Database.Dapper;

namespace ImageService.IService
{
    public interface ICreateUser<T> :  ISaveUpdateService<T> where T : class { }
   

}
