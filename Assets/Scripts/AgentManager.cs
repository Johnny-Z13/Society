using System;
using System.Text;
using System.Collections;
using NativeWebSocket;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    private WebSocket websocket;
    private bool isConnecting = false;
    private const int MAX_RECONNECT_ATTEMPTS = 5;
    private const float RECONNECT_DELAY = 3f;
    private int currentReconnectAttempts = 0;
    private const string SERVER_URL = "ws://localhost:8765";

    private async void Start()
    {
        await ConnectToServer();
    }

    private async void ConnectToServer()
    {
        if (isConnecting) return;

        try
        {
            isConnecting = true;
            Debug.Log($"Attempting to connect to server at {SERVER_URL}");

            websocket = new WebSocket(SERVER_URL);

            websocket.OnOpen += () =>
            {
                Debug.Log("Connected to AG2 server!");
                currentReconnectAttempts = 0;
                SendTestMessage();
            };

            websocket.OnMessage += (bytes) =>
            {
                string message = Encoding.UTF8.GetString(bytes);
                Debug.Log($"Message received from server: {message}");
                HandleMessage(message);
            };

            websocket.OnError += (error) =>
            {
                Debug.LogError($"WebSocket Error: {error}");
            };

            websocket.OnClose += (code) =>
            {
                Debug.Log($"WebSocket closed with code: {code}");
                HandleDisconnection();
            };

            await websocket.Connect();
        }
        catch (Exception e)
        {
            Debug.LogError($"Connection error: {e.Message}");
            HandleDisconnection();
        }
        finally
        {
            isConnecting = false;
        }
    }

    private void HandleDisconnection()
    {
        if (currentReconnectAttempts >= MAX_RECONNECT_ATTEMPTS)
        {
            Debug.LogError("Max reconnection attempts reached");
            return;
        }

        currentReconnectAttempts++;
        Debug.Log($"Attempting to reconnect... Attempt {currentReconnectAttempts}/{MAX_RECONNECT_ATTEMPTS}");
        StartCoroutine(ReconnectWithDelay());
    }

    private IEnumerator ReconnectWithDelay()
    {
        yield return new WaitForSeconds(RECONNECT_DELAY);
        ConnectToServer();
    }

    private void HandleMessage(string message)
    {
        try
        {
            // Add your message handling logic here
            Debug.Log($"Processing message: {message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error processing message: {e.Message}");
        }
    }

    private async void SendTestMessage()
    {
        try
        {
            if (websocket?.State == WebSocketState.Open)
            {
                string message = "Hello from Unity!";
                Debug.Log($"Sending: {message}");
                await websocket.SendText(message);
            }
            else
            {
                Debug.LogWarning("Cannot send message: WebSocket is not connected");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error sending message: {e.Message}");
        }
    }

    private void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        websocket?.DispatchMessageQueue();
        #endif
    }

    private async void OnApplicationQuit()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.Close();
        }
    }

    private async void OnDisable()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.Close();
        }
    }
}