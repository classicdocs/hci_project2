using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand AddNewResource = new RoutedUICommand(
            "Add new resource",
            "AddNewResource",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.R, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand AddNewType = new RoutedUICommand(
            "Add new type",
            "AddNewType",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.T, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand AddNewTag = new RoutedUICommand(
            "Add new tag",
            "AddNewTag",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.G, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand ViewAllResources = new RoutedUICommand(
            "View all resources",
            "ViewAllResources",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.R, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand ViewAllTypes = new RoutedUICommand(
            "View all types",
            "ViewAllTypes",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.T, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand ViewAllTags = new RoutedUICommand(
            "View all tags",
            "ViewAllTags",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.G, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand Demo = new RoutedUICommand(
            "Demo",
            "Demo",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.D, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand Map1 = new RoutedUICommand(
            "Map1",
            "Map1",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.D1, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand Map2 = new RoutedUICommand(
            "Map2",
            "Map2",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.D2, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand Map3 = new RoutedUICommand(
            "Map3",
            "Map3",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.D3, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand Map4 = new RoutedUICommand(
            "Map4",
            "Map4",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.D4, ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand Search = new RoutedUICommand(
            "Search",
            "Search",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.S, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand Filter = new RoutedUICommand(
            "Filter",
            "Filter",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.F, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand ClearSearch = new RoutedUICommand(
            "ClearSearch",
            "ClearSearch",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.H, ModifierKeys.Control),
            }
            );

        public static readonly RoutedUICommand Zoom = new RoutedUICommand(
            "Zoom",
            "Zoom",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.Scroll, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand Documentation = new RoutedUICommand(
            "Documentation",
            "Documentation",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.Home, ModifierKeys.Control),
            }
            );

    }
}
