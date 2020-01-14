using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace RaidBot.UI.Forms.Bot
{
    public interface IColoredProgressBar
    {
        bool AllowDrop { get; set; }
        AnchorStyles Anchor { get; set; }
        Point AutoScrollOffset { get; set; }
        bool AutoSize { get; set; }
        Color BackColor { get; set; }
        Image BackgroundImage { get; set; }
        ImageLayout BackgroundImageLayout { get; set; }
        BindingContext BindingContext { get; set; }
        ContextMenu ContextMenu { get; set; }
        ContextMenuStrip ContextMenuStrip { get; set; }
        Cursor Cursor { get; set; }
        Rectangle DisplayRectangle { get; }
        DockStyle Dock { get; set; }
        bool Focused { get; }
        Font Font { get; set; }
        Color ForeColor { get; set; }
        LayoutEngine LayoutEngine { get; }
        Size MaximumSize { get; set; }
        Size MinimumSize { get; set; }
        RightToLeft RightToLeft { get; set; }
        bool RightToLeftLayout { get; set; }
        ISite Site { get; set; }
        string Text { get; set; }

        ObjRef CreateObjRef(Type requestedType);
        bool Equals(object obj);
        int GetHashCode();
        Size GetPreferredSize(Size proposedSize);
        object InitializeLifetimeService();
        bool PreProcessMessage(ref Message msg);
        void Refresh();
        void ResetBackColor();
        void ResetCursor();
        void ResetFont();
        void ResetForeColor();
        void ResetRightToLeft();
        void ResetText();
        string ToString();
    }
}