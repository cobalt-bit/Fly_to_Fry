using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Obstacles
{
    public class PlaceObstacle : MonoBehaviour
    {
        [SerializeField] GameObject[] obstacles;
        [SerializeField] SpacesData data;
        [Serializable] struct MinToMax{ public float min, max; }
        [SerializeField] MinToMax coolTime_range;
        [SerializeField] MinToMax intervalRange;
        [SerializeField] ScriptableDatas.PlayerBaseYDataScript baseY;
        private IsLandingManeger playerLanding;
        private ScriptableDatas.PlayerMoveData playerMoveData;
        private Transform playerTransform;
        private Rigidbody2D playerRigidbody;
        private bool isWaiting = false;
        private List<GameObject> useObjects = new List<GameObject>();
        private float latestPosition;

        private float timer = 0;
        private float nextPlaceTime;

        void ResetForNext()
        {
            timer = 0;
            nextPlaceTime = UnityEngine.Random.Range(coolTime_range.min, coolTime_range.max);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameObject player = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player;
            if (player == null) throw new System.NullReferenceException("Player");
            playerLanding = player.GetComponent<IsLandingManeger>();
            //playerMoveData = player.GetComponent<Player.PlayerMover>().moveData;
            playerTransform = player.transform;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            ResetForNext();
        }

        async UniTask Place()
        {
            if (isWaiting) return;
            isWaiting = true;
            await UniTask.WaitUntil(() => playerLanding.isLanding);
            await UniTask.NextFrame();
            uint obstacleNumber = (uint)UnityEngine.Random.Range(1, 5);
            float basePositionX = playerTransform.position.x + 20;
            float interval = UnityEngine.Random.Range(intervalRange.min, intervalRange.max);
            for (uint i = 0; i < obstacleNumber; ++i)
            {
                float positionX = i * interval + basePositionX;     //Don't change!
                if (positionX <= latestPosition || positionX - latestPosition < 5) break;
                GameObject obstacle = Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Length - 1)]);
                latestPosition = positionX;
                obstacle.transform.position = new Vector2(positionX, baseY.surfaceHeight);
                useObjects.Add(obstacle);
            }
            isWaiting = false;
            ResetForNext();
        }

        void Replace()
        {
            for (int i = 0; i < useObjects.Count; i++)
            {
                if (playerTransform.position.x - useObjects[i].transform.position.x < 10) continue;
                Destroy(useObjects[i]);
                //unuseObjects.Enqueue(useObjects[i]);
                useObjects.RemoveAt(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer < nextPlaceTime) return;
            Place();
            Replace();
        }
    }

    public class SpacesData
    {
        public float diffcultySpeed, resetInterval;
    }
}