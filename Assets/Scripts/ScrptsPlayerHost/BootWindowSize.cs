using UnityEngine;
using UnityEngine.Rendering;

public class BootWindowSize : MonoBehaviour
{
    [SerializeField] int width = 960;
    [SerializeField] int height = 540;

    void Awake()
    {
        // En servidor/headless no hay ventana ni GPU: no tocar APIs de pantalla
        if (Application.isBatchMode || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
            return;

        // Ventana en cliente/host
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(width, height, FullScreenMode.Windowed);
        // Nota: el "resizable" se activa en Player Settings → Resolution and Presentation → **Resizable Window**
    }
}
