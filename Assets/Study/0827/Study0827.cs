using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study08272 : MonoBehaviour
{
    [SerializeField]Transform A;
    [SerializeField] Transform B;
    private void Start()
    {
        A.position = new Vector3(1, 3, 5);
        B.position = new Vector3(5, 7, 9);
       

        Vector3 a  = B.position - A.position;
        float dif = a.magnitude;
        Debug.Log(dif);

        float distance = Vector3.Distance(A.position, B.position);
        Debug.Log(a);
        Debug.Log(distance);

    }


  
}
