using System;
using System.IO;
using ToDoList.BL.Helpers;

namespace ToDoList.BL.ServiceInterfaces
{
    public interface IFileService
    {
        void Upload(Stream fileStream, string fileName, Guid userId, string catalogName = null);
        DeleteFileResult Delete(Guid id);
        byte[] GetFileBytes(Guid id);
    }
}