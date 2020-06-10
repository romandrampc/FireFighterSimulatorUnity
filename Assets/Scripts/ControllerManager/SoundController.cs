using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfButton
{
    actionButton,
    playSoundButton
}

public class SoundController : MonoBehaviour
{
    public static SoundController instanceSound;

    [SerializeField] AudioSource SoundEffectSource;

    List<AudioSource> listOfAudioSource =new List<AudioSource>();

    private void Awake()
    {
        if (instanceSound != null && instanceSound != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instanceSound = this;
        }
    }

    public void AudioPlay(AudioClip clip,float volume,bool isLoop)
    {
        Debug.Log("Test2");
        AudioSource tempSoundEffect = new GameObject().AddComponent<AudioSource>();
        tempSoundEffect.volume = volume;
        tempSoundEffect.loop = isLoop;
        listOfAudioSource.Add(tempSoundEffect);
        tempSoundEffect.clip = clip;
        if (!tempSoundEffect.isPlaying)
        {
            tempSoundEffect.Play();
        }
        StartCoroutine(WaitForSound(tempSoundEffect,clip));
    }

    IEnumerator WaitForSound(AudioSource aS,AudioClip c)
    {
        float lengthC = c.length;
        yield return new WaitForSeconds(lengthC);
        for (int i =0; i<listOfAudioSource.Count;i++)
        {
            if (listOfAudioSource[i] == aS)
            {
                listOfAudioSource.Remove(aS);
                Destroy(aS);
            }
        }
    }
}
