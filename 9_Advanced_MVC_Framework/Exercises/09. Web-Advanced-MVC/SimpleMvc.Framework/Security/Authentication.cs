
namespace SimpleMvc.Framework.Security
{
    public class Authentication
    {
        public Authentication()
        {
            IsAuthenticated = false;
        }

        public Authentication(string name)
        {
            this.IsAuthenticated = true;
            this.Name = name;
        }

        public string Name { get; private set; }

        public bool IsAuthenticated { get; private set; }
    }
}
