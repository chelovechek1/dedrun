using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasInvertory
{
	protected Invertory _invertory { get; set; }
	public Invertory Invertory
	{
		get => _invertory;
		set {
			if (_invertory != null)
				_invertory.Owner = null;
			_invertory = value;
			_invertory.Owner = this;
		}
	}
}
