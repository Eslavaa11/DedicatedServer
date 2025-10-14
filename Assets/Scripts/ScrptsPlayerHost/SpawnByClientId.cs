// SpawnByClientId.cs
using Unity.Netcode;
using UnityEngine;
public class SpawnByClientId : NetworkBehaviour
{
    public float spacing = 2.5f;
    public Vector3 origin = new(0f, 0.5f, 0f);
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        int i = (int)OwnerClientId;
        transform.position = origin + new Vector3(i * spacing, 0f, 0f);
    }
}
