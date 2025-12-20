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
    public Sprite soilTilled, soilWatered;

    public SpriteRenderer cropSR;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;

    public bool isWatered = false;

    public bool preventUse;

    private Vector2Int gridPostion;

    public CropController.CropType cropType;
    public float growChanceFail;

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

#if UNITY_EDITOR

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif
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
            if (isWatered == true) 
            {
                sr.sprite = soilWatered;
            }
            else
            {
                sr.sprite = soilTilled;
            }           
        }

        UpdateGridInfo();
    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
        }
    }

    public void WaterSoil()
    {
        if(preventUse == false)
        {
            isWatered = true;

            SetSoilSprite();
        }
    }

    public void PlantCrop(CropController.CropType cropToPlant)
    {
        if(currentStage == GrowthStage.ploughed && isWatered == true && preventUse == false)
        {
            currentStage = GrowthStage.planted;

            cropType = cropToPlant;

            growChanceFail = CropController.instance.GetCropInfo(cropType).growthFailChance;

            UpdateCropSprite();
        }
    }

    public void UpdateCropSprite()
    {
        CropInfo activeCrop = CropController.instance.GetCropInfo(cropType);


        switch (currentStage)
        {
            case GrowthStage.planted:

                //cropSR.sprite = cropPlanted;
                cropSR.sprite = activeCrop.planted;

                break;

            case GrowthStage.growing1:

                //cropSR.sprite = cropGrowing1;
                cropSR.sprite = activeCrop.growState1;

                break;

            case GrowthStage.growing2:

                //cropSR.sprite = cropGrowing2;
                cropSR.sprite = activeCrop.growState2;

                break;

            case GrowthStage.ripe:

                //cropSR.sprite = cropRipe;
                cropSR.sprite = activeCrop.ripe;

                break;
        }

        UpdateGridInfo();
    }

    public void AdvanceCrop()
    {
        if(isWatered == true && preventUse == false)
        {
            if(currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;

                isWatered = false;
                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if(currentStage == GrowthStage.ripe && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();

            cropSR.sprite = null;

            CropController.instance.AddCrop(cropType);
        }
    }

    public void SetGridPosition(int x, int y)
    {
        gridPostion = new Vector2Int(x, y);
    }

    void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, gridPostion.x, gridPostion.y);
    }

}
