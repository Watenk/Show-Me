using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
public class GameSettings : ScriptableObject
{
    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameSettings>("GameSettings");
                if (instance == null)
                {
                    Debug.LogError("Couldn't find GameSettings in Resources folder");
                }
            }
            return instance;
        }
    }
    private static GameSettings instance;

    [Header("Cursor Settings")]
    public bool CursorVisible;
    public bool CursorLocked;

    [Header("Player Settings")]
    public float PlayerSpeed;
    public float PlayerHeadRotationSpeed;
    public float PlayerJumpStrenght;
    public int PlayerMaxHealth;
    public float PlayerKnockback;

    [Header("Bullet Settings")]
    public int BulletHealth;
    public float NormalBulletSpeed;
    public int NormalBulletDamage;

    [Header("Raft Settings")]
    public float RaftMoveForceOnGunShot;
    public float RaftMoveSpeed;

    [Header("Enemy Settings")]
    public float EnemytargetRange;
    public float EnemyShootDelay;
    public int EnemyHealth;
}
