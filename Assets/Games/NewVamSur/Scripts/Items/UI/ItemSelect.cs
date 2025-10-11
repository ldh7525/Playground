using UnityEngine;

public class ItemSelect : MonoBehaviour
{
    [SerializeField] private GameObject menuParent;
    [SerializeField] private ItemPanel[] panels;


    private void Start()
    {
        Player.Instance.playerLevel.OnLevelup += ShowItemSelectMenu;
    }


    private void ShowItemSelectMenu()
    {
        menuParent.SetActive(true);

    }   



}
