﻿using SevenFoodApp.Model;
using SevenFoodApp.Repository;
using SevenFoodApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SevenFoodApp.Util.Enums;

namespace SevenFoodApp.Controller
{
    internal class RestaurantController
    {
        RestaurantRepository repository = new RestaurantRepository(CONTEXT.RESTAURANT);
        public bool Add(string name)
        {
            int id = this.GetNextId();
            Restaurant restaurant = new Restaurant(id, name);
            return repository.Insert(restaurant);
        }

        private int GetNextId()
        {
            return Please.GetNextId();
        }

        public List<Dictionary<string, string>>? getAll()
        {

            List<Restaurant> restaurants = repository.GetAll();

            if (restaurants != null && restaurants.Count() > 0)
            {
                var restaurantsString = new List<Dictionary<string, string>>();

                foreach (var restaurant in restaurants)
                {
                    var userString = this.castObjectToDictionary(restaurant);
                    restaurantsString.Add(userString);
                }
                return restaurantsString;
            }
            return null;
        }

        public Dictionary<string, string>? getById(int id)
        {
            var restaurant = repository.GetById(id);
            return restaurant != null ? this.castObjectToDictionary(restaurant) : null;
        }

        public bool remove(int id)
        {
            return repository.Delete(id);
        }

        internal bool update(int id, string name, bool isActive)
        {
            Restaurant Restaurant = new Restaurant(id, name, isActive);
            return repository.Update(Restaurant);
        }

        private Dictionary<string, string> castObjectToDictionary(Restaurant restaurant)
        {
            var restaurantString = new Dictionary<string, string>
                {
                    { "id", restaurant.Id.ToString() },
                    { "name",  restaurant.Name },
                    { "active", Please.TranslateFromBool(restaurant.Active) },
                };
            return restaurantString;
        }
    }
}
