using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Vector3[] movePosition = new Vector3[2];
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float startUpTime = 2.5f;
    [SerializeField] private float totalTime = 3.0f;
    [SerializeField] private float boundLength = 5.0f;
    private bool isUp;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        isUp = true;
        time = startUpTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time >= totalTime - 1f || time <= 0f + 1f)
        {
            isUp = !isUp;
        }

        if (isUp) { time += Time.deltaTime * speed; }
        else { time -= Time.deltaTime * speed; }

        float pos;

        pos = BackInOut(time, totalTime, movePosition[0].y, movePosition[1].y, boundLength);

        transform.localPosition = new Vector3(transform.localPosition.x, pos, transform.localPosition.z);

    }

    public static float BackInOut(float t, float totaltime, float min, float max, float s)
    {
        max -= min;
        s *= 1.525f;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * (t * t * ((s + 1) * t - s)) + min;

        t = t - 2;
        return max / 2 * (t * t * ((s + 1) * t + s) + 2) + min;
    }
}
