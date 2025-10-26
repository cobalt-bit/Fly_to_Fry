using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] TMP_Text OutPut;
    [SerializeField] ScriptableDatas.CmPerUnitScript cmPerUnitData;
    Transform playerTransform;
    [NonSerialized] public float score = 0;
    float immediatelyPrecedingPositionX;
    private Queue<float> scoreEffectEvents = new(0);
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
        score = playerTransform.position.x - immediatelyPrecedingPositionX;
        score *= cmPerUnitData.unitPer_cm;

        OutPut.text = string.Format("SCORE:{0:0.0}", score);
    }

    void calculateScore()
    {

    }
}
