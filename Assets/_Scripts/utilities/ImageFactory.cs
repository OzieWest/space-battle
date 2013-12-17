using UnityEngine;
using System.Collections;

public class ImageFactory : BaseBehaviour<ImageFactory>
{
	public Texture2D SmallShipPicture;
	public Texture2D MediumShipPicture;
	public Texture2D BigShipPicture;

	public Sprite IconDefault;
	public Sprite IconMove;
	public Sprite IconAttack;

	public void Start()
	{
		Current = this;
	}

	public Texture2D GetImageByType(eShipType type)
	{
		switch (type)
		{
			case eShipType.Small:
				return SmallShipPicture;
			case eShipType.Medium:
				return MediumShipPicture;
			case eShipType.Big:
				return BigShipPicture;
		}
		return null;
	}
}
