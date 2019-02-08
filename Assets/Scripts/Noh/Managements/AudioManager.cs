using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    private List<AudioSource> effectSource = new List<AudioSource>();
    private AudioSource[] bgmSource = new AudioSource[2];
    public AudioClip[] effectList;
    public AudioClip[] bgmList;
    private Coroutine bgmCorutine;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        for (int i = 0; i < 2; i++)
        {
            AudioSource audiosource = gameObject.AddComponent<AudioSource>();
            audiosource.playOnAwake = false;
            audiosource.loop = true;
            bgmSource[i] = audiosource;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayEffect(string _name)
    {
        if (effectSource.Count > 5)
            return;
        if (_name == null)
            return;
        foreach (AudioSource temp in effectSource)
        {
            if(!temp.isPlaying)
            {
                for (int i = 0; i < effectList.Length; i++)
                {
                    if (effectList[i].name == _name)
                    {
                        temp.clip = effectList[i];
                        
                        temp.Play();
                        return;
                    }
                }
            }
        }
        AudioSource audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.playOnAwake = false;
        audiosource.Stop();
        effectSource.Add(audiosource);
        PlayEffect(_name);
    }
    public void PlayBGM(string _name)
    {
        if (bgmSource[0].volume < 1.0f && bgmSource[0].volume > 0.0f)
            return;
        for(int i = 0; i < bgmList.Length;i++)
        {
            if(bgmList[i].name == _name)
            {
                bgmCorutine = StartCoroutine(FadeOutBGMCor(bgmList[i]));
                //bgmSource[0].clip = bgmList[i];
                //bgmSource[0].Play();
                return;
            }
        }
        Debug.Log("bgm사운드를 찾을수없음");
        return;
    }
    public void PlayBGM(int _count)
    {
        int i;
        for (i = 0; i < bgmSource.Length; i++)
        {
            if (bgmSource[i].volume == 0.0f)
            {
                break;
            }
        }
        bgmSource[i].volume = 1.0f;
        bgmSource[i].clip = bgmList[_count];
        bgmSource[i].Play();
    }

    public void PauseBGM()
    {
        int i;
        for (i = 0; i < bgmSource.Length; i++)
        {
            bgmSource[i].Pause();
        }
    }
    public void FadeOutBGM()
    {
        StartCoroutine(FadeOutBGMCor());
    }
    IEnumerator FadeOutBGMCor()
    {
        StopCoroutine(bgmCorutine);
        while (bgmSource[0].volume > 0.0f || bgmSource[1].volume > 0.0f)
        {
            yield return null;
            for (int i = 0; i < bgmSource.Length; i++)
            {
                if(bgmSource[i].volume > 0.0f)
                    bgmSource[i].volume -= Time.deltaTime;
            }
        }
        PauseBGM();
    }
    IEnumerator FadeOutBGMCor(AudioClip _newClip)
    {
        AudioSource temp;//켜져있는것
        AudioSource temp2;//켜야하는것
        if(bgmSource[0].isPlaying)
        {
            temp = bgmSource[0];
            temp2 = bgmSource[1];
        }else
        {
            temp = bgmSource[1];
            temp2 = bgmSource[0];
        }
        temp2.volume = 0.0f;
        temp2.clip = _newClip;
        temp2.Play();
        while (temp.volume > 0.0f || temp2.volume < 1.0f)
        {
            yield return null;
            temp.volume -= Time.deltaTime;
            temp2.volume += Time.deltaTime;
        }
        temp.Pause();
    }
}
