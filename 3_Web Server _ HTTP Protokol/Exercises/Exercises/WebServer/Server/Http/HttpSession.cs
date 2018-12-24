
namespace WebServer.Server.Http
{
    using Contracts;
    using global::WebServer.Server.Common;
    using System.Collections.Generic;

    public class HttpSession : IHttpSession   //2 Session
    {
        private readonly IDictionary<string, object> values;//object is info for the session

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));
            this.Id = id;
            this.values = new Dictionary<string, object>();
        }

        public string Id { get; set; }

        public void Add(string key, object value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));

            this.values[key] = value;
        }

        public void Clear()
        {
            this.values.Clear();
        }

        public object Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                return null;
            }

            return this.values[key];
        }

        public T Get<T>(string key) //9 Session
            => (T)this.Get(key);
    }
}
