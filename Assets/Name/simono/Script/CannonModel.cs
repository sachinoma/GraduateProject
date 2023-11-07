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

    float timer = 0.0f;

    private void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanShot())
        {

            var ammo = Instantiate(Ammos, ShotSocket.position, ShotSocket.rotation);
            var model = ammo.GetComponent<AmmoModel>();

            model.Force = ShotForce;

            //タイマーの更新
            timer = Time.time;
        }
    }

    bool CanShot()
    {
        return Time.time - timer >= interval;
    }
}
