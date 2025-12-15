using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreCalculator : MonoBehaviour
    {
        [SerializeField] TMP_Text OutPut;
        [SerializeField] ScriptableDatas.CmPerUnitScript cmPerUnitData;
        Transform playerTransform;
        public float score { get; protected set; } = 0;
        float precedingPositionX;
        public bool stop { protected get; set; } = false;

        readonly object calculate_obj = new object();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameObject playerobj = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player;
            playerTransform = playerobj.transform;
            precedingPositionX = playerTransform.position.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (stop) return;
            score += (playerTransform.position.x - precedingPositionX) * cmPerUnitData.cmPerUnit;
            precedingPositionX = playerTransform.position.x;

            ShowScore();
        }

        void ShowScore()
        {
            if (OutPut == null)
            {
                if (OutPut == null) throw new System.NullReferenceException("Score Display Text");
                return;
            }
            OutPut.text = string.Format("{0:0.0}", score);
        }

        public void calculateScore(float add)
        {
            lock (calculate_obj)
            {
                score += add;
            }
        }
    }
}
