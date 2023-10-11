using NSubstitute;
using NUnit.Framework;

public class test_inventory
{
    [Test]
    public void only_allows_one_chest_to_be_equipped_at_a_time()
    {
        // ARRANGE
        ITestCharacter character = Substitute.For<ITestCharacter>();
        TestInventory inventory = new TestInventory(character);
        TestItem chestOne = new TestItem() { EquipSlots = EquipSlots.Chest };
        TestItem chestTwo = new TestItem() { EquipSlots = EquipSlots.Chest };

        // ACT
        inventory.EquipItem(chestOne);
        inventory.EquipItem(chestTwo);

        // ASSERT
        TestItem equippedItem = inventory.GetItem(EquipSlots.Chest);
        Assert.AreEqual(chestTwo, equippedItem);

    }

    [Test]
    public void tells_character_when_an_item_is_equipped_successfully()
    {
        // ARRANGE
        ITestCharacter character = Substitute.For<ITestCharacter>();
        TestInventory inventory = new TestInventory(character);
        TestItem chestOne = new TestItem() { EquipSlots = EquipSlots.Chest };

        // ACT
        inventory.EquipItem(chestOne);

        // ASSERT
        character.Received().OnItemEquipped(chestOne);
    }
}
