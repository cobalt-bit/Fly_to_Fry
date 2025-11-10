using Cysharp.Threading.Tasks;
using ScriptableDatas;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class LoopMusicPlayer : MonoBehaviour
{
    [SerializeField] LoopMusicScriptable[] playList;
    AudioSource audioSource;
    LoopMusicScriptable selected;

    void MusicSelect() => selected = playList[UnityEngine.Random.Range(0, playList.Length)];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) throw new System.NullReferenceException("AudioSource");
        MusicSelect();
        if (audioSource.playOnAwake) Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async UniTask Play()
    {
        if (selected.audio_begin)
        {
            audioSource.clip = selected.audio_begin;
            audioSource.Play();
            await Task.Delay(selected.beginLength_millise);
        }
        if (audioSource == null) return;
        audioSource.clip = selected.audio_loop;
        audioSource.Play();
        audioSource.loop = true;
    }
}
