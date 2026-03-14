
using System.Windows;
using System.Windows.Input;
using global::Hercules.ViewModels;
using Hercules.Interface;
using Hercules.ViewModels;

namespace Hercules.Traits
{
    public static class DragTrait
    {
        public static readonly DependencyProperty DragTraitProperty =
            DependencyProperty.RegisterAttached(
                "IsDraggable", 
                typeof(bool), 
                typeof(DragTrait),
                new PropertyMetadata(false, OnIsDraggableChanged)
        );

        public static void SetIsDraggable(UIElement element, bool value) => element.SetValue(DragTraitProperty, value);
        public static bool GetIsDraggable(UIElement element) => (bool)element.GetValue(DragTraitProperty);

        private static bool _isDragging = false;
        private static Point _lastMousePosition;

        private static void OnIsDraggableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                // NewValue 是新设定的值
                if ((bool)e.NewValue)
                {
                    element.MouseDown += Element_MouseDown;
                    element.MouseMove += Element_MouseMove;
                    element.MouseUp += Element_MouseUp;
                }
                else
                {
                    element.MouseDown -= Element_MouseDown;
                    element.MouseMove -= Element_MouseMove;
                    element.MouseUp -= Element_MouseUp;
                }
            }
        }

        private static void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                var parentWindow = Window.GetWindow(element);
                if (parentWindow != null)
                {
                    _isDragging = true;
                    _lastMousePosition = e.GetPosition(parentWindow);
                    element.CaptureMouse();
                    e.Handled = true;
                }
            }
        }

        private static void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && sender is FrameworkElement element)
            {
                var parentWindow = Window.GetWindow(element);
                if (parentWindow == null) return;

                Point currentPosition = e.GetPosition(parentWindow);
                double deltaX = currentPosition.X - _lastMousePosition.X;
                double deltaY = currentPosition.Y - _lastMousePosition.Y;

                if (element.DataContext is IPositionAble vm)
                {
                    vm.x += deltaX;
                    vm.y += deltaY;
                }

                _lastMousePosition = currentPosition;
                e.Handled = true;
            }
        }

        private static void Element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging && sender is UIElement element)
            {
                _isDragging = false;
                element.ReleaseMouseCapture();
                e.Handled = true;
            }
        }
    }
}
