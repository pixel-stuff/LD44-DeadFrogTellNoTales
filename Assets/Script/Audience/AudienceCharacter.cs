using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudienceCharacter : MonoBehaviour
{
    public UnityEvent isOver; 
    public AudienceData refData;
    public int currentEpicness;
    public int currentRomance;

    public bool DebugOver = true;

    public void Fill(AudienceData data)
    {
        refData = data;
        currentEpicness = (int)Random.Range(data.epicnessNeeds.x, data.epicnessNeeds.y);
        currentRomance = (int)Random.Range(data.romanceNeeds.x, data.romanceNeeds.y);
        this.GetComponent<Image>().sprite = data.calmSprite;
        DebugOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DebugOver)
        {
            isOver.Invoke();
        }
    }
}
