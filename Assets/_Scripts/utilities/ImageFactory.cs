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

	public Texture2D GetImageByType(ShipType type)
	{
		switch (type)
		{
			case ShipType.Small:
				return SmallShipPicture;
			case ShipType.Medium:
				return MediumShipPicture;
			case ShipType.Big:
				return BigShipPicture;
		}
		return null;
	}
}
