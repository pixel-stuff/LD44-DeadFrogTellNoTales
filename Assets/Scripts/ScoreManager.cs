using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour {
  public MyStringEvent scoreEvent;
  public int score = 0;
  public int romanceMultiplicator = 5;
  public int epicnessMultiplicator = 5;
  public float maxSize = 2f;
  public float growSpeed = 0.5f;
  public float reductionSpeed = 0.2f;

  public int firstStartScore = 100;
  public int secondStartScore = 200;
  public int thirdStartScore = 300;
  public string menuSceneString = "MenuScene";
  public string nextSceneString = "TutoScene";
  public UnityEvent noScoreReach;
  public StringEvent firstStartScoreEvent;
  public StringEvent secondStartScoreEvent;
  public StringEvent thirdStartScoreEvent;
  public UnityEvent firstStartScoreReached;
  public UnityEvent secondStartScoreReached;
  public UnityEvent thirdStartScoreReached;

  public void Update() {
    this.transform.localScale = new Vector3((this.transform.localScale.x <= 1f) ? 1f : this.transform.localScale.x - reductionSpeed, (this.transform.localScale.x <= 1f) ? 1f : this.transform.localScale.x - reductionSpeed, 1f);
  }

  public void AddScore(AudienceCharacter charac) {
    charac.pointCount.Invoke(charac.neededEpicness * romanceMultiplicator + charac.neededRomance * epicnessMultiplicator);
  }

  void OnParticleCollision(GameObject other) {
    score++;
    this.transform.localScale = new Vector3((this.transform.localScale.x >= maxSize) ? maxSize : this.transform.localScale.x + growSpeed, (this.transform.localScale.x >= maxSize) ? maxSize : this.transform.localScale.x + growSpeed, 1f);
    scoreEvent.Invoke(score.ToString());
  }

  public void ShowScore() {
    firstStartScoreEvent.Invoke(score.ToString() + "/" + firstStartScore.ToString());
    secondStartScoreEvent.Invoke(score.ToString() + "/" + secondStartScore.ToString());
    thirdStartScoreEvent.Invoke(score.ToString() + "/" + thirdStartScore.ToString());
    if(score < firstStartScore) {
      noScoreReach.Invoke();
    } else {
      firstStartScoreReached.Invoke();
      if(score > secondStartScore) {
        secondStartScoreReached.Invoke();
      }
      if(score > thirdStartScore) {
        thirdStartScoreReached.Invoke();
      }
    }
  }

  public void Retry() => LoadScene(SceneManager.GetActiveScene().name);
  public void NextLevel() => LoadScene(nextSceneString);
  public void Menu() => LoadScene(menuSceneString);
  void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
