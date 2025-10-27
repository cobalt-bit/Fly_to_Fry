using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] TMP_Text OutPut;
    [SerializeField] ScriptableDatas.CmPerUnitScript cmPerUnitData;
    Transform playerTransform;
    public float score { get; protected set; } = 0;
    float immediatelyPrecedingPositionX;
    public bool stop { protected get; set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerobj = GameObject.Find("PlayerSearcher").GetComponent<Player.PlayerSearcher>().player;
        playerTransform = playerobj.transform;
        immediatelyPrecedingPositionX = playerTransform.position.x;

        if (OutPut == null) throw new System.NullReferenceException("Score Display Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        score += (playerTransform.position.x - immediatelyPrecedingPositionX) * cmPerUnitData.cmPerUnit;
        immediatelyPrecedingPositionX = playerTransform.position.x;

        OutPut.text = string.Format("SCORE : {0:0.0}", score);
    }

    public void calculateScore(float add)
    {
        score += add;
    }
}
