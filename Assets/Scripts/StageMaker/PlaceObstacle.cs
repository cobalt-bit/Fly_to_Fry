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
        [SerializeField] ScriptableDatas.PlayerBaseYDataScript baseY;
        private IsLandingManeger playerLanding;
        private ScriptableDatas.PlayerMoveData playerMoveData;
        private Transform playerTransform;
        private Rigidbody2D playerRigidbody;
        private bool isWaiting = false;
        private List<GameObject> useObjects = new List<GameObject>();

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
            Debug.Log("Call Place");
            await UniTask.WaitUntil(() => playerLanding.isLanding);
            await UniTask.NextFrame();
            uint obstacleNumber = (uint)UnityEngine.Random.Range(1, 5);
            float basePositionX = playerTransform.position.x + 20;
            for (uint i = 0; i < obstacleNumber; ++i)
            {
                GameObject obstacle = Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Length - 1)]);
                float positionX = i * UnityEngine.Random.Range(2.5f, 3) + basePositionX;    //Don't change!
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