
using UnityEngine;

public class TestCharacter : MonoBehaviour, ITestCharacter
{
    public TestInventory Inventory { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }
    public void OnItemEquipped(TestItem item)
    {
        Debug.Log($"You equipped the {item} in {item.EquipSlots}"); 
    }
}
