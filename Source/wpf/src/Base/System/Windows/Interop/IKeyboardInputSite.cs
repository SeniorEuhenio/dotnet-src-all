using System;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;

namespace System.Windows.Interop
{
    /// <summary>
    ///     Containers provide a unique IKeyboardInputSite instance for each
    ///     component they contain.
    /// </summary> 
    public interface IKeyboardInputSite
    {
        /// <summary>
        ///     Unregisters a child KeyboardInputSink from this sink.
        /// </summary>
        /// <remarks>
        ///     This API requires unrestricted UI Window permission.
        /// </remarks>
        /// <SecurityNote>
        ///     Critical: This API can be used for input spoofing
        ///     PublicOK: This method has a demand on it.
        /// </SecurityNote>
        [SecurityCritical, UIPermissionAttribute(SecurityAction.LinkDemand, Unrestricted=true)]
        void Unregister();

        /// <summary>
        ///     Returns the sink associated with this site (the "child", not
        ///     the "parent" sink that owns the site).  There's no way of
        ///     getting from the site to the parent sink.
        /// </summary> 
        IKeyboardInputSink Sink {get;}

        /// <summary>
        ///     Components call this when they want to move focus ("tab") but
        ///     have nowhere further to tab within their own component.  Return
        ///     value is true if the site moved focus, false if the calling
        ///     component still has focus and should wrap around.
        /// </summary> 
        bool OnNoMoreTabStops(TraversalRequest request);

    }
}

