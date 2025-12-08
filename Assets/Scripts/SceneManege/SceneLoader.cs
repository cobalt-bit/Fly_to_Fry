using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] asyncLoadSceneNames;
    [SerializeField] Dictionary<string, AsyncOperation> asyncLoadSceneData = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (asyncLoadSceneNames != null && asyncLoadSceneNames.Length > 0)
        {
            foreach (var name in asyncLoadSceneNames)
            {
                asyncLoadSceneData.Add(name, SceneManager.LoadSceneAsync(name, LoadSceneMode.Single));
                asyncLoadSceneData[name].allowSceneActivation = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ReloadThisScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        Reload(thisScene).Forget();
    }

    private async UniTask Reload(Scene scene)
    {
        await SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Single);
    }

    public void CallAsyncSceneLoad(string sceneName)
    {
        asyncLoadSceneData[sceneName].allowSceneActivation = true;
    }
}
