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
	[Register ("FilteredTripsViewController")]
	partial class FilteredTripsViewController
	{
		[Outlet]
		AppKit.NSTableView FilteredTripsTable { get; set; }

		[Action ("RowClicked:")]
		partial void RowClicked (AppKit.NSTableView sender);

		void ReleaseDesignerOutlets ()
		{
			if (FilteredTripsTable != null) {
				FilteredTripsTable.Dispose ();
				FilteredTripsTable = null;
			}

		}
	}
}
