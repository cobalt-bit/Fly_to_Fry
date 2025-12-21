using UnityEngine;

public class FrameRateSetting : MonoBehaviour
{
    [SerializeField, Min(0)] int targetFPS_smart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Application.isMobilePlatform) Application.targetFrameRate = targetFPS_smart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
