using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 m_targetScale;
    
    // Start is called before the first frame update
    void Start()
    {
        
        transform.DOScale(m_targetScale, 1f);

    }

}
