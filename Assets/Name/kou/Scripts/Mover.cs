using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Drop,
        Jump
    }
    public State state;

    [SerializeField]
    private bool isLobby;

    [SerializeField]
    private Animator animator;  
    private CharacterController controller;

    //プレイヤー
    Quaternion targetRotation;
    //重力
    const float gravityScaleNum = -9.8f;

    float gravityScale = 0.0f;

    private bool isJump = false; //ジャンプフラグ
    [SerializeField]
    float jumpScale = 3.0f;     //ジャンプ力

    private bool isMove = false; //移動入力フラグ

    //カメラ
    [SerializeField]
    Camera playerCamera;

    private bool isGround; //接地フラグ
    [SerializeField]
    private float playerSpeed = 2.0f; //移動速度


    private Vector3 moveDirection = Vector2.zero; //移動方向
    private Vector2 inputVector = Vector2.zero; //入力方向

    AudioSource audioSource;


    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        targetRotation = transform.rotation;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        state = State.Idle;
        if(isLobby)
        {
            playerCamera = Camera.main;
        }
    }

    void FixedUpdate()
    {

    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    //入力を感知してからの移動処理
    public void OnMove(InputAction.CallbackContext context)
    {
        isMove = true;
        inputVector = context.ReadValue<Vector2>();
        //もし移動ベクトルが0の場合戻す（入力していない）
        if(inputVector == Vector2.zero) 
        {
            isMove = false;
            return;
        }
    }

    //入力を感知してからのCamera移動処理
    public void OnCameraMove(InputAction.CallbackContext context)
    {

    }


    //入力を感知してからのJump処理
    public void OnJump(InputAction.CallbackContext context)
    {
        if(state == State.Idle || state == State.Run)
        {
            if (!context.action.triggered)
                return;
            if(!isJump)
            {
                isJump = true;
                gravityScale = jumpScale;
                //jumped = context.action.triggered;
            }
        }
    }

    private void SetIsAttackFalse()
    {
        animator.SetBool("isAttack", false);
    }

    private void ResetTime(int num)
    {
       
    }

    void Update()
    {
        GravityControl();
        ChangeIsJump();
        isGround = CheckIsGround();
        DecideState();
        PlayState();
    }

    public static float Vector2ToAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.collider.attachedRigidbody;
        if (rigidbody != null)
        {
           
        }
    }

    private bool CheckIsGround()
    {
        return controller.isGrounded;
    }

    private void MovePlayer()
    {
        //入力方向を基にmoveベクトルを作る
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(playerCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 gravity = (new Vector3(0, 1, 0) * gravityScale);

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 lookForward = cameraForward * inputVector.y + cameraRight * inputVector.x;
        lookForward *= playerSpeed;

        // キャラクターの向きをLook方向に
        if (lookForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookForward);
        }
        //重力を付けて
        Vector3 moveForward = lookForward + gravity;
        //移動する
        controller.Move(moveForward * Time.deltaTime);
    }

    private void ChangeIsJump()
    {
        if(isJump)
        {
            if (gravityScale <= 0)
            {
                isJump = false;
            }
        }
    }

    private void GravityControl()
    {
        if (gravityScale > gravityScaleNum)
        {
            gravityScale = gravityScale - 0.2f;
        } 

        if(gravityScale < gravityScaleNum)
        {
            gravityScale = gravityScaleNum;
        }
    }

    private void DecideState()
    {
        if (state == State.Idle)
        {
            if (isMove)
                state = State.Run;
            if (isJump)
                state = State.Jump;
        }
        else if (state == State.Run)
        {
            if (!isMove)
                state = State.Idle;
            if (isJump)
                state = State.Jump;
        }
        else if (state == State.Jump)
        {
            if (!isJump)
                state = State.Drop;
            if (isGround)
                state = State.Idle;
        }
        else if (state == State.Drop)
        {
            if (isGround)
                state = State.Idle;
        }
    }

    private void PlayState()
    {
        if (state == State.Idle)
        {
            animator.SetBool("isRun", false);
        }
        else if(state == State.Run)
        {
            MovePlayer();
            animator.SetBool("isRun", true);
        }
        else if (state == State.Jump)
        {
            MovePlayer();
            animator.SetBool("isRun", false);
        }
        else if (state == State.Drop)
        {
            MovePlayer();
            animator.SetBool("isRun", false);
        }
    }
}
