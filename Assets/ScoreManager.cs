using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public MyStringEvent scoreEvent;
    public int score = 0;
    public int romanceMultiplicator = 5;
    public int epicnessMultiplicator = 5;
    public float maxSize = 2f;
    public float growSpeed = 0.5f;
    public float reductionSpeed = 0.2f;

    public void Update()
    {
        this.transform.localScale = new Vector3((this.transform.localScale.x <= 1f) ? 1f : this.transform.localScale.x - reductionSpeed, (this.transform.localScale.x <= 1f) ? 1f : this.transform.localScale.x - reductionSpeed, 1f);
    }

    public void AddScore(AudienceCharacter charac)
    {
        charac.pointCount.Invoke(charac.neededEpicness * romanceMultiplicator + charac.neededRomance * epicnessMultiplicator); 
    }

    void OnParticleCollision(GameObject other)
    {
        score++;
        this.transform.localScale = new Vector3((this.transform.localScale.x >= maxSize) ? maxSize : this.transform.localScale.x + growSpeed, (this.transform.localScale.x >= maxSize) ? maxSize : this.transform.localScale.x + growSpeed, 1f);
        scoreEvent.Invoke(score.ToString());
    }
}
