using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;

    public Sprite itemIcon;

    public GameObject blockPrefab;
}
