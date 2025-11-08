using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    float speed = 5f;
    bool isInput;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }

    }


    void FixedUpdate()
    {
            Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            rb.linearVelocity = m_Input * Time.fixedDeltaTime * speed;
    }
}
