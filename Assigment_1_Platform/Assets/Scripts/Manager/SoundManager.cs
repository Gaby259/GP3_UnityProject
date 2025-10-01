using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    private static SoundManager Instance;
    private static AudioSource _audioSource;
    private static SoundLibrary _soundLibrary; 
    private Slider volumeSlider;//Manage volume 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _soundLibrary = GetComponent<SoundLibrary>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
     // volumeSlider.onValueChanged.AddListener(delegate{OnVolumeChanged();}); When the volume slides is created use this
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = _soundLibrary.GetRandom(soundName);
        if (audioClip != null)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }

    private static void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    private void OnVolumeChanged()
    {
        SetVolume(volumeSlider.value);
    }
}