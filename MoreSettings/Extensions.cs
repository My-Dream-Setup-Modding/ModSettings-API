using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModSettingsApi
{
    internal static class Extensions
    {
        /// <summary>
        /// Instantiate the gameobject <paramref name="gameObject"/>, and returns the Component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Instantiate<T>(this GameObject gameObject, string name) where T : Component
        {
            var newGameObject = GameObject.Instantiate(gameObject, gameObject.transform.parent, false);
            newGameObject.name = name;
            T component = newGameObject.GetComponent<T>();
            return component;
        }
    }
}
