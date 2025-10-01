using System.Collections.Generic;
using UnityEngine;

public class PlaceByMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    public GameObject placedObject;
    [Min(0)] public float interval;
    [SerializeField] float aheadOffset;
    [SerializeField] float behindOffset;
    [SerializeField] Transform offsetTarget;
    [SerializeField] float positionY;
    [SerializeField] float offsetX;

    private float previosPlacePositionX = 0;
    List<GameObject> useObjects = new List<GameObject>();
    //Queue<GameObject> unuseObjects = new Queue<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previosPlacePositionX -= aheadOffset;
    }

    void place()
    {
        float moveDistance = target.position.x - previosPlacePositionX;
        while (moveDistance >= interval)
        {
            GameObject currentPlacedObject;
            //if (!unuseObjects.TryDequeue(out currentPlacedObject))
            //{
            //    currentPlacedObject = Instantiate(placedObject);
            //    useObjects.Add(currentPlacedObject);
            //}
            currentPlacedObject = Instantiate(placedObject);
            useObjects.Add(currentPlacedObject);

            currentPlacedObject.transform.position = new Vector2(previosPlacePositionX + interval + aheadOffset + offsetTarget.position.x, offsetTarget.position.y);
            previosPlacePositionX += interval;
            moveDistance -= interval;
        }
    }

    void Replace()
    {
        for (int i = 0; i < useObjects.Count; i++)
        {
            if (target.position.x - useObjects[i].transform.position.x < behindOffset) continue;
            Destroy(useObjects[i]);
            //unuseObjects.Enqueue(useObjects[i]);
            useObjects.RemoveAt(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        place();
        Replace();
    }
}
