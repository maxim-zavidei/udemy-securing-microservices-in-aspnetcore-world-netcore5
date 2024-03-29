﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jobs.Mvc.Models;
using Jobs.Mvc.Services;
using Microsoft.AspNetCore.Authorization;

namespace Jobs.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IJobService jobService;

    public HomeController(ILogger<HomeController> logger, IJobService jobService)
    {
        _logger = logger;
        this.jobService = jobService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var jobs = await jobService.GetJobs();
        return View(jobs);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
