using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace Player
{
    public class PlayerSearcher : MonoBehaviour
    {
        public GameObject player;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in objects)
            {
                if (!obj.activeInHierarchy) continue;
                player = obj.transform.parent.gameObject;   // 何故か子オブジェクトを取得する
                break;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
