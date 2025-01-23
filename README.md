# UnityHttpClient
**UnityHttpClient** is a lightweight HTTP client designed for Unity projects. Simplifies sending HTTP request (GET, POST, PUT, DELETE) and handling responses, making API integration easy.

## Features
- 🛠️ **Support all HTTP method**: support with `GET`, `POST`, `PUT`, `DELETE` requests.
- 📦 **JSON Serialization**: Automatically handles object serialization to JSON.
- 🔄 **Reusable Singleton**: Globally accessible throughout your Unity project.
- 🎯 **Customizable Callbacks**: Handle success and error responses with ease.
- 🚀 **Comfortable**: Simple to integrate into any Unity project.

## Installation
1. Clone this repository or download the `UnityHttpClient.cs` to your Unity project.
2. Ready to go!!

## Data Model
```cs
// Example data model
[System.Serializable]
public class MessageData
{
    public string message;
    public MessageData(string message)
    {
        this.message = message;
    }
}
```
You can customize your own data model in `UnityHttpClient.cs` as you wish to covered your need.

## Example Usage
```cs
public class HttpClientUsage : MonoBehaviour
{
    private void Start()
    {
        MessageData postData = new MessageData("Hello from Unity!"); // Converted data to json

        UnityHttpClient.Instance.SendRequest(
            "http://localhost:4000/send",
            "POST",
            postData, 
            onSuccess: (response) => {
              Debug.Log("POST Response: " + response),
            }
            onError: (error) => { 
              Debug.LogError("POST Error: " + error)
            }
        );
    }
}
```
