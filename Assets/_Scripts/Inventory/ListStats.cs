using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListStats : MonoBehaviour
{
    public List<string> stats;
    public GameObject statPrefab;

    private void Awake()
    {
        statPrefab = Resources.Load<GameObject>("UI/StatPrefab");
    }

    public void ShowListStats()
    {
        Helpers.DestroyChildren(transform);
        foreach (var i in stats)
        {
            GameObject stat = Instantiate(statPrefab, transform);
            var statText = stat.GetComponent<Text>();
            statText.text = i;
        }
    }
}
