namespace EmptyBiobotMaker;

using KSerialization;
using UnityEngine;
using UnityEngine.Serialization;

[SerializationConfig(MemberSerialization.OptIn)]
public class MorbRoverMakerSlider : KMonoBehaviour, ISingleSliderControl
{
    public float sliderMax = 1800f;
    public float sliderMin = 300f;
    public string titleKey = CUSTOM_STRINGS.SLIDER.TITLE.key.String;
    public string units = Strings.TryGet(CUSTOM_STRINGS.SLIDER.UNITS.key, out StringEntry entry) ? entry.String : "kg";
    public string tooltipKey = CUSTOM_STRINGS.SLIDER.TOOLTIP.key.String;
    public int decimalPlaces = 0;
    
    public delegate void CustomSlideHandlerFn(float val);
    public CustomSlideHandlerFn OnValueSet;
    
    [SerializeField]
    [Serialize]
    private float currentValue  = 1800f;

    public string SliderTitleKey => titleKey;
    public string SliderUnits => units;
    public int SliderDecimalPlaces(int index) => decimalPlaces;
        
    public float GetSliderMin(int index) => sliderMin;
    public float GetSliderMax(int index) => sliderMax;
    public float GetSliderValue(int index) => currentValue;

    public void SetSliderValue(float val, int index)
    {
        currentValue = val;
        OnValueSet?.Invoke(val);
    }

    public string GetSliderTooltipKey(int index) => tooltipKey;
    public string GetSliderTooltip(int index) => CUSTOM_STRINGS.SLIDER.TOOLTIP.text;
}