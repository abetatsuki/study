using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study08272a : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform enemy;

    private void Update()
    {
        CheckCubeCollision(player.position, player.localScale, enemy.position, enemy.localScale);
    }
    bool CheckCubeCollision(Vector3 posA, Vector3 sizeA, Vector3 posB, Vector3 sizeB)
    {
        Vector3 minA = posA - sizeA / 2f;
        Vector3 maxA = posA + sizeA / 2f;

        Vector3 minB = posB - sizeB / 2f;
        Vector3 maxB = posB + sizeB / 2f;

        // X, Y, Z 方向で重なりがあるかチェック
        bool overlapX = maxA.x >= minB.x && minA.x <= maxB.x;
        bool overlapY = maxA.y >= minB.y && minA.y <= maxB.y;
        bool overlapZ = maxA.z >= minB.z && minA.z <= maxB.z;

        if (overlapX && overlapY && overlapZ) Debug.Log("Hit");
        return overlapX && overlapY && overlapZ;
    }

}
