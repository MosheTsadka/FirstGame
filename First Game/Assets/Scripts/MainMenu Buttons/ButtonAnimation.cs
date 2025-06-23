using System;
using UnityEngine;


public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private float startMoveTime;
    [SerializeField] private float lengthTime;
    
    [SerializeField] private GameObject buttonGameObject;
    
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 scale;

    private void Start()
    {
        LeanTween.init();
    }

    private void Update()
    {
        startMoveTime -= Time.deltaTime;

        if (startMoveTime < 0)
        {
            MoveUpAnimation();
            ScaleUpAnimation();
        }
    }

    private void MoveUpAnimation()
    {
        LeanTween.move(buttonGameObject, endPosition, lengthTime);
    }

    private void ScaleUpAnimation()
    {
        LeanTween.scale(buttonGameObject, scale, lengthTime);
    }
}
