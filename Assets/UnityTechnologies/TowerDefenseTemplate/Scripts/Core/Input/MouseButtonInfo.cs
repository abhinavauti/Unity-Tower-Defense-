using UnityEngine;
namespace Core.Input

{
	/// <summary>
	/// Info for mouse
	/// </summary>
	public class MouseButtonInfo : PointerActionInfo
	{
		/// <summary>
		/// Is this mouse button down
		/// </summary>
		public bool isDown;

		/// <summary>
		/// Our mouse button id
		/// </summary>
		public int mouseButtonId;
        /// <summary>
        /// Is this mouse button dragging
        /// </summary>
        public bool isDragging;

        /// <summary>
        /// The position where dragging started
        /// </summary>
        public Vector2 startDragPosition;
    }
}