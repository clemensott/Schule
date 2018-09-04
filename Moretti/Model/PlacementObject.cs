using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObjectOrientedDrawingOfObjects.Model
{
    class PlacementObject : Shape
    {
        private Point internalMouseCoordinates = new Point(0, 0);
        public Point InternalMouseCoordinates { get { return internalMouseCoordinates; } set { internalMouseCoordinates = value; } }  // Mousecoordinates in relation to this

        bool firstCreated = true;                                               // true if object is first created to not remove it in the mouseUp event

        private ObjectTypes type = ObjectTypes.Rechteck;
        public ObjectTypes Type { get { return type; } set { type = value; } }           // Property for Type of Object (Kreis, Dreieck, Rechteck)

        private Geometry geometry = new RectangleGeometry();

        protected override Geometry DefiningGeometry { get { return geometry; } }               // Geometry of Object

        Stopwatch stopwatch = new Stopwatch();                                  // to evaluate short klick

        // is needed for displaying purposes
        #region DependencyPropertyForSize
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(Double), typeof(PlacementObject));

        public double Size
        {
            get { return (double)this.GetValue(SizeProperty); }
            set { this.SetValue(SizeProperty, value); }
        }
        #endregion

        public PlacementObject(double a, ObjectTypes objectType, Color color)
        {
            Type = objectType;

            MouseDown += PlacementObject_MouseDown;
            MouseMove += PlacementObject_MouseMove;
            MouseUp += PlacementObject_MouseUp;

            Width = a;
            Height = a;

            Fill = new SolidColorBrush(color);

            geometry = Parser.GetGeometryFromObjectType(objectType, a);

            #region ContextMenu
            ContextMenu = new ContextMenu();
            MenuItem x = new MenuItem() { Header = "Delete Item" };
            x.Click += (s, e) => PlacementObjectHandler.Instance.CanvasElements.Remove(this);
            ContextMenu.Items.Add(x);
            x = new MenuItem() { Header = "Bring to foreground" };
            x.Click += (s, e) => PlacementObjectHandler.Instance.MoveCanvasElementTo(this, Positions.Foreground);
            ContextMenu.Items.Add(x);
            x = new MenuItem() { Header = "Bring to background" };
            x.Click += (s, e) => PlacementObjectHandler.Instance.MoveCanvasElementTo(this, Positions.Background);
            ContextMenu.Items.Add(x);
            x = new MenuItem() { Header = "Bring one layer to front" };
            x.Click += (s, e) => PlacementObjectHandler.Instance.MoveCanvasElementTo(this, Positions.OneLayerToFront);
            ContextMenu.Items.Add(x);
            x = new MenuItem() { Header = "Bring one layer to back" };
            x.Click += (s, e) => PlacementObjectHandler.Instance.MoveCanvasElementTo(this, Positions.OneLayerToBack);
            ContextMenu.Items.Add(x);
            #endregion
        }

        private void PlacementObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds < 200 && !firstCreated && PlacementObjectHandler.Instance.IsCurrentPlacementObject(this))
                {
                    geometry = null;
                    PlacementObjectHandler.Instance.CanvasElements.Remove(this);
                }
                PlacementObjectHandler.Instance.FocusedPlacementObject = null;
            }
            firstCreated = false;
        }

        private void PlacementObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed && PlacementObjectHandler.Instance.FocusedPlacementObject != null) 
                PlacementObjectHandler.Instance.FocusedPlacementObject.SetNewCanvasCoordinates();
        }

        private void PlacementObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InternalMouseCoordinates = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                PlacementObjectHandler.Instance.FocusedPlacementObject = this;
                stopwatch.Reset();
                stopwatch.Start();
            }
        }

        public void SetNewCanvasCoordinates()
        {
            Canvas.SetLeft(this, PlacementObjectHandler.Instance.MouseCoordinates.X - InternalMouseCoordinates.X);
            Canvas.SetTop(this, PlacementObjectHandler.Instance.MouseCoordinates.Y - InternalMouseCoordinates.Y);
        }
    }
}
