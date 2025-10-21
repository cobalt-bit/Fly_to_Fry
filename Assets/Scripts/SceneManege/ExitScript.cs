using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ExitScript : MonoBehaviour
{
    PlayerInput input;

    private void Start()
    {
        getPlayerInput();
        input.actions["Exit"].started += ExitByInput;
    }

    void getPlayerInput()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Controller");
        foreach (GameObject obj in objs)
        {
            if (obj.TryGetComponent(out input)) break;
        }
        return;
    }

    void ExitByInput(InputAction.CallbackContext context) => Exit();

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
