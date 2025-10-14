// ServerBootstrap.cs
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using System;

public class ServerBootstrap : MonoBehaviour
{
    [SerializeField] ushort port = 7777;

    void Start()
    {
#if UNITY_SERVER
        StartDedicated();
#else
        var args = Environment.GetCommandLineArgs();
        if (Array.Exists(args, a => a.Equals("-dedicated", StringComparison.OrdinalIgnoreCase)))
            StartDedicated();
#endif
    }

    void StartDedicated()
    {
        var nm = NetworkManager.Singleton;
        var utp = (UnityTransport)nm.NetworkConfig.NetworkTransport;
        utp.SetConnectionData("0.0.0.0", port);
        nm.StartServer();
        Debug.Log($"[DEDICATED] Listening UDP {port}");
    }
}
