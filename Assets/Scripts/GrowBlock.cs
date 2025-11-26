using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    public enum GrowthStage 
    { 
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowthStage currentStage;

    public SpriteRenderer sr;
    public Sprite soilTilled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Keyboard.current.eKey.wasPressedThisFrame)
        //{
        //    AdvanceStage();

        //    SetSoilSprite();
        //}
    }

    public void AdvanceStage()
    {
        currentStage = currentStage + 1;

        if ((int)currentStage >= 6)
        {
            currentStage = GrowthStage.barren;
        }
    }

    public void SetSoilSprite()
    {
        if (currentStage == GrowthStage.barren)
        {
            sr.sprite = null;
        }
        else 
        {
            sr.sprite = soilTilled;
        }
    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
        }
    }
}
