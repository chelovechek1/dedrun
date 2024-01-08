using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalSystems
{

	/// <summary>
	/// ��������, ��������������� �������������� ������ � ����� (������������� ����������, ���������� ����� � �. �.)
	/// </summary>
	public static class UserControllerGS
	{
		/// <summary>
		/// �������������� ���
		/// </summary>
		public static float HorizontalAxis => Input.GetAxisRaw("Horizontal");

		/// <summary>
		/// ������������ ���
		/// </summary>
		public static float VerticalAxis => Input.GetAxisRaw("Vertical");

		/// <summary>
		/// ������ �� ������ ������ �� ������� ������ (�������� ������ � <c>Update()</c>) (��� �������� �� ������� ��������� <see cref="IsJumpButtonHolded"/>)
		/// </summary>
		public static bool IsJumpButtonPressed => Input.GetKeyDown(KeyCode.Space);

		/// <summary>
		/// ������ �� ������ ������ �� ������� ������ (��� �������� ���������� ������� ��������� <see cref="IsJumpButtonPressed"/>)
		/// </summary>
		public static bool IsJumpButtonHolded => Input.GetKey(KeyCode.Space);
	}
}