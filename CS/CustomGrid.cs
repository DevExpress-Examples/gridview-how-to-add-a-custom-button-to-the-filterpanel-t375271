using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Skins;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Handler;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace dxExample
{
    [ToolboxItem(true)]
    public class CustomGrid : GridControl
    {
        protected override BaseView CreateDefaultView()
        {
            return CreateView("CustomGridView");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridViewInfoRegistrator());
        }
    }

    public class CustomGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName {   get{ return "CustomGridView"; }}
        CustomGridSkinPaintStyle ps;
        public override ViewPaintStyle PaintStyleByLookAndFeel(DevExpress.LookAndFeel.UserLookAndFeel lookAndFeel, string name)
        {
            if (ps == null)
            {
                ps = new CustomGridSkinPaintStyle();
                PaintStyles.Add(ps);
            }
            return ps;
        }
        public override BaseView CreateView(GridControl grid)
        {
            return new CustomGridView(grid);
        }

        public override BaseViewHandler CreateHandler(BaseView view)
        {
            return new CustomGridViewHandler(view as CustomGridView);
        }
    }
    public class CustomGridSkinPaintStyle : GridSkinPaintStyle
    {
        public CustomGridSkinPaintStyle()  { }
        public override DevExpress.XtraGrid.Views.Grid.Drawing.GridElementsPainter CreateElementsPainter(BaseView view)
        {
            return new CustomGridSkinElementsPainter(view);
        }
    }

    public class CustomGridSkinElementsPainter : GridSkinElementsPainter
    {
        public CustomGridSkinElementsPainter(BaseView view)  : base(view)   { }
        protected override ObjectPainter CreateFilterPanelPainter()
        {
            GridFilterPanelPainter painter = new CustomGridFilterPanelPainter(View);
            return painter;
        }
    }
    public class CustomGridFilterPanelPainter : SkinGridFilterPanelPainter
    {
        public CustomGridFilterPanelPainter(GridView provider): base(provider)
        {
            View = provider as CustomGridView;
            customButtonPainter = new SkinEditorButtonPainter(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel);
        }
        protected override Point CalcButtonLocation(Rectangle client, Size size, bool isRight)
        {
            Point point = base.CalcButtonLocation(client, size, isRight);
            if (point.X < 10)
                point.X += View.UpdateButtonsRects(); ; // set the indent
            return point;
        }

        public override void DrawObject(ObjectInfoArgs e)
        {
            base.DrawObject(e);
            DrawCustomButtons(e.Cache);
        }

        public CustomGridView View { get; set; }
        SkinEditorButtonPainter customButtonPainter;
        private void DrawCustomButtons(GraphicsCache cache)
        {
            foreach (EditorButton button in View.ButtonsStrore.Keys)
                customButtonPainter.DrawObject(GetButtonInfoArgs(cache, View.ButtonsStrore[button],button ));
        }

        internal EditorButtonObjectInfoArgs GetButtonInfoArgs(GraphicsCache cache, EditorButtonObjectInfoArgs args, EditorButton button)
        {
            args.Cache = cache;
            ObjectState state = ObjectState.Normal;
            if (button.Tag is ObjectState)
                state = (ObjectState)button.Tag;
            args.State = state;
            return args;
        }
    }

    public class CustomGridView : GridView
    {
        public CustomGridView()
        {
            ButtonsStrore = new Dictionary<EditorButton, EditorButtonObjectInfoArgs>();
        }

        public CustomGridView(GridControl grid): base(grid) { }
        protected override string ViewName{ get{return "CustomGridView"; } }
        internal Dictionary<EditorButton, EditorButtonObjectInfoArgs> ButtonsStrore;

        public void AddButton(EditorButton b)
        {
            ButtonsStrore.Add(b, new EditorButtonObjectInfoArgs(b, new DevExpress.Utils.AppearanceObject()));
        }

        internal int  UpdateButtonsRects()
        {
            int offset = 10;
            foreach (EditorButton button in ButtonsStrore.Keys)
            {
                int y = ViewInfo.ClientBounds.Y + (ViewInfo.ClientBounds.Height - (ViewInfo.FilterPanel.Bounds.Height / 8 * 7));
                int x = offset;
                offset += 5 + button.Width;
                Rectangle buttonRect = new Rectangle(x, y, button.Width, ViewInfo.FilterPanel.Bounds.Height / 8 *6);
                ButtonsStrore[button].Bounds = buttonRect;
            }
            return offset;
        }

        public event EventHandler CustomButtonClick;
        #region OnCustomButtonClick
        /// <summary>
        /// Triggers the CustomButtonClick event.
        /// </summary>
        public virtual void OnCustomButtonClick(EditorButton b, EventArgs ea)
        {
            EventHandler handler = CustomButtonClick;
            if (handler != null)
                handler(b, ea);
        }
        #endregion

        bool shouldReset = false;
        internal void UpdateButtonState(ObjectState state, Point point)
        {
            GridHitInfo hitInfo = CalcHitInfo(point);
            if (hitInfo.HitTest != GridHitTest.FilterPanel)
            {
                if (shouldReset)
                {
                    shouldReset = false;
                    if(ResetButtonState())
                        InvalidateFilterPanel(); 
                }
                return;
            }
            UpdateButtonStateCore(state, point);
            InvalidateFilterPanel();
            shouldReset = true;
        }


        private bool ResetButtonState()
        {
            bool res = false;
            foreach (EditorButton button in ButtonsStrore.Keys)
                if (button.Tag != null && (ObjectState)button.Tag != ObjectState.Normal)
                {
                    button.Tag = ObjectState.Normal;
                    res = true;
                }
            return res;
        }

        private void UpdateButtonStateCore(ObjectState state, Point point)
        {
            foreach (EditorButton button in ButtonsStrore.Keys)
            {
                button.Tag = ObjectState.Normal;
                if (ButtonsStrore[button].Bounds.Contains(point))
                {
                    button.Tag = state;
                    if (state == ObjectState.Pressed)
                        OnCustomButtonClick(button, EventArgs.Empty);
                }
            }
        }
    }

    public class CustomGridViewHandler : GridHandler
    {
        public CustomGridViewHandler(GridView view) : base(view) { }
        public CustomGridView View { get { return base.View as CustomGridView; } }
        protected override bool OnMouseDown(MouseEventArgs ev)
        {
            View.UpdateButtonState(ObjectState.Pressed,ev.Location);
            return base.OnMouseDown(ev);
        }
        protected override bool OnMouseMove(MouseEventArgs ev)
        {
            View.UpdateButtonState(ObjectState.Hot,ev.Location);
            return base.OnMouseMove(ev);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            View.UpdateButtonState(ObjectState.Normal, Point.Empty);
            base.OnMouseLeave(e);
        }
        protected override bool OnMouseUp(MouseEventArgs ev)
        {
            View.UpdateButtonState(ObjectState.Normal,ev.Location);
            return base.OnMouseUp(ev);
        }
    }
}
