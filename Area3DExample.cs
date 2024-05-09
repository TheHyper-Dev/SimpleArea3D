using System;
using Godot;

[GlobalClass]
public sealed partial class Area3DExample : SimpleArea3D
{
    public override void _EnterTree()
    {
        base._EnterTree();
    }
    public override void _ExitTree()
    {
        base._ExitTree();
    }
    public override void OnBodyEnter(in CollisionObject3D collisionObject)
    {
        GD.Print($"object {collisionObject.Name} has just entered");
    }

    public override void OnBodyExit(in CollisionObject3D collisionObject)
    {
        GD.Print($"object {collisionObject.Name} has just exit");
    }
}
