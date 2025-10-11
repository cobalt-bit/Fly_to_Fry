using UnityEngine;

namespace Obstacles
{
    public class PlaceObstacle : MonoBehaviour
    {
        [SerializeField] GameObject[] obstacles;
        [SerializeField] SpacesData data;
        private ScriptableDatas.PlayerMoveData playerMoveData;

        private float timer = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
        }
    }

    public class SpacesData
    {
        public float diffcultySpeed, resetInterval;
    }
}