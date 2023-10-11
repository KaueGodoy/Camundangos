public interface ITestCharacter
{
    int Health { get; }
    TestInventory Inventory { get; }
    int Level { get; }
    void OnItemEquipped(TestItem item);
}