using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace Player
{
    public class PlayerSearcher : MonoBehaviour
    {
        public GameObject player;

        private void Awake()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in objects)
            {
                if (!obj.activeInHierarchy) continue;
                Debug.Log(obj);
                //player = obj.transform.parent.gameObject;   // 何故か子オブジェクトを取得する
                player = obj;
                break;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
