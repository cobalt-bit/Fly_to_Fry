using Cysharp.Threading.Tasks;
using UnityEngine;

public class FriedSpeedDebuger : MonoBehaviour
{
    [SerializeField] 
    new Rigidbody2D rigidbody;
    Transform player;
    bool touchedOil = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!TryGetComponent(out rigidbody)) throw new System.NullReferenceException("fried rigid");
        player = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player.transform;
        Search().Forget();
    }

    async UniTask Search()
    {
        await UniTask.NextFrame();
        await UniTask.WaitForFixedUpdate();
        if (touchedOil) return;
        Debug.Log("Fried position:" + transform.position.x);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Oil")) return;
        touchedOil = true;
    }
}
