namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using NotebookStore.Entities;

public class UserService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public UserService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<IEnumerable<UserDto>> GetUsers()
	{
		var users = await unitOfWork.Users.Read();

		return mapper.Map<IEnumerable<UserDto>>(users);
	}

	public async Task<UserDto> GetUser(int id)
	{
		var user = await unitOfWork.Users.Find(id);

		return mapper.Map<UserDto>(user);
	}

	public async Task CreateUser(UserDto userDto)
	{
		var user = mapper.Map<User>(userDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Users.Create(user);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task UpdateUser(UserDto userDto)
	{
		var user = mapper.Map<User>(userDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Users.Update(user);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task DeleteUser(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Users.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
		}
		catch (Exception)
		{
			unitOfWork.RollbackTransaction();
			throw;
		}
	}

	public async Task<bool> UserExists(int id)
	{
		return await unitOfWork.Users.Find(id) != null;
	}
}
