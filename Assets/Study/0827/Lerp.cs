using UnityEngine;
using System.Collections;

public class JumpExample : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float duration = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        Vector3 start = transform.position;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

           
            float x = start.x;
            float z = start.z;

            
            float y = start.y + jumpHeight * 4 * normalized * (1 - normalized);

            transform.position = new Vector3(x, y, z);
            yield return null;
        }

       
        transform.position = start;
    }
}
