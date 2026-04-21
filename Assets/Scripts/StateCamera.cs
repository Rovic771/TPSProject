using System.Collections;
using DG.Tweening;
using UnityEditor.Timeline;
using UnityEngine;

public class StateCamera : MonoBehaviour
{
    private Vector3 initPosition;
    private Quaternion initRotation;
    [SerializeField] private float waitTime = 3;

    public IEnumerator WaitChangeState()
    {
        yield return new WaitForSeconds(waitTime);
        ChangeToInitState();
    }
    
    void Start()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
    }

    public void ChangeToInitState()
    {
        transform.DORotate(initRotation.eulerAngles, 1);
    }

    public void ChangeToPlayerPos(Transform playerTransform)
    {
        transform.DORotate(playerTransform.rotation.eulerAngles, 1);
    }
}
