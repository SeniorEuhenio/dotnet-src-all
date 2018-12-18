//----------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace System.Activities.Presentation
{
    using System.Windows.Media;

    internal static class WorkflowDesignerColorsInternal
    {
        public const string WorkflowViewElementSelectedCaptionColorKey = "WorkflowViewElementSelectedCaptionBrushColorKey";
        public const string PropertyToolBarHightlightedButtonForegroundColorKey = "PropertyToolBarHightlightedButtonForegroundColor";
        public const string FlowchartConnectorColorKey = "FlowchartConnectorColorKey";
        public const string ActivityDesignerSelectedTitleForegroundColorKey = "ActivityDesignerSelectedTitleForegroundColorKey";

        public static Color WorkflowViewElementSelectedCaptionColor
        {
            get
            {
                return WorkflowDesignerColors.WorkflowViewElementSelectedCaptionColor;
            }
        }

        public static Color PropertyToolBarHightlightedButtonForegroundColor
        {
            get
            {
                return WorkflowDesignerColors.PropertyToolBarHightlightedButtonForegroundColor;
            }
        }

        public static Color FlowchartConnectorColor
        {
            get
            {
                return WorkflowDesignerColors.FlowchartConnectorColor;
            }
        }

        public static Color ContextMenuItemTextHoverQuirkedColor
        {
            get
            {
                return WorkflowDesignerColors.ContextMenuItemTextHoverQuirkedColor;
            }
        }

        public static Color ActivityDesignerSelectedTitleForegroundColor
        {
            get
            {
                return WorkflowDesignerColors.ActivityDesignerSelectedTitleForegroundColor;
            }
        }
    }
}
