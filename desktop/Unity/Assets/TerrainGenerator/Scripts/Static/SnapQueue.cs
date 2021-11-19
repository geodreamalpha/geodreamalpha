using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TerrainGeneratorComponent
{
    public static class SnapQueue
    {
        static Queue<CharacterController> controllerQueue = new Queue<CharacterController>();
		const int snapsPerFrame = 100;

        public static void Add(CharacterController controller)
        {
            controllerQueue.Enqueue(controller);
        }

        public static void UpdateSnaps()
        {
			int index = 0;
			CharacterController controller;
			
			if (controllerQueue.Count > 0 && index < snapsPerFrame)
            {
				controller = controllerQueue.Peek();
				if (controller != null)
				{
					TrySnap(controller);

					if (controller.isGrounded)
						controllerQueue.Dequeue();
					else
						controllerQueue.Enqueue(controllerQueue.Dequeue());
				}
                else
                {
					controllerQueue.Enqueue(controllerQueue.Dequeue());
				}
				index++;
			}
        }

		/// <summary>
		/// snaps character (such as player or enemy) to the height (y) of the terrain depending on their current (x) and (z) position so that they are properly grounded.
		/// </summary>
		/// <param name="controller">the character controller component of the character</param>
		static void TrySnap(CharacterController controller)
		{
			//snaps character (such as player or enemy) to the height (y) of the terrain depending on their current (x) and (z) position so that they are properly grounded.
			RaycastHit hit;
			controller.Move(Vector3.up * 1300);
			Vector3 newPosition = controller.transform.position;

			Physics.Raycast(newPosition, Vector3.down, out hit, float.PositiveInfinity, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore);
			newPosition.y = hit.point.y;
			controller.Move(-controller.transform.position + newPosition);
		}//
	}
}