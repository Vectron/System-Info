namespace System.Windows.Forms
{
	using System.Drawing;

	public static class WinFormsExtension
	{
		public static Rectangle Coordinates(this Control control)
		{
			// Extend System.Windows.Forms.Control to have a Coordinates property.
			// The Coordinates property contains the control's form-relative location.
			Rectangle coordinates;
			Form form = (Form)control.TopLevelControl;

			if (control == form)
			{
				coordinates = form.ClientRectangle;
			}
			else
			{
				coordinates = form.RectangleToClient(control.Parent.RectangleToScreen(control.Bounds));
			}

			return coordinates;
		}
	}
}