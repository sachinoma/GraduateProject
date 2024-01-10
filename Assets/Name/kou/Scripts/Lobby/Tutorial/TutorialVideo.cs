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
    {   // �ŏ��͕\�����Ȃ�
        rawImage.enabled = false;
    }

    private void Start()
    {
        Play(clip);
    }


    /// <summary>
    /// �r�f�I�N���b�v���w�肵�čĐ�����
    /// </summary>
    public void Play(VideoClip videoClip)
    {
        // ����̐ݒ�
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;

        // �Đ������̊J�n
        videoPlayer.prepareCompleted += OnPrepareCompleted;
        videoPlayer.Prepare();
    }

    /// <summary>
    /// �Đ���������������
    /// </summary>
    void OnPrepareCompleted(VideoPlayer vp)
    {
        // �C���[�W�ɓ���e�N�X�`�����Z�b�g����
        rawImage.texture = videoPlayer.texture;

        // �C���[�W�T�C�Y�𓮉�Ɠ����傫���ɂ���
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(videoPlayer.texture.width, videoPlayer.texture.height);

        // �C�x���g�n���h�����Z�b�g���čĐ�����
        videoPlayer.started += OnMovieStarted;
        videoPlayer.loopPointReached += OnMovieStarted;
        videoPlayer.Play();
    }

    /// <summary>
    /// �Đ����J�n���ꂽ�Ƃ��ɌĂ΂��C�x���g
    /// </summary>
    void OnMovieStarted(VideoPlayer vp)
    {
        // �Đ����J�n���ꂽ��C���[�W��\������
        rawImage.enabled = true;
        TutorialStart();
    }

    public void TutorialStart()
    {
        animator.SetTrigger("Start");
    }
}
