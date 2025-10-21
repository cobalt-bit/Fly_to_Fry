using System;
using UnityEngine;

public class TouchedPlayerTrigger : MonoBehaviour
{
    public event Action events;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        events += GameObject.FindGameObjectWithTag("Finish").GetComponent<GameOverManeger>().GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        events();
    }
}
