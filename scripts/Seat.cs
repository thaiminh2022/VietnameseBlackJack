using Godot;

public partial class Seat : Node2D {
    [Export] float childRotation;

    public override void _Ready() {
        foreach (var child in GetChildren()) {
            if (child is ISeatable seatable) {
                seatable.RotateVisual(childRotation);
            }
        }
    }
}