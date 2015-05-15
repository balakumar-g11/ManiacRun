/**
 * File : SoundManager.cs
 * Author : Balakumar Marimuthu
 * Use : The script is used to manage the sounds in the game
 **/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundGroup
    {
        public AudioClip audioClip;
        public string soundName;
    }

    public AudioSource runningSound;

    public AudioSource bgmSound;

    public List<SoundGroup> sound_List = new List<SoundGroup>();

    public static SoundManager instance;

    public void Start()
    {
        instance = this;
        StartCoroutine(StartBGM());
    }

    public void PlaySound(string _soundName)
    {
        if (GameAttribute.gameAttribute.isVolumeMute)
            return;
        AudioSource.PlayClipAtPoint(sound_List[FindSound(_soundName)].audioClip, Camera.main.transform.position);
    }

    private int FindSound(string _soundName)
    {
        int i = 0;
        while (i < sound_List.Count)
        {
            if (sound_List[i].soundName == _soundName)
            {
                return i;
            }
            i++;
        }
        return i;
    }

    public void resumeRunningSound()
    {
    }

    void ManageBGM()
    {
        if (GameAttribute.gameAttribute.isVolumeMute)
            return;

        StartCoroutine(StartBGM());
    }

    void audioMute()
    {
        GameAttribute.gameAttribute.isVolumeMute = true;

        bgmSound.Stop();
    }

    void audioPlay()
    {
        StartBGM();
    }

    IEnumerator StartBGM()
    {
        yield return new WaitForSeconds(0.5f);

        bgmSound.Play();
    }

}
