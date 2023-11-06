using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Item : MonoBehaviour, IItemable 
{
    public string Name { get { return namee; } }
    [SerializeField]
    private string namee;
    public int Amount { get { return amount; } }
    [SerializeField]
    private int amount;
    public Sprite ItemSprite { get { return sprite; } }
    [SerializeField]
    private Sprite sprite;

    private Collider boxCollider;

    public void Start()
    {
        boxCollider = GetComponent<Collider>();
        SetSprite();
    }

    public void PickUp()
    {
        boxCollider.enabled = false;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InventoryManager.Instance.PickUpItem(this.gameObject);
        }
    }

    public void SetSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) { Debug.LogError(this.name + " has no SpriteRenderer"); }
        spriteRenderer.sprite = ItemSprite;
    }
}