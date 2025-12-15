using UnityEngine;

public class FrameRateSetting : MonoBehaviour
{
    [SerializeField, Min(0)] int targetFPS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = targetFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
