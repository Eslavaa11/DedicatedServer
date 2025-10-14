using Unity.Netcode;
using UnityEngine;

public class ColorByOwner : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        var mr = GetComponent<MeshRenderer>();
        if (mr == null) return;
        float hue = (OwnerClientId % 10) / 10f;
        mr.material.color = Color.HSVToRGB(hue, 0.8f, 0.9f);
    }
}
