using UnityEngine;
using System.Collections;

public class JumpForward1 : MonoBehaviour
{
 
    public float jumpHeight = 2f; // �W�����v�̍���
    public float duration = 0.5f; // �W�����v�ɂ����鎞��
    public float step = 1f;       // �O�ɐi�ދ����i1�}�X�j
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
            Debug.Log(enemy.name + " �ƏՓˁI");
        }
    }

    }

    IEnumerator Jump()
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(step, 0, 0); // x������1�}�X
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;

            // x�����͐��`���
            float x = Mathf.Lerp(start.x, end.x, normalized);

            // y�����͕�����
            float y = start.y + jumpHeight * 4 * normalized * (1 - normalized);

            // �X�V
            transform.position = new Vector3(x, y, start.z);

            // �����G�ɑ΂��ďՓ˃`�F�b�N
            foreach (Transform enemy in enemies)
            {
                if (CheckCubeCollision(transform.position, size, enemy.position, enemy.localScale))
                {
                    Debug.Log(enemy.name + " �ɏՓ˂����̂ŃW�����v���f");
                    yield break; // Coroutine ��r���ŏI��
                }
               
            }

            yield return null;
        }

        transform.position = end; // �ŏI�ʒu�␳
        
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
