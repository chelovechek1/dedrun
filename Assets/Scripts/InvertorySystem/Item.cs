using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
	public virtual Vector2Int Size => new(1, 1);
}
