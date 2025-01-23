using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A HTTP client library for sending requests in Unity.
/// </summary>
public class UnityHttpClient : MonoBehaviour
{
    private static UnityHttpClient _instance;

    /// <summary>
    /// Access the UnityHttpClient instance.
    /// </summary>
    public static UnityHttpClient Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject clientObject = new GameObject("UnityHttpClient");
                _instance = clientObject.AddComponent<UnityHttpClient>();
                DontDestroyOnLoad(clientObject);
            }
            return _instance;
        }
    }

    /// <summary>
    /// Sends HTTP request to the specified URL.
    /// </summary>
    /// <typeparam name="T">Type of the data to send (Optional).</typeparam>
    /// <param name="url">The URL endpoint.</param>
    /// <param name="method">The HTTP method (GET, POST, PUT, DELETE).</param>
    /// <param name="data">The data to send as JSON (for POST/PUT).</param>
    /// <param name="onSuccess">Callback for successful responses.</param>
    /// <param name="onError">Callback for errors.</param>
    public void SendRequest<T>(
        string url,
        string method,
        T data,
        System.Action<string> onSuccess,
        System.Action<string> onError)
    {
        string json = data != null ? JsonUtility.ToJson(data) : null;
        StartCoroutine(SendRequestCoroutine(url, method, json, onSuccess, onError));
    }

    private IEnumerator SendRequestCoroutine(
        string url,
        string method,
        string json,
        System.Action<string> onSuccess,
        System.Action<string> onError)
    {
        UnityWebRequest request = new UnityWebRequest(url, method.ToUpper());

        if (!string.IsNullOrEmpty(json) && (method == "POST" || method == "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.SetRequestHeader("Content-Type", "application/json");
        }

        request.downloadHandler = new DownloadHandlerBuffer();

        Debug.Log($"Sending {method.ToUpper()} request to: {url}");
        if (!string.IsNullOrEmpty(json))
        {
            Debug.Log($"Payload: {json}");
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Response from server: {request.downloadHandler.text}");
            onSuccess?.Invoke(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Request failed: {request.error}");
            onError?.Invoke(request.error);
        }
    }
}

/// <summary>
/// Example data model for sending JSON data.
/// </summary>
[System.Serializable]
public class MessageData
{
    public string message;

    public MessageData(string message)
    {
        this.message = message;
    }
}