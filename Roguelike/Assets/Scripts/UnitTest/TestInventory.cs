using System.Collections.Generic;
using System.Linq;

public class TestInventory
{
    Dictionary<EquipSlots, TestItem> _equippedItems = new Dictionary<EquipSlots, TestItem>();
    List<TestItem> _unequippedItems = new List<TestItem>();

    private readonly ITestCharacter _character;

    public TestInventory(ITestCharacter character)
    {
        _character = character;
    }

    public void EquipItem(TestItem item)
    {
        if (_equippedItems.ContainsKey(item.EquipSlots))
            _unequippedItems.Add(_equippedItems[item.EquipSlots]);

        // this key = that value
        _equippedItems[item.EquipSlots] = item;

        _character.OnItemEquipped(item);
    }

    public TestItem GetItem(EquipSlots equipSlot)
    {
        if (_equippedItems.ContainsKey(equipSlot))
            return _equippedItems[equipSlot];

        return null;
    }

    public int GetTotalArmor()
    {
        int totalArmor = _equippedItems.Values.Sum(t => t.Armor);
        return totalArmor;
    }
}