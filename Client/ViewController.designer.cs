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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField landmarkTextField { get; set; }

		[Outlet]
		AppKit.NSTextField lowerLimitTextField { get; set; }

		[Outlet]
		AppKit.NSTableView tripTable { get; set; }

		[Outlet]
		AppKit.NSTextField upperLimitTextField { get; set; }

		[Action ("filterBtn:")]
		partial void filterBtn (AppKit.NSButton sender);

		[Action ("logOutBtn:")]
		partial void logOutBtn (AppKit.NSButton sender);

		[Action ("RowClicked:")]
		partial void RowClicked (AppKit.NSTableView sender);

		void ReleaseDesignerOutlets ()
		{
			if (landmarkTextField != null) {
				landmarkTextField.Dispose ();
				landmarkTextField = null;
			}

			if (lowerLimitTextField != null) {
				lowerLimitTextField.Dispose ();
				lowerLimitTextField = null;
			}

			if (tripTable != null) {
				tripTable.Dispose ();
				tripTable = null;
			}

			if (upperLimitTextField != null) {
				upperLimitTextField.Dispose ();
				upperLimitTextField = null;
			}

		}
	}
}
