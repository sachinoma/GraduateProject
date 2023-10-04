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

    //�v���C���[
    Quaternion targetRotation;
    //�d��
    const float gravityScaleNum = -9.8f;

    float gravityScale = 0.0f;

    private bool isJump = false; //�W�����v�t���O
    [SerializeField]
    float jumpScale = 3.0f;     //�W�����v��

    private bool isMove = false; //�ړ����̓t���O

    //�J����
    [SerializeField]
    Camera playerCamera;

    private bool isGround; //�ڒn�t���O
    [SerializeField]
    private float playerSpeed = 2.0f; //�ړ����x


    private Vector3 moveDirection = Vector2.zero; //�ړ�����
    private Vector2 inputVector = Vector2.zero; //���͕���

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

    //���͂����m���Ă���̈ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        isMove = true;
        inputVector = context.ReadValue<Vector2>();
        //�����ړ��x�N�g����0�̏ꍇ�߂��i���͂��Ă��Ȃ��j
        if(inputVector == Vector2.zero) 
        {
            isMove = false;
            return;
        }
    }

    //���͂����m���Ă����Camera�ړ�����
    public void OnCameraMove(InputAction.CallbackContext context)
    {

    }


    //���͂����m���Ă����Jump����
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
        //���͕��������move�x�N�g�������
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(playerCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 gravity = (new Vector3(0, 1, 0) * gravityScale);

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 lookForward = cameraForward * inputVector.y + cameraRight * inputVector.x;
        lookForward *= playerSpeed;

        // �L�����N�^�[�̌�����Look������
        if (lookForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookForward);
        }
        //�d�͂�t����
        Vector3 moveForward = lookForward + gravity;
        //�ړ�����
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
