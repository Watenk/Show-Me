using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDamagable, IMovable
{
    public GameObject Head;
    public GameObject Body;
    public WeaponManager WeaponManager;
    public int MaxHealth { get; set; }
    public int Health { get; set; }

    private Vector3 direction = Vector3.zero;

    //References
    private Camera cam;
    private CheckGround checkGround;

    public Rigidbody rb {  get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) { Debug.LogError("PlayerController Couldn't find a rigidbody"); }

        cam = GetComponentInChildren<Camera>();
        if (cam == null) { Debug.LogError("PlayerController Couldn't find a Camera In Children"); }

        checkGround = GetComponentInChildren<CheckGround>();
        if (checkGround == null) { Debug.LogError("PlayerController Couldn't find CheckGround Script in Children"); }
    }

    void Start()
    {
        InputManager.Instance.OnW += MoveForwards;
        InputManager.Instance.OnA += MoveLeft;
        InputManager.Instance.OnS += MoveBackwards;
        InputManager.Instance.OnD += MoveRight;
        InputManager.Instance.OnMouseMovement += MouseMovement;
        InputManager.Instance.OnSpaceDown += Jump;
        WeaponManager.OnShoot += Shoot;

        MaxHealth = GameSettings.Instance.PlayerMaxHealth;
        Health = MaxHealth;
    }

    private void FixedUpdate()
    {
        //Some extra Gravity
        rb.AddForce(new Vector3(0, -0.5f, 0), ForceMode.Impulse);

        //MovePlayer
        direction.Normalize();
        direction *= GameSettings.Instance.PlayerSpeed;
        rb.AddForce(direction);
        direction = Vector3.zero;
    }

    private void Shoot()
    {
        Move(-Head.transform.forward, GameSettings.Instance.PlayerKnockback, ForceMode.Impulse);
    }

    private void MoveForwards()
    {
        direction += Body.transform.forward;
    }

    private void MoveLeft()
    {
        direction += -Body.transform.right;
    }

    private void MoveBackwards()
    {
        direction += -Body.transform.forward;
    }

    private void MoveRight()
    {
        direction += Body.transform.right;
    }

    private void Jump()
    {
        if (checkGround.IsOnGround)
        {
            rb.AddForce(new Vector3(0, GameSettings.Instance.PlayerJumpStrenght, 0), ForceMode.Impulse);
        }
    }

    private void MouseMovement(Vector2 mouseDir)
    {
        float newCameraX = Head.transform.eulerAngles.x - mouseDir.x * GameSettings.Instance.PlayerHeadRotationSpeed;
        float newPlayerY = Body.transform.eulerAngles.y + mouseDir.y * GameSettings.Instance.PlayerHeadRotationSpeed;

        Head.transform.localEulerAngles = new Vector3(newCameraX, 0, 0);
        Body.transform.localEulerAngles = new Vector3(0, newPlayerY, 0);
    }

    public void TakeDamage(int _amount)
    {
        Health -= _amount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Died");
    }

    public void Move(Vector3 direction, float forceMultiplier, ForceMode forceMode)
    {
        rb.AddForce(direction * forceMultiplier, forceMode);
    }
}
