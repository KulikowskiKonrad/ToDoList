using System;

namespace ToDoList.BL.LogicInterfaces
{
    public interface ITokenLogic
    {
        string GenerateToken(Guid userId);
    }
}