using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string videoFileName;

    [SerializeField] string videoPath;

    private void Start()
    {
        videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        Debug.Log($"Video path: {videoPath}");

        if (videoPlayer != null && !string.IsNullOrEmpty(videoPath))
        {
            videoPlayer.url = videoPath;
        }
        else
        {
            Debug.LogWarning("Видеоплеер не назначен или указан неверный путь к видео.");
        }
    }

    public void PlayVideo()
    {
        if (videoPlayer != null && !string.IsNullOrEmpty(videoPath))
        {
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Не удается воспроизвести видео: отсутствует видеоплеер или путь к нему.");
        }
    }

    public void StopVideo()
    {
        if (videoPlayer != null && !string.IsNullOrEmpty(videoPath))
        {
            videoPlayer.Stop();
        }
        else
        {
            Debug.LogWarning("Не удается воспроизвести видео: отсутствует видеоплеер или путь к нему.");
        }
    }
}