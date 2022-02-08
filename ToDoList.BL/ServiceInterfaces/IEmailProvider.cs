namespace ToDoList.BL.ServiceInterfaces
{
    public interface IEmailProvider
    {
        void Send(string to, string subject, string html);
    }
}