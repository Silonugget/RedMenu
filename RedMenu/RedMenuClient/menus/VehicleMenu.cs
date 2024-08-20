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
    class VehicleDistance
    {
        public int vehicle;
        public float distance;

        public VehicleDistance(int vehicle)
        {
            this.vehicle = vehicle;

            Vector3 pCoords = GetEntityCoords(PlayerPedId(), true, true);
            Vector3 vCoords = GetEntityCoords(vehicle, true, true);
            distance = GetDistanceBetweenCoords(pCoords.X, pCoords.Y, pCoords.Z, vCoords.X, vCoords.Y, vCoords.Z, true);
        }
    }
    class VehicleMenu
    {
        private static Menu menu = new Menu("Vehicle Menu", "Vehicle related options.");
        private static bool setupDone = false;
        private static int currentVehicle = 0;
            // Define the vehicleConfigs variable globally
    private static Dictionary<string, Dictionary<string, object>> vehicleConfigs;

        // Lua event handler to receive vehicle configs
        [EventHandler("receiveVehicleConfig")]
        private static void OnReceiveVehicleConfig(dynamic config)
        {
            // Convert dynamic config into a dictionary for easier processing in C#
            vehicleConfigs = config.ToObject<Dictionary<string, Dictionary<string, object>>>();
        }

        private static int BlipAddForEntity(int blipHash, int entity)
        {
            return Function.Call<int>((Hash)0x23F74C2FDA6E7C61, blipHash, entity);
        }

        private static void FixHotAirBalloon(int veh)
        {
            SetVehicleAsNoLongerNeeded(ref veh);
        }

private static void AddVehicleSubmenu(Menu menu, List<string> hashes, string name, string description)
{
    // Create a new submenu with the provided name and description
    Menu submenu = new Menu(name, description);
    // Create a menu item that when selected, will navigate to the submenu
    MenuItem submenuBtn = new MenuItem(name, description) { RightIcon = MenuItem.Icon.ARROW_RIGHT };
    // Add the menu item to the main menu
    menu.AddMenuItem(submenuBtn);
    // Add the submenu to the menu controller and bind it to the submenu button
    MenuController.AddSubmenu(menu, submenu);
    MenuController.BindMenuItem(menu, submenu, submenuBtn);

    // Add menu items for each hash in the provided list
    foreach (var hash in hashes)
    {
        MenuItem item = new MenuItem(hash);
        submenu.AddMenuItem(item);
    }

    // Define what happens when an item in the submenu is selected
submenu.OnItemSelect += async (m, item, index) =>
{
    // Check for the "classic" item and execute the "ironhorse" command
    if (item.Text.Equals("Classic"))
    {
        ExecuteCommand("ironhorse");
        // Count unique types in the vehicle config
                    var distinctTypes = vehicleConfigs.Values.Select(v => v["type"]).Distinct().Count();


                    Debug.WriteLine($"Number of distinct vehicle types: {distinctTypes}");
                
        return; // Stop further execution
    }
        else if (item.Text.Equals("More Classic"))
    {
        ExecuteCommand("classic");
        return; // Stop further execution
    }
        else if (item.Text.Equals("Funny Classic"))
    {
        ExecuteCommand("classic2");
        return; // Stop further execution
    }
    // Check for the "offroad" item and execute the "spawn_truck" command
    else if (item.Text.Equals("Offroad"))
    {
        ExecuteCommand("spawn_truck");
        return; // Stop further execution
    }
    // Check for the "emergency" item and execute the "spawn_police2" command
    else if (item.Text.Equals("Emergency"))
    {
        ExecuteCommand("spawn_police2");
        return; // Stop further execution
    }
    // Check for the "sport" item and execute the "g37" command
    else if (item.Text.Equals("Sport"))
    {
        ExecuteCommand("g37");
        return; // Stop further execution
    }
        else if (item.Text.Equals("Jet Horse Ski"))
    {
        ExecuteCommand("jetwaterhorski");
        return; // Stop further execution
    }
        else if (item.Text.Equals("HorseBat"))
    {
        ExecuteCommand("bat");
        return; // Stop further execution
    }
            else if (item.Text.Equals("HorseBat Classic"))
    {
        ExecuteCommand("bat2");
        return; // Stop further execution
    }
            else if (item.Text.Equals("Muscle"))
    {
        ExecuteCommand("silocar");
        return; // Stop further execution
    }
            else if (item.Text.Equals("Super Sport"))
    {
        ExecuteCommand("ironsport");
        return; // Stop further execution
    }
            else if (item.Text.Equals("Xwing"))
    {
        ExecuteCommand("xwing");
        return; // Stop further execution
    }
    else if (item.Text.Equals("Lancer"))
    {
        ExecuteCommand("lancer");
        return; // Stop further execution
    }
    else if (item.Text.Equals("Micahcycle"))
    {
        ExecuteCommand("micahcycle");
        return; // Stop further execution
    }
    else if (item.Text.Equals("Sandrail"))
    {
        ExecuteCommand("sandrail");
        return; // Stop further execution
    }
    else if (item.Text.Equals("Hellcat"))
    {
        ExecuteCommand("hellcat");
        return; // Stop further execution
    }
    else if (item.Text.Equals("Fireplane"))
    {
        ExecuteCommand("fireplane");
        return; // Stop further execution
    }
        else if (item.Text.Equals("GTR"))
    {
        ExecuteCommand("gtr");
        return; // Stop further execution
    }
        else if (item.Text.Equals("Malibu"))
    {
        ExecuteCommand("malibu");
        return; // Stop further execution
    }
        else if (item.Text.Equals("Tank"))
    {
        ExecuteCommand("tank");
        return; // Stop further execution
    }
        else if (item.Text.Equals("Roadster"))
    {
        ExecuteCommand("roadster");
        return; // Stop further execution
    }
        // Existing code for spawning vehicles
        if (currentVehicle != 0)
        {
            DeleteVehicle(ref currentVehicle);
            currentVehicle = 0;
        }

                uint model = (uint)GetHashKey(hashes[index]);

                int ped = PlayerPedId();
                Vector3 coords = GetEntityCoords(ped, false, false);
                float h = GetEntityHeading(ped);

                // Get a point in front of the player
                float r = -h * (float)(Math.PI / 180);
                float x2 = coords.X + (float)(5 * Math.Sin(r));
                float y2 = coords.Y + (float)(5 * Math.Cos(r));

                if (IsModelInCdimage(model))
                {
                    RequestModel(model, false);
                    while (!HasModelLoaded(model))
                    {
                        await BaseScript.Delay(0);
                    }

                    currentVehicle = CreateVehicle(model, x2, y2, coords.Z, h, true, true, false, true);
                    SetModelAsNoLongerNeeded(model);
                    SetVehicleOnGroundProperly(currentVehicle, 0);
                    SetEntityVisible(currentVehicle, true);
                    BlipAddForEntity(631964804, currentVehicle);

                    if (UserDefaults.VehicleSpawnInside)
                    {
                        TaskWarpPedIntoVehicle(ped, currentVehicle, -1);
                    }

                    // If this isn't done, the hot air balloon won't move with the wind for some reason
                    if (hashes[index] == "hotairballoon01")
                    {
                        FixHotAirBalloon(currentVehicle);
                    }
                }
                else
                {
                    Debug.WriteLine($"^1[ERROR] This vehicle model is not present in the game files {model}.^7");
                }
            };
        }
        

        private static int GetNearestVehicle()
        {
            List<VehicleDistance> vehicles = new List<VehicleDistance>();
            int veh = 0;
            int handle = FindFirstVehicle(ref veh);
            vehicles.Add(new VehicleDistance(veh));
            while (FindNextVehicle(handle, ref veh))
            {
                vehicles.Add(new VehicleDistance(veh));
            }
            return vehicles.OrderBy(v => v.distance).First().vehicle;
        }

        private static void SetVehicleTint(int vehicle, int tint)
        {
            Function.Call((Hash)0x8268B098F6FCA4E2, vehicle, tint);
        }

        public static void SetupMenu()
        {
            if (setupDone) return;
            TriggerServerEvent("requestVehicleConfig");
            setupDone = true;

            MenuCheckboxItem spawnInside = new MenuCheckboxItem("Spawn Inside Vehicle", "Automatically spawn inside vehicles.", UserDefaults.VehicleSpawnInside);
            MenuItem repairVehicle = new MenuItem("Repair Vehicle", "Repair the vehicle you are currently in.");
            MenuItem teleport = new MenuItem("Teleport Into Vehicle", "Teleport into the closest vehicle with an open seat.");
            MenuListItem engineOnOff = new MenuListItem("Engine", new List<string>() { "On", "Off" }, 0, "Set vehicle engine on/off.");
            MenuListItem lightsOnOff = new MenuListItem("Lights", new List<string>() { "On", "Off" }, 0, "Set vehicle lights on/off.");
            MenuItem deleteVehicle = new MenuItem("Delete Vehicle", "Delete the vehicle you are currently in.");

            MenuDynamicListItem vehicleTint = new MenuDynamicListItem("Vehicle Tint", "0", new MenuDynamicListItem.ChangeItemCallback((item, left) =>
            {
                if (int.TryParse(item.CurrentItem, out int val))
                {
                    int newVal = val;
                    if (left)
                    {
                        newVal--;
                        if (newVal < 0)
                        {
                            newVal = 0;
                        }
                    }
                    else
                    {
                        newVal++;
                    }
                    SetVehicleTint(GetVehiclePedIsIn(PlayerPedId(), false), newVal);
                    return newVal.ToString();
                }
                return "0";
            }), "Select a predefined tint for the vehicle you are currently in.");

            if (PermissionsManager.IsAllowed(Permission.VMSpawn))
{
    Menu spawnVehicleMenu = new Menu("Spawn Vehicle", "Spawn a vehicle.");
    MenuItem spawnVehicle = new MenuItem("Spawn Vehicle", "Spawn a vehicle.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
    menu.AddMenuItem(spawnVehicle);
    MenuController.AddSubmenu(menu, spawnVehicleMenu);
    MenuController.BindMenuItem(menu, spawnVehicleMenu, spawnVehicle);

       
                // Addon Vehicles Submenu
                Menu addonVehiclesMenu = new Menu("Addon Vehicles", "Spawn an addon vehicle.");
                MenuItem addonVehicles = new MenuItem("Addon Vehicles", "Spawn an addon vehicle.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
                spawnVehicleMenu.AddMenuItem(addonVehicles);
                MenuController.AddSubmenu(spawnVehicleMenu, addonVehiclesMenu);
                MenuController.BindMenuItem(spawnVehicleMenu, addonVehiclesMenu, addonVehicles);

                // Define list of hashes for the iron horses here, including the string "classic"
                List<string> ironHorseHashes = new List<string> { "Classic", "More Classic", "Funny Classic", "Offroad", "Sandrail", "Muscle", "Hellcat", "Sport", "Lancer", "Super Sport", "HorseBat", "Micahcycle", "HorseBat Classic", "Roadster", "GTR", "Malibu", "Tank", "Emergency" };
                AddVehicleSubmenu(addonVehiclesMenu, ironHorseHashes, "Iron Horses", "Spawn an iron horse.");
                // Water Horses Submenu
                List<string> waterHorseHashes = new List<string> { "Jet Horse Ski" };
                AddVehicleSubmenu(addonVehiclesMenu, waterHorseHashes, "Water Horses", "Spawn a water horse.");

               // Air Horses Submenu
               List<string> airHorseHashes = new List<string> { "Xwing", "Fireplane" };
               AddVehicleSubmenu(addonVehiclesMenu, airHorseHashes, "Air Horses", "Spawn an air horse (spawn in water).");


    // Regular Vehicles Submenu
    Menu regularVehiclesMenu = new Menu("Regular", "Spawn a regular vehicle.");
    MenuItem regularVehicles = new MenuItem("Regular", "Spawn a regular vehicle.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
    spawnVehicleMenu.AddMenuItem(regularVehicles);
    MenuController.AddSubmenu(spawnVehicleMenu, regularVehiclesMenu);
    MenuController.BindMenuItem(spawnVehicleMenu, regularVehiclesMenu, regularVehicles);

    AddVehicleSubmenu(regularVehiclesMenu, data.VehicleData.BuggyHashes, "Buggies", "Spawn a buggy.");
    AddVehicleSubmenu(regularVehiclesMenu, data.VehicleData.BoatHashes, "Boats", "Spawn a boat.");
    AddVehicleSubmenu(regularVehiclesMenu, data.VehicleData.CartHashes, "Carts", "Spawn a cart.");
    AddVehicleSubmenu(regularVehiclesMenu, data.VehicleData.CoachHashes, "Coaches", "Spawn a coach.");
    AddVehicleSubmenu(regularVehiclesMenu, data.VehicleData.WagonHashes, "Wagons", "Spawn a wagon.");
    AddVehicleSubmenu(regularVehiclesMenu, data.VehicleData.MiscHashes, "Misc", "Spawn a miscellaneous vehicle.");
}


            if (PermissionsManager.IsAllowed(Permission.VMSelectTint))
            {
                menu.AddMenuItem(vehicleTint);
            }

            if (PermissionsManager.IsAllowed(Permission.VMSpawnInside))
            {
                menu.AddMenuItem(spawnInside);
            }

            if (PermissionsManager.IsAllowed(Permission.VMRepair))
            {
                menu.AddMenuItem(repairVehicle);
            }

            if (PermissionsManager.IsAllowed(Permission.VMTeleport))
            {
                menu.AddMenuItem(teleport);
            }

            if (PermissionsManager.IsAllowed(Permission.VMEngineOnOff))
            {
                menu.AddMenuItem(engineOnOff);
            }

            if (PermissionsManager.IsAllowed(Permission.VMLightsOnOff))
            {
                menu.AddMenuItem(lightsOnOff);
            }

            if (PermissionsManager.IsAllowed(Permission.VMDoors))
            {
                Menu doorsMenu = new Menu("Doors", "Open/Close vehicle doors.");
                MenuItem doors = new MenuItem("Doors", "Open/Close vehicle doors.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
                menu.AddMenuItem(doors);
                MenuController.AddSubmenu(menu, doorsMenu);
                MenuController.BindMenuItem(menu, doorsMenu, doors);

                MenuListItem all = new MenuListItem("All", new List<string>() { "Open", "Close" }, 0);
                MenuListItem frontL = new MenuListItem("Front Left", new List<string>() { "Open", "Close" }, 0);
                MenuListItem frontR = new MenuListItem("Front Right", new List<string>() { "Open", "Close" }, 0);
                MenuListItem backL = new MenuListItem("Back Left", new List<string>() { "Open", "Close" }, 0);
                MenuListItem backR = new MenuListItem("Back Right", new List<string>() { "Open", "Close" }, 0);
                MenuListItem hood = new MenuListItem("Hood", new List<string>() { "Open", "Close" }, 0);
                MenuListItem trunk = new MenuListItem("Trunk", new List<string>() { "Open", "Close" }, 0);
                MenuListItem back1 = new MenuListItem("Back 1", new List<string>() { "Open", "Close" }, 0);
                MenuListItem back2 = new MenuListItem("Back 2", new List<string>() { "Open", "Close" }, 0);

                doorsMenu.AddMenuItem(all);
                doorsMenu.AddMenuItem(frontL);
                doorsMenu.AddMenuItem(frontR);
                doorsMenu.AddMenuItem(backL);
                doorsMenu.AddMenuItem(backR);
                doorsMenu.AddMenuItem(hood);
                doorsMenu.AddMenuItem(trunk);
                doorsMenu.AddMenuItem(back1);
                doorsMenu.AddMenuItem(back2);

                doorsMenu.OnListItemSelect += (m, listItem, selectedIndex, itemIndex) =>
                {
                    int doorIndex;

                    if (listItem == frontL)
                    {
                        doorIndex = 0;
                    }
                    else if (listItem == frontR)
                    {
                        doorIndex = 1;
                    }
                    else if (listItem == backL)
                    {
                        doorIndex = 2;
                    }
                    else if (listItem == backR)
                    {
                        doorIndex = 3;
                    }
                    else if (listItem == hood)
                    {
                        doorIndex = 4;
                    }
                    else if (listItem == trunk)
                    {
                        doorIndex = 5;
                    }
                    else if (listItem == back1)
                    {
                        doorIndex = 6;
                    }
                    else if (listItem == back2)
                    {
                        doorIndex = 7;
                    }
                    else
                    {
                        doorIndex = -1;
                    }

                    string selected = listItem.GetCurrentSelection();

                    int veh = GetVehiclePedIsIn(PlayerPedId(), false);

                    if (selected == "Open")
                    {
                        if (doorIndex == -1)
                        {
                            for (int i = 0; i < 8; ++i)
                            {
                                SetVehicleDoorOpen(veh, i, false, false);
                            }
                        }
                        else
                        {
                            SetVehicleDoorOpen(veh, doorIndex, false, false);
                        }
                    }
                    else
                    {
                        if (doorIndex == -1)
                        {
                            SetVehicleDoorsShut(veh, false);
                        }
                        else
                        {
                            SetVehicleDoorShut(veh, doorIndex, false);
                        }
                    }
                };
            }

            if (PermissionsManager.IsAllowed(Permission.VMDelete))
            {
                menu.AddMenuItem(deleteVehicle);
            }

            menu.OnItemSelect += (m, item, index) =>
            {
                if (item == repairVehicle)
                {
                    int veh = GetVehiclePedIsIn(PlayerPedId(), true);

                    if (veh != 0)
                    {
                        SetVehicleFixed(veh);
                    }
                }
                else if (item == teleport)
                {
                    int veh = GetNearestVehicle();

                    if (veh != 0)
                    {
                        NetworkRequestControlOfEntity(veh);
                        TaskWarpPedIntoVehicle(PlayerPedId(), veh, -1);
                    }
                }
                else if (item == deleteVehicle)
                {
                    int veh = GetVehiclePedIsIn(PlayerPedId(), true);

                    if (veh != 0)
                    {
                        DeleteEntity(ref veh);
                    }
                }
            };

            menu.OnCheckboxChange += (m, item, index, _checked) =>
            {
                if (item == spawnInside)
                {
                    UserDefaults.VehicleSpawnInside = _checked;
                }
            };

            menu.OnListItemSelect += (m, item, listIndex, itemIndex) =>
            {
                if (item == engineOnOff)
                {
                    switch (listIndex)
                    {
                        case 0: // on
                            SetVehicleEngineOn(GetVehiclePedIsIn(PlayerPedId(), false), 1, 0);
                            break;
                        case 1: // off
                            SetVehicleEngineOn(GetVehiclePedIsIn(PlayerPedId(), false), 0, 0);
                            break;
                        default:
                            break;
                    }
                }
                else if (item == lightsOnOff)
                {
                    switch (listIndex)
                    {
                        case 0: // on
                            SetVehicleLights(GetVehiclePedIsIn(PlayerPedId(), true), 0);
                            break;
                        case 1: // off
                            SetVehicleLights(GetVehiclePedIsIn(PlayerPedId(), true), 1);
                            break;
                        default:
                            break;
                    }
                }
            };
        }


        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}
