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
using Newtonsoft.Json.Linq; // For handling JSON data

namespace RedMenuClient.menus
{
    public class VehicleDistance
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

    public class VehicleMenu : BaseScript
    {
        // Global list to store objectModel values
        private static List<string> vehicleObjectModels = new List<string>();

        // Global dictionary to store vehicle configurations
        private static Dictionary<string, JObject> vehicleConfigs = new Dictionary<string, JObject>();

        public VehicleMenu()
        {
            // Register the event that will be triggered from Lua
            EventHandlers["receiveVehicleConfigJSON"] += new Action<string>(ReceiveVehicleConfig);
        }

        private static Dictionary<string, List<string>> categorizedVehicles = new Dictionary<string, List<string>>(); // Store vehicles by type

private static void ReceiveVehicleConfig(string configJson)
{
    // Parse the incoming JSON string into a JObject
    JObject configData = JObject.Parse(configJson);

    // Clear any existing configs and objectModel list before updating
    vehicleConfigs.Clear();
    vehicleObjectModels.Clear();
    categorizedVehicles.Clear(); // Clear categorized vehicles

    // Loop through the config and add it to the dictionary and categorized vehicles list
    foreach (var vehicleType in configData)
    {
        vehicleConfigs.Add(vehicleType.Key, (JObject)vehicleType.Value);

        // Extract the objectModel value and add it to the global list
        if (vehicleType.Value["objectModel"] != null)
        {
            string objectModel = vehicleType.Value["objectModel"].ToString();
            vehicleObjectModels.Add(objectModel);
        }

        // Categorize vehicles by type
        if (vehicleType.Value["type"] != null)
        {
            string type = vehicleType.Value["type"].ToString();
            if (!categorizedVehicles.ContainsKey(type))
            {
                categorizedVehicles.Add(type, new List<string>());
            }
            categorizedVehicles[type].Add(vehicleType.Key); // Add the vehicle key to the appropriate type
        }
        else
        {
            // If no type is defined, categorize as "Misc"
            if (!categorizedVehicles.ContainsKey("Misc"))
            {
                categorizedVehicles.Add("Misc", new List<string>());
            }
            categorizedVehicles["Misc"].Add(vehicleType.Key);
        }
    }

    // Debug print: How many different vehicle types and object models are there
    Debug.WriteLine($"Number of different vehicle types: {vehicleConfigs.Count}");
    Debug.WriteLine($"Number of object models: {vehicleObjectModels.Count}");
}
            



        private static Menu menu = new Menu("Vehicle Menu", "Vehicle related options.");
        private static bool setupDone = false;
        private static int currentVehicle = 0;

        private static int BlipAddForEntity(int blipHash, int entity)
        {
            return Function.Call<int>((Hash)0x23F74C2FDA6E7C61, blipHash, entity);
        }

        private static void FixHotAirBalloon(int veh)
        {
            SetVehicleAsNoLongerNeeded(ref veh);
        }

       private static void AddVehicleSubmenu(Menu menu, List<string> keys, string name, string description, bool isAddonVehicle = false)
{
    // Create a new submenu with the provided name and description
    Menu submenu = new Menu(name, description);
    MenuItem submenuBtn = new MenuItem(name, description) { RightIcon = MenuItem.Icon.ARROW_RIGHT };
    menu.AddMenuItem(submenuBtn);
    MenuController.AddSubmenu(menu, submenu);
    MenuController.BindMenuItem(menu, submenu, submenuBtn);

    foreach (var key in keys)
    {
        MenuItem item = new MenuItem(key);
        submenu.AddMenuItem(item);
    }

    submenu.OnItemSelect += async (m, item, index) => // Mark the lambda as async
    {
        string selectedKey = keys[index];

        if (isAddonVehicle)
        {
            // Execute the command with the selected key
            ExecuteCommand($"balboni {selectedKey}");
        }
        else
        {
            // Regular vehicle spawning logic

            if (currentVehicle != 0)
            {
                DeleteVehicle(ref currentVehicle);
                currentVehicle = 0;
            }

            uint model = (uint)GetHashKey(keys[index]);

            int ped = PlayerPedId();
            Vector3 coords = GetEntityCoords(ped, false, false);
            float h = GetEntityHeading(ped);

            float r = -h * (float)(Math.PI / 180);
            float x2 = coords.X + (float)(5 * Math.Sin(r));
            float y2 = coords.Y + (float)(5 * Math.Cos(r));

            if (IsModelInCdimage(model))
            {
                RequestModel(model, false);
                while (!HasModelLoaded(model))
                {
                    await BaseScript.Delay(0); // Use await correctly now
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

                if (keys[index] == "hotairballoon01")
                {
                    FixHotAirBalloon(currentVehicle);
                }
            }
            else
            {
                Debug.WriteLine($"^1[ERROR] This vehicle model is not present in the game files: {model}.^7");
            }
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

        public static async void SetupMenu()
{
    if (setupDone) return;

    // Wait until vehicleObjectModels has been populated
    while (vehicleObjectModels.Count == 0)
    {
        Debug.WriteLine("Waiting for vehicleObjectModels to populate...");
        await BaseScript.Delay(100);  // Wait for 100ms before checking again
    }

    Debug.WriteLine("vehicleObjectModels populated, setting up menu.");
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
    Menu spawnVehicleMenu = new Menu("Spawn Vehicle", "Spawn a vehicle");
    MenuItem spawnVehicle = new MenuItem("Spawn Vehicle", "Spawn a vehicle") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
    menu.AddMenuItem(spawnVehicle);
    MenuController.AddSubmenu(menu, spawnVehicleMenu);
    MenuController.BindMenuItem(menu, spawnVehicleMenu, spawnVehicle);


  // Addon Vehicles Selection
        Menu addonVehicleTypeMenu = new Menu("Vehicle Types", "Select vehicle type");
        MenuItem addonVehicles = new MenuItem("Addon Vehicles", "Spawn addon vehicle") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
        spawnVehicleMenu.AddMenuItem(addonVehicles);
        MenuController.AddSubmenu(spawnVehicleMenu, addonVehicleTypeMenu);
        MenuController.BindMenuItem(spawnVehicleMenu, addonVehicleTypeMenu, addonVehicles);

        // Create submenus for each addon vehicle type
        foreach (var type in categorizedVehicles.Keys)
        {
            Menu typeMenu = new Menu(type, $"Select a {type} vehicle");
            MenuItem typeMenuButton = new MenuItem(type, $"Select a {type} vehicle") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
            addonVehicleTypeMenu.AddMenuItem(typeMenuButton);
            MenuController.AddSubmenu(addonVehicleTypeMenu, typeMenu);
            MenuController.BindMenuItem(addonVehicleTypeMenu, typeMenu, typeMenuButton);

            // Add vehicles of this type to the submenu
            AddVehicleSubmenu(typeMenu, categorizedVehicles[type], type, $"List of available {type} vehicles", true);
        }


    // Regular Vehicles Submenu
    Menu regularVehiclesMenu = new Menu("Regular", "Spawn a regular vehicle");
    MenuItem regularVehicles = new MenuItem("Regular", "Spawn a regular vehicle") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
    spawnVehicleMenu.AddMenuItem(regularVehicles);
    MenuController.AddSubmenu(spawnVehicleMenu, regularVehiclesMenu);
    MenuController.BindMenuItem(spawnVehicleMenu, regularVehiclesMenu, regularVehicles);

    // Call AddVehicleSubmenu with other vehicle lists
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
            // Trigger the server event to request the vehicle config after setting up the menu
    BaseScript.TriggerServerEvent("requestVehicleConfigJSON");

            SetupMenu();
            return menu;
        }

    }
}
