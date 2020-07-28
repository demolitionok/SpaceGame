using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Text MusicText;
    public Dropdown MusicDropdown;
    public Animation PopupAnim;
    public List<Music> MusicList = MusicLoader.musicList;
    private AudioSource audioContainer;

    private int _musicIndex;
    public int musicIndex 
    {
        get { return _musicIndex; }
        set 
        {
            if (value != _musicIndex)
            {
                if (value > MusicList.Count - 1)
                {
                    _musicIndex = 0;
                }
                else if (value < 0)
                {
                    _musicIndex = MusicList.Count - 1;
                }
                else
                {
                    _musicIndex = value;
                }

                if (MusicList.Count != 0)
                {
                    OnMusicChange.Invoke();
                    audioContainer.clip = currentMusic.clip;
                }
            }
        } 
    }
    public Music currentMusic { get { return MusicList[_musicIndex]; } }
    public Canvas MusicPlayerCanvas;
    public UnityEvent OnMusicChange = new UnityEvent();

    public void SetMusicText()
    {
        MusicText.text = currentMusic.name;
    }
    //public void SetDropDown() 
    //{
    //    List<string> musicNameList = new List<string>();
    //    foreach (Music mus in MusicList) 
    //    {
    //        musicNameList.Add(mus.name);
    //    }
    //    MusicDropdown.AddOptions(musicNameList);
    //}
    //public void DropdownMusicChange(int ind) 
    //{
    //    musicIndex = ind;
    //}
    public void Stop() 
    {
        audioContainer.Pause();
    }
    public void Prev() 
    {
        musicIndex--;
    }
    public void Next() 
    {
        musicIndex++;
    }
    public void Play()
    {
        audioContainer.Play();
    }
    public void Start()
    {
        MusicLoader.songLoaded.AddListener(Next);
        audioContainer = gameObject.GetComponent<AudioSource>();
        if (MusicList.Count != 0)
        {
        }
    }
    public void Update()
    {
        if (audioContainer.clip != null && audioContainer.time >= audioContainer.clip.length - 0.1) 
        {
            musicIndex++;
        }
    }

}
