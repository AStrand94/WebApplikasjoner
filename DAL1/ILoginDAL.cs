namespace WebApplication3.DAL
{
    public interface ILoginDAL
    {
        bool userExists(string username, byte[] password);
    }
}