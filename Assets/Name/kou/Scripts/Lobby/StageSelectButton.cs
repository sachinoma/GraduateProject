using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isPressed;

    [SerializeField]
    private Image stageSelectUI;
    [SerializeField]
    private Sprite[] stageSelectSprites;

    [SerializeField]
    private int stageCount;
    public int StageCount { get { return stageCount; } }

    // Start is called before the first frame update
    void Start()
    {
        stageCount = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetIsPressed(true);
            animator.SetBool("IsPressed", isPressed);
        }
    }
    
    public void SetIsPressed(bool flag)
    {
        isPressed = flag;
    }

    public void ButtonOver()
    {
        SetIsPressed(false);
        animator.SetBool("IsPressed", isPressed);
    }
    public void ButtonEvent()
    {
        SetStageCount();
        stageSelectUI.sprite = stageSelectSprites[stageCount];
    }
    private void SetStageCount()
    {
        stageCount++;
        if (stageCount > 2)
        {
            stageCount = 0;
        }
    }

}
