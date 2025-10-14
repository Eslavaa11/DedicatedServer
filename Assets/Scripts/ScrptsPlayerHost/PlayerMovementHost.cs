using Unity.Netcode;
using UnityEngine;

public class PlayerMovementHost : NetworkBehaviour
{
    public float speed = 5f;
    void Update()
    {
        if (!IsOwner) return;
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0f, v) * speed * Time.deltaTime, Space.World);
    }
}
