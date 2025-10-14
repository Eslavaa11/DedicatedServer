// GlobalPause.cs
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class GlobalPause : NetworkBehaviour
{
    [SerializeField] KeyCode toggleKey = KeyCode.P;

    // value, readPerm, writePerm  (sin parámetros con nombre)
    private readonly NetworkVariable<bool> paused =
        new NetworkVariable<bool>(false,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    Texture2D _pixel;

    void Awake() => _pixel = Texture2D.whiteTexture;

    void OnEnable()  => paused.OnValueChanged += OnPauseChanged;
    void OnDisable() => paused.OnValueChanged -= OnPauseChanged;

    void Update()
    {
        // Solo el host/servidor alterna la pausa
        if (IsServer && Input.GetKeyDown(toggleKey))
            TogglePause();
    }

    void OnPauseChanged(bool _, bool now)
    {
        Time.timeScale = now ? 0f : 1f;
        AudioListener.pause = now; // opcional
    }

    public void TogglePause()
    {
        if (!IsServer) return;
        paused.Value = !paused.Value;
    }

    void OnGUI()
    {
        if (!paused.Value) return;

        // Overlay oscuro
        var prev = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.5f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _pixel);
        GUI.color = prev;

        // Texto "PAUSA" centrado
        var style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = Mathf.RoundToInt(Screen.height * 0.12f)
        };
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(2, 2, Screen.width, Screen.height), "PAUSA", style);
        GUI.contentColor = Color.white;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "PAUSA", style);
        GUI.contentColor = Color.white;
    }
}
