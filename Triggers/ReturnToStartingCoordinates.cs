using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteToCompassion.Animations;

namespace WriteToCompassion.Triggers
{
    public class ReturnToStartingCoordinates : TriggerAction<VisualElement>
    {
        public AnimationBase Animation { get; set; }
        protected override async void Invoke(VisualElement sender)
        {

        }
    }
}
