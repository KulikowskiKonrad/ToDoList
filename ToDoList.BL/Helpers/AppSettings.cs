namespace ToDoList.BL.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public EmailProviderType EmailProviderType { get; set; }
    }

    public enum EmailProviderType
    {
        Smtp = 1,
        Fake = 2,
        Db = 3,
        Ethernal = 4
    }
}