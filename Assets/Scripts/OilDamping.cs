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
        if (!collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody)) return;
        rigidbody.AddForceX(-dampingX * rigidbody.linearVelocityX);
        rigidbody.AddForceY(-dampingY * rigidbody.linearVelocityY);
    }
}
