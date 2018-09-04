using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ObjectOrientedDrawingOfObjects.Model;

namespace ObjectOrientedDrawingOfObjects.ViewModel
{
    /// <summary>
    /// The ViewModel of the MainWindow
    /// </summary>
    class MainViewModel : BaseViewModel
    {
        // CanvasElements wrapper
        public ObservableCollection<UIElement> CanvasElements { get { return PlacementObjectHandler.Instance.CanvasElements; } }

        // ListBox element List
        private ObservableCollection<ObjectTypes> objectList = new ObservableCollection<ObjectTypes>();
        public ObservableCollection<ObjectTypes> ObjectList { get { return objectList; } set { objectList = value; } }

        // Selected Color of color Picker
        private Color selectedColor = Colors.Black;
        public Color SelectedColor { get { return selectedColor; } set { selectedColor = value; } }

        // Value of slider
        private double sliderValue = 10;
        public double SliderValue { get { return sliderValue; } set { sliderValue = value; } }

        public ObjectTypes SelectedComboboxItem { get; set; }

        public void AddElement(object sender, MouseButtonEventArgs e)
        {
            bool isOverChildren = false;

            foreach (var item in CanvasElements) if (item.IsMouseOver) { isOverChildren = true; break; };

            if (!isOverChildren)
            {
                PlacementObjectHandler.Instance.AddPlacementObject(SliderValue, SelectedComboboxItem, SelectedColor);
            }
        }

        public void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            PlacementObjectHandler.Instance.MouseCoordinates = new Point(e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y);
            if(PlacementObjectHandler.Instance.FocusedPlacementObject != null)
                PlacementObjectHandler.Instance.FocusedPlacementObject.SetNewCanvasCoordinates();
        }

        public void ClearCanvasElements()
        {
            PlacementObjectHandler.Instance.ResetCanvasElements();
        }

        public MainViewModel()
        {
            foreach (var type in Enum.GetValues(typeof(ObjectTypes))) ObjectList.Add((ObjectTypes)type);

            SelectedComboboxItem = ObjectList[0];
        }
    }
}
