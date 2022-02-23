using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] Image gas;
    [SerializeField] float duration;
    [SerializeField] float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = duration;
        StartCoroutine(TimeIEn());
    }

    IEnumerator TimeIEn(){
        while(currentTime >= 0){
            gas.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
    }
}
