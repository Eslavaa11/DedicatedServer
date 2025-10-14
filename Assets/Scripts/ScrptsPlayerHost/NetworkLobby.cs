// NetworkLobby.cs
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class NetworkLobby : MonoBehaviour
{
    public static readonly Dictionary<ulong, string> Clients = new();

    NetworkManager nm;

    void Awake()
    {
        nm = GetComponent<NetworkManager>();
    }

    void OnEnable()
    {
        // Evita errores en editor cuando no está en Play, o si falta el NM
        if (!Application.isPlaying || nm == null) return;

        nm.OnClientConnectedCallback += OnClientConnected;
        nm.OnClientDisconnectCallback += OnClientDisconnected;
    }

    void OnDisable()
    {
        if (nm == null) return;
        nm.OnClientConnectedCallback -= OnClientConnected;
        nm.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    void OnClientConnected(ulong clientId)
    {
        Clients[clientId] = $"Player {clientId}";
        Debug.Log($"Client connected: {clientId}");
    }

    void OnClientDisconnected(ulong clientId)
    {
        Clients.Remove(clientId);
        Debug.Log($"Client disconnected: {clientId}");
    }
}
