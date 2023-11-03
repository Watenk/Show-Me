using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool IsOnGround { get; private set; }

    public bool IsOnRaft { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsOnGround = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Raft"))
        {
            IsOnRaft = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsOnGround = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Raft"))
        {
            IsOnRaft = false;
        }
    }
}
