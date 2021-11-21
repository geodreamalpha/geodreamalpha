using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TerrainGeneratorComponent
{
	public static class SnapList
	{
		static List<CharacterController> controllers = new List<CharacterController>();

		public static void Add(CharacterController controller)
		{
			controllers.Add(controller);
		}

		public static void UpdateSnaps()
		{
			for (int i = 0; i < controllers.Count; i++)
				if (controllers[i] == null || controllers[i].isGrounded)
				{
					controllers.RemoveAt(i);
					i--;
				}
				else
					TrySnap(controllers[i]);
		}

		static void TrySnap(CharacterController controller)
		{
			RaycastHit[] hits;
			hits = Physics.RaycastAll(controller.transform.position + Vector3.up * 1000, Vector3.down, float.PositiveInfinity,
				LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore);

			foreach (RaycastHit hit in hits)
				if (hit.collider.name == "Terrain")
					controller.Move(new Vector3(0, -controller.transform.position.y + hit.point.y, 0));				
		}//
	}
}
