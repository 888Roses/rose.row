using rose.row.data;
using UnityEngine;

namespace rose.row.main_menu.war.war_data
{
    public readonly struct MapLocation
    {
        public readonly int x;
        public readonly int y;

        public readonly float latitude;
        public readonly float longitude;

        public MapLocation(int x, int y, float latitude, float longitude)
        {
            this.x = x;
            this.y = y;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// Gets the coordinates of a latitude,longitude point using this point as a reference.
        /// </summary>
        /// <param name="latitude">The latitude of the point you wish to get.</param>
        /// <param name="longitude">The latitude of the point you wish to get.</param>
        /// <returns></returns>
        public Vector2 getCoordinatesFromLatitudeAndLongitude(float latitude, float longitude)
        {
            var img = ImageRegistry.warMap.get();
            var mapWidth = img.width;
            var mapHeight = img.height;
            var x = (longitude + 180) * (mapWidth / 360f);
            var latRad = latitude * Mathf.PI / 180f;
            var mercN = Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latRad / 2)));
            var y = (mapHeight / 2f) - (mapWidth * mercN / (2f * Mathf.PI));

            return new Vector2(x, y);
        }
    }
}