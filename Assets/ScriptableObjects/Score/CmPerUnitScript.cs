using UnityEngine;

namespace ScriptableDatas
{
    [CreateAssetMenu(fileName = "CmPerUnitScript", menuName = "Scriptable Objects/ScoreCalculator/cmPerUnitData")]
    public class CmPerUnitScript : ScriptableObject
    {
        [Min(0.001f)] public float unitPer_cm;
    }
}
