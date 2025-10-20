using UnityEditor;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Exit();
#endif
    }
}
