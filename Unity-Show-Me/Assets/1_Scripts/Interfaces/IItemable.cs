using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemable
{
    string Name { get; }
    int Amount { get; }
    Sprite ItemSprite { get; }

    void SetSprite();
    void PickUp();
}
