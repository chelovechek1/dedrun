using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory
{
	public IHasInventory owner;
	public abstract void Clear();
}
/// <summary>
/// Список хранящихся предметов
/// </summary>
public class ListInventory : Inventory
{
	public List<Item> items;
	public override void Clear() => items.Clear();
}

/// <summary>
/// Сетка хранящихся предметов
/// </summary>
public class GridInventory : Inventory
{
	public List<ItemInvertoryPlacementInfo> items;

	public override void Clear() => items.Clear();
}
public struct ItemInvertoryPlacementInfo
{
	public ItemInvertoryPlacementInfo(Item item, Vector2Int pos, bool rotated) =>
		(this.item, position, this.rotated) = (item, pos, rotated);

	public Item item;
	public Vector2Int position;
	public bool rotated;
}

/// <summary>
/// Интерфейс, наличие которого говорит о наличии инвентаря у сущности
/// </summary>
public interface IHasInventory
{
	public Inventory inventory { get; set; }
	public T Get<T>() where T : Inventory => (T)inventory; // Для лёгкого преобразования к PlayerInvertory и подобным
	public bool TryGet<T>(out T inv) where T : Inventory
	{
		inv = inventory as T;
		return inv != null;
	}
}

public static class IHasInvertoryExtension
{
	public static Inventory SetInvertory<PARENT, INV>(this PARENT invertoryOwner, INV invertoryField, INV newInvertory) 
		where INV : Inventory
		where PARENT : IHasInventory
	{
		if (invertoryField != null)
			invertoryField.owner = null;
		newInvertory.owner = invertoryOwner;
		return newInvertory;
	}
}