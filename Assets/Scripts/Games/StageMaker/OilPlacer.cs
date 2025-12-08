using System;
using UnityEngine;

public class OilPlacer : PlaceByMovement
{
    float needPositionX;
    public void SetNeedPosistion(float position) => needPositionX = position;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        needPositionX = MathF.Max(target.position.x, needPositionX);
        Place();
        Replace();
    }

    public override void Place()
    {
        float moveDistance = needPositionX - previosPlacePositionX;
        while (moveDistance >= interval)
        {
            GameObject currentPlacedObject = Instantiate(placedObject);
            AddObjectPool(currentPlacedObject);
            currentPlacedObject.transform.position = new Vector2(previosPlacePositionX + interval + aheadOffset + offsetTarget.position.x, offsetTarget.position.y);
            previosPlacePositionX += interval;
            moveDistance -= interval;
        }
    }
}
