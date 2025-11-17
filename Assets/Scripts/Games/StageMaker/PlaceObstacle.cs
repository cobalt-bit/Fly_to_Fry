using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Score;

namespace Obstacles
{
    public class PlaceObstacle : MonoBehaviour
    {
        [SerializeField] GameObject[] obstacles;
        [Serializable] struct MinToMax{ public float min, max; }
        [SerializeField] MinToMax coolTime_range;
        [SerializeField] MinToMax intervalRange;
        [SerializeField] uint incrementStepByScore;
        [SerializeField] float intervalFromPlayer;
        [SerializeField] ScriptableDatas.PlayerBaseYDataScript baseY;
        [SerializeField] ScoreCalculator scoreCalculator;
        [SerializeField] OilPlacer oilPlacer;
        private IsLandingManeger playerLanding;
        private Transform playerTransform;
        private Rigidbody2D playerRigidbody;
        private List<GameObject> useObjects = new List<GameObject>();

        private int nextPlaceTime;
        private bool isFirst = true;

        void ResetForNext()
        {
            nextPlaceTime = UnityEngine.Random.Range((int)(coolTime_range.min * 1000), (int)(coolTime_range.max * 1000));
            if (isFirst) nextPlaceTime = (int)MathF.Max(nextPlaceTime, 3000);
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
            if (!oilPlacer) throw new System.NullReferenceException("oilPlacer");

            Place().Forget();
        }

        async UniTask Place()
        {
            await UniTask.WaitForEndOfFrame();
            while (true)
            {
                ResetForNext();
                await UniTask.WaitUntil(() => useObjects.Count == 0 && playerLanding.isLanding);
                await UniTask.Delay(nextPlaceTime);
                uint obstacleNumber = (uint)UnityEngine.Random.Range(1, scoreCalculator.score / incrementStepByScore + 2);
                if (isFirst) obstacleNumber = 1;
                await UniTask.SwitchToMainThread();
                float basePositionX = playerTransform.position.x + intervalFromPlayer;
                float interval = UnityEngine.Random.Range(intervalRange.min, intervalRange.max);
                oilPlacer.SetNeedPosistion(basePositionX + interval * obstacleNumber);
                int obstacleType = UnityEngine.Random.Range(0, obstacles.Length);

                for (uint i = 0; i < obstacleNumber; ++i)
                {
                    float positionX = i * interval + basePositionX;     //Don't change!
                    GameObject obstacle = Instantiate(obstacles[obstacleType]);
                    obstacle.transform.position = new Vector2(positionX, baseY.surfaceHeight);
                    obstacle.GetComponent<TouchedPlayerTrigger>().playerRigid = playerRigidbody;
                    obstacle.GetComponent<Rigidbody2D>().linearVelocityY = 0;
                    obstacle.GetComponent<AddScoreWhenPass>()?.Initialize(playerTransform, scoreCalculator);
#if UNITY_EDITOR
                    obstacle.AddComponent<FriedSpeedDebuger>();
#endif
                    useObjects.Add(obstacle);

                    //AttachLucky(obstacle, isFirst);
                }
                isFirst = false;
            }
        }

        void AttachLucky(GameObject obstacle, bool isFirst = false)
        {
            if (isFirst) return;
            if (UnityEngine.Random.Range(0f, 1f) < 0.01f) obstacle.AddComponent<LuckyEvent.FriedLuckyJumper>();
        }


        void Replace()
        {
            // コレクション操作を行うからLinqが使えなかった;;
            for (int i = 0; i < useObjects.Count; i++)
            {
                if (playerTransform.position.x - useObjects[i].transform.position.x < 8) continue;
                Destroy(useObjects[i]);
                useObjects.RemoveAt(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Replace();
        }
    }
}