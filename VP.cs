// Examples of VideoPlayer function

using UnityEngine;

public class VP : MonoBehaviour
{
    public GameObject cam;

    void Start()
    {

    }

    public void PlayVideo(string video,bool looping,float volume)
    {
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().url = Application.dataPath + "/Video/" + video;
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().isLooping = looping;
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().SetDirectAudioVolume(0,volume);
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
    }
    
    public void StopVideo()
    {
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
    }
}