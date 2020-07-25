using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GlobExpressions;
using UnityEngine;
using UnityEngine.Events;

public class MusicLoader : MonoBehaviour
{
    public static List<Music> musicList { get; private set; } = new List<Music>();

    public string musicFolderPath;
    private string[] matchingFiles;

    public static UnityEvent songLoaded = new UnityEvent();
    private IEnumerator LoadAudio(string path)
    {
        path = "file://" + path;
        WWW URL = new WWW(path);
        yield return URL;

        musicList.Add(new Music { clip = URL.GetAudioClip(false, true), name = Path.GetFileName(path) });
        songLoaded.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        musicFolderPath = musicFolderPath.Replace('\\', '/');
        matchingFiles = Glob.Files($"{musicFolderPath}", "*.wav").ToArray();

        foreach (string path in matchingFiles)
        {
            StartCoroutine(LoadAudio(musicFolderPath + "/" + path));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
