using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public MyStringEvent scoreEvent;
    public int score = 0;
    public int romanceMultiplicator = 10;
    public int epicnessMultiplicator = 10;

    public void AddScore(AudienceCharacter charac)
    {
        score += charac.neededEpicness * romanceMultiplicator + charac.neededRomance * epicnessMultiplicator;
        scoreEvent.Invoke(score.ToString());
    }
}
