using UnityEngine;
using System.Collections;

public class JumpForwardQueue : MonoBehaviour
{
    public float jumpHeight = 2f; // ジャンプの高さ
    public float duration = 0.5f; // ジャンプにかかる時間
    public float step = 1f;       // 前に進む距離（1マス）
    [SerializeField] Transform[] enemies;

    public Vector3 size = new Vector3(1, 1, 1);

    private bool isJumping = false;
    private bool isFalling = false;

    void Update()
    {
        // 前の処理中は入力を無視
        if (!isJumping && !isFalling && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

        // 敵との衝突チェック
        foreach (Transform enemy in enemies)
        {
            if (CheckCubeCollision(transform.position, size, enemy.position, enemy.localScale))
            {
                Debug.Log(enemy.name + " と衝突！");
            }
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;
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

            foreach (Transform enemy in enemies)
            {
                if (CheckCubeCollision(newPos, size, enemy.position, enemy.localScale))
                {
                    Debug.Log(enemy.name + " に衝突したのでジャンプ中断");
                    isJumping = false;
                    yield break;
                }
            }

            transform.position = newPos;
            yield return null;
        }

        transform.position = end;
        isJumping = false;

        if (!IsGrounded(transform.position))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        if (isFalling) yield break;
        isFalling = true;

        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x, 0f, start.z);
        float fallDuration = 0.1f;
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
        isFalling = false;

        if (!IsGrounded(transform.position))
        {
            StartCoroutine(Fall());
        }
    }

    bool IsGrounded(Vector3 pos)
    {
        return pos.y <= 0.01f;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
