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

namespace RedMenuClient.menus
{
    class MainMenu
    {
        private static Menu mainMenu = new Menu("RedMenu", "Welcome to RH FreeRoam");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            mainMenu.MenuTitle = GetPlayerName(PlayerId());

            MenuController.AddMenu(mainMenu);

            // Player Menu
            if (PermissionsManager.IsAllowed(Permission.PMMenu))
            {
                MenuController.AddSubmenu(mainMenu, PlayerMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Player Menu", "All kinds of player related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, PlayerMenu.GetMenu(), submenuBtn);
            }

            // Weapons Menu
            if (PermissionsManager.IsAllowed(Permission.WMMenu))
            {
                MenuController.AddSubmenu(mainMenu, WeaponsMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Weapons Menu", "Weapon and ammo related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, WeaponsMenu.GetMenu(), submenuBtn);
            }

                        if (PermissionsManager.IsAllowed(Permission.VMMenu))
            {
                MenuController.AddSubmenu(mainMenu, VehicleMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Vehicle Menu", "Vehicle related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, VehicleMenu.GetMenu(), submenuBtn);
            }


            if (PermissionsManager.IsAllowed(Permission.MMMenu))
            {
                MenuController.AddSubmenu(mainMenu, MountMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Mount Menu", "Mount related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, MountMenu.GetMenu(), submenuBtn);
            }


            // Teleport Menu
            if (PermissionsManager.IsAllowed(Permission.TMMenu))
            {
                MenuController.AddSubmenu(mainMenu, TeleportMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Teleport Menu", "Teleport options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, TeleportMenu.GetMenu(), submenuBtn);
            }

            // World Options Menu
            if (PermissionsManager.IsAllowed(Permission.WOMenu))
            {
                MenuController.AddSubmenu(mainMenu, WorldMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("World Menu", "World related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, WorldMenu.GetMenu(), submenuBtn);
            }

// Battlepass
if (PermissionsManager.IsAllowed(Permission.VOMenu)) // Adjust the permission as needed
{
    MenuItem battlepassBtn = new MenuItem("Battlepass", "Access the Battlepass.");
    mainMenu.AddMenuItem(battlepassBtn);

    // Event handler for button press
    mainMenu.OnItemSelect += (sender, item, index) =>
    {
        if (item == battlepassBtn)
        {
            ExecuteCommand("battlepass");
            mainMenu.CloseMenu();
        }
    };
}


            // Server Info
            MenuController.AddSubmenu(mainMenu, ServerInfoMenu.GetMenu());
            MenuItem serverBtn = new MenuItem("Server Info", "Information about this server.")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(serverBtn);
            MenuController.BindMenuItem(mainMenu, ServerInfoMenu.GetMenu(), serverBtn);

        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }
    }
}
