using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Windows;
using Unity.VisualScripting;

public class Mover : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Drop,
    }
    public State state;

    //�J����
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    GameObject cameraMain;

    [SerializeField]
    private bool isLobby;

    [SerializeField]
    private Animator animator;

    //�v���C���[
    [SerializeField]
    PlayerStatus playerStatus;

    Quaternion targetRotation;
    Rigidbody rb;
    [SerializeField]
    Foot foot;

    [SerializeField]
    private bool isGround; //�ڒn�t���O

    [SerializeField] 
    private Vector3 localGravity; //�J�X�^���d��

    private bool isJump = false; //�W�����v�t���O
    [SerializeField]
    float jumpScale = 3.0f;     //�W�����v��

    private bool isMove = false; //�ړ����̓t���O

    
    [SerializeField]
    private float playerSpeed = 2.0f; //�����x
    private float playerSpeedSaved;

    private Vector3 moveDirection = Vector2.zero; //�ړ�����
    private Vector2 inputVector = Vector2.zero; //���͕���

    AudioSource audioSource;

    [SerializeField]
    private GameObject[] ItemEffects;

    [SerializeField]
    private GameObject blowingCol;

    [SerializeField]
    private float upSpeed = 30.0f;
    [SerializeField]
    private float slowSpeed = 11.0f;
    [SerializeField]
    private float stunSpeed = 7.0f;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();  // rigidbody���擾
        targetRotation = transform.rotation;
    }

    private void Start()
    {
        rb.useGravity = false; //�ŏ���rigidBody�̏d�͂��g��Ȃ�����
        audioSource = GetComponent<AudioSource>();
        SetBlowingColFalse();
        state = State.Idle;
        playerSpeedSaved = playerSpeed;
        if (isLobby)
        {
            cameraMain.SetActive(false);
            playerCamera = Camera.main;
        }
    }
       

    public void SetIsLobby(bool flag)
    {
        isLobby = flag;
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
        if (context.action == null)
            return;

        if(state == State.Idle || state == State.Run)
        {
            if (!context.action.triggered)
                return;
            if(!isJump)
            {
                isJump = true;
                Vector3 force = new Vector3(0.0f, jumpScale, 0.0f);  // �͂�ݒ�
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }

    private void SetIsAttackFalse()
    {
        animator.SetBool("isAttack", false);
    }

    void Update()
    {
        DecideState();        
    }

    void FixedUpdate()
    {
        SetLocalGravity(); //�d�͂�AddForce�ł����郁�\�b�h���Ă�
        ChangeIsJump();
        isGround = CheckIsGround();
        PlayState();
    }

    public void ChangeOutfit()
    {
        int newOutfitNum = playerStatus.GetOutfitNum() + 1;
        if (newOutfitNum >= playerStatus.GetOutfitMax())
        {
            newOutfitNum = 0;
        }
        PlayerConfigurationManager.Instance.SetPlayerPrefab(playerStatus.GetPlayerNum(), newOutfitNum);
        playerStatus.ChangeOutfit(newOutfitNum);
    }



    #region Item
    public void SpeedUp()
    {
        CancelInvoke(nameof(RecoverSpeed));
        GameObject effect = (GameObject)Instantiate(ItemEffects[0], this.transform.position, Quaternion.identity);
        effect.transform.parent = this.transform;
        playerSpeed = upSpeed;
        Invoke(nameof(RecoverSpeed), 6.0f);
    }
    public void Slow()
    {
        CancelInvoke(nameof(RecoverSpeed));
        GameObject effect = (GameObject)Instantiate(ItemEffects[1], this.transform.position, Quaternion.identity);
        effect.transform.parent = this.transform;
        playerSpeed = slowSpeed;
        Invoke(nameof(RecoverSpeed), 6.0f);
    }
    public void Stun()
    {
        CancelInvoke(nameof(RecoverSpeed));
        GameObject effect = (GameObject)Instantiate(ItemEffects[2], this.transform.position, Quaternion.identity);
        effect.transform.parent = this.transform;
        playerSpeed = stunSpeed;
        Invoke(nameof(RecoverSpeed), 3.5f);
    }
    public void Blowing()
    {
        CancelInvoke(nameof(RecoverSpeed));
        GameObject effect = (GameObject)Instantiate(ItemEffects[3], this.transform.position + new Vector3(0,1,0), Quaternion.identity);
        effect.transform.parent = this.transform;
        playerSpeed = 20;
        blowingCol.SetActive(true);
        Invoke(nameof(RecoverSpeed), 6.0f);
        Invoke(nameof(SetBlowingColFalse), 6.0f);
    }

    private void SetBlowingColFalse()
    {
        blowingCol.SetActive(false);
    }

    private void RecoverSpeed()
    {
        playerSpeed = playerSpeedSaved;
    }
    #endregion

    #region Move
    private void SetLocalGravity()
    {
        rb.AddForce(localGravity, ForceMode.Acceleration);
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
        return foot.GetIsGround();
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

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 lookForward = cameraForward * inputVector.y + cameraRight * inputVector.x;

        // �L�����N�^�[�̌�����Look������
        if (lookForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookForward);
        }

        Vector3 moveForward = lookForward;

        //�ړ�����
        rb.AddForce(moveForward.normalized * playerSpeed , ForceMode.Acceleration);            // �͂�������(RigidBody��Drag���ݒ肵������A�����Ɖ����̏�Ԃɂ͂Ȃ�Ȃ�)        
    }

    public void BounceAction(Vector3 forceVec, float force)
    {
        rb.AddForce(forceVec * force, ForceMode.Impulse);
    }

    public void MoveClear()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }


    private void ChangeIsJump()
    {
        if(isJump)
        {
            if(rb.velocity.y <= 0)
            {
                isJump = false;
            }
        }
    }
    #endregion



    private void DecideState()
    {
        if (state == State.Idle)
        {
            if (isMove)
                state = State.Run;
            if (isJump)
                state = State.Drop;
            if (!isGround)
                state = State.Drop;

        }
        else if (state == State.Run)
        {
            if (!isMove)
                state = State.Idle;
            if (isJump)
                state = State.Drop;
            if (!isGround)
                state = State.Drop;
        }
        else if (state == State.Drop)
        {
            if (isGround && !isMove)
                state = State.Idle;
            if (isGround && isMove)
                state = State.Run;
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
        else if (state == State.Drop)
        {
            MovePlayer();
            animator.SetBool("isRun", false);
        }
    }
}
