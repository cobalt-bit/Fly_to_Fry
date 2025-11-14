using System.Collections.Generic;
using UnityEngine;

public class PlaceByMovement : MonoBehaviour
{
    protected Transform target;
    public GameObject placedObject;
    [SerializeField, Min(0)] protected float interval;
    [SerializeField] protected float aheadOffset;
    [SerializeField] protected float behindOffset;
    [SerializeField] protected Transform offsetTarget;

    protected float previosPlacePositionX = 0;
    List<GameObject> useObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previosPlacePositionX -= aheadOffset;
        target = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player.GetComponent<Transform>();
    }

    public virtual void place()
    {
        float moveDistance = target.position.x - previosPlacePositionX;
        while (moveDistance >= interval)
        {
            GameObject currentPlacedObject = Instantiate(placedObject);
            useObjects.Add(currentPlacedObject);
            currentPlacedObject.transform.position = new Vector2(previosPlacePositionX + interval + aheadOffset + offsetTarget.position.x, offsetTarget.position.y);
            previosPlacePositionX += interval;
            moveDistance -= interval;
        }
    }

    protected void AddObjectPool(GameObject obj) => useObjects.Add(obj);

    protected void Replace()
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
