using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SearchJson
{
    /// <summary>
    /// Search JSON Utilities for primitive types & string
    /// </summary>
    public static class SearchJsonUtils
    {
        #region Main Methods

        /// <summary>
        /// Search the JSON for the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The JSON value</param>
        /// <param name="itemName">The item's name</param>
        /// <param name="itemValue">The first occurence of the item</param>
        /// <returns>true if the search was succesful</returns>
        public static bool TrySearchItem<T>(this string json, string itemName, out T itemValue)
            where T : struct => TrySearchItemPrivate(json, itemName, out itemValue);

        /// <summary>
        /// Search the JSON for the specified item.
        /// </summary>
        /// <param name="json">The JSON value</param>
        /// <param name="itemName">The item's name</param>
        /// <param name="itemValue">The first occurence of the item</param>
        /// <returns>true if the search was succesful</returns>
        public static bool TrySearchItem(this string json, string itemName, out string itemValue)
            => TrySearchItemPrivate(json, itemName, out itemValue);

        /// <summary>
        /// Search the JSON for the specified items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The JSON value</param>
        /// <param name="itemName">The item's name</param>
        /// <param name="itemValues">All the occurence of the item</param>
        /// <returns>true if the search was succesful</returns>
        public static bool TrySearchItems<T>(this string json, string itemName, out IEnumerable<T> itemValues) where T : struct
            => TrySearchItemsPrivate(json, itemName, out itemValues);

        /// <summary>
        /// Search the JSON for the specified items
        /// </summary>
        /// <param name="json">The JSON value</param>
        /// <param name="itemName">The item's name</param>
        /// <param name="itemValues">All the occurence of the item</param>
        /// <returns>true if the search was succesful</returns>
        public static bool TrySearchItems(this string json, string itemName, out IEnumerable<string> itemValues)
            => TrySearchItemsPrivate(json, itemName, out itemValues);

        /// <summary>
        /// Checks the validity of the specified JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns>A flag which specifies the validity of the JSON</returns>
        public static bool IsValidJson(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return false;

            json = json.Trim();

            if (!(json.StartsWith("{") && json.EndsWith("}")) && !(json.StartsWith("[") && json.EndsWith("]")))
                return false;

            try
            {
                _ = JToken.Parse(json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Search the JSON for the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The JSON value</param>
        /// <param name="itemName">The item's name</param>
        /// <param name="itemValue">The first occurence of the item</param>
        /// <returns>true if the search was succesful</returns>
        private static bool TrySearchItemPrivate<T>(this string json, string itemName, out T itemValue)
        {
            itemValue = default;
            if (!IsValidSearch(json, itemName)) return false;

            json = json.Trim();
            itemName = itemName.Trim();

            var obj = JToken.Parse(json);
            return SearchItem(itemName, obj, ref itemValue);
        }

        /// <summary>
        /// Search the JSON for the specified items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The JSON value</param>
        /// <param name="itemName">The item's name</param>
        /// <param name="itemValues">All the occurence of the item</param>
        /// <returns>true if the search was succesful</returns>
        private static bool TrySearchItemsPrivate<T>(this string json, string itemName, out IEnumerable<T> itemValues)
        {
            itemValues = default;
            var list = new List<T>();

            if (!IsValidSearch(json, itemName)) return false;

            json = json.Trim();
            itemName = itemName.Trim();

            var obj = JToken.Parse(json);
            var res = SearchItem(itemName, obj, ref list);
            itemValues = list;

            return res;
        }

        /// <summary>
        /// Checks if the search request is valid
        /// </summary>
        /// <param name="json"></param>
        /// <param name="itemName"></param>
        /// <returns>true if valud search criteria</returns>
        private static bool IsValidSearch(string json, string itemName) => (json?.IsValidJson() ?? false) && !string.IsNullOrWhiteSpace(itemName);

        /// <summary>
        /// Recursively search for the first occurence of the item.
        /// The item's value should not be null, default and if it is a string not an empty one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemName">The item's name</param>
        /// <param name="obj">The object</param>
        /// <param name="itemValue">The item's value</param>
        /// <returns>A flag which indicates the result of the search</returns>
        private static bool SearchItem<T>(string itemName, JToken obj, ref T itemValue)
        {
            if (obj == null) return false;

            itemValue = RetrieveValue<T>(itemName, obj);
            if (itemValue != null && !itemValue.Equals(default(T))) return true;

            var childrenList = obj.Children();
            foreach (var children in childrenList)
            {
                if (SearchItem(itemName, children, ref itemValue)) return true;
            }

            return false;
        }

        /// <summary>
        /// Recursively search for the all the occurence of the item.
        /// The item's value should not be null, default and if it is a string not an empty one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemName">The item's name</param>
        /// <param name="obj">The object</param>
        /// <param name="itemValues">The item's value</param>
        /// <returns>A flag which indicates the result of the search</returns>
        private static bool SearchItem<T>(string itemName, JToken obj, ref List<T> itemValues)
        {
            if (obj == null) return false;

            var itemValue = RetrieveValue<T>(itemName, obj);
            if (itemValue != null && !itemValue.Equals(default(T))) itemValues.Add(itemValue);

            var childrenList = obj.Children();
            foreach (var children in childrenList)
            {
                SearchItem(itemName, children, ref itemValues);
            }

            return itemValues.Count > 0;
        }

        /// <summary>
        /// Retrieve object value based on the item's name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemName">The item's name</param>
        /// <param name="obj">The object</param>
        /// <returns>Item's value</returns>
        private static T RetrieveValue<T>(string itemName, JToken obj)
        {
            var value = obj.SelectToken(itemName);
            if (value == null) return default;

            T convertedValue = value.GetType() != typeof(JObject) ? value.Value<T>() : default;

            if (convertedValue != null
                && !convertedValue.Equals(default(T))
                && (typeof(T) != typeof(string) || !string.IsNullOrWhiteSpace(convertedValue.ToString()))
            )
            {

                return value.Value<T>();
            }

            return default;
        }

        #endregion
    }
}