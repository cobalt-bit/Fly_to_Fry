using UnityEngine;

public class FriedSpeedDebuger : MonoBehaviour
{
    [SerializeField] 
    new Rigidbody2D rigidbody;
    Transform player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!TryGetComponent(out rigidbody)) throw new System.NullReferenceException("fried rigid");
        player = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > transform.position.x) return;
        if (rigidbody.linearVelocity.magnitude <= 4) return;
        Debug.Log("Fried position:" + transform.position.x);
        UnityEditor.EditorApplication.isPaused = true;
    }
}
