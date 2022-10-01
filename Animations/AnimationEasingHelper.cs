
namespace WriteToCompassion.Animations;

public enum EasingType
{
    BounceIn,
    BounceOut,
    CubicIn,
    CubicInOut,
    CubicOut,
    Linear,
    SinIn,
    SinInOut,
    SinOut,
    SpringIn,
    SpringOut
}
public static class AnimationEasingHelper
{
    public static Easing GetEasing(EasingType type) => type switch
    {
        EasingType.BounceIn => Easing.BounceIn,
        EasingType.BounceOut => Easing.BounceOut,
        EasingType.CubicIn => Easing.CubicIn,
        EasingType.CubicInOut => Easing.CubicInOut,
        EasingType.CubicOut => Easing.CubicOut,
        EasingType.Linear => Easing.Linear,
        EasingType.SinIn => Easing.SinIn,
        EasingType.SinInOut => Easing.SinInOut,
        EasingType.SinOut => Easing.SinOut,
        EasingType.SpringIn => Easing.SpringIn,
        EasingType.SpringOut => Easing.SpringOut,
        _ => null,
    };
}
