﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEntegrasyon.Models.ViewModel;

namespace MyEntegrasyon.Controllers
{
    public class JsonDesenController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult newDesen()
        {
            newJSONviewModel newJSO = new newJSONviewModel();

            List<Typeliste> _type = new List<Typeliste>();
            _type.Add(new Typeliste { Id = "Create",  deger = "Create" });
            _type.Add(new Typeliste { Id = "Read", deger = "Read" });
            _type.Add(new Typeliste { Id = "Update", deger = "Update" });
            _type.Add(new Typeliste { Id = "Delete", deger = "Delete" });

            List<Firmaliste> _firma = new List<Firmaliste>();
            _firma.Add(new Firmaliste { Id = "Ikas", deger = "Ikas" });
            _firma.Add(new Firmaliste { Id = "Nebim", deger = "Nebim" });

            ViewBag.TypeId = new SelectList(_type, "Id", "deger");
            ViewBag.FirmaId = new SelectList(_firma, "Id", "deger");
            return View();
        }

        public class Typeliste
        {
            public string? Id { get; set; }
            public string? deger { get; set; }
        }
        public class Firmaliste
        {
            public string? Id { get; set; }
            public string? deger { get; set; }
        }
        public IActionResult ikas()
        {
            return View();
        }
        public IActionResult Nebim()
        {
            return View();
        }
    }
}
