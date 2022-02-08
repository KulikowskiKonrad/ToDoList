using System;
using System.IO;
using Microsoft.Extensions.Options;
using ToDoList.BL.Helpers;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.BL.ServiceInterfaces;
using FileInfo = ToDoList.Models.Entity.FileInfo;
using Path = System.IO.Path;

namespace ToDoList.BL.Services
{
    public class LocalFileService : IFileService
    {
        private readonly IRepository<FileInfo> _fileInfoRepository;
        private readonly FileSettings _fileSettings;

        public LocalFileService(IRepository<FileInfo> fileInfoRepository, IOptions<FileSettings> fileSettingsAccessor)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileSettings = fileSettingsAccessor.Value;
        }

        public void Upload(Stream file, string fileName, Guid userId, string catalogName)
        {
            var directoryPath = !string.IsNullOrEmpty(catalogName)
                ? Path.Combine(_fileSettings.Path, catalogName)
                : _fileSettings.Path;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = !string.IsNullOrEmpty(catalogName)
                ? Path.Combine(_fileSettings.Path, catalogName, fileName)
                : Path.Combine(_fileSettings.Path, fileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            var fileInfo = new FileInfo()
            {
                OriginalName = fileName,
                Size = file.Length,
                AddDate = DateTime.UtcNow,
                UserId = userId,
                ModificationDate = DateTime.UtcNow,
                RelativePath = !string.IsNullOrEmpty(catalogName)
                    ? Path.Combine(catalogName, fileName)
                    : fileName
            };

            _fileInfoRepository.Add(fileInfo);
        }

        public DeleteFileResult Delete(Guid id)
        {
            var file = _fileInfoRepository.GetById(id);
            if (file != null)
            {
                var filePath = Path.Combine(_fileSettings.Path, file.RelativePath);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                file.IsDeleted = true;
                file.ModificationDate = DateTime.UtcNow;

                _fileInfoRepository.Update(file);

                return DeleteFileResult.Ok;
            }
            else
            {
                return DeleteFileResult.FileNotFound;
            }
        }

        public byte[] GetFileBytes(Guid id)
        {
            var file = _fileInfoRepository.GetById(id);
            var filePath = Path.Combine(_fileSettings.Path, file.RelativePath);
            byte[] result = File.ReadAllBytes(filePath);

            return result;
        }
    }
}