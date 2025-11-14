using UnityEngine;

public class AddScoreWhenPass : MonoBehaviour
{
    Transform target;
    ScoreCalculator scoreCalc;
    [SerializeField] float offset;
    [SerializeField] float addScore;

    public void Initialize(Transform target, ScoreCalculator calculator)
    {
        this.target = target;
        this.scoreCalc = calculator;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null) throw new System.NullReferenceException("Target Transform");
        if (scoreCalc == null) throw new System.NullReferenceException("Score Calculator");
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.x < transform.position.x + offset) return;
        scoreCalc.calculateScore(addScore);
        Destroy(this);
    }
}
