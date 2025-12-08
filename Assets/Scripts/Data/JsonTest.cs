using System;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveData.JsonSavere.Save(new Test(2), "Test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
class Test
{
    public int testdata;
    public Test(int testdata)
    {
        this.testdata = testdata;
    }
}