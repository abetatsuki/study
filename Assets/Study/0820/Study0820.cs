using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoopExample : MonoBehaviour
{
    private Queue<int> cc = new Queue<int>();
    private Stack<int> s = new Stack<int>();

    void Start()
    {
        // 0Å`4 Çäiî[
        for (int i = 0; i <= 4; i++)
        {
            cc.Enqueue(i);
            s.Push(i);
        }

        // ÉRÉãÅ[É`ÉìäJén
        StartCoroutine(PrintLoop());
    }

    private IEnumerator PrintLoop()
    {
        while (true) // ñ≥å¿ÉãÅ[Év
        {
            // 1Å`4 (Queue)
            foreach (int num in cc)
            {
                if (num > 0) Debug.Log(num);
                yield return new WaitForSeconds(0.5f); // 0.5ïbë“Ç¬
            }

            // 4Å`0 (Stack)
            foreach (int num in s)
            {
                Debug.Log(num);
                yield return new WaitForSeconds(0.5f);
            }

          
        }
    }
}
