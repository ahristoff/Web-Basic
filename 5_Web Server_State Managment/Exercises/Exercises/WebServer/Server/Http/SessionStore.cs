
namespace WebServer.Server.Http
{
    using System.Collections.Concurrent;

    public static class SessionStore //4 Session
    {
        //session ID -> HttpSession
        public const string SessionCookieKey = "MY_SID";
        public const string CurrentUserKey = "Current_User";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions
            = new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
        => sessions.GetOrAdd(id, _ =>
        {
            return new HttpSession(id);
        });
    }
}
