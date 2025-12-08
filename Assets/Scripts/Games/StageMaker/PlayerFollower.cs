using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] Vector2 slipspeed;
    [SerializeField] Vector2 offset;
    [SerializeField] ScriptableDatas.PlayerBaseYDataScript playerBaseYData;
    Transform target;
    Vector3 beforePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player.transform;
        beforePosition = target.position;
        //if (playerBaseYData != null) transform.position = new Vector3(transform.position.x, playerBaseYData.surfaceHeight, transform.position.z);

        if (slipspeed.x < 0 || slipspeed.x > 1 || slipspeed.y < 0 || slipspeed.y > 1) Debug.LogError("SlipSpeed is not between 0 and 1.");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(
            (target.position.x - beforePosition.x) * slipspeed.x, 
            (target.position.y - beforePosition.y) * slipspeed.y, 
            transform.position.z);
        beforePosition = target.position;
    }
}
