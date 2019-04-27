using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public UnityEvent isDisappeared;
    public UnityEvent isSelected;
    public CardData refData;
    public int epicness;
    public int romance;

    public bool DebugDisappear = true;
    // Start is called before the first frame update
    public void Fill(CardData data)
    {
        refData = data;
        epicness = (int)Random.Range(data.epicness.x, data.epicness.y);
        romance = (int)Random.Range(data.romance.x, data.romance.y);
        this.GetComponent<Image>().sprite = data.cardSprite;
        DebugDisappear = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugDisappear)
        {
            isDisappeared.Invoke();
        }
    }
}
