using UnityEngine;

namespace FriedObject
{
    public class AddScoreWhenPass : MonoBehaviour
    {
        Transform target;
        ScoreCalculator scoreCalc;
        float offset;
        float addScore;

        public void Initialize(Transform target, ScoreCalculator calculator, float add, float offset = 0)
        {
            this.target = target;
            this.scoreCalc = calculator;
            this.offset = offset;
            this.addScore = add;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (!target) throw new System.NullReferenceException("Target Transform");
        }

        // Update is called once per frame
        void Update()
        {
            if (target.position.x < transform.position.x + offset) return;
            scoreCalc.calculateScore(addScore);
            Destroy(this);
        }
    }
}
