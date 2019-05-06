using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    public StringEvent timerEvent;
    public UnityEvent timeOver;
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
        if(timeSecond <= 0)
        {
            timeOver.Invoke();
        }
        timeSecond -= Time.deltaTime;
        if (timeSecond < 0)
            timeSecond = 0;
        float timePercent = timeSecond / maxTimeSecond;

        Candle.transform.localPosition = new Vector3(Candle.transform.localPosition.x, pixelToMove * timePercent, Candle.transform.localPosition.z);
        int Ssecond = ((int)(timeSecond % 60));
        int SMinute = ((int)(timeSecond / 60));
        //Debug.Log("S:" + Ssecond + " M:" + SMinute + "  String:" + string.Format("{0:00}", SMinute) + ":" + string.Format("{0:00}", Ssecond));
        timerEvent.Invoke(string.Format("{0:00}",SMinute) + ":" + string.Format("{0:00}", Ssecond));
    }
}
