using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;

    float horizontalInput, verticalInput;
    Rigidbody rigidbody;
    bool isGrounded, isRunning;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        isGrounded = true;
        isRunning = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Alpha1)) transform.position = new Vector3(10, 2, 10);
        if (Input.GetKeyDown(KeyCode.Alpha2)) transform.position = new Vector3(0, 2, -10);

        if (isRunning)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 2 * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * 2 * horizontalInput);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }

    //Aby dzia³a³ skok nale¿y pod³odze nadaæ tag Ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}

