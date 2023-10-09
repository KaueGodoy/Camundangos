using NSubstitute;
using NUnit.Framework;

public class character_with_inventory
{
    [Test]
    public void with_90_armor_takes_10_percent_damage()
    {
        // ARRANGE
        ITestCharacter character = Substitute.For<ITestCharacter>();
        TestInventory inventory = new TestInventory();
        TestItem pants = new TestItem() { EquipSlots = EquipSlots.Legs, Armor = 40 };
        TestItem shield = new TestItem() { EquipSlots = EquipSlots.RightHand, Armor = 50 };

        inventory.EquipItem(pants);
        inventory.EquipItem(shield);

        character.Inventory.Returns(inventory);

        // ACT
        int calculatedDamage = DamageCalculator.CalculateDamage(1000, character);

        // ASSERT
        Assert.AreEqual(100, calculatedDamage);

    }
}
