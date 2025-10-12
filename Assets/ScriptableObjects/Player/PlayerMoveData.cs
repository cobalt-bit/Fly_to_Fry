using System;
using UnityEngine;

namespace ScriptableDatas
{
    [CreateAssetMenu(fileName = "PlayerMoveData", menuName = "Scriptable Objects/Player/Move")]
    public class PlayerMoveData : ScriptableObject
    {
        [Serializable] public struct MoveStruct { public float force, maxSpeed, breakForce; }
        public MoveStruct move;
        [Serializable] public struct JumpStruct { public float timeToVertex, height, timeToSus, grideDownSpeed; }
        public JumpStruct jump;
        [Serializable] public struct StartMoveStruct { public float maxSpeed, force; }
        public StartMoveStruct startMove;
        //public float mass;
    }
}
