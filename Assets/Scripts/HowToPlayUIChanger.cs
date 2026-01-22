using TMPro;
using UnityEngine;

public class HowToPlayUIChanger : MonoBehaviour
{
    [SerializeField] TMP_Text shower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WebGLPlayer)
            shower.text += "※スペースキーでも同様の操作ができます。";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
