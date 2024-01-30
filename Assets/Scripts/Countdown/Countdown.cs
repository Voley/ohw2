using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public Action OnCountdownEnded;

    [SerializeField] TMP_Text _buttonText;

    public void StartCountdown()
    {
        StartCoroutine(BeginCountdownCoroutine());
    }

    private IEnumerator BeginCountdownCoroutine()
    {
        int counter = 3;

        while (counter > 0) {

            _buttonText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }

        OnCountdownEnded();
    }

}
