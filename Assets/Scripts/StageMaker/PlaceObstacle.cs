using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace Obstacles
{
    public class PlaceObstacle : MonoBehaviour
    {
        [SerializeField] GameObject[] obstacles;
        [SerializeField] SpacesData data;
        [SerializeField] ScriptableDatas.PlayerBaseYDataScript baseY;
        private IsLandingManeger playerLanding;
        private ScriptableDatas.PlayerMoveData playerMoveData;
        private Transform playerTransform;
        private Rigidbody2D playerRigidbody;
        private bool isWaiting = false;

        private float timer = 0;
        private float nextPlaceTime;

        void ResetForNext()
        {
            timer = 0;
            nextPlaceTime = UnityEngine.Random.Range(3, 5);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameObject player = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().gameObject;
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
            GameObject obstacle = Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Length - 1)]);
            float positionX = playerRigidbody.linearVelocityX * UnityEngine.Random.Range(0.2f, 3) + playerTransform.position.x;     //Don't change!
            obstacle.transform.position = new Vector2(positionX, baseY.surfaceHeight);
            isWaiting = false;
            ResetForNext();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer > nextPlaceTime) return;
            Place();
        }
    }

    public class SpacesData
    {
        public float diffcultySpeed, resetInterval;
    }
}