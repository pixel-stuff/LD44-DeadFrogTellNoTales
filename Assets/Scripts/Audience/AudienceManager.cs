using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public List<AudienceData> audienceList;

    public AudienceCharacter[] characArray;
    public int progression = 0;
    private System.Random random;

    public bool pathOver = false;
    public bool cardActivated = false;
    public int cardEpicness = 0;
    public int cardRomance = 0;

    public bool bossIsSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (AudienceCharacter charac in characArray)
        {
            charac.Fill(PickAudience());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pathOver)
        {
            ResolveAudience();
            pathOver = false;
        }
        if(cardActivated)
        {
            foreach (AudienceCharacter charac in characArray)
            {
                charac.ApplyModifier(cardEpicness, cardRomance);
            }
            cardActivated = false;
        }
    }

    public void CharacterOver(AudienceCharacter target)
    {
        progression ++;
        target.Fill(PickAudience());
    }

    public void CardActive(Card card)
    {
        foreach(AudienceCharacter charac in characArray)
        {
            charac.ApplyModifier(card.epicness, card.romance);
        }
    }

    public void ResolveAudience()
    {
        foreach (AudienceCharacter charac in characArray)
        {
            charac.Resolve();
        }
    }

    AudienceData PickAudience()
    {
        int weightSum = 0;
        foreach (AudienceData data in audienceList)
        {
            if(!data.isBoss || (data.isBoss && !bossIsSpawned))
            {
                weightSum += (int)data.weightCurve.Evaluate(progression);
            }
        }
        //random selected a Data
        int randomValue = (int)Random.Range(0, weightSum);
        for (int i = 0; i < audienceList.Count; i++)
        {
            if (!audienceList[i].isBoss || (audienceList[i].isBoss && !bossIsSpawned))
            {
                randomValue -= (int)audienceList[i].weightCurve.Evaluate(progression);
            }

            if (randomValue < 0)
            {
                if(audienceList[i].isBoss)
                {
                    bossIsSpawned = true;
                }

                return audienceList[i];
            }
        }
        return new AudienceData(); // should not happend
    }
}
