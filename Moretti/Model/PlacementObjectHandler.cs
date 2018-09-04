using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ObjectOrientedDrawingOfObjects.Model
{
    class PlacementObjectHandler
    {
        private PlacementObjectHandler() { }
        private static PlacementObjectHandler instance = new PlacementObjectHandler();

        // Singleton implementation
        public static PlacementObjectHandler Instance { get {return instance;} }

        // Current focused PlacementObject... null if none focused
        public PlacementObject FocusedPlacementObject { get; set; }

        // Mousecoordinates in relation to the Canvas
        private Point mouseCoordinates = new Point(0,0);
        public Point MouseCoordinates { get {return mouseCoordinates;} set {mouseCoordinates = value;} }

        public bool IsCurrentPlacementObject(PlacementObject placementObject) {return FocusedPlacementObject == placementObject; }

        private ObservableCollection<UIElement> canvasElemts = new ObservableCollection<UIElement>();
        public ObservableCollection<UIElement> CanvasElements { get { return canvasElemts; } set { canvasElemts = value; } }

        public void AddPlacementObject(double size, ObjectTypes type, Color color)
        {
            var newObject = new PlacementObject(size, type, color) { InternalMouseCoordinates = new Point(size / 2, size / 2) };
            FocusedPlacementObject = newObject;
            newObject.SetNewCanvasCoordinates();
            CanvasElements.Add(newObject);
        }

        public void ResetCanvasElements()
        {
            while (CanvasElements.Count > 0)
            {
                CanvasElements.RemoveAt(0);
            }
        }

        public void MoveCanvasElementTo(PlacementObject placementObject, Positions pos)
        {
            switch (pos)
            {
                case Positions.Foreground:
                    if(CanvasElements.IndexOf(placementObject) != CanvasElements.Count - 1)
                    CanvasElements.Move(CanvasElements.IndexOf(placementObject), CanvasElements.Count - 1);
                    break;
                case Positions.Background:
                    if (CanvasElements.IndexOf(placementObject) != 0)
                        CanvasElements.Move(CanvasElements.IndexOf(placementObject), 0);
                    break;
                case Positions.OneLayerToBack:
                    if (CanvasElements.IndexOf(placementObject) > 0)
                        CanvasElements.Move(CanvasElements.IndexOf(placementObject), CanvasElements.IndexOf(placementObject) - 1);
                    break;
                case Positions.OneLayerToFront:
                    if (CanvasElements.IndexOf(placementObject) != CanvasElements.Count - 1)
                        CanvasElements.Move(CanvasElements.IndexOf(placementObject), CanvasElements.IndexOf(placementObject) + 1);
                    break;
                default:
                    break;
            }
        }
    }
}
