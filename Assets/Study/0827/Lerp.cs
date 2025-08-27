using UnityEngine;
using System.Collections;
public class SimpleMove : MonoBehaviour
{
    public Vector3 destination = new Vector3(5, 0, 0);
    public float delay = 1f;
    public float duration = 1f;

    void Start()
    {
        StartCoroutine(Move());
       
    }

    IEnumerator Move()
    {
        float t = 0f;
        Vector3 start = transform.position;

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, destination, t / duration);
            yield return null;
        }

        transform.position = destination; 
    }
}
