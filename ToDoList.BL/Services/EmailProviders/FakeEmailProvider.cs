using ToDoList.BL.ServiceInterfaces;

namespace ToDoList.BL.Services.EmailProviders
{
    public class FakeEmailProvider : IEmailProvider
    {
        public void Send(string to, string subject, string html)
        {
            throw new System.NotImplementedException();
        }
    }
}