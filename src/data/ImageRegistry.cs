﻿namespace rose.row.data
{
    //public static class ImageNames
    //{
    //    public static string basePath => $"{Constants.basePath}/Textures/UI";

    //    #region crosshair

    //    public static readonly ImageHolder crosshairHorizontal = new ImageHolder("crosshair/crosshair_horizontal.png");
    //    public static readonly ImageHolder crosshairVertical = new ImageHolder("crosshair/crosshair_vertical.png");

    //    #endregion

    //    #region Capture Points

    //    public static readonly string spawnPointNeutral = "capture_points/spawn_point_neutral.png";
    //    public static readonly string spawnPointFriendly = "capture_points/spawn_point_friendly.png";
    //    public static readonly string spawnPointEnemy = "capture_points/spawn_point_enemy.png";
    //    public static readonly string capturePointNeutral = "capture_points/capture_point_neutral.png";
    //    public static readonly string capturePointFriendly = "capture_points/capture_point_friendly.png";
    //    public static readonly string capturePointEnemy = "capture_points/capture_point_enemy.png";
    //    public static readonly string capturePointSelected = "capture_points/capture_point_selected.png";

    //    #endregion Capture Points

    //    #region Factions

    //    public static readonly string germanyFactionImage = "factions/germany_icon_low_resolution.png";
    //    public static readonly string urssFactionImage = "factions/urss_icon_low_resolution.png";
    //    public static readonly string usaFactionImage = "factions/usa_icon_low_resolution.png";

    //    #endregion Factions

    //    #region Vehicle Resources

    //    public static readonly string resourceTransportVehicle = "resources/resource_transport_vehicle.png";
    //    public static readonly string resourceTransportPlane = "resources/resource_transport_plane.png";
    //    public static readonly string resourceTankCrew = "resources/resource_tank_crew.png";
    //    public static readonly string resourceReconPlane = "resources/resource_recon_plane.png";
    //    public static readonly string resourceRecon = "resources/resource_recon.png";
    //    public static readonly string resourceParatrooper = "resources/resource_paratrooper.png";
    //    public static readonly string resourceNeutral = "resources/resource_neutral.png";
    //    public static readonly string resourceMotorcycle = "resources/resource_motorcycle.png";
    //    public static readonly string resourceMediumTankDestroyer = "resources/resource_medium_tank_destroyer.png";
    //    public static readonly string resourceMediumArmor = "resources/resource_medium_armor.png";
    //    public static readonly string resourceLightTankDestroyer = "resources/resource_light_tank_destroyer.png";
    //    public static readonly string resourceLightArmor = "resources/resource_light_armor.png";
    //    public static readonly string resourceInfantry = "resources/resource_infantry.png";
    //    public static readonly string resourceHeavyTankDestroyer = "resources/resource_heavy_tank_destroyer.png";
    //    public static readonly string resourceHeavyFighterPlane = "resources/resource_heavy_fighter_plane.png";
    //    public static readonly string resourceHeavyArmor = "resources/resource_heavy_armor.png";
    //    public static readonly string resourceHalfTrack = "resources/resource_half_track.png";
    //    public static readonly string resourceFighterPlane = "resources/resource_fighter_plane.png";
    //    public static readonly string resourceFighterPilot = "resources/resource_fighter_pilot.png";
    //    public static readonly string resourceDefault = "resources/resource_default.png";
    //    public static readonly string resourceArtillery = "resources/resource_artillery.png";
    //    public static readonly string resourceArmoredCar = "resources/resource_armored_car.png";

    //    #endregion Vehicle Resources

    //    #region resupply

    //    public static readonly string resupplyHealth = "resupply/resupply_health.png";
    //    public static readonly string resupplyAmmunition = "resupply/resupply_ammo.png";
    //    public static readonly string resupplyExplosives = "resupply/resupply_explosives.png";

    //    #endregion resupply

    //    #region menu

    //    #region window

    //    public static readonly string menuWindowTop = "menu/window/menu_top.png";
    //    public static readonly string menuWindowInside = "menu/window/menu_inside.png";
    //    public static readonly string menuWindowBottom = "menu/window/menu_bottom.png";

    //    #endregion window

    //    #region buttons

    //    public static readonly string menuButton = "menu/buttons/button.png";
    //    public static readonly string desktopMenuButton = "menu/buttons/desktop_button.png";

    //    #endregion buttons

    //    #endregion menu

    //    #region main menu

    //    #region backgrounds

    //    // Doing a test with using the blurred images instead of the normal ones.
    //    // TODO: Since we're not in URP, one cool thing we can do is actual good UI blur. This
    //    // would allow us to have a dynamically blurred window. For this reason, *look more into
    //    // how to add shaders to a bepinex mod dynamically, without the use of asset bundles*. If
    //    // that's not possible, then look into how to include asset bundles directly in the dll
    //    // file.
    //    public static readonly string mainMenuBackground0 = "backgrounds/background_0_blur.png";

    //    public static readonly string mainMenuBackground1 = "backgrounds/background_1_blur.png";
    //    public static readonly string mainMenuBackground2 = "backgrounds/background_2_blur.png";

    //    public static readonly string desktopBackground = "backgrounds/desktop_background.png";
    //    public static readonly string desktopBackgroundCover = "backgrounds/desktop_background_cover.png";

    //    #endregion backgrounds

    //    #region shadows

    //    public static readonly string fieldShadow = "shadows/field_shadow.png";

    //    #endregion shadows

    //    #region war

    //    public static readonly string warWindowBackground = "backgrounds/war_window_background.png";
    //    public static readonly string warMap = "war_map.jpg";
    //    public static readonly string warMissionButton = "menu/buttons/war_mission_button.png";

    //    #endregion war

    //    #endregion main menu
    //}

    public static class ImageRegistry
    {
        public static string basePath => $"{Constants.basePath}/Textures/UI";

        // Spawn Menu
        public static readonly ImageHolder spawnMenuDeployingBackground = new ImageHolder("spawn_menu/deploying_background.png");
        public static readonly ImageHolder spawnMenuHomeIcon = new ImageHolder("spawn_menu/deploying_home.png");

        // Ranks
        public static readonly ImageHolder[] ranksGM = new ImageHolder[]
        {
            new ImageHolder("ranks/gm/1.png"),
            new ImageHolder("ranks/gm/2.png"),
            new ImageHolder("ranks/gm/3.png"),
            new ImageHolder("ranks/gm/4.png"),
            new ImageHolder("ranks/gm/5.png"),
            new ImageHolder("ranks/gm/6.png"),
            new ImageHolder("ranks/gm/7.png"),
            new ImageHolder("ranks/gm/8.png"),
            new ImageHolder("ranks/gm/9.png"),
            new ImageHolder("ranks/gm/10.png"),
            new ImageHolder("ranks/gm/11.png"),
            new ImageHolder("ranks/gm/12.png"),
            new ImageHolder("ranks/gm/13.png"),
            new ImageHolder("ranks/gm/14.png"),
            new ImageHolder("ranks/gm/15.png"),
            new ImageHolder("ranks/gm/16.png"),
            new ImageHolder("ranks/gm/17.png"),
            new ImageHolder("ranks/gm/18.png"),
            new ImageHolder("ranks/gm/19.png"),
            new ImageHolder("ranks/gm/20.png"),
            new ImageHolder("ranks/gm/21.png"),
            new ImageHolder("ranks/gm/22.png"),
        };

        public static readonly ImageHolder[] ranksUS = new ImageHolder[]
        {
            new ImageHolder("ranks/us/1.png"),
            new ImageHolder("ranks/us/2.png"),
            new ImageHolder("ranks/us/3.png"),
            new ImageHolder("ranks/us/4.png"),
            new ImageHolder("ranks/us/5.png"),
            new ImageHolder("ranks/us/6.png"),
            new ImageHolder("ranks/us/7.png"),
            new ImageHolder("ranks/us/8.png"),
            new ImageHolder("ranks/us/9.png"),
            new ImageHolder("ranks/us/10.png"),
            new ImageHolder("ranks/us/11.png"),
            new ImageHolder("ranks/us/12.png"),
            new ImageHolder("ranks/us/13.png"),
            new ImageHolder("ranks/us/14.png"),
            new ImageHolder("ranks/us/15.png"),
            new ImageHolder("ranks/us/16.png"),
            new ImageHolder("ranks/us/17.png"),
            new ImageHolder("ranks/us/18.png"),
            new ImageHolder("ranks/us/19.png"),
            new ImageHolder("ranks/us/20.png"),
            new ImageHolder("ranks/us/21.png"),
            new ImageHolder("ranks/us/22.png"),
        };

        public static readonly ImageHolder[] ranksRU = new ImageHolder[]
        {
            new ImageHolder("ranks/ru/1.png"),
            new ImageHolder("ranks/ru/2.png"),
            new ImageHolder("ranks/ru/3.png"),
            new ImageHolder("ranks/ru/4.png"),
            new ImageHolder("ranks/ru/5.png"),
            new ImageHolder("ranks/ru/6.png"),
            new ImageHolder("ranks/ru/7.png"),
            new ImageHolder("ranks/ru/8.png"),
            new ImageHolder("ranks/ru/9.png"),
            new ImageHolder("ranks/ru/10.png"),
            new ImageHolder("ranks/ru/11.png"),
            new ImageHolder("ranks/ru/12.png"),
            new ImageHolder("ranks/ru/13.png"),
            new ImageHolder("ranks/ru/14.png"),
            new ImageHolder("ranks/ru/15.png"),
            new ImageHolder("ranks/ru/16.png"),
            new ImageHolder("ranks/ru/17.png"),
            new ImageHolder("ranks/ru/18.png"),
            new ImageHolder("ranks/ru/19.png"),
            new ImageHolder("ranks/ru/20.png"),
            new ImageHolder("ranks/ru/21.png"),
            new ImageHolder("ranks/ru/22.png"),
        };

        // Scoreboard
        public static readonly ImageHolder scoreboardHeaderBlue = new ImageHolder("scoreboard/scoreboard_header_blue.png");
        public static readonly ImageHolder scoreboardHeaderRed = new ImageHolder("scoreboard/scoreboard_header_red.png");
        public static readonly ImageHolder scoreboardPlayerEntry = new ImageHolder("scoreboard/scoreboard_player_entry.png");
        public static readonly ImageHolder scoreboardPlayerEntrySelected = new ImageHolder("scoreboard/scoreboard_player_entry_selected.png");
        public static readonly ImageHolder scoreboardPlayerDead = new ImageHolder("scoreboard/dead.png");
        public static readonly ImageHolder scoreboardPlayerDeadSquad = new ImageHolder("scoreboard/dead_squad.png");
        public static readonly ImageHolder scoreboardDeaths = new ImageHolder("scoreboard/deaths.png");
        public static readonly ImageHolder scoreboardDestroyedPlanes = new ImageHolder("scoreboard/destroyed_planes.png");
        public static readonly ImageHolder scoreboardDestroyedTanks = new ImageHolder("scoreboard/destroyed_tanks.png");
        public static readonly ImageHolder scoreboardHeadshots = new ImageHolder("scoreboard/headshots.png");
        public static readonly ImageHolder scoreboardKills = new ImageHolder("scoreboard/kills.png");

        public static readonly ImageHolder[] scoreboardPing = new ImageHolder[]
        {
            new ImageHolder("scoreboard/ping_1.png"),
            new ImageHolder("scoreboard/ping_2.png"),
            new ImageHolder("scoreboard/ping_3.png"),
            new ImageHolder("scoreboard/ping_4.png")
        };

        public static readonly ImageHolder[] scoreboardPingSquad = new ImageHolder[]
        {
            new ImageHolder("scoreboard/ping_1_squad.png"),
            new ImageHolder("scoreboard/ping_2_squad.png"),
            new ImageHolder("scoreboard/ping_3_squad.png"),
            new ImageHolder("scoreboard/ping_4_squad.png")
        };

        // End Screen
        public static readonly ImageHolder endScreenStar = new ImageHolder("end_screen/star.png");
        public static readonly ImageHolder endScreenQuitButton = new ImageHolder("end_screen/quit_button.png");

        // Crosshair
        public static readonly ImageHolder hitmarker = new ImageHolder("crosshair/hitmarker.png");
        public static readonly ImageHolder crosshairHorizontal = new ImageHolder("crosshair/crosshair_horizontal.png");
        public static readonly ImageHolder crosshairVertical = new ImageHolder("crosshair/crosshair_vertical.png");

        // Capture Points
        public static readonly ImageHolder spawnPointNeutral = new ImageHolder("capture_points/spawn_point_neutral.png");
        public static readonly ImageHolder spawnPointFriendly = new ImageHolder("capture_points/spawn_point_friendly.png");
        public static readonly ImageHolder spawnPointEnemy = new ImageHolder("capture_points/spawn_point_enemy.png");
        public static readonly ImageHolder capturePointNeutral = new ImageHolder("capture_points/capture_point_neutral.png");
        public static readonly ImageHolder capturePointFriendly = new ImageHolder("capture_points/capture_point_friendly.png");
        public static readonly ImageHolder capturePointEnemy = new ImageHolder("capture_points/capture_point_enemy.png");
        public static readonly ImageHolder capturePointSelected = new ImageHolder("capture_points/capture_point_selected.png");

        // Factions
        public static readonly ImageHolder germanyFactionImage = new ImageHolder("factions/germany_icon_low_resolution.png");
        public static readonly ImageHolder ruFactionImage = new ImageHolder("factions/ru_icon_low_resolution.png");
        public static readonly ImageHolder usFactionImage = new ImageHolder("factions/us_icon_low_resolution.png");
        public static readonly ImageHolder germanyFactionImageHighRes = new ImageHolder("factions/germany_icon_high_resolution.png");
        public static readonly ImageHolder ruFactionImageHighRes = new ImageHolder("factions/ru_icon_high_resolution.png");
        public static readonly ImageHolder usFactionImageHighRes = new ImageHolder("factions/us_icon_high_resolution.png");

        // Resources
        public static readonly ImageHolder resourceTransportVehicle = new ImageHolder("resources/resource_transport_vehicle.png");
        public static readonly ImageHolder resourceTransportPlane = new ImageHolder("resources/resource_transport_plane.png");
        public static readonly ImageHolder resourceTankCrew = new ImageHolder("resources/resource_tank_crew.png");
        public static readonly ImageHolder resourceReconPlane = new ImageHolder("resources/resource_recon_plane.png");
        public static readonly ImageHolder resourceRecon = new ImageHolder("resources/resource_recon.png");
        public static readonly ImageHolder resourceParatrooper = new ImageHolder("resources/resource_paratrooper.png");
        public static readonly ImageHolder resourceNeutral = new ImageHolder("resources/resource_neutral.png");
        public static readonly ImageHolder resourceMotorcycle = new ImageHolder("resources/resource_motorcycle.png");
        public static readonly ImageHolder resourceMediumTankDestroyer = new ImageHolder("resources/resource_medium_tank_destroyer.png");
        public static readonly ImageHolder resourceMediumArmor = new ImageHolder("resources/resource_medium_armor.png");
        public static readonly ImageHolder resourceLightTankDestroyer = new ImageHolder("resources/resource_light_tank_destroyer.png");
        public static readonly ImageHolder resourceLightArmor = new ImageHolder("resources/resource_light_armor.png");
        public static readonly ImageHolder resourceInfantry = new ImageHolder("resources/resource_infantry.png");
        public static readonly ImageHolder resourceHeavyTankDestroyer = new ImageHolder("resources/resource_heavy_tank_destroyer.png");
        public static readonly ImageHolder resourceHeavyFighterPlane = new ImageHolder("resources/resource_heavy_fighter_plane.png");
        public static readonly ImageHolder resourceHeavyArmor = new ImageHolder("resources/resource_heavy_armor.png");
        public static readonly ImageHolder resourceHalfTrack = new ImageHolder("resources/resource_half_track.png");
        public static readonly ImageHolder resourceFighterPlane = new ImageHolder("resources/resource_fighter_plane.png");
        public static readonly ImageHolder resourceFighterPilot = new ImageHolder("resources/resource_fighter_pilot.png");
        public static readonly ImageHolder resourceDefault = new ImageHolder("resources/resource_default.png");
        public static readonly ImageHolder resourceArtillery = new ImageHolder("resources/resource_artillery.png");
        public static readonly ImageHolder resourceArmoredCar = new ImageHolder("resources/resource_armored_car.png");

        // Resupply
        public static readonly ImageHolder resupplyHealth = new ImageHolder("resupply/resupply_health.png");
        public static readonly ImageHolder resupplyAmmunition = new ImageHolder("resupply/resupply_ammo.png");
        public static readonly ImageHolder resupplyExplosives = new ImageHolder("resupply/resupply_explosives.png");

        // Menus
        public static readonly ImageHolder menuWindowTop = new ImageHolder("menu/window/menu_top.png");
        public static readonly ImageHolder menuWindowInside = new ImageHolder("menu/window/menu_inside.png");
        public static readonly ImageHolder menuWindowBottom = new ImageHolder("menu/window/menu_bottom.png");
        public static readonly ImageHolder menuButton = new ImageHolder("menu/buttons/button.png");
        public static readonly ImageHolder desktopMenuButton = new ImageHolder("menu/buttons/desktop_button.png");
        public static readonly ImageHolder warMissionButton = new ImageHolder("menu/buttons/war_mission_button.png");

        // Backgrounds
        public static readonly ImageHolder mainMenuBackground0 = new ImageHolder("backgrounds/background_0_blur.png");
        public static readonly ImageHolder mainMenuBackground1 = new ImageHolder("backgrounds/background_1_blur.png");
        public static readonly ImageHolder mainMenuBackground2 = new ImageHolder("backgrounds/background_2_blur.png");
        public static readonly ImageHolder desktopBackground = new ImageHolder("backgrounds/desktop_background.png");
        public static readonly ImageHolder desktopBackgroundCover = new ImageHolder("backgrounds/desktop_background_cover.png");
        public static readonly ImageHolder warWindowBackground = new ImageHolder("backgrounds/war_window_background.png");

        // Shadows
        public static readonly ImageHolder fieldShadow = new ImageHolder("shadows/field_shadow.png");

        // War
        public static readonly ImageHolder warMap = new ImageHolder("war_map.jpg");
    }
}