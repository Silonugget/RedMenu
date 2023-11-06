using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;
using RedMenuShared;
using RedMenuClient.util;
using System.Net;
using RedMenuClient.data;

namespace RedMenuClient.menus
{
    class WorldMenu : BaseScript
    {
        private static Menu menu = new Menu("World Menu", "World related options");
        private static bool setupDone = false;

        private static void NetworkOverrideClockTime(int hour, int minute, int second, int transitionTime, bool freezeTime)
        {
            Function.Call((Hash)0x669E223E64B1903C, hour, minute, second, transitionTime, freezeTime);
        }

        private static void SetWeatherType(int weatherHash, bool p1, bool p2, bool p3, float p4, bool p5)
        {
            Function.Call((Hash)0x59174F1AFE095B5A, weatherHash, p1, p2, p3, p4, p5);
        }

        private static void SetSnowCoverageType(int type)
        {
            Function.Call((Hash)0xF02A9C330BBFC5C7, type);
        }

        public static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            if (PermissionsManager.IsAllowed(Permission.WOTimecycleModifiers))
            {
                Menu timecycleModifiersMenu = new Menu("Timecycle", "Set/Clear timecycle modifiers");
                MenuItem timeCycleModifiers = new MenuItem("Timecycle Modifiers", "Set/Clear timecycle modifiers.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
                menu.AddMenuItem(timeCycleModifiers);
                MenuController.AddSubmenu(menu, timecycleModifiersMenu);
                MenuController.BindMenuItem(menu, timecycleModifiersMenu, timeCycleModifiers);

                MenuListItem modifier = new MenuListItem("Modifier", data.WorldData.TimecycleModifiers, 0, "Set a timecycle modifier.");
                MenuItem clear = new MenuItem("Clear Timecycle Modifier", "Clear the timecycle modifier.");

                timecycleModifiersMenu.AddMenuItem(modifier);
                timecycleModifiersMenu.AddMenuItem(clear);

                timecycleModifiersMenu.OnListItemSelect += (menu, listItem, selectedIndex, itemIndex) =>
                {
                    if (listItem == modifier)
                    {
                        SetTimecycleModifier(modifier.GetCurrentSelection());
                    }
                };

                timecycleModifiersMenu.OnListIndexChange += (menu, listItem, oldSelectionIndex, newSelectionIndex, itemIndex) =>
                {
                    if (listItem == modifier)
                    {
                        SetTimecycleModifier(modifier.GetCurrentSelection());
                    }
                };

                timecycleModifiersMenu.OnItemSelect += (menu, item, index) =>
                {
                    if (item == clear)
                    {
                        ClearTimecycleModifier();
                    }
                };
            }

            if (PermissionsManager.IsAllowed(Permission.WOAnimpostfx))
            {
                Menu animpostfxMenu = new Menu("Animpostfx", "Play animated post-effects");
                MenuItem animpostfx = new MenuItem("Animpostfx", "Play animated post-effects.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
                menu.AddMenuItem(animpostfx);
                MenuController.AddSubmenu(menu, animpostfxMenu);
                MenuController.BindMenuItem(menu, animpostfxMenu, animpostfx);

                MenuListItem effect = new MenuListItem("Effect", data.WorldData.AnimpostfxEffects, 0, "Choose an effect.");
                MenuItem play = new MenuItem("Play", "Start playing the selected effect.");
                MenuItem stop = new MenuItem("Stop", "Stop playing the selected effect.");
                MenuItem stopAll = new MenuItem("Stop All", "Stop playing all effects.");

                animpostfxMenu.AddMenuItem(effect);
                animpostfxMenu.AddMenuItem(play);
                animpostfxMenu.AddMenuItem(stop);
                animpostfxMenu.AddMenuItem(stopAll);

                animpostfxMenu.OnItemSelect += (menu, item, index) =>
                {
                    if (item == play)
                    {
                        AnimpostfxPlay(effect.GetCurrentSelection());
                    }
                    else if (item == stop)
                    {
                        AnimpostfxStop(effect.GetCurrentSelection());
                    }
                    else if (item == stopAll)
                    {
                        AnimpostfxStopAll();
                    }
                };
            }
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}
