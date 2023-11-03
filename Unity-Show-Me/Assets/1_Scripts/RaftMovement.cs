using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftMovement : MonoBehaviour, IMovable
{
    public CheckGround PlayerCheckGround;
    public WeaponManager WeaponManager;
    public PlayerController PlayerController;
    public GameObject PlayerHead;
    public Rigidbody rb { get; private set; }

    private Vector3 moveRaftIfPlayerLooksInThisDirection = new Vector3(0f, 180f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        WeaponManager.OnShoot += MoveRaft;
    }

    public void FixedUpdate()
    {
        Move(Vector3.forward, GameSettings.Instance.RaftMoveSpeed, ForceMode.Force);
    }

    private void MoveRaft()
    {
        if (PlayerCheckGround.IsOnRaft)
        {
            float angle = Vector3.Angle(PlayerHead.transform.eulerAngles, moveRaftIfPlayerLooksInThisDirection);

            if (angle < 30)
            {
                PlayerController.Move(this.transform.forward, GameSettings.Instance.RaftMoveForceOnGunShot / 6, ForceMode.Impulse);
                Move(Vector3.forward, GameSettings.Instance.RaftMoveForceOnGunShot, ForceMode.Impulse);
            }
        }
    }

    public void Move(Vector3 direction, float forceMultiplier, ForceMode forceMode)
    {
        rb.AddForce(direction * forceMultiplier, forceMode);
    }
}
