using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraLayerUpdater : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _cinemachineCamera;

    // �v���C���[�C���f�b�N�X�ƃ��C���[�̑Ή�
    [Serializable]
    public struct PlayerLayer
    {
        public int index;
        public int layer;
    }

    [SerializeField] private PlayerLayer[] _playerLayers;

    private int _currentIndex = -1;

    private void Start()
    {
        // ���C���[�X�V
        OnIndexUpdated(playerNum);
    }

    // �L����
    private void OnEnable()
    {
        if (PlayerInputManager.instance == null) return;

        // �v���C���[���ގ��������̃C�x���g���Ď�����
        PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
    }

    // ������
    private void OnDisable()
    {
        if (PlayerInputManager.instance == null) return;

        PlayerInputManager.instance.onPlayerLeft -= OnPlayerLeft;
    }

    // �v���C���[���ގ��������ɌĂ΂��
    private void OnPlayerLeft(PlayerInput playerInput)
    {
        // ���v���C���[���ގ��������̓C���f�b�N�X�������\��������̂�
        // ���C���[���X�V����
        if (playerInput.user.index >= playerNum)
            return;

        // ���̎��A�܂��C���f�b�N�X�͑O�̂܂܂Ȃ̂�-1����K�v������
        OnIndexUpdated(playerNum - 1);
    }

    // �v���C���[�C���f�b�N�X���X�V���ꂽ���ɌĂ΂��
    private void OnIndexUpdated(int index)
    {
        if (_currentIndex == index) return;

        // �C���f�b�N�X�ɉ��������C���[���擾
        var layerIndex = Array.FindIndex(_playerLayers, x => x.index == index);
        if (layerIndex < 0) return;

        // �v���C���[�p�̃J�����擾
        var playerCamera = _camera;
        if (playerCamera == null) return;

        // �J������CullingMask��ύX
        // ���g�̃��C���[�͕\���A���v���C���[�̃��C���[�͔�\���ɂ���
        for (var i = 0; i < _playerLayers.Length; i++)
        {
            var layer = 1 << _playerLayers[i].layer;

            if (i == index)
                playerCamera.cullingMask |= layer;
            else
                playerCamera.cullingMask &= ~layer;
        }

        // Cinemachine�J�����̃��C���[�ύX
        _cinemachineCamera.gameObject.layer = _playerLayers[layerIndex].layer;

        _currentIndex = index;
    }

    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }
}