namespace ToDoList.BL.ServiceInterfaces
{
    public interface IEmailProviderFabric
    {
        IEmailProvider GetEmailProvider();
    }
}