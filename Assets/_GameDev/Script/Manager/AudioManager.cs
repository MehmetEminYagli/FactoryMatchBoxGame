using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip pop;

    private AudioSource _standard, _increasing;

    [Header("Audio Mixer"), SerializeField]
    private AudioMixer masterMixer;

    private AudioMixerGroup _musicGroup;
    private AudioMixerGroup _soundGroup;


    private void Start()
    {
        Initialize();
    }

    public AudioManager Initialize()
    {
        var mainCameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");

        _standard = mainCameraGameObject.AddComponent<AudioSource>();
        _increasing = mainCameraGameObject.gameObject.AddComponent<AudioSource>();

        _musicGroup = masterMixer.FindMatchingGroups("Music")[0];
        _soundGroup = masterMixer.FindMatchingGroups("Sound Effects")[0];

        _standard.outputAudioMixerGroup = _soundGroup;
        _increasing.outputAudioMixerGroup = _soundGroup;

        UpdateAudioStates();

        return this;
    }

    public void UpdateAudioStates()
    {
        SetMusicState(DataManager.Music);
        SetSoundState(DataManager.Sound);
    }

    public void SetMusicState(bool state)
    {
        masterMixer.SetFloat("Music_Volume", Mathf.Log10(state ? 1 : 0) * 20f);
    }

    public void SetSoundState(bool state)
    {
        masterMixer.SetFloat("Sound_Volume", Mathf.Log10(state ? 1 : 0) * 20f);
    }

    public void PlayUIButtonClick()
    {
        Play(pop);
    }

    private void Play(AudioClip clip)
    {
        if (!DataManager.Sound)
            return;

        _standard.PlayOneShot(clip);
    }

    private void PlayWithPitch(AudioClip clip, float pitch)
    {
        if (!DataManager.Sound)
            return;

        _increasing.pitch = pitch;
        _increasing.PlayOneShot(clip);
    }
}