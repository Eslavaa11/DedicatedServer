// PauseManager.cs
using Unity.Netcode;
using UnityEngine;

public class PauseManager : NetworkBehaviour
{
    private readonly NetworkVariable<bool> paused = new(writePerm: NetworkVariableWritePermission.Server);

    void OnEnable() => paused.OnValueChanged += OnPauseChanged;
    void OnDisable() => paused.OnValueChanged -= OnPauseChanged;

    void OnPauseChanged(bool _, bool now) => Time.timeScale = now ? 0f : 1f;

    // Llamar desde un botón solo visible al host
    [ContextMenu("Toggle Pause")]
    public void TogglePause()
    {
        if (!IsServer) return;
        paused.Value = !paused.Value;
    }
}
