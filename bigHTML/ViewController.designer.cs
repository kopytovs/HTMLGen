// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace bigHTML
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSProgressIndicator magicBar { get; set; }

		[Outlet]
		AppKit.NSTextField myLabel { get; set; }

		[Action ("doSmthMagic:")]
		partial void doSmthMagic (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (myLabel != null) {
				myLabel.Dispose ();
				myLabel = null;
			}

			if (magicBar != null) {
				magicBar.Dispose ();
				magicBar = null;
			}
		}
	}
}
