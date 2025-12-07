using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WaitForRelease : MonoBehaviour
{
    public UnityEvent actions;
    PlayerInput input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (input == null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Controller");
            foreach(GameObject obj in objects)
            {
                if (obj.TryGetComponent(out input)) break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallWait(string actionName)
    {
        Wait(actionName).Forget();
    }
    async UniTask Wait(string actionName, CancellationToken cancellation = default)
    {
        await UniTask.WaitWhile(() => input.actions[actionName].IsPressed(), cancellationToken: cancellation);
        actions.Invoke();
    }
}
