using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalSystems
{

	/// <summary>
	/// Синглтон, предоставляющий взаимодействия игрока с игрой (внутриигровое управление, управление игрой и т. д.)
	/// </summary>
	public static class UserControllerGS
	{
		/// <summary>
		/// Горизонтальная ось
		/// </summary>
		public static float HorizontalAxis => Input.GetAxisRaw("Horizontal");

		/// <summary>
		/// Вертикальная ось
		/// </summary>
		public static float VerticalAxis => Input.GetAxisRaw("Vertical");

		/// <summary>
		/// Нажата ли кнопка прыжка на текущем фрейме (работает только в <c>Update()</c>) (для проверки на зажатие используй <see cref="IsJumpButtonHolded"/>)
		/// </summary>
		public static bool IsJumpButtonPressed => Input.GetKeyDown(KeyCode.Space);

		/// <summary>
		/// Зажата ли кнопка прыжка на текущем фрейме (для проверки единичного нажатия используй <see cref="IsJumpButtonPressed"/>)
		/// </summary>
		public static bool IsJumpButtonHolded => Input.GetKey(KeyCode.Space);
	}
}