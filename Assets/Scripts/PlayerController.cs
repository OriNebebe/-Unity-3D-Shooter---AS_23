using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]GameObject[] skillParticles;
    Animator animator;
    private IEnumerator coroutine;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;

    float horizontalInput, verticalInput;
    Rigidbody rigidbody;
    bool isGrounded, isRunning;


        void Start()
    {
        for (int i = 0; i < skillParticles.Length; i++)
        {
            skillParticles[i].SetActive(false);
        }
        
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        isGrounded = true;
        isRunning = false;
    }

        void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        verticalInput = Input.GetAxis("Vertical");
        animator.SetFloat("vertical", Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Alpha1)) transform.position = new Vector3(10, 2, 10);
        if (Input.GetKeyDown(KeyCode.Alpha2)) transform.position = new Vector3(0, 2, -10);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseSkill(1);

        void UseSkill(int skillNumber)
        {
            animator.SetTrigger("Skill" + skillNumber);
            skillParticles[skillNumber - 1].SetActive(true);
            coroutine = WaitToEnableObject(skillParticles[skillNumber - 1], 2);
            StartCoroutine(coroutine);
        }
        IEnumerator WaitToEnableObject(GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(false);

        }
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
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            Run(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            Run(false);
        }
    }
        public void Run(bool startMovement)
            {
        isRunning = startMovement;
            animator.SetBool("isRunning", isRunning);
            }

        public void Jump()
        {
            animator.SetTrigger("Jump");
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

