using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    private Animator anim;
    private Terminal terminal;

    [HideInInspector] public bool canMove = true;

    Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 movement;

    void Start()
    {
        anim = GetComponent<Animator>();
        terminal = FindObjectOfType<Terminal>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
        {
            anim.SetFloat("lastMoveX", movement.x);
            anim.SetFloat("lastMoveY", movement.y);
        }

        movement.Normalize();
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();

        if (item)
        {   
            //pick up item, displac name in terminal and set the input line to the bottom
            terminal.AddDirectoryLine("You picked up " + item.GetComponent<Item>().item.name);
            terminal.userInputLine.transform.SetAsLastSibling();
            inventory.AddItem(item.item, 1);
            FindObjectOfType<AudioManager>().Play("ItemPickup");
            Destroy(collision.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}

