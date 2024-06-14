using System;
using NotebookStore.DAL;

namespace NotebookStore.Business
{
    //Func<StorageDto, UserDto, IUnitOfWork, StorageDto>
    public delegate StorageDto AssignPermission(StorageDto storageDto, UserDto currentUser, IUnitOfWork unitOfWork);

        //(StorageDto storageDto, UserDto currentUser, IUnitOfWork unitOfWork) =>
        //{
        //    var storage = unitOfWork.Storages.Find(storageDto.Id).Result;

        //    var createdBy = storage?.CreatedBy;

        //    storageDto.CanUpdate = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;
        //    storageDto.CanDelete = createdBy == currentUser.Id || currentUser.Role == "Admin" || createdBy == null;

        //    return storageDto;
        //};
}
