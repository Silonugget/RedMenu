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
        private static Menu mainMenu = new Menu("RedMenu", "Welcome to RedMenu!");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            mainMenu.MenuTitle = GetPlayerName(PlayerId());

            MenuController.AddMenu(mainMenu);

            // Online Players Menu

                MenuController.AddSubmenu(mainMenu, OnlinePlayersMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Online Players", "List of players in the server.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, OnlinePlayersMenu.GetMenu(), submenuBtn);
            

            // Player Menu

                MenuController.AddSubmenu(mainMenu, PlayerMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Player Menu", "All kinds of player related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, PlayerMenu.GetMenu(), submenuBtn);
            

            // Weapons Menu

                MenuController.AddSubmenu(mainMenu, WeaponsMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Weapons Menu", "Weapon and ammo related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, WeaponsMenu.GetMenu(), submenuBtn);
            


                MenuController.AddSubmenu(mainMenu, MountMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Mount Menu", "Mount related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, MountMenu.GetMenu(), submenuBtn);
            


                MenuController.AddSubmenu(mainMenu, VehicleMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Vehicle Menu", "Vehicle related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, VehicleMenu.GetMenu(), submenuBtn);
            

            // Teleport Menu

                MenuController.AddSubmenu(mainMenu, TeleportMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Teleport Menu", "Teleport options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, TeleportMenu.GetMenu(), submenuBtn);
            

            // World Options Menu

                MenuController.AddSubmenu(mainMenu, WorldMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("World Menu", "World related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, WorldMenu.GetMenu(), submenuBtn);
            


                MenuController.AddSubmenu(mainMenu, VoiceMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Voice Menu", "Voice related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, VoiceMenu.GetMenu(), submenuBtn);
            

            // Misc settings
            MenuController.AddSubmenu(mainMenu, MiscSettingsMenu.GetMenu());
            MenuItem miscBtn = new MenuItem("Misc Settings", "Miscellaneous settings and menu options.")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(miscBtn);
            MenuController.BindMenuItem(mainMenu, MiscSettingsMenu.GetMenu(), miscBtn);


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
