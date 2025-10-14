// HostPanel.cs
using Unity.Netcode;
using UnityEngine;

public class HostPanel : MonoBehaviour
{
    Vector2 scroll;
    GlobalPause pause;   // <- referencia al GlobalPause

    void Awake()
    {
        pause = FindObjectOfType<GlobalPause>(); // busca el de la escena
    }

    void OnGUI()
    {
        var nm = NetworkManager.Singleton;
        if (nm == null || !nm.IsServer) return; // solo lo ve el host/servidor

        GUILayout.BeginArea(new Rect(10, 10, 260, 320), GUI.skin.window);
        GUILayout.Label("Host Panel");

        // --- Botón de Pausa ---
        if (GUILayout.Button("Toggle Pause", GUILayout.Width(120), GUILayout.Height(24)))
        {
            pause = pause ? pause : FindObjectOfType<GlobalPause>();
            pause?.TogglePause();
        }
        GUILayout.Space(6);

        // --- Lista de jugadores + Kick (lo que ya tenías) ---
        scroll = GUILayout.BeginScrollView(scroll, GUILayout.Height(220));
        foreach (var kv in NetworkLobby.Clients)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(kv.Value);
            if (kv.Key != nm.LocalClientId && GUILayout.Button("Kick", GUILayout.Width(60)))
                nm.DisconnectClient(kv.Key);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }
}
