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

    //カメラ
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    CinemachineVirtualCamera vCam = null;
    Cinemachine3rdPersonFollow follow = null;
    Vector2 cameraRotationInput = Vector2.zero; // カメラ操作入力を記憶するプロパティ
    public Vector2 rotationSpeed = new Vector2(180, 180); // 1秒間180度

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

        //vCam = GetComponent<CinemachineVirtualCamera>();
        if (vCam != null)
        {
            follow = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
    }

    void FixedUpdate()
    {
        if (vCam != null)
        {
            Transform target = vCam.Follow; // バーチャルカメラの追跡ターゲットを取得
            if (target != null)
            {
                // ターゲットの回転をオイラー角度（x, y, z）で取得
                Vector3 targetEulerAngles = target.rotation.eulerAngles;

                // y軸の回転を変える
                targetEulerAngles.x += cameraRotationInput.y * rotationSpeed.y * Time.fixedDeltaTime;
                targetEulerAngles.y += cameraRotationInput.x * rotationSpeed.y * Time.fixedDeltaTime;

                // オイラー角度をクオータニオンに変換して追跡ターゲットの回転を変える
                //target.transform.rotation = Quaternion.Euler(targetEulerAngles);
            }
        }
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
        cameraRotationInput = context.ReadValue<Vector2>();
        //attackTrigger.GetComponent<AttackTrigger>().UpdateInputVec(inputVector);
        //もし移動ベクトルが0の場合戻す（入力していない）
        if (cameraRotationInput == Vector2.zero)
        {
            return;
        }
    }


    //入力を感知してからのAttack処理
    public void OnAttack(InputAction.CallbackContext context)
    {
        //attackTrigger.GetComponent<AttackTrigger>().ChangeForce(power);
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
        if (groundedPlayer && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
        }

        //入力方向を基にmoveベクトルを作る
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);
        //controller.Move(move * Time.deltaTime * playerSpeed);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        //デバッグのジャンプ処理
        /*
        if (attacked && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        */


        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVector.y + playerCamera.transform.right * inputVector.x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        //rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        controller.Move(moveForward * Time.deltaTime);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //移動する
        //moveDirection.y += gravityValue * Time.deltaTime;
        //controller.Move(moveDirection * Time.deltaTime);
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

    //プレイヤー自身の衝突判定
    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if(rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0f;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
    */

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
