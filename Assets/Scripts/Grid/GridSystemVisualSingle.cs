using System.Collections;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
	[SerializeField] private MeshRenderer mesh;
	public void Hide()
	{
		mesh.enabled = false;
	}

	public void Show(Material material)
	{
		mesh.enabled = true;
		mesh.material = material;
	}
}
