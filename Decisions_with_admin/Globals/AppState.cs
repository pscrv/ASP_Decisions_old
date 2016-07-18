using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decisions_with_admin.Globals
{
    public static class AppState
    {
        public enum Connection { Untried, Success, NoConnection }

        public static Connection LastConnectionAttempt { get; set; } = Connection.Untried;

        public static List<string> HttpRequestExceptions = new List<string>();
        public static List<string> UnknownExceptions = new List<string>();

    }
}