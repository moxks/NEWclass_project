using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class player_Controller : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed = 75f;
    public float jumpForce;
    public Rigidbody rig;
    public int health;
    public int coinCount;

    public Animator anim;

    void Move()
    {
        // get the inpput axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 rotation = Vector3.up * rotateSpeed;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        //calculate a direction relative to where we are facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        //Set that as our velocity
        rig.velocity = dir;

        //rig.MoveRotation(rig.rotation + AngleRot)

        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    void tryJump()
    {
        //create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        //shoot the raycast
        if (Physics.Raycast(ray, 1.5f))
        {
            anim.SetTrigger("isJumping");
            rig.AddForce(Vector3.up * jumpForce, mode: ForceMode.Impulse);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //input for movement
        Move();

        //input for jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tryJump();
        }
        if (health <= 0)
        {
            anim.SetBool("die", true);
            StartCoroutine("DieButCool");
        }
    }
    IEnumerator DieButCool()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            health -= 100;
        }
        if (other.gameObject.name == "FallCollider")
        {
            SceneManager.LoadScene(0);
        }
    }

}
