using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OvniSM : MonoBehaviour
{

    private Coroutine currentState;
    [SerializeField] private float m_interpolationTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IdleState());
    }

    
    private IEnumerator IdleState()
    {
        Debug.Log("SCANING");
        
       yield return new WaitForSeconds(2);
        SwitchState(MovingState());
    }

    private IEnumerator MovingState()
    {
        float x = Random.Range(0, 20f);
        float z = Random.Range(0, 20f);

        Vector3 target = new Vector3(x, 10, z);
        

        for (float i = 0; i < m_interpolationTime; i += Time.deltaTime)
        {
            float t = i / m_interpolationTime;
            transform.position = Vector3.Lerp(transform.position, target, t);
            yield return null;
        }
       
        SwitchState(AttakingState());
    }

    private IEnumerator AttakingState()
    {
        Debug.Log("Attaking");
        yield return new WaitForSeconds(0.5f);
        SwitchState(IdleState());
    }

    private void SwitchState(IEnumerator state)
    {

        if (currentState != null) StopCoroutine(currentState);


        if (state == null) return;


        currentState = StartCoroutine(state);
    }





}
