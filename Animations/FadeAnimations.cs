using CommunityToolkit.Maui.Animations;



namespace WriteToCompassion.Animations
{
    public class FadeAnimations : BaseAnimation
    {
        public double Scale { get; set; }

        public override Task Animate(VisualElement view) => view.ScaleTo(Scale, Length, Easing);


    }
}
