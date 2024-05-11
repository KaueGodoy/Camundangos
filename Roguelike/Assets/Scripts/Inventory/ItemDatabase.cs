using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Localization.Settings;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; set; }

    private List<Item> Items { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {
        BuildDatabase();

    }

    private void BuildDatabase()
    {
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items_pt").ToString());
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[2])
        {
            Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items_jp").ToString());
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[3])
        {
            Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items_es").ToString());
        }
        else
        {
            Debug.LogWarning("Item database not found!");
        }


        //Debug.Log(Items[0].Stats[1].StatName + " level is " + Items[0].Stats[1].GetCalculatedStatValue());
        //Debug.Log(Items[0].ItemName);
    }

    public Item GetItem(string itemSlug)
    {
        foreach (Item item in Items)
        {
            if (item.ObjectSlug == itemSlug)
            {
                return item;
            }
        }

        Debug.LogWarning("couldn't find item: " + itemSlug);
        return null;

    }

}
