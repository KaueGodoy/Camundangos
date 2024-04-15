using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AI_GameController : MonoBehaviour
{
    public Graph graph;
    public TMP_Dropdown cityDropdown;
    public TMP_Dropdown searchDropdown;
    public TextMeshProUGUI resultText;

    void Start()
    {
        // Example: Creating some cities and connections
        City cityA = new City("A");
        City cityB = new City("B");
        City cityC = new City("C");
        City cityD = new City("D");

        cityA.AddNeighbor(cityB);
        cityA.AddNeighbor(cityC);
        cityB.AddNeighbor(cityD);
        cityC.AddNeighbor(cityD);

        graph.AddCity(cityA);
        graph.AddCity(cityB);
        graph.AddCity(cityC);
        graph.AddCity(cityD);

        // Populate the city dropdown options
        List<TMP_Dropdown.OptionData> cityOptions = graph.Cities.Select(city => new TMP_Dropdown.OptionData(city.Name)).ToList();
        cityDropdown.ClearOptions();
        cityDropdown.AddOptions(cityOptions);

        // Populate the search dropdown options
        List<string> searchOptions = new List<string> { "BFS", "DFS" };
        searchDropdown.ClearOptions();
        searchDropdown.AddOptions(searchOptions);
    }

    public void OnSearchButtonClicked()
    {
        string selectedCityName = cityDropdown.options[cityDropdown.value].text;
        City startCity = graph.Cities.Find(city => city.Name == selectedCityName);

        if (startCity == null)
        {
            Debug.LogError("City not found in graph.");
            return;
        }

        string selectedSearch = searchDropdown.options[searchDropdown.value].text;

        City randomTargetCity = graph.Cities[Random.Range(0, graph.Cities.Count)];

        List<City> path;
        if (selectedSearch == "BFS")
        {
            path = graph.BFS(startCity, randomTargetCity);
        }
        else // selectedSearch == "DFS"
        {
            path = graph.DFS(startCity, randomTargetCity);
        }

        if (path != null)
        {
            string pathString = string.Join(" -> ", path.Select(city => city.Name).ToArray());
            resultText.text = $"Path: {pathString} (Length: {path.Count - 1})";
        }
        else
        {
            resultText.text = "No path found.";
        }
    }
}
