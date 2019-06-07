using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDisplayCell : ResourceDisplay
{
    public GameObject cellPrefab;
    public Transform cellStart;
    List<GameObject> cells = new List<GameObject>();

    protected override void UpdateDisplay(float current, float max)
    {
        base.UpdateDisplay(current, max);
        if (current > cells.Count)
        {
            int amountToAdd = (int)current - cells.Count;

            for (int i = 0; i < amountToAdd; i++)
            {
                AddCell();
            }
        }

        if (current < cells.Count)
        {
            int amountToTake = cells.Count - (int)current;

            for (int i = 0; i < amountToTake; i++)
            {
                TakeCell();
            }
        }
    }

    void AddCell()
    {
        GameObject spawnedCell = PoolManager.Spawn(cellPrefab, cellStart.position, Quaternion.identity);
        spawnedCell.transform.SetParent(cellStart);
        spawnedCell.transform.localScale = new Vector3(1, 1, 1);
        cells.Add(spawnedCell);
    }

    void TakeCell()
    {
        GameObject removedCell = cells[cells.Count - 1];       
        cells.RemoveAt(cells.Count - 1);
        PoolManager.Despawn(removedCell);
    }
}