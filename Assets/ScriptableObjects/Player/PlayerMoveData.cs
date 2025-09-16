using System;
using UnityEngine;

namespace ScriptableDatas
{
    [CreateAssetMenu(fileName = "PlayerMoveData", menuName = "Scriptable Objects/PlayerMoveData")]
    public class PlayerMoveData : ScriptableObject
    {
        public Sprite foodTexture;
        [Serializable] public struct MoveStruct { public float force, maxSpeed; }
        public MoveStruct move;
        [Serializable] public struct JumpStruct
        {
            public float timeToVertex, height, timeToSus, grideDownSpeed;
        }
        public JumpStruct jump;
        public float mass;
    }
}
