using UnityEngine;

namespace ScriptableDatas
{
    [CreateAssetMenu(fileName = "CmPerUnitScript", menuName = "Scriptable Objects/ScoreCalculator/cmPerUnitData")]
    public class CmPerUnitScript : ScriptableObject
    {
        [Min(0)] public float cmPerUnit;

        private void OnValidate()
        {
            if (cmPerUnit <= 0) Debug.LogError("cmPreUnit is not over 0.");
        }
    }
}
