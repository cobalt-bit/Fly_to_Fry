using System;
using UnityEngine;

public class TouchedPlayerTrigger : MonoBehaviour
{
    public event Action events;
    [NonSerialized] public Rigidbody2D playerRigid;
    new private Rigidbody2D rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!TryGetComponent(out rigidbody)) throw new System.NullReferenceException("Rigidbody");
        if (!playerRigid)
            playerRigid = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player.GetComponent<Rigidbody2D>();
        events += GameObject.FindGameObjectWithTag("Finish").GetComponent<GameOverManeger>().GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (playerRigid == null) throw new System.NullReferenceException("Player Rigidbody couldn't get");
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        rigidbody.linearVelocity = Vector2.zero;
        playerRigid.bodyType = RigidbodyType2D.Kinematic;
        playerRigid.linearVelocity = Vector2.zero;

        events();
    }
}
