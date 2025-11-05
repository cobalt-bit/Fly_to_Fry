using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public class PlaceObstacle : MonoBehaviour
    {
        [SerializeField] GameObject[] obstacles;
        [Serializable] struct MinToMax{ public float min, max; }
        [SerializeField] MinToMax coolTime_range;
        [SerializeField] MinToMax intervalRange;
        [SerializeField] ScriptableDatas.PlayerBaseYDataScript baseY;
        [SerializeField] ScoreCalculator scoreCalculator;
        private IsLandingManeger playerLanding;
        private Transform playerTransform;
        private Rigidbody2D playerRigidbody;
        private bool isWaiting = false;
        private List<GameObject> useObjects = new List<GameObject>();

        private float timer = 0;
        private float nextPlaceTime;
        private bool isFirst = true;

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
            uint obstacleNumber = (uint)UnityEngine.Random.Range(1, scoreCalculator.score / 50 + 3);
            if (isFirst) obstacleNumber = 1;
            float basePositionX = playerTransform.position.x + 20;
            float interval = UnityEngine.Random.Range(intervalRange.min, intervalRange.max);
            for (uint i = 0; i < obstacleNumber; ++i)
            {
                float positionX = i * interval + basePositionX;     //Don't change!
                GameObject obstacle = Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Length - 1)]);
                obstacle.transform.position = new Vector2(positionX, baseY.surfaceHeight);
                obstacle.GetComponent<TouchedPlayerTrigger>().playerRigid = playerRigidbody;
                useObjects.Add(obstacle);

                if (UnityEngine.Random.Range(0f, 1f) < 0.01f) obstacle.AddComponent<LuckyEvent.FriedLuckyJumper>();
            }
            isWaiting = false;
            isFirst = false;
            ResetForNext();
        }

        void Replace()
        {
            // コレクション操作を行うからLinqが使えなかった
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
            Replace();
            timer += Time.deltaTime;
            if (timer < nextPlaceTime) return;
            Place();
        }
    }
}