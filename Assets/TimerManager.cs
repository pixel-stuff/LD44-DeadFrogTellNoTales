using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public MyStringEvent timerEvent;
    public float maxTimeSecond = 30.0f;
    public float timeSecond = 30.0f;
    public int pixelToMove = 100;
    public GameObject Candle;

    void Start()
    {
        timeSecond = maxTimeSecond;
    }

    void Update()
    {
        if(timeSecond < 0)
        {
            Debug.Log("GameOver");
        }
        timeSecond -= Time.deltaTime;
        float timePercent = timeSecond / maxTimeSecond;
        if (timePercent < 0)
            timePercent = 0;
        Candle.transform.localPosition = new Vector3(Candle.transform.localPosition.x, pixelToMove * timePercent, Candle.transform.localPosition.z);
        timerEvent.Invoke(((int)(timeSecond)).ToString());
    }
}
