﻿using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiayDep.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly GiaydepContext _context;
        public MenuViewComponent (GiaydepContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var lstSP = _context.SanPhams
               .Include(n => n.ManhaccNavigation)
               .Include(n => n.MaloaispNavigation);
            return View(lstSP);
        }
    }
}
