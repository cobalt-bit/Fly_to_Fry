using Cysharp.Threading.Tasks;
using ScriptableDatas;
using UnityEngine;

public class LoopMusicPlayer : MonoBehaviour
{
    [SerializeField] LoopMusicScriptable[] playList;
    AudioSource audioSource;
    LoopMusicScriptable selected;

    void MusicSelect() => selected = playList[Random.Range(0, playList.Length)];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) throw new System.NullReferenceException("AudioSource");
        MusicSelect();
        if (audioSource.playOnAwake) Play().Forget();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async UniTask Play()
    {
        if (selected.audio_begin != null)
        {
            audioSource.clip = selected.audio_begin;
            audioSource.Play();
            await UniTask.WaitWhile(() => audioSource.isPlaying);
        }
        if (audioSource == null) return;
        audioSource.clip = selected.audio_loop;
        audioSource.Play();
        audioSource.loop = true;
    }
}
