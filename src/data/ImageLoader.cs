using rose.row.util;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace rose.row.data
{
    public static class ImageLoader
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        //public static void loadRequiredImages()
        //{
        //    loadImage(ImageNames.resupplyAmmunition);
        //    loadImage(ImageNames.resupplyHealth);
        //    loadImage(ImageNames.resupplyExplosives);

        //    loadImage(ImageNames.warMap);
        //    loadImage(ImageNames.warWindowBackground);
        //    loadImage(ImageNames.warMissionButton);

        //    loadImage(ImageNames.desktopBackground);
        //    loadImage(ImageNames.desktopBackgroundCover);

        //    loadImage(ImageNames.fieldShadow);

        //    loadImage(ImageNames.mainMenuBackground0);
        //    loadImage(ImageNames.mainMenuBackground1);
        //    loadImage(ImageNames.mainMenuBackground2);

        //    loadImage(ImageNames.capturePointSelected);

        //    loadImage(ImageNames.capturePointFriendly);
        //    loadImage(ImageNames.capturePointNeutral);
        //    loadImage(ImageNames.capturePointEnemy);

        //    loadImage(ImageNames.spawnPointFriendly);
        //    loadImage(ImageNames.spawnPointNeutral);
        //    loadImage(ImageNames.spawnPointEnemy);

        //    loadImage(ImageNames.germanyFactionImage);
        //    loadImage(ImageNames.urssFactionImage);
        //    loadImage(ImageNames.usaFactionImage);

        //    loadImage(ImageNames.resourceTransportVehicle);
        //    loadImage(ImageNames.resourceTransportPlane);
        //    loadImage(ImageNames.resourceTankCrew);
        //    loadImage(ImageNames.resourceReconPlane);
        //    loadImage(ImageNames.resourceRecon);
        //    loadImage(ImageNames.resourceParatrooper);
        //    loadImage(ImageNames.resourceNeutral);
        //    loadImage(ImageNames.resourceMotorcycle);
        //    loadImage(ImageNames.resourceMediumTankDestroyer);
        //    loadImage(ImageNames.resourceMediumArmor);
        //    loadImage(ImageNames.resourceLightTankDestroyer);
        //    loadImage(ImageNames.resourceLightArmor);
        //    loadImage(ImageNames.resourceInfantry);
        //    loadImage(ImageNames.resourceHeavyTankDestroyer);
        //    loadImage(ImageNames.resourceHeavyFighterPlane);
        //    loadImage(ImageNames.resourceHeavyArmor);
        //    loadImage(ImageNames.resourceHalfTrack);
        //    loadImage(ImageNames.resourceFighterPlane);
        //    loadImage(ImageNames.resourceFighterPilot);
        //    loadImage(ImageNames.resourceDefault);
        //    loadImage(ImageNames.resourceArtillery);
        //    loadImage(ImageNames.resourceArmoredCar);

        //    loadImage(ImageNames.menuButton);
        //    loadImage(ImageNames.desktopMenuButton);

        //    loadImage(ImageNames.menuWindowTop);
        //    loadImage(ImageNames.menuWindowInside);
        //    loadImage(ImageNames.menuWindowBottom);
        //}

        public static bool loadImage(string name)
        {
            var path = Path.Combine(ImageRegistry.basePath, name);
            if (TextureUtil.loadTexture(path, out var texture))
            {
                textures.Add(name, texture);
                return true;
            }

            Debug.LogError($"Couldn't load image \"{name}\"");
            return false;
        }
    }
}