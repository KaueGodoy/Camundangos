
using UnityEngine;

public class TestCharacter : MonoBehaviour, ITestCharacter
{
    public TestInventory Inventory { get; set; }
    public int Health { get; set; }
    public int Level { get; set; }
}
