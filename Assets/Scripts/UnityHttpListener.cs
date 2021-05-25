using System;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class UnityHttpListener : MonoBehaviour
{
    private HttpListener listener;
    private Thread listenerThread;
    private readonly string url = "http://localhost:8080/";

    void Start()
    {
        listener = new HttpListener();
        listener.Prefixes.Add(url);
        listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
        listener.Start();

        listenerThread = new Thread(StartListener);
        listenerThread.Start();
        Debug.Log("Server Started");
    }

    private void StartListener()
    {
        while (true)
        {
            var result = listener.BeginGetContext(ListenerCallback, listener);
            result.AsyncWaitHandle.WaitOne();
        }
    }

    private void ListenerCallback(IAsyncResult result)
    {
        var context = listener.EndGetContext(result);

        Debug.Log("Method: " + context.Request.HttpMethod);
        Debug.Log("LocalUrl: " + context.Request.Url.LocalPath);

        if (context.Request.HttpMethod == "GET")
        {
            Debug.Log("Sending data...");
            string logContent = LoggingManager.GetContainer();
            context.Response.ContentType = "application/json";
            byte[] encodedPayload = new UTF8Encoding().GetBytes(logContent);
            context.Response.ContentLength64 = encodedPayload.Length;
            System.IO.Stream output = context.Response.OutputStream;
            output.Write(encodedPayload, 0, encodedPayload.Length);
        }
        context.Response.Close();
    }
}