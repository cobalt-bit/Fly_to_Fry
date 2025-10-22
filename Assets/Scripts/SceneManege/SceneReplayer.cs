using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReplayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadThisScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        Reload(thisScene);
    }

    private async UniTask Reload(Scene scene)
    {
        await SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Single);
    }
}
