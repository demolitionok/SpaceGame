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
    public int musIndex = 0;
    public Music currentMusic;
    public Canvas MusicPlayerCanvas;

    public bool isStopped = false;
    public string musicFolderPath;
    string[] matchingFiles;

    IEnumerator LoadAudio(string path)
    {
        path = "file://" + path;
        WWW URL = new WWW(path);
        yield return URL;

        MusicList.Add(new Music { clip = URL.GetAudioClip(false, true), name = Path.GetFileName(path) });
        if (audioContainer.clip == null)
            audioContainer.clip = MusicList[MusicList.Count - 1].clip;
    }
    public void SetMusicText() 
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
        isStopped = true;
    }
    public void Prev() 
    {
        musIndex--;
    }
    public void Next() 
    {
        musIndex++;
    }
    public void Play()
    {

        audioContainer.Play();
        isStopped = false;
    }
    public void SetMusic(Music music) 
    {
        currentMusic = music;
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
        if (musIndex > MusicList.Count - 1)
        {
            musIndex = 0;
        }
        else if (musIndex < 0) 
        {
            musIndex = MusicList.Count - 1;
        }
        if (MusicList.Count != 0)
        {
            SetMusic(MusicList[musIndex]);
            SetMusicText();
            audioContainer.clip = currentMusic.clip;
            if (audioContainer.time >= audioContainer.clip.length - 0.1) 
            {
                musIndex++;

            }
        }
        if (!isStopped)
        {
        }
    }

}
