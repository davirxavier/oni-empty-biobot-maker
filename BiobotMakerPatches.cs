namespace EmptyBiobotMaker;

using HarmonyLib;
using KMod;
using UnityEngine;

public class EmptyBiobotMaker : UserMod2
{
    public override void OnLoad(Harmony harmony)
    {
        base.OnLoad(harmony);
        
        Strings.Add(CUSTOM_STRINGS.SLIDER.TITLE.key.String, CUSTOM_STRINGS.SLIDER.TITLE.text);
        Strings.Add(CUSTOM_STRINGS.SLIDER.UNITS.key.String, CUSTOM_STRINGS.SLIDER.UNITS.text);
        Strings.Add(CUSTOM_STRINGS.SLIDER.TOOLTIP.key.String, CUSTOM_STRINGS.SLIDER.TOOLTIP.text);
    }

    [HarmonyPatch(typeof(BuildingLoader), "CreateBuildingComplete")]
    public static class MorbRoverMaker_CreateBuildingComplete_Patches
    {
        static void Postfix(GameObject go, BuildingDef def)
        {
            if (def.PrefabID == "MorbRoverMaker")
            {
                go.AddOrGet<DropAllWorkable>();
                go.AddOrGet<MorbRoverMakerSlider>();
            }
        }
    }

    [HarmonyPatch(typeof(MorbRoverMaker.Instance), "StartSM")]
    public static class Test
    {
        static void Postfix(MorbRoverMaker.Instance __instance)
        {
            var slider = __instance.GetComponent<MorbRoverMakerSlider>();
            var delivery = __instance.GetComponent<ManualDeliveryKG>();
            var emptyWorkable = __instance.GetComponent<DropAllWorkable>();
            delivery.capacity = slider.GetSliderValue(0);

            slider.OnValueSet = val =>
            {
                var oldCapacity = delivery.capacity;
                delivery.capacity = val;
                
                if (oldCapacity > val)
                {
                    emptyWorkable.DropAll();
                } 
                else if (oldCapacity < val)
                {
                    delivery.RequestDelivery();
                }
            };
        }
    }
}