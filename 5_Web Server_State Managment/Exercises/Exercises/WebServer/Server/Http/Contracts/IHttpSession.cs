
namespace WebServer.Server.Http.Contracts
{
    public interface IHttpSession   //1 Session
    {
        string Id { get; }

        object Get(string key);

        T Get<T>(string key); //8 Session -> zaradi Cast in Homecontroller

        bool Contains(string key);

        void Add(string key, object value);

        void Clear();

        //bool IsAuthenticated();
    }
}
