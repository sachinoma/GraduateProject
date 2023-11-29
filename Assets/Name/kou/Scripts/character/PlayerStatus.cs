using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerStatus : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]    InputReceiver inputReceiver;

    [SerializeField]    private int playerNum;

    [SerializeField]    private int outfitNum;

    [SerializeField]    private GameObject outfitNow;
    [SerializeField]    private GameObject[] outfit;

    [SerializeField]    private Vector3 savePoint;

    [SerializeField]    private Mover mover;

    [SerializeField]    private PlayerConfiguration[] playerConfigs;

    [SerializeField]    private ResultData[] resultData;
    [SerializeField]    private int nowRank;
    [SerializeField]    private bool isGoal = false;
    [SerializeField]    private bool isSolo = false;
    [SerializeField]    private Animator uiAnimator;

    [SerializeField]    private Sprite[] rankSprite;
    [SerializeField]    private Image rankImage;

    private void Start()
    {
        GameObject playerInputManager = GameObject.Find("PlayerInputManager");
        gameManager = playerInputManager.GetComponent<GameManager>();

        Invoke(nameof(StartMethod), 0.05f);
    }

    private void StartMethod()
    {
        if (inputReceiver.GetInput() != null)
            playerNum = inputReceiver.GetInput().GetPlayerNo();
        DiableAllOutfit();
        ChangeOutfit(GetOutfitNum());
        SetSavePoint(this.transform);
        resultData = GameManager.Instance.GetResultData().ToArray();
    }

    private void Update()
    {
        nowRank = gameManager.GetRank(playerNum);
        if(!isGoal)
        {
            if (!isSolo)
            {
                isGoal = gameManager.GetRankLock(playerNum);
                if (isGoal)
                {
                    uiAnimator.SetBool("isGoal", true);
                }
            }
        }
        if (nowRank >= 0)
        {
            rankImage.sprite = rankSprite[nowRank];
        }
        else
        {
            Debug.Log("rankはマイナスです！");
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10.0f)
        {
            resultData[playerNum].PlusFallNum();
            ReSpawn();
        }
    }

    public int GetPlayerNum()
    {
        return playerNum;
    }

    public int GetOutfitNum()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        outfitNum = playerConfigs[GetPlayerNum()].PlayerPrefabNum;
        return outfitNum;
    }

    public int GetOutfitMax()
    {
        return outfit.Length;
    }

    public void DiableAllOutfit()
    {
        for(int i = 0; i < outfit.Length; i++)
        {
            outfit[i].SetActive(false);
        }
    }

    public void ChangeOutfit(int num)
    {
        if(outfitNow != null)
            outfitNow.SetActive(false);
        outfit[num].SetActive(true);
        outfitNow = outfit[num].gameObject;
    }

    public void ReSpawn()
    {
        transform.position = savePoint;
        mover.MoveClear();
    }

    public void SetSavePoint(Transform gameObject)
    {
        savePoint = gameObject.position;
    }

    public void SetIsSolo(bool flag)
    {
        isSolo = flag;
    }
}
