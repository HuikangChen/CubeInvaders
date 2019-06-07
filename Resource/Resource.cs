using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float maxAmount;
    [HideInInspector]public float currentAmount;

    public delegate void ResourceHandler(float current, float max);
    public event ResourceHandler OnAdd;
    public event ResourceHandler OnTake;
    public event ResourceHandler OnFull;
    public event ResourceHandler OnEmpty;
    public event ResourceHandler OnInitialize;

    public virtual void OnEnable()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        currentAmount = maxAmount;
        if (OnInitialize != null)
            OnInitialize(currentAmount, maxAmount);
    }

    public virtual void Add(float amount)
    {
        if (currentAmount + amount >= maxAmount)
        {
            currentAmount = maxAmount;

            if (OnFull != null)
                OnFull(currentAmount, maxAmount);
        }
        else
        {
            currentAmount += amount;
        }

        if (OnAdd != null)
            OnAdd(currentAmount, maxAmount);
    }

    public virtual bool Take(float amount)
    {
        if (currentAmount - amount <= 0)
        {
            currentAmount = 0;

            if (OnTake != null)
                OnTake(currentAmount, maxAmount);

            if (OnEmpty != null)
                OnEmpty(currentAmount, maxAmount);

            return false;
        }

        currentAmount -= amount;

        if (OnTake != null)
            OnTake(currentAmount, maxAmount);

        return true;
    }

    public virtual bool SafeTake(float amount)
    {
        if (currentAmount - amount <= 0)
        {
            if (OnTake != null)
                OnTake(currentAmount, maxAmount);

            if (OnEmpty != null)
                OnEmpty(currentAmount, maxAmount);

            return false;
        }

        currentAmount -= amount;

        if (OnTake != null)
            OnTake(currentAmount, maxAmount);

        return true;
    }
}
