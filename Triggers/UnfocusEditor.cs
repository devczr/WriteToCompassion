

namespace WriteToCompassion.Triggers;

public class UnfocusEditor : TriggerAction<Editor>
{
    protected override void Invoke(Editor editor)
    {
        editor.Unfocus();
    }


}
