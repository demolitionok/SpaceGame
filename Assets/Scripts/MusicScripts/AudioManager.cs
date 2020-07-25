using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobExpressions;

public class AudioManager : MonoBehaviour
{
    public List<Music> MusicList;
    public AudioSource audioContainer;

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
                    SetMusicText();
                    audioContainer.clip = currentMusic.clip;
                }
            }
        } 
    }
    public Music currentMusic { get { return MusicList[_musicIndex]; } }
    public Canvas MusicPlayerCanvas;

    public string musicFolderPath;
    string[] matchingFiles;

    IEnumerator LoadAudio(string path)
    {
        path = "file://" + path;
        WWW URL = new WWW(path);
        yield return URL;

        MusicList.Add(new Music { clip = URL.GetAudioClip(false, true), name = Path.GetFileName(path) });
        if (audioContainer.clip == null)
             musicIndex++;

    }
    public void SetMusicText()// доставать автора название и тд из файла
    {
        MusicPlayerCanvas.transform.Find("CurrentMusicName").gameObject.GetComponent<Text>().text = currentMusic.name;
    }
    public void SetDropDown() 
    {
        Dropdown MusDropdown = MusicPlayerCanvas.transform.Find("MusicDropdown").gameObject.GetComponent<Dropdown>();
        List<string> musicNameList = new List<string>();
        foreach (Music mus in MusicList) 
        {
            musicNameList.Add(mus.name);
        }
        MusDropdown.AddOptions(musicNameList);
    }
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
    public void MusicDropdown(int ind) 
    {

    }
    public void Start()
    {
        musicFolderPath = musicFolderPath.Replace('\\', '/');
        audioContainer = gameObject.GetComponent<AudioSource>();
        matchingFiles = Glob.Files($"{musicFolderPath}", "*.wav").ToArray();

        foreach (string path in matchingFiles)
        {
            StartCoroutine(LoadAudio(musicFolderPath + "/" + path));
        }
        if (MusicList.Count != 0)
        {
            SetDropDown();
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
