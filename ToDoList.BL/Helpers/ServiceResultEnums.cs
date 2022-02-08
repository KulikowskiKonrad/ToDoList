using System.ComponentModel;

namespace ToDoList.BL.Helpers
{
    public enum DeleteFileResult
    {
        Ok = 1,
        [Description("File not found")]
        FileNotFound = 2
    }
}