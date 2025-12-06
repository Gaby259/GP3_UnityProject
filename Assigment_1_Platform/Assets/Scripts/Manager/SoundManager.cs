using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private static AudioSource _musicSource;
    private static AudioSource _sfxSource;
    private static SoundLibrary _library;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            AudioSource[] sources = GetComponents<AudioSource>();
            
            _musicSource = sources[0]; // Loop, música
            _sfxSource = sources[1];   // PlayOneShot, SFX

            _library = GetComponent<SoundLibrary>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🎵 MUSICA
    public static void PlayMusic(string name)
    {
        AudioClip clip = _library.GetRandom(name);
        if (clip == null) return;

        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public static void StopMusic()
    {
        _musicSource.Stop();
    }

    // 🔊 EFECTOS
    public static void PlaySFX(string name)
    {
        AudioClip clip = _library.GetRandom(name);
        if (clip == null) return;

        _sfxSource.PlayOneShot(clip);
    }
}