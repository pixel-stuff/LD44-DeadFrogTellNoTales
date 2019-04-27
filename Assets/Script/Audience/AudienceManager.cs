﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public List<AudienceData> audienceList;

    public AudienceCharacter character1;
    public AudienceCharacter character2;
    public AudienceCharacter character3;

    public int progression = 0;
    private System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        character1.isOver.AddListener(character1Over);
        character2.isOver.AddListener(character2Over);
        character3.isOver.AddListener(character3Over);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void character1Over()
    {
        progression ++;
        character1.Fill(PickAudience());
    }

    void character2Over()
    {
        progression++;
        character2.Fill(PickAudience());
    }

    void character3Over()
    {
        progression++;
        character3.Fill(PickAudience());
    }

    AudienceData PickAudience()
    {
        int weightSum = 0;
        foreach (AudienceData data in audienceList)
        {
            weightSum += (int)data.weightCurve.Evaluate(progression);
        }
        //random selected a Data
        int randomValue = (int)Random.Range(0, weightSum);
        for (int i = 0; i < audienceList.Count; i++)
        {
            randomValue -= (int)audienceList[i].weightCurve.Evaluate(progression);
            if (randomValue < 0)
            {
                return audienceList[i];
            }
        }
        return new AudienceData(); // should not happend
    }
}
