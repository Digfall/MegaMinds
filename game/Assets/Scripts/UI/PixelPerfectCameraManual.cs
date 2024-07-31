using UnityEngine;

public class PixelPerfectCameraManual : MonoBehaviour
{
    public int targetWidth = 736;
    public int targetHeight = 414;
    public int pixelsPerUnit = 30;

    void Start()
    {
        AdjustCameraSize();
    }
    void Update()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        float targetAspect = (float)targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;

        if (windowAspect >= targetAspect)
        {
            Camera.main.orthographicSize = targetHeight / 2.0f / pixelsPerUnit;
        }
        else
        {
            float differenceInSize = targetAspect / windowAspect;
            Camera.main.orthographicSize = targetHeight / 2.0f / pixelsPerUnit * differenceInSize;
        }
    }
}
