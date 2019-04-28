using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public MyStringEvent timerEvent;
    public float timeSecond = 30.0f;

    void Update()
    {
        if(timeSecond < 0)
        {
            Debug.Log("GameOver");
        }
        timeSecond -= Time.deltaTime;
        timerEvent.Invoke(((int)(timeSecond)).ToString());
    }
}
