using Godot;
using System;
using System.Collections.Generic;

public partial class Map : WorldEnvironment
{
	public static Map singleton;
	public static HashSet<SimpleArea3D> SimpleArea3Ds = new(30);

	public override void _EnterTree()
	{
		singleton = this;
	}
	public override void _PhysicsProcess(double delta)
	{
		foreach (SimpleArea3D simpleArea3D in SimpleArea3Ds) // Must have this
		{
			simpleArea3D.PhysicsUpdate();
		}
	}
}
