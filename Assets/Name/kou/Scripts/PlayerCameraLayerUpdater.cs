using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraLayerUpdater : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _cinemachineCamera;

    // プレイヤーインデックスとレイヤーの対応
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
        // レイヤー更新
        OnIndexUpdated(playerNum);
    }

    // 有効化
    private void OnEnable()
    {
        if (PlayerInputManager.instance == null) return;

        // プレイヤーが退室した時のイベントを監視する
        PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
    }

    // 無効化
    private void OnDisable()
    {
        if (PlayerInputManager.instance == null) return;

        PlayerInputManager.instance.onPlayerLeft -= OnPlayerLeft;
    }

    // プレイヤーが退室した時に呼ばれる
    private void OnPlayerLeft(PlayerInput playerInput)
    {
        // 他プレイヤーが退室した時はインデックスがずれる可能性があるので
        // レイヤーを更新する
        if (playerInput.user.index >= playerNum)
            return;

        // この時、まだインデックスは前のままなので-1する必要がある
        OnIndexUpdated(playerNum - 1);
    }

    // プレイヤーインデックスが更新された時に呼ばれる
    private void OnIndexUpdated(int index)
    {
        if (_currentIndex == index) return;

        // インデックスに応じたレイヤー情報取得
        var layerIndex = Array.FindIndex(_playerLayers, x => x.index == index);
        if (layerIndex < 0) return;

        // プレイヤー用のカメラ取得
        var playerCamera = _camera;
        if (playerCamera == null) return;

        // カメラのCullingMaskを変更
        // 自身のレイヤーは表示、他プレイヤーのレイヤーは非表示にする
        for (var i = 0; i < _playerLayers.Length; i++)
        {
            var layer = 1 << _playerLayers[i].layer;

            if (i == index)
                playerCamera.cullingMask |= layer;
            else
                playerCamera.cullingMask &= ~layer;
        }

        // Cinemachineカメラのレイヤー変更
        _cinemachineCamera.gameObject.layer = _playerLayers[layerIndex].layer;

        _currentIndex = index;
    }

    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }
}