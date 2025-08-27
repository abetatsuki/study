using UnityEngine;
using System.Collections;

public class JumpForward1 : MonoBehaviour
{
 
    public float jumpHeight = 2f; // ジャンプの高さ
    public float duration = 0.5f; // ジャンプにかかる時間
    public float step = 1f;       // 前に進む距離（1マス）
    [SerializeField] Transform player;
    [SerializeField] Transform[] enemies;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(Jump());
        foreach (Transform enemy in enemies)
    {
        if (CheckCubeCollision(player.position, player.localScale, enemy.position, enemy.localScale))
        {
            Debug.Log(enemy.name + " と衝突！");
        }
    }

    }

    IEnumerator Jump()
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(step, 0, 0); // x方向に1マス
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

            // x方向は線形補間
            float x = Mathf.Lerp(start.x, end.x, normalized);

            // y方向は放物線
            float y = start.y + jumpHeight * 4 * normalized * (1 - normalized);

            // 更新
            transform.position = new Vector3(x, y, start.z);

            // 複数敵に対して衝突チェック
            foreach (Transform enemy in enemies)
            {
                if (CheckCubeCollision(transform.position, size, enemy.position, enemy.localScale))
                {
                    Debug.Log(enemy.name + " に衝突したのでジャンプ中断");
                    yield break; // Coroutine を途中で終了
                }
               
            }

            yield return null;
        }

        transform.position = end; // 最終位置補正
        
    }




    public Vector3 size = new Vector3(1, 1, 1);

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;


        Gizmos.DrawWireCube(transform.position, size);
    }


    public bool CheckCubeCollision(Vector3 posA, Vector3 sizeA, Vector3 posB, Vector3 sizeB)
    {
        Vector3 minA = posA - sizeA / 2f;
        Vector3 maxA = posA + sizeA / 2f;

        Vector3 minB = posB - sizeB / 2f;
        Vector3 maxB = posB + sizeB / 2f;

        bool overlapX = maxA.x >= minB.x && minA.x <= maxB.x;
        bool overlapY = maxA.y >= minB.y && minA.y <= maxB.y;
        bool overlapZ = maxA.z >= minB.z && minA.z <= maxB.z;

        
        return overlapX && overlapY && overlapZ;
    }
}
