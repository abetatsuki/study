using UnityEngine;
using System.Collections;
public class JumpForward12 : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float duration = 0.5f;
    public float step = 1f;
    [SerializeField] Transform player;
    [SerializeField] Transform[] enemies;

    public Vector3 size = new Vector3(1, 1, 1);

    private Coroutine currentJump;   // 現在のジャンプコルーチン
    private Coroutine currentFall;   // 現在の落下コルーチン

    void Update()
    {
        // ジャンプ入力
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 既存のジャンプ・落下を停止
            if (currentJump != null) StopCoroutine(currentJump);
            if (currentFall != null) StopCoroutine(currentFall);

            currentJump = StartCoroutine(Jump());
        }

        // 敵との衝突チェック
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
        Vector3 end = start + new Vector3(step, 0, 0);
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

            float x = Mathf.Lerp(start.x, end.x, normalized);
            float y = start.y + jumpHeight * 4 * normalized * (1 - normalized);

            Vector3 newPos = new Vector3(x, y, start.z);

            // 衝突チェック
            foreach (Transform enemy in enemies)
            {
                if (CheckCubeCollision(newPos, size, enemy.position, enemy.localScale))
                {
                    Debug.Log(enemy.name + " に衝突したのでジャンプ中断");
                    yield break;
                }
            }

            transform.position = newPos;
            yield return null;
        }

        transform.position = end;

        // ジャンプ後に落下
        if (!IsGrounded(transform.position))
        {
            currentFall = StartCoroutine(Fall());
        }

        currentJump = null;
    }

    IEnumerator Fall()
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x, 0f, start.z);
        float fallDuration = 0.5f;
        float t = 0f;

        while (t < fallDuration)
        {
            t += Time.deltaTime;
            float normalized = t / fallDuration;
            Vector3 newPos = Vector3.Lerp(start, end, normalized);

            if (newPos.y <= 0f)
            {
                newPos.y = 0f;
                transform.position = newPos;
                break;
            }

            transform.position = newPos;
            yield return null;
        }

        transform.position = end;

        // 再落下チェック
        if (!IsGrounded(transform.position))
        {
            currentFall = StartCoroutine(Fall());
        }
        else
        {
            currentFall = null;
        }
    }

    bool IsGrounded(Vector3 pos) => pos.y <= 0.01f;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
