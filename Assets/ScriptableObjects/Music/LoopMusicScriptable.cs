using UnityEngine;
using UnityEngine.Audio;

namespace ScriptableDatas
{
    [CreateAssetMenu(fileName = "LoopMusicScriptable", menuName = "Scriptable Objects/LoopMusicScriptable")]
    public class LoopMusicScriptable : ScriptableObject
    {
        public AudioClip audio_begin, audio_loop;
    }
}
