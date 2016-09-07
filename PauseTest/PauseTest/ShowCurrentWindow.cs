//------------------------------------------------------------------------------
// <copyright file="ShowCurrentWindow.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using EnvDTE80;
using System.Runtime.InteropServices;

namespace PauseTest
{

    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ShowCurrentWindow
    {
        private DTE2 dte;
        private Window nowActiveWindow;
        private Window lastActiveWindow;
        private DateTime startTime;
        private DateTime windowOpened;
        private TimeSpan timeHelper;
        private List<WindowData> data;
        private int listIndex;

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("f40da8c4-b65d-457a-9611-5f3f073626f2");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowCurrentWindow"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ShowCurrentWindow(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }

            dte = Marshal.GetActiveObject("VisualStudio.DTE.14.0") as DTE2;
            dte.Events.WindowEvents.WindowActivated += testlistener;

            data = new List<WindowData>();
            startTime = windowOpened = DateTime.Now;

        }

        private void testlistener(Window GotFocus, Window LostFocus)
        {
            nowActiveWindow = GotFocus;
            lastActiveWindow = LostFocus;

            timeHelper = DateTime.Now - windowOpened;
            windowOpened = DateTime.Now;

            listIndex = -1;

            //FindIndex returns -1 if no Match was found. This way checking for existence and looking up the index of an Element can be done in one Step.
            //Searching Lists is a linear Operation, so this will slow down if hundrets of different Windows are opened in one session. 
            if ((listIndex = data.FindIndex( (WindowData d) => { return d.window.Equals(lastActiveWindow); } )) < 0)
                data[listIndex].add(timeHelper);
            else
                data.Add(new WindowData(lastActiveWindow, timeHelper));

        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ShowCurrentWindow Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ShowCurrentWindow(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {

            // Get the instance number 0 of our tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.package.FindToolWindow(typeof(Frontpannel), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

/*       //Windows erlaubt den Zugriff auf die Passenden Events nicht...
         public class WindowsSaysIshouldnotListen : _dispWindowEvents_WindowActivatedEventHandler
         {

         }
 */
        private void testmethod()
        {

        }
    }
}
