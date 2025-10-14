using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class QuickStartHUD : MonoBehaviour
{
    [SerializeField] string address = "127.0.0.1";
    [SerializeField] ushort port = 7777;

    void Awake()
    {
        var nm = NetworkManager.Singleton;
        if (nm != null)
        {
            nm.OnClientConnectedCallback += id => Debug.Log($"[NGO] Connected: {id}");
            nm.OnClientDisconnectCallback += id => Debug.LogWarning($"[NGO] Disconnected: {id}");
        }
    }

    void OnGUI()
    {
        const int W = 160, H = 30, P = 10;
        var nm = NetworkManager.Singleton;
        if (nm == null) return;

        GUILayout.BeginArea(new Rect(P, P, 240, 200));

        if (!nm.IsClient && !nm.IsServer)
        {
            if (GUILayout.Button("Start Host", GUILayout.Width(W), GUILayout.Height(H)))
            {
                SetConn();
                nm.StartHost();
            }
            if (GUILayout.Button("Start Client", GUILayout.Width(W), GUILayout.Height(H)))
            {
                SetConn();
                nm.StartClient();
            }
            if (GUILayout.Button("Start Server", GUILayout.Width(W), GUILayout.Height(H)))
            {
                SetConn();
                nm.StartServer();
            }
        }
        else
        {
            if (GUILayout.Button("Shutdown", GUILayout.Width(W), GUILayout.Height(H)))
                nm.Shutdown();
        }

        GUILayout.EndArea();
    }

    void SetConn()
    {
        var utp = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        utp.SetConnectionData(address, port);
        Debug.Log($"[NGO] Using {address}:{port}");
    }
}
