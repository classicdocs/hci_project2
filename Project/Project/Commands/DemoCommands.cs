using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands
{
    public static class DemoCommands
    {
        public static readonly RoutedUICommand AddNewResource = new RoutedUICommand(
            "Add new resource",
            "AddNewResource",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.F1,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand AddNewType = new RoutedUICommand(
            "Add new type",
            "AddNewType",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.F2,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand AddNewTag = new RoutedUICommand(
            "Add new tag",
            "AddNewTag",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.F3,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand ViewAllResources = new RoutedUICommand(
            "View all resources",
            "ViewAllResources",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.F4,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand ViewAllTypes = new RoutedUICommand(
            "View all types",
            "ViewAllTypes",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.F5,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand ViewAllTags = new RoutedUICommand(
            "View all tags",
            "ViewAllTags",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.F6,  ModifierKeys.Control ),
            }
            );
    }
}
