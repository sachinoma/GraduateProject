using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    [SerializeField]
    private Animator animator;  
    private CharacterController controller;

    //プレイヤー
    Quaternion targetRotation;
    [SerializeField]
    const float gravityScaleNum = 9.8f;
    float gravityScale = 0.0f;

    //カメラ
    [SerializeField]
    Camera playerCamera;


    private bool groundedPlayer; //接地フラグ
    [SerializeField]
    private float playerSpeed = 2.0f; //移動速度
    [SerializeField]
    private float jumpHeight = 1.0f; //ジャンプ力
    [SerializeField]
    private float gravityValue = -9.81f; //重力

    private Vector3 moveDirection = Vector2.zero; //移動方向
    private Vector2 inputVector = Vector2.zero; //入力方向
    private bool attacked = false; //攻撃フラグ

    [SerializeField]
    private float forceMagnitude = 10.0f; //攻撃の力加減

    AudioSource audioSource;


    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        targetRotation = transform.rotation;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        inputVector = context.ReadValue<Vector2>();
        //attackTrigger.GetComponent<AttackTrigger>().UpdateInputVec(inputVector);
        //もし移動ベクトルが0の場合戻す（入力していない）
        if(inputVector == Vector2.zero) 
        {
            return;
        }
        //float angle = Vector2ToAngle(inputVector);
        //transform.rotation = Quaternion.Euler(0, -angle - 90, 0);
        animator.SetBool("isRun", true);
    }

    //入力を感知してからのCamera移動処理
    public void OnCameraMove(InputAction.CallbackContext context)
    {

    }


    //入力を感知してからのAttack処理
    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
        //animator.SetBool("isAttack", attacked);
    }

    public void OnSkill(InputAction.CallbackContext context, int num)
    {
        
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
        //プレイヤーが地面にいるかどうか
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            gravityScale = 0.0f;
        }
        else
        {
            gravityScale = gravityScaleNum;
        }

        //入力方向を基にmoveベクトルを作る
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);
        //controller.Move(move * Time.deltaTime * playerSpeed);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        //デバッグのジャンプ処理
        if (attacked && groundedPlayer)
        {
            gravityScale = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(playerCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 gravity = (new Vector3(0, -1, 0) * gravityScale).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 lookForward = cameraForward * inputVector.y + cameraRight * inputVector.x;

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

    public static float Vector2ToAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    //AnimationEventで呼ぶ　AttackTriggerを有効にする
    public void AttackStart()
    {
        
    }
    //AnimationEventで呼ぶ　AttackTriggerを無効にする
    public void AttackEnd()
    {
       
    }

    public void PlayKickSE()
    {
        
    }

    public void PlayCharaKickVoice()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = collision.collider.attachedRigidbody;
        if (rigidbody != null)
        {
            Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }


}
