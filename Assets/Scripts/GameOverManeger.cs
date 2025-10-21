using UnityEngine;
using UnityEngine.Events;

public class GameOverManeger : MonoBehaviour
{
    public UnityEvent events;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        events.AddListener(StopGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        events.Invoke();
    }

    private void StopGame()
    {
        GameObject playerObject = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player;
        Rigidbody2D playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
        playerRigidbody.Sleep();
        playerRigidbody.linearVelocity = Vector2.zero;
        playerObject.GetComponent<Player.PlayerMover>().enabled = false;

        GameObject.Find("ObstaclePlacer").GetComponent<Obstacles.PlaceObstacle>().enabled = false;
    }
}
