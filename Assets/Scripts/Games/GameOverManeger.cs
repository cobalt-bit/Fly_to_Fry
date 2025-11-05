using UnityEngine;
using UnityEngine.Events;

public class GameOverManeger : MonoBehaviour
{
    public UnityEvent events;
    bool AlreadyGoal = false;

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
        if (AlreadyGoal) return;
        Debug.Log("GameOver!");
        AlreadyGoal = true;
        events.Invoke();
    }

    private void StopGame()
    {
        GameObject playerObject = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player;
        playerObject.GetComponent<Player.PlayerMover>().enabled = false;
        GameObject.Find("ObstaclePlacer").GetComponent<Obstacles.PlaceObstacle>().enabled = false;
        GameObject.Find("ScoreManeger").GetComponent<ScoreCalculator>().stop = true;
    }
}
