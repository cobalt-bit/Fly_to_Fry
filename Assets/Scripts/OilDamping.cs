using UnityEngine;

public class OilDamping : MonoBehaviour
{
    public float dampingX, dampingY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.rigidbody == null) return;
        //Debug.Log(collision.gameObject.name);
        //collision.rigidbody.AddForce(new Vector2(-dampingX * collision.rigidbody.linearVelocityX, -dampingY * collision.rigidbody.linearVelocityY));
        if (!collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody)) return;
        rigidbody.AddForceX(-dampingX * rigidbody.linearVelocityX);
        rigidbody.AddForceY(-dampingY * rigidbody.linearVelocityY);
    }
}
