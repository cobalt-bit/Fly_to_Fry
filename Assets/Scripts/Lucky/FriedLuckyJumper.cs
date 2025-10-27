using UnityEngine;

namespace LuckyEvent
{
    public class FriedLuckyJumper : MonoBehaviour
    {
        new Rigidbody2D rigidbody;
        Transform playerTransform;
        [SerializeField] float JumpForce = 40;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            playerTransform = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x - playerTransform.position.x > 7) return;
            rigidbody.AddForceY(JumpForce, ForceMode2D.Impulse);
            Destroy(this);
        }
    }
}
