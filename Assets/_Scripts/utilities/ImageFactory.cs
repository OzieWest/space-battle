using UnityEngine;
using System.Collections;

public class ImageFactory : BaseBehaviour<ImageFactory>
{
	public Texture2D SmallShipPicture;
	public Texture2D MediumShipPicture;
	public Texture2D BigShipPicture;

	public void Start()
	{
		Current = this;
	}
}
