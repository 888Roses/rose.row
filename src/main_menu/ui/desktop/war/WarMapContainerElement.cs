using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.main_menu.war.war_data;
using rose.row.util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace rose.row.main_menu.ui.desktop.war
{
    public class CityElement : UiElement
    {
        public CityInfo city;
        public int connectionCount;
        public List<CityElement> connections = new List<CityElement>();
    }

    public class UiLinkElement : UiElement
    {
        public RectTransform from;
        public RectTransform to;
        public float thickness = 2.5f;

        private Vector3 _storedFromPos;
        private Vector3 _storedToPos;

        protected override void Awake()
        { }

        public override void build()
        {
            image();
        }

        private void Update()
        {
            if (_storedFromPos != from.position || _storedToPos != to.position)
            {
                _storedFromPos = from.position;
                _storedToPos = to.position;

                rectTransform.position = (from.position + to.position) / 2;
                rectTransform.right = (from.position - to.position).normalized;
                rectTransform.sizeDelta = new Vector2(Vector3.Distance(from.position, to.position), thickness);
            }
        }
    }

    public class WarMapElement : UiElement,
        IPointerDownHandler
    {
        public const float k_ZoomPercentChange = -10;
        public const float k_ZoomMax = 4f;
        public const float k_ZoomMin = 0.5f;

        public const float k_LineThickness = 1f;

        private bool _isDragging = false;

        private Vector3 _initialDraggingOffset;
        private Vector2 _originalSize;

        private readonly List<CityElement> _cities = new List<CityElement>();
        private readonly List<UiLinkElement> _links = new List<UiLinkElement>();

        protected override void Awake()
        {
            initialBuild();
        }

        private void initialBuild()
        {
            var image = ImageRegistry.warMap.get();
            this.image().texture = image;
            _originalSize = image.getSize();
            setSize(_originalSize);
            transform.position = Vector3.zero;
        }

        private void Start()
        {
            moveToBack();
            createPoints();
            createLinks();
        }

        private void createPoints()
        {
            WarCityDatabase.readCities();
            var cityList = WarCityDatabase.loadedCities.OrderBy(x => x.iso2);

            _cities.Clear();

            var lastCityIso2 = "";
            var currentColour = Color.white;

            foreach (var loadedCity in cityList)
            {
                if (loadedCity.iso2 != lastCityIso2)
                {
                    lastCityIso2 = loadedCity.iso2;
                    currentColour = Random.ColorHSV();
                }

                var city = UiFactory.createUiElement<CityElement>(loadedCity.name, this);
                city.city = loadedCity;
                setupCity(city, currentColour);
                _cities.Add(city);
            }

            updatePointsPosition();
        }

        private void setupCity(CityElement element, Color32 color)
        {
            element.setAnchors(Anchors.BottomLeft, false);
            element.setPivot(0.5f, 0.5f);
            element.setAnchoredPosition(0, 0);
            element.setSize(5, 5);
            element.image().raycastTarget = false;
            element.image().color = color;
        }

        public const int k_MaxConnectionsPerCity = 3;
        public const float k_PreferedCityDistance = 25f;

        private void createLinks()
        {
            _links.Clear();

            var cities = new List<CityElement>(_cities);
            var currentPointIndex = 0;
            var currentCity = cities[0];

            while (currentPointIndex < cities.Count)
            {
                var closestCityDist = float.MaxValue;
                CityElement closestCity = null;

                foreach (var city in cities)
                {
                    if (city == currentCity)
                        continue;

                    if (city.connectionCount >= k_MaxConnectionsPerCity)
                        continue;

                    if (city.connections.Contains(currentCity)
                        || currentCity.connections.Contains(city))
                        continue;

                    var dst = Vector3.Distance(
                        a: city.transform.position,
                        b: currentCity.transform.position
                    );

                    if (dst < closestCityDist)
                    {
                        closestCityDist = dst;
                        closestCity = city;
                    }
                }

                if (closestCity == null)
                {
                    continue;
                }

                createLink(currentCity, closestCity);

                closestCity.connectionCount++;
                closestCity.connections.Add(currentCity);
                currentCity.connectionCount++;
                currentCity.connections.Add(closestCity);

                currentCity = closestCity;
                currentPointIndex++;
            }
        }

        /*
        private void createLinks()
        {
            _links.Clear();
            var operationCount = 1;
            var safetyTokens = 50;
            var cities = new List<CityElement>(_cities);

            while (operationCount > 0)
            {
                cities.OrderBy(x => x.connectionCount);

                safetyTokens--;
                if (safetyTokens <= 0)
                {
                    Debug.LogWarning($"Hit safety tokens limit!");
                    break;
                }

                operationCount = 0;
                for (int i = 0; i < cities.Count; i++)
                {
                    var currentCity = cities[i];
                    if (currentCity.connectionCount >= k_MaxConnectionsPerCity)
                        continue;

                    var currentClosestCityDistance = float.MaxValue;
                    CityElement currentClosestCity = null;
                    foreach (var otherCity in cities)
                    {
                        if (otherCity == currentCity)
                            continue;

                        if (otherCity.connectionCount >= k_MaxConnectionsPerCity)
                            continue;

                        if (otherCity.connections.Contains(currentCity)
                            || currentCity.connections.Contains(otherCity))
                            continue;

                        var dist = Vector3.Distance(currentCity.transform.position, otherCity.transform.position);
                        if (dist < currentClosestCityDistance)
                        {
                            currentClosestCityDistance = dist;
                            currentClosestCity = otherCity;
                        }
                    }

                    if (currentClosestCity == null)
                    {
                        currentCity.transform.localScale = Vector3.one * 3f;
                        continue;
                    }

                    currentClosestCity.connectionCount++;
                    currentCity.connectionCount++;

                    currentClosestCity.connections.Add(currentCity);
                    currentCity.connections.Add(currentClosestCity);

                    createLink(currentCity, currentClosestCity);
                    operationCount++;
                }
            }
        }
        */

        private UiLinkElement createLink(CityElement from, CityElement to)
        {
            var name = $"From '{from.city.name}' to '{to.city.name}'";
            var link = UiFactory.createUiElement<UiLinkElement>(name, this);
            link.from = from.rectTransform;
            link.to = to.rectTransform;
            link.thickness = k_LineThickness;
            link.build();
            link.image().color = Color.white.with(a: 0.5f);
            return link;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != 0)
                return;

            _isDragging = true;
            _initialDraggingOffset = (Vector3) rectTransform.anchoredPosition - Input.mousePosition;
        }

        private float getWidthOffset()
        {
            return Screen.width * 0.75f;
        }

        private float getHeightOffset()
        {
            return Screen.height - DesktopWindowElement.k_HeaderHeight;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                return;
            }

            if (_isDragging)
            {
                var draggedPos = Input.mousePosition + _initialDraggingOffset;
                rectTransform.anchoredPosition = draggedPos;
            }

            updateZoom();
            updateAnchoredPosition();
        }

        private void updateZoom()
        {
            var mouseScrollWheel = Mathf.Clamp(
                value: Input.GetAxisRaw("Mouse ScrollWheel"),
                min: -1,
                max: 1
            );

            if (mouseScrollWheel != 0)
            {
                var fracChange = (100f - k_ZoomPercentChange * mouseScrollWheel) / 100f;
                var newSize = size * fracChange;
                newSize.x = Mathf.Clamp(newSize.x, _originalSize.x * k_ZoomMin, _originalSize.x * k_ZoomMax);
                newSize.y = Mathf.Clamp(newSize.y, _originalSize.y * k_ZoomMin, _originalSize.y * k_ZoomMax);
                if (Vector3.Distance(newSize, size) <= 0.1f)
                    return;

                setSize(newSize);
                transform.position += (1 - fracChange) * (Input.mousePosition - transform.position);

                updatePointsPosition();
            }
        }

        private void updatePointsPosition()
        {
            foreach (var city in _cities)
            {
                var anchoredPosition = new Vector3(
                    x: city.city.longitude / 360f * size.x,
                    y: city.city.latitude / 180f * size.y,
                    z: 0
                );
                anchoredPosition += (Vector3) size / 2;

                city.setAnchoredPosition(anchoredPosition);
            }
        }

        /// <summary>
        /// Updates the anchored position to make sure that it stays within the bounds of it's parent.
        /// </summary>
        private void updateAnchoredPosition()
        {
            var anchoredPos = rectTransform.anchoredPosition;

            var w = getWidth() - getWidthOffset();
            var h = getHeight() - getHeightOffset();

            anchoredPos.x = Mathf.Clamp(anchoredPos.x, -w / 2, w / 2);
            anchoredPos.y = Mathf.Clamp(anchoredPos.y, -h / 2, h / 2);
            //anchoredPos.x = Mathf.Clamp(anchoredPos.x, -(getWidth() + getWidthOffset()) / 2, (getWidth() - getWidthOffset()) / 2);
            //anchoredPos.y = Mathf.Clamp(anchoredPos.y, -(getHeight() + getHeightOffset()) / 2, (getHeight() - getHeightOffset()) / 2);
            rectTransform.anchoredPosition = anchoredPos;
        }
    }

    public class WarMapContainerElement : UiElement
    {
        #region components

        private WarMapElement _map;

        #endregion components

        protected override void Awake()
        { }

        public override void build()
        {
            initializeMask();
            createMap();
        }

        private void initializeMask()
        {
            image();
            var mask = use<Mask>();
            mask.showMaskGraphic = false;
        }

        private void createMap()
        {
            //_map = UiFactory.createUiElement<WarMapElement>("War Map", this);
            //_map.setAnchors(anchors: Anchors.MiddleCenter, updateOffsets: false);
            //_map.setPivot(0.5f, 0.5f);
        }
    }
}