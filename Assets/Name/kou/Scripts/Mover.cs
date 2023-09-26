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

    //�v���C���[
    Quaternion targetRotation;

    //�J����
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    CinemachineVirtualCamera vCam = null;
    Cinemachine3rdPersonFollow follow = null;
    Vector2 cameraRotationInput = Vector2.zero; // �J����������͂��L������v���p�e�B
    public Vector2 rotationSpeed = new Vector2(180, 180); // 1�b��180�x

    private bool groundedPlayer; //�ڒn�t���O
    [SerializeField]
    private float playerSpeed = 2.0f; //�ړ����x
    [SerializeField]
    private float jumpHeight = 1.0f; //�W�����v��
    [SerializeField]
    private float gravityValue = -9.81f; //�d��

    private Vector3 moveDirection = Vector2.zero; //�ړ�����
    private Vector2 inputVector = Vector2.zero; //���͕���
    private bool attacked = false; //�U���t���O

    [SerializeField]
    private float forceMagnitude = 10.0f; //�U���̗͉���

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
            Transform target = vCam.Follow; // �o�[�`�����J�����̒ǐՃ^�[�Q�b�g���擾
            if (target != null)
            {
                // �^�[�Q�b�g�̉�]���I�C���[�p�x�ix, y, z�j�Ŏ擾
                Vector3 targetEulerAngles = target.rotation.eulerAngles;

                // y���̉�]��ς���
                targetEulerAngles.x += cameraRotationInput.y * rotationSpeed.y * Time.fixedDeltaTime;
                targetEulerAngles.y += cameraRotationInput.x * rotationSpeed.y * Time.fixedDeltaTime;

                // �I�C���[�p�x���N�I�[�^�j�I���ɕϊ����ĒǐՃ^�[�Q�b�g�̉�]��ς���
                //target.transform.rotation = Quaternion.Euler(targetEulerAngles);
            }
        }
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    //���͂����m���Ă���̈ړ�����
    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        //attackTrigger.GetComponent<AttackTrigger>().UpdateInputVec(inputVector);
        //�����ړ��x�N�g����0�̏ꍇ�߂��i���͂��Ă��Ȃ��j
        if(inputVector == Vector2.zero) 
        {
            return;
        }
        //float angle = Vector2ToAngle(inputVector);
        //transform.rotation = Quaternion.Euler(0, -angle - 90, 0);
        animator.SetBool("isRun", true);
    }

    //���͂����m���Ă����Camera�ړ�����
    public void OnCameraMove(InputAction.CallbackContext context)
    {
        cameraRotationInput = context.ReadValue<Vector2>();
        //attackTrigger.GetComponent<AttackTrigger>().UpdateInputVec(inputVector);
        //�����ړ��x�N�g����0�̏ꍇ�߂��i���͂��Ă��Ȃ��j
        if (cameraRotationInput == Vector2.zero)
        {
            return;
        }
    }


    //���͂����m���Ă����Attack����
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
        //�v���C���[���n�ʂɂ��邩�ǂ���
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
        }

        //���͕��������move�x�N�g�������
        Vector3 move = new Vector3(-inputVector.x, 0, -inputVector.y);
        //controller.Move(move * Time.deltaTime * playerSpeed);

        if (move == Vector3.zero)
        {
            animator.SetBool("isRun", false);
        }

        //�f�o�b�O�̃W�����v����
        /*
        if (attacked && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        */


        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVector.y + playerCamera.transform.right * inputVector.x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        //rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        controller.Move(moveForward * Time.deltaTime);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        //�ړ�����
        //moveDirection.y += gravityValue * Time.deltaTime;
        //controller.Move(moveDirection * Time.deltaTime);
    }

    public static float Vector2ToAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    //AnimationEvent�ŌĂԁ@AttackTrigger��L���ɂ���
    public void AttackStart()
    {
        
    }
    //AnimationEvent�ŌĂԁ@AttackTrigger�𖳌��ɂ���
    public void AttackEnd()
    {
       
    }

    public void PlayKickSE()
    {
        
    }

    public void PlayCharaKickVoice()
    {
        
    }

    //�v���C���[���g�̏Փ˔���
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
