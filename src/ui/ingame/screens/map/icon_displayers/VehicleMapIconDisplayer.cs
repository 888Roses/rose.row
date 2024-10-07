using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.ui.elements;
using rose.row.ui.ingame.screens.death_screen;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.ui.ingame.screens.map.icon_displayers
{
    public class VehicleMapIconDisplayer : AbstractTargetMapIconDisplayer
    {
        public static Texture2D getTextureForVehicle(VehicleSpawner.VehicleSpawnType spawnType)
        {
            var vehicleIcon = ImageRegistry.resourceNeutral.get();
            switch (spawnType)
            {
                case VehicleSpawner.VehicleSpawnType.Jeep:
                    vehicleIcon = ImageRegistry.resourceTransportVehicle.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.JeepMachineGun:
                    vehicleIcon = ImageRegistry.resourceTransportVehicle.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.Quad:
                    vehicleIcon = ImageRegistry.resourceMotorcycle.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.Tank:
                    vehicleIcon = ImageRegistry.resourceHeavyArmor.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.Apc:
                    vehicleIcon = ImageRegistry.resourceArmoredCar.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.AttackPlane:
                    vehicleIcon = ImageRegistry.resourceFighterPlane.get();
                    break;

                case VehicleSpawner.VehicleSpawnType.BomberPlane:
                    vehicleIcon = ImageRegistry.resourceHeavyFighterPlane.get();
                    break;
            };

            return vehicleIcon;
        }

        public Vehicle vehicle;
        public VehicleSpawner.VehicleSpawnType spawnType;

        public void setup(Vehicle vehicle, VehicleSpawner.VehicleSpawnType spawnType)
        {
            this.vehicle = vehicle;
            this.spawnType = spawnType;

            buildUi();
        }

        public override Transform target => vehicle.transform;

        public UiElement vehicleImage;
        public HoveredElementIndicator hoveredIcon;

        public override void buildUi()
        {
            vehicleImage = UiFactory.createGenericUiElement("Vehicle Icon", rectTransform);
            vehicleImage.setAnchors(UiElement.Anchors.FillParent);
            vehicleImage.image();

            hoveredIcon = UiFactory.createUiElement<HoveredElementIndicator>("Hover Indicator", rectTransform);

            initializeUi();
        }

        protected virtual void initializeUi()
        {
            if (vehicleImage.image() == null)
                vehicleImage.image();

            if (hoveredIcon.image() == null)
                hoveredIcon.image();

            vehicleImage.image().texture = getTextureForVehicle(spawnType);
            hoveredIcon.image().texture = ImageRegistry.capturePointSelected.get();
            hoveredIcon.gameObject.SetActive(false);
        }

        public virtual bool canBeSeen()
        {
            return vehicle.HasDriver() && vehicle.Driver().team == ActorManager.instance.player.team
                    && DeathScreenOld.canEnterVehicle(vehicle);
        }

        private void Update()
        {
            if (!canBeSeen())
            {
                vehicleImage.gameObject.SetActive(false);
                return;
            }

            vehicleImage.gameObject.SetActive(true);
            rectTransform.position = getScreenPosition();
        }

        protected override void mouseDown(PointerEventData eventData)
        {
            if (canBeSeen())
            {
                DeathScreenOld.queuedVehicle = vehicle;
                DeathScreenOld.spawn();
            }
        }

        protected override void mouseEnter(PointerEventData eventData)
        {
            setHovered(true);
        }

        protected override void mouseExit(PointerEventData eventData)
        {
            setHovered(false);
        }

        protected virtual void setHovered(bool isHovered)
        {
            if (canBeSeen())
                hoveredIcon.gameObject.SetActive(isHovered);
            else
            {
                if (vehicle == null || vehicle.dead || vehicle.health == 0)
                {
                    dispose();
                }
            }
        }

        private void dispose()
        {
            if (DeathScreenOld.instance.vehicleDisplayers.Contains(this))
                DeathScreenOld.instance.vehicleDisplayers.Remove(this);

            Destroy(gameObject);
        }
    }
}