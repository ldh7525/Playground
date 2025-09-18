using TMPro;
using UnityEngine;

public class Apple : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rigid;
    TMP_Text valueText;

    public int value;

    // [SerializeField] private float bouncePower;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        value = Random.Range(1, 10);
        valueText = GetComponentInChildren<TMP_Text>();
        valueText.text = value.ToString();
    }

    public void ChosenOne()
    {
        sr.color = Color.yellow;
    }

    public void fuck()
    {
        sr.color = new Color32(255, 100, 100, 255);
    }

    public void ExecuteDie()
    {
        rigid.gravityScale = AppleManager.instance.gravity;
        float angle = Random.Range(60f, 120f);
        float rad = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        rigid.AddForce(direction * AppleManager.instance.bouncePower, ForceMode2D.Impulse);
    }
}
