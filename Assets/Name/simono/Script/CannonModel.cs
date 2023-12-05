using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonModel : MonoBehaviour
{
    [SerializeField] float interval = 1.0f;
    [SerializeField] GameObject Ammos;
    [SerializeField] Transform ShotSocket;
    [SerializeField] float ShotForce;

    [SerializeField] GameObject shootParticle;

    [SerializeField]
    SoundEffect soundEffect;
    [SerializeField]
    AudioClip clip;

    [SerializeField]
    float soundDis = 50.0f;

    [SerializeField]
    bool isCanPlaySound = false;
    [SerializeField]
    bool isReady = false;

    GameObject[] allPlayer;
    GameObject[] allAvatar;
    private int playerNum;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    float timer = 0.0f;

    private void Start()
    {
        //playerConfigs
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        playerNum = playerConfigs.Length;
        allAvatar = new GameObject[playerNum];

        timer = Time.time;

        Invoke(nameof(LateStart), 0.2f);
    }

    private void LateStart()
    {
        int allAvatarNum = 0;
        allPlayer = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < allPlayer.Length; i++)
        {
            if (allPlayer[i].name == "Avatar")
            {
                allAvatar[allAvatarNum] = allPlayer[i];
                allAvatarNum++;
            }
        }
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanShot())
        {
            if (isReady)
            {
                for(int i = 0;i < allAvatar.Length;i++)
                {
                    if ((transform.position - allAvatar[i].transform.position).magnitude <= soundDis)
                    {
                        ChangeIsCanPlaySound(true);
                        break;
                    }
                    else
                    {
                        ChangeIsCanPlaySound(false);
                    }
                }
                if (isCanPlaySound) 
                { 
                    soundEffect.PlaySoundEffectClip(clip); 
                }
            }
            
            var ammo = Instantiate(Ammos, ShotSocket.position, ShotSocket.rotation);           
            var model = ammo.GetComponent<AmmoModel>();
            Instantiate(shootParticle, ShotSocket.position, ShotSocket.rotation);

            model.Force = ShotForce;

            //タイマーの更新
            timer = Time.time;
        }
    }

    public void ChangeIsCanPlaySound(bool flag)
    {
        isCanPlaySound = flag;
    }

    bool CanShot()
    {
        return Time.time - timer >= interval;
    }
}
