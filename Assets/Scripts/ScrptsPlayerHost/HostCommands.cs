// HostCommands.cs  (ponerlo en un GameObject con NetworkObject)
using UnityEngine;
using Unity.Netcode;

public class HostCommands : NetworkBehaviour
{
    public static HostCommands Instance;

    void Awake() => Instance = this;

    public override void OnNetworkSpawn()
    {
        if (IsServer && !NetworkObject.IsSpawned) NetworkObject.Spawn();
    }

    [ClientRpc] void SetPausedClientRpc(bool pause) => Time.timeScale = pause ? 0f : 1f;

    public void TogglePauseForAll(bool pause)
    {
        if (!IsServer) return;
        SetPausedClientRpc(pause); // afecta host y clientes
    }

    public void Kick(ulong clientId)
    {
        if (!IsServer || clientId == NetworkManager.Singleton.LocalClientId) return;
        NetworkManager.Singleton.DisconnectClient(clientId);
    }
}
