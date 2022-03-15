using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float duration = 1f;
    public bool start = false;
    public AnimationCurve curve;
    public FollowPlayer camera;

    // Update is called once per frame
    void LateUpdate()
    {
     if (start)
        {
            
            StartCoroutine(Shaking());
        }   
    }

    public IEnumerator Shaking() 
    {
        Vector3 startPosition = camera.newPos;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere;
            yield return null;
        }
        transform.position = startPosition;
        start = false;
    }
}
