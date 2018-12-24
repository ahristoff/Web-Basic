using System;

using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Http
{
    using System.Collections.Concurrent;

    public static class SessionStore //4 Session
    {
        //session ID -> HttpSession
        public const string SessionCookieKey = "MY_SID";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions
            = new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id) //get session by id
        => sessions.GetOrAdd(id, _ =>
        //get session, if session not exist, create new session with this id
        // sessions.GetOrAdd(id, _ => if here is parameter that not be used in func body,
        //it will replace from "_"
        {
            return new HttpSession(id);
        });
    }
}
