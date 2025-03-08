using System;

using AppKit;
using Foundation;
using Model;
using Services;

namespace Client
{
    public partial class LoginViewController : NSViewController
    {

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }
        
        partial void LoginClicked(AppKit.NSButton sender)
        {
            if (usernameTextField.StringValue == "" ||
                PasswordTextField.StringValue == "")
                return;

            IService service = SingletonProxy.GetInstance();
            
            var storyboard = NSStoryboard.MainStoryboard;
            var controller = storyboard.InstantiateControllerWithIdentifier("tripsController") as NSWindowController;
            var viewController = (ViewController) controller.ContentViewController;
            viewController.User = new User(1L, usernameTextField.StringValue, PasswordTextField.StringValue);
            viewController.View.Window.Title = $"User: {usernameTextField.StringValue}";
            
            if (service.Login(viewController.User,  viewController))
            {
                viewController.FillTable();
                controller.ShowWindow(this);
                View.Window.Close();
            }
        }
    }
}
