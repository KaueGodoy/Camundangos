using NUnit.Framework;

public class test_inventory
{
    [Test]
    public void only_allows_one_chest_to_be_equipped_at_a_time()
    {
        // ARRANGE
        TestInventory inventory = new TestInventory();
        TestItem chestOne = new TestItem() { EquipSlots = EquipSlots.Chest };
        TestItem chestTwo = new TestItem() { EquipSlots = EquipSlots.Chest };

        // ACT
        inventory.EquipItem(chestOne);
        inventory.EquipItem(chestTwo);

        // ASSERT
        TestItem equippedItem = inventory.GetItem(EquipSlots.Chest);
        Assert.AreEqual(chestTwo, equippedItem);

    }
}
