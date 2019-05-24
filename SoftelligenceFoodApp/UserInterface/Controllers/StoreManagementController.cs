﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Abstractions;
using Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class StoreManagementController : Controller
    {
        private AdminService adminService;
        public StoreManagementController(AdminService adminService)
        {
            this.adminService = adminService;
        }
        public IActionResult Index()
        {
            ViewBag.ViewName = "StoreManagement";
            var storesList = new List<StoreVM>();

            var storeList = adminService.GetAllStores();

            foreach (var store in storeList)
            {
                var storeToDisplay = new StoreVM();
                storeToDisplay.Id = store.Id;
                storeToDisplay.Name = store.Name;
                if (store.Menu != null)
                {
                    if (store.Menu.Hyperlink != null)
                    {
                        storeToDisplay.Hyperlink = store.Menu.Hyperlink;
                    }
                    if (store.Menu.Image != null)
                    {
                        storeToDisplay.Image = store.Menu.Image;
                    }
                }
                
                storesList.Add(storeToDisplay);
            }

            return View(storesList);
        }

        [HttpPost]
        public IActionResult Add([FromForm]StoreVM newStore)
        {
            if(ModelState.IsValid)
            {
                var storeToAdd = new Store
                {
                    Name = newStore.Name,
                    Menu = new Menu
                    {
                        Hyperlink = newStore.Hyperlink,
                        Image = newStore.Image
                    }
                };
                adminService.AddStore(storeToAdd);

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            var store = new StoreVM();
            return View(store);
        }

        public IActionResult Details(int? id)
        {
            Store store = new Store();
            var storeRepo = adminService.GetAllStores();
            foreach(var item in storeRepo)
            {
                if(item.Id == id)
                {
                    store = item;
                }
            }
            return View(store);
        }

        public IActionResult Update(int? id)
        {
            Store store = new Store();
            var storeRepo = adminService.GetAllStores();
            foreach (var item in storeRepo)
            {
                if (item.Id == id)
                {
                    store = item;
                }
            }
            return View(store);
        }

    }
}