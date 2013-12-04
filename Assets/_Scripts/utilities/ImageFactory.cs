using UnityEngine;
using System.Collections;

public class ImageFactory : BaseBehaviour<ImageFactory>
{
	public Texture2D IconAttack;
	public Texture2D IconMove;

	public void Start()
	{
		Current = this;
	}
}
