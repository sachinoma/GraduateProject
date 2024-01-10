using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialVideo : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField] RawImage rawImage = null;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip clip;


    private void Awake()
    {   // 最初は表示しない
        rawImage.enabled = false;
    }

    private void Start()
    {
        Play(clip);
    }


    /// <summary>
    /// ビデオクリップを指定して再生する
    /// </summary>
    public void Play(VideoClip videoClip)
    {
        // 動画の設定
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;

        // 再生準備の開始
        videoPlayer.prepareCompleted += OnPrepareCompleted;
        videoPlayer.Prepare();
    }

    /// <summary>
    /// 再生準備が完了した
    /// </summary>
    void OnPrepareCompleted(VideoPlayer vp)
    {
        // イメージに動画テクスチャをセットする
        rawImage.texture = videoPlayer.texture;

        // イメージサイズを動画と同じ大きさにする
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(videoPlayer.texture.width, videoPlayer.texture.height);

        // イベントハンドラをセットして再生する
        videoPlayer.started += OnMovieStarted;
        videoPlayer.loopPointReached += OnMovieStarted;
        videoPlayer.Play();
    }

    /// <summary>
    /// 再生が開始されたときに呼ばれるイベント
    /// </summary>
    void OnMovieStarted(VideoPlayer vp)
    {
        // 再生が開始されたらイメージを表示する
        rawImage.enabled = true;
        TutorialStart();
    }

    public void TutorialStart()
    {
        animator.SetTrigger("Start");
    }
}
