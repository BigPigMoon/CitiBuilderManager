namespace Engine.Components;

public class VisibilityComponent(Visibility visibility = Visibility.Visible)
{
    public Visibility Visibility { get; set; } = visibility;
}

public enum Visibility
{
    Visible,
    Hidden,
}
