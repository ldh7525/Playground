using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void OnGet(Sprite sprite)
    {
        gameObject.SetActive(true);
        transform.SetParent(GemManager.Instance.transform);

        spriteRenderer.sprite = sprite;
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: ¤¾¤¾
        Player.Instance.playerLevel.AddExperience(1);
    }
}
