using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeItem : MonoBehaviour
{
    [SerializeField] private float itemTime = 0f;
    public TimerForTimeTrial timer;
    public GameObject BonusGO;
    public TMP_Text bonusTimeText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer.PlayCollectSound();
            timer.IncreaseTime(itemTime); // Increase timer by 10 seconds (or however much time you want to add)
            SetTextBonusTime();
            Destroy(gameObject); // Destroy the item object once it has been collected
        }
    }

    private void SetTextBonusTime()
    {
        BonusGO.SetActive(true);
        bonusTimeText.text = "+ " + itemTime + "s";
    }
}
