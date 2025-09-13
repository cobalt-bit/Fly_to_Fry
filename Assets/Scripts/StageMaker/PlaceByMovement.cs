using UnityEngine;

public class PlaceByMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    public GameObject placedObject;
    [Min(0)] public float interval;
    [SerializeField] float offset;
    [SerializeField] float positionY;

    private float previosPlacePositionX = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previosPlacePositionX -= offset;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = target.position.x - previosPlacePositionX;
        while (moveDistance >= interval)
        {
            GameObject currentPlacedObject = Instantiate(placedObject);
            currentPlacedObject.transform.position = new Vector2(previosPlacePositionX + interval + offset, positionY);
            Debug.Log("Oil Place:" + currentPlacedObject.transform.position);
            previosPlacePositionX += interval;
            moveDistance -= interval;
        }
    }
}
