using UnityEngine;

public class IsLandingManeger : MonoBehaviour
{
    public bool isLanding { get; private set; } = false;
    private short landingObjectCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Oil")) return;
        landingObjectCounter++;
        if (landingObjectCounter < 1) Debug.LogError("IsLandingManeger Overflow!");
        isLanding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Oil")) return;
        landingObjectCounter--;
        if (landingObjectCounter > 0) return;
        isLanding = false;
    }
}
