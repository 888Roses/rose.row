using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rose.row.ui.ingame.screens.death_screen
{
    using default_package;
    using rose.row.actor.player.camera;
    using rose.row.easy_events;
    using rose.row.easy_package.ui.factory;
    using rose.row.ui;
    using rose.row.ui.cursor;
    using rose.row.ui.ingame.screens.map.icon_displayers;
    using util;

    public class DeathScreen : Singleton<DeathScreen>
    {
        public static Vehicle queuedVehicle = null;
        public static bool hasSetupEvents;

        public static bool canEnterQueuedVehicle()
        {
            return canEnterVehicle(queuedVehicle);
        }

        public static bool canEnterVehicle(Vehicle vehicle)
        {
            var validVehicle = vehicle != null && !vehicle.dead;
            if (validVehicle)
                if (vehicle.AllSeatsTaken())
                    return vehicle.seats.Any(x => x.occupant.aiControlled);
                else
                    return true;

            return false;
        }

        public static void spawn()
        {
            LoadoutUi.instance.OnDeployClick();
        }

        private UiScreen _screen;
        private List<SpawnPointMapIconDisplayer> _spawnPointDisplayers;
        public List<VehicleMapIconDisplayer> vehicleDisplayers;

        public static void create()
        {
            var screen = UiFactory.createUiScreen("Death Screen Ui", order: ScreenOrder.deathScreen);
            var deathScreenUi = new GameObject("Death Screen").AddComponent<DeathScreen>();
            screen.canvas.transform.SetParent(deathScreenUi.transform);
            deathScreenUi._screen = screen;
        }

        public static void subscribeToInitializationEvents()
        {
            MouseCursor.cursorHandlers.Add((e) => instance != null && instance._screen.gameObject.activeSelf);
        }

        private void Awake()
        {
            Events.onPlayerSpawn.after += disable;
            Events.onPlayerDie.after += enable;
            Events.onPointCaptured.after += onCapturePointCaptured;

            Events.onVehicleSpawn.after += onVehicleSpawned;
            Events.onVehicleDie.before += onVehicleDestroyed;

            Events.onLoadoutUiShow.after += onLoadoutShow;

            _spawnPointDisplayers = new List<SpawnPointMapIconDisplayer>();
            vehicleDisplayers = new List<VehicleMapIconDisplayer>();
        }

        private void onLoadoutShow(bool showLoadout)
        {
            LoadoutUi.instance.SetLoadoutVisible(false);
            LoadoutUi.instance.SetMinimapVisible(false);
            LoadoutUi.instance.hideCanvas();
        }

        private void OnDestroy()
        {
            Events.onPlayerSpawn.after -= disable;
            Events.onPlayerDie.after -= enable;
            Events.onPointCaptured.after -= onCapturePointCaptured;

            Events.onVehicleSpawn.after -= onVehicleSpawned;
            Events.onVehicleDie.before -= onVehicleDestroyed;

            Events.onLoadoutUiShow.after -= onLoadoutShow;
        }

        private static void onVehicleDestroyed(Vehicle vehicle, DamageInfo info)
        {
            foreach (var vehicleDisplayer in instance.vehicleDisplayers.Where(x => x.vehicle == vehicle))
            {
                Destroy(vehicleDisplayer);
                instance.vehicleDisplayers.Remove(vehicleDisplayer);
                break;
            }
        }

        private static void onVehicleSpawned(Vehicle vehicle,
                                      VehicleSpawner.VehicleSpawnType type,
                                      Vector3 position,
                                      Quaternion rotation,
                                      int team)
        {
            Debug.Log($"Creating vehicle icon displayer of type '{type}' for vehicle: '{vehicle.name}' (Instance ID: {vehicle.GetInstanceID()}).");

            if (instance._screen.gameObject == null)
            {
                instance._screen = UiFactory.createUiScreen("Death Screen Ui", order: ScreenOrder.deathScreen);
                disable();
            }

            var displayerGameObject = new GameObject(vehicle.name);
            var rectTransform = displayerGameObject.AddComponent<RectTransform>();

            rectTransform.SetParent(instance._screen.gameObject.transform);
            rectTransform.position = Vector3.zero;
            rectTransform.sizeDelta = Vector2.one * 48f;

            var vehicleDisplayer = displayerGameObject.AddComponent<VehicleMapIconDisplayer>();
            vehicleDisplayer.setup(vehicle, type);

            if (instance.vehicleDisplayers == null)
                instance.vehicleDisplayers = new List<VehicleMapIconDisplayer>();

            instance.vehicleDisplayers.Add(vehicleDisplayer);
        }

        private static void onCapturePointCaptured(SpawnPoint capturePoint, int team, bool initialOwner)
        {
            foreach (var displayer in instance._spawnPointDisplayers)
                if (displayer.spawnPoint == capturePoint)
                    displayer.refreshUi();
        }

        private void Start()
        {
            createSpawnPointIconDisplayers();
            refreshCapturePointDisplayers();
        }

        public static void enable(FpsActorController controller = null)
        {
            if (controller == null)
                controller = FpsActorController.instance;

            FpsActorController.instance.FirstPersonCamera();

            instance._screen.gameObject.SetActive(true);
            LoadoutUi.Hide(true);
            KillCamera.Hide();
        }

        public static void disable(FpsActorController controller = null)
        {
            queuedVehicle = null;

            if (controller == null)
                controller = FpsActorController.instance;

            instance._screen.gameObject.SetActive(false);

            instance.createSpawnPointIconDisplayers();
            instance.refreshCapturePointDisplayers();
        }

        private void createSpawnPointIconDisplayers()
        {
            if (_spawnPointDisplayers == null)
            {
                _spawnPointDisplayers = new List<SpawnPointMapIconDisplayer>();
            }
            else
            {
                foreach (var displayer in _spawnPointDisplayers)
                    if (displayer != null)
                        Destroy(displayer.rectTransform.gameObject);
                _spawnPointDisplayers.Clear();
            }

            var points = FindObjectsOfType<SpawnPoint>();
            Debug.Log($"Found {points.Count()} available spawn points:");
            foreach (var point in points)
                Debug.Log($" * '{point.name}' (at {point.transform.position}). Capture range: {point.GetCaptureRange()}");

            var player = ActorManager.instance.player;
            foreach (var point in FindObjectsOfType<SpawnPoint>())
            {
                var displayerGameObject = new GameObject(point.name);

                var rectTransform = displayerGameObject.AddComponent<RectTransform>();
                rectTransform.SetParent(_screen.gameObject.transform);
                rectTransform.position = Vector3.zero;
                rectTransform.sizeDelta = Vector2.one * 48f;

                var displayer = rectTransform.addComponent<SpawnPointMapIconDisplayer>();
                displayer.setup(point);
                displayer.onMouseDown += (eventData) =>
                {
                    if (point.owner != player.team)
                        return;

                    MinimapUi.instance.selectedSpawnPoint = point;
                    spawn();
                };
                displayer.refreshUi();
                _spawnPointDisplayers.Add(displayer);
            }
        }

        private void refreshCapturePointDisplayers()
        {
            if (_spawnPointDisplayers == null || _spawnPointDisplayers.Count == 0)
                return;

            var camera = DeathCamera.instance.camera;
            foreach (var displayer in _spawnPointDisplayers)
            {
                var capturePointPos = displayer.spawnPoint
                    .transform.position.add(y: 100f).grounded(distance: Mathf.Infinity);
                displayer.rectTransform.position = camera.WorldToScreenPoint(capturePointPos).with(z: 0);
            }
        }
    }
}