using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public abstract partial class SimpleArea3D : ShapeCast3D
{
	public HashSet<CollisionObject3D> objects_inside;
	public override void _EnterTree()
	{
		Map.bodyDetects.Add(this);
		objects_inside = new(MaxResults);
	}
	public override void _ExitTree()
	{
		Map.bodyDetects.Remove(this);
	}

	public void PhysicsUpdate()
	{
		int col_count = GetCollisionCount();
		int i;

		Span<CollisionObject3D> current_detected_bodies = new CollisionObject3D[col_count];
		for (i = 0; i < col_count; i++)
		{
			current_detected_bodies[i] = (CollisionObject3D)GetCollider(i);
			ref CollisionObject3D collisionObject = ref current_detected_bodies[i];
			if (objects_inside.Add(collisionObject)) // adds and makes sure it's the first entrance
			{
				OnBodyEnter(in collisionObject);
			}
		}
		// check if the current detected bodies don't include the previously detected ones 
		foreach (CollisionObject3D collisionObject in objects_inside)
		{
			bool is_outdated = true;
			for (i = 0; i < col_count; ++i)
			{
				if (collisionObject == current_detected_bodies[i])
				{
					is_outdated = false;
					break;
				}
			}
			if (is_outdated)
			{
				objects_inside.Remove(collisionObject);
				OnBodyExit(in collisionObject);
			}
		}
	}
	public abstract void OnBodyEnter(in CollisionObject3D collisionObject);
	public abstract void OnBodyExit(in CollisionObject3D collisionObject);
}
