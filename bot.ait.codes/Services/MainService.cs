using System;
using System.Collections.Generic;
using RestSharp;

namespace bot.ait.codes.Services
{
    public class MainService
    {
        private static readonly TimeSpan UpdateInterval = TimeSpan.FromMinutes(20);
        public static readonly List<Group> Groups = new List<Group>();

        public static void Log(string message)
        {
        }
        private static readonly RestClient GroupClient = new RestClient("http://14.0.20.45:8888/groups.txt")
        {
            FollowRedirects = true,

        };
        private static readonly RestRequest GroupRequest = new RestRequest(Method.GET);
    }
}
