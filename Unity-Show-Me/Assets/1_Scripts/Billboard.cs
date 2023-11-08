using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Billboard : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 0.5f);
    }
}
