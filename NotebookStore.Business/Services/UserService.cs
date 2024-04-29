namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

	public async Task<bool> CreateUser(UserDto userDto)
	{
		var user = mapper.Map<User>(userDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Users.Create(user);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante la creazione dell'utente", ex);
		}
	}

	public async Task<bool> UpdateUser(UserDto userDto)
	{
		var user = mapper.Map<User>(userDto);

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Users.Update(user);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'aggiornamento dell'utente", ex);
		}
	}

	public async Task<bool> DeleteUser(int id)
	{
		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Users.Delete(id);
			await unitOfWork.SaveAsync();
			unitOfWork.CommitTransaction();
			return true;
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			throw new Exception("Errore durante l'eliminazione dell'utente", ex);
		}
	}

	public async Task<bool> UserExists(int id)
	{
		return await unitOfWork.Users.Find(id) != null;
	}
}
