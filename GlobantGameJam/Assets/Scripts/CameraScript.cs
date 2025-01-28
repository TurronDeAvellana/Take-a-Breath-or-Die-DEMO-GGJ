using UnityEngine;

public class CameraScript : MonoBehaviour
{   
    private float LastWindowAspect = (float)Screen.width / (float)Screen.height;

    private void Start()
    {
        Adjust();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
            Screen.fullScreen = !Screen.fullScreen;
            Adjust();
        }   

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        float ActualWindowAspect = (float)Screen.width / (float)Screen.height;
        if (LastWindowAspect != ActualWindowAspect)
        {
            LastWindowAspect = ActualWindowAspect;
            Adjust();
        }
        
    }

    public void Adjust()
    {
        float TargetAspect = 711.0f / 325.0f;
        float WindowAspect = (float)Screen.width / (float)Screen.height;
        float ScaleHeight = WindowAspect / TargetAspect;
        Camera camera = GetComponent<Camera>();
        
        if (ScaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = ScaleHeight;
            rect.x = 0;
            rect.y = (1.0f - ScaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float ScaleWidth = 1.0f / ScaleHeight;
            Rect rect = camera.rect;
            rect.width = ScaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - ScaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
