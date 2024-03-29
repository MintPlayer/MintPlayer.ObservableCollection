﻿using System;
using System.Linq;
using System.Threading;
using MintPlayer.ObservableCollection.Test.Extensions;

namespace MintPlayer.ObservableCollection.Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Demo1();
            Demo2();
            //Demo3();
            Console.ReadKey();
        }

        private static void Demo1()
        {
            var collection = new ObservableCollection<string>();
            collection.CollectionChanged += (sender, e) =>
            {
                Console.WriteLine($"Collection changed:");
                if (!e.NewItems.IsNullOrEmpty())
                {
                    var newItemsArray = new string[e.NewItems.Count];
                    e.NewItems.CopyTo(newItemsArray, 0);
                    Console.WriteLine($"- items added: {string.Join(", ", newItemsArray)}");
                }
                if (!e.OldItems.IsNullOrEmpty())
                {
                    var oldItemsArray = new string[e.OldItems.Count];
                    e.OldItems.CopyTo(oldItemsArray, 0);
                    Console.WriteLine($"- items removed: {string.Join(", ", oldItemsArray)}");
                }
            };

            collection.Add("Michael");
            collection.Enabled = false;
            collection.Add("Junior");
            collection.Enabled = true;
            collection.Add("Jackson");
        }

        private static void Demo2()
        {
            var collection = new ObservableCollection<Person>();
            collection.CollectionChanged += (sender, e) =>
            {
                Console.WriteLine($"Collection changed:");
                if (!e.NewItems.IsNullOrEmpty())
                {
                    var newItemsArray = new Person[e.NewItems.Count];
                    e.NewItems.CopyTo(newItemsArray, 0);
                    Console.WriteLine($"- items added: {string.Join(", ", newItemsArray.Select(p => p.FullName))}");
                }
                if (!e.OldItems.IsNullOrEmpty())
                {
                    var oldItemsArray = new Person[e.OldItems.Count];
                    e.OldItems.CopyTo(oldItemsArray, 0);
                    Console.WriteLine($"- items removed: {string.Join(", ", oldItemsArray.Select(p => p.FullName))}");
                }
            };
            collection.ItemPropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"Item property changed: {e.PropertyName}");
            };

            new Thread(new ThreadStart(() =>
            {
                var person1 = new Person { FirstName = "John", LastName = "Doe" };
                var person2 = new Person { FirstName = "Jimmy", LastName = "Fallon" };
                var person3 = new Person { FirstName = "Michael", LastName = "Douglas" };

                // Add 3 items at once
                collection.AddRange(new[] {
                    person1,
                    person2,
                    person3
                });

                // Change an item property
                collection[1].LastName = "Knibble";

                // Replace item
                collection[1] = new Person { FirstName = "Sim", LastName = "Salabim" };

                // Remove 2 items at once
                collection.RemoveRange(new[] { person1, person3 });
            })).Start();
        }

        private static void Demo3()
        {
            var col = new ObservableCollection<Person>();
            var people = new[] {
                new Person { FirstName = "Pieterjan", LastName = "De Clippel" },
                new Person { FirstName = "Sam", LastName = "Hunt" },
                new Person { FirstName = "Michael", LastName = "Jefferson" },
                new Person { FirstName = "Bill", LastName = "Belichick" },
            };
            col.AddRange(people);
        }
    }
}
