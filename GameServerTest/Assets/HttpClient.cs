using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Tools
{
    public static class HttpClient 
    {
        public static async Task<T> Get<T>(string endpoint)
        {
            UnityWebRequest request = CreateRequest(endpoint);
            request.SendWebRequest();

            while (!request.isDone)
                await Task.Delay(10);
            return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
        }
        
        public static async Task<T> Post<T>(string endpoint, object payload)
        {
            UnityWebRequest request = CreateRequest(endpoint, RequestType.POST, payload);
            request.SendWebRequest();

            while (!request.isDone)
                await Task.Delay(10);
            return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
        }

        private static UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET,
            object data = null)
        {
            UnityWebRequest request = new UnityWebRequest(path, type.ToString());

            if (data != null)
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }

        private static void AttachHeader(UnityWebRequest request, string key, string value)
        {
            request.SetRequestHeader(key, value);
        }
        
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
    }
}

