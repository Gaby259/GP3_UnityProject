using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string, List<AudioClip>> _soundDictionary;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        _soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
        {
            _soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioclips;
        }
    }

    public AudioClip GetRandom(string name)
    {
        if (_soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = _soundDictionary[name];
            if (audioClips.Count > 0)
            {
                return audioClips[Random.Range(0, audioClips.Count)];
            }
        }
        return null;
    }
}
[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public List<AudioClip> audioclips;
}                                        
