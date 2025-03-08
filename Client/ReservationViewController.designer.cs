// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Client
{
	[Register ("ReservationViewController")]
	partial class ReservationViewController
	{
		[Outlet]
		AppKit.NSTextField DepartureTimeTextField { get; set; }

		[Outlet]
		AppKit.NSTextField LandmarkTextField { get; set; }

		[Outlet]
		AppKit.NSTextField NameTextField { get; set; }

		[Outlet]
		AppKit.NSTextField PhoneTextField { get; set; }

		[Outlet]
		AppKit.NSTextField TicketsNoTextField { get; set; }

		[Outlet]
		AppKit.NSTextField TotalTextField { get; set; }

		[Outlet]
		AppKit.NSTextField TransporCompanyTextField { get; set; }

		[Action ("ModifiedTicketsNo:")]
		partial void ModifiedTicketsNo (AppKit.NSTextFieldCell sender);

		[Action ("ReserveClicked:")]
		partial void ReserveClicked (AppKit.NSButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (DepartureTimeTextField != null) {
				DepartureTimeTextField.Dispose ();
				DepartureTimeTextField = null;
			}

			if (LandmarkTextField != null) {
				LandmarkTextField.Dispose ();
				LandmarkTextField = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (PhoneTextField != null) {
				PhoneTextField.Dispose ();
				PhoneTextField = null;
			}

			if (TicketsNoTextField != null) {
				TicketsNoTextField.Dispose ();
				TicketsNoTextField = null;
			}

			if (TotalTextField != null) {
				TotalTextField.Dispose ();
				TotalTextField = null;
			}

			if (TransporCompanyTextField != null) {
				TransporCompanyTextField.Dispose ();
				TransporCompanyTextField = null;
			}

		}
	}
}
