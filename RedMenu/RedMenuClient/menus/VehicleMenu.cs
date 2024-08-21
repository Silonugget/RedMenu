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
using Newtonsoft.Json.Linq;

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
        private static Dictionary<string, JObject> vehicleConfigs = new Dictionary<string, JObject>();

        public VehicleMenu()
        {
            EventHandlers["receiveVehicleConfigJSON"] += new Action<string>(ReceiveVehicleConfig);
        }

        private static void ReceiveVehicleConfig(string configJson)
        {
            JObject configData = JObject.Parse(configJson);
            vehicleConfigs.Clear();

            foreach (var vehicleType in configData)
            {
                vehicleConfigs.Add(vehicleType.Key, (JObject)vehicleType.Value);
            }

            Debug.WriteLine($"Number of different vehicle types: {vehicleConfigs.Count}");
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

        // Reintroduced: AddVehicleSubmenu for regular vehicle submenus
        private static void AddVehicleSubmenu(Menu menu, List<string> hashes, string name, string description)
        {
            Menu submenu = new Menu(name, description);
            MenuItem submenuBtn = new MenuItem(name, description) { RightIcon = MenuItem.Icon.ARROW_RIGHT };
            menu.AddMenuItem(submenuBtn);
            MenuController.AddSubmenu(menu, submenu);
            MenuController.BindMenuItem(menu, submenu, submenuBtn);

            foreach (var hash in hashes)
            {
                MenuItem item = new MenuItem(hash);
                submenu.AddMenuItem(item);
            }

            submenu.OnItemSelect += async (m, item, index) =>
            {
                if (currentVehicle != 0)
                {
                    DeleteVehicle(ref currentVehicle);
                    currentVehicle = 0;
                }

                uint model = (uint)GetHashKey(hashes[index]);

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

            BaseScript.TriggerServerEvent("requestVehicleConfigJSON");
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

                // Loop through addon vehicles from vehicleConfigs and add to the menu
                foreach (var vehicleType in vehicleConfigs.Keys)
                {
                    MenuItem item = new MenuItem(vehicleType);
                    addonVehiclesMenu.AddMenuItem(item);
                }

                addonVehiclesMenu.OnItemSelect += async (m, item, index) =>
                {
                    string selectedVehicleType = item.Text;

                    // Get the first vehicle of the selected type and spawn it
                    if (vehicleConfigs.ContainsKey(selectedVehicleType))
                    {
                        JArray vehicles = (JArray)vehicleConfigs[selectedVehicleType]["vehicles"];
                        string firstVehicle = vehicles[0]["model"].ToString();

                        if (currentVehicle != 0)
                        {
                            DeleteVehicle(ref currentVehicle);
                            currentVehicle = 0;
                        }

                        uint model = (uint)GetHashKey(firstVehicle);

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

                            if (firstVehicle == "hotairballoon01")
                            {
                                FixHotAirBalloon(currentVehicle);
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"^1[ERROR] This vehicle model is not present in the game files {model}.^7");
                        }
                    }
                };

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
