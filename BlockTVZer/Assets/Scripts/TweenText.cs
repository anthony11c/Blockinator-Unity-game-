using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenText : MonoBehaviour
{
    public float tweenTime;

    // Start is called before the first frame update
    void Start()
    {
        Tween();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tween()
    {
        LeanTween.cancel(gameObject);

        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * 3, tweenTime).setEasePunch();
    }
}
