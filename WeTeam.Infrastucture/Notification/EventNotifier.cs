using System;
using System.Threading;
using System.Threading.Tasks;
using FcmSharp;
using FcmSharp.Model.Options;
using FcmSharp.Model.Topics;
using FcmSharp.Requests.Topics;
using FcmSharp.Settings;
using WeTeam.Infrastucture.Rest;

namespace WeTeam.Infrastucture.Notification
{
    public class EventNotifier
    {
        public static async Task Notify(string key, string text)
        {
            RestService.Call("https://fcm.googleapis.com/fcm/send", key, text);
        }
    }
}
