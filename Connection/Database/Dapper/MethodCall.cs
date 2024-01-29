namespace Connection.Database.Dapper
{
	public class MethodCall
	{
		public string? CreatedBy { get; set; }
		public bool IsActive { get; set; }
	}

	public class Response
	{
		public string message { get; set; }
		public string error { get; set; }
		public List<object> datalist { get; set; }
		public dynamic data { get; set; }
		public bool flag { get; set; } = false;

	}

	public class ResponseResult
	{
		public Response GetLoadResult(Response obj)
		{
			if (string.IsNullOrEmpty(obj.error))
			{
				obj.flag = true;
			}
			else
			{
				obj.flag = false;
			}
			return obj;
		}
	}

	public interface IGetService<T> where T : class
	{
		Task<Response> GetAll();
	}

	public interface IIGetService<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
	}

	public interface IFindService<T> where T : class
	{
		Task<Response> FindByModel(T model);
	}

	public interface IFindByIdService<T> where T : class
	{
		Task<Response> IFindByIdService(object id, object param);
	}
	public interface ISaveService<T> where T : class
	{
		Task<long> Save(T model);
	}
	public interface ISaveUpdateService<T> where T : class
	{
		Task<Response> SaveUpdate(T model);
	}


	public interface IUpdateService<T> where T : class
	{
		T Update(T model);
	}
	public interface IDeleteService<T> where T : class
	{
		Task<Response> DeleteByModel(T model);
	}

	public interface IDeleteByIdService<T> where T : class
	{
		Task<Response> DeleteById(object id, object userId);
	}
}
