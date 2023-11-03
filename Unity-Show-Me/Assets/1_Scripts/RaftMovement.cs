using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftMovement : MonoBehaviour, IMovable
{
    public CheckGround PlayerCheckGround;
    public WeaponManager WeaponManager;
    public GameObject PlayerHead;

    public Rigidbody rb { get; private set; }

    private Vector3 raftShootDirection = new Vector3(0f, 180f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        WeaponManager.OnShoot += MoveRaft;
    }

    private void MoveRaft()
    {
        if (PlayerCheckGround.IsOnRaft)
        {
            float angle = Vector3.Angle(PlayerHead.transform.eulerAngles, raftShootDirection);

            if (angle < 30)
            {
                Move(Vector3.forward, 10, ForceMode.Impulse);
            }
        }
    }

    public void Move(Vector3 direction, float forceMultiplier, ForceMode forceMode)
    {
        rb.AddForce(direction * forceMultiplier, forceMode);
    }
}
